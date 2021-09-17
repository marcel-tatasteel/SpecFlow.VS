﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;

namespace SpecFlow.VisualStudio.Editor.Commands
{
    internal class RenameStepStepDefinitionClassAction : RenameStepAction
    {
        public override void PerformRenameStep(RenameStepCommandContext ctx)
        {
            GetMethod(ctx);
            if (ctx.IsErroneous) return;

            var expressionsToReplace = ExpressionsToReplace(ctx);
            if (ctx.IsErroneous) return;

            //TODO: The replacement text should be `@"expression"` if expression contains \ or " (" must be doubled) OR `"expression"` otherwise
            EditTextBuffer(ctx.TextBufferOfStepDefinitionClass, expressionsToReplace, CalculateReplaceSpan, ctx.UpdatedExpression);

            ctx.ProjectOfStepDefinitionClass.IdeScope.Logger.Log(TraceLevel.Info, ctx.Method.AttributeLists.Count.ToString());
        }

        private void GetMethod(RenameStepCommandContext ctx)
        {
            var syntaxTree = ctx.IdeScope.GetSyntaxTree(ctx.TextBufferOfStepDefinitionClass);
            if (!syntaxTree.TryGetRoot(out SyntaxNode? rootNode)){
                ctx.AddProblem("Couldn't find syntax root");
                return;
            }

            var methodLine =
                ctx.TextBufferOfStepDefinitionClass.CurrentSnapshot.GetLineFromLineNumber(ctx.StepDefinitionBinding.Implementation
                    .SourceLocation.SourceFileLine - 1);
            var methodColumn = ctx.StepDefinitionBinding.Implementation.SourceLocation.SourceFileColumn - 1;
            var methodPosition = methodLine.Start + methodColumn;
            var node = rootNode.FindNode(new TextSpan(methodPosition, 1));

            ctx.Method = node.Parent as MethodDeclarationSyntax;
            if (ctx.Method == null)
            {
                ctx.AddProblem($"Method not found for {ctx.StepDefinitionBinding}.");
            }
        }

        private static SyntaxToken[] ExpressionsToReplace(RenameStepCommandContext ctx)
        {
            var attributesWithMatchingExpression = GetAttributesWithTokens(ctx.Method)
                .Where(awt => !awt.Item2.IsMissing && MatchesWithOriginalText(awt.Item2))
                .ToArray();

            if (attributesWithMatchingExpression.Length > 1)
                attributesWithMatchingExpression =
                    attributesWithMatchingExpression
                        .Where(awt => MatchesAttributeNameWithStepType(awt.Item1))
                        .ToArray();

            var stepDefinitionAttributeTextTokens =
                attributesWithMatchingExpression
                    .Select(awt => awt.Item2)
                    .OrderByDescending(tok => tok.SpanStart)
                    .ToArray();

            if (stepDefinitionAttributeTextTokens.Length == 0)
            {
                ctx.AddProblem($"No expressions found to replace for {ctx.StepDefinitionBinding}");
            }

            return stepDefinitionAttributeTextTokens;

            bool MatchesWithOriginalText(SyntaxToken tok) => tok.ValueText == ctx.OriginalExpression;
            bool MatchesAttributeNameWithStepType(AttributeSyntax a) => ctx.StepDefinitionBinding.StepDefinitionType.ToString().Equals(a.Name.ToString());
        }

        internal static IEnumerable<Tuple<AttributeSyntax, SyntaxToken>> GetAttributesWithTokens(MethodDeclarationSyntax method)
        {
            return method.AttributeLists
                .SelectMany(al => al.Attributes)
                .Select(a => new Tuple<AttributeSyntax, SyntaxToken>(a, GetAttributeToken(a)));
        }

        private static SyntaxToken GetAttributeToken(AttributeSyntax attributeSyntax)
        {
            AttributeArgumentListSyntax? attributeArgumentListSyntax = attributeSyntax.ArgumentList;
            return attributeArgumentListSyntax == null || attributeArgumentListSyntax.Arguments.Count == 0
                ? SyntaxFactory.MissingToken(SyntaxKind.StringLiteralToken)
                : attributeArgumentListSyntax.Arguments.Single().Expression.GetFirstToken();
        }

        private static Span CalculateReplaceSpan(SyntaxToken token)
        {
            var offset = IsVerbatim(token) ? 2 : 1;

            var replaceSpan = new Span(token.SpanStart + offset, token.Span.Length - offset - 1);
            return replaceSpan;
        }

        private static bool IsVerbatim(SyntaxToken token)
        {
            return token.Text.StartsWith("@");
        }
    }
}
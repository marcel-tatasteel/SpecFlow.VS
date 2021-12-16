﻿global using Microsoft;
global using Microsoft.CodeAnalysis;
global using Microsoft.CodeAnalysis.CSharp;
global using Microsoft.CodeAnalysis.CSharp.Syntax;
global using Microsoft.CodeAnalysis.Text;
global using Microsoft.VisualStudio;
global using Microsoft.VisualStudio.ApplicationInsights;
global using Microsoft.VisualStudio.ApplicationInsights.Channel;
global using Microsoft.VisualStudio.ApplicationInsights.DataContracts;
global using Microsoft.VisualStudio.ApplicationInsights.Extensibility;
global using Microsoft.VisualStudio.Editor;
global using Microsoft.VisualStudio.Language.Intellisense;
//global using Microsoft.VisualStudio.OLE.Interop; //Causes many conflicts with System.IServiceProvider 
global using Microsoft.VisualStudio.Shell;
global using Microsoft.VisualStudio.Shell.Interop;
global using Microsoft.VisualStudio.Text;
global using Microsoft.VisualStudio.Text.Editor;
global using Microsoft.VisualStudio.Text.Editor.OptionsExtensionMethods;
global using Microsoft.VisualStudio.Text.Tagging;
global using Microsoft.VisualStudio.TextManager.Interop;
global using Microsoft.VisualStudio.Threading;
global using Microsoft.VisualStudio.Utilities;
global using SpecFlow.VisualStudio.Analytics;
global using SpecFlow.VisualStudio.Annotations;
global using SpecFlow.VisualStudio.Common;
global using SpecFlow.VisualStudio.Configuration;
global using SpecFlow.VisualStudio.Diagnostics;
global using SpecFlow.VisualStudio.Discovery;
global using SpecFlow.VisualStudio.Editor.Commands;
global using SpecFlow.VisualStudio.Editor.Commands.Infrastructure;
global using SpecFlow.VisualStudio.Editor.Completions.Infrastructure;
global using SpecFlow.VisualStudio.Editor.Services;
global using SpecFlow.VisualStudio.Editor.Services.Parser;
global using SpecFlow.VisualStudio.Editor.Services.StepDefinitions;
global using SpecFlow.VisualStudio.Generation;
global using SpecFlow.VisualStudio.Monitoring;
global using SpecFlow.VisualStudio.Notifications;
global using SpecFlow.VisualStudio.ProjectSystem;
global using SpecFlow.VisualStudio.ProjectSystem.Actions;
global using SpecFlow.VisualStudio.ProjectSystem.Configuration;
global using SpecFlow.VisualStudio.ProjectSystem.Settings;
global using SpecFlow.VisualStudio.Snippets;
global using SpecFlow.VisualStudio.Snippets.Fallback;
global using SpecFlow.VisualStudio.SpecFlowConnector;
global using SpecFlow.VisualStudio.UI.ViewModels;
global using System.Collections;
global using System.Collections.Immutable;
global using System.ComponentModel.Composition;
global using System.IO.Abstractions;
global using System.Runtime.CompilerServices;
global using System.Windows.Threading;

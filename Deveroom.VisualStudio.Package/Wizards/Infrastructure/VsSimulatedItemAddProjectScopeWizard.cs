﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using Deveroom.VisualStudio.Diagonostics;
using EnvDTE;

namespace Deveroom.VisualStudio.Wizards.Infrastructure
{
    public abstract class VsSimulatedItemAddProjectScopeWizard<TWizard> : VsProjectScopeWizard<TWizard> where TWizard : class, IDeveroomWizard
    {
        private bool _usingMicrosoftNetSdk = false;
        private bool _enableSimulatedItemAdd = false;
        private string _templateFileName;

        protected override bool RunStarted(Project project, WizardRunParameters wizardRunParameters, TWizard wizard)
        {
            _usingMicrosoftNetSdk = GetUsingMicrosoftNETSdk(project);
            _enableSimulatedItemAdd = 
                wizardRunParameters.IsAddNewItem && 
                _usingMicrosoftNetSdk && 
                wizardRunParameters.TargetFolder != null;

            if (_enableSimulatedItemAdd)
                Logger?.LogVerbose($"Using simulated item add for project '{project.Name}'");

            return base.RunStarted(project, wizardRunParameters, wizard);
        }

        private bool GetUsingMicrosoftNETSdk(Project project)
        {
            var propValue = VsUtils.GetMsBuildPropertyValue(project, "UsingMicrosoftNETSdk");
            return string.Equals(propValue, "true", StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool ShouldAddProjectItem(string filePath)
        {
            _templateFileName = filePath;
            return base.ShouldAddProjectItem(filePath) && !_enableSimulatedItemAdd;
        }

        public override void RunFinished()
        {
            if (_isValidRun && _enableSimulatedItemAdd && _templateFileName != null)
            {
                var targetFile = Path.Combine(_wizardRunParameters.TargetFolder, _wizardRunParameters.TargetFileName);
                var sourceFile = Path.Combine(_wizardRunParameters.TemplateFolder, _templateFileName);
                CopyWithTemplateParamResolution(sourceFile, targetFile);
                ScheduleOpenFile(targetFile);
            }

            base.RunFinished();
        }

        private void CopyWithTemplateParamResolution(string sourceFile, string targetFile)
        {
            var fileSystem = _wizardRunParameters.ProjectScope.IdeScope.FileSystem;
            if (fileSystem == null)
                return;

            var replacementsDictionary = new Dictionary<string, string>(_wizardRunParameters.ReplacementsDictionary);
            replacementsDictionary["$itemname$"] = Path.GetFileNameWithoutExtension(targetFile);

            try
            {
                var fileContent = fileSystem.File.ReadAllText(sourceFile);
                var updatedFileContent = Regex.Replace(fileContent, @"\$[^\$\s]+\$", 
                    match => replacementsDictionary.TryGetValue(match.Value, out var value) ? value : match.Value);
                fileSystem.File.WriteAllText(targetFile, updatedFileContent, Encoding.UTF8);
                Logger.LogVerbose($"New file created: {targetFile}");
            }
            catch (Exception ex)
            {
                Logger.LogException(MonitoringService, ex);
            }
        }

        private DispatcherTimer _openFileTimer;

        private void ScheduleOpenFile(string targetFile)
        {
            var project = _project;
            var logger = Logger;
            _openFileTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.ContextIdle,
                (sender, args) =>
                {
                    _openFileTimer.Stop();
                    _openFileTimer = null;
                    OpenFile(targetFile, project);
                }, Dispatcher.CurrentDispatcher);
            _openFileTimer.Start();
        }

        private void OpenFile(string targetFile, Project project)
        {
            try
            {
                var projectItem = VsUtils.FindProjectItemByFilePath(project, targetFile);
                if (projectItem != null)
                {
                    //projectItem.Open();
                    project.DTE.ExecuteCommand("File.OpenFile", targetFile);
                    Logger.LogVerbose($"File opened: {targetFile}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(MonitoringService, ex);
            }
        }
    }
}

﻿using CleanArchitectureCQRSTemplate.Common;
using CleanArchitectureCQRSTemplate.Forms;
using EnvDTE;
using Microsoft.Build.Construction;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanArchitectureCQRSTemplate.WizardImp
{
    /// <summary>
    /// DDD Taiwan 2020 的 Clean Architecture (CQRS) 的 Infrastructure 層範本.
    /// </summary>
    public class InfrastructureLayerWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            string PERSISTANCE_FOLDER = "Persistance";
            ProjectItem modelsFolder = Utils.GetProjectItemFolder(project, PERSISTANCE_FOLDER)
                .FirstOrDefault();

            Utils.CreateDbContextFromSourceTables(project, modelsFolder);

            List<string> projectNames = new List<string>();

            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            foreach (Project prj in project.DTE.Solution.Projects)
            {
                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

                if (prj.Name != project.Name)
                {
                    projectNames.Add(prj.Name);
                }
            }

            if(projectNames.Count() > 0)
            {
                frmAddProjectsRef addPrjRef = new frmAddProjectsRef(projectNames);

                System.Windows.Forms.DialogResult settingResult = addPrjRef.ShowDialog();

                IEnumerable<string> selectedProjectNames = frmAddProjectsRef.GetSelectCheckListBox.SelectedItems.OfType<string>().ToArray();

                ProjectRootElement projectRoot = ProjectRootElement.Open(project.FullName);

                foreach (var selectedPrjName in selectedProjectNames)
                {
                    if (selectedPrjName != project.Name)
                    {
                        ProjectItemGroupElement projectGroup01 = projectRoot.AddItemGroup();
                        projectGroup01.AddItem("ProjectReference", $"..\\{selectedPrjName}\\{selectedPrjName}.csproj");
                    }
                }

                projectRoot.Save();
            }
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            frmMyORMappingWindow infraForm = new frmMyORMappingWindow();
            GlobalVar.GetSetMyORMMappingWindowForm = infraForm;
            System.Windows.Forms.DialogResult result = infraForm.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK)
            {

            }
            else
            {
                throw new WizardCancelledException("使用者取消！");
            }

        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}

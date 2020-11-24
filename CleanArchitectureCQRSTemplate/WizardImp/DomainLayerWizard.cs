using CleanArchitectureCQRSTemplate.Common;
using CleanArchitectureCQRSTemplate.Forms;
using EnvDTE;
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
    /// DDD Taiwan 2020 的 Clean Architecture (CQRS) 的 Domain 層範本.
    /// </summary>
    public class DomainLayerWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string currentEndNamsSpace = "";
            string entityFolderName = "Entities";
            ProjectItem entityFolder = null; 

            entityFolder = Utils.CreateRootFolder(project, entityFolderName, entityFolder);

            foreach (string node in frmEntitiesCreateWindow.SelectedTables)
            {
                Utils.CreateEnitiesFromSourceTables(
                    project,
                    currentEndNamsSpace, 
                    entityFolder, 
                    node, 
                    node, 
                    true, 
                    CLASS_TYPE.ENTITY);
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
            frmEntitiesCreateWindow entWindow = new frmEntitiesCreateWindow();
            DialogResult result = entWindow.ShowDialog();

            if(result == DialogResult.OK)
            {

            }
            else
            {
                throw new WizardCancelledException("使用者取消作業！");
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true; ;
        }
    }
}

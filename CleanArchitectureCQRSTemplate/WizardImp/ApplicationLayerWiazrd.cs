using CleanArchitectureCQRSTemplate.ClassesDef;
using CleanArchitectureCQRSTemplate.Common;
using CleanArchitectureCQRSTemplate.Forms;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanArchitectureCQRSTemplate.WizardImp
{
    /// <summary>
    /// Clean Architecture (CQRS) 的 Application 範本精靈.
    /// </summary>
    public class ApplicationLayerWiazrd : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        /// <summary>
        /// 當整個專案正在進行 Generator 時所引發的事件.
        /// </summary>
        /// <param name="project">由 IDE 工具傳入目前的正在載入的專案 Project （定義／內容）</param>
        public void ProjectFinishedGenerating(Project project)
        {
            if (GlobalVar.GetSetApplicationLayerWizardForm == null)
            {
                return;
            }

            string QUERY_COMMAND_FOLDER = QUERY_COMMAND_FOLDER = GlobalVar.GetSetApplicationLayerWizardForm.GetQueryDtoName;
            ProjectItem CQRSFolder = null;

            #region 勾選建立 CQRS Query Command
            // 勾選建立 CQRS Query Command
            if (GlobalVar.GetSetApplicationLayerWizardForm.ChkQueryCommandState)
            {
                #region 建立 CQRS 主資料夾 & Command 資料夾
                string queryFolderName = "Queries";
                
                ProjectItem queryFolder = null;

                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

                CQRSFolder = Utils.CreateCQRSCommandFolder(project, QUERY_COMMAND_FOLDER, CQRSFolder);

                // 建立資料夾
                queryFolder = Utils.GetOrCreateCurrentFolder(CQRSFolder, queryFolderName);
                Utils.CreateCQRSQueryCommandClassFromSource(
                    project,
                    queryFolder,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryCommandName,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryDtoName);

                #endregion

                // 建立 Query Command DTO 物件
                Utils.CreateEnitiesFromSourceTables(
                    project,
                    queryFolderName,
                    queryFolder,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryDtoName,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryCommandName,
                    false,
                    CLASS_TYPE.DTO);

                var interfaceFolder = project.ProjectItems.OfType<ProjectItem>()
                    .Where(c => c.Name == "Interfaces")
                    .FirstOrDefault();
            }
            #endregion

            #region 勾選建立 CQRS Create Command Handler
            if (GlobalVar.GetSetApplicationLayerWizardForm.ChkCreateCommandState)
            {
                if(CQRSFolder == null)
                {
                    CQRSFolder = Utils.CreateCQRSCommandFolder(project, QUERY_COMMAND_FOLDER, CQRSFolder);
                }

                ProjectItem currentFolder = null;
                string queryFolderName = "Commands";
                currentFolder = Utils.GetOrCreateCurrentFolder(CQRSFolder, queryFolderName);
                Utils.CreateCQRSCreateCommandClassFromSource(
                    project,
                    currentFolder,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryCommandName,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryDtoName);
            }
            #endregion

            #region 勾選建立 CQRS Update Command Handler
            if (GlobalVar.GetSetApplicationLayerWizardForm.ChkUpdateCommandState)
            {
                if (CQRSFolder == null)
                {
                    CQRSFolder = Utils.CreateCQRSCommandFolder(project, QUERY_COMMAND_FOLDER, CQRSFolder);
                }

                ProjectItem currentFolder = null;
                string queryFolderName = "Commands";
                currentFolder = Utils.GetOrCreateCurrentFolder(CQRSFolder, queryFolderName);
                Utils.UpdateCQRSCreateCommandClassFromSource(
                    project,
                    currentFolder,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryCommandName,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryDtoName);
            }
            #endregion

            #region 勾選建立 CQRS Delete Command Handler
            if (GlobalVar.GetSetApplicationLayerWizardForm.ChkDeleteCommandState)
            {
                if (CQRSFolder == null)
                {
                    CQRSFolder = Utils.CreateCQRSCommandFolder(project, QUERY_COMMAND_FOLDER, CQRSFolder);
                }

                ProjectItem currentFolder = null;
                string queryFolderName = "Commands";
                currentFolder = Utils.GetOrCreateCurrentFolder(CQRSFolder, queryFolderName);
                Utils.DeleteCQRSCreateCommandClassFromSource(
                    project,
                    currentFolder,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryCommandName,
                    GlobalVar.GetSetApplicationLayerWizardForm.GetQueryDtoName);
            }
            #endregion
        }



        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        /// <summary>
        /// (RunStart事件)開始產生新的專案.
        /// </summary>
        /// <param name="automationObject">目前執行的 Visual Studio 的執行個體.</param>
        /// <param name="replacementsDictionary">專案的所有文字檔案內容</param>
        /// <param name="runKind">指定會定義範本精靈可建立之不同範本的常數</param>
        /// <param name="customParams"></param>
        public void RunStarted(
            object automationObject, 
            Dictionary<string, string> replacementsDictionary, 
            WizardRunKind runKind, object[] customParams)
        {
            frmApplicationLayerWizard inputAppForm = new frmApplicationLayerWizard();
            GlobalVar.GetSetApplicationLayerWizardForm = inputAppForm;

            DialogResult result = inputAppForm.ShowDialog();

            if(result ==  DialogResult.OK)
            {

            }
            else
            {
                throw new WizardCancelledException("使用者取消操作！");
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}

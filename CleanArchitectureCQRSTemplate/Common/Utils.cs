using CleanArchitectureCQRSTemplate.ClassesDef;
using CleanArchitectureCQRSTemplate.Data;
using CleanArchitectureCQRSTemplate.Forms;
using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Utils
    {
        #region 建立供 Model 使用的 Entity 檔案
        /// <summary>
        /// 建立供 Model 使用的 Entity 檔案
        /// </summary>
        /// <param name="ClassDef">Class 內容定義</param>
        /// <param name="ClassFillPath">要建立的 Class 檔案的完整路徑</param>
        /// <returns>傳回建立的 CS 檔案路徑</returns>
        public static void CreateModelCSFile(string ClassDef, string ClassFullPath)
        {
            FileStream fs = new FileStream(ClassFullPath, FileMode.Create);
            try
            {
                StreamWriter sw = new StreamWriter(fs);
                try
                {
                    sw.WriteLine(ClassDef);
                }
                finally
                {
                    sw.Close();
                }
            }
            finally
            {
                fs.Close();
            }
        }
        #endregion

        #region 取得 Project 內的資料夾 (ProjectItem 物件)
        /// <summary>
        /// 取得 Project 內的資料夾 (ProjectItem 物件)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="QUERY_COMMAND_FOLDER"></param>
        /// <returns></returns>
        public static IEnumerable<ProjectItem> GetProjectItemFolder(Project project, string QUERY_COMMAND_FOLDER)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            return project.ProjectItems.OfType<ProjectItem>().Where(c => c.Name.ToLower() == QUERY_COMMAND_FOLDER.ToLower());
        }
        #endregion

        #region 透過資料夾名稱取得專案的 ProjectItem 物件執行個體
        /// <summary>
        /// 取得資料夾 Folder 的 ProjectItem 執行個體
        /// </summary>
        /// <param name="CQRSFolder"></param>
        /// <param name="queryFolderName"></param>
        /// <returns></returns>
        public static ProjectItem GetOrCreateCurrentFolder(ProjectItem CQRSFolder, string queryFolderName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            ProjectItem currentFolder;
            try
            {
                currentFolder = CQRSFolder.ProjectItems.AddFolder(queryFolderName);
            }
            catch (NullReferenceException nex)
            {
                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

                var tmpQueryFolder = CQRSFolder.ProjectItems.OfType<ProjectItem>()
#pragma warning disable VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .Where(c => c.Name == queryFolderName)
#pragma warning restore VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .FirstOrDefault();

                currentFolder = tmpQueryFolder;
            }

            return currentFolder;
        }
        #endregion

        #region 建立 CQRS 使用的 Command 資料夾
        /// <summary>
        /// 建立 CQRS 使用的 Command 資料夾
        /// </summary>
        /// <param name="project"></param>
        /// <param name="QUERY_COMMAND_FOLDER"></param>
        /// <param name="CQRSFolder"></param>
        /// <returns></returns>
        public static ProjectItem CreateCQRSCommandFolder(Project project, string QUERY_COMMAND_FOLDER, ProjectItem CQRSFolder)
        {
            IEnumerable<ProjectItem> resultEntity = Utils.GetProjectItemFolder(project, QUERY_COMMAND_FOLDER);

            if (resultEntity.FirstOrDefault() == null)
            {
#pragma warning disable VSTHRD108 // 無條件判斷提示執行緒親和性
                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
#pragma warning restore VSTHRD108 // 無條件判斷提示執行緒親和性

                CQRSFolder = CreateRootFolder(project, QUERY_COMMAND_FOLDER, CQRSFolder);
            }
            else
            {
                //若 Entities 這個目錄已經存在，則直接取得這個目錄.
                CQRSFolder = resultEntity.FirstOrDefault();
            }

            CQRSFolder = project.ProjectItems.OfType<ProjectItem>()
                        .Where(c => c.Name.ToLower() == QUERY_COMMAND_FOLDER.ToLower())
                        .FirstOrDefault();

            return CQRSFolder;
        }

        /// <summary>
        /// 建立專案 Root 資料夾
        /// </summary>
        /// <param name="project"></param>
        /// <param name="ROOT_FOLDER_NAME"></param>
        /// <param name="newFolder"></param>
        /// <returns></returns>
        public static ProjectItem CreateRootFolder(Project project, string ROOT_FOLDER_NAME, ProjectItem newFolder)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                newFolder = project.ProjectItems.AddFolder(ROOT_FOLDER_NAME);
            }
            catch (NullReferenceException nex) //指攔下 NullReferenceException
            {
                // VS2019 的非同步機制會在這裡取不到剛建立的資料夾，所以暫時忽略此錯誤 NullReferenceException 訊息
                // WriteLog
            }
            catch(InvalidOperationException ioex) 
            {
                // 當資料夾存在時，回傳 InvalidOperationEException 因此重新取得存在的資料夾即可
                newFolder = project.ProjectItems
                    .OfType<ProjectItem>()
#pragma warning disable VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .Where(c => c.Name.ToLower() == ROOT_FOLDER_NAME.ToLower())
#pragma warning restore VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .FirstOrDefault();
            }

            return newFolder;
        }
        #endregion

        #region 產生 DbContext 定義
        /// <summary>
        /// 產生 DbContext 定義
        /// </summary>
        /// <param name="project"></param>
        /// <param name="INTERFACE_FOLDER"></param>
        /// <param name="modelsFolder"></param>
        public static void CreateDbContextFromSourceTables(Project project, ProjectItem modelsFolder)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string DbContextDefined = DbContextDef.GetClassTemplate;
            string EntitiesName = "ApplicationDbContext"; // string.Format("{0}Model", ConnectionServices.ConnectionInfo.Initial_Catalog).ToUpperFirstWord().Replace(" ", "");
            string DbSetDefined = string.Empty;

            DbContextDefined = DbContextDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));
            DbContextDefined = DbContextDefined.Replace("$(ENTITIES_DEF)$", EntitiesName);

            string DbContextFileName = $"{EntitiesName}.cs"; //string.Format("{0}Context.cs", EntitiesName);

            SQLStore store = new SQLStore();

            //int count = 0;
            DbSetDefined += "/* 請參考 Domain 專案後再將之取消註解\r\n";
            foreach (string node in frmMyORMappingWindow.SelectedTables)
            {
                DbSetDefined += string.Format("{1}public virtual DbSet<{0}> {0} {{ get; set; }}\r\n", node.ToUpperFirstWord().Replace(" ", "_"), "\t\t");
                //count++;
            }
            DbSetDefined += "\t\t*/";

            DbContextDefined = DbContextDefined.Replace("$(DB_SET_DEF)$", DbSetDefined);

            //產生 DbContext 檔案，使用檔名 InitialCalog+Model+Context.cs 並先暫放在 Temp 資料夾下.
            string TempInterfacePath = AddFile2ProjectItem(modelsFolder, DbContextDefined, DbContextFileName);

            //刪除掉暫存檔案
            try
            {
                File.Delete(TempInterfacePath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
        }
        #endregion

        #region 初始化專案 Project 內容
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <param name="currentFolder"></param>
        /// <param name="commandClassName"></param>
        /// <param name="dtoClassName"></param>
        public static void CreateCQRSCreateCommandClassFromSource(Project project, ProjectItem currentFolder, string commandClassName, string dtoClassName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            SQLStore store = new SQLStore();
            StringBuilder sb = new StringBuilder();
            int columnOrder = 0;

            string commandHandlerDefined = CreateCommandDef.GetClassTemplate;

            commandHandlerDefined = commandHandlerDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));
            commandHandlerDefined = commandHandlerDefined.Replace("$(CREATE_COMMAND_NAME)$", commandClassName);
            
            ClassDef.GetClassProperties(store.GetNoDataDataTableByName(dtoClassName), new string[] { }, sb, columnOrder);
            commandHandlerDefined = commandHandlerDefined.Replace("$(CLASS_PROPERTIES_DEF)$", sb.ToString());

            string CommandHandlerFileName = $"Create{commandClassName}Command.cs";

            //產生 GenericRepository 檔案，使用檔名 GenericRepository.cs 並先暫放在 Temp 資料夾下.
            string TempPath = AddFile2ProjectItem(currentFolder, commandHandlerDefined, CommandHandlerFileName);
            //刪除掉暫存檔案
            try
            {
                File.Delete(TempPath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
        }
        /// <summary>
        /// 建立 CQRS 的 Query Command Handler 定義
        /// </summary>
        /// <param name="project"></param>
        /// <param name="MODEL_FOLDER"></param>
        /// <param name="currentFolder"></param>
        /// <param name="commandClassName"></param>
        /// <param name="dtoClassName"></param>
        public static void CreateCQRSQueryCommandClassFromSource(Project project, ProjectItem currentFolder, string commandClassName, string dtoClassName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string commandHandlerDefined = QueriesDef.GetCQRSQueryCommandTemplate;
            //string DbContextName = string.Format("{0}Model", ConnectionServices.ConnectionInfo.Initial_Catalog).ToUpperFirstWord().Replace(" ", "");

            commandHandlerDefined = commandHandlerDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));
            commandHandlerDefined = commandHandlerDefined.Replace("$(QUERY_COMMAND_NAME)$", commandClassName);
            commandHandlerDefined = commandHandlerDefined.Replace("$(QUERY_DTO)$", $"{dtoClassName.ToUpperFirstWord()}Dto");

            string CommandHandlerFileName = $"Get{commandClassName}Query.cs";

            //產生 GenericRepository 檔案，使用檔名 GenericRepository.cs 並先暫放在 Temp 資料夾下.
            string TempPath = AddFile2ProjectItem(currentFolder, commandHandlerDefined, CommandHandlerFileName);
            //刪除掉暫存檔案
            try
            {
                File.Delete(TempPath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
        }
        /// <summary>
        /// 在目前的 ProjectItem 的資料夾產生一個 CS 檔案
        /// </summary>
        /// <param name="currentFolder"></param>
        /// <param name="contentDefined"></param>
        /// <param name="csFileName"></param>
        /// <returns></returns>
        private static string AddFile2ProjectItem(ProjectItem currentFolder, string contentDefined, string csFileName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string TempPath = Path.Combine(
                Environment.GetEnvironmentVariable("temp"),
                csFileName);

            //建立暫存的 Interface Provider Class 檔案
            Common.Utils.CreateModelCSFile(contentDefined, TempPath);
            //加入暫存的 Class 檔案
            currentFolder.ProjectItems.AddFromFileCopy(TempPath);
            return TempPath;
        }
        #endregion

        #region 產生 Entities 的 Class 定義.
        /// <summary>
        /// 產生 Entities 的 Class 定義
        /// </summary>
        /// <param name="project"></param>
        /// <param name="currentFolder"></param>
        /// <param name="entitiesFolder"></param>
        /// <param name="classType">需要產生的類別類型 (DTO 物件／Entity 物件)</param>
        public static void CreateEnitiesFromSourceTables(
            Project project, 
            string currentFolder, 
            ProjectItem entitiesFolder, 
            string classNameOrTableName,
            string commandName,
            bool createKeyAttribute,
            CLASS_TYPE classType)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string ClassName = classNameOrTableName.Replace(" ", "_");
            ClassDef clsDef = new ClassDef();
            SQLStore store = new SQLStore();
            string ClassDefined = ClassDef.GetClassTemplate;
            string classEndWord = classType == CLASS_TYPE.DTO ? "Dto" : "Ent";
            string usingStatement = classType == CLASS_TYPE.ENTITY ? $"\nusing {project.Name}.Common;" : "";

            ClassDefined = ClassDefined.Replace("$(USING)$", usingStatement);
            if(string.IsNullOrEmpty(currentFolder))
            { 
                ClassDefined = ClassDefined.Replace(".$(FOLDER_NAME)$", currentFolder);
            }
            else
            {
                ClassDefined = ClassDefined.Replace("$(FOLDER_NAME)$", currentFolder);
            }
            ClassDefined = ClassDefined.Replace("$(FOLDER_NAME)$", currentFolder);
            ClassDefined = ClassDefined.Replace("$(NAMESPACE_DEF)$", $"{project.Name}");
            ClassDefined = ClassDefined.Replace("$(QUERY_COMMAND_NAME)$", $"{commandName}");

            ClassDefined = ClassDefined.Replace(
                    "$(CLASS_DEF)$",
                    clsDef.GetClassDef(
                        store.GetNoDataDataTableByName(classNameOrTableName), 
                        $"{ClassName.ToUpperFirstWord()}{classEndWord}", 
                        createKeyAttribute ? store.GetTableKeyByName(string.Format("{0}", ClassName)) : new string[] { }, 
                        classType));

            //產生等會使用的暫存檔名
            string TempCSPath = Path.Combine(
                Environment.GetEnvironmentVariable("temp"),
                $"{classNameOrTableName.ToUpperFirstWord()}{classEndWord}.cs");

            if (!Directory.Exists(Path.GetDirectoryName(TempCSPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(TempCSPath));
            }
            //建立暫存的 Class 檔案
            CreateModelCSFile(ClassDefined, TempCSPath);
            //加入暫存的 Class 檔案
            entitiesFolder.ProjectItems.AddFromFileCopy(TempCSPath);
            //刪除掉暫存檔案
            try
            {
                File.Delete(TempCSPath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
            //}
            
        }
        #endregion

        /*
        public static void CreateApplicationDbContextFromSource(Project project, ProjectItem currentFolder)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string applicationContextDef = DbContextDef.GetDbContextTemplate; ;
            string csFileName = "IApplicationDbContext.cs";

            applicationContextDef = applicationContextDef.Replace("$(NAMESPACE_DEF)$", project.Name);

            string TempPath = AddFile2ProjectItem(currentFolder, applicationContextDef, csFileName);

            //刪除掉暫存檔案
            try
            {
                File.Delete(TempPath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
        }
        */

        /// <summary>
        /// 將匿名型別轉換為 T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T Cast<T>(object obj, T type)
        {
            return (T)obj;
        }
    }
}

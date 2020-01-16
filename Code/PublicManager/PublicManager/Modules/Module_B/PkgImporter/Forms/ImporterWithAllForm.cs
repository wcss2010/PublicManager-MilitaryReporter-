using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTreeList.Nodes;
using PublicManager.DB;
using PublicManager.DB.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PublicManager.Modules.Module_B.PkgImporter.Forms
{
    public partial class ImporterWithAllForm : Form
    {
        private string decompressDir;
        private string totalDir;
        private MainView mainView;
        private List<string> pkgList = new List<string>();
        private LocalUnit localUnitObj;

        public ImporterWithAllForm(MainView mv, bool isImportAll, string sourceDir, string destDir)
        {
            InitializeComponent();

            totalDir = sourceDir;
            decompressDir = destDir;
            mainView = mv;

            //刷新子目录列表
            getFileTree(sourceDir);

            //判断是否为全部导入，如果是则选择所有子目录
            if (isImportAll)
            {
                //选择所有子目录
                foreach (TreeNode tn in tlTestA.Nodes)
                {
                    tn.Checked = true;
                }
            }
        }

        public void getFileTree(string path)
        {
            try
            {
                //查找zip文件
                string[] filess = System.IO.Directory.GetFiles(path);
                foreach (string f in filess)
                {
                    if (f.EndsWith(".zip"))
                    {
                        if (ZipTool.isFileInZip(f, new string[] { "static.db" }))
                        {
                            pkgList.Add(f);
                        }
                    }
                }

                //遍历目录
                string[] dirs = System.IO.Directory.GetDirectories(path);
                foreach (string s in dirs)
                {
                    //目录信息
                    System.IO.DirectoryInfo fi = new System.IO.DirectoryInfo(s);
                    string newPath = path + "/" + fi.Name;
                    getFileTree(newPath);
                }
            }
            catch (Exception ex) { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (clearProjectWithDutyUnit())
            {
                //需要替换的申报包列表
                List<TreeNode> needAddList = new List<TreeNode>();

                #region 查的需要导入的路径
                foreach (TreeNode tn in tlTestA.Nodes)
                {
                    //判断是否被选中
                    if (tn.Checked)
                    {
                        //读取目录名称中的项目编号
                        string projectNumber = tn.Text;

                        //需要替换，添加到ImportList列表中
                        needAddList.Add(tn);
                    }
                }
                #endregion

                //开始导入
                ProgressForm pf = new ProgressForm();
                pf.Show();
                pf.run(needAddList.Count, 0, new EventHandler(delegate(object senders, EventArgs ee)
                {
                    //进度数值
                    int progressVal = 0;
                    int index = -1;
                    //导入
                    foreach (TreeNode tnnn in needAddList)
                    {
                        string zipName = tnnn.Text;
                        string zipFile = tnnn.Name;

                        try
                        {
                            progressVal++;
                            index++;

                            if (tnnn.Tag == null)
                            {
                                List<string> messageList = null;
                                pf.reportProgress(progressVal, zipName + "_开始解压");
                                bool returnContent = unZipFile(zipFile, zipName, out messageList);
                                if (returnContent)
                                {
                                    //报告进度
                                    pf.reportProgress(progressVal, zipName + "_开始导入");
                                    BaseModuleMainForm.writeLog("开始导入__" + zipName);

                                    //导入数据库
                                    new DBImporter().addOrReplaceProject(zipFile, zipName, Path.Combine(Path.Combine(decompressDir, zipName), "static.db"));

                                    //报告进度
                                    pf.reportProgress(progressVal, zipName + "_结束导入");
                                    BaseModuleMainForm.writeLog("结束导入__" + zipName);
                                }
                                pf.reportProgress(progressVal, zipName + "_结束解压");
                            }
                            else
                            {
                                //报告进度
                                pf.reportProgress(progressVal, zipName + "_开始导入");
                                BaseModuleMainForm.writeLog("开始导入__" + zipName);

                                ImportDataItem idi = (ImportDataItem)tnnn.Tag;
                                idi.ProjectObj.ProfessionID = ConnectionManager.Context.table("Professions").where("ProfessionCategory='" + DBImporter.ProfessionRecordDict[idi.CatalogObj.CatalogName].ProfessionType + "'").select("ProfessionID").getValue<string>(string.Empty);
                                idi.ProjectObj.LastProfessionName = DBImporter.ProfessionRecordDict[idi.CatalogObj.CatalogName].ProfessionName;
                                idi.ProjectObj.ProfessionSort = DBImporter.ProfessionRecordDict[idi.CatalogObj.CatalogName].ProfessionSort;
                                idi.CatalogObj.copyTo(ConnectionManager.Context.table("Catalog")).insert();
                                idi.ProjectObj.copyTo(ConnectionManager.Context.table("Project")).insert();

                                //报告进度
                                pf.reportProgress(progressVal, zipName + "_结束导入");
                                BaseModuleMainForm.writeLog("结束导入__" + zipName);
                            }
                        }
                        catch (Exception ex)
                        {
                            BaseModuleMainForm.writeLog(ex.ToString());
                        }
                    }

                    //检查是否已创建句柄，并调用委托执行UI方法
                    if (pf.IsHandleCreated)
                    {
                        pf.Invoke(new MethodInvoker(delegate()
                        {
                            try
                            {
                                //刷新Catalog列表
                                mainView.updateCatalogs();

                                //关闭进度窗口
                                pf.Close();

                                //关闭窗口
                                Close();
                            }
                            catch (Exception ex)
                            {
                                BaseModuleMainForm.writeLog(ex.ToString());
                            }
                        }));
                    }
                    else
                    {
                        //关闭窗口
                        Close();
                    }
                }));
            }
        }

        private bool clearProjectWithDutyUnit()
        {
            if (string.IsNullOrEmpty(localUnitObj.LocalUnitID))
            {
                MessageBox.Show("对不起，当前数据库没有设置责任单位！");
                return false;
            }
            else
            {
                if (localUnitObj.LocalUnitName == "军委机构")
                {
                    MessageBox.Show("对不起，当前数据库不支持导入！");
                    return false;
                }
                else
                {
                    #region 删除所有这个单位的数据
                    List<Project> projList = ConnectionManager.Context.table("Project").where("DutyUnit='" + localUnitObj.LocalUnitName + "'").select("*").getList<Project>(new Project());
                    foreach (Project proj in projList)
                    {
                        ConnectionManager.Context.table("Person").where("CatalogID='" + proj.CatalogID + "'").delete();
                        ConnectionManager.Context.table("Moneys").where("CatalogID='" + proj.CatalogID + "'").delete();
                        ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").delete();
                        ConnectionManager.Context.table("Project").where("CatalogID='" + proj.CatalogID + "'").delete();
                    }
                    #endregion
                    return true;
                }
            }
        }

        /// <summary>
        /// 解压项目文件
        /// </summary>
        /// <param name="zipFile">ZIP文件</param>
        /// <param name="zipName">ZIP名称</param>
        /// <param name="outList">输入错误信息</param>
        /// <returns></returns>
        private bool unZipFile(string pkgZipFile, string zipName, out List<string> outList)
        {
            outList = new List<string>();
            //生成路径字段
            string unZipDir = System.IO.Path.Combine(decompressDir, zipName);
            //删除旧的目录
            try
            {
                Directory.Delete(unZipDir, true);
            }
            catch (Exception ex) { }
            //申报主文件夹创建
            Directory.CreateDirectory(unZipDir);

            BaseModuleMainForm.writeLog("开始解析__" + zipName);

            //判断是否存在申报包
            if (pkgZipFile != null && pkgZipFile.EndsWith(".zip"))
            {
                BaseModuleMainForm.writeLog("项目" + zipName + "的解包操作，开始ZIP文件解压");

                //解压这个包
                new ZipTool().UnZipFile(pkgZipFile, unZipDir, string.Empty, true);
                //校验文件信息
                string[] foldersValidata = MainConfig.Config.StringDict["报告验证_目录"].Split(',');
                int foldersLen = foldersValidata.Length;
                string[] filesValidata = MainConfig.Config.StringDict["报告验证_文件"].Split(',');
                int filesLen = filesValidata.Length;
                for (int i = 0; i < foldersLen; i++)
                {
                    if (!System.IO.Directory.Exists(Path.Combine(unZipDir, foldersValidata[i])))
                    {
                        BaseModuleMainForm.writeLog("项目" + zipName + "的解包操作，" + foldersValidata[i] + "文件夹不存在");
                        outList.Add(foldersValidata[i] + "文件夹 不存在");
                    }
                }
                for (int i = 0; i < filesLen; i++)
                {
                    if (!File.Exists(Path.Combine(unZipDir, filesValidata[i])))
                    {
                        BaseModuleMainForm.writeLog("项目" + zipName + "的解包操作，" + filesValidata[i] + "不存在");
                        outList.Add(filesValidata[i] + " 不存在");
                    }
                }
                BaseModuleMainForm.writeLog("项目" + zipName + "的解包操作，结束ZIP文件解压");
            }
            else
            {
                BaseModuleMainForm.writeLog("项目" + zipName + "没有找到ZIP文件");
                outList.Add("没有找到ZIP文件");
            }

            BaseModuleMainForm.writeLog("结束解析__" + zipName);
            if (outList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得勾选节点列表
        /// </summary>
        /// <returns></returns>
        private List<TreeNode> getCheckedNodeList()
        {
            List<TreeNode> results = new List<TreeNode>();
            foreach (TreeNode tn in tlTestA.Nodes)
            {
                if (tn.Checked)
                {
                    results.Add(tn);
                }
            }
            return results;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (ofdDB.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtDBFile.Text = ofdDB.FileName;

                DBImporter.ProfessionRecordDict.Clear();
                List<ImportDataItem> cpList = new List<ImportDataItem>();
                #region 读取数据
                //SQLite数据库工厂
                System.Data.SQLite.SQLiteFactory factory = new System.Data.SQLite.SQLiteFactory();
                //NDEY数据库连接
                Noear.Weed.DbContext context = new Noear.Weed.DbContext("main", "Data Source = " + txtDBFile.Text, factory);
                //是否在执入后执行查询（主要针对Sqlite）
                context.IsSupportSelectIdentityAfterInsert = false;
                //是否在Dispose后执行GC用于解决Dispose后无法删除的问题（主要针对Sqlite）
                context.IsSupportGCAfterDispose = true;

                try
                {
                    List<Catalog> catList = context.table("Catalog").select("*").getList<Catalog>(new Catalog());
                    foreach (Catalog catObj in catList)
                    {
                        Project projObj = context.table("Project").where("CatalogID='" + catObj.CatalogID + "'").select("*").getItem<Project>(new Project());

                        //查询专业类别
                        Professions professionObj = context.table("Professions").where("ProfessionID='" + projObj.ProfessionID + "'").select("*").getItem<Professions>(new Professions());
                        DBImporter.ProfessionRecordDict[projObj.ProjectName] = new ProfessionRecordObject();
                        DBImporter.ProfessionRecordDict[projObj.ProjectName].ProfessionType = professionObj.ProfessionCategory;
                        DBImporter.ProfessionRecordDict[projObj.ProjectName].ProfessionName = professionObj.ProfessionName;
                        DBImporter.ProfessionRecordDict[projObj.ProjectName].ProfessionSort = projObj.ProfessionSort;
                        
                        ImportDataItem idi = new ImportDataItem();
                        idi.CatalogObj = catObj;
                        idi.ProjectObj = projObj;
                        cpList.Add(idi);
                    }

                    localUnitObj = context.table("LocalUnit").select("*").getItem<LocalUnit>(new LocalUnit());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
                finally
                {
                    factory.Dispose();
                    context.Dispose();
                }
                #endregion

                //刷新文件树
                getFileTree(totalDir);

                Dictionary<string, TreeNode> tempMap = new Dictionary<string, TreeNode>();
                foreach (string f in pkgList)
                {
                    TreeNode tn = new TreeNode();
                    tn.Checked = true;
                    tn.Text = Path.GetFileNameWithoutExtension(f);
                    tn.Name = f;

                    tempMap[Path.GetFileNameWithoutExtension(f)] = tn;
                }

                int errorCount = 0;
                btnOK.Enabled = true;
                tlTestA.Nodes.Clear();
                foreach (ImportDataItem idi in cpList)
                {
                    if (tempMap.ContainsKey(idi.CatalogObj.CatalogNumber))
                    {
                        tlTestA.Nodes.Add(tempMap[idi.CatalogObj.CatalogNumber]);
                    }
                    else
                    {
                        if (idi.ProjectObj.IsPrivateProject == "true")
                        {
                            TreeNode tnn = new TreeNode();
                            tnn.Text = idi.CatalogObj.CatalogName;
                            tnn.Name = idi.CatalogObj.CatalogName;
                            tnn.Tag = idi;
                            tnn.Checked = true;
                            tlTestA.Nodes.Add(tnn);
                        }
                        else
                        {
                            errorCount++;
                            TreeNode tnn = new TreeNode();
                            tnn.Text = idi.CatalogObj.CatalogName + "(没有申报包)";
                            tnn.Name = idi.CatalogObj.CatalogName;
                            tnn.Tag = idi;
                            tnn.Checked = true;
                            tlTestA.Nodes.Add(tnn);
                            btnOK.Enabled = false;
                        }
                    }
                }

                if (errorCount >= 1)
                {
                    MessageBox.Show("有" + errorCount + "个项目无申报数据包，不允许进行导入");
                }
            }
        }
    }

    public class ImportDataItem
    {
        public Catalog CatalogObj { get; set; }
        public Project ProjectObj { get; set; }
    }
}
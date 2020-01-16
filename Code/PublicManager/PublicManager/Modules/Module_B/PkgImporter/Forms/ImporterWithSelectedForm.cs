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
    public partial class ImporterWithSelectedForm : Form
    {
        /// <summary>
        /// 是否需要更新替换字典在改变替换列表中项目状态时
        /// </summary>
        private bool isNeedUpdateDict = true;

        /// <summary>
        /// 替换字典，用于表示哪些项目可以替换(Key=项目ID,Value=是否替换)
        /// </summary>
        private Dictionary<string, bool> replaceDict = new Dictionary<string, bool>();
        private string decompressDir;
        private string totalDir;
        private MainView mainView;

        public ImporterWithSelectedForm(MainView mv,bool isImportAll, string sourceDir, string destDir)
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
                            TreeNode tn = new TreeNode();
                            tn.Text = Path.GetFileNameWithoutExtension(f);
                            tn.Name = f;
                            tlTestA.Nodes.Add(tn);
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
            DBImporter.ProfessionRecordDict.Clear();

            //需要替换的申报包列表
            List<string> importList = new List<string>();
            List<string> importFileNameList = new List<string>();

            #region 查的需要导入的路径
            foreach (TreeNode tn in tlTestA.Nodes)
            {
                //判断是否被选中
                if (tn.Checked)
                {
                    //读取目录名称中的项目编号
                    string projectNumber = tn.Text;

                    //判断是否需要替换
                    if (replaceDict.ContainsKey(projectNumber))
                    {
                        //需要替换

                        //判断是否替换这个项目
                        if (replaceDict[projectNumber])
                        {
                            //需要替换，添加到ImportList列表中
                            importList.Add(Path.Combine(tn.Name, ""));
                            importFileNameList.Add(tn.Text);
                        }
                        else
                        {
                            //不需要替换
                            continue;
                        }
                    }
                    else
                    {
                        //不需要替换
                        importList.Add(Path.Combine(tn.Name, ""));
                        importFileNameList.Add(tn.Text);
                    }
                }
            }
            #endregion

            //开始导入
            ProgressForm pf = new ProgressForm();
            pf.Show();
            pf.run(importList.Count, 0, new EventHandler(delegate(object senders, EventArgs ee)
            {
                //进度数值
                int progressVal = 0;
                int index = -1;
                //导入
                foreach (string zipFile in importList)
                {
                    try
                    {
                        progressVal++;
                        index++;
                        //申报文件名
                        string zipName = importFileNameList[index].ToString();
                        List<string> messageList = null;
                        pf.reportProgress(progressVal, zipName + "_开始解压");
                        bool returnContent = unZipFile(zipFile, zipName, out messageList);
                        if (returnContent)
                        {
                            //报告进度
                            pf.reportProgress(progressVal, zipName + "_开始导入");
                            BaseModuleMainForm.writeLog("开始导入__" + zipName);

                            //检查当前项目有没有设置专业类别
                            if (cbIsKeepProfessionConfig.Checked)
                            {
                                Project lastProject = ConnectionManager.Context.table("Project").where("CatalogID in (select CatalogID from Catalog where CatalogNumber = '" + zipName + "')").select("*").getItem<Project>(new Project());
                                if (!string.IsNullOrEmpty(lastProject.ProjectName))
                                {
                                    DBImporter.ProfessionRecordDict[lastProject.ProjectName] = new ProfessionRecordObject();
                                    DBImporter.ProfessionRecordDict[lastProject.ProjectName].ProfessionType = ConnectionManager.Context.table("Professions").where("ProfessionID='" + lastProject.ProfessionID + "'").select("ProfessionCategory").getValue<string>(string.Empty);
                                    DBImporter.ProfessionRecordDict[lastProject.ProjectName].ProfessionName = lastProject.LastProfessionName;
                                    DBImporter.ProfessionRecordDict[lastProject.ProjectName].ProfessionSort = lastProject.ProfessionSort;
                                }
                            }

                            //导入数据库
                            new DBImporter().addOrReplaceProject(zipFile, zipName, Path.Combine(Path.Combine(decompressDir, zipName), "static.db"));

                            //报告进度
                            pf.reportProgress(progressVal, zipName + "_结束导入");
                            BaseModuleMainForm.writeLog("结束导入__" + zipName);
                        }
                        pf.reportProgress(progressVal, zipName + "_结束解压");
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
        /// 刷新替换列表
        /// </summary>
        private void reloadReplaceList()
        {
            //锁定替换列表点击CheckBox时更新替换字典的功能
            isNeedUpdateDict = false;

            //清空替换列表
            lvErrorList.Items.Clear();
            //显示替换列表数
            foreach (KeyValuePair<string, bool> kvp in replaceDict)
            {
                //列表项
                ListViewItem lvi = new ListViewItem();
                lvi.Text = kvp.Key;
                lvi.Checked = kvp.Value;

                //向替换列表添加
                lvErrorList.Items.Add(lvi);
            }

            //解锁替换列表点击CheckBox时更新替换字典的功能
            isNeedUpdateDict = true;
        }

        private void tlTestA_AfterSelect(object sender, TreeViewEventArgs e)
        {
            List<TreeNode> checkedList = getCheckedNodeList();

            //移除不需要选项
            List<string> delList = new List<string>();
            foreach (KeyValuePair<string, bool> kvp in replaceDict)
            {
                bool needRemove = true;

                foreach (TreeNode selected in checkedList)
                {
                    if (selected.Text == kvp.Key)
                    {
                        needRemove = false;
                        break;
                    }
                }

                if (needRemove)
                {
                    delList.Add(kvp.Key);
                }
            }
            foreach (string s in delList)
            {
                replaceDict.Remove(s);
            }

            //检查需要添加的选项
            foreach (TreeNode selected in checkedList)
            {
                //读取目录名称中的项目编号
                string catalogNumber = selected.Text;

                //根据项目编号查询项目数量
                long projectCount = ConnectionManager.Context.table("Catalog").where("catalognumber='" + catalogNumber + "'").select("count(*)").getValue<long>(0);
                //判断这个项目是否被导入过
                if (projectCount >= 1)
                {
                    replaceDict[catalogNumber] = true;
                }
            }

            //刷新替换列表
            reloadReplaceList();
        }

        private void lvErrorList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //改变选择状态
            if (isNeedUpdateDict)
            {
                if (replaceDict.ContainsKey(e.Item.Text))
                {
                    replaceDict[e.Item.Text] = e.Item.Checked;
                }
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

        private void tlTestA_AfterCheck(object sender, TreeViewEventArgs e)
        {
            List<TreeNode> checkedList = getCheckedNodeList();

            //移除不需要选项
            List<string> delList = new List<string>();
            foreach (KeyValuePair<string, bool> kvp in replaceDict)
            {
                bool needRemove = true;

                foreach (TreeNode selected in checkedList)
                {
                    if (selected.Text == kvp.Key)
                    {
                        needRemove = false;
                        break;
                    }
                }

                if (needRemove)
                {
                    delList.Add(kvp.Key);
                }
            }
            foreach (string s in delList)
            {
                replaceDict.Remove(s);
            }

            //检查需要添加的选项
            foreach (TreeNode selected in checkedList)
            {
                //读取目录名称中的项目编号
                string catalogNumber = selected.Text;

                //根据项目编号查询项目数量
                long projectCount = ConnectionManager.Context.table("Catalog").where("catalognumber='" + catalogNumber + "'").select("count(*)").getValue<long>(0);
                //判断这个项目是否被导入过
                if (projectCount >= 1)
                {
                    replaceDict[catalogNumber] = true;
                }
            }

            //刷新替换列表
            reloadReplaceList();
        }

        private void txtFilteKeys_TextChanged(object sender, EventArgs e)
        {
            foreach (TreeNode tn in tlTestA.Nodes)
            {
                if (tn.Text != null && tn.Text.Contains(txtFilteKeys.Text))
                {
                    tlTestA.SelectedNode = tn;
                    break;
                }
            }
        }
    }
}
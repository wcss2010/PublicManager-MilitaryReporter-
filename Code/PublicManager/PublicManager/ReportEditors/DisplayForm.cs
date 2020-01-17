using PublicReporterLib;
using SuperCodeFactoryUILib.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PublicReporter
{
    public partial class DisplayForm : Form
    {
        private bool needExport = false;

        /// <summary>
        /// 插件目录
        /// </summary>
        public static string PluginWorkDir = Path.Combine(Application.StartupPath, "ReportPlugins");

        public string DestZipPath { get; set; }

        public event ExportCompleteDelegate OnExportComplete;

        public DisplayForm()
        {
            InitializeComponent();

            try
            {
                Directory.CreateDirectory(DisplayForm.PluginWorkDir);
            }
            catch (Exception ex) { }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {   
            #region 获得上一次保存日期
            dynamic script = CSScriptLibrary.CSScript.LoadCode(
                           @"using System.Windows.Forms;
                             using System;
                             using System.Data;
                             using System.IO;
                             using System.Text;
                             using PublicReporterLib;
                             
                             public class Script
                             {
                                 public DateTime getLastUpdateDate()
                                 {
                                     return ((ProjectMilitaryTechnologPlanPlugin.PluginRoot)PublicReporterLib.PluginLoader.CurrentPlugin).getLastUpdateDate();
                                 }
                             }")
                             .CreateObject("*");
            DateTime dtLastUpdateDate = script.getLastUpdateDate();
            #endregion

            #region 获得上一次Doc修改时间
            script = CSScriptLibrary.CSScript.LoadCode(
                           @"using System.Windows.Forms;
                             using System;
                             using System.Data;
                             using System.IO;
                             using System.Text;
                             using PublicReporterLib;
                             
                             public class Script
                             {
                                 public string getDataDir()
                                 {
                                     ProjectMilitaryTechnologPlanPlugin.PluginRoot rootObj = (ProjectMilitaryTechnologPlanPlugin.PluginRoot)PublicReporterLib.PluginLoader.CurrentPlugin;
                                     return rootObj.dataDir;
                                 }
                             }")
                             .CreateObject("*");
            string dataDir = script.getDataDir();
            string docFile = Path.Combine(dataDir, "论证报告.doc");
            DateTime dtDoc = DateTime.MinValue;
            if (File.Exists(docFile))
            {
                dtDoc = File.GetLastWriteTime(docFile);
            }
            #endregion

            if (dtLastUpdateDate > dtDoc)
            {
                MessageBox.Show("对不起，当前的论证报告书不是最新的，请点击\"生成报告\"或\"预览\"按钮重新生成论证报告书！");

                //需要导出
                needExport = true;

                //取消关闭
                e.Cancel = true;
            }
            else
            {
                #region 检查是不是可以导出
                script = CSScriptLibrary.CSScript.LoadCode(
                               @"using System.Windows.Forms;
                             using System;
                             using System.Data;
                             using System.IO;
                             using System.Text;
                             using PublicReporterLib;
                             
                             public class Script
                             {
                                 public bool isAcceptExport()
                                 {
                                     return ((ProjectMilitaryTechnologPlanPlugin.PluginRoot)PublicReporterLib.PluginLoader.CurrentPlugin).isAcceptExport();
                                 }
                             }")
                                 .CreateObject("*");
                bool isAcceptExport = script.isAcceptExport();
                #endregion

                if (isAcceptExport)
                {
                    //检查是否加载了插件
                    if (PluginLoader.CurrentPlugin != null)
                    {
                        //检查当前是否允行退出
                        if (PluginLoader.CurrentPlugin.isAcceptClose())
                        {
                            //导出数据包
                            exportPkg(dataDir, e);

                            //检查是否导出成功了,如果当前Cancel为true则说明失败了
                            if (e.Cancel == true)
                            {
                                //导出失败
                            }
                            else
                            {
                                //导出成功，插件停止
                                PluginLoader.CurrentPlugin.stop(e);
                            }
                        }
                        else
                        {
                            //取消关闭
                            e.Cancel = true;
                        }
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 检查文件是否被占用(True=被占用,False=没有被占用)
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static bool IsFileInUse(string fileName)
        {
            bool inUse = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                inUse = false;
            }
            catch
            { }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return inUse;//true表示正在使用,false没有使用  
        }

        private void exportPkg(string currentDir, FormClosingEventArgs e)
        {
            if (needExport)
            {
                //检查是否文件被占有了
                bool acceptExport = true;
                List<string> filesList = new List<string>();
                filesList.AddRange(Directory.GetFiles(currentDir));
                filesList.AddRange(Directory.GetFiles(Path.Combine(currentDir, "Files")));
                foreach (string s in filesList)
                {
                    if (s != null && s.Contains("static.db"))
                    {
                        continue;
                    }
                    else
                    {
                        if (IsFileInUse(s))
                        {
                            e.Cancel = true;
                            acceptExport = false;
                            MessageBox.Show("对不起，文件(" + s + ")被占用，无法导出！");
                            break;
                        }
                    }
                }

                //导出
                if (acceptExport)
                {
                    //检查目标文件是否存在，如果存在则删除
                    string destFile = DestZipPath;
                    if (File.Exists(destFile))
                    {
                        try
                        {
                            File.Delete(destFile);
                        }
                        catch (Exception ex) { }
                    }

                    try
                    {
                        CircleProgressBarDialog dialoga = new CircleProgressBarDialog();
                        dialoga.TransparencyKey = dialoga.BackColor;
                        dialoga.ProgressBar.ForeColor = Color.Red;
                        dialoga.MessageLabel.ForeColor = Color.Blue;
                        dialoga.FormBorderStyle = FormBorderStyle.None;
                        dialoga.Start(new EventHandler<CircleProgressBarEventArgs>(delegate(object thisObject, CircleProgressBarEventArgs argss)
                        {
                            CircleProgressBarDialog senderForm = ((CircleProgressBarDialog)thisObject);

                            senderForm.ReportProgress(10, 100);
                            senderForm.ReportInfo("准备导出...");
                            try { System.Threading.Thread.Sleep(1000); }
                            catch (Exception ex) { }

                            #region 尝试关闭Sqlite数据库连接
                            try
                            {
                                dynamic script = CSScriptLibrary.CSScript.LoadCode(
                                       @"using System.Windows.Forms;
                             public class Script
                             {
                                 public void CloseDB()
                                 {
                                     ProjectMilitaryTechnologPlanPlugin.DB.ConnectionManager.Close();
                                 }
                             }")
                                         .CreateObject("*");
                                script.CloseDB();
                            }
                            catch (Exception ex) { }
                            #endregion
                            
                            senderForm.ReportProgress(20, 100);
                            senderForm.ReportInfo("正在导出...");
                            try { System.Threading.Thread.Sleep(1000); }
                            catch (Exception ex) { }

                            //压缩
                            new PublicReporterLib.Utility.ZipUtil().ZipFileDirectory(currentDir, destFile);

                            senderForm.ReportProgress(90, 100);
                            senderForm.ReportInfo("导出完成...");
                            try { System.Threading.Thread.Sleep(1000); }
                            catch (Exception ex) { }

                            //导出完成事件
                            if (senderForm.IsHandleCreated)
                            {
                                senderForm.Invoke(new MethodInvoker(delegate()
                                    {
                                        if (OnExportComplete != null)
                                        {
                                            OnExportComplete(this, new ExportCompleteEventArgs(DestZipPath));
                                        }
                                    }));
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败！Ex:" + ex.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 载入插件
        /// </summary>
        /// <param name="workDir">插件工作目录</param>
        /// <returns></returns>
        public void loadPlugin(string workDir)
        {
            try
            {
                //加载插件
                PluginLoader.searchAndLoadPlugin(workDir);

                //判断是否可用
                if (PluginLoader.CurrentPlugin != null)
                {
                    //设置程序标题
                    this.Text = PluginLoader.CurrentPlugin.DefaultTitle;

                    //设置工作目录
                    PluginLoader.CurrentPlugin.RootDir = workDir;

                    //初始化
                    PluginLoader.CurrentPlugin.init(this, tsButtonBar, ilNodeImage, tvSubjects, plTreeButtons, scContent.Panel2, ssHintBar, tsslHint);

                    //添加日志事件
                    PluginLoader.CurrentPlugin.Log += CurrentPlugin_Logs;

                    //启动插件
                    PluginLoader.CurrentPlugin.start();
                }
                else
                {
                    throw new Exception("没有找到填报插件！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void CurrentPlugin_Logs(object sender, LogEventArgs args)
        {
            System.Console.WriteLine(args.ErrorMsg);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public delegate void ExportCompleteDelegate(object sender, ExportCompleteEventArgs args);
    public class ExportCompleteEventArgs : EventArgs
    {
        public ExportCompleteEventArgs(string zipFile)
        {
            DestZipFile = zipFile;
        }

        public string DestZipFile { get; set; }
    }
}
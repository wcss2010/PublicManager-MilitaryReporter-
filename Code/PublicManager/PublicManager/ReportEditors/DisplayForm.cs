using PublicReporterLib;
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
        /// <summary>
        /// 插件目录
        /// </summary>
        public static string PluginDirs = Path.Combine(Application.StartupPath, "ReportPlugins");

        public DisplayForm()
        {
            InitializeComponent();

            try
            {
                Directory.CreateDirectory(DisplayForm.PluginDirs);
            }
            catch (Exception ex) { }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //检查是否加载了插件
            if (PluginLoader.CurrentPlugin != null)
            {
                //检查当前是否允行退出
                if (PluginLoader.CurrentPlugin.isAcceptClose())
                {
                    //插件停止
                    PluginLoader.CurrentPlugin.stop(e);
                }
                else 
                {
                    //取消关闭
                    e.Cancel = true;
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
}
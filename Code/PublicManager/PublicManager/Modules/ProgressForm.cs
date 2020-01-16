using SuperCodeFactoryUILib.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PublicManager.Modules
{
    public partial class ProgressForm : CircleProgressBarDialog
    {
        private decimal progressPercent = 0;

        /// <summary>
        /// 错误计数
        /// </summary>
        public static int errorCount;

        /// <summary>
        /// 是否显示错误日志
        /// </summary>
        public static bool isNeedShowLog = false;

        public ProgressForm()
            : base()
        {
            TransparencyKey = BackColor;
            ProgressBar.ForeColor = Color.Red;
            MessageLabel.ForeColor = Color.Blue;
            FormBorderStyle = FormBorderStyle.None;
        }

        /// <summary>
        /// 运行代码
        /// </summary>
        /// <param name="total">总进度</param>
        /// <param name="cur">当前进度</param>
        /// <param name="ehDynamic">工作事件</param>
        public void run(int total, int cur, EventHandler ehDynamic)
        {
            Start(new EventHandler<CircleProgressBarEventArgs>(delegate(object thisObject, CircleProgressBarEventArgs argss)
            {
                CircleProgressBarDialog senderDialog = ((CircleProgressBarDialog)thisObject);

                //计算每个进度占比
                progressPercent = (decimal)total / (decimal)100;

                //显示初始进度
                senderDialog.ReportProgress((int)((decimal)cur * progressPercent), 100);

                //运行工作事件
                if (ehDynamic != null)
                {
                    try
                    {
                        ehDynamic(this, new EventArgs());
                    }
                    catch (Exception ex)
                    {
                        BaseModuleMainForm.writeLog(ex.ToString());
                    }
                }

                //异常本窗体
                if (senderDialog.IsHandleCreated)
                {
                    senderDialog.Invoke(new MethodInvoker(delegate()
                    {
                        Visible = false;
                    }));
                }

                //检查是否需要显示错误日志
                if (errorCount >= 1 && isNeedShowLog)
                {
                    //错误数量清空
                    errorCount = 0;

                    //日志路径
                    string logFile = System.IO.Path.Combine(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"), string.Format("{0}.txt", DateTime.Now.ToString("yyyyMMdd")));

                    //询问是否需要显示日志
                    if (MessageBox.Show("是否需要打开错误日志文件?", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //打开日志文件
                        try
                        {
                            System.Diagnostics.Process.Start(logFile);
                        }
                        catch (Exception ex) { }
                    }
                }
            }));
        }

        /// <summary>
        /// 设置当前进度
        /// </summary>
        /// <param name="current">进度值</param>
        public void reportProgress(int current, string text)
        {
            ReportProgress((int)((decimal)current * progressPercent), 100);
            ReportInfo(text);

            try
            {
                Thread.Sleep(500);
            }
            catch (Exception ex) { }
        }
    }
}
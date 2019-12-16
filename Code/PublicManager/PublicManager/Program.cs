using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PublicManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //初始化数据库
            PublicManager.DB.ConnectionManager.Open("main", "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "static.db"));

            //载入配置
            MainConfig.loadConfig();

            //加载皮肤
            if (MainConfig.Config.Dict.ContainsKey("当前皮肤"))
            {
                string skinName = MainConfig.Config.Dict["当前皮肤"];
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(string.IsNullOrEmpty(skinName) ? "Office 2010 Blue" : skinName);
            }
            if (MainConfig.Config.Dict.ContainsKey("皮肤颜色1"))
            {
                int colorVal = int.Parse(MainConfig.Config.Dict["皮肤颜色1"]);
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(colorVal);
            }
            if (MainConfig.Config.Dict.ContainsKey("皮肤颜色2"))
            {
                int colorVal = int.Parse(MainConfig.Config.Dict["皮肤颜色2"]);
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor2 = Color.FromArgb(colorVal);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
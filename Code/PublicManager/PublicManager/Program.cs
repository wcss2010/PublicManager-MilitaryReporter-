using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            //添加忽略
            PublicReporterLib.PluginLoader.IgnoreLoadDllFiles.Add("PublicReporterLib.dll");
            PublicReporterLib.PluginLoader.IgnoreLoadDllFiles.Add("Noear.Weed3.dll");
            PublicReporterLib.PluginLoader.IgnoreLoadDllFiles.Add("Aspose.Words.dll");
            PublicReporterLib.PluginLoader.IgnoreLoadDllFiles.Add("Newtonsoft.Json.dll");
            PublicReporterLib.PluginLoader.IgnoreLoadDllFiles.Add("ICSharpCode.SharpZipLib.dll");
            PublicReporterLib.PluginLoader.IgnoreLoadDllFiles.Add("SuperCodeFactoryLib.dll");
            PublicReporterLib.PluginLoader.IgnoreLoadDllFiles.Add("SuperCodeFactoryUILib.dll");

            //载入配置
            MainConfig.constConfigFile = Path.Combine(Application.StartupPath, "config.json");
            MainConfig.loadConfig();

            //加载皮肤
            if (MainConfig.Config.StringDict.ContainsKey("当前皮肤"))
            {
                string skinName = MainConfig.Config.StringDict["当前皮肤"];
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(string.IsNullOrEmpty(skinName) ? "Office 2010 Blue" : skinName);
            }
            if (MainConfig.Config.StringDict.ContainsKey("皮肤颜色1"))
            {
                int colorVal = int.Parse(MainConfig.Config.StringDict["皮肤颜色1"]);
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(colorVal);
            }
            if (MainConfig.Config.StringDict.ContainsKey("皮肤颜色2"))
            {
                int colorVal = int.Parse(MainConfig.Config.StringDict["皮肤颜色2"]);
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor2 = Color.FromArgb(colorVal);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WelcomeForm());
        }
    }
}
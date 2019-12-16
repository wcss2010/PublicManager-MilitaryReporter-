using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PublicManager
{
    /// <summary>
    /// 程序配置文件
    /// </summary>
    public class MainConfig
    {
        /// <summary>
        /// 配置
        /// </summary>
        public static MainConfig Config { get; set; }

        private Dictionary<string, string> dict = new Dictionary<string, string>();
        /// <summary>
        /// 数据字典
        /// </summary>
        public Dictionary<string, string> Dict
        {
            get { return dict; }
        }

        /// <summary>
        /// 载入配置
        /// </summary>
        public static void loadConfig()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "config.json")))
            {
                Config = Newtonsoft.Json.JsonConvert.DeserializeObject<MainConfig>(File.ReadAllText(Path.Combine(Application.StartupPath, "config.json")));
            }
            else
            {
                Config = new MainConfig();

                Config.Dict["合同验证_目录"] = "files";
                Config.Dict["合同验证_文件"] = "static.db,合同书.doc";

                Config.Dict["建议验证_目录"] = "files";
                Config.Dict["建议验证_文件"] = "static.db,项目申报书.doc";

            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public static void saveConfig()
        {
            string cnt = Newtonsoft.Json.JsonConvert.SerializeObject(Config, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Path.Combine(Application.StartupPath, "config.json"), cnt);
        }
    }
}
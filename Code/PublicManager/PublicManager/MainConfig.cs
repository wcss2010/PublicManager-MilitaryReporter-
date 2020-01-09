using SuperCodeFactoryLib.Collections;
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
        /// 行分割符
        /// </summary>
        public const string rowFlag = "{(<r>)}";

        /// <summary>
        /// 单元格分割符
        /// </summary>
        public const string cellFlag = "{(<c>)}";

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string constConfigFile = string.Empty;

        /// <summary>
        /// 配置
        /// </summary>
        public static MainConfig Config { get; set; }

        private SerialDictionary<string, string> stringDict = new SerialDictionary<string, string>();
        /// <summary>
        /// 字符数据字典
        /// </summary>
        public SerialDictionary<string, string> StringDict
        {
            get { return stringDict; }
        }

        private SerialDictionary<string, object> objectDict = new SerialDictionary<string, object>();
        /// <summary>
        /// 对象数据字典
        /// </summary>
        public SerialDictionary<string, object> ObjectDict
        {
            get { return objectDict; }
        }
        
        /// <summary>
        /// 载入配置
        /// </summary>
        public static void loadConfig()
        {
            try
            {
                //检查是不是存在配置文件
                if (File.Exists(constConfigFile))
                {
                    //读取配置文件
                    Config = Newtonsoft.Json.JsonConvert.DeserializeObject<MainConfig>(File.ReadAllText(constConfigFile));
                }
                else
                {
                    //初始化
                    initConfig();
                }
            }
            catch (Exception ex)
            {
                //初始化
                initConfig();
            }
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        public static void initConfig()
        {
            Config = new MainConfig();

            Config.StringDict["论证报告总目录"] = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            Config.StringDict["论证报告解压目录"] = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            Config.StringDict["报告验证_目录"] = "files";
            Config.StringDict["报告验证_文件"] = "static.db";
            Config.ObjectDict.Add("责任单位", new string[] { "东部战区", "南部战区", "西部战区", "北部战区", "中部战区", "陆军", "海军", "空军", "火箭军", "战略支援部队", "联勤保障部队", "军委办公厅", "军委联合参谋部", "军委政治工作部", "军委后勤保障部", "军委装备发展部", "军委训练管理部", "军委国防动员部", "军委纪律检察委员会", "军委政法委员会", "军委科学技术委员会", "军委战略规划办公室", "军委改革和编制办公室", "军委国际军事合作办公室", "军委审计署", "军委机关事务管理总局", "军事科学院", "国防大学", "国防科技大学", "武装警察部队" });
            Config.ObjectDict.Add("研究周期", new string[] { "6个月" + rowFlag + "6", "12个月" + rowFlag + "12", "18个月" + rowFlag + "18", "24个月" + rowFlag + "24", "36个月" + rowFlag + "36" });
            Config.ObjectDict.Add("责任单位与专业类别映射", new string[] { "陆军", "海军", "空军", "火箭军", "战略支援部队", "联勤保障部队", "军事科学院", "国防大学", "国防科技大学", "武装警察部队" });
            Config.ObjectDict.Add("项目类别", new string[] { "重大" + rowFlag + "[100,180]" + rowFlag + "[5,10]" + rowFlag + "300", "重点" + rowFlag + "[90,150]" + rowFlag + "[3,7]" + rowFlag + "100" });

            //保存初始化内容
            saveConfig();

            //重新加载一次
            loadConfig();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public static void saveConfig()
        {
            string cnt = Newtonsoft.Json.JsonConvert.SerializeObject(Config, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(constConfigFile, cnt);
        }
    }
}
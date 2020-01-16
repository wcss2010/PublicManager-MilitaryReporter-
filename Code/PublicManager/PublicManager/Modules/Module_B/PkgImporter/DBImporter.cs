using Noear.Weed;
using PublicManager.DB;
using PublicManager.DB.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublicManager.Modules.Module_B.PkgImporter
{
    public class DBImporter : BaseDBImporter
    {
        private static Dictionary<string, ProfessionRecordObject> professionRecordDicts = new Dictionary<string, ProfessionRecordObject>();
        /// <summary>
        /// 其它地区里设置的专业类别字典(Key=项目名称,Value=专业类别对象)
        /// </summary>
        public static Dictionary<string, ProfessionRecordObject> ProfessionRecordDict
        {
            get { return DBImporter.professionRecordDicts; }
        }

        /// <summary>
        /// 获得旧的专业类别
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public ProfessionRecordObject getProfessionRecord(string projectName)
        {
            if (professionRecordDicts.ContainsKey(projectName))
            {
                return professionRecordDicts[projectName];
            }
            else
            {
                return new ProfessionRecordObject();
            }
        }

        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <returns></returns>
        protected override string importDB(string zipFile, string catalogNumber, string sourceFile, Noear.Weed.DbContext localContext)
        {
            //数据库版本号
            string catalogVersionStr = "v1.0";

            //附件目录
            string filesDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(sourceFile), "Files");

            //处理项目信息
            DataItem diProject = localContext.table("JiBenXinXiBiao").select("*").getDataItem();
            if (diProject != null && diProject.count() >= 1)
            {
                #region 读取版本号并更新Catalog信息
                //读取版本号
                try
                {
                    catalogVersionStr = localContext.table("Version").select("VersionNum").getValue<string>(catalogVersionStr);
                }
                catch (Exception ex) { }

                //更新Catalog
                Catalog catalog = updateAndClearCatalog(catalogNumber, diProject.getString("XiangMuMingCheng"), "论证报告书", catalogVersionStr, zipFile);
                #endregion

                #region 导入项目
                //添加项目信息
                Project proj = new Project();
                proj.ProjectID = catalog.CatalogID;
                proj.CatalogID = catalog.CatalogID;
                proj.ProjectName = catalog.CatalogName;
                proj.StudyDest = diProject.get("YanJiuMuBiao") != null ? diProject.get("YanJiuMuBiao").ToString() : string.Empty;
                proj.StudyContent = diProject.get("YanJiuNeiRong") != null ? diProject.get("YanJiuNeiRong").ToString() : string.Empty;
                proj.WillResult = diProject.get("YuQiChengGuo") != null ? diProject.get("YuQiChengGuo").ToString() : string.Empty;
                proj.StudyTime = diProject.get("YanJiuZhouQi") != null ? decimal.Parse(diProject.get("YanJiuZhouQi").ToString()) : 0;
                proj.StudyMoney = diProject.get("JingFeiYuSuan") != null ? decimal.Parse(diProject.get("JingFeiYuSuan").ToString()) : 0;
                proj.ProjectSort = diProject.get("XiangMuLeiBie") != null ? diProject.get("XiangMuLeiBie").ToString() : string.Empty;
                proj.DutyUnit = diProject.get("ZeRenDanWei") != null ? diProject.get("ZeRenDanWei").ToString() : string.Empty;
                proj.NextUnit = diProject.get("XiaJiDanWei") != null ? diProject.get("XiaJiDanWei").ToString() : string.Empty;
                proj.Memo = diProject.get("BeiZhu") != null ? diProject.get("BeiZhu").ToString() : string.Empty;
                proj.Worker = diProject.get("QianTouRen") != null ? diProject.get("QianTouRen").ToString() : string.Empty;
                proj.WorkerCardID = diProject.get("QianTouRenShenFenZheng") != null ? diProject.get("QianTouRenShenFenZheng").ToString() : string.Empty;
                proj.WorkerSex = diProject.get("QianTouRenXingBie") != null ? diProject.get("QianTouRenXingBie").ToString() : string.Empty;
                proj.WorkerNation = diProject.get("QianTouRenMinZu") != null ? diProject.get("QianTouRenMinZu").ToString() : string.Empty;
                proj.WorkerBirthday = diProject.get("QianTouRenShengRi") != null ? DateTime.Parse(diProject.get("QianTouRenShengRi").ToString()) : DateTime.Now;
                proj.WorkerTelephone = diProject.get("QianTouRenDianHua") != null ? diProject.get("QianTouRenDianHua").ToString() : string.Empty;
                proj.WorkerMobilephone = diProject.get("QianTouRenShouJi") != null ? diProject.get("QianTouRenShouJi").ToString() : string.Empty;
                proj.SectionJobCateGory = diProject.get("BuZhiBie") != null ? diProject.get("BuZhiBie").ToString() : string.Empty;
                proj.AllStudyUnit = diProject.get("LianHeYanJiuDanWei") != null ? diProject.get("LianHeYanJiuDanWei").ToString() : string.Empty;
                proj.RequestMoney = diProject.get("ShenQingJingFei") != null ? decimal.Parse(diProject.get("ShenQingJingFei").ToString()) : 0;
                proj.TaskCompleteTime = diProject.get("JiHuaWanChengShiJian") != null ? DateTime.Parse(diProject.get("JiHuaWanChengShiJian").ToString()) : DateTime.Now;
                proj.IsPrivateProject = "false";
                proj.ProfessionSort = 0;

                //专业类别配置
                proj.ProfessionID = ConnectionManager.Context.table("Professions").where("ProfessionCategory='" + getProfessionRecord(proj.ProjectName).ProfessionType + "'").select("ProfessionID").getValue<string>(string.Empty);
                proj.LastProfessionName = getProfessionRecord(proj.ProjectName).ProfessionName;
                proj.ProfessionSort = getProfessionRecord(proj.ProjectName).ProfessionSort;

                //过滤文本--处理备注
                proj.Memo = proj.Memo != null && proj.Memo.Contains(MainConfig.rowFlag) ? proj.Memo.Replace(MainConfig.rowFlag, ":") : proj.Memo;

                //过滤文本--处理预期成果
                StringBuilder sbWillResult = new StringBuilder();
                if (proj.WillResult != null && proj.WillResult.Contains(MainConfig.rowFlag))
                {
                    string[] tttt = proj.WillResult.Split(new string[] { MainConfig.rowFlag }, StringSplitOptions.None);
                    if (tttt != null)
                    {
                        foreach (string ss in tttt)
                        {
                            string[] vvvv = ss.Split(new string[] { MainConfig.cellFlag }, StringSplitOptions.None);
                            if (vvvv != null && vvvv.Length >= 2)
                            {
                                if (string.IsNullOrEmpty(vvvv[0])) { continue; }

                                sbWillResult.Append(vvvv[0].Insert(vvvv[0].IndexOf("("), vvvv[1]).Replace("(", string.Empty).Replace(")", string.Empty)).Append(",");
                            }
                        }
                    }
                }
                proj.WillResult = sbWillResult.ToString();

                //过滤文本--处理研究内容
                StringBuilder sbStudyContent = new StringBuilder();
                if (proj.StudyContent != null)
                {
                    string[] cList = proj.StudyContent.Split(new string[] { MainConfig.rowFlag }, StringSplitOptions.None);
                    if (cList != null && cList.Length >= 1)
                    {
                        int indexx = 0;
                        foreach (string s in cList)
                        {
                            indexx++;

                            if (string.IsNullOrEmpty(s))
                            {
                                continue;
                            }

                            sbStudyContent.Append(indexx).Append(". ").Append(s).AppendLine();
                        }
                    }
                }
                proj.StudyContent = sbStudyContent.ToString();

                //过滤文本--处理研究目标
                proj.StudyDest = proj.StudyDest;

                proj.copyTo(ConnectionManager.Context.table("Project")).insert();
                #endregion

                #region 导入人员信息
                //处理人员信息
                DataList dlPersonDatas = localContext.table("RenYuanBiao").select("*").getDataList();
                foreach (DataItem diPrn in dlPersonDatas.getRows())
                {
                    Person obj = new Person();
                    obj.PersonID = Guid.NewGuid().ToString();
                    obj.CatalogID = proj.CatalogID;
                    obj.ProjectID = proj.ProjectID;
                    obj.PersonName = diPrn.get("XingMing") != null ? diPrn.get("XingMing").ToString() : string.Empty;
                    obj.PersonIDCard = diPrn.get("ShenFenZhengHao") != null ? diPrn.get("ShenFenZhengHao").ToString() : string.Empty;
                    obj.PersonNation = diPrn.get("MinZu") != null ? diPrn.get("MinZu").ToString() : string.Empty;
                    obj.PersonSex = diPrn.get("XingBie") != null ? diPrn.get("XingBie").ToString() : string.Empty;
                    obj.PersonBirthday = diPrn.get("ShengRi") != null ? DateTime.Parse(diPrn.get("ShengRi").ToString()) : DateTime.Now;
                    obj.PersonJob = diPrn.get("ZhuanYeZhiWu") != null ? diPrn.get("ZhuanYeZhiWu").ToString() : string.Empty;
                    obj.PersonSpecialty = diPrn.get("YanJiuZhuanChang") != null ? diPrn.get("YanJiuZhuanChang").ToString() : string.Empty;
                    obj.JobInProject = diPrn.get("ZhiWu") != null ? diPrn.get("ZhiWu").ToString() : string.Empty;
                    obj.IsProjectMaster = diPrn.get("ShiXiangMuFuZeRen") != null ? diPrn.get("ShiXiangMuFuZeRen").ToString() : string.Empty;
                    obj.WorkUnit = diPrn.get("GongZuoDanWei") != null ? diPrn.get("GongZuoDanWei").ToString() : string.Empty;
                    obj.Telephone = diPrn.get("DianHua") != null ? diPrn.get("DianHua").ToString() : string.Empty;
                    obj.Mobilephone = diPrn.get("ShouJI") != null ? diPrn.get("ShouJI").ToString() : string.Empty;

                    //插入数据
                    obj.copyTo(ConnectionManager.Context.table("Person")).insert();
                }
                #endregion

                #region 导入经费信息表
                DataList dlMoneys = localContext.table("YuSuanBiao").select("*").getDataList();
                if (dlMoneys != null && dlMoneys.getRowCount() >= 1)
                {
                    foreach (DataItem di in dlMoneys.getRows())
                    {
                        //添加字典
                        addMoney(catalog, proj, di.getString("MingCheng"), di.getString("ShuJu"));
                    }
                }
                #endregion

                return catalog.CatalogID;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 清除表格
        /// </summary>
        /// <param name="catalogID"></param>
        public override void clearProjectDataWithCatalogID(string catalogID)
        {
            ConnectionManager.Context.table("Project").where("CatalogID='" + catalogID + "'").delete();
            ConnectionManager.Context.table("Person").where("CatalogID='" + catalogID + "'").delete();
            ConnectionManager.Context.table("Moneys").where("CatalogID='" + catalogID + "'").delete();
            //ConnectionManager.Context.table("Professions").where("CatalogID='" + catalogID + "'").delete();
        }

        /// <summary>
        /// 添加金额数据
        /// </summary>
        /// <param name="catalog"></param>
        /// <param name="project"></param>
        /// <param name="dName"></param>
        /// <param name="dValue"></param>
        protected void addMoney(Catalog catalog, Project project, string dName, string dValue)
        {
            Moneys dict = new Moneys();
            dict.MoneyID = Guid.NewGuid().ToString();
            dict.CatalogID = catalog.CatalogID;
            dict.ProjectID = project.ProjectID;
            dict.MoneyName = dName;
            dict.MoneyValue = dValue;
            dict.copyTo(ConnectionManager.Context.table("Moneys")).insert();
        }
    }

    [Serializable]
    public class ProfessionRecordObject
    {
        public ProfessionRecordObject() { }

        public ProfessionRecordObject(string pfType, string pfName, decimal pfSort)
        {
            ProfessionType = pfType;
            ProfessionName = pfName;
            ProfessionSort = pfSort;
        }

        /// <summary>
        /// 专业类型
        /// </summary>
        public string ProfessionType { get; set; }

        /// <summary>
        /// 专业类别名称
        /// </summary>
        public string ProfessionName { get; set; }

        /// <summary>
        /// 专业类别序号
        /// </summary>
        public decimal ProfessionSort { get; set; }
    }
}
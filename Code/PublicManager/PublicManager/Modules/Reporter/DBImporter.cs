using Noear.Weed;
using PublicManager.DB;
using PublicManager.DB.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublicManager.Modules.Reporter
{
    public class DBImporter : BaseDBImporter
    {
        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <param name="catalogNumber"></param>
        /// <param name="sourceFile"></param>
        /// <param name="localContext"></param>
        /// <returns></returns>
        protected override string importDB(string catalogNumber, string sourceFile, Noear.Weed.DbContext localContext)
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
                Catalog catalog = updateAndClearCatalog(catalogNumber, diProject.getString("XiangMuMingCheng"), "论证报告书", catalogVersionStr);
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
                DataList dlMoneys = localContext.table("MoneyAndYear").select("*").getDataList();
                if (dlMoneys != null && dlMoneys.getRowCount() >= 1)
                {
                    foreach (DataItem di in dlMoneys.getRows())
                    {
                        //添加字典
                        addDict(catalog, proj, "Money,Info", di.getString("Name"), di.getString("Value"), string.Empty);
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
            ConnectionManager.Context.table("Project").delete();
            ConnectionManager.Context.table("Person").delete();
            ConnectionManager.Context.table("Dicts").delete();
        }
    }
}
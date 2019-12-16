using Noear.Weed;
using PublicManager.DB;
using PublicManager.DB.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublicManager.Modules.Contract
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
            string catalogVersionStr = "v1.1";

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
                Catalog catalog = updateAndClearCatalog(catalogNumber, diProject.getString("HeTongMingCheng"), "合同书", catalogVersionStr);
                #endregion

                #region 导入项目及课题信息
                //处理项目信息
                Project proj = new Project();
                proj.ProjectID = catalog.CatalogID;
                proj.CatalogID = catalog.CatalogID;
                proj.ProjectName = catalog.CatalogName;
                proj.SecretLevel = diProject.getString("HeTongMiJi");
                proj.TotalMoney = diProject.get("HeTongJiaKuan") != null ? decimal.Parse(diProject.get("HeTongJiaKuan").ToString()) : 0;
                proj.ProjectNumber = diProject.getString("HeTongBianHao");

                int totalYear = 0;
                try
                {
                    DateTime startTime = diProject.getDateTime("HeTongKaiShiShiJian");
                    DateTime endTime = diProject.getDateTime("HeTongJieShuShiJian");
                    totalYear = (endTime.Year + 1) - startTime.Year;
                }
                catch (Exception ex) { }
                proj.TotalTime = totalYear;

                //导入1.3版之后版本新添加的字段
                switch (catalogVersionStr)
                {
                    case "v1.1":
                        break;
                    case "v1.2":
                        break;
                    case "v1.3":
                        break;
                    case "v1.4":
                        proj.Keywords = diProject.getString("HeTongGuanJianZi");
                        proj.Domains = diProject.getString("HeTongSuoShuLingYu");
                        proj.DutyUnit = diProject.getString("HeTongFuZeDanWei");
                        proj.DutyUnitOrg = diProject.getString("HeTongSuoShuBuMen");
                        proj.DutyUnitAddress = getAddress(diProject.getString("HeTongSuoShuDiDian"));
                        break;
                }

                proj.copyTo(ConnectionManager.Context.table("Project")).insert();

                //处理课题列表
                DataList dlSubject = localContext.table("KeTiBiao").orderBy("ZhuangTai,ModifyTime").select("*").getDataList();
                foreach (DataItem di in dlSubject.getRows())
                {
                    Subject obj = new Subject();
                    obj.SubjectID = di.getString("BianHao");
                    obj.CatalogID = proj.CatalogID;
                    obj.ProjectID = proj.ProjectID;
                    obj.SubjectName = di.getString("KeTiMingCheng");
                    //obj.TotalMoney = di.get("") != null ? decimal.Parse(di.get("").ToString()) : 0;
                    obj.WorkDest = di.getString("KeTiYanJiuMuBiao");
                    obj.WorkContent = di.getString("KeTiYanJiuNeiRong");
                    obj.WorkTask = di.getString("KeTiCanJiaDanWeiFenGong");

                    //导入1.3版之后版本新添加的字段
                    switch (catalogVersionStr)
                    {
                        case "v1.1":
                            break;
                        case "v1.2":
                            break;
                        case "v1.3":
                            break;
                        case "v1.4":
                            obj.DutyUnit = di.getString("KeTiFuZeDanWei");
                            obj.DutyUnitOrg = di.getString("KeTiSuoShuBuMen");
                            obj.DutyUnitAddress = getAddress(di.getString("KeTiSuoShuDiDian"));

                            //处理参加单位分工
                            StringBuilder sbWorkTask = new StringBuilder();
                            DataList items = localContext.table("RenWuBiao").where("KeTiBianHao='" + di.getString("BianHao") + "'").select("*").getDataList();
                            if (items.getRowCount() >= 1)
                            {
                                sbWorkTask.Append("该课题由").Append(items.getRow(0).getString("DanWeiMing")).Append("单位负责，承担").Append(items.getRow(0).getString("RenWuFenGong")).Append("等任务；").AppendLine();
                            }
                            for (int kk = 1; kk < items.getRowCount(); kk++)
                            {
                                DataItem rwb = items.getRow(kk);
                                sbWorkTask.Append(rwb.getString("DanWeiMing")).Append("单位参加，承担").Append(rwb.getString("RenWuFenGong")).Append("等任务；\n");
                            }
                            if (sbWorkTask.Length >= 1)
                            {
                                sbWorkTask.Remove(sbWorkTask.Length - 1, 1);
                            }
                            obj.WorkTask = sbWorkTask.ToString();
                            break;
                    }

                    obj.copyTo(ConnectionManager.Context.table("Subject")).insert();
                }
                #endregion

                #region 导入人员信息
                //处理人员信息
                DataList dlPerson = localContext.table("RenYuanBiao").select("*").getDataList();
                foreach (DataItem di in dlPerson.getRows())
                {
                    Person obj = new Person();
                    obj.PersonID = Guid.NewGuid().ToString();
                    obj.CatalogID = proj.CatalogID;
                    obj.ProjectID = proj.ProjectID;
                    obj.SubjectID = di.getString("KeTiBiaoHao");
                    obj.PersonName = di.getString("XingMing");
                    obj.PersonIDCard = di.getString("ShenFenZhengHao");
                    obj.PersonSex = di.getString("XingBie");
                    obj.PersonJob = di.getString("ZhiCheng");
                    obj.PersonSpecialty = di.getString("ZhuanYe");
                    obj.TotalTime = di.getInt("MeiNianTouRuShiJian");
                    obj.TaskContent = di.getString("RenWuFenGong");
                    obj.WorkUnit = di.getString("GongZuoDanWei");
                    obj.Telephone = di.exists("DianHua") ? di.getString("DianHua") : string.Empty;
                    obj.Mobilephone = di.exists("ShouJi") ? di.getString("ShouJi") : string.Empty;

                    //设置项目中职务
                    obj.JobInProject = di.getString("ZhiWu");

                    //是否为项目负责人
                    switch (di.getString("ShiXiangMuFuZeRen"))
                    {
                        case "rbIsOnlyProject":
                            obj.PersonID = Guid.NewGuid().ToString();
                            obj.SubjectID = string.Empty;
                            obj.IsProjectMaster = "true";
                            //插入数据
                            obj.copyTo(ConnectionManager.Context.table("Person")).insert();
                            break;
                        case "rbIsProjectAndSubject":
                            string oldSubjectID = obj.SubjectID;

                            //保存项目负责人
                            obj.PersonID = Guid.NewGuid().ToString();
                            obj.SubjectID = string.Empty;
                            obj.IsProjectMaster = "true";
                            //插入数据
                            obj.copyTo(ConnectionManager.Context.table("Person")).insert();

                            //保存课题负责人
                            obj.PersonID = Guid.NewGuid().ToString();
                            obj.SubjectID = oldSubjectID;
                            obj.IsProjectMaster = "false";
                            //插入数据
                            obj.copyTo(ConnectionManager.Context.table("Person")).insert();
                            break;
                        case "rbIsOnlySubject":
                            //保存课题负责人或成员
                            obj.PersonID = Guid.NewGuid().ToString();
                            obj.IsProjectMaster = "false";
                            //插入数据
                            obj.copyTo(ConnectionManager.Context.table("Person")).insert();
                            break;
                    }
                }
                #endregion

                #region 写入金额数据
                DataList dlMoneys = localContext.table("YuSuanBiao").select("*").getDataList();
                if (dlMoneys != null && dlMoneys.getRowCount() >= 1)
                {
                    foreach (DataItem di in dlMoneys.getRows())
                    {
                        //添加字典
                        addDict(catalog, proj, "Money,Info,SendRule", (di.get("MingCheng") != null ? di.get("MingCheng").ToString() : string.Empty), (di.get("ShuJu") != null ? di.get("ShuJu").ToString() : string.Empty), string.Empty);
                    }
                }
                DataList dlSendRule = localContext.table("BoFuBiao").orderBy("YuJiShiJian").select("*").getDataList();
                if (dlSendRule != null && dlSendRule.getRowCount() >= 1)
                {
                    int lastYear = 0;
                    int yearIndex = 0;
                    foreach (DataItem di in dlSendRule.getRows())
                    {
                        DateTime date = DateTime.Parse(di.get("YuJiShiJian") != null ? di.get("YuJiShiJian").ToString() : DateTime.Now.ToString());
                        if (date.Year != lastYear)
                        {
                            lastYear = date.Year;
                            yearIndex++;
                        }

                        //添加字典
                        addDict(catalog, proj, "Money,Info,SendRule", "Year" + yearIndex + "_SendDate", date.ToString("yyyy年MM月dd日"), string.Empty);
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

        private string getAddress(string temp)
        {
            string result = temp;
            if (temp != null && temp.Contains("%|||%"))
            {
                string[] tt = temp.Split(new string[] { "%|||%" }, StringSplitOptions.None);
                if (tt != null && tt.Length >= 3)
                {
                    if (!tt[1].Contains(tt[0]))
                    {
                        result = tt[0] + tt[1] + tt[2];
                    }
                    else
                    {
                        result = tt[1] + tt[2];
                    }
                }
            }
            return result;
        }
    }
}
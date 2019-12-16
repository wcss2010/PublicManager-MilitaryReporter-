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
            string catalogVersionStr = "v1.1";

            //附件目录
            string filesDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(sourceFile), "Files");

            //处理项目信息
            DataItem diProject = localContext.table("Project").where("Type = '项目'").select("*").getDataItem();
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
                Catalog catalog = updateAndClearCatalog(catalogNumber, diProject.getString("Name"), "建议书", catalogVersionStr);
                #endregion

                #region 导入项目及课题信息
                //添加项目信息
                Project proj = new Project();
                proj.ProjectID = catalog.CatalogID;
                proj.CatalogID = catalog.CatalogID;
                proj.ProjectName = catalog.CatalogName;
                proj.SecretLevel = diProject.getString("SecretLevel");
                proj.TotalMoney = diProject.get("TotalMoney") != null ? decimal.Parse(diProject.get("TotalMoney").ToString()) : 0;
                proj.Keywords = diProject.getString("Keywords");
                proj.Domains = diProject.getString("Domain");
                proj.DutyUnit = localContext.table("Unit").where("ID='" + diProject.getString("UnitID") + "'").select("UnitName").getValue<string>("未知");
                //proj.DutyUnitOrg = "未知";
                proj.DutyUnitAddress = localContext.table("Unit").where("ID='" + diProject.getString("UnitID") + "'").select("Address").getValue<string>("未知");
                proj.ProjectNumber = string.Empty;
                proj.TotalTime = diProject.getInt("TotalTime"); ;
                proj.copyTo(ConnectionManager.Context.table("Project")).insert();
                
                //处理课题列表
                DataList dlSubject = localContext.table("Project").where("Type = '课题'").select("*").getDataList();
                foreach (DataItem di in dlSubject.getRows())
                {
                    Subject obj = new Subject();
                    obj.SubjectID = di.getString("ID");
                    obj.CatalogID = proj.CatalogID;
                    obj.ProjectID = proj.ProjectID;
                    obj.SubjectName = di.getString("Name");

                    object objMoney = localContext.table("Task").where("Role='负责人' and Type = '课题' and ProjectID = '" + obj.SubjectID + "'").select("TotalMoney").getValue();
                    obj.TotalMoney = objMoney != null ? decimal.Parse(objMoney.ToString()) : 0;

                    obj.WorkDest = System.IO.Path.Combine(filesDir, "课题详细_" + obj.SubjectName + "_研究目标" + ".doc");
                    obj.WorkContent = System.IO.Path.Combine(filesDir, "课题详细_" + obj.SubjectName + "_研究内容" + ".doc");
                    obj.WorkTask = string.Empty;

                    obj.DutyUnit = localContext.table("Unit").where("ID='" + di.getString("UnitID") + "'").select("UnitName").getValue<string>("未知");
                    //obj.DutyUnitOrg = "未知";
                    obj.DutyUnitAddress = localContext.table("Unit").where("ID='" + di.getString("UnitID") + "'").select("Address").getValue<string>("未知");

                    obj.copyTo(ConnectionManager.Context.table("Subject")).insert();
                }
                #endregion

                #region 导入人员信息
                //处理人员信息
                DataList dlTask = localContext.table("Task").select("*").getDataList();
                foreach (DataItem diTask in dlTask.getRows())
                {
                    DataItem diPerson = localContext.table("Person").where("ID = '" + diTask.getString("PersonID") + "'").select("*").getDataItem();
                    if (diPerson != null && diPerson.count() >= 1)
                    {
                        Person obj = new Person();
                        obj.PersonID = Guid.NewGuid().ToString();
                        obj.CatalogID = proj.CatalogID;
                        obj.ProjectID = proj.ProjectID;
                        obj.SubjectID = diTask.getString("ProjectID");
                        obj.PersonName = diPerson.getString("Name");
                        obj.PersonIDCard = diPerson.getString("IDCard");
                        obj.PersonSex = diPerson.getString("Sex");
                        obj.PersonJob = diPerson.getString("Job");
                        obj.PersonSpecialty = diPerson.getString("Specialty");
                        obj.TotalTime = diTask.getInt("TotalTime");
                        obj.TaskContent = diTask.getString("Content");
                        obj.Telephone = diPerson.getString("Telephone");
                        obj.Mobilephone = diPerson.getString("MobilePhone");

                        DataItem diUnit = localContext.table("Unit").where("ID='" + diPerson.getString("UnitID") + "'").select("*").getDataItem();
                        if (diUnit != null && diUnit.count() >= 1)
                        {
                            obj.WorkUnit = diUnit.getString("UnitName");
                        }
                        else
                        {
                            obj.WorkUnit = "未知";
                        }

                        //设置项目中职务
                        obj.JobInProject = diTask.getString("Role");

                        //是否为项目负责人
                        obj.IsProjectMaster = diTask.getString("Type") == "项目" ? "true" : "false";

                        //如果是项目负责人就清空课题ID
                        if (obj.IsProjectMaster == "true")
                        {
                            obj.SubjectID = string.Empty;
                        }

                        //插入数据
                        obj.copyTo(ConnectionManager.Context.table("Person")).insert();
                    }
                }
                #endregion

                #region 导入经费信息表
                DataList dlMoneys = localContext.table("MoneyAndYear").select("*").getDataList();
                if (dlMoneys != null && dlMoneys.getRowCount() >= 1)
                {
                    //关键字映射表
                    Dictionary<string, string> nameDicts = new Dictionary<string, string>();
                    nameDicts["ProjectRFA"] = "Money1";
                    nameDicts["ProjectRFA1"] = "Money2";
                    nameDicts["ProjectRFA1_1"] = "Money3";
                    nameDicts["ProjectRFA1_1_1"] = "Money3_1";
                    nameDicts["ProjectRFA1_1_2"] = "Money3_2";
                    nameDicts["ProjectRFA1_1_3"] = "Money3_3";
                    nameDicts["ProjectRFA1_2"] = "Money4";
                    nameDicts["ProjectRFA1_3"] = "Money5";
                    nameDicts["ProjectRFA1_3_1"] = "Money5_1";
                    nameDicts["ProjectRFA1_3_2"] = "Money5_2";
                    nameDicts["ProjectRFA1_4"] = "Money6";
                    nameDicts["ProjectRFA1_5"] = "Money7";
                    nameDicts["ProjectRFA1_6"] = "Money8";
                    nameDicts["ProjectRFA1_7"] = "Money9";
                    nameDicts["ProjectRFA1_8"] = "Money10";
                    nameDicts["ProjectRFA1_9"] = "Money11";
                    nameDicts["ProjectRFA2"] = "Money12";
                    nameDicts["ProjectRFA2_1"] = "Money12_1";
                    nameDicts["Projectoutlay1"] = "Year1";
                    nameDicts["Projectoutlay2"] = "Year2";
                    nameDicts["Projectoutlay3"] = "Year3";
                    nameDicts["Projectoutlay4"] = "Year4";
                    nameDicts["Projectoutlay5"] = "Year5";
                    nameDicts["ProjectRFArm"] = "Info1";
                    nameDicts["ProjectRFA1rm"] = "Info2";
                    nameDicts["ProjectRFA1_1rm"] = "Info3";
                    nameDicts["ProjectRFA1_1_1rm"] = "Info3_1";
                    nameDicts["ProjectRFA1_1_2rm"] = "Info3_2";
                    nameDicts["ProjectRFA1_1_3rm"] = "Info3_3";
                    nameDicts["ProjectRFA1_2rm"] = "Info4";
                    nameDicts["ProjectRFA1_3rm"] = "Info5";
                    nameDicts["ProjectRFA1_3_1rm"] = "Info5_1";
                    nameDicts["ProjectRFA1_3_2rm"] = "Info5_2";
                    nameDicts["ProjectRFA1_4rm"] = "Info6";
                    nameDicts["ProjectRFA1_5rm"] = "Info7";
                    nameDicts["ProjectRFA1_6rm"] = "Info8";
                    nameDicts["ProjectRFA1_7rm"] = "Info9";
                    nameDicts["ProjectRFA1_8rm"] = "Info10";
                    nameDicts["ProjectRFA1_9rm"] = "Info11";
                    nameDicts["ProjectRFA2rm"] = "Info12";
                    nameDicts["ProjectRFA2_1rm"] = "Info12_1";

                    foreach (DataItem di in dlMoneys.getRows())
                    {
                        //添加字典
                        addDict(catalog, proj, "Money,Info", nameDicts[di.getString("Name")], di.getString("Value"), string.Empty);
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
    }
}
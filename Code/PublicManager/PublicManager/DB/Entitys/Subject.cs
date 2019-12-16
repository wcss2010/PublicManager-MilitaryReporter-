using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys 
{
    /// <summary>
    /// 类Subject。
    /// </summary>
    [Serializable]
    public partial class Subject : IEntity
    {
        public Subject() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("SubjectID", SubjectID);
            query.set("CatalogID", CatalogID);
            query.set("ProjectID", ProjectID);
            query.set("SubjectName", SubjectName);
            query.set("TotalMoney", TotalMoney);
            query.set("WorkDest", WorkDest);
            query.set("WorkContent", WorkContent);
            query.set("WorkTask", WorkTask);
            query.set("DutyUnit", DutyUnit);
            query.set("DutyUnitOrg", DutyUnitOrg);
            query.set("DutyUnitAddress", DutyUnitAddress);

            return query;
        }

        public string SubjectID { get; set; }
        public string CatalogID { get; set; }
        public string ProjectID { get; set; }
        public string SubjectName { get; set; }
        public decimal TotalMoney { get; set; }
        public string WorkDest { get; set; }
        public string WorkContent { get; set; }
        public string WorkTask { get; set; }
        public string DutyUnit { get; set; }
        public string DutyUnitOrg { get; set; }
        public string DutyUnitAddress { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            SubjectID = source("SubjectID").value<string>(Guid.NewGuid().ToString());
            CatalogID = source("CatalogID").value<string>("");
            ProjectID = source("ProjectID").value<string>("");
            SubjectName = source("SubjectName").value<string>("");
            TotalMoney = source("TotalMoney").value<decimal>(0);
            WorkDest = source("WorkDest").value<string>("");
            WorkContent = source("WorkContent").value<string>("");
            WorkTask = source("WorkTask").value<string>("");
            DutyUnit = source("DutyUnit").value<string>("");
            DutyUnitOrg = source("DutyUnitOrg").value<string>("");
            DutyUnitAddress = source("DutyUnitAddress").value<string>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new Subject();
        }
    }
}
using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys
{
    /// <summary>
    /// 类Project。
    /// </summary>
    [Serializable]
    public partial class Project : IEntity
    {
        public Project() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("ProjectID", ProjectID);
            query.set("CatalogID", CatalogID);
            query.set("ProjectName", ProjectName);
            query.set("StudyDest", StudyDest);
            query.set("StudyContent", StudyContent);
            query.set("WillResult", WillResult);
            query.set("StudyTime", StudyTime);
            query.set("StudyMoney", StudyMoney);
            query.set("ProjectSort", ProjectSort);
            query.set("ProfessionSort", ProfessionSort);
            query.set("DutyUnit", DutyUnit);
            query.set("NextUnit", NextUnit);
            query.set("Memo", Memo);
            query.set("Worker", Worker);
            query.set("WorkerCardID", WorkerCardID);
            query.set("WorkerSex", WorkerSex);
            query.set("WorkerNation", WorkerNation);
            query.set("WorkerBirthday", WorkerBirthday);
            query.set("WorkerTelephone", WorkerTelephone);
            query.set("WorkerMobilephone", WorkerMobilephone);
            query.set("SectionJobCateGory", SectionJobCateGory);
            query.set("AllStudyUnit", AllStudyUnit);
            query.set("RequestMoney", RequestMoney);
            query.set("TaskCompleteTime", TaskCompleteTime);

            return query;
        }

        public string ProjectID { get; set; }
        public string CatalogID { get; set; }
        public string ProjectName { get; set; }
        public string StudyDest { get; set; }
        public string StudyContent { get; set; }
        public string WillResult { get; set; }
        public decimal StudyTime { get; set; }
        public decimal StudyMoney { get; set; }
        public string ProjectSort { get; set; }
        public string ProfessionSort { get; set; }
        public string DutyUnit { get; set; }
        public string NextUnit { get; set; }
        public string Memo { get; set; }
        public string Worker { get; set; }
        public string WorkerCardID { get; set; }
        public string WorkerSex { get; set; }
        public string WorkerNation { get; set; }
        public DateTime WorkerBirthday { get; set; }
        public string WorkerTelephone { get; set; }
        public string WorkerMobilephone { get; set; }
        public string SectionJobCateGory { get; set; }
        public string AllStudyUnit { get; set; }
        public decimal RequestMoney { get; set; }
        public DateTime TaskCompleteTime { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            ProjectID = source("ProjectID").value<string>("");
            CatalogID = source("CatalogID").value<string>("");
            ProjectName = source("ProjectName").value<string>("");
            StudyDest = source("StudyDest").value<string>("");
            StudyContent = source("StudyContent").value<string>("");
            WillResult = source("WillResult").value<string>("");
            StudyTime = source("StudyTime").value<decimal>(0);
            StudyMoney = source("StudyMoney").value<decimal>(0);
            ProjectSort = source("ProjectSort").value<string>("");
            ProfessionSort = source("ProfessionSort").value<string>("");
            DutyUnit = source("DutyUnit").value<string>("");
            NextUnit = source("NextUnit").value<string>("");
            Memo = source("Memo").value<string>("");
            Worker = source("Worker").value<string>("");
            WorkerCardID = source("WorkerCardID").value<string>("");
            WorkerSex = source("WorkerSex").value<string>("");
            WorkerNation = source("WorkerNation").value<string>("");
            WorkerBirthday = source("WorkerBirthday").value<DateTime>(DateTime.Now);
            WorkerTelephone = source("WorkerTelephone").value<string>("");
            WorkerMobilephone = source("WorkerMobilephone").value<string>("");
            SectionJobCateGory = source("SectionJobCateGory").value<string>("");
            AllStudyUnit = source("AllStudyUnit").value<string>("");
            RequestMoney = source("RequestMoney").value<decimal>(0);
            TaskCompleteTime = source("TaskCompleteTime").value<DateTime>(DateTime.Now);
        }

        public override Noear.Weed.IBinder clone()
        {
            return new Project();
        }
    }
}
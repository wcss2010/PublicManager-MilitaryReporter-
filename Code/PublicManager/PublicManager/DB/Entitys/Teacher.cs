using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys
{
    /// <summary>
    /// 类Teacher。
    /// </summary>
    [Serializable]
    public partial class Teacher : IEntity
    {
        public Teacher() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("TeacherID", TeacherID);
            query.set("TName", TName);
            query.set("TIDCard", TIDCard);
            query.set("TSex", TSex);
            query.set("TPhone", TPhone);
            query.set("TJob", TJob);
            query.set("TUnit", TUnit);
            query.set("TRange", TRange);

            return query;
        }

        public string TeacherID { get; set; }
        public string TName { get; set; }
        public string TIDCard { get; set; }
        public string TSex { get; set; }
        public string TPhone { get; set; }
        public string TJob { get; set; }
        public string TUnit { get; set; }
        public string TRange { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            TeacherID = source("TeacherID").value<string>(Guid.NewGuid().ToString());
            TName = source("TName").value<string>("");
            TIDCard = source("TIDCard").value<string>("");
            TSex = source("TSex").value<string>("");
            TPhone = source("TPhone").value<string>("");
            TJob = source("TJob").value<string>("");
            TUnit = source("TUnit").value<string>("");
            TRange = source("TRange").value<string>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new Teacher();
        }
    }
}

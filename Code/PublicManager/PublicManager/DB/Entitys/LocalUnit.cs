using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys 
{
    /// <summary>
    /// 类LocalUnit。
    /// </summary>
    [Serializable]
    public partial class LocalUnit : IEntity
    {
        public LocalUnit() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("LocalUnitID", LocalUnitID);
            query.set("LocalUnitName", LocalUnitName);

            return query;
        }

        public string LocalUnitID { get; set; }
        public string LocalUnitName { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            LocalUnitID = source("LocalUnitID").value<string>("");
            LocalUnitName = source("LocalUnitName").value<string>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new LocalUnit();
        }
    }
}
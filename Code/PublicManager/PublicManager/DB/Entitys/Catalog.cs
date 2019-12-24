using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys 
{
    /// <summary>
    /// 类Catalog。
    /// </summary>
    [Serializable]
    public partial class Catalog : IEntity
    {
        public Catalog() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("CatalogID", CatalogID);
            query.set("CatalogNumber", CatalogNumber);
            query.set("CatalogName", CatalogName);
            query.set("CatalogType", CatalogType);
            query.set("CatalogVersion", CatalogVersion);
            query.set("ImportTime", ImportTime);

            return query;
        }

        public string CatalogID { get; set; }
        public string CatalogNumber { get; set; }
        public string CatalogName { get; set; }
        public string CatalogType { get; set; }
        public string CatalogVersion { get; set; }
        public datetime ImportTime { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            CatalogID = source("CatalogID").value<string>("");
            CatalogNumber = source("CatalogNumber").value<string>("");
            CatalogName = source("CatalogName").value<string>("");
            CatalogType = source("CatalogType").value<string>("");
            CatalogVersion = source("CatalogVersion").value<string>("");
            ImportTime = source("ImportTime").value<datetime>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new Catalog();
        }
    }
}

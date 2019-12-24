using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys 
{
    /// <summary>
    /// 类Moneys。
    /// </summary>
    [Serializable]
    public partial class Moneys : IEntity
    {
        public Moneys() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("MoneyID", MoneyID);
            query.set("CatalogID", CatalogID);
            query.set("ProjectID", ProjectID);
            query.set("MoneyName", MoneyName);
            query.set("MoneyValue", MoneyValue);

            return query;
        }

        public string MoneyID { get; set; }
        public string CatalogID { get; set; }
        public string ProjectID { get; set; }
        public string MoneyName { get; set; }
        public string MoneyValue { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            MoneyID = source("MoneyID").value<string>("");
            CatalogID = source("CatalogID").value<string>("");
            ProjectID = source("ProjectID").value<string>("");
            MoneyName = source("MoneyName").value<string>("");
            MoneyValue = source("MoneyValue").value<string>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new Moneys();
        }
    }
}
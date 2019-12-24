using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys 
{
    /// <summary>
    /// 类Professions。
    /// </summary>
    [Serializable]
    public partial class Professions : IEntity
    {
        public Professions() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("ProfessionID", ProfessionID);
            query.set("CatalogID", CatalogID);
            query.set("ProjectID", ProjectID);
            query.set("ProfessionNum", ProfessionNum);
            query.set("ProfessionCategory", ProfessionCategory);
            query.set("ProfessionName", ProfessionName);

            return query;
        }

        public string ProfessionID { get; set; }
        public string CatalogID { get; set; }
        public string ProjectID { get; set; }
        public string ProfessionNum { get; set; }
        public string ProfessionCategory { get; set; }
        public string ProfessionName { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            ProfessionID = source("ProfessionID").value<string>("");
            CatalogID = source("CatalogID").value<string>("");
            ProjectID = source("ProjectID").value<string>("");
            ProfessionNum = source("ProfessionNum").value<string>("");
            ProfessionCategory = source("ProfessionCategory").value<string>("");
            ProfessionName = source("ProfessionName").value<string>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new Professions();
        }
    }
}

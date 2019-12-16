using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys
{
    /// <summary>
    /// 类Dicts。
    /// </summary>
    [Serializable]
    public partial class Dicts : IEntity
    {
        public Dicts() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("DictID", DictID);
            query.set("CatalogID", CatalogID);
            query.set("ProjectID", ProjectID);
            query.set("SubjectID", SubjectID);
            query.set("PersonID", PersonID);
            query.set("DictType", DictType);
            query.set("DictName", DictName);
            query.set("DictValue", DictValue);
            query.set("DictTag", DictTag);
            query.set("ParentDictID", ParentDictID);

            return query;
        }

        public string DictID { get; set; }
        public string CatalogID { get; set; }
        public string ProjectID { get; set; }
        public string SubjectID { get; set; }
        public string PersonID { get; set; }
        public string DictType { get; set; }
        public string DictName { get; set; }
        public string DictValue { get; set; }
        public string DictTag { get; set; }
        public string ParentDictID { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            DictID = source("DictID").value<string>(Guid.NewGuid().ToString());
            CatalogID = source("CatalogID").value<string>("");
            ProjectID = source("ProjectID").value<string>("");
            SubjectID = source("SubjectID").value<string>("");
            PersonID = source("PersonID").value<string>("");
            DictType = source("DictType").value<string>("");
            DictName = source("DictName").value<string>("");
            DictValue = source("DictValue").value<string>("");
            DictTag = source("DictTag").value<string>("");
            ParentDictID = source("ParentDictID").value<string>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new Dicts();
        }
    }
}
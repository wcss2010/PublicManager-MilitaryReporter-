using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys 
{
    /// <summary>
    /// 类Person。
    /// </summary>
    [Serializable]
    public partial class Person : IEntity
    {
        public Person() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("PersonID", PersonID);
            query.set("CatalogID", CatalogID);
            query.set("ProjectID", ProjectID);
            query.set("PersonName", PersonName);
            query.set("PersonIDCard", PersonIDCard);
            query.set("PersonNation", PersonNation);
            query.set("PersonSex", PersonSex);
            query.set("PersonBirthday", PersonBirthday);
            query.set("PersonJob", PersonJob);
            query.set("PersonSpecialty", PersonSpecialty);
            query.set("JobInProject", JobInProject);
            query.set("IsProjectMaster", IsProjectMaster);
            query.set("WorkUnit", WorkUnit);
            query.set("Telephone", Telephone);
            query.set("Mobilephone", Mobilephone);

            return query;
        }

        public string PersonID { get; set; }
        public string CatalogID { get; set; }
        public string ProjectID { get; set; }
        public string PersonName { get; set; }
        public string PersonIDCard { get; set; }
        public string PersonNation { get; set; }
        public string PersonSex { get; set; }
        public datetime PersonBirthday { get; set; }
        public string PersonJob { get; set; }
        public string PersonSpecialty { get; set; }
        public string JobInProject { get; set; }
        public string IsProjectMaster { get; set; }
        public string WorkUnit { get; set; }
        public string Telephone { get; set; }
        public string Mobilephone { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            PersonID = source("PersonID").value<string>("");
            CatalogID = source("CatalogID").value<string>("");
            ProjectID = source("ProjectID").value<string>("");
            PersonName = source("PersonName").value<string>("");
            PersonIDCard = source("PersonIDCard").value<string>("");
            PersonNation = source("PersonNation").value<string>("");
            PersonSex = source("PersonSex").value<string>("");
            PersonBirthday = source("PersonBirthday").value<datetime>("");
            PersonJob = source("PersonJob").value<string>("");
            PersonSpecialty = source("PersonSpecialty").value<string>("");
            JobInProject = source("JobInProject").value<string>("");
            IsProjectMaster = source("IsProjectMaster").value<string>("");
            WorkUnit = source("WorkUnit").value<string>("");
            Telephone = source("Telephone").value<string>("");
            Mobilephone = source("Mobilephone").value<string>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new Person();
        }
    }
}

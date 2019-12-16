using System;
using System.Data;
using System.Text;

namespace PublicManager.DB.Entitys
{
    /// <summary>
    /// 类TeacherComment。
    /// </summary>
    [Serializable]
    public partial class TeacherComment : IEntity
    {
        public TeacherComment() { }

        public override Noear.Weed.DbTableQuery copyTo(Noear.Weed.DbTableQuery query)
        {
            //设置值
            query.set("TeacherCommentID", TeacherCommentID);
            query.set("CommentDate", CommentDate);
            query.set("TeacherID", TeacherID);
            query.set("CommentText", CommentText);

            return query;
        }

        public string TeacherCommentID { get; set; }
        public DateTime CommentDate { get; set; }
        public string TeacherID { get; set; }
        public string CommentText { get; set; }

        public override void bind(Noear.Weed.GetHandlerEx source)
        {
            TeacherCommentID = source("TeacherCommentID").value<string>(Guid.NewGuid().ToString());
            CommentDate = source("CommentDate").value<DateTime>(DateTime.Now);
            TeacherID = source("TeacherID").value<string>("");
            CommentText = source("CommentText").value<string>("");
        }

        public override Noear.Weed.IBinder clone()
        {
            return new TeacherComment();
        }
    }
}
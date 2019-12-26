using System; 
using System.Collections.Generic; 
using System.Text; 
using Noear.Weed; 
 
namespace PublicManager.DB 
{ 
    public class ConnectionManager 
    { 
        private static System.Data.Common.DbProviderFactory factory = null; 
 
        /// <summary> 
        /// 数据库上下文 
        /// </summary> 
        public static DbContext Context { get; set; } 
 
        /// <summary> 
        /// 初始化数据库连接 
        /// </summary> 
        /// <param name="schemaName">连接名称</param> 
        /// <param name="connStr">连接字符串</param> 
        public static void Open(string schemaName,string connStr) 
        {
            ConnectionString = connStr;

            factory = new System.Data.SQLite.SQLiteFactory(); 
            Context = new DbContext(schemaName, connStr, factory); 
            //是否在执入后执行查询（主要针对Sqlite）
            Context.IsSupportSelectIdentityAfterInsert = false; 
            //是否在Dispose后执行GC用于解决Dispose后无法删除的问题（主要针对Sqlite）
            Context.IsSupportGCAfterDispose = true; 
        } 
 
        public static void Close() 
        { 
            try 
            { 
                //factory.Dispose(); 
            } 
            catch (Exception ex) { } 
            factory = null; 
 
            try 
            { 
                Context.Dispose(); 
            } 
            catch (Exception ex) { } 
            Context = null; 
        }

        public static string ConnectionString { get; set; }
    } 
}
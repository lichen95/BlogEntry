using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.Service
{
    using NiuNiu.BlogEntry.Common;
    using SqlSugar;
    using System.Linq;

    public static class SqlSugarHelper
    {
        public static SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                InitKeyType = InitKeyType.Attribute,
                ConnectionString = Config.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
            return db;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.Service
{
    using Entity;
    using IService;
    using NiuNiu.BlogEntry.Entity.DTO;

    /// <summary>
    /// 权限数据处理层
    /// ** 创始时间：2019-03-01
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public class PermissionsService : IPermissionsService
    {
        public bool Add(Permissions t)
        {
            return SqlSugarHelper.GetInstance().Insertable<Permissions>(t).ExecuteCommand() > 0;
        }

        public bool Delete(string Ids)
        {
            var id = Ids.Split(',');
            int[] output = Array.ConvertAll<string, int>(id, delegate (string s) { return int.Parse(s); });
            return SqlSugarHelper.GetInstance().Deleteable<Permissions>().In(id).ExecuteCommand() > 0;
        }

        public PageResult<Permissions> Query(PageResult<Permissions> model)
        {
            int totalCount = 0;
            model.Data = SqlSugarHelper.GetInstance().Queryable<Permissions>().ToPageList(model.pageIndex, model.pageSize, ref totalCount);
            model.totalCount = totalCount;
            return model;
        }

        public Permissions QueryById(int Id)
        {
            return SqlSugarHelper.GetInstance().Queryable<Permissions>().Where(m => m.PermissionId.Equals(Id)).First() ?? null;
        }

        public bool Update(Permissions t)
        {
            return SqlSugarHelper.GetInstance().Updateable<Permissions>(t).ExecuteCommand() > 0;
        }
    }
}

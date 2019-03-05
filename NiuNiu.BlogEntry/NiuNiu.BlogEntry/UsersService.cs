using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.Service
{
    using Entity;
    using IService;
    using NiuNiu.BlogEntry.Entity.DTO;

    /// <summary>
    /// 用户数据处理层
    /// ** 创始时间：2019-02-27
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public class UsersService : IUsersService
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Add(Users t)
        {
           return SqlSugarHelper.GetInstance().Insertable<Users>(t).ExecuteCommand()>0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public bool Delete(string Ids)
        {
            var id = Ids.Split(',');
            int[] output = Array.ConvertAll<string, int>(id, delegate (string s) { return int.Parse(s); });
            return SqlSugarHelper.GetInstance().Deleteable<Users>().In(id).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public Users Login(string Name, string Pwd)
        {
            return SqlSugarHelper.GetInstance().Queryable<Users>().Where(m=>m.UserName.Equals(Name)&&m.Password.Equals(Pwd)).First()??null;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public PageResult<Users> Query(PageResult<Users> model)
        {
            int totalCount = 0;
            model.Data= SqlSugarHelper.GetInstance().Queryable<Users>().ToPageList(model.pageIndex, model.pageSize, ref totalCount);
            model.totalCount = totalCount;
            return model;
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Users QueryById(int Id)
        {
            return SqlSugarHelper.GetInstance().Queryable<Users>().Where(m => m.Id.Equals(Id)).First() ?? null;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(Users t)
        {
            return SqlSugarHelper.GetInstance().Updateable<Users>(t).ExecuteCommand() > 0;
        }
    }
}

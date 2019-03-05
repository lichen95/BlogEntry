using NiuNiu.BlogEntry.Entity;
using NiuNiu.BlogEntry.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.IService
{
    /// <summary>
    /// ** 描述：后台管理用户DAL接口
    /// ** 创始时间：2019-03-01
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public interface IAdmin_UsersService:IBase<Admin_Users>
    {
        PageResult<Admin_Users> Query(PageResult<Admin_Users> model);
    }
}

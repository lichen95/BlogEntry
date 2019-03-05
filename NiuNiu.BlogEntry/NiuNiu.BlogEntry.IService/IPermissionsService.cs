using NiuNiu.BlogEntry.Entity;
using NiuNiu.BlogEntry.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.IService
{
    /// <summary>
    /// ** 描述：后台权限DAL接口
    /// ** 创始时间：2019-03-01
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public interface IPermissionsService:IBase<Permissions>
    {
        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <returns></returns>
        PageResult<Permissions> Query(PageResult<Permissions> model);
    }
}

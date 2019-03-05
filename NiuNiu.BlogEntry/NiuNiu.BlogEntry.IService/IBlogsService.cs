using System;
using System.Collections.Generic;

namespace NiuNiu.BlogEntry.IService
{
    using Entity;
    using NiuNiu.BlogEntry.Entity.DTO;

    /// <summary>
    /// ** 描述：博客DAL接口
    /// ** 创始时间：2019-01-21
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public interface IBlogsService : IBase<Blogs>
    {
        /// <summary>
        /// 获取博客信息
        /// </summary>
        /// <returns></returns>
        PageResult<Blogs> Query(PageResult<Blogs> model);
    }
}

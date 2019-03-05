using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.IService
{
    using Entity;
    using NiuNiu.BlogEntry.Entity.DTO;

    /// <summary>
    /// ** 描述：博客类型DAL接口
    /// ** 创始时间：2019-01-21
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public interface IBlogTypesService : IBase<BlogTypes>
    {
        /// <summary>
        /// 获取博客类型信息
        /// </summary>
        /// <returns></returns>
        PageResult<BlogTypes> Query(PageResult<BlogTypes> model);
    }
}

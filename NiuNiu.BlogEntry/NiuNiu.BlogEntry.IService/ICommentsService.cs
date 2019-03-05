using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.IService
{
    using Entity;
    using NiuNiu.BlogEntry.Entity.DTO;

    /// <summary>
    /// ** 描述：评论DAL接口
    /// ** 创始时间：2019-01-21
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public interface ICommentsService : IBase<Comments>
    {
        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <returns></returns>
        PageResult<Comments> Query(PageResult<Comments> model);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NiuNiu.BlogEntry.IService
{
    /// <summary>
    /// ** 描述：IDAL接口基类
    /// ** 创始时间：2019-01-21
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public interface IBase<T> where T:class,new ()
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">实体</param>
        /// <returns></returns>
        bool Add(T t);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">id集合</param>
        /// <returns></returns>
        bool Delete(string Ids);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t">实体</param>
        /// <returns></returns>
        bool Update(T t);

        /// <summary>
        /// 根据Id获取信息
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns></returns>
        T QueryById(int Id);
    }
}

using System;

namespace NiuNiu.BlogEntry.Entity
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// ** 描述：博客类型表
    /// ** 创始时间：2019-01-21
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public partial class BlogTypes
    {
        public BlogTypes()
        {
            CreateDate = DateTime.Now;
            IsDel = 0;

        }
        /// <summary>
        /// Desc:类型ID
        /// Default:
        /// Nullable:False
        /// </summary>    
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Desc:类型名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:PID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int PId { get; set; }

        /// <summary>
        /// Desc:是否删除
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int IsDel { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CreateDate { get; set; }

    }
}

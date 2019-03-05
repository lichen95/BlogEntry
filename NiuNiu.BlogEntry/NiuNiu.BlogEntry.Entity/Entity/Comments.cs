using System;

namespace NiuNiu.BlogEntry.Entity
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// ** 描述：评论表
    /// ** 创始时间：2019-01-21
    /// ** 修改时间：-
    /// ** 作者：lc
    /// </summary>
    public partial class Comments
    {
        public Comments()
        {
            CreateDate = DateTime.Now;
            IsDel = 0;

        }
        /// <summary>
        /// Desc:评论消息id
        /// Default:
        /// Nullable:False
        /// </summary>    
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Desc:评论内容
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Content { get; set; }

        /// <summary>
        /// Desc:评论博客
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int BlogId { get; set; }

        /// <summary>
        /// Desc:评论人
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int UserId { get; set; }

        /// <summary>
        /// Desc:评论时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Desc:父级ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int PId { get; set; }

        /// <summary>
        /// Desc:是否删除
        /// Default: 0 默认 1删除
        /// Nullable:False
        /// </summary>           
        public int IsDel { get; set; }
    }
}

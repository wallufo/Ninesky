using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Core.Category
{
    /// <summary>
    /// 栏目模型类
    /// </summary>
    public class Category
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int CategoryID { get; set; }
        /// <summary>
        /// 栏目类型
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [Display(Name = "栏目类型")]
        public CategoryType Type { get; set; }
        /// <summary>
        /// 父栏目
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "父栏目ID")]
        public int ParentID { get; set; }
        /// <summary>
        /// 父栏目路径格式
        /// </summary>
        [Display(Name = "父栏目路径")]
        public string ParentPath { get; set; }
        /// <summary>
        /// 深度
        /// </summary>
        [Display(Name = "深度")]
        public int Depth { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        [Display(Name = "栏目名称")]
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(50,ErrorMessage ="不超过{1}")]
        public string Name { get; set; }
        /// <summary>
        /// 栏目描述
        /// </summary>
        [Required(ErrorMessage ="必须输入{0}")]
        [Display(Name="栏目描述")]
        [StringLength(1000,ErrorMessage ="不超过{1}")]
        public string Description { get; set; }
        /// <summary>
        /// 栏目排序
        /// </summary>
        [Display(Name="栏目排序")]
        public int Order { get; set; }
        /// <summary>
        /// 打开方式
        /// </summary>
        [Display(Name="打开方式")]
        public string Target { get; set; }
    }
}

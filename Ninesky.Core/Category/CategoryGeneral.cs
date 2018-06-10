using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Core.Category
{
    /// <summary>
    /// 常规栏目
    /// </summary>
    public class CategoryGeneral
    {
        [Key]
        public int CategoryGeneralID { get; set; }
        /// <summary>
        /// 栏目ID
        /// </summary>
        [Required()]
        [Display(Name = "栏目ID")]
        public int CategoryID { get; set; }

        /// <summary>
        /// 栏目视图
        /// </summary>
        [Required()]
        [Display(Name = "栏目视图")]
        public string View { get; set; }

        /// <summary>
        /// 内容类型ID【0时表示此栏目无内容模型，不能添加内容】
        /// </summary>
        [Required()]
        [Display(Name = "内容类型")]
        public int ContentTypeID { get; set; }

        /// <summary>
        /// 栏目视图
        /// </summary>
        [Display(Name = "内容视图")]
        public string ContentView { get; set; }
    }
}

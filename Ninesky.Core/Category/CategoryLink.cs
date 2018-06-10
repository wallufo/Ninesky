using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.Core.Category
{
    /// <summary>
    /// 外部链接模型
    /// </summary>
    public class CategoryLink
    {
        [Key]
        public int CategoryLinkID { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "栏目ID")]
        public int CategoryID { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(255, ErrorMessage = "必须少于{1}个字")]
        [Display(Name = "链接地址")]
        public string Url { get; set; }
    }
}

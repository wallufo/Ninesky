using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Core
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(20,MinimumLength =2,ErrorMessage ="{0}长度{2}-{1}个字符")]
        [Display(Name ="名称")]
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(1000,ErrorMessage ="{0}必须少于{1}个字符")]
        [Display(Name ="说明")]
        public string Description { get; set; }
    }
}

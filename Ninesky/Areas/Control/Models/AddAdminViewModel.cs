using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Areas.Control.Models
{
    public class AddAdminViewModel
    {
        /// <summary>
        /// 帐号
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name ="账号")]
        public string Accounts { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="{0}长度为{1}个字符")]
        [Display(Name ="密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
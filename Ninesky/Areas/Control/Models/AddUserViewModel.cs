using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Ninesky.Areas.Control.Models
{
    /// <summary>
    /// 添加用户视图模型类
    /// </summary>
    public class AddUserViewModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required(ErrorMessage ="必须选择{0}")]
        [Display(Name ="角色ID")]
        public int RoleID { get; set; }
        [Remote("CanUsername", "User", HttpMethod = "Post", ErrorMessage = "用户名已存在")]
        [StringLength(50,MinimumLength =4,ErrorMessage ="{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage ="必须输入{0}")]
        [Display(Name ="用户名")]
        public string Username { get; set; }
        /// <summary>
        /// 姓名【可做昵称、真实姓名等】
        /// </summary>
        [StringLength(20, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 性别【0-女，1-男，2-保密】
        /// </summary>
        [Required(ErrorMessage = "必须选择{0}")]
        [Range(0, 2, ErrorMessage = "{0}范围{1}-{2}")]
        [Display(Name = "性别")]
        public int Sex { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [DataType(DataType.Password)]
        [StringLength(256, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "必须输入{0}")]
        [DataType(DataType.EmailAddress)]
        [Remote("CanEmail", "User", HttpMethod = "Post", ErrorMessage = "Email已存在")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
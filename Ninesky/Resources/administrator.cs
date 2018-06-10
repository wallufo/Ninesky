using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Core
{
    /// <summary>
    /// 管理员模型
    /// </summary>
    public class Administrator
    {
        [Key]
        public int AdministratorID { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        [Required(ErrorMessage ="必须输入{0}")]
        [StringLength(30,MinimumLength =4,ErrorMessage ="{0}长度{2}-{1}个字符")]
        [Display(Name ="账号")]
        public string Accounts { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage ="必须输入{0}")]
        [DataType(DataType.Password)]
        [StringLength(256,ErrorMessage ="{0}长度少于{1}个字符")]
        [Display(Name ="密码")]
        public string  Password { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        [Display(Name ="登录IP")]
        public string LoginIP { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        [Display(Name ="登录时间")]
        public Nullable<DateTime> LoginTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name ="创建时间")]
        public DateTime CreateTime { get; set; }

        
    }
}

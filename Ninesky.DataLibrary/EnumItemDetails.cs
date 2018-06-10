using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DataLibrary
{
    /// <summary>
    /// 枚举详细信息
    /// </summary>
    public class EnumItemDetails
    {
        /// <summary>
        /// 枚举的字符串形式
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 枚举值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 标注名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标注详细说明
        /// </summary>
        public string Description { get; set; }
    }
}

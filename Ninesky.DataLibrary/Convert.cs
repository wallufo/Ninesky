using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ninesky.DataLibrary
{
    /// <summary>
    /// 转换类
    /// </summary>
    public class Convert
    {
        public static List<EnumItemDetails> EnumToDetailsList(Enum _enum)
        {
            List<EnumItemDetails> _itemDetails = new List<EnumItemDetails>();
            FieldInfo _fieldInfo;
            DisplayAttribute _displayAttribute;
            foreach(var _item in Enum.GetValues(_enum.GetType()))
            {
                try
                {
                    _fieldInfo = _item.GetType().GetField(_item.ToString());
                    _displayAttribute = (DisplayAttribute)_fieldInfo.GetCustomAttribute(typeof(DisplayAttribute));
                    if (_displayAttribute != null) _itemDetails.Add(new EnumItemDetails() { Text = _item.ToString(), Value = (int)_item, Name = _displayAttribute.Name, Description = _displayAttribute.Description });
                    else _itemDetails.Add(new EnumItemDetails() { Text = _item.ToString(), Value = (int)_item });

                }
                catch
                {
                    _itemDetails.Add(new EnumItemDetails() { Text = _item.ToString(), Value = (int)_item });
                }
            }
            return _itemDetails;
        }
    }
}

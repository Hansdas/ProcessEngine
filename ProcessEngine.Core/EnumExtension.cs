using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessEngine.Core
{
   public static class EnumExtension
    {
        public static object GetValue(this Enum @enum)
        {
           return @enum.GetType().GetField(@enum.ToString()).GetRawConstantValue();
        }
        public static Dictionary<string,int> ConvertToDictionary(this Type enumType)
        {
            Array enumValues=  Enum.GetValues(enumType);
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach(var item in enumValues)
            {
               string enumName= Enum.GetName(enumType, item);
                dic.Add(enumName, (int)item);
            }
            return dic;
        }
        public static IList<EnumTextValue> ToKeyValueList(this Type enumType)
        {
            Array enumValues = Enum.GetValues(enumType);
            IList<EnumTextValue> list = new List<EnumTextValue>();
            foreach (var item in enumValues)
                list.Add(new EnumTextValue(Enum.GetName(enumType, item), (int)item));
            return list;
        }
    }
    /// <summary>
    /// 枚举键值类
    /// </summary>
    public struct EnumTextValue
    {
        public EnumTextValue(string text,int value) 
        {
            Text = text;
            Value = value;
        }
        public string Text { get; private set; }
        public int Value { get; private set; }
    }
}

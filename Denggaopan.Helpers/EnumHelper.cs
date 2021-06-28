using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value, bool nameInstend = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null && nameInstend == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }

        public static List<EnumModel> GetList<T>()
        {
            var list = new List<EnumModel>();
            foreach (T t in Enum.GetValues(typeof(T)))
            {
                var item = new EnumModel();
                item.Id = t.GetHashCode();
                item.Name = t.ToString();
                var field = t.GetType().GetField(item.Name);
                var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                item.Description = attr != null ? attr.Description : item.Name;
                list.Add(item);
            }
            return list;
        }
    }

    public class EnumModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    
}

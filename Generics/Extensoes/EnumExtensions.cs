namespace Generics.Extensoes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class EnumExtensions
    {
        public static string DisplayName(this Enum enumValue) 
        {
            DisplayAttribute da= enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();
            if (da != null)
                return da.Name;
            else
                return enumValue.GetType().Name;
        }
    }
}

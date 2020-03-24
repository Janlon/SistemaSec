namespace Generics.Extensoes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class GenericsExtensions
    {
        public static bool IsGenericList(this object value)
        {
            var oType = value.GetType();
            return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }
        public static bool IsGenericList<T>(this T value) where T : class
        {
            var oType = value.GetType();
            return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }
    }
}

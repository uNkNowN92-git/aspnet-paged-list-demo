using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;


namespace PagedListDemo
{
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class ServiceInterfaceAttribute : Attribute
    {
    }

    public class Utility
    {
        /// <summary>
        /// Retrieve all class with custome attribute
        /// </summary>
        /// <typeparam name="T">Out Object</typeparam>
        /// <param name="referenceName">Attribute Name</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetClassListWithAttribute<T>(string referenceName) where T : Attribute
        {
            var types = (from lAssembly in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name == referenceName)
                         from lType in lAssembly.GetTypes()
                         where lType.GetInterfaces().Where(i => i.GetCustomAttributes<T>().Any()).Any()
                         select lType);

            return types;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Out Object</typeparam>
        /// <param name="referenceName">Attribute Name</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetClassWithInherited<T>(string referenceName)
        {
            var types = (from lAssembly in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name == referenceName)
                         from lType in lAssembly.GetTypes()
                         where lType.BaseType == typeof(T)
                         select lType);
            return types;
        }
    }
}
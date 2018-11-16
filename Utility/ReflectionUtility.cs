using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public static class ReflectionUtility
    {
        public static IEnumerable<Assembly> Assemblies
        {
            get
            {
                yield return Assembly.GetExecutingAssembly();
            }
        }

        public static IEnumerable<Type> AllTypes
        {
            get
            {
                foreach (Assembly assembly in Assemblies)
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        yield return type;
                    }
                }
            }
        }

        public static Type GetTypeWithName(string name)
        {
            return AllTypes.FirstOrDefault(type => type.FullName.EndsWith(name));
        }

        public static IEnumerable<Type> GetInheritedTypes(Type parentType, bool includeParent = false)
        {
            if(includeParent)
            {
                yield return parentType;
            }

            foreach (Type type in AllTypes)
            {
                if(type.IsSubclassOf(parentType))
                {
                    yield return type;
                }
            }
        }
    }
}

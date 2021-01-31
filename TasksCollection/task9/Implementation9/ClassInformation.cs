using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Implementation9
{
    public class ClassInformation
    {
        public Assembly Assembly { get; set; }

        public ClassInformation(string path)
        {
            Assembly = Assembly.LoadFrom(path);
        }

        public List<Type> GetTypesOfImplementingClasses(Type baseType)
        {
            List<Type> types = new List<Type>();
            foreach (Type type in Assembly.GetTypes())
            {
                if (type.GetInterfaces().Contains(baseType))
                    types.Add(type);
            }
            return types;
        }
    }
}

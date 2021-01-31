using System;
using System.Reflection;
using System.Text;

namespace Implementation7
{
    public class ClassInfo
    {
        public Type ClassType { get; private set; }

        public ConstructorInfo[] ClassConstructors { get; private set; }

        public MethodInfo[] ClassMethods { get; private set; }

        public ClassInfo(Type type, ConstructorInfo[] constructors, MethodInfo[] methods)
        {
            ClassType = type;
            ClassConstructors = constructors;
            ClassMethods = methods;
        }

        public string GetClassName()
        {
            return ClassType.Name;
        }

        public string[] GetMethodNames()
        {
            string[] methods = new string[ClassConstructors.Length + ClassMethods.Length];
            for (int i = 0; i < ClassConstructors.Length; i++)
            {
                methods[i] = ClassConstructors[i].Name;
            }
            for (int i = ClassConstructors.Length; i < methods.Length; i++)
            {
                methods[i] = ClassMethods[i - ClassConstructors.Length].Name;
            }
            return methods;
        }

        public string GetMethodParameters(int index)
        {
            ParameterInfo[] parameters = IsConstructor(index) ?
                ClassConstructors[index].GetParameters() :
                ClassMethods[index - ClassConstructors.Length].GetParameters();
            if (parameters.Length == 0)
            {
                return "null; ";
            }
            StringBuilder builder = new StringBuilder();
            foreach (ParameterInfo parameter in parameters)
            {
                builder.Append($"{parameter.Name}; ");
            }
            return builder.ToString();
        }

        public bool IsConstructor(int index)
        {
            return index < ClassConstructors.Length;
        }
    }
}

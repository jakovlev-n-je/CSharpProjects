using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Implementation7
{
    public class ClassInformation
    {
        public Type Type { get; private set; }

        public ConstructorInfo[] Constructors { get; private set; }

        public MethodInfo[] Methods { get; private set; }

        public ClassInformation(Type type, ConstructorInfo[] constructors, MethodInfo[] methods)
        {
            Type = type;
            Constructors = constructors;
            ConstructorsSort();
            Methods = methods;
            MethodsSort();
        }

        public string GetClassName()
        {
            return Type.Name;
        }

        public string[] GetMethodAndConstructorNames()
        {
            string[] names = new string[Constructors.Length + Methods.Length];
            for (int i = 0; i < Constructors.Length; i++)
            {
                names[i] = Constructors[i].Name;
            }
            for (int i = Constructors.Length; i < names.Length; i++)
            {
                names[i] = Methods[i - Constructors.Length].Name;
            }
            return names;
        }

        public string GetMethodParameters(int index)
        {
            ParameterInfo[] parameters = IsConstructor(index) ? Constructors[index].GetParameters() : Methods[index - Constructors.Length].GetParameters();
            if (parameters.Length == 0)
            {
                return "null; ";
            }
            StringBuilder builder = new StringBuilder();
            foreach (ParameterInfo parameter in parameters)
            {
                builder.Append($"{parameter.ParameterType.Name}: {parameter.Name}; ");
            }
            return builder.ToString();
        }

        public Type[] GetMethodParametersTypes(int index)
        {
            List<Type> types = new List<Type>();
            if (IsConstructor(index))
            {
                foreach (ParameterInfo parameterInfo in Constructors[index].GetParameters())
                {
                    types.Add(parameterInfo.ParameterType);
                }
            }
            else
            {
                foreach (ParameterInfo parameterInfo in Methods[index - Constructors.Length].GetParameters())
                {
                    types.Add(parameterInfo.ParameterType);
                }
            }
            return types.ToArray();
        }

        public bool IsConstructor(int index)
        {
            return index < Constructors.Length;
        }

        public override string ToString()
        {
            return Type.Name;
        }

        private void ConstructorsSort()
        {
            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 1; i < Constructors.Length; i++)
                {
                    if (Constructors[i].Name.CompareTo(Constructors[i - 1].Name) == -1)
                    {
                        ConstructorInfo tmp = Constructors[i];
                        Constructors[i] = Constructors[i - 1];
                        Constructors[i - 1] = tmp;
                        isSorted = false;
                    }
                }
            }
        }

        private void MethodsSort()
        {
            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 1; i < Methods.Length; i++)
                {
                    if (Methods[i].Name.CompareTo(Methods[i - 1].Name) == -1)
                    {
                        MethodInfo tmp = Methods[i];
                        Methods[i] = Methods[i - 1];
                        Methods[i - 1] = tmp;
                        isSorted = false;
                    }
                }
            }
        }
    }
}

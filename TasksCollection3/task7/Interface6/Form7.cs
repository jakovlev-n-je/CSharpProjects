using Implementation7;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Interface7
{
    public partial class Form7 : Form
    {
        public List<ClassInformation> ClassesInformation;

        public List<ISpaceObject> ObjectInstances;

        public Form7()
        {
            InitializeComponent();
            ObjectInstances = new List<ISpaceObject>();
            textBox1.Text = Directory.GetCurrentDirectory() + @"\SpaceLibrary.dll";
        }

        private object[] ConvertParameters(Type[] methodParametersTypes)
        {
            string[] stringParameters = textBox3.Text.Trim().Split(';');
            textBox3.Text = "";
            if (stringParameters[0].Trim() == "")
            {
                return new object[0];
            }
            if (stringParameters.Length != methodParametersTypes.Length)
            {
                throw new TargetParameterCountException();
            }
            object[] objectParameters = new object[methodParametersTypes.Length];
            for (int i = 0; i < objectParameters.Length; i++)
            {
                objectParameters[i] = Convert.ChangeType(stringParameters[i], methodParametersTypes[i]);
            }
            return objectParameters;
        }

        private bool ContainsObject(string name)
        {
            foreach (ISpaceObject spaceObject in ObjectInstances)
            {
                if (spaceObject.ObjectName == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void InitializeClasses()
        {
            ClassesInformation = new List<ClassInformation>();
            foreach (Type type in Assembly.LoadFrom(textBox1.Text.Trim()).GetTypes())
            {
                if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(ISpaceObject)))
                {
                    ClassesInformation.Add(new ClassInformation(type, type.GetConstructors(), type.GetMethods()));
                }
            }
        }

        private void UpdateComboBox1()
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = ClassesInformation;
        }

        private void UpdateListBox1()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = ClassesInformation[comboBox1.SelectedIndex].GetMethodAndConstructorNames();
        }

        private void UpdateListBox2()
        {
            listBox2.DataSource = null;
            listBox2.DataSource = ObjectInstances;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeClasses();
                comboBox1.Enabled = true;
                UpdateComboBox1();
                ShowInformationBox("Классы были успешно загружены из библиотеки!");
            }
            catch
            {
                ShowErrorBox("Задан некорректный путь к библиотеке классов!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClassesInformation[comboBox1.SelectedIndex].IsConstructor(listBox1.SelectedIndex))
                {
                    InvokeConstructor(comboBox1.SelectedIndex, listBox1.SelectedIndex);
                }
                else
                {
                    InvokeMethod(comboBox1.SelectedIndex, listBox1.SelectedIndex);
                }
            }
            catch (TargetParameterCountException)
            {
                ShowErrorBox("Указано некорректное число параметров!");
            }
            catch (MissingMethodException)
            {
                ShowErrorBox("Указаны некорректные параметры!");
            }
            catch (FormatException)
            {
                ShowErrorBox("Указаны некорректные параметры!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                listBox1.Enabled = true;
                UpdateListBox1();
            }
            else
            {
                listBox1.Enabled = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                textBox2.Text = ClassesInformation[comboBox1.SelectedIndex].GetMethodParameters(listBox1.SelectedIndex);
                button2.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                textBox2.Text = "";
                button2.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = listBox2.SelectedIndex > -1 ?
                            ObjectInstances[listBox2.SelectedIndex].GetObjectInformation().Replace("\n", Environment.NewLine) :
                            "";
        }

        private void CreateRandomInstance(int classIndex, int methodIndex, object[] parameters)
        {
            ISpaceObject spaceObject = (ISpaceObject)ClassesInformation[classIndex].Methods[methodIndex].Invoke(null, parameters);
            if (ContainsObject(spaceObject.ObjectName))
            {
                ShowErrorBox("Экземпляр класса с таким названием уже присутствует в списке!");
                return;
            }
            ObjectInstances.Add(spaceObject);
            UpdateListBox2();
            ShowInformationBox("Экземпляр класса был создан и успешно добавлен в список!");
        }

        private void InvokeConstructor(int classIndex, int constructorIndex)
        {
            object[] parameters = ConvertParameters(ClassesInformation[classIndex].GetMethodParametersTypes(constructorIndex));
            ISpaceObject spaceObject = (ISpaceObject)Activator.CreateInstance(ClassesInformation[classIndex].Type, parameters);
            if (spaceObject == null)
            {
                ShowErrorBox("Указанные параметры не удовлетворяют требуемым условиям!");
                return;
            }
            if (ContainsObject(spaceObject.ObjectName))
            {
                ShowErrorBox("Экземпляр класса с таким названием уже присутствует в списке!");
                return;
            }
            ObjectInstances.Add(spaceObject);
            UpdateListBox2();
            ShowInformationBox("Экземпляр класса был создан и успешно добавлен в список!");
        }

        private void InvokeMethod(int classIndex, int methodIndex)
        {
            object[] parameters = ConvertParameters(ClassesInformation[classIndex].GetMethodParametersTypes(methodIndex));
            methodIndex -= ClassesInformation[comboBox1.SelectedIndex].Constructors.Length;
            if (ClassesInformation[classIndex].Methods[methodIndex].IsStatic)
            {
                CreateRandomInstance(classIndex, methodIndex, parameters);
                return;
            }
            int instanceIndex = listBox2.SelectedIndex;
            if (instanceIndex == -1)
            {
                ShowErrorBox("Не указан экземпляр класса!");
                return;
            }
            if (ObjectInstances[instanceIndex].GetType() != ClassesInformation[classIndex].Type)
            {
                ShowErrorBox("Указан неправильный экземпляр класса!");
                return;
            }
            string text = ClassesInformation[classIndex].Methods[methodIndex].ReturnType != typeof(void) ?
                          ClassesInformation[classIndex].Methods[methodIndex].Invoke(ObjectInstances[instanceIndex], parameters).ToString() :
                          "Метод был успешно вызван!";
            if (ObjectInstances[instanceIndex].ObjectName == null)
            {
                ObjectInstances.RemoveAt(instanceIndex);
            }
            UpdateListBox2();
            ShowMethodResultBox($"Результат вызова метода:\n{text}");
        }

        private void ShowErrorBox(string text)
        {
            MessageBox.Show(
                text,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
        }

        private void ShowInformationBox(string text)
        {
            MessageBox.Show(
                text,
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }

        private void ShowMethodResultBox(string text)
        {
            MessageBox.Show(
                text,
                "Результат вызова метода",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }
    }
}

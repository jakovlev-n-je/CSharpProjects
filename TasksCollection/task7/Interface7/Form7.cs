using FilmLibrary;
using Implementation7;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Interface7
{
    public partial class Form7 : Form
    {
        private readonly List<ClassInfo> Classes = new List<ClassInfo>();

        private readonly Cinema Cinema = new Cinema();

        private readonly string[] Films = {"Иван Васильевич меняет профессию", "Операция «Ы» и другие приключения Шурика", "Любовь и голуби",
                                           "Бриллиантовая рука", "Джентльмены удачи", "Кавказская пленница, или Новые приключения Шурика",
                                           "Девчата", "О чём говорят мужчины", "12 стульев", "Жмурки"};

        private readonly string[] Producers = {"Гайдай", "Меньшов", "Серый", "Чулюкин", "Дьяченко", "Балабанов", "Климов", "Рязанов",
                                               "Войтинский", "Зайцев"};

        public Form7()
        {
            InitializeComponent();
            listBox2.DrawItem += new DrawItemEventHandler(listBox2_DrawItem);
            textBox1.Text = Directory.GetCurrentDirectory() + @"\FilmLibrary.dll";
        }

        private void InitializeClasses()
        {
            foreach (Type type in Assembly.LoadFrom(textBox1.Text.Trim()).GetTypes())
            {
                if (!type.IsAbstract && type.GetInterface(typeof(IFilm).Name) != null)
                {
                    Classes.Add(new ClassInfo(type, type.GetConstructors(), type.GetMethods()));
                }
            }
        }

        private string[] GetClassNames()
        {
            string[] names = new string[Classes.Count];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = Classes[i].GetClassName();
            }
            return names;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeClasses();
                UpdateComboBox();
                ShowInformationBox("Классы были успешно загружены из библиотеки!");
            }
            catch
            {
                ShowErrorBox("Задан некорректный путь к библиотеке классов!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            textBox3.Text = Films[random.Next(0, 10)] + "; " +
                 random.Next(1950, 2050) + "; " +
                 Producers[random.Next(0, 10)] + "; " +
                 random.Next(10, 250) + "; " +
                 random.Next(30, 300);
            ShowInformationBox("Случайные параметры для конструктора были успешно заданы!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Classes[comboBox1.SelectedIndex].IsConstructor(listBox1.SelectedIndex))
                {
                    InvokeConstructor(comboBox1.SelectedIndex, listBox1.SelectedIndex);
                }
                else
                {
                    InvokeMethod(comboBox1.SelectedIndex, listBox1.SelectedIndex - Classes[comboBox1.SelectedIndex].
                        ClassConstructors.Length);
                }
            }
            catch
            {
                ShowErrorBox("Некорректные параметры!");
            }
        }

        private void InvokeConstructor(int classIndex, int constructorIndex)
        {
            NationalFilm film = (NationalFilm)Classes[classIndex].ClassConstructors[constructorIndex].
                Invoke(ParseParameters());
            if (Cinema.ContainsFilm(film.Title))
            {
                ShowErrorBox("Фильм с таким названием уже присутствует в списке!");
                return;
            }
            Cinema.AddFilmToCinema(film);
            UpdateListBox2();
            ShowInformationBox("Экземпляр класса был создан и успешно добавлен в список!");
        }

        private void InvokeMethod(int classIndex, int methodIndex)
        {
            int instanceIndex = listBox2.SelectedIndex;
            if (instanceIndex == -1)
            {
                ShowErrorBox("Не выбран экземпляр класса!");
                return;
            }
            if (Cinema.Films[instanceIndex].GetType() != Classes[classIndex].ClassType)
            {
                ShowErrorBox("Выбран неправильный экземпляр класса!");
                return;
            }
            string result = Classes[classIndex].ClassMethods[methodIndex].Invoke(Cinema.Films
                [instanceIndex], ParseParameters()).ToString();
            UpdateListBox2();
            ShowMethodResultBox(result);
        }

        private object[] ParseParameters()
        {
            string[] line = textBox3.Text.Trim().Split(';');
            if (line.Length == 1 && line[0].Trim() == "")
            {
                return new object[0];
            }
            object[] parameters = new object[line.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (int.TryParse(line[i], out int value))
                {
                    parameters[i] = value;
                }
                else
                {
                    parameters[i] = line[i].Trim();
                }
            }
            return parameters;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                textBox2.Text = "";
                button3.Enabled = false;
                return;
            }
            textBox2.Text = Classes[comboBox1.SelectedIndex].GetMethodParameters(listBox1.SelectedIndex);
            button3.Enabled = true;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListBox2();
            if (listBox2.SelectedIndex == -1)
            {
                return;
            }
            textBox4.Text = Cinema.Films[listBox2.SelectedIndex].GetInfo().Replace("\n", Environment.NewLine);
        }

        private void UpdateListBox2()
        {
            listBox2.DataSource = null;
            listBox2.DataSource = Cinema.Films;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = Classes[comboBox1.SelectedIndex].GetMethodNames();
        }

        private void UpdateComboBox()
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = GetClassNames();
            comboBox1.Enabled = true;
        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            int status = Cinema.Films[e.Index].RentalStatus;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.Orange, e.Bounds);
            }
            else
            {
                switch (status)
                {
                    case 0:
                        e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
                        break;
                    case 1:
                        e.Graphics.FillRectangle(Brushes.Green, e.Bounds);
                        break;
                    case 2:
                        e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
                        break;
                }

            }
            e.Graphics.DrawString(listBox2.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
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

        private void ShowErrorBox(string text)
        {
            MessageBox.Show(
                text,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
        }
    }
}

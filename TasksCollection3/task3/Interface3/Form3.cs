using Implementation3;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Interface3
{
    public partial class Form3 : Form
    {
        public List<NaturalPerson> NaturalPersons;

        public List<Revenue> CurrentRevenues;

        public Form3()
        {
            InitializeComponent();
            NaturalPersons = new List<NaturalPerson>();
            CurrentRevenues = new List<Revenue>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            if (IsEmpty(name) || !IsOnlyLetters(name))
            {
                ShowErrorBox("Имя физического лица указано некорректно!");
                return;
            }
            string surname = textBox2.Text.Trim();
            if (IsEmpty(surname) || !IsOnlyLetters(surname))
            {
                ShowErrorBox("Фамилия физического лица указана некорректно!");
                return;
            }
            if (comboBox1.SelectedIndex < 0)
            {
                ShowErrorBox("Количество детей физического лица не указано!");
                return;
            }
            NaturalPerson person = new NaturalPerson(name, surname, comboBox1.SelectedIndex, CurrentRevenues);
            ClearPersonFields();
            ClearListBox1();
            ClearRevenueFields();
            button3.Enabled = false;
            if (!HasPerson(person))
            {
                NaturalPersons.Add(person);
                ClearListBox2();
                listBox2.DataSource = NaturalPersons;
                CurrentRevenues = new List<Revenue>();
                ShowInformationBox("Новое физическое лицо было успешно добавленно в список!");
            }
            else
                ShowErrorBox("Данное физическое лицо уже имеется в списке!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0)
            {
                ShowErrorBox("Тип дохода не указан!");
                return;
            }
            Revenue revenue = new Revenue((RevenueType)comboBox2.SelectedIndex, Convert.ToInt32(numericUpDown1.Value));
            CurrentRevenues.Add(revenue);
            ClearListBox1();
            listBox1.DataSource = CurrentRevenues;
            ClearRevenueFields();
            ShowInformationBox("Новый доход был успешно добавлен в список!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CurrentRevenues.RemoveAt(listBox1.SelectedIndex);
            ClearListBox1();
            listBox1.DataSource = CurrentRevenues;
            ShowInformationBox("Выбранный доход был успешно удален из списка!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            NaturalPersons.RemoveAt(listBox2.SelectedIndex);
            ClearListBox2();
            listBox2.DataSource = NaturalPersons;
            ShowInformationBox("Выбранное физическое лицо было успешно удаленно из списка!");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0 && HasType(comboBox2.SelectedIndex))
            {
                comboBox2.SelectedIndex = -1;
                ShowErrorBox("Выбранный тип дохода уже был добавлен в список!");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = listBox1.SelectedIndex >= 0;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                textBox3.Text = NaturalPersons[listBox2.SelectedIndex].GetInformation().Replace("\n", Environment.NewLine);
                button4.Enabled = true;
            }
            else
                button4.Enabled = false;
        }

        private bool IsEmpty(string text)
        {
            return text == "";
        }

        private bool IsOnlyLetters(string text)
        {
            foreach (char symbol in text)
            {
                if (!char.IsLetter(symbol))
                    return false;
            }
            return true;
        }

        private bool HasPerson(NaturalPerson newPerson)
        {
            foreach (NaturalPerson person in NaturalPersons)
            {
                if (person.Equals(newPerson))
                    return true;
            }
            return false;
        }

        private bool HasType(int index)
        {
            foreach (Revenue revenue in CurrentRevenues)
            {
                if (index == (int)revenue.Type)
                    return true;
            }
            return false;
        }

        private void ClearPersonFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void ClearRevenueFields()
        {
            numericUpDown1.Value = 0;
            comboBox2.SelectedIndex = -1;
        }

        private void ClearListBox1()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
        }

        private void ClearListBox2()
        {
            listBox2.DataSource = null;
            listBox2.Items.Clear();
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

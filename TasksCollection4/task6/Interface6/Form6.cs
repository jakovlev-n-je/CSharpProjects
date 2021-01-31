using Implementation6;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Interface6
{
    public partial class Form6 : Form
    {
        public List<IProduction> Productions { get; private set; }

        public Form6()
        {
            InitializeComponent();
            Productions = new List<IProduction>();
        }

        private void Clear()
        {
            textBox1.Text = "";
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown3.Value = 1;
        }

        private string GenerateRandomName(int capacity = 10)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < capacity; i++)
            {
                if (Convert.ToBoolean(random.Next(0, 2)))
                {
                    builder.Append((char)random.Next(65, 91));
                }
                else
                {
                    builder.Append(char.ToLower((char)random.Next(65, 91)));
                }
            }
            return builder.ToString();
        }

        private bool IsCorrertnessName(string text)
        {
            if (text == "")
            {
                return false;
            }
            foreach (char symbol in text)
            {
                if (!char.IsLetter(symbol))
                {
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            if (!IsCorrertnessName(name))
            {
                ShowErrorBox("Некорректно указано название предприятия!");
                return;
            }
            int itemPrice = Convert.ToInt32(numericUpDown1.Value);
            int productivity = Convert.ToInt32(numericUpDown2.Value);
            int workersCount = Convert.ToInt32(numericUpDown3.Value);
            Productions.Add(new BrickFactory(name, itemPrice, productivity, workersCount));
            UpdateListBox1();
            Clear();
            ShowInformationBox($"Производство \"{name}\" было успешно добавлено в список!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = GenerateRandomName();
            Random random = new Random();
            numericUpDown1.Value = random.Next(10, 1001);
            numericUpDown2.Value = random.Next(1, 50);
            numericUpDown3.Value = random.Next(1, 1001);
            ShowInformationBox($"Случайные значения был успешно сгенерированы и установленны!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int revenue = Convert.ToInt32(numericUpDown4.Value);
            numericUpDown4.Value = 1;
            UpdateListBox1();
            ShowInformationBox(((BrickFactory)Productions[listBox1.SelectedIndex]).NeedToSellProducts(revenue));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int itemsCount = Convert.ToInt32(numericUpDown5.Value);
            numericUpDown5.Value = 1;
            UpdateListBox1();
            ShowInformationBox(((BrickFactory)Productions[listBox1.SelectedIndex]).NeedWorkersForItemsCount(Convert.ToInt32(itemsCount)));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string message = Productions[listBox1.SelectedIndex].Produce();
            UpdateListBox1();
            ShowInformationBox(message);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string message = ((BrickFactory)Productions[listBox1.SelectedIndex]).SellProducts();
            UpdateListBox1();
            ShowInformationBox(message);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Productions.RemoveAt(listBox1.SelectedIndex);
            UpdateListBox1();
            ShowInformationBox("Экземпляр класса был успешно удален из списка!");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                numericUpDown4.Enabled = true;
                button3.Enabled = true;
                numericUpDown5.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                textBox2.Text = Productions[listBox1.SelectedIndex].GetInformation().Replace("\n", Environment.NewLine);
            }
            else
            {
                numericUpDown4.Enabled = false;
                button3.Enabled = false;
                numericUpDown5.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                textBox2.Text = "Экземпляр класса не выбран";
            }
        }

        private void UpdateListBox1()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = Productions;
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
    }
}

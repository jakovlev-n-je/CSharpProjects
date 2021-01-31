using Implementation6;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Interface6
{
    public partial class Form6 : Form
    {
        private readonly Cinema Cinema = new Cinema();

        private readonly string[] Films = {"Иван Васильевич меняет профессию", "Операция «Ы» и другие приключения Шурика", "Любовь и голуби",
                                           "Бриллиантовая рука", "Джентльмены удачи", "Кавказская пленница, или Новые приключения Шурика",
                                           "Девчата", "О чём говорят мужчины", "12 стульев", "Жмурки"};

        private readonly string[] Producers = {"Гайдай", "Меньшов", "Серый", "Чулюкин", "Дьяченко", "Балабанов", "Климов", "Рязанов",
                                               "Войтинский", "Зайцев"};

        public Form6()
        {
            InitializeComponent();
            listBox1.DrawItem += new DrawItemEventHandler(listBox1_DrawItem);
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            Comedy comedy = (Comedy)Cinema.Films[e.Index];
            int status = comedy.RentalStatus;
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
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, Brushes.Black,
                e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            textBox2.Text = Films[random.Next(0, 10)];
            numericUpDown1.Value = random.Next(1950, 2050);
            textBox3.Text = Producers[random.Next(0, 10)];
            numericUpDown2.Value = random.Next(10, 250);
            numericUpDown3.Value = random.Next(50, 300);
            ShowInformationBox("Случайные значения были успешно заданы!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string title = textBox2.Text.Trim();
            if (title.Length == 0)
            {
                ShowErrorBox("Некорректное название фильма!");
                return;
            }
            if (Cinema.ContainsFilm(title))
            {
                ShowErrorBox("Фильм с таким названием уже присутствует в списке!");
                return;
            }
            string producer = textBox3.Text.Trim();
            bool isFit = true;
            for (int i = 0; i < producer.Length; i++)
            {
                if (char.IsDigit(producer[i]))
                {
                    isFit = false;
                    break;
                }
            }
            if (producer.Length == 0 || !isFit)
            {
                ShowErrorBox("Некорректная фамилия режиссера!");
                return;
            }
            Cinema.AddFilm(new Comedy(title, Convert.ToInt32(numericUpDown1.Value), producer,
                    Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value)));
            UpdateListBox1();
            UpdateComboBoxes();
            UpdateListBox2();
            ClearInput();
            ShowInformationBox($"Фильм '{title}' был создан и успешно добавлен в список!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string title = Cinema.Films[listBox1.SelectedIndex].Title;
            Cinema.RemoveFilm((Comedy)Cinema.Films[listBox1.SelectedIndex]);
            textBox1.Text = "";
            UpdateListBox1();
            UpdateComboBoxes();
            UpdateListBox2();
            ShowInformationBox($"Фильм '{title}' был успешно удален из списка!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = Cinema.AddToRental(listBox1.SelectedIndex);
            UpdateListBox1();
            ShowInformationBox(text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string text = Cinema.RemoveFromRental(listBox1.SelectedIndex);
            UpdateListBox1();
            ShowInformationBox(text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListBox1();
            if (listBox1.SelectedIndex != -1)
            {
                Comedy comedy = (Comedy)Cinema.Films[listBox1.SelectedIndex];
                textBox1.Text = comedy.GetInfo().Replace("\n", Environment.NewLine);
                button4.Enabled = comedy.RentalStatus == 0;
                button5.Enabled = comedy.RentalStatus == 1;
            }
            else
            {
                button4.Enabled = false;
                button5.Enabled = false;
            }
            button3.Enabled = listBox1.SelectedIndex != -1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                comboBox2.Enabled = false;
            }
            else
            {
                listBox2.DataSource = null;
                listBox2.Items.Clear();
            }
            comboBox1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                comboBox1.Enabled = false;
            }
            else
            {
                listBox2.DataSource = null;
                listBox2.Items.Clear();
            }
            comboBox2.Enabled = checkBox2.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedItem != null) && (checkBox1.Enabled == true))
            {
                listBox2.DataSource = null;
                UpdateListBox2();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null && checkBox2.Enabled == true)
            {
                listBox2.DataSource = null;
                UpdateListBox2();
            }
        }

        private void UpdateListBox1()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = Cinema.Films;
        }

        private void UpdateListBox2()
        {
            listBox2.DataSource = null;
            if (comboBox1.SelectedItem != null && checkBox1.Checked)
            {
                listBox2.DataSource = Cinema.FindFilmsByYear((int)comboBox1.SelectedItem);
            }
            else if (comboBox2.SelectedItem != null && checkBox2.Checked)
            {
                listBox2.DataSource = Cinema.FindFilmsByProducer((string)comboBox2.SelectedItem);
            }
            else
            {
                listBox2.Items.Clear();
            }
        }

        private void UpdateComboBoxes()
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = Cinema.Years;
            comboBox2.DataSource = null;
            comboBox2.DataSource = Cinema.Producers;

        }

        private void ClearInput()
        {
            textBox2.Text = "";
            numericUpDown1.Value = 2000;
            textBox3.Text = "";
            numericUpDown2.Value = 10;
            numericUpDown3.Value = 50;
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

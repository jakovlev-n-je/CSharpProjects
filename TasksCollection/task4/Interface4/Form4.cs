using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Implementation4;

namespace Interface4
{
    public partial class Form4 : Form
    {
        private List<object> Programmers { get; set; } = new List<object> { };

        private List<string> ProgrammersNames { get; set; } = new List<string> { };

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text.Trim();
            if (name != "" && !ProgrammersNames.Contains(name))
            {
                if (checkBox1.Checked)
                {
                    int pcount = Convert.ToInt32(numericUpDown1.Value);
                    int cpcount = Convert.ToInt32(numericUpDown3.Value);
                    if (cpcount > pcount)
                    {
                        MessageBox.Show(
                        "Неверно указаны данные!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        Programmers.Add(new Middle(name, pcount,
                        Convert.ToInt32(numericUpDown2.Value), cpcount));
                        ProgrammersNames.Insert(ProgrammersNames.Count, name);
                        listBox1.Items.Insert(ProgrammersNames.IndexOf(name), name);
                        textBox2.Text = "";

                        MessageBox.Show(
                        "Новый Middle - программист был успешно добавлен!",
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    Programmers.Add(new Programmer(name, Convert.ToInt32(numericUpDown1.Value),
                        Convert.ToInt32(numericUpDown2.Value)));
                    ProgrammersNames.Insert(ProgrammersNames.Count, name);
                    listBox1.Items.Insert(ProgrammersNames.IndexOf(name), name);
                    textBox2.Text = "";

                    MessageBox.Show(
                    "Новый программист был успешно добавлен!",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                }
                checkBox1.Checked = false;
            }
            else
            {
                MessageBox.Show(
                "Неверная фамилия программиста!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Programmers.RemoveAt(listBox1.SelectedIndex);
            ProgrammersNames.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.Remove(listBox1.SelectedItem);
            textBox1.Text = "";

            MessageBox.Show(
                "Программист был успешно удален!",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown3.Enabled = true;
            }
            else
            {
                numericUpDown3.Enabled = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                button2.Enabled = true;
                UpdateText();
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Space) &&
                !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void UpdateText()
        {
            textBox1.Text = "";
            string text;
            if (Programmers.ElementAt(listBox1.SelectedIndex) is Middle)
            {
                Middle programmer = (Middle)Programmers.ElementAt(listBox1.SelectedIndex);
                text = "Ранг: Middle - программист" + programmer.GetMiddleInfo();
            }
            else
            {
                Programmer programmer = (Programmer)Programmers.ElementAt(listBox1.SelectedIndex);
                text = "Ранг: отсутствует" + programmer.GetProgrammerInfo();
            }
            textBox1.Text = text.Replace("\n", Environment.NewLine);
        }
    }
}

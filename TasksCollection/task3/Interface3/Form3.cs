using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Implementation3;

namespace Interface3
{
    public partial class Form3 : Form
    {
        private List<TextFile> Files { get; set; } = new List<TextFile> { };

        private List<String> Filenames { get; set; } = new List<String> { };

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = textBox2.Text.Trim();
            if (filename != "" && !Filenames.Contains(filename))
            {
                TextFile file = new TextFile();
                file.Create("C://ExampleFolder//ExampleTextFiles//" + filename + ".txt", filename);
                Files.Add(file);
                Filenames.Insert(Filenames.Count, filename);
                listBox1.Items.Insert(Filenames.IndexOf(filename), filename + ".txt");
                textBox2.Text = "";

                MessageBox.Show(
                "Файл был успешно добавлен!",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show(
                "Неверное имя файла!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Files[listBox1.SelectedIndex].Remove();
            Files.RemoveAt(listBox1.SelectedIndex);
            Filenames.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.Remove(listBox1.SelectedItem);
            textBox1.Text = "";

            MessageBox.Show(
                "Файл был успешно удален!",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = textBox3.Text.Trim();
            if (filename != "" && !Filenames.Contains(filename))
            {
                Files[listBox1.SelectedIndex].Rename(filename);
                Files[listBox1.SelectedIndex].Directory.Path = "C://ExampleFolder//ExampleTextFiles//"
                    + filename + ".txt";
                Filenames[listBox1.SelectedIndex] = filename;
                listBox1.Items[listBox1.SelectedIndex] = filename + ".txt";

                MessageBox.Show(
                "Файл был успешно переименован!",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show(
                "Неверное имя файла!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Files[listBox1.SelectedIndex].Supplement(textBox4.Text);
            UpdateText();

            MessageBox.Show(
               "Текст к файлу был успешно добавлен!",
               "Сообщение",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            if (listBox1.SelectedIndex != -1)
            {
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                UpdateText();
            }
            else
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void UpdateText()
        {
            textBox1.Text = "";
            TextFile file = Files.ElementAt(listBox1.SelectedIndex);
            textBox1.Text += ("Имя файла: " + file.File.Name + ".txt" +
                "\nПуть к файлу: " + file.Directory.Path +
                "\nГлубина пути к файлу: " + file.Directory.Depth +
                "\nРазмер файла: " + file.File.Size +
                "\nСодержимое файла: " + file.Text).Replace("\n", Environment.NewLine);
        }
    }
}

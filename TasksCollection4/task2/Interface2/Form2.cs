using Implementation2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Interface2
{
    public partial class Form2 : Form
    {
        public Alphabet Alphabet { get; set; }

        public Form2()
        {
            InitializeComponent();
            Alphabet = new Alphabet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                InitializeOpenFileDialog1(openFileDialog1);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;
                    Stream fileStream = openFileDialog1.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        richTextBox1.Text = reader.ReadToEnd().Replace("\n", Environment.NewLine);
                    }
                    button1.Enabled = false;
                    button2.Enabled = true;
                    textBox2.Text = filePath;
                    ShowInformationBox("Выбранный файл успешно загружен!");
                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = false;
                    textBox2.Text = "Выберите файл";
                    ShowErrorBox("Файл не был выбран!");
                }
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            Alphabet.Clear();
            foreach (char symbol in richTextBox1.SelectedText)
            {
                Alphabet.Add(symbol);
            }
            StringBuilder builder = new StringBuilder("В выделенном участке текста отсутствуют следующие буквы:\n");
            List<Letter> missingLetters = Alphabet.GetMissingLetters();
            if (missingLetters.Count == 0)
            {
                textBox1.Text = "В выделенном участке текста нет отсутствующих букв";
                return;
            }
            foreach (Letter letter in missingLetters)
            {
                builder.Append($"{letter}\n");
            }
            textBox1.Text = builder.ToString().Replace("\n", Environment.NewLine);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            richTextBox1.Text = "";
            textBox1.Text = "Текст не выделен";
            textBox2.Text = "Выберите файл";
            ShowInformationBox("Данные были успешно очищены!");
        }

        private void InitializeOpenFileDialog1(OpenFileDialog openFileDialog1)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Текстовый документ|*.txt";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
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

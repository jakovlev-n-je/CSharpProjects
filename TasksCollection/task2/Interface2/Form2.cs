using System;
using System.IO;
using System.Windows.Forms;
using Implementation2;

namespace Interface2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox4.Text = new FileInfo(Directory.GetCurrentDirectory()).Directory.Parent.
                Parent.FullName + "\\task2\\Temp";
            openFileDialog1.InitialDirectory = textBox4.Text;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Текстовый файл|*.txt";
            openFileDialog1.RestoreDirectory = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = new FileInfo(openFileDialog1.FileName).Directory.FullName;
                using (StreamReader reader = new StreamReader(openFileDialog1.FileName))
                {
                    textBox1.Text = reader.ReadToEnd().Replace("\n", Environment.NewLine);
                }
                button1.Enabled = false;
                button3.Enabled = false;
                button2.Enabled = true;
                if (textBox1.Text.Trim() == "")
                {
                    button4.Enabled = true;
                }
                else
                {
                    button6.Enabled = true;
                }

                MessageBox.Show(
                "Выбранный файл был успешно загружен!",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show(
                "Файл не был выбран!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox4.Text = new FileInfo(Directory.GetCurrentDirectory()).Directory.Parent.Parent.FullName + "\\task2\\Temp";
            openFileDialog1.InitialDirectory = textBox4.Text;
            openFileDialog1.FileName = "";
            button2.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            button1.Enabled = true;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.Create(textBox4.Text + "\\NameT.txt").Dispose();
            button1.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;

            MessageBox.Show(
            "Новый файл был успешно создан!",
            "Сообщение",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(textBox4.Text + "\\NameT.txt");
            string line = StringUtils.Generate();
            writer.WriteLine(line);
            writer.Dispose();
            textBox1.Text = line.Replace("\n", Environment.NewLine);
            button2.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = true;

            MessageBox.Show(
            "Текстовый файл был успешно заполнен случайной строкой!",
            "Сообщение",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            File.Delete(textBox4.Text + "\\NameT.txt");
            textBox2.Text = "";
            File.Delete(textBox4.Text + "\\NameS.txt");
            textBox3.Text = "";
            File.Delete(textBox4.Text + "\\NameR.txt");
            button5.Enabled = false;
            button1.Enabled = true;
            button3.Enabled = true;

            MessageBox.Show(
            "Все рабочие текстовые файлы были успешно удалены!",
            "Сообщение",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StringUtils utils = new StringUtils(textBox4.Text);
            utils.Write(utils.Read());
            using (StreamReader reader = new StreamReader(textBox4.Text + "\\NameS.txt"))
            {
                textBox2.Text = reader.ReadToEnd().Replace("\n", Environment.NewLine);
            }
            using (StreamReader reader = new StreamReader(textBox4.Text + "\\NameR.txt"))
            {
                textBox3.Text = reader.ReadToEnd().Replace("\n", Environment.NewLine);
            }
            button2.Enabled = false;
            button6.Enabled = false;
            button5.Enabled = true;

            MessageBox.Show(
            "Содержимое исходного текстового файла было успешно разбито на несколько текстовых файлов!",
            "Сообщение",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }
    }
}

using Implementation;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class Form1 : Form
    {
        private readonly Trie _trie = new Trie();

        private readonly string _libraryPath = Directory.GetCurrentDirectory() + @"\WordsLibrary.txt";

        public Form1()
        {
            InitializeComponent();
            InitializeTrie();
        }

        private void InitializeTrie()
        {
            using (StreamReader reader = new StreamReader(_libraryPath))
            {
                string word = reader.ReadLine();
                int wordNumber = 1;
                while (word != null)
                {
                    word = word.Trim();
                    if (string.IsNullOrEmpty(word) || !IsOnlyLetters(word))
                        continue;
                    _trie.Add(word, wordNumber);
                    wordNumber++;
                    word = reader.ReadLine();
                }
            }
        }

        private bool IsOnlyLetters(string line)
        {
            foreach (char letter in line)
            {
                if (!char.IsLetter(letter))
                    return false;
            }
            return true;
        }

        private bool IsFewWords(string line)
        {
            foreach (char symbol in line)
            {
                if (symbol == 32)
                    return true;
            }
            return false;
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

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            string line = textBox1.Text.Trim();
            if (!IsOnlyLetters(line) || IsFewWords(line))
            {
                ShowErrorBox("Строка не может содержать несколько слов и некорректные символы!");
                textBox1.Text = "";
                textBox2.Text = "";
                return;
            }
            if (line != "")
            {
                textBox2.Text = "";
                StringBuilder builder = new StringBuilder();
                Word[] words = _trie.FindSimilarWords(line);
                Array.Sort(words);
                if (words.Length == 0)
                    builder.Append("Похожих слов не найдено");
                else
                {
                    foreach (Word word in words)
                    {
                        builder.Append($"{word}\n");
                    }
                }
                textBox2.Text = builder.ToString().Replace("\n", Environment.NewLine);
            }
            else
            {
                textBox2.Text = "Не задано слово";
            }
        }
    }
}

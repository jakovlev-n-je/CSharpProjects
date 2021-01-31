using System;

namespace Implementation3
{
    public class TextFile
    {
        public Directory Directory { get; set; }

        public File File { get; set; }

        public string Text { get; set; }

        public void Create(string path, string name)
        {
            Directory = new Directory(path);
            File = new File(name, 0);
            Text = "";
        }

        public void Print()
        {
            Console.WriteLine("Содержимое файла: \n" + Text);
        }

        public void Remove()
        {
            Directory = null;
            File = null;
            Text = null;
        }

        public void Rename(string name)
        {
            File.Name = name;
        }

        public void Supplement(string text)
        {
            Text += text;
            File.Size += text.Length;
        }
    }
}

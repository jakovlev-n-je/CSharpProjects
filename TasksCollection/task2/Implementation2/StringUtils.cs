using System;
using System.IO;

namespace Implementation2
{
    public class StringUtils
    {

        public string Root { get; set; }

        public StringUtils(string path)
        {
            Root = path;
        }

        public string[] Read()
        {
            string[] content = new string[2];
            StreamReader reader = new StreamReader(Root + "\\NameT.txt");
            for (int i = 0; i < 60; i++)
            {
                content[0] += (char)reader.Read();
            }
            content[1] = reader.ReadToEnd();
            reader.Dispose();
            return content;
        }

        public void Write(string[] content)
        {
            StreamWriter writer = new StreamWriter(Root + "\\NameS.txt");
            writer.WriteLine(content[0]);
            writer.Dispose();
            writer = new StreamWriter(Root + "\\NameR.txt");
            string[] numbers = content[1].Split(' ');
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = numbers[i].Trim();
                if (numbers[i].Length == 0)
                {
                    continue;
                }
                else
                {
                    writer.WriteLine(numbers[i]);
                }
            }
            writer.Dispose();
        }

        public static string Generate()
        {
            string numbers = "";
            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(10, 30); i++)
            {
                numbers += string.Format(" {0:f5}", rnd.NextDouble() * 30 - 15);
            }
            string text = "";
            for (int i = 0; i < 60; i++)
            {
                if (rnd.Next(0, 2) == 1)
                {
                    text += (char)rnd.Next(0x0041, 0x005A);
                }
                else
                {
                    text += (char)rnd.Next(0x0061, 0x007A);
                }
            }
            return text + numbers;
        }
    }
}

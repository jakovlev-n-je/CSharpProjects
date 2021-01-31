namespace Implementation3
{
    public class File
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}

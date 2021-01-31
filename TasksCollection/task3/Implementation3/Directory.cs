namespace Implementation3
{
    public class Directory
    {
        public int Depth { get; set; }

        public string Path { get; set; }

        public Directory(string path)
        {
            Path = path;
            Depth = CalculateDepth(path);
        }

        private int CalculateDepth(string path)
        {
            return path.Split('\\').Length + 1;
        }
    }
}

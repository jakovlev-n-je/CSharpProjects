namespace Implementation2
{
    public class Letter
    {
        public char Symbol { get; set; }

        public bool IsContained { get; set; }

        public Letter(char symbol, bool isContained)
        {
            Symbol = char.ToUpper(symbol);
            IsContained = isContained;
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}

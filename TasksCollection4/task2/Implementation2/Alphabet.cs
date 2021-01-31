using System.Collections.Generic;

namespace Implementation2
{
    public class Alphabet
    {
        private List<Letter> Letters { get; set; }

        public Alphabet()
        {
            InitializeAlphabet();
        }

        public void Add(char symbol)
        {
            int index = GetIndex(symbol);
            if (index != -1)
            {
                Letters[index].IsContained = true;
            }
        }

        public void Clear()
        {
            InitializeAlphabet();
        }

        public void Remove(char symbol)
        {
            int index = GetIndex(symbol);
            if (index != -1)
            {
                Letters[index].IsContained = false;
            }
        }

        public List<Letter> GetMissingLetters()
        {
            List<Letter> letters = new List<Letter>();
            foreach (Letter letter in Letters)
            {
                if (!letter.IsContained)
                {
                    letters.Add(letter);
                }
            }
            return letters;
        }

        private int GetIndex(char symbol)
        {
            symbol = char.ToUpper(symbol);
            return IsLetter(symbol) ? symbol - 65 : -1;
        }

        private void InitializeAlphabet()
        {
            Letters = new List<Letter>();
            for (int symbol = 65; symbol < 91; symbol++)
            {
                Letters.Add(new Letter((char)symbol, false));
            }
        }

        private bool IsLetter(char symbol)
        {
            return 64 < symbol && symbol < 91;
        }
    }
}

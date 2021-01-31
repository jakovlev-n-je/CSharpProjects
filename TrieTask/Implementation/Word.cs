using System;

namespace Implementation
{
    public class Word : IComparable
    {
        public string Key { get; set; }

        public int TyposCount { get; set; }

        public Word(string key, int typosCount)
        {
            Key = key;
            TyposCount = typosCount;
        }

        public int CompareTo(object obj)
        {
            return TyposCount < ((Word)obj).TyposCount ? TyposCount == ((Word)obj).TyposCount ? 0 : -1 : 1;
        }

        public override string ToString()
        {
            return $"Слово: {Key} | Количество опечаток: {TyposCount}";
        }
    }
}

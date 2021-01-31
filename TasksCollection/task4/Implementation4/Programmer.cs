namespace Implementation4
{
    public class Programmer
    {
        public int LanguagesCount { get; set; }

        public int ProgramsСount { get; set; }

        public string Surname { get; set; }

        public Programmer(string surname, int pcount, int lcount)
        {
            Surname = surname;
            ProgramsСount = pcount;
            LanguagesCount = lcount;
        }

        public string GetProgrammerInfo()
        {
            return "\nФамилия: " + Surname +
                "\nЧисло программ, написанных программистом: " + ProgramsСount +
                "\nЧисло языков программирования, которыми владеет программист: " + LanguagesCount +
                "\nКачество программиста: " + Calculate();
        }

        protected virtual int Calculate()
        {
            return ProgramsСount * LanguagesCount;
        }

        public int GetQuality()
        {
            return Calculate();
        }

        protected int GetProgramsCount()
        {
            return ProgramsСount;
        }
    }
}

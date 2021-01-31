namespace Implementation4
{
    public class Middle : Programmer
    {
        private int CorrectProgramsСount { get; set; }

        public Middle(string surname, int pcount, int lcount, int cpcount) :
            base(surname, pcount, lcount)
        {
            CorrectProgramsСount = cpcount;
        }

        public string GetMiddleInfo()
        {
            return GetProgrammerInfo() + "\nЧисло программ, написанных программистом правильно: " +
                CorrectProgramsСount;
        }

        protected override int Calculate()
        {
            return base.Calculate() * CorrectProgramsСount / GetProgramsCount();
        }
    }
}

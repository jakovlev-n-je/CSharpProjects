namespace Implementation6
{
    public abstract class NationalFilm : IFilm
    {
        public abstract string Title { get; protected set; }

        public abstract int Year { get; protected set; }

        public abstract string Producer { get; protected set; }

        public abstract int Budget { get; protected set; }

        public abstract int Duration { get; protected set; }

        public abstract int RentalStatus { get; protected set; }

        public string AddToRental()
        {
            if (RentalStatus != 0)
            {
                throw new System.Exception();
            }
            RentalStatus = 1;
            return $"Отечественный фильм '{Title}' был успешно выпущен в прокат!";
        }

        public string RemoveFromRental()
        {
            if (RentalStatus != 1)
            {
                throw new System.Exception();
            }
            RentalStatus = 2;
            CalculateStatistics();
            return $"Отечественный фильм '{Title}' был успешно снят с проката!";
        }

        protected abstract void CalculateStatistics();

        public abstract string GetBoxOfficeReceipts();

        public abstract string GetInfo();
    }
}

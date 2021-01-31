using System;
using System.Text;

namespace Implementation6
{
    public class Comedy : NationalFilm
    {
        public override string Title { get; protected set; }

        public override int Year { get; protected set; }

        public override string Producer { get; protected set; }

        public override int Budget { get; protected set; }

        public override int Duration { get; protected set; }

        public override int RentalStatus { get; protected set; }

        private double Rating { get; set; }

        private int Viewers { get; set; }

        public Comedy(string title, int year, string producer, int budget, int duration)
        {
            if (!IsCorrectParameters(title, year, producer, budget, duration))
            {
                throw new ArgumentException();
            }
            Title = title.Trim();
            Year = year;
            Producer = producer.Trim();
            Budget = budget;
            Duration = duration;
        }

        public override string GetBoxOfficeReceipts()
        {
            return RentalStatus == 2 ?
                $"Кассовые сборы комедии '{Title}' составили {CalculateBoxOfficeReceipts()} млн. руб.!" :
                $"Кассовые сборы комедии '{Title}' на данный момент невозможно подсчитать!";
        }

        public override string GetInfo()
        {
            StringBuilder info = new StringBuilder(
                $"Название: {Title}\r\n" +
                "Жанр: Комедия\r\n" +
                $"Год выпуска: {Year}\r\n" +
                $"Режиссер: {Producer}\r\n" +
                $"Бюджет фильма: {Budget} млн. руб.\r\n" +
                $"Длительность: {Duration} мин.\r\n");
            switch (RentalStatus)
            {
                case 0:
                    info.Insert(0, "Фильм не вышел в прокат\r\n\n");
                    break;
                case 1:
                    info.Insert(0, "Фильм находится в прокате\r\n\n");
                    break;
                case 2:
                    info.Insert(0, "Фильм был снят с проката\r\n\n");
                    info.Append(
                        $"Рейтинг фильма: {Rating} из 10.00\r\n" +
                        $"Зрители: {Viewers} тыс. чел.\r\n" +
                        $"Кассовые сборы: {CalculateBoxOfficeReceipts()} млн. руб.");
                    break;
            }
            return info.ToString();
        }

        public override string ToString()
        {
            return Title;
        }

        protected override void CalculateStatistics()
        {
            if (Rating == 0 && Viewers == 0)
            {
                CalculateRating();
                CalculateViewers();
            }
        }

        private int CalculateBoxOfficeReceipts()
        {
            return (int)(Viewers * Rating * 50) / 1000;
        }

        private void CalculateRating()
        {
            Rating = Budget * 0.04;
        }

        private void CalculateViewers()
        {
            Viewers = (int)(Rating * 250);
        }

        private bool IsCorrectParameters(string title, int year, string producer, int budget, int duration)
        {
            return IsDigitsOnly(title) &&
                Math.Abs(2000 - year) <= 50 &&
                IsDigitsOnly(producer) &&
                Math.Abs(130 - budget) <= 120 &&
                Math.Abs(175 - duration) <= 125;
        }

        private bool IsDigitsOnly(string line)
        {
            foreach (char symbol in line)
            {
                if (char.IsDigit(symbol))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

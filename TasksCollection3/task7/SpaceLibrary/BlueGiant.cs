using System;
using System.Text;

namespace Implementation7
{
    public class BlueGiant : Star
    {
        public double Density { get; protected set; }

        public override double Mass { get; protected set; }

        public override SpectralСlass SpectralСlass { get; protected set; }

        public double Volume { get; protected set; }

        public BlueGiant(string objectName, double mass, int temprature)
        {
            ObjectName = objectName;
            StudyStatus = false;
            Mass = mass;
            SpectralСlass = new SpectralСlass(temprature);
            Volume = CalculateVolume();
            Density = CalculateDensity();
        }

        public override string FinishLifeCycle()
        {
            string information = $"Звезда \"{ObjectName}\" успешно закончила свой жизненный цикл!";
            ObjectName = null;
            StudyStatus = false;
            Mass = 0;
            SpectralСlass = null;
            Volume = 0;
            Density = 0;
            return information;
        }

        public string GetPercentStars()
        {
            return $"В Наблюдаемой Вселенной насчитывают около 0,00003034 процентов звезд, принадлежащих к спектральному классу \"{SpectralСlass.ClassName}\"!";
        }

        public string GetVisibleColor()
        {
            return $"Истинный цвет звезд данного типа: \"{StarColorExtensions.ToString(SpectralСlass.Color)}\", в то время как видим мы: \"Голубой\"!";
        }

        public static BlueGiant CreateRandomBlueGiant(int nameSize)
        {
            Random random = new Random();
            return new BlueGiant(GetRandomName(nameSize, random), random.NextDouble() * 42 + 18, random.Next(30000, 60001));
        }

        private double CalculateDensity()
        {
            return Mass / Volume;
        }

        private double CalculateVolume()
        {
            return 4 / 3 * 3.14 * Math.Pow(SpectralСlass.Radius, 3);
        }

        private static string GetRandomName(int nameSize, Random random)
        {
            if (nameSize <= 0)
            {
                throw new ArgumentException();
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nameSize; i++)
            {
                bool isLetter = Convert.ToBoolean(random.Next(0, 2));
                builder.Append(isLetter ? GetRandomLetter(random).ToString() : GetRandomNumber(random).ToString());
            }
            return builder.ToString();
        }

        private static char GetRandomLetter(Random random)
        {
            bool isUpper = Convert.ToBoolean(random.Next(0, 2));
            return isUpper ? char.ToUpper((char)random.Next(97, 123)) : (char)random.Next(97, 123);
        }

        private static int GetRandomNumber(Random random)
        {
            return random.Next(0, 10);
        }
    }
}

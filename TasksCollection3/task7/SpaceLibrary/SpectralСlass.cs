using System;

namespace Implementation7
{
    public class SpectralСlass
    {
        public char ClassName { get; protected set; }

        public StarColor Color { get; protected set; }

        public double Luminosity { get; protected set; }

        public double Radius { get; protected set; }

        public int Temprature { get; }

        public SpectralСlass(int temprature)
        {
            Temprature = temprature;
            InitializeSpectralClass();
        }

        public override string ToString()
        {
            return $"Спектральный класс: {ClassName}" +
                   $"\nИстинный цвет: {StarColorExtensions.ToString(Color)}" +
                   $"\nСветимость: {Luminosity} Солн. св." +
                   $"\nРадиус: {Radius} Солн. рад." +
                   $"\nТемпература: {Temprature} К.";
        }

        private void InitializeSpectralClass()
        {
            Random random = new Random();
            if (BelongsToInterval(0, 3500, Temprature))
            {
                ClassName = 'M';
                Color = StarColor.Red;
                Luminosity = 0.04;
                Radius = random.NextDouble() * 0.3 + 0.1;
                return;
            }
            if (BelongsToInterval(3500, 5000, Temprature))
            {
                ClassName = 'K';
                Color = StarColor.Orange;
                Luminosity = 0.4;
                Radius = random.NextDouble() * 0.5 + 0.4;
                return;
            }
            if (BelongsToInterval(5000, 6000, Temprature))
            {
                ClassName = 'G';
                Color = StarColor.Yellow;
                Luminosity = 1.2;
                Radius = random.NextDouble() * 0.2 + 0.9;
                return;
            }
            if (BelongsToInterval(6000, 7500, Temprature))
            {
                ClassName = 'F';
                Color = StarColor.Yellow_White;
                Luminosity = 6;
                Radius = random.NextDouble() * 0.2 + 1.1;
                return;
            }
            if (BelongsToInterval(7500, 10000, Temprature))
            {
                ClassName = 'A';
                Color = StarColor.White;
                Luminosity = 80;
                Radius = random.NextDouble() * 0.8 + 1.3;
                return;
            }
            if (BelongsToInterval(10000, 30000, Temprature))
            {
                ClassName = 'B';
                Color = StarColor.White_Blue;
                Luminosity = 20000;
                Radius = random.NextDouble() * 4.9 + 2.1;
                return;
            }
            if (BelongsToInterval(30000, 60000, Temprature))
            {
                ClassName = 'O';
                Color = StarColor.Blue;
                Luminosity = 1400000;
                Radius = random.NextDouble() * 8 + 7;
                return;
            }
        }

        private bool BelongsToInterval(int min, int max, int value)
        {
            return min < value && value <= max;
        }
    }
}

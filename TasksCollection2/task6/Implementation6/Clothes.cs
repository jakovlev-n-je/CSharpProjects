using System;

namespace Implementation6
{
    public abstract class Clothes : IFabric
    {
        public abstract int DefectsCount { get; set; }

        public string FabricType { get; set; }

        public abstract int WearPercentage { get; set; }

        public abstract string Discard();

        public virtual string GetInforamtion()
        {
            return "Тип одежды: костюм" +
                   $"\nТип ткани: {FabricType}" +
                   $"\nКоличество дефектов: {DefectsCount}" +
                   $"\nПроцент изношенности: {WearPercentage}";
        }

        public string SewUp()
        {
            if (WearPercentage < 1)
            {
                return "Одежду незачем восстанавливать!";
            }
            DefectsCount--;
            WearPercentage -= 5;
            return "Одежда была успешно восстановленна";
        }

        public string WearOut()
        {
            if (WearPercentage > 99)
            {
                return "Одежда слишком сильно повреждена!";
            }
            DefectsCount++;
            WearPercentage += 5;
            return GetRandomReason();
        }

        private string GetRandomReason()
        {
            Random random = new Random();
            switch (random.Next(0, 3))
            {
                case 0:
                    return "Одежда случайно натерлась!";
                case 1:
                    return "Одежда невзначай  была порвана!";
                default:
                    return "Одежда была непредвиденно повреждена!";
            }
        }
    }
}

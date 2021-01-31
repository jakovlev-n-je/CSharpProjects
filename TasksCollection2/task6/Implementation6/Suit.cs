namespace Implementation6
{
    public class Suit : Clothes
    {
        public string Color { get; set; }

        public override int DefectsCount { get; set; }

        public int Price { get; set; }

        public override int WearPercentage { get; set; }

        public Suit(string fabricType, string color, int price)
        {
            color = color.Trim();
            Color = color.Substring(0, 1).ToUpper() +
                    color.Substring(1, color.Length - 1).ToLower();
            DefectsCount = 0;
            fabricType = fabricType.Trim();
            FabricType = fabricType.Substring(0, 1).ToUpper() +
                         fabricType.Substring(1, fabricType.Length - 1).ToLower();
            Price = price;
            WearPercentage = 0;
        }

        public override string Discard()
        {
            Color = null;
            DefectsCount = 0;
            FabricType = null;
            Price = 0;
            WearPercentage = 0;
            return $"Костюм был выброшен в мусорку!";
        }

        public override string GetInforamtion()
        {
            return "Тип одежды: костюм" +
                   $"\nЦвет: {Color}" +
                   $"\nТип ткани: {FabricType}" +
                   $"\nКоличество дефектов: {DefectsCount}" +
                   $"\nЦена: {Price} руб." +
                   $"\nПроцент изношенности: {WearPercentage}";
        }

        public string GiveAwayToTailor()
        {
            if (WearPercentage < 1)
            {
                return "Одежду незачем относить к портному!";
            }
            DefectsCount = 0;
            WearPercentage = 0;
            return $"{ToString()} был отдан портному на исправление и успешно восстановлен!";
        }

        public string Sell()
        {
            string name = ToString();
            int price = CalculatePrice();
            Discard();
            return $"{name} был успешно продан за {price} руб.!";
        }

        public override string ToString()
        {
            return $"{Color} костюм";
        }

        private int CalculatePrice()
        {
            return WearPercentage == 0 ? Price : Price * (100 - WearPercentage) / 100;
        }
    }
}

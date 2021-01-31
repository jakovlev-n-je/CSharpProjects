namespace Implementation3
{
    public class Revenue
    {
        public RevenueType Type { get; set; }

        public int RevenueAmount { get; set; }

        public Revenue(RevenueType type, int revenueAmount)
        {
            Type = type;
            RevenueAmount = revenueAmount;
        }

        public string GetInformation()
        {
            return $"\n\nТип дохода: {ToString()}" +
                   $"\nСумма дохода без вычета налога: {RevenueAmount}";
        }

        public override string ToString()
        {
            switch (Type)
            {
                case RevenueType.MainJob:
                    return "Основная работа";
                case RevenueType.AdditionalJob:
                    return "Дополнительная работа";
                case RevenueType.AuthorRemuneration:
                    return "Авторское вознаграждение";
                case RevenueType.PropertySale:
                    return "Продажа имущества";
                case RevenueType.GiftMoney:
                    return "Подаренная денежная сумма";
                case RevenueType.GiftProperty:
                    return "Подаренное имущество";
                case RevenueType.ForeignTransfer:
                    return "Перевод из-за границы";
                default:
                    return "Материальная помощь";
            }
        }
    }
}

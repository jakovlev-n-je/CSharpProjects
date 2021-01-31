namespace Implementation3
{
    public class Tax
    {
        public Revenue Revenue { get; set; }

        public int TaxAmount { get; set; }

        public Tax(Revenue revenue, int childCount)
        {
            Revenue = revenue;
            TaxAmount = CalculateTaxAmount(CalculatePersent(), childCount);
        }

        public string GetInformation()
        {
            return $"\nСумма налога: {TaxAmount}" +
                   $"\nСумма дохода с вычетом налога: {Revenue.RevenueAmount - TaxAmount}";
        }

        private int CalculatePersent()
        {
            switch (Revenue.Type)
            {
                case RevenueType.GiftMoney:
                    return 0;
                case RevenueType.ForeignTransfer:
                    return 0;
                case RevenueType.FinancialAssistance:
                    return Revenue.RevenueAmount <= 4000 ? 0 : 13;
                default:
                    return 13;
            }
        }

        private int CalculateTaxAmount(int percent, int childCount)
        {
            if (Revenue.Type == RevenueType.MainJob || Revenue.Type == RevenueType.AdditionalJob)
                return Revenue.RevenueAmount / 100 * percent - (childCount < 3 ? 182 * childCount : 182 * 2 + 390);
            else
                return Revenue.RevenueAmount / 100 * percent;
        }
    }
}

namespace Implementation6
{
    public class BrickFactory : Factory
    {
        public int ItemPrice { get; protected set; }

        public override int Productivity { get; protected set; }

        public int Revenue { get; protected set; }

        public override int WorkersCount { get; protected set; }

        public BrickFactory(string name, int itemPrice, int productivity, int workersCount)
        {
            Name = name;
            ItemsCount = 0;
            ItemPrice = itemPrice;
            Productivity = productivity;
            Revenue = 0;
            WorkersCount = workersCount;
        }

        public string NeedToSellProducts(int revenue)
        {
            return $"Для получения выручки в размере {revenue} руб., необходимо продать около {revenue / ItemPrice} ед. товара!";
        }

        public string NeedWorkersForItemsCount(int itemsCount)
        {
            return $"Для производства {itemsCount} ед. товара в день, необходимо иметь около {itemsCount / Productivity} " +
                   $"рабочих с производительностью около {Productivity} ед. товара в день!";
        }

        public override string SellProducts()
        {
            Revenue = ItemsCount * ItemPrice;
            string result = $"Было успешно продано {ItemsCount} ед. товара по цене {ItemPrice} руб. за ед. товара! Выручка составила: {Revenue}";
            ItemsCount = 0;
            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

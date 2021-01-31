namespace Implementation6
{
    public abstract class Factory : IProduction
    {
        public int ItemsCount { get; set; }

        public string Name { get; set; }

        public abstract int Productivity { get; protected set; }

        public abstract int WorkersCount { get; protected set; }

        public string GetInformation()
        {
            return $"Наименование производства: {Name}" +
                   "\nТип производства: Завод" +
                   $"\nЧисло рабочих: {WorkersCount}" +
                   $"\nПродуктивность одного рабочего: {Productivity}" +
                   $"\nТовара хранится на складе: {ItemsCount}";
        }

        public string Produce()
        {
            int itemsCount = 0;
            for (int i = 0; i < WorkersCount; i++)
            {
                if (ItemsCount + Productivity > 100000)
                {
                    return $"Склад переполнен! Товара на складе {ItemsCount}!";
                }
                ItemsCount += Productivity;
                itemsCount += Productivity;
            }
            return $"За день было успешно произведено {itemsCount} ед. товара!";
        }

        public abstract string SellProducts();
    }
}

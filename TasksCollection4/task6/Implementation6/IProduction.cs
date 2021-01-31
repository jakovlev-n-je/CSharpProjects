namespace Implementation6
{
    public interface IProduction
    {
        int ItemsCount { get; set; }

        string Name { get; set; }

        string GetInformation();

        string Produce();
    }
}

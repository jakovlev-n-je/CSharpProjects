namespace Implementation6
{
    public interface IFilm
    {
        string Title { get; }

        string AddToRental();

        string RemoveFromRental();
    }
}

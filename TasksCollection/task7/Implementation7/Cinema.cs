using FilmLibrary;
using System.Collections.Generic;

namespace Implementation7
{
    public class Cinema
    {
        public List<NationalFilm> Films { get; private set; }

        public Cinema()
        {
            Films = new List<NationalFilm>();
        }

        public void AddFilmToCinema(NationalFilm comedy)
        {
            Films.Add(comedy);
        }

        public bool ContainsFilm(string title)
        {
            foreach (NationalFilm film in Films)
            {
                if (film.Title == title)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

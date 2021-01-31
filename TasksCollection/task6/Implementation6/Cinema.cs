using System.Collections.Generic;

namespace Implementation6
{
    public class Cinema
    {
        public List<IFilm> Films { get; set; }

        public List<string> Producers { get; set; }

        public List<int> Years { get; set; }

        public Cinema()
        {
            Films = new List<IFilm>();
            Producers = new List<string>();
            Years = new List<int>();
        }

        public void AddFilm(Comedy comedy)
        {
            Films.Add(comedy);
            if (!Producers.Contains(comedy.Producer))
            {
                Producers.Add(comedy.Producer);
            }
            if (!Years.Contains(comedy.Year))
            {
                Years.Add(comedy.Year);
            }
        }

        public void RemoveFilm(Comedy comedy)
        {
            if (CountContainsYear(comedy.Year) == 1)
            {
                Years.Remove(comedy.Year);
            }
            if (CountContainsProducer(comedy.Producer) == 1)
            {
                Producers.Remove(comedy.Producer);
            }
            Films.Remove(comedy);

        }

        public string AddToRental(int index)
        {
            return Films[index].AddToRental();
        }

        public string RemoveFromRental(int index)
        {
            return Films[index].RemoveFromRental();
        }

        public List<IFilm> FindFilmsByProducer(string producer)
        {
            List<IFilm> films = new List<IFilm>();
            foreach (Comedy comedy in Films)
            {
                if (comedy.Producer == producer)
                {
                    films.Add(comedy);
                }
            }
            return films;
        }

        public int CountContainsProducer(string producer)
        {
            int count = 0;
            foreach (Comedy comedy in Films)
            {
                if (comedy.Producer == producer)
                {
                    count++;
                }
            }
            return count;
        }

        public List<IFilm> FindFilmsByYear(int year)
        {
            List<IFilm> films = new List<IFilm>();
            foreach (Comedy comedy in Films)
            {
                if (comedy.Year == year)
                {
                    films.Add(comedy);
                }
            }
            return films;
        }

        public int CountContainsYear(int year)
        {
            int count = 0;
            foreach (Comedy comedy in Films)
            {
                if (comedy.Year == year)
                {
                    count++;
                }
            }
            return count;
        }

        public bool ContainsFilm(string title)
        {
            foreach (Comedy comedy in Films)
            {
                if (comedy.Title == title)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

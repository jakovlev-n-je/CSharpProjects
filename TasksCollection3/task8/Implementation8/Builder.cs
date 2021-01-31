using System.Threading;

namespace Implementation8
{
    public class Builder
    {
        public Coordinates BaseCoordinates { get; set; }

        public House House { get; set; }

        public Coordinates NextCoordinates { get; set; }

        public Builder(Coordinates baseCoordinates, Coordinates nextCoordinates, House house)
        {
            BaseCoordinates = baseCoordinates;
            NextCoordinates = nextCoordinates;
            House = house;
        }

        public bool InHouse()
        {
            return NextCoordinates.IsLocatedIn(BaseCoordinates);
        }

        public void MoveTo(Coordinates coordinates)
        {
            Thread.Sleep(100);
            if (!NextCoordinates.IsLocatedIn(coordinates))
            {
                NextCoordinates.ShiftTo(coordinates);
            }
        }

        public void Run()
        {
            while (true)
            {
                if (!House.NeedMaterials)
                {
                    MoveTo(BaseCoordinates);

                }
                else
                {
                    Coordinates coordinates = new Coordinates(House.Coordinates.X, House.Coordinates.Y + 190, House.Coordinates.Step);
                    while (!NextCoordinates.IsLocatedIn(coordinates))
                    {
                        MoveTo(coordinates);
                    }
                    UnloadMaterials();
                }
            }
        }

        public void UnloadMaterials()
        {
            Thread.Sleep(2000);
            House.BuildingMaterials = House.Random.Next(0, 3);
            House.NeedMaterials = false;
        }
    }
}

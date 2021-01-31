using System.Collections.Generic;
using System.Threading;

namespace Implementation8
{
    public class ProfiledSheet : IConstructionEquipment
    {
        public Coordinates BaseCoordinates { get; set; }

        public Coordinates NextCoordinates { get; set; }

        public List<House> Houses { get; set; }

        public ProfiledSheet(Coordinates baseCoordinates, Coordinates nextCoordinates)
        {
            BaseCoordinates = baseCoordinates;
            NextCoordinates = nextCoordinates;
            Houses = new List<House>();
        }

        public void InstallRoof(House house)
        {
            Thread.Sleep(1500);
            house.RoofType = typeof(ProfiledSheet);
            house.NeedRoof = false;
            Houses.Remove(house);
        }

        public void MoveTo(Coordinates coordinates)
        {
            Thread.Sleep(100);
            if (!NextCoordinates.IsLocatedIn(coordinates))
            {
                NextCoordinates.ShiftTo(coordinates);
            }
        }

        public void NeedToBringRoof(House house)
        {
            Houses.Add(house);
        }

        public bool CarInWarehouse()
        {
            return NextCoordinates.IsLocatedIn(BaseCoordinates);
        }

        public void Run()
        {
            while (true)
            {
                if (Houses.Count == 0)
                {
                    MoveTo(BaseCoordinates);
                }
                else
                {
                    House house = Houses[0];
                    Coordinates coordinates = new Coordinates(house.Coordinates.X, house.Coordinates.Y + 162, house.Coordinates.Step);
                    while (!NextCoordinates.IsLocatedIn(coordinates))
                    {
                        MoveTo(coordinates);
                    }
                    InstallRoof(house);
                }
            }
        }
    }
}

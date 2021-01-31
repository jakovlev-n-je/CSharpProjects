using System.Collections.Generic;
using System.Threading;

namespace Implementation8
{
    public class CorrugatedSheet : IConstructionMachinery
    {
        public Coordinates BaseCoordinates { get; set; }

        public Coordinates NextCoordinates { get; set; }

        public List<House> Houses { get; set; }

        public CorrugatedSheet(Coordinates baseCoordinates, Coordinates nextCoordinates)
        {
            BaseCoordinates = baseCoordinates;
            NextCoordinates = nextCoordinates;
            Houses = new List<House>();
        }

        public void InstallRoof(House house)
        {
            Thread.Sleep(1500);
            house.RoofType = typeof(CorrugatedSheet);
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

        public bool TruckInWarehouse()
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
                    Coordinates coordinates = new Coordinates(house.Coordinates.X, house.Coordinates.Y + 210, house.Coordinates.Step);
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

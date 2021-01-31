using System.Collections.Generic;
using System.Threading;

namespace Implementation9
{
    public class ForkliftSlow : IForklift
    {
        public Coordinates BaseCoordinates { get; set; }

        public Coordinates NextCoordinates { get; set; }

        public List<OilDerrick> OilDerricks { get; set; }

        public ForkliftSlow(Coordinates baseCoordinates, Coordinates nextCoordinates)
        {
            BaseCoordinates = baseCoordinates;
            NextCoordinates = nextCoordinates;
            OilDerricks = new List<OilDerrick>();
        }

        public bool InWarehouse()
        {
            return NextCoordinates.IsLocatedIn(BaseCoordinates);
        }

        public void MoveTo(Coordinates coordinates)
        {
            while (!NextCoordinates.IsLocatedIn(coordinates))
            {
                Thread.Sleep(100);
                NextCoordinates.ShiftTo(coordinates);
            }
        }

        public void NeedToUnload(OilDerrick oilDerrick)
        {
            OilDerricks.Add(oilDerrick);
        }

        public void Run()
        {
            while (true)
            {
                if (OilDerricks.Count == 0)
                {
                    MoveTo(BaseCoordinates);
                }
                else
                {
                    OilDerrick oilDerrick = OilDerricks[0];
                    Coordinates coordinates = new Coordinates(oilDerrick.Coordinates.X, oilDerrick.Coordinates.Y + 118, oilDerrick.Coordinates.Step);
                    while (!NextCoordinates.IsLocatedIn(coordinates))
                    {
                        MoveTo(coordinates);
                    }
                    Unload(oilDerrick);
                    MoveTo(BaseCoordinates);
                }
            }
        }

        public void Unload(OilDerrick oilDerrick)
        {
            Thread.Sleep(1000);
            oilDerrick.Workload = 0;
            oilDerrick.NeedLoader = false;
            OilDerricks.Remove(oilDerrick);
        }
    }
}

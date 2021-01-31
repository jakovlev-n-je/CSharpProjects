using System.Threading;

namespace Implementation9
{
    public class Mechanic
    {
        public Coordinates BaseCoordinates { get; set; }

        public Coordinates NextCoordinates { get; set; }

        public Mechanic(Coordinates baseCoordinates, Coordinates nextCoordinates)
        {
            BaseCoordinates = baseCoordinates;
            NextCoordinates = nextCoordinates;
        }

        public void Extinguish(OilDerrick oilDerrick, Thread thread)
        {
            lock (this)
            {
                Coordinates coordinates = new Coordinates(oilDerrick.Coordinates.X, oilDerrick.Coordinates.Y - 25, oilDerrick.Coordinates.Step);
                while (!NextCoordinates.IsLocatedIn(coordinates))
                {
                    MoveTo(coordinates);
                }
                if (oilDerrick.IsBroke)
                {
                    MoveToHome();
                    return;
                }
                thread.Abort();
                Thread.Sleep(1000);
                oilDerrick.NeedMechanic = false;
                oilDerrick.Status = 1;
                MoveToHome();
            }
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

        public void MoveToHome()
        {
            while (!NextCoordinates.IsLocatedIn(BaseCoordinates))
            {
                MoveTo(BaseCoordinates);
            }
        }
    }
}

using System.Threading;

namespace Implementation8
{
    public class ForkLoader
    {
        public Coordinates BaseCoordinates { get; set; }

        public Factory Factory { get; set; }

        public Coordinates NextCoordinates { get; set; }

        public ForkLoader(Coordinates baseCoordinates, Coordinates nextCoordinates, Factory factory)
        {
            BaseCoordinates = baseCoordinates;
            NextCoordinates = nextCoordinates;
            Factory = factory;
        }

        public bool InWarehouse()
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
                if (!Factory.NeedSugar)
                {
                    MoveTo(BaseCoordinates);
                }
                else
                {
                    Coordinates coordinates = new Coordinates(Factory.Coordinates.X, Factory.Coordinates.Y + 170, Factory.Coordinates.Step);
                    while (!NextCoordinates.IsLocatedIn(coordinates))
                    {
                        MoveTo(coordinates);
                    }
                    UnloadSugar();
                }
            }
        }

        public void UnloadSugar()
        {
            Thread.Sleep(3000);
            Factory.SugarQuantity = Factory.Random.Next(100, 251);
            Factory.NeedSugar = false;
        }
    }
}

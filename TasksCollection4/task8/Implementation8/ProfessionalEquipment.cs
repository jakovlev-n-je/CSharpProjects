using System.Collections.Generic;
using System.Threading;

namespace Implementation8
{
    public class ProfessionalEquipment : IEquipment
    {
        public Coordinates BaseCoordinates { get; set; }

        public List<Factory> Factories { get; set; }

        public int ImageId { get; set; }

        public Coordinates NextCoordinates { get; set; }

        public ProfessionalEquipment(Coordinates baseCoordinates, Coordinates nextCoordinates)
        {
            BaseCoordinates = baseCoordinates;
            NextCoordinates = nextCoordinates;
            Factories = new List<Factory>();
            ImageId = 0;
        }

        public bool TruckInWarehouse()
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

        public void NeedToBringEquipment(Factory factory)
        {
            Factories.Add(factory);
        }

        public void Run()
        {
            while (true)
            {
                if (Factories.Count == 0)
                {
                    ImageId = 0;
                    MoveTo(BaseCoordinates);
                }
                else
                {
                    ImageId = 1;
                    Factory factory = Factories[0];
                    Coordinates coordinates = new Coordinates(factory.Coordinates.X, factory.Coordinates.Y + 170, factory.Coordinates.Step);
                    while (!NextCoordinates.IsLocatedIn(coordinates))
                    {
                        MoveTo(coordinates);
                    }
                    UnloadEquipment(factory);
                }
            }
        }

        public void UnloadEquipment(Factory factory)
        {
            Thread.Sleep(1000);
            factory.EquipmenType = typeof(ProfessionalEquipment);
            factory.NeedNewEquipment = false;
            Factories.Remove(factory);
        }
    }
}

using System;
using System.Threading;

namespace Implementation8
{
    public class House
    {
        public event Emulator.SendMaterials BuildingMaterialsEnded;

        public event Emulator.CoveRoof NeedCoverRoof;

        public Coordinates Coordinates { get; set; }

        public int BuildingMaterials { get; set; }

        public int ImageId { get; set; }

        public bool NeedMaterials { get; set; }

        public bool NeedRoof { get; set; }

        public Random Random { get; set; }

        public Type RoofType { get; set; }

        public House(Coordinates coordinates, Type roofType, Random random)
        {
            Coordinates = coordinates;
            RoofType = roofType;
            Random = random;
            BuildingMaterials = Random.Next(0, 3);
            ImageId = 1;
            NeedRoof = false;
            NeedMaterials = false;
        }

        public void Run()
        {
            while (true)
            {
                if (ImageId == 7)
                {
                    return;
                }
                if (!NeedRoof && !NeedMaterials)
                {
                    Upgrade();
                    if (ImageId == 3)
                    {
                        NeedRoof = true;
                        NeedCoverRoof?.Invoke(this);
                    }
                }
            }
        }

        private void Upgrade()
        {
            Thread.Sleep(3000);
            ImageId += 1;
            BuildingMaterials -= 1;
            if (BuildingMaterials == 0)
            {
                NeedMaterials = true;
                BuildingMaterialsEnded?.Invoke();
            }
        }
    }
}

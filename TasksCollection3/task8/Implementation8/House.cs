using System;
using System.Threading;

namespace Implementation8
{
    public class House
    {
        public event ProcessEmulator.SendMaterials BuildingMaterialsEnded;

        public event ProcessEmulator.CoveRoof NeedCoverRoof;

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
                if (ImageId == 5)
                {
                    return;
                }
                if (!NeedRoof && !NeedMaterials)
                {
                    Upgrade();
                    if (ImageId == 2)
                    {
                        NeedRoof = true;
                        NeedCoverRoof?.Invoke(this);
                    }
                }
            }
        }

        private void Upgrade()
        {
            Thread.Sleep(2000);
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

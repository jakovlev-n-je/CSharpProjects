using System.Collections.Generic;
using System.Threading;

namespace Implementation8
{
    public class ProcessEmulator
    {
        public delegate void SendMaterials();

        public delegate void CoveRoof(House house);

        public IConstructionEquipment ConstructionEquipment { get; set; }

        public List<Builder> Builders { get; set; }

        public List<Thread> Threads { get; set; }

        public ProcessEmulator(IConstructionEquipment constructionMachinery, List<Builder> builders)
        {
            ConstructionEquipment = constructionMachinery;
            Builders = builders;
            Threads = new List<Thread>();
        }

        public void Abort()
        {
            foreach (Thread thread in Threads)
            {
                thread.Abort();
            }
        }

        public void Run()
        {
            for (int i = 0; i < Builders.Count; i++)
            {
                Builders[i].House.NeedCoverRoof += ConstructionEquipment.NeedToBringRoof;
                Builders[i].House.BuildingMaterialsEnded += Builders[i].UnloadMaterials;
                Thread builderThread = new Thread(Builders[i].Run);
                builderThread.Start();
                Threads.Add(builderThread);
                Thread houseThread = new Thread(Builders[i].House.Run);
                houseThread.Start();
                Threads.Add(houseThread);
            }
            Thread constuctionEquipmentThread = new Thread(ConstructionEquipment.Run);
            constuctionEquipmentThread.Start();
            Threads.Add(constuctionEquipmentThread);
        }
    }
}

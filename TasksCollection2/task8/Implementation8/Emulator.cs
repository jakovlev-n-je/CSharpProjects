using System.Collections.Generic;
using System.Threading;

namespace Implementation8
{
    public class Emulator
    {
        public delegate void SendMaterials();

        public delegate void CoveRoof(House house);

        public IConstructionMachinery ConstructionMachinery { get; set; }

        public List<Mason> Masons { get; set; }

        public List<Thread> Threads { get; set; }

        public Emulator(IConstructionMachinery constructionMachinery, List<Mason> masons)
        {
            ConstructionMachinery = constructionMachinery;
            Masons = masons;
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
            for (int i = 0; i < Masons.Count; i++)
            {
                Masons[i].House.NeedCoverRoof += ConstructionMachinery.NeedToBringRoof;
                Masons[i].House.BuildingMaterialsEnded += Masons[i].UnloadMaterials;
                Thread masonThread = new Thread(Masons[i].Run);
                masonThread.Start();
                Threads.Add(masonThread);
                Thread houseThread = new Thread(Masons[i].House.Run);
                houseThread.Start();
                Threads.Add(houseThread);
            }
            Thread constuctionMachineryThread = new Thread(ConstructionMachinery.Run);
            constuctionMachineryThread.Start();
            Threads.Add(constuctionMachineryThread);
        }
    }
}

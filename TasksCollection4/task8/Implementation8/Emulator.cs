using System.Collections.Generic;
using System.Threading;

namespace Implementation8
{
    public class Emulator
    {
        public delegate void SendSugarBoxes();

        public delegate void SendNewEquipment(Factory factory);

        public IEquipment Truck { get; set; }

        public List<ForkLoader> ForkLoaders { get; set; }

        public List<Thread> Threads { get; set; }

        public Emulator(IEquipment equipment, List<ForkLoader> forkLoaders)
        {
            Truck = equipment;
            ForkLoaders = forkLoaders;
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
            for (int i = 0; i < ForkLoaders.Count; i++)
            {
                ForkLoaders[i].Factory.EquipmentBrokeDown += Truck.NeedToBringEquipment;
                ForkLoaders[i].Factory.SugarRanOut += ForkLoaders[i].UnloadSugar;
                Thread forkLoaderThread = new Thread(ForkLoaders[i].Run);
                forkLoaderThread.Start();
                Threads.Add(forkLoaderThread);
                Thread factoryThread = new Thread(ForkLoaders[i].Factory.Run);
                factoryThread.Start();
                Threads.Add(factoryThread);
            }
            Thread equipmentThread = new Thread(Truck.Run);
            equipmentThread.Start();
            Threads.Add(equipmentThread);
        }
    }
}

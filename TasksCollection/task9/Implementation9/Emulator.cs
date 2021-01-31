using System.Collections.Generic;
using System.Threading;

namespace Implementation9
{
    public class Emulator
    {
        public delegate void LightUp(OilDerrick oilDerrick, Thread thread);

        public delegate void SendForklift(OilDerrick oilDerrick);

        public IForklift Forklift { get; set; }

        public Mechanic Mechanic { get; set; }

        public List<OilDerrick> OilDerricks { get; set; }

        public List<Thread> Threads { get; set; }

        public Emulator(IForklift forklift, Mechanic mechanic, List<OilDerrick> oilDerricks)
        {
            Forklift = forklift;
            Mechanic = mechanic;
            OilDerricks = oilDerricks;
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
            for (int i = 0; i < OilDerricks.Count; i++)
            {
                OilDerricks[i].OilShipping += Forklift.NeedToUnload;
                OilDerricks[i].OilDerrickCaughtFire += Mechanic.Extinguish;
                Thread oilDerrickThread = new Thread(OilDerricks[i].Run);
                oilDerrickThread.Start();
                Threads.Add(oilDerrickThread);
            }
            Thread forkLiftThread = new Thread(Forklift.Run);
            forkLiftThread.Start();
            Threads.Add(forkLiftThread);
        }
    }
}

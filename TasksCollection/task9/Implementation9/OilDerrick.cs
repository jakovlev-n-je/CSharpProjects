using System;
using System.Threading;
using System.Threading.Tasks;

namespace Implementation9
{
    public class OilDerrick
    {
        public event Emulator.LightUp OilDerrickCaughtFire;

        public event Emulator.SendForklift OilShipping;

        public int Capacity { get; set; }

        public Coordinates Coordinates { get; set; }

        public int FireChance { get; set; }

        public bool IsBroke { get; set; }

        public bool NeedLoader { get; set; }

        public bool NeedMechanic { get; set; }

        public Random Random { get; set; }

        public int Status { get; set; }

        public int Workload { get; set; }

        public OilDerrick(Coordinates coordinates, int fireChance, Random random)
        {
            Coordinates = coordinates;
            FireChance = fireChance;
            Random = random;
            Capacity = random.Next(10000, 50001);
            IsBroke = false;
            NeedLoader = false;
            NeedMechanic = false;
            Status = 1;
            Workload = 0;
        }

        public void Run()
        {
            if (IsBroke)
            {
                return;
            }
            while (true)
            {
                PumpOil();
                if (!NeedMechanic && !NeedLoader)
                {
                    NeedMechanic = IsFire();
                    if (NeedMechanic)
                    {
                        Status = 3;
                        Thread currentThread = Thread.CurrentThread;
                        Thread mechanicThread = new Thread(async () =>
                        {
                            await Task.Delay(5000);
                            if (NeedMechanic)
                            {
                                IsBroke = true;
                                Status = 4;
                                Thread.Sleep(200);
                                Status = 5;
                                currentThread.Abort();
                            }
                        });
                        mechanicThread.Start();
                        OilDerrickCaughtFire?.Invoke(this, mechanicThread);
                    }
                    else
                    {
                        Thread.Sleep(100);
                        Status = 2;
                        Thread.Sleep(100);
                        Status = 1;
                    }
                }
            }
        }


        private bool IsFire()
        {
            return Random.Next(0, 101) < FireChance;
        }

        private void PumpOil()
        {
            if (NeedMechanic || NeedLoader)
            {
                return;
            }
            Workload += Random.Next(100, 1000);
            if (Workload > Capacity)
            {
                NeedLoader = true;
                OilShipping?.Invoke(this);
            }
        }
    }
}

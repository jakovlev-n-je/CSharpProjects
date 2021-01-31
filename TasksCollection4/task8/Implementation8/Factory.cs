using System;
using System.Threading;

namespace Implementation8
{
    public class Factory
    {
        public event Emulator.SendSugarBoxes SugarRanOut;

        public event Emulator.SendNewEquipment EquipmentBrokeDown;

        public int BreakdownChance { get; set; }

        public int CandySugar { get; set; }

        public Coordinates Coordinates { get; set; }

        public Type EquipmenType { get; set; }

        public int ImageId { get; set; }

        public bool NeedNewEquipment { get; set; }

        public bool NeedSugar { get; set; }

        public Random Random { get; set; }

        public int SugarQuantity { get; set; }

        public Factory(Coordinates coordinates, int breakdownChance, Type equipmentType, Random random)
        {
            Coordinates = coordinates;
            BreakdownChance = breakdownChance;
            EquipmenType = equipmentType;
            Random = random;
            CandySugar = random.Next(10, 15);
            ImageId = 0;
            NeedNewEquipment = false;
            NeedSugar = false;
            SugarQuantity = random.Next(100, 250);
        }

        public void Run()
        {
            while (true)
            {
                if (!NeedNewEquipment && !NeedSugar)
                {
                    MakeCandy();
                    if (NeedSugar)
                    {
                        continue;
                    }
                    NeedNewEquipment = IsBrokenEquipment();
                    if (NeedNewEquipment)
                    {
                        ImageId = 0;
                        EquipmentBrokeDown?.Invoke(this);
                    }
                }
            }
        }

        private bool IsBrokenEquipment()
        {
            return Random.Next(0, 101) < BreakdownChance;
        }

        private void MakeCandy()
        {
            int interval = EquipmenType == typeof(ProfessionalEquipment) ? 300 :
                                   EquipmenType == typeof(AmateurEquipment) ? 600 : 900;
            Thread.Sleep(interval);
            ImageId = 1;
            Thread.Sleep(interval);
            ImageId = 2;
            Thread.Sleep(interval);
            ImageId = 0;
            SugarQuantity -= CandySugar;
            if (SugarQuantity <= 0)
            {
                NeedSugar = true;
                SugarRanOut?.Invoke();
            }
        }
    }
}

﻿namespace Implementation8
{
    public interface IConstructionEquipment
    {
        Coordinates BaseCoordinates { get; set; }

        Coordinates NextCoordinates { get; set; }

        void InstallRoof(House house);

        void MoveTo(Coordinates coordinates);

        void NeedToBringRoof(House house);

        void Run();

        bool CarInWarehouse();
    }
}

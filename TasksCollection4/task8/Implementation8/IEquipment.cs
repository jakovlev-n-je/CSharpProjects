namespace Implementation8
{
    public interface IEquipment
    {
        Coordinates BaseCoordinates { get; set; }

        int ImageId { get; set; }

        Coordinates NextCoordinates { get; set; }

        bool TruckInWarehouse();

        void MoveTo(Coordinates coordinates);

        void NeedToBringEquipment(Factory factory);

        void Run();

        void UnloadEquipment(Factory factory);
    }
}

namespace Implementation9
{
    public interface IForklift
    {
        Coordinates BaseCoordinates { get; set; }

        Coordinates NextCoordinates { get; set; }

        bool InWarehouse();

        void MoveTo(Coordinates coordinates);

        void NeedToUnload(OilDerrick oilDerrick);

        void Run();

        void Unload(OilDerrick oilDerrick);
    }
}

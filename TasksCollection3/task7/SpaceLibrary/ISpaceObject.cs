namespace Implementation7
{
    public interface ISpaceObject
    {
        string ObjectName { get; }

        bool StudyStatus { get; }

        string ExploreObject();

        string GetObjectInformation();
    }
}

namespace Implementation7
{
    public abstract class Star : ISpaceObject
    {
        public abstract double Mass { get; protected set; }

        public string ObjectName { get; protected set; }

        public abstract SpectralСlass SpectralСlass { get; protected set; }

        public bool StudyStatus { get; protected set; }

        public override string ToString()
        {
            return ObjectName;
        }

        public string ExploreObject()
        {
            if (!StudyStatus)
            {
                StudyStatus = true;
                return $"Объект \"{ObjectName}\" был успешно изучен!";
            }
            return $"Объект \"{ObjectName}\" не требует исследования!";
        }

        public abstract string FinishLifeCycle();

        public string GetObjectInformation()
        {
            return StudyStatus ? $"Наименование: \"{ObjectName}\"" +
                                 $"\nТип: \"Звезда\"" +
                                 $"\nМасса: {Mass} Солн. мас.\n" +
                                 SpectralСlass :
                                 $"Объект \"{ObjectName}\" не изучен!";
        }

        protected bool BelongsToInterval(double min, double max, double value)
        {
            return min < value && value <= max;
        }

        protected bool CheckNameCorrectness(string value)
        {
            if (value == null)
            {
                return true;
            }
            foreach (char symbol in value)
            {
                if (!char.IsLetterOrDigit(symbol))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

namespace Implementation7
{
    public enum StarColor
    {
        Red,
        Orange,
        Yellow,
        Yellow_White,
        White,
        White_Blue,
        Blue
    }

    public static class StarColorExtensions
    {
        public static string ToString(this StarColor color)
        {
            switch (color)
            {
                case StarColor.Red:
                    return "Красный";
                case StarColor.Orange:
                    return "Оранжевый";
                case StarColor.Yellow:
                    return "Желтый";
                case StarColor.Yellow_White:
                    return "Желто-белый";
                case StarColor.White:
                    return "Белый";
                case StarColor.White_Blue:
                    return "Бело-голубой";
                default:
                    return "Голубой";
            }
        }
    }
}

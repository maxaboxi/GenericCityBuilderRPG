using GenericCityBuilderRPG.Enums;

namespace GenericLooterShooterRPG.General
{
    public static class PlayerResources
    {
        public static int Wood { get; private set; }
        public static int Copper { get; private set; }
        public static int Rock { get; private set; }
        public static int Sand { get; private set; }
        public static int Gold { get; private set; }
        public static int Silver { get; private set; }
        public static int Coal { get; private set; }
        public static int Diamond { get; private set; }
        public static int Water { get; private set; }

        public static void AddResource(ResourceType type, int amount)
        {
            switch (type)
            {
                case ResourceType.Wood:
                    Wood += amount;
                    break;
                case ResourceType.Copper:
                    Copper += amount;
                    break;
                case ResourceType.Rock:
                    Rock += amount;
                    break;
                case ResourceType.Sand:
                    Sand += amount;
                    break;
                case ResourceType.Gold:
                    Gold += amount;
                    break;
                case ResourceType.Silver:
                    Silver += amount;
                    break;
                case ResourceType.Coal:
                    Coal += amount;
                    break;
                case ResourceType.Diamond:
                    Diamond += amount;
                    break;
                case ResourceType.Water:
                    Water += amount;
                    break;
            }
        }
    }
}

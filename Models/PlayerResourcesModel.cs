using GenericCityBuilderRPG.Enums;

namespace GenericCityBuilderRPG.Models
{
    public class PlayerResourcesModel
    {
        public int Wood { get; private set; }
        public int Copper { get; private set; }
        public int Rock { get; private set; }
        public int Sand { get; private set; }
        public int Gold { get; private set; }
        public int Silver { get; private set; }
        public int Coal { get; private set; }
        public int Diamond { get; private set; }
        public int Water { get; private set; }

        public void AddResource(ResourceType type, int amount)
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

using GenericCityBuilderRPG.Enums;
using GenericLooterShooterRPG.Enums;
using GenericLooterShooterRPG.Models;
using System.Collections.Generic;

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
        public int Food { get; private set; }
        public int Population { get; private set; }
        public int PopulationLimit { get; private set; } = 50;
        public List<ResourceCostModel> ResourceCosts { get; private set; } = new List<ResourceCostModel>();

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
                case ResourceType.Food:
                    Food += amount;
                    break;
                case ResourceType.Population:
                    Population += amount;
                    break;
            }
        }

        public bool HasEnoughResources(List<ResourceCostModel> cost)
        {
            bool hasEnough = false;
            foreach(var c in cost)
            {
                switch (c.Type)
                {
                    case ResourceType.Wood:
                        hasEnough = Wood - c.Amount >= 0;
                        break;
                    case ResourceType.Copper:
                        hasEnough = Copper - c.Amount >= 0;
                        break;
                    case ResourceType.Rock:
                        hasEnough = Rock - c.Amount >= 0;
                        break;
                    case ResourceType.Sand:
                        hasEnough = Sand - c.Amount >= 0;
                        break;
                    case ResourceType.Gold:
                        hasEnough = Gold - c.Amount >= 0;
                        break;
                    case ResourceType.Silver:
                        hasEnough = Silver - c.Amount >= 0;
                        break;
                    case ResourceType.Coal:
                        hasEnough = Coal - c.Amount >= 0;
                        break;
                    case ResourceType.Diamond:
                        hasEnough = Diamond - c.Amount >= 0;
                        break;
                    case ResourceType.Water:
                        hasEnough = Water - c.Amount >= 0;
                        break;
                    case ResourceType.Food:
                        hasEnough = Food - c.Amount >= 0;
                        break;
                }

                if (!hasEnough)
                {
                    return hasEnough;
                }
            }

            return hasEnough;
        }

        public void AddResourceCost(ResourceCostModel resourceCost)
        {
            var found = false;
            foreach (var c in ResourceCosts)
            {
                if (c.Type == resourceCost.Type)
                {
                    c.Amount += 1;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                ResourceCosts.Add(resourceCost);
            }
            
        }

        public void SubtractResources(ResourceType type)
        {
            foreach (var c in ResourceCosts)
            {
                if (c.Type == type)
                {
                    AddResource(c.Type, -c.Amount);

                    if (c.Type == ResourceType.Food && Food < 0)
                    {
                        AddResource(ResourceType.Population, -c.Amount / 2 < 1 ? 1 : c.Amount / 2);
                    }
                }
            }
        }
    }
}

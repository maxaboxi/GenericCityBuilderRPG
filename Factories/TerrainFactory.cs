using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Models;
using GenericLooterShooterRPG.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GenericCityBuilderRPG.Factories
{
    static class TerrainFactory
    {
        private static readonly List<TerrainTileModel> _tiles = new List<TerrainTileModel>();
        public static IEnumerable<TerrainTileModel> GenerateTiles()
        {
            for (int y = 0; y < (int)MapSize.Height; y += (int)TerrainTileModelSize.Height)
            {
                for (int x = 0; x < (int)MapSize.Width; x += (int)TerrainTileModelSize.Width)
                {
                    var nx = x / (int)MapSize.Width - 0.5;
                    var ny = y / (int)MapSize.Height - 0.5;
                    var e = (1.00 * GenerateNoise1(1 * nx, 1 * ny)
                           + 0.50 * GenerateNoise1(2 * nx, 2 * ny)
                           + 0.25 * GenerateNoise1(4 * nx, 4 * ny)
                           + 0.13 * GenerateNoise1(8 * nx, 8 * ny)
                           + 0.06 * GenerateNoise1(16 * nx, 16 * ny)
                           + 0.03 * GenerateNoise1(32 * nx, 32 * ny));
                    e /= (1.00 + 0.50 + 0.25 + 0.13 + 0.06 + 0.03);
                    e = Math.Pow(e, 5.00);
                    var m = (1.00 * GenerateNoise2(1 * nx, 1 * ny)
                           + 0.75 * GenerateNoise2(2 * nx, 2 * ny)
                           + 0.33 * GenerateNoise2(4 * nx, 4 * ny)
                           + 0.33 * GenerateNoise2(8 * nx, 8 * ny)
                           + 0.33 * GenerateNoise2(16 * nx, 16 * ny)
                           + 0.50 * GenerateNoise2(32 * nx, 32 * ny));
                    m /= (1.00 + 0.75 + 0.33 + 0.33 + 0.33 + 0.50);
                    CreateTile(new Vector2(x, y), e, m);
                }
            }

            return _tiles;
        }

        public static IEnumerable<ResourceModel> GenerateResources(IEnumerable<TerrainTileModel> tiles)
        {
            List<ResourceModel> resources = new List<ResourceModel>();
            foreach (var tile in tiles)
            {
                double noise = GenerateNoise1(tile.Position.X, tile.Position.Y);

                var resource = GetResources(tile, noise);
                if (resource != null)
                {
                    resources.Add(resource);
                }
            }

            return resources;
        }

        private static double GenerateNoise1(double nx, double ny)
        {
            var seed = (long)Guid.NewGuid().GetHashCode();
            var noise = new OpenSimplexNoise(seed);
            var e = 1 * noise.Evaluate(1 * nx, 1 * ny) + 0.5 * noise.Evaluate(2 * nx, 2 * ny) + 0.25 * noise.Evaluate(4 * nx, 4 * ny);
            return e / 2 + 0.5;
        }
        private static double GenerateNoise2(double nx, double ny)
        {
            var seed = (long)Guid.NewGuid().GetHashCode();
            var noise = new OpenSimplexNoise(seed);
            var e = 1 * noise.Evaluate(1 * nx, 1 * ny) + 0.5 * noise.Evaluate(2 * nx, 2 * ny) + 0.25 * noise.Evaluate(4 * nx, 4 * ny);
            return e / 2 + 0.5;
        }

        private static void CreateTile(Vector2 position, double elevation, double moisture)
        {
            BiomeType type = GetType(elevation, moisture);
            int frame = GetFrame(type);
            _tiles.Add(new TerrainTileModel(frame, (int)TerrainTileModelSize.Width, (int)TerrainTileModelSize.Height, type, position, new Vector2(1, 1)));
        }

        private static int GetFrame(BiomeType type)
        {
            var gravelList = new List<int>
            {
                1,5
            };
            var rockList = new List<int>
            {
                2,3,6,7,8
            };
            var random = new Random();
            switch (type)
            {
                case BiomeType.Water:
                    return 0;
                case BiomeType.Dirt:
                    return 9;
                case BiomeType.Rainforest:
                    return 17;
                case BiomeType.Grassland:
                    return 14;
                case BiomeType.Taiga:
                    return 11;
                case BiomeType.Bare:
                    return 4;
                case BiomeType.Gravel:
                    var gravelFrame = random.Next(gravelList.Count);
                    return gravelList[gravelFrame];
                case BiomeType.Rock:
                    var rockFrame = random.Next(rockList.Count);
                    return rockList[rockFrame];
                case BiomeType.Sand:
                    return 0;
                default:
                    return 14;
            }
        }

        private static BiomeType GetType(double elevation, double moisture)
        {
            if (elevation < 0.003)
            {
                return BiomeType.Water;
            }

            if (elevation < 0.005)
            {
                return BiomeType.Sand;
            }
            
            if (elevation > 0.5)
            {
                if (moisture < 0.1)
                {
                    return BiomeType.Rock;
                }

                if (moisture < 0.2)
                {
                    return BiomeType.Gravel;
                }

                return BiomeType.Bare;
            }

            if (elevation > 0.4)
            {
                if (moisture < 0.33)
                {
                    return BiomeType.Sand;
                }

                if (moisture < 0.66)
                {
                    return BiomeType.Bare;
                }

                return BiomeType.Taiga;
            }


            if (elevation > 0.1)
            {
                if (moisture < 0.16)
                {
                    return BiomeType.Sand;
                }

                if (moisture < 0.5)
                {
                    return BiomeType.Dirt;
                }

                if (moisture < 0.83)
                {
                    return BiomeType.Grassland;
                }

                return BiomeType.Rainforest;
            }

            if (moisture < 0.1)
            {
                return BiomeType.Rock;
            }

            if (moisture < 0.13)
            {
                return BiomeType.Gravel;
            }

            if (moisture < 0.16)
            {
                return BiomeType.Sand;
            }

            if (moisture < 0.33)
            {
                return BiomeType.Dirt;
            }

            if (moisture < 0.66)
            {
                return BiomeType.Grassland;
            }

            return BiomeType.Grassland;
        }

        private static ResourceModel GetResources(TerrainTileModel tile, double noise)
        {
            Random random = new Random();

            if (tile.Type == BiomeType.Water)
            {
                return new ResourceModel(random.Next(2, 9), ResourceType.Water, random.Next(1, 500), tile.Position, tile.Area);
            }

            ResourceModel mineralResource;
            mineralResource = AddMineral(tile, noise);
            if (mineralResource != null)
            {
                return mineralResource;
            }

            switch (tile.Type)
            {
                case BiomeType.Grassland:
                case BiomeType.Rainforest:
                    if (random.Next(100) <= 30)
                    {
                        return new ResourceModel(random.Next(2, 9), ResourceType.Wood, random.Next(1, 50), tile.Position, tile.Area);
                    }
                    return new ResourceModel(0, ResourceType.Rock, random.Next(1, 60), tile.Position, tile.Area);
                case BiomeType.Taiga:
                    if (random.Next(100) <= 30)
                    {
                        return new ResourceModel(random.Next(0, 1), ResourceType.Wood, random.Next(1, 50), tile.Position, tile.Area);
                    }
                    return new ResourceModel(0, ResourceType.Rock, random.Next(1, 60), tile.Position, tile.Area);
                case BiomeType.Rock:
                    return new ResourceModel(0, ResourceType.Rock, random.Next(1, 60), tile.Position, tile.Area);
                case BiomeType.Sand:
                    return new ResourceModel(0, ResourceType.Sand, random.Next(1, 30), tile.Position, tile.Area);
                case BiomeType.Gravel:
                case BiomeType.Bare:
                    if (random.Next(100) <= 5)
                    {
                        return new ResourceModel(random.Next(2,3), ResourceType.Wood, random.Next(1, 50), tile.Position, tile.Area);
                    }
                    return new ResourceModel(0, ResourceType.Rock, random.Next(1, 60), tile.Position, tile.Area);
                case BiomeType.Dirt:
                    if (random.Next(100) <= 20)
                    {
                        return new ResourceModel(random.Next(2,3), ResourceType.Wood, random.Next(1, 50), tile.Position, tile.Area);
                    }
                    return new ResourceModel(0, ResourceType.Rock, random.Next(1, 60), tile.Position, tile.Area);
            }

            return null;
        }

        private static ResourceModel AddMineral(TerrainTileModel tile, double noise)
        {
            var random = new Random();

            if (noise < 0.05)
            {
                return new ResourceModel(2, ResourceType.Diamond, random.Next(1, 20), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            if (noise < 0.1)
            {
                return new ResourceModel(3, ResourceType.Gold, random.Next(1, 30), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            if (noise < 0.15)
            {
                return new ResourceModel(6, ResourceType.Silver, random.Next(1, 40), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            if (noise < 0.2)
            {
                return new ResourceModel(0, ResourceType.Coal, random.Next(1, 50), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            if (noise < 0.25)
            {
                return new ResourceModel(1, ResourceType.Copper, random.Next(1, 40), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            return null;
        }
    }
}

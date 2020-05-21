using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Models;
using GenericLooterShooterRPG.Enums;
using Microsoft.Xna.Framework;
using System;

namespace GenericCityBuilderRPG.Factories
{
    static class TerrainFactory
    {
        public static readonly TerrainTileModel[,] Tiles = new TerrainTileModel[(int)MapSize.Width, (int)MapSize.Height];
        public static readonly ResourceModel[,] Resources = new ResourceModel[(int)MapSize.Width, (int)MapSize.Height];

        public static void GenerateGrid()
        {
            for (int x = 0; x < (int)MapSize.Width; x++)
            {
                for (int y = 0; y < (int)MapSize.Height; y++)
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
                    CreateTile(x, y, e, m);
                }
            }
        }

        public static void GenerateResourceGrid()
        {
            var random = new Random();
            var randomizedTiles = Tiles;
            int lengthRow = randomizedTiles.GetLength(1);
            for (int i = randomizedTiles.Length - 1; i > 0; i--)
            {
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;
                int j = random.Next(i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;
                TerrainTileModel temp = randomizedTiles[i0, i1];
                randomizedTiles[i0, i1] = randomizedTiles[j0, j1];
                randomizedTiles[j0, j1] = temp;
            }

            for (int x = 0; x < randomizedTiles.GetLength(0); x++)
            {
                for (int y = 0; y < randomizedTiles.GetLength(1); y++)
                {
                    var tile = randomizedTiles[x, y];
                    Resources[x, y] = GetResources(tile);
                }
            }
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

        private static void CreateTile(int x, int y, double elevation, double moisture)
        {
            BiomeType type = GetType(elevation, moisture);
            int frame = GetFrame(type);
            var tile = new TerrainTileModel(frame, (int)TerrainTileModelSize.Width, (int)TerrainTileModelSize.Height, type, new Vector2((int)TerrainTileModelSize.Width * x, (int)TerrainTileModelSize.Height * y), new Vector2(1, 1));
            Tiles[x, y] = tile;
        }

        private static int GetFrame(BiomeType type)
        {
            switch (type)
            {
                case BiomeType.Water:
                    return 0;
                case BiomeType.Dirt:
                    return 4;
                case BiomeType.Rainforest:
                    return 7;
                case BiomeType.Grassland:
                    return 6;
                case BiomeType.Taiga:
                    return 5;
                case BiomeType.Bare:
                    return 3;
                case BiomeType.Gravel:
                    return 1;
                case BiomeType.Rock:
                    return 2;
                case BiomeType.Sand:
                    return 0;
                default:
                    return 6;
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

        private static ResourceModel GetResources(TerrainTileModel tile)
        {
            double noise = GenerateNoise1(tile.Position.X, tile.Position.Y);
            Random random = new Random();

            if (tile.Type == BiomeType.Water)
            {
                return new ResourceModel(0, ResourceType.Water, random.Next(1000, 2000), tile.Position, tile.Area);
            }

            ResourceModel mineralResource;
            mineralResource = AddMineral(tile, noise, random);
            if (mineralResource != null)
            {
                return mineralResource;
            }

            switch (tile.Type)
            {
                case BiomeType.Grassland:
                    if (random.Next(100) <= 30)
                    {
                        return AddWood(tile, random, 2, 9);
                    }
                    return AddRock(tile, random);
                case BiomeType.Rainforest:
                    if (random.Next(100) <= 30)
                    {
                        return AddWood(tile, random, 6, 7);
                    }
                    return AddRock(tile, random);
                case BiomeType.Taiga:
                    if (random.Next(100) <= 30)
                    {
                        return AddWood(tile, random, 0, 1);
                    }
                    return AddRock(tile, random);
                case BiomeType.Rock:
                    return AddRock(tile, random);
                case BiomeType.Sand:
                    return new ResourceModel(0, ResourceType.Sand, random.Next(200, 600), tile.Position, tile.Area);
                case BiomeType.Gravel:
                case BiomeType.Bare:
                    if (random.Next(100) <= 5)
                    {
                        return AddWood(tile, random, 2, 3);
                    }
                    return AddRock(tile, random);
                case BiomeType.Dirt:
                    if (random.Next(100) <= 20)
                    {
                        return AddWood(tile, random, 2, 3);
                    }
                    return AddRock(tile, random);
                    
            }

            return null;
        }

        private static ResourceModel AddMineral(TerrainTileModel tile, double noise, Random random)
        {
            if (noise < 0.05)
            {
                return new ResourceModel(2, ResourceType.Diamond, random.Next(1, 100), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2 - 15, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            if (noise < 0.1)
            {
                return new ResourceModel(3, ResourceType.Gold, random.Next(1, 200), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2 - 15, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            if (noise < 0.15)
            {
                return new ResourceModel(6, ResourceType.Silver, random.Next(1, 200), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2 - 15, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            if (noise < 0.2)
            {
                return new ResourceModel(0, ResourceType.Coal, random.Next(1, 300), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2 - 15, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            if (noise < 0.25)
            {
                return new ResourceModel(1, ResourceType.Copper, random.Next(1, 300), new Vector2(tile.Area.Center.X - (int)ResourceSize.MineralWidth / 2 - 15, tile.Area.Center.Y - (int)ResourceSize.MineralHeight / 2), tile.Area);
            }
            return null;
        }

        private static ResourceModel AddWood(TerrainTileModel tile, Random random, int minFrame, int maxFrame)
        {
            return new ResourceModel(random.Next(minFrame, maxFrame), ResourceType.Wood, random.Next(10, 150), new Vector2(tile.Area.Center.X - (int)ResourceSize.TreeWidth / 2, tile.Area.Center.Y - (int)ResourceSize.TreeHeight / 2), tile.Area);
        }

        private static ResourceModel AddRock(TerrainTileModel tile, Random random)
        {
            return new ResourceModel(0, ResourceType.Rock, random.Next(500, 2500), tile.Position, tile.Area);
        }
    }
}

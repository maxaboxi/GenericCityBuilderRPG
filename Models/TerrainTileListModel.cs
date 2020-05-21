using System.Collections.Generic;

namespace GenericCityBuilderRPG.Models
{
    class TerrainTileListModel
    {
        public TerrainTileModel[,] Tiles { get; }
        public ResourceModel[,] Resources { get; }

        public TerrainTileListModel(TerrainTileModel[,] tiles, ResourceModel[,] resources)
        {
            Tiles = tiles;
            Resources = resources;
        }
    }
}

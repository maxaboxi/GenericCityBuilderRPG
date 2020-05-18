using System;
using System.Collections.Generic;

namespace GenericCityBuilderRPG.Models
{
    class TerrainTileListModel
    {
        public List<TerrainTileModel> Tiles { get; } = new List<TerrainTileModel>();
        public List<ResourceModel> Resources { get; } = new List<ResourceModel>();

        public TerrainTileListModel(IEnumerable<TerrainTileModel> tiles, IEnumerable<ResourceModel> resources)
        {
            Tiles.AddRange(tiles);
            Resources.AddRange(resources);
        }

        internal object Where()
        {
            throw new NotImplementedException();
        }
    }
}

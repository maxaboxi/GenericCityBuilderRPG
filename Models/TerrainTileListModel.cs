using System;
using System.Collections.Generic;

namespace GenericCityBuilderRPG.Models
{
    class TerrainTileListModel
    {
        public List<TerrainTileModel> Tiles { get; } = new List<TerrainTileModel>();
        public List<Resource> Resources { get; } = new List<Resource>();

        public TerrainTileListModel(IEnumerable<TerrainTileModel> tiles, IEnumerable<Resource> resources)
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

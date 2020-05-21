using GenericCityBuilderRPG.Enums;
using GenericCityBuilderRPG.General;
using GenericCityBuilderRPG.Models;
using GenericLooterShooterRPG.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GenericCityBuilderRPG.Views
{
    class BackgroundView : BaseView
    {
        private readonly SpriteSheet _terrain;
        private readonly SpriteSheet _water;
        private readonly SpriteSheet _tree;
        private readonly SpriteSheet _minerals;
        private readonly TerrainTileListModel _tileList;
        private readonly PlayerModel _playerModel;
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteFont _gameFont;
        public BackgroundView(ContentManager contentManager, SpriteBatch spriteBatch, TerrainTileListModel tileList, PlayerModel playerModel) : base(contentManager, spriteBatch)
        {
            var terrainTextures = contentManager.Load<Texture2D>("terrain");
            var treeTextures = contentManager.Load<Texture2D>("trees");
            var waterTexture = contentManager.Load<Texture2D>("water");
            var mineralTextures = contentManager.Load<Texture2D>("minerals");
            _terrain = new SpriteSheet(spriteBatch, terrainTextures, (int)TerrainTileModelSize.Width, (int)TerrainTileModelSize.Height);
            _water = new SpriteSheet(spriteBatch, waterTexture, (int)TerrainTileModelSize.Width, (int)TerrainTileModelSize.Height);
            _tree = new SpriteSheet(spriteBatch, treeTextures, (int)ResourceSize.TreeWidth, (int)ResourceSize.TreeHeight);
            _minerals = new SpriteSheet(spriteBatch, mineralTextures, (int)ResourceSize.MineralWidth, (int)ResourceSize.MineralHeight);
            _tileList = tileList;
            _playerModel = playerModel;
            _spriteBatch = spriteBatch;
            _gameFont = contentManager.Load<SpriteFont>("FontFile");
        }

        public override void Draw()
        {
            var visibleArea = VirtualScreenSize.CalculateVisibleArea(_playerModel.Position);
            visibleArea.Width += 20;
            visibleArea.Height += 20;

            // Draw map and resources
            for (var x = 0; x < _tileList.Tiles.GetLength(0); x++)
            {
                for (var y = 0; y < _tileList.Tiles.GetLength(1); y++)
                {
                    var tile = _tileList.Tiles[x, y];
                    if (tile.Area.Intersects(visibleArea))
                    {
                        if (tile.Type == BiomeType.Water)
                        {
                            _water.Draw(tile.Position, tile.Frame, Color.White, tile.Scale);
                        }
                        else
                        {
                            _terrain.Draw(tile.Position, tile.Frame, Color.White, tile.Scale);
                        }

                        var resource = _tileList.Resources[x, y];
                        if (resource.Amount <= 0)
                        {
                            continue;
                        }

                        if (resource.Type == ResourceType.Rock || resource.Type == ResourceType.Sand || resource.Type == ResourceType.Water)
                        {
                            if (resource.AmountVisible)
                            {
                                DrawResourceAmount(resource);
                            }
                            continue;
                            
                        }

                        if (resource.Type == ResourceType.Wood)
                        {
                            _tree.Draw(resource.Position, resource.Frame, Color.White);
                        }
                        else
                        {
                            _minerals.Draw(resource.Position, resource.Frame, resource.Amount > 0 ? Color.White : Color.Red);
                        }

                        if (resource.AmountVisible)
                        {
                            DrawResourceAmount(resource);
                        }

                    }
                }
            }
        }

        private void DrawResourceAmount(ResourceModel resource)
        {
            if (resource.Type == ResourceType.Rock || resource.Type == ResourceType.Sand || resource.Type == ResourceType.Water)
            {
                _spriteBatch.DrawString(_gameFont, resource.Type.ToString() + ": " + resource.Amount.ToString(), new Vector2(resource.Position.X + (int)TerrainTileModelSize.Width / 2 - 85, resource.Position.Y + (int)TerrainTileModelSize.Height / 2 - 30), Color.White);
            } 
            else
            {
                _spriteBatch.DrawString(_gameFont, resource.Type.ToString() + " " + resource.Amount.ToString(), new Vector2(resource.Position.X - 15, resource.Position.Y - 30), Color.White);
            }
            
        }
    }
}

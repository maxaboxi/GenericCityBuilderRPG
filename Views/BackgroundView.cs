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

        // Uncomment for debugging
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

            // Uncomment for debugging
            _spriteBatch = spriteBatch;
            _gameFont = contentManager.Load<SpriteFont>("FontFile");
        }

        public override void Draw()
        {
            var visibleArea = VirtualScreenSize.CalculateVisibleArea(_playerModel.Position);
            visibleArea.Width += 20;
            visibleArea.Height += 20;
            foreach (var tile in _tileList.Tiles)
            {
                if (tile.Area.Intersects(visibleArea)) {
                    if (tile.Type == BiomeType.Water)
                    {
                        _water.Draw(tile.Position, tile.Frame, Color.White, tile.Scale);
                    } else
                    {
                        _terrain.Draw(tile.Position, tile.Frame, Color.White, tile.Scale);
                    }
                }

                // Uncomment for debugging
                //var rect = CalculateBoundingRectangle(_playerModel.Position);
                //_spriteBatch.DrawString(_gameFont, tile.Area.ToString().Replace("{", "").Replace("}", ""), new Vector2(tile.Area.Left, tile.Area.Center.Y), tile.IsVisible ? Color.Black : Color.White);
                //_spriteBatch.DrawString(_gameFont, "X: " + tile.Area.X.ToString() + " Y: " + tile.Area.Y.ToString(), new Vector2(tile.Area.Left, tile.Area.Center.Y), Color.Black);
                //_spriteBatch.DrawString(_gameFont, "W: " + tile.Area.Width.ToString() + " H: " + tile.Area.Height.ToString(), new Vector2(tile.Area.Left, tile.Area.Center.Y + 25), tile.IsVisible ? Color.Black : Color.White);
                //_spriteBatch.DrawString(_gameFont, rect.ToString().Replace("{", "").Replace("}", ""), new Vector2(rect.X + 10, rect.Y + 30), Color.Red);
            }

            foreach (var resource in _tileList.Resources)
            {
                if (resource.TileArea.Intersects(visibleArea) && resource.Amount > 0)
                {
                    if (resource.AmountVisible)
                    {
                        _spriteBatch.DrawString(_gameFont, resource.Type.ToString() + " " + resource.Amount.ToString(), new Vector2(resource.Position.X + 5, resource.Position.Y - 30), Color.White);
                    }

                    if (resource.Type == ResourceType.Rock || resource.Type == ResourceType.Sand || resource.Type == ResourceType.Water)
                    {
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
                }
            }
        }
    }
}

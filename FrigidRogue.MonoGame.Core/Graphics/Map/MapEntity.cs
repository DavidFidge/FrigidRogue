﻿using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Graphics.Quads;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Point = SadRogue.Primitives.Point;

namespace FrigidRogue.MonoGame.Core.Graphics.Map
{
    public class MapEntity : Entity, IDrawable, IDisposable
    {
        private TexturedQuadTemplate _mapQuad;
        private float _mapWidth;
        private float _mapHeight;
        private float _tileWidth;
        private float _tileHeight;
        private Vector3 _initialMapTranslation;

        public bool IsVisible { get; set; } = true;

        public float MapWidth => _mapWidth;
        public float HalfMapWidth => _mapWidth / 2f;
        public float MapHeight => _mapHeight;
        public float HalfMapHeight => _mapHeight / 2f;

        public MapEntity(IGameProvider gameProvider)
        {
            _mapQuad = new TexturedQuadTemplate(gameProvider);
            _mapQuad.AlphaEnabled = false;
        }
        
        public void Initialize(float tileWidth, float tileHeight)
        {
            _tileHeight = tileHeight;
            _tileWidth = tileWidth;
        }

        public void Draw(Matrix view, Matrix projection, Matrix world)
        {
            _mapQuad.Draw(view, projection, world);
        }

        public void SetMapTexture(Texture2D texture)
        {
            if (_mapQuad.Texture != texture)
                _mapQuad.Texture = texture;
        }

        public void LoadContent(int mapWidth, int mapHeight)
        {
            _mapWidth = mapWidth * _tileWidth;
            _mapHeight = mapHeight * _tileHeight;

            _initialMapTranslation = new Vector3(0, 3f, -15f);

            Transform.ChangeTranslation(_initialMapTranslation);
            TransformChanged();

            _mapQuad.LoadContent(_mapWidth, _mapHeight, null);
        }

        public void SetCentreTranslation(Point targetTile)
        {
            var xTranslation = (-targetTile.X * _tileWidth) + HalfMapWidth;
            var yTranslation = (targetTile.Y * _tileHeight) - HalfMapHeight;

            Transform.ChangeTranslation(_initialMapTranslation);
            Transform.ChangeTranslationRelative(new Vector3(xTranslation, yTranslation, 0));
            TransformChanged();
        }

        public void Dispose()
        {
            _mapQuad?.Dispose();
        }
    }
}
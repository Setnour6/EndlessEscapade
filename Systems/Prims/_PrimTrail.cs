﻿using EEMod.Autoloading;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;
using EEMod.Extensions;
using System.Linq;
using System;
using EEMod.Effects;
using EEMod.Items.Weapons.Mage;
using static Terraria.ModLoader.ModContent;
using System.Reflection;


namespace EEMod.Prim
{
    public partial class Primitive : IUpdateable
    {
        protected int RENDERDISTANCE => 2000;

        protected GraphicsDevice _device { get; private set; }
        protected Effect _effect { get; set; }
        protected Entity BindableEntity { get; set; }
        protected ITrailShader TrailShader { get; set; }

        public float _width;
        protected float Alpha;
        protected int _cap;
        public int _counter;
        protected int _noOfPoints;
        public List<Vector2> _points = new List<Vector2>();
        protected bool _destroyed = false;
        public bool behindTiles = false;
        public bool pixelated = false;
        public VertexPositionColorTexture[] vertices;
        protected int currentIndex;

        public Primitive(Entity entity)
        {
            TrailShader = new DefaultShader();
            _device = Main.graphics.GraphicsDevice;
            BindableEntity = entity;

            SetDefaults();

            vertices = new VertexPositionColorTexture[_cap];
        }

        public void Dispose()
        {
            PrimitiveSystem.primitives._trails.Remove(this);
        }

        public void Update()
        {
            OnUpdate();
        }

        public bool manualDraw = false;
        public void Draw()
        {
            vertices = new VertexPositionColorTexture[_noOfPoints];

            currentIndex = 0;

            PrimStructure(Main.spriteBatch);

            SetShaders(); //applying all shaders

            if (!manualDraw)
            {
                if (_noOfPoints >= 1)
                {
                    _device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, _noOfPoints / 3);
                }
            }

            PostDraw();
        }
        public virtual void OnUpdate() { }
        public virtual void PrimStructure(SpriteBatch spriteBatch) { }
        public virtual void SetShaders() { }
        public virtual void SetDefaults() { }
        public virtual void OnDestroy() { }
        public virtual void PostDraw() { }
    }
}
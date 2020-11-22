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
using EEMod.Projectiles.Mage;
using static Terraria.ModLoader.ModContent;
using System.Reflection;
using EEMod.Projectiles.Ranged;
using EEMod.Projectiles.Melee;
using EEMod.NPCs.CoralReefs;

namespace EEMod.Prim
{
    public partial class PrimTrail
    {
        protected static Vector2 CurveNormal(List<Vector2> points, int index)
        {
            if (points.Count == 1) return points[0];

            if (index == 0)
            {
                return Clockwise90(Vector2.Normalize(points[1] - points[0]));
            }
            if (index == points.Count - 1)
            {
                return Clockwise90(Vector2.Normalize(points[index] - points[index - 1]));
            }
            return Clockwise90(Vector2.Normalize(points[index + 1] - points[index - 1]));
        }
        protected static Vector2 Clockwise90(Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }

        protected void PrepareShader(Effect effects)
        {
            int width = _device.Viewport.Width;
            int height = _device.Viewport.Height;
            Vector2 zoom = Main.GameViewMatrix.Zoom;
            Matrix view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) * Matrix.CreateTranslation(width / 2, height / -2, 0) * Matrix.CreateRotationZ(MathHelper.Pi) * Matrix.CreateScale(zoom.X, zoom.Y, 1f);
            Matrix projection = Matrix.CreateOrthographic(width, height, 0, 1000);
            effects.Parameters["WorldViewProjection"].SetValue(view * projection);
            //_trailShader.ApplyShader(effects, this, _points, "MainPS");
        }
        protected void PrepareBasicShader()
        {
            int width = _device.Viewport.Width;
            int height = _device.Viewport.Height;
            Vector2 zoom = Main.GameViewMatrix.Zoom;
            Matrix view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) * Matrix.CreateTranslation(width / 2, height / -2, 0) * Matrix.CreateRotationZ(MathHelper.Pi) * Matrix.CreateScale(zoom.X, zoom.Y, 1f);
            Matrix projection = Matrix.CreateOrthographic(width, height, 0, 1000);
            _basicEffect.View = view;
            _basicEffect.Projection = projection;
            foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }
        }
        protected void AddVertex(Vector2 position, Color color, Vector2 uv)
        {
            if (currentIndex < vertices.Length)
                vertices[currentIndex++] = new VertexPositionColorTexture(new Vector3(position.ForDraw(), 0f), color, uv);
        }
        protected void MakePrimMidFade(int i, int Width, float alphaValue, ref int currentIndex, ref VertexPositionColorTexture[] vertices, Color baseColour = default, float fadeValue = 1, float sineFactor = 0)
        {
            Color c = (baseColour == default ? Color.White : baseColour) * (i / _cap) * fadeValue;
            Vector2 normal = CurveNormal(_points, i);
            Vector2 normalAhead = CurveNormal(_points, i + 1);
            float j = (_cap - (i * 0.9f)) / _cap;
            float width = (i / _cap) * Width;
            float width2 = ((i + 1) / _cap) * Width;
            Vector2 firstUp = _points[i] - normal * width + new Vector2(0, (float)Math.Sin(_counter / 10f + i / 3f)) * sineFactor;
            Vector2 firstDown = _points[i] + normal * width + new Vector2(0, (float)Math.Sin(_counter / 10f + i / 3f)) * sineFactor;
            Vector2 secondUp = _points[i + 1] - normalAhead * width2 + new Vector2(0, (float)Math.Sin(_counter / 10f + (i + 1) / 3f)) * sineFactor;
            Vector2 secondDown = _points[i + 1] + normalAhead * width2 + new Vector2(0, (float)Math.Sin(_counter / 10f + (i + 1) / 3f)) * sineFactor;

            AddVertex(firstDown, c * alphaValue, new Vector2((i / _cap), 1));
            AddVertex(firstUp, c * alphaValue, new Vector2((i / _cap), 0));
            AddVertex(secondDown, c * alphaValue, new Vector2((i + 1) / _cap, 1));

            AddVertex(secondUp, c * alphaValue, new Vector2((i + 1) / _cap, 0));
            AddVertex(secondDown, c * alphaValue, new Vector2((i + 1) / _cap, 1));
            AddVertex(firstUp, c * alphaValue, new Vector2((i / _cap), 0));
        }
        protected void DrawBasicTrail(Color c1, float widthVar)
        {
            int currentIndex = 0;
            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[_noOfPoints];
            for (int i = 0; i < _points.Count; i++)
            {
                if (i == 0)
                {
                  
                    Vector2 normalAhead = CurveNormal(_points, i + 1);
                    Vector2 secondUp = _points[i + 1] - normalAhead * widthVar;
                    Vector2 secondDown = _points[i + 1] + normalAhead * widthVar;
                    AddVertex(_points[i], c1 * _alphaValue, new Vector2((float)Math.Sin(_counter / 20f), (float)Math.Sin(_counter / 20f)));
                    AddVertex(secondUp, c1 * _alphaValue, new Vector2((float)Math.Sin(_counter / 20f), (float)Math.Sin(_counter / 20f)));
                    AddVertex(secondDown, c1 * _alphaValue, new Vector2((float)Math.Sin(_counter / 20f), (float)Math.Sin(_counter / 20f)));
                }
                else
                {
                    if (i != _points.Count - 1)
                    {
                        Vector2 normal = CurveNormal(_points, i);
                        Vector2 normalAhead = CurveNormal(_points, i + 1);
                        float j = (_cap + ((float)(Math.Sin(_counter / 10f)) * 1) - i * 0.1f) / _cap;
                        widthVar *= j;
                        Vector2 firstUp = _points[i] - normal * widthVar;
                        Vector2 firstDown = _points[i] + normal * widthVar;
                        Vector2 secondUp = _points[i + 1] - normalAhead * widthVar;
                        Vector2 secondDown = _points[i + 1] + normalAhead * widthVar;

                        AddVertex(firstDown, c1 * _alphaValue, new Vector2((i / _cap), 1));
                        AddVertex(firstUp, c1 * _alphaValue, new Vector2((i / _cap), 0));
                        AddVertex(secondDown, c1 * _alphaValue, new Vector2((i + 1) / _cap, 1));

                        AddVertex(secondUp, c1 * _alphaValue, new Vector2((i + 1) / _cap, 0));
                        AddVertex(secondDown, c1 * _alphaValue, new Vector2((i + 1) / _cap, 1));
                        AddVertex(firstUp, c1 * _alphaValue, new Vector2((i / _cap), 0));
                    }
                    else
                    {

                    }
                }
            }
            PrepareBasicShader();
            _device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, _noOfPoints / 3);
        }
    }
}

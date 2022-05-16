﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EEMod.ID;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.ID;
using EEMod.Seamap.Content;
using System.Diagnostics;
using ReLogic.Content;
using EEMod.Prim;
using EEMod.Extensions;
using EEMod.Seamap.Core;
using EEMod.Seamap.Content.Cannonballs;
using EEMod.Seamap.Content.Islands;

namespace EEMod.Seamap.Content
{
    public class SeamapPlayerShip : SeamapObject
    {
        public float ShipHelthMax = 5;
        public float shipHelth = 5;
        public int cannonDelay = 60;

        public int abilityDelay = 120;

        public Player myPlayer;

        public int invFrames = 20;


        public SeamapPlayerShip(Vector2 pos, Vector2 vel, Player player) : base(pos, vel)
        {
            position = pos;

            velocity = vel;

            myPlayer = player;

            width = 124;
            height = 98;

            rot = MathHelper.TwoPi * 3f / 4f;

            texture = ModContent.Request<Texture2D>("EEMod/Seamap/Content/SeamapPlayerShip", AssetRequestMode.ImmediateLoad).Value;

            PrimitiveSystem.primitives.CreateTrail(foamTrail = new FoamTrail(this, Color.Orange, 0.25f, 260));
        }

        FoamTrail foamTrail;

        public float boatSpeed = 0.0175f;

        public float rot;
        public float forwardSpeed;
        public float topSpeed = 2.4f;

        public Vector2 movementVel;

        public override void Update()
        {
            float prevForwardSpeed = movementVel.Length();

            CollisionChecks();

            if (invFrames > 0) invFrames--;

            if (invFrames <= 0)
            {
                if (myPlayer.controlUp && (forwardSpeed < topSpeed))
                {
                    forwardSpeed += boatSpeed;
                    forwardSpeed = MathHelper.Clamp(forwardSpeed, -boatSpeed * 5, topSpeed);
                }
                if (myPlayer.controlDown && forwardSpeed > (-boatSpeed * 5))
                {
                    forwardSpeed -= boatSpeed * 0.5f;
                    forwardSpeed = MathHelper.Clamp(forwardSpeed, -boatSpeed * 5, topSpeed);
                }
                if (myPlayer.controlRight)
                {
                    rot += (0.03f - MathHelper.Clamp(0.015f * (forwardSpeed / 2f), 0f, 0.015f));
                }
                if (myPlayer.controlLeft)
                {
                    rot -= (0.03f - MathHelper.Clamp(0.015f * (forwardSpeed / 2f), 0f, 0.015f));
                }

                if (myPlayer.controlUseItem && cannonDelay <= 0 && myPlayer == Main.LocalPlayer)
                {
                    LeftClickAbility();

                    cannonDelay = 60;
                }

                if (myPlayer.controlUseTile && abilityDelay <= 0)
                {
                    RightClickAbility();

                    abilityDelay = 120;
                }

                cannonDelay--;
                abilityDelay--;
            }

            if (shipHelth <= 0) Die();

            movementVel = Vector2.UnitX.RotatedBy(rot) * forwardSpeed;

            position += movementVel - (Seamap.Core.Seamap.windVector * 0.2f);

            if (!foamTrail.disposing)
            {
                boatTrailVector.X += VectorAbs(movementVel - (Seamap.Core.Seamap.windVector * 0.2f)).Length() / 2f;
                boatTrailVector.X += VectorAbs(velocity).Length() / 2f;
            }

            forwardSpeed = movementVel.Length();

            if (movementVel.Length() <= 0.5f && prevForwardSpeed > 0.5f)
            {
                foamTrail.disposing = true;
            }

            if(prevForwardSpeed <= movementVel.Length())
            {
                foamTrail.disposing = false;
            }

            base.Update();

            forwardSpeed *= 0.999f;
            velocity *= 0.96f;

            #region Position constraints
            if (position.X < 0) position.X = 0;
            if (position.X > Core.Seamap.seamapWidth - width) position.X = Core.Seamap.seamapWidth - width;

            if (position.Y < 0) position.Y = 0;
            if (position.Y > Core.Seamap.seamapHeight - height - 200) position.Y = Core.Seamap.seamapHeight - height - 200;
            #endregion
        }

        public override bool PreDraw(SpriteBatch spriteBatch)
        {
            Texture2D playerShipTexture = ModContent.Request<Texture2D>("EEMod/Seamap/Content/SeamapPlayerShip").Value;

            int frame = 0;
            float spriteRot = 0f;
            bool flipped = false;

            rot = TwoPiRestrict(rot);

            float rotForSprite = TwoPiRestrict(rot + MathHelper.PiOver2);
            float rotAbsed = Math.Abs(rotForSprite - MathHelper.Pi);

            if (rotForSprite > MathHelper.Pi && rotAbsed > (MathHelper.Pi / 9f) && rotAbsed < (8f * MathHelper.Pi / 9f)) flipped = true;

            if(rotAbsed < MathHelper.Pi / 9f)
            {
                frame = 8;
                spriteRot = (DynamicClamp(rotForSprite, MathHelper.Pi / 4.5f) - (MathHelper.Pi / 9f));
            }
            else if(rotAbsed < 2 * MathHelper.Pi / 9f)
            {
                frame = 7;
                spriteRot = DynamicClamp(rotForSprite, MathHelper.Pi / 9f) - (MathHelper.Pi / 18f);
            }
            else if(rotAbsed < 3 * MathHelper.Pi / 9f)
            {
                frame = 6;
                spriteRot = DynamicClamp(rotForSprite, MathHelper.Pi / 9f) - (MathHelper.Pi / 18f);
            }
            else if(rotAbsed < 4 * MathHelper.Pi / 9f)
            {
                frame = 5;
                spriteRot = DynamicClamp(rotForSprite, MathHelper.Pi / 9f) - (MathHelper.Pi / 18f);
            }
            else if(rotAbsed < 5 * MathHelper.Pi / 9f)
            {
                frame = 4;
                spriteRot = DynamicClamp(rotForSprite, MathHelper.Pi / 9f) - (MathHelper.Pi / 18f);
            }
            else if(rotAbsed < 6 * MathHelper.Pi / 9f)
            {
                frame = 3;
                spriteRot = DynamicClamp(rotForSprite, MathHelper.Pi / 9f) - (MathHelper.Pi / 18f);
            }
            else if(rotAbsed < 7 * MathHelper.Pi / 9f)
            {
                frame = 2;
                spriteRot = DynamicClamp(rotForSprite, MathHelper.Pi / 9f) - (MathHelper.Pi / 18f);
            }
            else if (rotAbsed < 8 * MathHelper.Pi / 9f)
            {
                frame = 1;
                spriteRot = DynamicClamp(rotForSprite, MathHelper.Pi / 9f) - (MathHelper.Pi / 18f);
            }
            else
            {
                frame = 0;
                spriteRot = (DynamicClamp(rotAbsed, MathHelper.Pi / 4.5f) - (MathHelper.Pi / 9f)) * (rotForSprite > MathHelper.Pi ? 1f : -1f);
            }


            int yVal = 114 * frame;

            spriteRot += (float)Math.Sin(Main.GameUpdateCount / 5f) * (invFrames / 80f);

            //spriteRot += (float)Math.Sin(Main.GameUpdateCount / 60f) / 12f;

            spriteBatch.Draw(playerShipTexture, Center - Main.screenPosition,
                new Rectangle(0, yVal, 124, 114),
                Color.White.LightSeamap(), spriteRot, 
                new Rectangle(0, 0, 124, 114).Size() / 2,
                1, flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            return false;
        }

        public void LeftClickAbility()
        {
            myPlayer.GetModPlayer<ShipyardPlayer>().LeftClickAbility(this);
        }

        public void RightClickAbility()
        {
            myPlayer.GetModPlayer<ShipyardPlayer>().RightClickAbility(this);
        }

        public void Die()
        {
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("EEMod/Assets/Sounds/ShipDeath"));

            myPlayer.GetModPlayer<SeamapPlayer>().ReturnHome();

            shipHelth = ShipHelthMax;
        }

        public float CannonRestrictRange()
        {
            float mouseRot = Vector2.Normalize(Main.MouseWorld - Center).ToRotation() + MathHelper.Pi;

            float angleOfFreedom = 0.4f;

            float realRot = rot;

            /*if (mouseRot - realRot > 0)
                return TwoPiRestrict(MathHelper.Clamp(mouseRot, realRot + 1.57f - angleOfFreedom, realRot + 1.57f + angleOfFreedom));
            else
                return TwoPiRestrict(MathHelper.Clamp(mouseRot, realRot - 1.57f - angleOfFreedom, realRot - 1.57f + angleOfFreedom));*/

            float toTheLeft = TwoPiRestrict(realRot - MathHelper.PiOver2);
            float toTheRight = TwoPiRestrict(realRot + MathHelper.PiOver2);

            if (Math.Acos(Vector2.Dot(Vector2.UnitX.RotatedBy(mouseRot), Vector2.UnitX.RotatedBy(toTheLeft))) < Math.Acos(Vector2.Dot(Vector2.UnitX.RotatedBy(mouseRot), Vector2.UnitX.RotatedBy(toTheRight))))
            {
                if(Math.Acos(Vector2.Dot(Vector2.UnitX.RotatedBy(mouseRot), Vector2.UnitX.RotatedBy(toTheLeft))) > angleOfFreedom)
                {
                    if ((mouseRot - toTheLeft) > MathHelper.Pi) return (toTheLeft - angleOfFreedom);
                    if ((mouseRot - toTheLeft) < -MathHelper.Pi) return (toTheLeft + angleOfFreedom);

                    return (((mouseRot - toTheLeft) < 0)) ? (toTheLeft - angleOfFreedom) : (toTheLeft + angleOfFreedom);
                }
                else
                {
                    return mouseRot;
                }
            }
            else 
            {
                if (Math.Acos(Vector2.Dot(Vector2.UnitX.RotatedBy(mouseRot), Vector2.UnitX.RotatedBy(toTheRight))) > angleOfFreedom)
                {
                    if ((mouseRot - toTheRight) > MathHelper.Pi) return (toTheRight - angleOfFreedom);
                    if ((mouseRot - toTheRight) < -MathHelper.Pi) return (toTheRight + angleOfFreedom);

                    return (((mouseRot - toTheRight) < 0)) ? (toTheRight - angleOfFreedom) : (toTheRight + angleOfFreedom);
                }
                else
                {
                    return mouseRot;
                }
            }
        }

        public float TwoPiRestrict(float val)
        {
            while (val > MathHelper.TwoPi)
                val -= MathHelper.TwoPi;

            while (val < 0)
                val += MathHelper.TwoPi;

            return val;
        }

        public float DynamicClamp(float val, float clamper)
        {
            while (val > clamper)
                val -= clamper;

            while (val < 0)
                val += clamper;

            return val;
        }

        #region Collision nonsense
        public static bool IsTouchingLeft(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return rect1.Right + vel.X > rect2.Left &&
              rect1.Left < rect2.Left &&
              rect1.Bottom > rect2.Top &&
              rect1.Top < rect2.Bottom;
        }

        public static bool IsTouchingRight(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return rect1.Left + vel.X < rect2.Right &&
              rect1.Right > rect2.Right &&
              rect1.Bottom > rect2.Top &&
              rect1.Top < rect2.Bottom;
        }

        public static bool IsTouchingTop(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return rect1.Bottom + vel.Y > rect2.Top &&
              rect1.Top < rect2.Top &&
              rect1.Right > rect2.Left &&
              rect1.Left < rect2.Right;
        }

        public static bool IsTouchingBottom(Rectangle rect1, Rectangle rect2, Vector2 vel)
        {
            return rect1.Top + vel.Y < rect2.Bottom &&
              rect1.Bottom > rect2.Bottom &&
              rect1.Right > rect2.Left &&
              rect1.Left < rect2.Right;
        }

        public void CollisionChecks()
        {
            foreach (SeamapObject obj in SeamapObjects.SeamapEntities)
            {
                if (obj == null) continue;

                if (obj.collides && invFrames <= 0)
                {
                    if (new Rectangle((int)position.X, (int)position.Y, 124, 98).Intersects(obj.Hitbox))
                    {
                        if (obj is Cannonball)
                            if ((int)(obj as Cannonball).team == myPlayer.team) continue;

                        shipHelth--;
                        invFrames = 20;

                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot("EEMod/Assets/Sounds/ShipHurt"));

                        velocity += Vector2.Normalize(obj.Center - Center) * boatSpeed * -15;
                        forwardSpeed = 0;
                    }
                }
            }
        }
        #endregion

        public Vector2 boatTrailVector;


        public Vector2 VectorAbs(Vector2 toAbs)
        {
            return new Vector2(Math.Abs(toAbs.X), Math.Abs(toAbs.Y));
        }
    }

    class FoamTrail : Primitive
    {
        public FoamTrail(Entity projectile, Color _color, float width = 40, int cap = 10) : base(projectile)
        {
            BindableEntity = projectile;
            _width = width;
            color = _color;
            _cap = cap;

            velocities = new List<float>();

            PrimitiveSystem.primitives.CreateTrail(trailLeft = new WakeTrail(this, BindableEntity, new Color(74, 189, 255), 2, _width, 1000, true));
            PrimitiveSystem.primitives.CreateTrail(trailRight = new WakeTrail(this, BindableEntity, new Color(74, 189, 255), 2, _width, 1000, false));
        }

        private Color color;

        public WakeTrail trailLeft;
        public WakeTrail trailRight;

        public List<float> velocities;

        public bool disposing = false;

        public override void SetDefaults()
        {
            Alpha = 0.8f;

            behindTiles = false;
            ManualDraw = false;
            pixelated = true;
            manualDraw = true;
        }

        public float lastDisposalSpeed;

        public override void PrimStructure(SpriteBatch spriteBatch)
        {
            if (_noOfPoints <= 1 || _points.Count() <= 1) return;
            float widthVar;

            for (int i = 0; i < 5; i++)
            {
                velocities.Insert(velocities.Count - 1, (BindableEntity as SeamapPlayerShip).movementVel.Length());

                _points.Insert(_points.Count - 1, BindableEntity.Center + new Vector2(0, 38) + new Vector2(-(((i / 5f) * 60f) - 30) * (float)Math.Cos((BindableEntity as SeamapPlayerShip).rot), -(((i / 5f) * 16f) - 8) * (float)Math.Sin((BindableEntity as SeamapPlayerShip).rot)));
            }

            float colorSin = (float)Math.Sin(_counter / 3f);
            {
                widthVar = 0;

                Vector2 normalAhead = CurveNormal(_points, 1);
                Vector2 secondUp = _points[1] - normalAhead * widthVar;
                Vector2 secondDown = _points[1] + normalAhead * widthVar;

                AddVertex(_points[0], Color.Lerp(Color.Black, Color.White, 1 / (float)(_points.Count() - 1)), new Vector2(0, 1));
                AddVertex(secondUp, Color.Lerp(Color.Black, Color.White, 1 / (float)(_points.Count() - 1)), new Vector2(0, 0));
                AddVertex(secondDown, Color.Lerp(Color.Black, Color.White, 1 / (float)(_points.Count() - 1)), new Vector2(0, 1));
            }

            for (int i = 1; i < _points.Count() - 1; i++)
            {
                widthVar = (_points.Count() - 1 - i) * _width * (velocities[i] / (BindableEntity as SeamapPlayerShip).topSpeed)
                    + ((float)Math.Sin(_points[i].X / 5f - _points[i].Y / 5f) - (float)Math.Cos(_points[i].Y / 5f - _points[i].X / 5f));

                widthVar = MathHelper.Clamp(widthVar, 0, 25);

                Vector2 normal = CurveNormal(_points, i);
                Vector2 normalAhead = CurveNormal(_points, i + 1);

                Vector2 firstUp = _points[i] - normal * (widthVar + 1f * (float)Math.Sin((i / 1f) + (_counter / 10f)));
                Vector2 firstDown = _points[i] + normal * (widthVar + 1f * (float)Math.Sin((i / 1f) + (_counter / 10f)));
                Vector2 secondUp = _points[i + 1] - normalAhead * (widthVar + 1f * (float)Math.Sin((i / 1f) + (_counter / 10f)));
                Vector2 secondDown = _points[i + 1] + normalAhead * (widthVar + 1f * (float)Math.Sin((i / 1f) + (_counter / 10f)));

                AddVertex(firstDown, Color.Lerp(Color.Lerp(Color.Black, Color.White, i / (float)(_points.Count() - 1)), Color.Black, 1 - disposingFloat), new Vector2((i / (float)(_points.Count())) % 1, 1));
                AddVertex(firstUp, Color.Lerp(Color.Lerp(Color.Black, Color.White, i / (float)(_points.Count() - 1)), Color.Black, 1 - disposingFloat), new Vector2((i / (float)(_points.Count())) % 1, 0));
                AddVertex(secondDown, Color.Lerp(Color.Lerp(Color.Black, Color.White, i / (float)(_points.Count() - 1)), Color.Black, 1 - disposingFloat), new Vector2(((i + 1) / (float)(_points.Count())) % 1, 1));

                AddVertex(secondUp, Color.Lerp(Color.Lerp(Color.Black, Color.White, i / (float)(_points.Count() - 1)), Color.Black, 1 - disposingFloat), new Vector2(((i + 1) / (float)(_points.Count())) % 1, 0));
                AddVertex(secondDown, Color.Lerp(Color.Lerp(Color.Black, Color.White, i / (float)(_points.Count() - 1)), Color.Black, 1 - disposingFloat), new Vector2(((i + 1) / (float)(_points.Count())) % 1, 1));
                AddVertex(firstUp, Color.Lerp(Color.Lerp(Color.Black, Color.White, i / (float)(_points.Count() - 1)), Color.Black, 1 - disposingFloat), new Vector2((i / (float)(_points.Count())) % 1, 0));
            }

            for (int i = 0; i < 5; i++)
            {
                velocities.RemoveAt(velocities.Count - 1);

                _points.RemoveAt(_points.Count - 1);
            }

            if(disposing)
            {
                disposingFloat -= (1f / 260f);

                disposingFloat = MathHelper.Clamp(disposingFloat, 0f, 1f);

                if(disposingFloat == 0)
                {
                    _points.Clear();

                    velocities.Clear();
                }
            }
            else
            {
                disposingFloat = 1f;
            }
        }

        public float disposingFloat = 1f;

        public override void SetShaders()
        {
            Matrix view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) * Matrix.CreateTranslation(_device.Viewport.Width / 2, _device.Viewport.Height / -2, 0) * Matrix.CreateRotationZ(MathHelper.Pi);

            Matrix projection = Matrix.CreateOrthographic(_device.Viewport.Width, _device.Viewport.Height, 0, 1000);

            Main.spriteBatch.End(); Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, default, default, EEMod.SeafoamShader);

            EEMod.SeafoamShader.Parameters["foamTexture"].SetValue(ModContent.GetInstance<EEMod>().Assets.Request<Texture2D>("Textures/Noise/FoamTrail3").Value);
            EEMod.SeafoamShader.Parameters["rippleTexture"].SetValue(ModContent.GetInstance<EEMod>().Assets.Request<Texture2D>("Textures/Noise/FoamTrail3").Value);

            EEMod.SeafoamShader.Parameters["offset"].SetValue(new Vector2((BindableEntity as SeamapPlayerShip).boatTrailVector.X + (BindableEntity as SeamapPlayerShip).boatTrailVector.Y, 0) / 400f);

            EEMod.SeafoamShader.Parameters["noColor"].SetValue(new Color(58, 110, 172).ToVector4() * 0f);
            EEMod.SeafoamShader.Parameters["color1"].SetValue(new Color(98, 153, 217).ToVector4() * 0.15f);
            EEMod.SeafoamShader.Parameters["color2"].SetValue(new Color(72, 159, 199).ToVector4() * 0.29f);
            EEMod.SeafoamShader.Parameters["color3"].SetValue(new Color(65, 198, 224).ToVector4() * 0.35f);
            EEMod.SeafoamShader.Parameters["color4"].SetValue(new Color(108, 211, 235).ToVector4() * 0.55f);
            EEMod.SeafoamShader.Parameters["color5"].SetValue(new Color(250, 255, 224).ToVector4() * 0.85f);

            EEMod.SeafoamShader.Parameters["WorldViewProjection"].SetValue(view * projection);

            if (vertices.Length == 0) return;

            DynamicVertexBuffer buffer = VertexBufferPool.Shared.RentDynamicVertexBuffer(VertexPositionColorTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            buffer.SetData(vertices);

            Main.graphics.GraphicsDevice.SetVertexBuffer(buffer);

            foreach (EffectPass pass in EEMod.SeafoamShader.CurrentTechnique.Passes)
            {
                pass.Apply();
            }

            if (_noOfPoints >= 1)
            {
                _device.DrawPrimitives(PrimitiveType.TriangleList, 0, _noOfPoints / 3);
            }

            VertexBufferPool.Shared.Return(buffer);
        }

        public override void OnUpdate()
        {
            _counter++;
            _noOfPoints = _points.Count() * 6;
            if (_cap < _noOfPoints / 6)
            {
                _points.RemoveAt(0);

                velocities.RemoveAt(0);
            }
            if ((!BindableEntity.active && BindableEntity != null) || _destroyed)
            {
                Dispose();
            }
            else
            {
                _points.Add(BindableEntity.Center + new Vector2(0, 38) + new Vector2(-30f * (float)Math.Cos((BindableEntity as SeamapPlayerShip).rot), -8f * (float)Math.Sin((BindableEntity as SeamapPlayerShip).rot)));

                velocities.Add((BindableEntity as SeamapPlayerShip).movementVel.Length());
            }
        }

        public override void OnDestroy()
        {
            _destroyed = true;
            _width *= 0.9f;
            if (_width < 0.05f)
            {
                Dispose();
            }
        }

        public override void PostDraw()
        {
            Main.spriteBatch.End(); Main.spriteBatch.Begin();
        }
    }

    class WakeTrail : Primitive
    {
        public WakeTrail(FoamTrail trail, Entity projectile, Color _color, int width = 40, float myWidth = 10, int cap = 10, bool _left = false) : base(projectile)
        {
            myTrail = trail;
            _width = width;
            _myWidth = myWidth;
            color = _color;
            _cap = cap;
            left = _left;
        }

        private Color color;

        public FoamTrail myTrail;

        public float _myWidth;

        public bool left;
        public override void SetDefaults()
        {
            Alpha = 0.8f;

            behindTiles = false;
            ManualDraw = false;
            pixelated = true;
            manualDraw = true;
        }

        public override void PrimStructure(SpriteBatch spriteBatch)
        {
            if (myTrail._points.Count() <= 1) return;

            _myWidth = myTrail._width + 0.1f;

            _points.Clear();

            if(left)
            {
                for(int i = 1; i < myTrail._points.Count() - 1; i++)
                {
                    Vector2 normal = CurveNormal(myTrail._points, i);

                    _points.Add(myTrail._points[i] - normal * ((myTrail._points.Count() - 1 - i) * _myWidth * (myTrail.velocities[i] / (BindableEntity as SeamapPlayerShip).topSpeed) + (1f * (float)Math.Sin((i / 1f) + (myTrail._counter / 10f)))));
                }
            }
            else
            {
                for (int i = 1; i < myTrail._points.Count() - 1; i++)
                {
                    Vector2 normal = CurveNormal(myTrail._points, i);

                    _points.Add(myTrail._points[i] + normal * ((myTrail._points.Count() - 1 - i) * _myWidth * (myTrail.velocities[i] / (BindableEntity as SeamapPlayerShip).topSpeed) + (1f * (float)Math.Sin((i / 1f) + (myTrail._counter / 10f)))));
                }
            }

            if (_points.Count() <= 1) return;

            float widthVar;

            float colorSin = (float)Math.Sin(_counter / 3f);
            {
                widthVar = 0;

                Vector2 normalAhead = CurveNormal(_points, 1);
                Vector2 secondUp = _points[1] - normalAhead * widthVar;
                Vector2 secondDown = _points[1] + normalAhead * widthVar;
                Vector2 v = new Vector2((float)Math.Sin(_counter / 20f));

                AddVertex(_points[0], color * Alpha, v);
                AddVertex(secondUp, color * Alpha, v);
                AddVertex(secondDown, color * Alpha, v);
            }

            for (int i = 1; i < _points.Count - 1; i++)
            {
                Alpha = (i / (float)(_points.Count - 1)) * 0.5f;
                widthVar = ((i) / (float)_points.Count) * _width * MathHelper.Clamp(myTrail.velocities[i], 0f, 1f);

                Vector2 normal = CurveNormal(_points, i);
                Vector2 normalAhead = CurveNormal(_points, i + 1);

                float j = (_cap + ((float)(Math.Sin(_counter / 10f)) * 1) - i * 0.1f) / _cap;
                widthVar *= j;

                Vector2 firstUp = _points[i] - normal * widthVar;
                Vector2 firstDown = _points[i] + normal * widthVar;
                Vector2 secondUp = _points[i + 1] - normalAhead * widthVar;
                Vector2 secondDown = _points[i + 1] + normalAhead * widthVar;

                AddVertex(firstDown, color * Alpha * myTrail.disposingFloat, new Vector2(((i + (_counter / BindableEntity.velocity.Length())) / (float)_cap) % 1, 1));
                AddVertex(firstUp, color * Alpha * myTrail.disposingFloat, new Vector2(((i + (_counter / BindableEntity.velocity.Length())) / (float)_cap) % 1, 0));
                AddVertex(secondDown, color * Alpha * myTrail.disposingFloat, new Vector2((((i + (_counter / BindableEntity.velocity.Length())) + 1) / (float)_cap) % 1, 1));

                AddVertex(secondUp, color * Alpha * myTrail.disposingFloat, new Vector2((((i + (_counter / BindableEntity.velocity.Length())) + 1) / (float)_cap) % 1, 0));
                AddVertex(secondDown, color * Alpha * myTrail.disposingFloat, new Vector2((((i + (_counter / BindableEntity.velocity.Length())) + 1) / (float)_cap) % 1, 1));
                AddVertex(firstUp, color * Alpha * myTrail.disposingFloat, new Vector2((((i + (_counter / BindableEntity.velocity.Length())) / (float)_cap)) % 1, 0));
            }
        }

        public override void SetShaders()
        {
            Matrix view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) * Matrix.CreateTranslation(_device.Viewport.Width / 2, _device.Viewport.Height / -2, 0) * Matrix.CreateRotationZ(MathHelper.Pi);

            Matrix projection = Matrix.CreateOrthographic(_device.Viewport.Width, _device.Viewport.Height, 0, 1000);

            Main.spriteBatch.End(); Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, default, default, EEMod.BasicEffect);

            EEMod.BasicEffect.Projection = view * projection;

            if (vertices.Length == 0) return;

            DynamicVertexBuffer buffer = VertexBufferPool.Shared.RentDynamicVertexBuffer(VertexPositionColorTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            buffer.SetData(vertices);

            Main.graphics.GraphicsDevice.SetVertexBuffer(buffer);

            foreach (EffectPass pass in EEMod.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }

            if (_noOfPoints >= 1)
            {
                _device.DrawPrimitives(PrimitiveType.TriangleList, 0, _noOfPoints / 3);
            }

            VertexBufferPool.Shared.Return(buffer);
        }

        public override void OnUpdate()
        {
            _counter++;
            _noOfPoints = _points.Count() * 6;
            if (_cap < _noOfPoints / 6)
            {
                _points.RemoveAt(0);
            }
            if ((!BindableEntity.active && BindableEntity != null) || _destroyed)
            {
                Dispose();
            }
        }

        public override void OnDestroy()
        {
            _destroyed = true;
            _width *= 0.9f;
            if (_width < 0.05f)
            {
                Dispose();
            }
        }

        public override void PostDraw()
        {
            Main.spriteBatch.End(); Main.spriteBatch.Begin();
        }
    }
}

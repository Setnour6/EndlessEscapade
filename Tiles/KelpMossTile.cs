using EEMod.Extensions;
using EEMod.Items.Placeables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.Tiles
{
    public class KelpMossTile : EETile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;

            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;

            Main.tileMerge[Type][ModContent.TileType<KelpLeafTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<LightGemsandTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<KelpRockTile>()] = true;

            AddMapEntry(new Color(88, 179, 179));

            DustType = DustID.Rain;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<LightGemsand>();
            //SoundStyle = 1;
            MineResist = 1f;
            MinPick = 0;
        }

        void PlaceGroundGrass(int i, int j)
        {
            int noOfGrassBlades = (int)(((i + j) % 16) * 0.2f);
            string tex = "Tiles/Foliage/KelpGrassShortMoss";
            string tex2 = "Tiles/Foliage/KelpGrassStubbedMoss";
            string Chosen = tex;

            for (int a = 0; a < noOfGrassBlades; a++)
            {
                switch ((i + j + a * 7) % 2)
                {
                    case 0:
                        Chosen = tex;
                        break;
                    case 1:
                        Chosen = tex2;
                        break;
                }
                float pos = i * 16 + (i + j * a + a * 7) % 16;
                //if ((i + j * a * 2) % 2 != 0)
                //    ModContent.GetInstance<EEMod>().TVH.AddElement(new Leaf(new Vector2(pos, j * 16), Chosen, 0f, Color.Lerp(Color.Yellow, Color.LightYellow, ((i + j + a * 3) % 4) / 4f), false, true,true));
                //else
                //{
                //    ModContent.GetInstance<EEMod>().TVH.AddElement(new Leaf(new Vector2(pos - ModContent.GetInstance<EEMod>().Assets.Request<Texture2D>(Chosen).Value.Width, j * 16), Chosen, 0f, Color.Lerp(Color.Yellow, Color.LightYellow, ((i + j + a * 3) % 4) / 4f), true, true, true));
                //}
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (!Main.tileSolid[Framing.GetTileSafely(i, j - 1).TileType] || !Framing.GetTileSafely(i, j - 1).HasTile && Framing.GetTileSafely(i, j).Slope == 0 && !Framing.GetTileSafely(i, j).IsHalfBlock && Main.GameUpdateCount % 500 == 0)
            {
                PlaceGroundGrass(i, j);
            }
            return true;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile || (!Main.tileSolid[(int)Framing.GetTileSafely(i, j - 1).TileType] && Framing.GetTileSafely(i, j - 1).HasTile))
            {
                Color color = Color.White * Math.Abs((float)Math.Sin(Main.GameUpdateCount / 200f));

                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);

                if (Main.rand.NextBool() && !Framing.GetTileSafely(i, j - 1).HasTile)
                {
                    Color chosen = Color.Lerp(Color.Yellow, Color.LightYellow, Main.rand.NextFloat(1f));
                    EEMod.MainParticles.SetSpawningModules(new SpawnRandomly(0.03f));
                    EEMod.MainParticles.SpawnParticles(new Vector2(i * 16, j * 16), -Vector2.UnitY, 3, chosen, new SlowDown(0.98f), new RotateTexture(Main.rand.NextFloat(-0.03f, 0.03f)), new SetMask(ModContent.GetInstance<EEMod>().Assets.Request<Texture2D>("Textures/RadialGradient").Value, Color.White), new AfterImageTrail(1f), new RotateVelocity(Main.rand.NextFloat(-0.02f, 0.02f)), new SetLighting(chosen.ToVector3(), 0.1f));
                }
                if (Main.drawToScreen)
                {
                    zero = Vector2.Zero;
                }

                Vector2 position = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;
                Texture2D texture = ModContent.GetInstance<EEMod>().Assets.Request<Texture2D>("Tiles/KelpMossTileGlow").Value;

                int TileFrameX = Framing.GetTileSafely(i, j).TileFrameX;
                int TileFrameY = Framing.GetTileSafely(i, j).TileFrameY;

                Rectangle rect = new Rectangle(TileFrameX, TileFrameY, 16, 16);

                Main.spriteBatch.Draw(texture, position, rect, Lighting.GetColor(i, j), 0f, default, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(texture, position, rect, color, 0f, default, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
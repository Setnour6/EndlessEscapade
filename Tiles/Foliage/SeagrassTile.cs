﻿using EEMod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace EEMod.Tiles.Foliage
{
    public class SeagrassTile : EETile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = false;
            Main.tileSolid[Type] = false;
            Main.tileBlendAll[Type] = false;
            Main.tileSolidTop[Type] = false;
            Main.tileNoAttach[Type] = true;

            AddMapEntry(new Color(28, 78, 47));
            //Main.tileCut[Type] = true;
            DustType = DustID.Grass;
            RegisterItemDrop(ModContent.ItemType<Kelp>());
            //SoundStyle = SoundID.Grass;
            MineResist = 1f;
            MinPick = 0;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 0, 0);
            TileObjectData.newTile.AnchorValidTiles = new int[] { ModContent.TileType<CoralSandTile>(), ModContent.TileType<SeagrassTile>() };
            TileObjectData.newTile.AnchorTop = default;

            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };

            TileObjectData.addTile(Type);
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (Framing.GetTileSafely(i, j + 1).TileType != ModContent.TileType<SeagrassTile>() && 
                Framing.GetTileSafely(i, j + 1).TileType != ModContent.TileType<CoralSandTile>() && 
                Framing.GetTileSafely(i, j + 1).TileType != TileID.Sand &&
                Framing.GetTileSafely(i, j + 1).TileType != ModContent.TileType<DirtySandTile>() &&
                Framing.GetTileSafely(i, j + 1).TileType != ModContent.TileType<KelpRockTile>())
            {
                //Main.tile[i, j].ClearTile();

                WorldGen.KillTile(i, j);
            }

            return true;
        }

        public override void RandomUpdate(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j - 1);
            if (!tile.HasTile && Main.rand.Next(4) == 0)
            {
                WorldGen.PlaceObject(i, j - 1, ModContent.TileType<SeagrassTile>());
                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<SeagrassTile>(), 0, 0, -1, -1);
            }
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Framing.GetTileSafely(i, j + 1);

            Texture2D tex = ModContent.GetInstance<EEMod>().Assets.Request<Texture2D>("Tiles/Foliage/SeagrassTile").Value;

            int TileFrameX = 0;
            int TileFrameY = 1;

            if(tile.TileType == ModContent.TileType<SeagrassTile>() && (!Framing.GetTileSafely(i, j - 1).HasTile || Framing.GetTileSafely(i, j - 1).TileType != ModContent.TileType<SeagrassTile>()))
            {
                TileFrameY = 0;
            }

            if (tile.TileType != ModContent.TileType<SeagrassTile>() && Framing.GetTileSafely(i, j - 1).TileType == ModContent.TileType<SeagrassTile>())
            {
                TileFrameY = 2;
            }

            TileFrameX = ((i * j) + (int)Math.Sin((i - (j * 2)) / (i * j))) % 2;

            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            Vector2 position = new Vector2(i * 16 - (int)Main.screenPosition.X + (int)(Math.Sin((Main.GameUpdateCount / 20f) + i - (j / 3f)) * 3f), j * 16 - (int)Main.screenPosition.Y) + zero;

            Rectangle rect = new Rectangle(TileFrameX * 16, TileFrameY * 16, 16, 16);

            Main.spriteBatch.Draw(tex, position, rect, Lighting.GetColor(i, j), 0f, default, 1f, SpriteEffects.None, 0f);

            if (TileFrameX == 1)
            {
                Lighting.AddLight(new Vector2(i * 16, j * 16), Color.Yellow.ToVector3() * 0.25f);

                Main.spriteBatch.Draw(tex, position, new Rectangle(32, TileFrameY * 16, 16, 16), Color.White, 0f, default, 1f, SpriteEffects.None, 0f);
            }

            return false;
        }
    }
}
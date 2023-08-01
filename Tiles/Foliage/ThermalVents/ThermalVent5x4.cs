using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;

namespace EEMod.Tiles.Foliage.ThermalVents
{
    public class ThermalVent5x4 : EETile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 5, 0);
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16,
                16,
                16
            };
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.CoordinatePadding = 0;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Thermal Vent");
            AddMapEntry(new Color(255, 100, 0), name);
            DustType = DustID.Dirt;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.40f;
            g = 0.08f;
            b = 0.13f;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Helpers.DrawTileGlowmask(Mod.Assets.Request<Texture2D>("Tiles/Foliage/ThermalVents/ThermalVent5x4Glow").Value, i, j);
        }
    }
}
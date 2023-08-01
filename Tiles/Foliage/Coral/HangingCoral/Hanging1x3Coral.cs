using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace EEMod.Tiles.Foliage.Coral.HangingCoral
{
    public class Hanging1x3Coral : EETile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.RandomStyleRange = 2;
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16,
                16
            };

            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Coral Lamp");
            AddMapEntry(new Color(0, 100, 200), name);
            DustType = DustID.Dirt;
        }
    }
}
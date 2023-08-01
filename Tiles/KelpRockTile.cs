using EEMod.Items.Placeables;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.Tiles
{
    public class KelpRockTile : EETile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;

            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;

            Main.tileMerge[Type][ModContent.TileType<KelpMossTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<KelpLeafTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<LightGemsandTile>()] = true;

            AddMapEntry(new Color(48, 200, 135));

            DustType = DustID.Rain;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<Gemsandstone>();
            ////SoundStyle = 1;
            MineResist = 1f;
            MinPick = 0;
        }
    }
}
using EEMod.Items.Placeables;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.Tiles
{
    public class CoralsandstoneTile : EETile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;

            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;

            Main.tileMerge[Type][ModContent.TileType<CoralSandTile>()] = true;

            AddMapEntry(new Color(26, 13, 0));

            DustType = DustID.Rain;
            RegisterItemDrop(ModContent.ItemType<Coralsandstone>());
            //SoundStyle = 1;
            MineResist = 1f;
            MinPick = 0;
        }
    }
}
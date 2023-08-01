using EEMod.Items.Placeables;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.Tiles
{
    public class DarkGemsandstoneTile : EETile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;

            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(new Color(67, 47, 155));

            DustType = DustID.Rain;
            RegisterItemDrop(ModContent.ItemType<DarkGemsandstone>());
            //SoundStyle = 1;
            MineResist = 1f;
            MinPick = 0;
        }
    }
}
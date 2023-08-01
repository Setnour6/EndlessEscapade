using EEMod.Items.Placeables.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EEMod.Tiles.Ores
{
    public class DalantiniumOreTile : EETile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            //Main.tileValue[Type] = 410; // Metal Detector value
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 1100; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true; // lemme open some important files so you can acess them
            Main.tileBlockLight[Type] = true; //

            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Dalantinium");
            AddMapEntry(new Color(152, 171, 198), name);

            DustType = DustID.Platinum;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<DalantiniumOre>();
            //SoundType = SoundID.Tink;
            //SoundStyle = 1;
            MineResist = 1f;
            MinPick = 60;
        }
    }
}
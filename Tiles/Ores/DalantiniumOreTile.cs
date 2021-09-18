using EEMod.Items.Placeables.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.Tiles.Ores
{
    public class DalantiniumOreTile : EETile
    {
        public override void SetDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            //Main.tileValue[Type] = 410; // Metal Detector value
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 1100; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true; // lemme open some important files so you can acess them
            Main.tileBlockLight[Type] = true; //

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Dalantinium");
            AddMapEntry(new Color(152, 171, 198), name);

            dustType = DustID.Platinum;
            drop = ModContent.ItemType<DalantiniumOre>();
            soundType = SoundID.Tink;
            soundStyle = 1;
            mineResist = 1f;
            minPick = 60;
        }

        public override bool CanExplode(int i, int j)
        {
            return EEWorld.EEWorld.downedHydros;
        }
    }
}
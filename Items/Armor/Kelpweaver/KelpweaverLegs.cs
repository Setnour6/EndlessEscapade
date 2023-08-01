using EEMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.Items.Armor.Kelpweaver
{
    [AutoloadEquip(EquipType.Legs)]
    public class KelpweaverLegs : EEItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kelpweaver Legs");
            // Tooltip.SetDefault("Creepy and crawly");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 30);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 4;
        }
    }
}
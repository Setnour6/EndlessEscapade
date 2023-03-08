using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EndlessEscapade.Content.Seamap
{
    class DevConch : ModItem { // alternative to eeseamap command
        public override string Texture => nameof(EndlessEscapade) + "/Assets/missing";
        public override void SetDefaults() {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = Item.useAnimation = 40;
            Item.consumable = false;
        }
        public override bool? UseItem(Player player) {
            SeamapHandler.EnterSeamap();
            return base.UseItem(player);
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.Items
{
    public class Glider : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glider");
            ItemID.Sets.SortingPriorityMaterials[item.type] = 59; // influences the inventory sort order. 59 is PlatinumBar, higher is more valuable.
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 50;

            item.maxStack = 1;

            item.holdStyle = 1;

            //item.flame = true; needs a flame texture.
            item.noWet = true;
        }
        int lerpage;
        public override void HoldStyle(Player player)
        {
            Tile tile = Main.tile[(int)player.position.X / 16, (int)player.position.Y / 16 + 3];
            if (tile.active()
                && Main.tileSolid[tile.type]
                && Math.Abs(player.fullRotation) > 0.01f)
            {
                player.fullRotation -= player.fullRotation / 16f;
                lerpage = 0;
            }
            else
            {
                if (player.velocity.Y > 0)
                {
                    lerpage += 1;
                    float rotFactor = Math.Max(0, player.velocity.Y / 150f);
                    player.fullRotation += (float)Math.Sin(lerpage / 20f - .1f) * rotFactor * Helpers.Clamp(lerpage/200f,0,1);
                }
            }
            if (player.velocity.Y > 0)
            {

                player.gravity = 0.133f;
                player.bodyFrame.Y = 4 * 56;

                player.velocity.Y = 1;
                if (Math.Abs(player.velocity.X) < 18)
                {
                    if ((player.controlRight && player.velocity.X > 0) || (player.controlLeft && player.velocity.X < 0))
                    {
                        player.velocity.X *= 1.02f;
                    }
                }
            }
        }
    }
}

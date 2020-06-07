﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InteritosMod.Items.Placeables.Ores;

namespace InteritosMod.Items.Weapons.Melee
{
    public class LythenSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lythen Sword");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.rare = ItemRarityID.Green;
            item.autoReuse = false;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 7f; // 5 and 1/4
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = Item.buyPrice(0, 0, 30, 0);
            item.damage = 12;
            item.width = 20;
            item.height = 20;
            item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<LythenBar>(), 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
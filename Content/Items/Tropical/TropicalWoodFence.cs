﻿using Terraria.ModLoader;

namespace EndlessEscapade.Content.Items.Tropical;

public class TropicalWoodFence : ModItem
{
    public override void SetDefaults() {
        Item.DefaultToPlaceableWall((ushort)ModContent.WallType<Walls.Tropical.TropicalWoodFence>());
    }
}

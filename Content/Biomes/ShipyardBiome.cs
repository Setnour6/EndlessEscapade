﻿using Terraria;
using Terraria.ModLoader;

namespace EndlessEscapade.Content.Biomes;

public class ShipyardBiome : ModBiome
{
    public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/ShipyardNight");

    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

    public override bool IsBiomeActive(Player player) {
        var isLeftSide = player.position.X / 16f < Main.maxTilesX / 2;

        return player.ZoneBeach && isLeftSide;
    }
}

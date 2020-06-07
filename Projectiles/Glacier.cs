
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Prophecy.Projectiles
{
    public class Glacier : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacier");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 8;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.scale = 1f;
        }
        public override void AI()
        {

        }

    }
}

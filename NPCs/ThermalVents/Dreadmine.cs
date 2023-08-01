using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace EEMod.NPCs.ThermalVents
{
    public class Dreadmine : EEProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dreadmine");
            //  Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 40;
            Projectile.penetrate = -1;

            // Projectile.tileCollide = false;
            // Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.damage = 200;
        }

        private NPC OwnerNpc => Main.npc[(int)Projectile.ai[0]];

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            Projectile.Center = new Vector2(OwnerNpc.ai[2], OwnerNpc.ai[3]);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie);
            }
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
            Projectile.Kill();
            NPC.HitInfo nPCHitInfo = new();
            nPCHitInfo.Damage = 55;
            OwnerNpc.StrikeNPC(nPCHitInfo); // Don't know what values to set ~Setnour6
        }
    }
}
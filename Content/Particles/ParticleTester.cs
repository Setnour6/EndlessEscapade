using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

#nullable enable
namespace EndlessEscapade.Content.Particles
{
    /*class ParticleTester  : ModSystem {
        public override void PostUpdateNPCs() {
            base.PostUpdateNPCs();
            for (int i = 0; i < 20; i++) {
                var particle = ParticleManager.Create();
                particle.Position = Main.MouseWorld;
                particle.Velocity = new Vector2(4, 0).RotatedBy(i/20f * MathHelper.TwoPi);
                particle.Color = Color.White;
                particle.Scale = new Vector2(2);
                particle.TimeLeft = 140;

                TestParticleType.SpawnAt(Main.MouseWorld, new Vector2(2, 1).RotatedBy(0.01f+i/20f * MathHelper.TwoPi));
            }
        }
    }

    internal class TestParticleType : ParticleType<TestParticleType> {
        public override void SetDefaults(Particle particle) {
            base.SetDefaults(particle);
            particle.TimeLeft = 40;
            particle.Color = Color.Blue;
            particle.Scale = new(5);
        }
    }*/
}

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable enable
namespace EndlessEscapade.Content.Particles
{
    internal class EEParticleSystem : ModSystem
    {
        internal static EEParticleSystem Instance => ModContent.GetInstance<EEParticleSystem>();

        Texture2D pixel = null!;

        public override void Load() {
            base.Load();
            var task = Main.RunOnMainThread(MainThreadLoad);
            On.Terraria.Main.DrawDust += Main_DrawDust;
            task.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public override void Unload() {
            base.Unload();
            Main.RunOnMainThread(pixel.Dispose);
        }

        void MainThreadLoad() {
            var gd = Main.graphics.GraphicsDevice;
            pixel = new(gd, 1, 1);
            pixel.SetData(new Color[] { Color.White });
        }

        private void Main_DrawDust(On.Terraria.Main.orig_DrawDust orig, Main self) {
            orig(self);
            DoDrawParticles();
        }

        public override void PostUpdateDusts() {
            DoUpdateParticles();
        }


        void DoDrawParticles() {
            var sb = Main.spriteBatch;
            System.Diagnostics.Stopwatch sw = new();
            sw.Start();
            sb.Begin();
            foreach(Particle particle in ParticleManager.ActiveParticles()) {
                    sb.Draw(particle.Get<TextureComponent>().Texture?.Value ?? pixel, particle.Position - Main.screenPosition, null, particle.Color, 0, default, particle.Scale, SpriteEffects.None, 0f);
            }
            sb.End();
            sw.Stop();
            //Console.WriteLine($"Drew particles in {sw.Elapsed.TotalMilliseconds}ms");
        }

        void DoUpdateParticles() {
            System.Diagnostics.Stopwatch sw = new();
            sw.Start();
            int count = 0;
            foreach(Particle particle in ParticleManager.ActiveParticles()) { 
                    UpdateParticle(particle);
                    count++;
            }
            sw.Stop();
            //Console.WriteLine($"Updated particles: {count} in {sw.Elapsed.TotalMilliseconds}ms");
        }

        static void UpdateParticle(Particle particle) {
            particle.Position += particle.Velocity;
            if (particle.TimeLeft-- < 0) {
                ParticleManager.Destroy(particle);
            }
            particle.Get<ParticleTypeComponent>().TypeInstance?.Update(particle);
        }

    }

}

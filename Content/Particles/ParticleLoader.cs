using Terraria.ModLoader;

namespace EndlessEscapade.Content.Particles
{
    class ParticleLoader : ModSystem {
        static int lastID;
        internal static int ReserveID() => lastID++;
        public override void Load() {
            base.Load();
        }
        public override void Unload() {
            base.Unload();
        }
    }
}

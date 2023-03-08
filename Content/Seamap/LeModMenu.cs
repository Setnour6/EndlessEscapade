using System.IO;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace EndlessEscapade.Content.Seamap
{
    class LeModMenu : ModMenu
    {
        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => base.MenuBackgroundStyle;
        public override Asset<Texture2D> MoonTexture => DummyTexture;
        private Asset<Texture2D> dummyTexture;
        private Asset<Texture2D> DummyTexture { 
            get {
                if (dummyTexture == null) {
                    using (MemoryStream ms = new(4 * sizeof(int)))
                    using (BinaryWriter writer = new(ms)) {
                        writer.Write(1); // width
                        writer.Write(1); // height
                        writer.Write(1); // version
                        writer.Write(0);
                        //writer.Write((uint)Color.White.PackedValue); // color
                        ms.Seek(0, SeekOrigin.Begin);
                        dummyTexture = EndlessEscapade.Instance.Assets.CreateUntracked<Texture2D>(ms, "DummyPixel", AssetRequestMode.ImmediateLoad);
                    }
                }
                return dummyTexture;
            } 
        }
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor) {
            return false;
        }
    }
}

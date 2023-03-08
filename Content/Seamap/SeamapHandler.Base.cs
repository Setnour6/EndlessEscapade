using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Social.Steam;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.Graphics.Effects;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace EndlessEscapade.Content.Seamap
{

    internal partial class SeamapHandler : ModSystem
    {
        static bool onSeamap;

        public override void Load() {
            base.Load();
            /*On.Terraria.Main.UpdateMenu += Main_UpdateMenu;
            On.Terraria.Main.SetBackColor += Main_SetBackColor;
            */
            On.Terraria.Main.DrawMenu += Main_DrawMenu;
            On.Terraria.Main.DrawBG += Main_DrawBG;
            On.Terraria.Main.DrawBackground += Main_DrawBackground;
            On.Terraria.Main.RenderBackground += Main_RenderBackground;
            On.Terraria.Graphics.Effects.SkyManager.Draw += SkyManager_Draw;
            On.Terraria.Graphics.Effects.OverlayManager.Draw += OverlayManager_Draw;
            IL.Terraria.Main.DoDraw += Main_DoDraw;
            //onSeamap = true;
        }

        public override void Unload() {
            base.Unload();
        }

        private static void Main_DoDraw(ILContext il) {
            ILCursor c = new(il);
            c.GotoNext(i=>i.MatchCallOrCallvirt<FilterManager>(nameof(FilterManager.BeginCapture)))
                .GotoNext(MoveType.After, i => i.MatchLdsfld<Main>(nameof(Main.mapFullscreen)))
                .EmitDelegate((bool value) => value || onSeamap);
        }

        private static void Main_SetBackColor(On.Terraria.Main.orig_SetBackColor orig, Main.InfoToSetBackColor info, out Color sunColor, out Color moonColor) {
            if (onSeamap) {
                sunColor = Color.Black;
                moonColor = Color.Black;
                return;
            }

            orig(info, out sunColor, out moonColor);
        }

        private static void OverlayManager_Draw(On.Terraria.Graphics.Effects.OverlayManager.orig_Draw orig, OverlayManager self, SpriteBatch spriteBatch, RenderLayers layer, bool beginSpriteBatch) {
            if (onSeamap)
                return;
            orig(self, spriteBatch, layer, beginSpriteBatch);
        }

        private static void SkyManager_Draw(On.Terraria.Graphics.Effects.SkyManager.orig_Draw orig, SkyManager self, SpriteBatch spriteBatch) {
            if (onSeamap)
                return;
            orig(self, spriteBatch);
        }

        private static void Main_RenderBackground(On.Terraria.Main.orig_RenderBackground orig, Main self) {
            if (onSeamap)
                return;
            orig(self);
        }

        private static void Main_DrawBG(On.Terraria.Main.orig_DrawBG orig, Main self) {
            if (onSeamap)
                return;
            orig(self);
        }

        private static void Main_DrawBackground(On.Terraria.Main.orig_DrawBackground orig, Main self) {
            if (onSeamap)
                return;
            orig(self);
        }


        private void Main_DrawMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime) {
            ;
            try {
                if (onSeamap) {
                    Main.spriteBatch.End();
                    DrawSeamap();
                    return;
                }
                else {
                    orig(self, gameTime);
                }
            }
            catch {

            }
        }

        private void Main_UpdateMenu(On.Terraria.Main.orig_UpdateMenu orig) {
            orig();
            if (onSeamap) {
                UpdateSeamap();
                return;
            }
        }

        internal static void DrawSeamap() {
            SeamapSystem.DrawSeamap();
        }

        internal static void UpdateSeamap() {
            SeamapSystem.UpdateSeamap();
        }

        static void QuitGame(Action callback = null) {
            SteamedWraps.StopPlaytimeTracking();
            SystemLoader.PreSaveAndQuit();
            IngameOptions.Close();
            Main.menuMode = 10;
            Main.gameMenu = true;
            WorldGen.SaveAndQuit(callback);
        }

        internal static void EnterSeamap() {
            QuitGame(() => {
                Main.menuMode = 10;
                Main.fastForwardTime = false;
                Main.statusText = "EEEEEE";
                onSeamap = true;
            });
            //SubworldSystem.Enter<SeamapSubworld>();
        }

        internal static void ExitSeamap() {

        }
    }
}

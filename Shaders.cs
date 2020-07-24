﻿using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using EEMod.Autoloading;
using EEMod.NPCs.Bosses.Akumo;

namespace EEMod
{
    public partial class EEMod
    {
        public static int noOfPasses = 499;
        public static int startingTermination = 1;
        [LoadingMethod(LoadMode.Client)]
        internal static void ShaderLoading()
        {
            // instance is a static field and this method is still inside the mod class
            Ref<Effect> screenRef = new Ref<Effect>(instance.GetEffect("Effects/PracticeEffect"));
            Ref<Effect> screenRef2 = new Ref<Effect>(instance.GetEffect("Effects/Shockwave"));
            Ref<Effect> screenRef3 = new Ref<Effect>(instance.GetEffect("Effects/Pause"));
            Ref<Effect> screenRef4 = new Ref<Effect>(instance.GetEffect("Effects/WhiteFlash"));
            Filters.Scene["EEMod:Akumo"] = new Filter(new AkumoScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.5f, 0.2f).UseOpacity(0.6f), EffectPriority.VeryHigh);
            Filters.Scene["EEMod:Boom"] = new Filter(new ScreenShaderData(screenRef, "DeathAnimation"), EffectPriority.VeryHigh);
            Filters.Scene["EEMod:Boom"].Load();
            Filters.Scene["EEMod:Shockwave"] = new Filter(new ScreenShaderData(screenRef2, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["EEMod:Shockwave"].Load();
            Filters.Scene["EEMod:Pause"] = new Filter(new ScreenShaderData(screenRef3, "Pauses"), EffectPriority.VeryHigh);
            Filters.Scene["EEMod:Pause"].Load();
            for (int i = startingTermination; i <= noOfPasses; i++)
            {
                Filters.Scene[$"EEMod:Filter{i}"] = new Filter(new ScreenShaderData(screenRef4, $"Filter{i}"), EffectPriority.VeryHigh);
                Filters.Scene[$"EEMod:Filter{i}"].Load();
            }
            SkyManager.Instance["EEMod:Akumo"] = new AkumoSky();
        }
    }
}

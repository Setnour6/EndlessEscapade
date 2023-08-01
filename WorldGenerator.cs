﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace EEMod
{
    public class WorldGenerator
    {
        internal List<GenPass> _passes = new List<GenPass>();
        internal double _totalLoadWeight; // Previously float
        internal int _seed;

        public WorldGenerator(int seed) => _seed = seed;

        public void Append(GenPass pass)
        {
            _passes.Add(pass);
            _totalLoadWeight += pass.Weight;
        }

        public void GenerateWorld(GenerationProgress progress = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            double num = 0f; // Previously float

            foreach (GenPass pass in _passes)
            {
                num += pass.Weight;
            }

            if (progress == null)
            {
                progress = new GenerationProgress();
            }

            progress.TotalWeight = num;
            Main.menuMode = 888;
            Main.MenuUI.SetState(new UIWorldLoad());

            foreach (GenPass pass in _passes)
            {
                WorldGen._genRand = new UnifiedRandom(_seed);
                Main.rand = new UnifiedRandom(_seed);
                stopwatch.Start();
                progress.Start(pass.Weight);

                try
                {
                    pass.Apply(progress, null);
                }
                catch (Exception ex)
                {
                    string text = string.Join("\n", Language.GetTextValue("tModLoader.WorldGenError"), pass.Name, ex);

                    throw ex;
                }

                progress.End();
                stopwatch.Reset();
            }
        }
    }
}
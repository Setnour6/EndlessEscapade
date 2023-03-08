using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using SubworldLibrary;
using Terraria.WorldBuilding;
using Terraria.IO;
using Microsoft.Xna.Framework;

namespace EndlessEscapade.Content.Seamap
{
    public class SeamapSubworld : Subworld
    {
        public override int Width => 800;

        public override int Height => 400;

        public override List<GenPass> Tasks => new() { new DefaultGenPass() };

        public override bool ShouldSave => false;

        public override bool NoPlayerSaving => true;

        public class DefaultGenPass : GenPass
        {
            public DefaultGenPass() : base("EE:DefaultSeamapPass", 1f) {

            }
            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
                Point center = new(_worldWidth, _worldHeight);
                for (int i = center.X - 2; i < center.Y + 2; i++) {
                    WorldGen.PlaceTile(i, center.Y, TileID.Dirt);
                }
            }
        }
    }
}

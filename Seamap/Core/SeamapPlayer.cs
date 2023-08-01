using EEMod.Buffs.Debuffs;
using EEMod.Config;
using EEMod.Extensions;
using EEMod.ID;
using EEMod.Net;
using EEMod.NPCs;
using EEMod.NPCs.Bosses;
using EEMod.NPCs.Bosses.Hydros;
using EEMod.Projectiles;
using EEMod.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using static EEMod.EEWorld.EEWorld;
using static Terraria.ModLoader.ModContent;
using ReLogic.Graphics;
using EEMod.Seamap.Content;
using EEMod.Seamap.Core;
using EEMod.Autoloading;
using EEMod.Seamap.Content.Islands;
using System.Diagnostics;
using EEMod.Tiles.Furniture;
using Terraria.Audio;
using Terraria.ModLoader.IO;
using EEMod.Subworlds;
using EEMod.Subworlds.CoralReefs;

namespace EEMod
{
    public class SeamapPlayer : ModPlayer
    {
        public bool importantCutscene;

        public int timerForCutscene;
        public bool arrowFlag = false;
        public static bool isSaving;
        public float titleText;
        public float titleText2;
        public float subTextAlpha;
        public bool noU;
        public int coralReefTrans;
        public int seamapUpdateCount;

        public bool IncreaseStarFall;

        public string prevKey = "Main";

        public bool hasLoadedIntoWorld;

        public Vector2 myLastBoatPos;

        public bool lastKeySeamap;

        public float quickOpeningFloat = 60;

        public string exitingSeamapKey;

        public void ReturnHome()
        {
            SubworldLibrary.SubworldSystem.Exit();

            Player.GetModPlayer<ShipyardPlayer>().cutSceneTriggerTimer = 0;
            Player.GetModPlayer<ShipyardPlayer>().triggerSeaCutscene = false;
            Player.GetModPlayer<ShipyardPlayer>().speedOfPan = 0;

            hasLoadedIntoWorld = false;

            lastKeySeamap = true;

            prevKey = KeyID.Sea;

            if (Main.netMode == NetmodeID.Server)
            {
                Netplay.Connection.State = 1;
            }

            EEMod.isSaving = true;
        }

        public override void OnEnterWorld()
        {
            if (prevKey == KeyID.Sea && !hasLoadedIntoWorld)
            {
                hasLoadedIntoWorld = true;
                if(lastKeySeamap) Player.position = (new Vector2((int)shipCoords.X - 2 + 7 + 12, (int)shipCoords.Y - 18 - 2 + 25) * 16);

                Player.GetModPlayer<ShipyardPlayer>().cutSceneTriggerTimer = 0;
                Player.GetModPlayer<ShipyardPlayer>().triggerSeaCutscene = false;
                Player.GetModPlayer<ShipyardPlayer>().speedOfPan = 0;

                lastKeySeamap = false;

                Main.screenPosition = Player.Center - new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f);
            }

            EEMod.isSaving = false;

            Main.time = time;
            Main.dayTime = dayTime;
        }

        public double time;
        public bool dayTime;

        public bool exitingSeamap = false;

        public void EnterSeamap()
        {
            time = Main.time;
            dayTime = Main.dayTime;

            seamapUpdateCount = 0;

            SubworldLibrary.SubworldSystem.Enter<Sea>();

            EEMod.isSaving = true;

            Player.GetModPlayer<ShipyardPlayer>().cutSceneTriggerTimer = 0;

            quickOpeningFloat = 60;

            exitingSeamap = false;
        }

        public override void PreUpdate()
        {
            if (!SubworldLibrary.SubworldSystem.IsActive<Sea>()) return;

            Player.position = Player.oldPosition;

            Player.position.X = (Main.maxTilesX * 16 * (2f / 3f)) + 300;
            Player.position.Y = (Main.maxTilesY * 16 * (2f / 3f)) + 300;

            Player.fallStart = (int)(Player.position.Y / 16f);

            #region Opening cutscene for seamap

            if(exitingSeamap)
            {
                quickOpeningFloat++;

                if (quickOpeningFloat > 60) OnExitSeamap();
            }
            else if(quickOpeningFloat > 0)
            {
                quickOpeningFloat--;
            }

            #endregion

            seamapUpdateCount++;

            if (seamapUpdateCount == 1)
                Seamap.Core.Seamap.InitializeSeamap();

            Seamap.Core.Seamap.UpdateSeamap();

            #region Island Interact methods
            foreach (SeamapObject obj in SeamapObjects.SeamapEntities)
            {
                if (obj is Island)
                {
                    Island island = obj as Island;

                    prevKey = KeyID.Sea;

                    Player.ClearBuff(BuffID.Cursed);
                    Player.ClearBuff(BuffID.Invisibility);

                    if (Vector2.DistanceSquared(SeamapObjects.localship.Hitbox.Center.ToVector2(), obj.Center) < (obj.width * 2f) * (obj.width * 2f) &&
                        Main.LocalPlayer.controlJump)
                    {
                        island.Interact();
                    }
                }
            }
            #endregion
        }

        public override void SaveData(TagCompound tag)
        {
            tag["lastPos"] = myLastBoatPos;
        }

        public override void LoadData(TagCompound tag)
        {
            tag.TryGetRef("lastPos", ref myLastBoatPos);
        }

        public void OnExitSeamap()
        {
            quickOpeningFloat = 0;

            switch (exitingSeamapKey)
            {
                case KeyID.CoralReefs:
                    SubworldLibrary.SubworldSystem.Enter<CoralReefs>();
                    break;
                case KeyID.GoblinFort:
                    SubworldLibrary.SubworldSystem.Enter<GoblinFort>();
                    break;
                case "Main":
                    ReturnHome();
                    break;
                default:
                    break;
            }
        }
    }
}
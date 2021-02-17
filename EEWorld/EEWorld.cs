using EEMod.Autoloading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using EEMod.Tiles;
using EEMod.Tiles.Furniture;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using EEMod.ID;
using EEMod.Tiles.Foliage.Coral;
using EEMod.Tiles.Ores;
using EEMod.Tiles.Walls;
using Terraria.GameContent.Events;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Terraria.DataStructures;
using EEMod.Tiles.EmptyTileArrays;
using System.Linq;
using EEMod.VerletIntegration;
using EEMod.Prim;

namespace EEMod.EEWorld
{
    public partial class EEWorld : ModWorld
    {
        public int minionsKilled;
        public static EEWorld instance => ModContent.GetInstance<EEWorld>();
        public static bool downedTalos;
        public static bool downedCoralGolem;
        public static bool downedAkumo;
        public static bool downedHydros;
        public static bool downedOmen;
        public static bool downedKraken;
        public static bool omenPath;
        public static Vector2[] reefMinibiomes = new Vector2[6];

        // private static List<Point> BiomeCenters;
        public static Vector2 yes;

        public static Vector2 ree;

        [FieldInit(FieldInitType.SubType, typeof(List<Vector2>))]
        public static IList<Vector2> EntracesPosses = new List<Vector2>();

        [FieldInit(FieldInitType.ArrayIntialization, 6)]
        public static byte[] LightStates = new byte[6];

        [FieldInit(FieldInitType.ArrayIntialization, 100)]
        public static Vector2[] PylonBegin = new Vector2[100];

        [FieldInit(FieldInitType.ArrayIntialization, 100)]
        public static Vector2[] PylonEnd = new Vector2[100];

        [FieldInit(FieldInitType.ArrayIntialization, 10000)]
        public static Vector2[] sinDis = new Vector2[10000];

        //public Vector2[] sinDis = new Vector2[10000];
        public override void Initialize()
        {
            ModContent.GetInstance<EEMod>().TVH.Clear();
            if (sinDis != null)
            {
                for (int i = 0; i < sinDis.Length; i++)
                {
                    // When an array is created all elements get initialized to the default value, and the default value of structs isn't null
                    sinDis[i].X = WorldGen.genRand.NextFloat(0, 0.03f);
                }
            }
            eocFlag = NPC.downedBoss1;
            if (EntracesPosses != null)
            {
                if (EntracesPosses.Count > 0)
                {
                    yes = EntracesPosses[0];
                }
            }
            Main.LocalPlayer.GetModPlayer<EEPlayer>().isInSubworld = Main.ActiveWorldFileData.Path.Contains($@"{Main.SavePath}\Worlds\{Main.LocalPlayer.GetModPlayer<EEPlayer>().baseWorldName}Subworlds");
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Endless Escapade Ores", EEModOres));
            }
            int MicroBiomes = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            int LivingTreesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
            /*if (LivingTreesIndex != -1)
            {
                tasks.Insert(LivingTreesIndex + 1, new PassLegacy("Post Terrain", delegate (GenerationProgress progress)
                {
                    progress.Message = "Generating structures";
                    for (int l = 0; l < 30; l++)
                    {
                        int posX = WorldGen.genRand.Next(0, Main.maxTilesX);
                        int posY = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY);
                        PlaceRuins(posX, posY, ruinsShape);
                    }
                }));
            }*/
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.WriteVector2(ree);
            writer.WriteVector2(yes);
            for (int i = 0; i < LightStates.Length; i++)
            {
                writer.Write(LightStates[i]);
            }
        }

        public override void NetReceive(BinaryReader reader)
        {
            ree = reader.ReadVector2();
            yes = reader.ReadVector2();
            for (int i = 0; i < LightStates.Length; i++)
            {
                LightStates[i] = reader.ReadByte();
            }
        }

        public override void PostWorldGen()
        {
            DoAndAssignShrineValues();
            DoAndAssignShipyardValues();
            for (int i = 0; i < sinDis.Length; i++)
            {
                sinDis[i].X = Main.rand.NextFloat(0, 0.03f);
            }
        }

        public void DrawVines()
        {
            if (EESubWorlds.ChainConnections.Count > 0)
            {
                for (int i = 1; i < EESubWorlds.ChainConnections.Count - 2; i++)
                {
                    sinDis[i].Y += sinDis[i].X;
                    Vector2 addOn = new Vector2(0, 8);
                    Vector2 ChainConneccPos = EESubWorlds.ChainConnections[i] * 16;
                    Vector2 LastChainConneccPos = EESubWorlds.ChainConnections[i - 1] * 16;
                    Tile CurrentTile = Main.tile[(int)EESubWorlds.ChainConnections[i].X, (int)EESubWorlds.ChainConnections[i].Y];
                    Tile LastTile = Main.tile[(int)EESubWorlds.ChainConnections[i - 1].X, (int)EESubWorlds.ChainConnections[i - 1].Y];
                    bool isValid = CurrentTile.active() && LastTile.active() && Main.tileSolid[CurrentTile.type] && Main.tileSolid[LastTile.type];
                    Vector2 MidNorm = (ChainConneccPos + LastChainConneccPos) / 2;
                    Vector2 Mid = (ChainConneccPos + LastChainConneccPos) / 2 + new Vector2(0, 50 + (float)(Math.Sin(sinDis[i].Y) * 30));
                    Vector2 lerp1 = Vector2.Lerp(ChainConneccPos, LastChainConneccPos, 0.2f);
                    Vector2 lerp2 = Vector2.Lerp(ChainConneccPos, LastChainConneccPos, 0.8f);
                    if (MidNorm.Y > 100 * 16 && Vector2.DistanceSquared(ChainConneccPos, LastChainConneccPos) < 40 * 16 * 40 * 16 && Vector2.DistanceSquared(Main.LocalPlayer.Center, MidNorm) < 2000 * 2000 && isValid && Collision.CanHit(lerp1, 1, 1, lerp2, 1, 1))
                    {
                        Color chosen = Color.Lerp(Color.Yellow, Color.LightGoldenrodYellow, Main.rand.NextFloat(1f));
                        Helpers.DrawBezier(ModContent.GetInstance<EEMod>().GetTexture("Projectiles/Vine"), Color.White, ChainConneccPos, LastChainConneccPos, Mid, 0.6f, MathHelper.PiOver2, true);
                        Helpers.DrawBezier(ModContent.GetInstance<EEMod>().GetTexture("Projectiles/VineLight"), Color.White, ChainConneccPos + addOn, LastChainConneccPos + addOn, Mid + addOn, 8f, MathHelper.PiOver2,true);
                        Helpers.DrawBezier(ModContent.GetInstance<EEMod>().GetTexture("Projectiles/VineLightGlow"), Color.White * Math.Abs((float)Math.Sin(Main.GameUpdateCount/200f + ChainConneccPos.X)), ChainConneccPos + addOn, LastChainConneccPos + addOn, Mid + addOn, 8f, MathHelper.PiOver2);
                        Helpers.DrawParticlesAlongBezier(LastChainConneccPos + addOn, ChainConneccPos + addOn, Mid + addOn, 1/8f, chosen,0.005f,(Vector2.UnitY).RotatedBy(Main.rand.NextFloat(6.24f)), new SlowDown(0.98f), new RotateTexture(Main.rand.NextFloat(-0.03f, 0.03f)), new SetMask(ModContent.GetInstance<EEMod>().GetTexture("Masks/RadialGradient")), new AfterImageTrail(1f), new RotateVelocity(Main.rand.NextFloat(-0.12f, 0.12f)), new SetLighting(chosen.ToVector3(), 0.3f), new ZigzagMotion(40f,3f));
                    }
                }
            }
        }
        

        /*private float HeartBeat;
        public override void PostDrawTiles()
        {
            for (int i = (int)Main.screenPosition.X / 16; i < (Main.screenPosition.X + Main.screenWidth)/16; i++)
            {
                for (int j = (int)Main.screenPosition.Y / 16; j < (Main.screenPosition.Y + Main.screenHeight) / 16; j++)
                {
                    if (Main.tile[i, j].type == ModContent.TileType<AquamarineLamp1>())
                    {
                        int frameX = Main.tile[i, j].frameX;
                        int frameY = Main.tile[i, j].frameY;
                        if (frameX == 0 && frameY == 0)
                        {
                            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                            if (Main.drawToScreen)
                            {
                                zero = Vector2.Zero;
                            }
                            Vector2 position = new Vector2(i * 16 - (int)Main.screenPosition.X, (j - 1) * 16 - (int)Main.screenPosition.Y) + zero;
                            Texture2D texture = ModContent.GetInstance<EEMod>().GetTexture("Tiles/Furniture/Coral/AquamarineLamp1Glow");

                            float timeBetween = 70;
                            float bigTimeBetween = 200;
                            if (Main.GameUpdateCount % 200 < timeBetween)
                            {
                                HeartBeat = Math.Abs((float)Math.Sin((Main.GameUpdateCount % bigTimeBetween) * (6.28f / timeBetween))) * (1 - (Main.GameUpdateCount % bigTimeBetween) / (timeBetween * 1.5f));
                            }
                            else
                            {
                                HeartBeat = 0;
                            }
                            Main.spriteBatch.Begin();
                            Main.spriteBatch.Draw(texture, position + new Vector2(0, 2 * (float)Math.Sin(Main.GameUpdateCount / 10) - 4), texture.Bounds, Color.White * ((HeartBeat / 2) + 0.5f), 0f, default, 1f, SpriteEffects.None, 0f);
                            Main.spriteBatch.End();
                        }
                    }
                }
            }
        }*/

        public override void PostUpdate()
        {
            if (NPC.downedBoss1)
            {
                if (!eocFlag)
                {
                    eocFlag = true;
                    Main.NewText("You hear a strong wind erupting from the desert...", 228, 171, 72);
                    StartSandstorm();
                }
            }
        }

        public static Vector2 SubWorldSpecificCoralBoatPos;
        public static Vector2 SubWorldSpecificVolcanoInsidePos = new Vector2(198, 189);

        public static int customBiome = 0;
        public static bool eocFlag;

        public static bool shipComplete;

        public static List<Vector2> missingShipTiles = new List<Vector2>();
        public static List<Texture2D> missingShipTilesItems = new List<Texture2D>();
        public static List<Vector2> missingShipTilesRespectedPos = new List<Vector2>();

        public static void ShipComplete()
        {
            /*if (ree == Vector2.Zero)
            {
                ShipTilePosX = 100;
                ShipTilePosY = TileCheckWater(100) - 22;
            }
            for (int i = ShipTilePosX; i < ShipTilePosX + ShipTiles.GetLength(1); i++)
            {
                for (int j = ShipTilePosY; j < ShipTilePosY + ShipTiles.GetLength(0); j++)
                {
                    if (WorldGen.InWorld(i - 3, i - 6, 32))
                    {
                        Tile tile = Framing.GetTileSafely(i - 3, j - 6);
                        int expectedType;
                        switch (ShipTiles[j - ShipTilePosY, i - ShipTilePosX])
                        {
                            case 1:
                                expectedType = TileID.WoodBlock;
                                if (tile.type != expectedType)
                                {
                                    missingShipTilesItems.Add(Main.itemTexture[ItemID.Wood]);
                                    missingShipTilesRespectedPos.Add(new Vector2(i, j));
                                }
                                break;

                            case 2:
                                expectedType = TileID.RichMahogany;
                                if (tile.type != expectedType)
                                {
                                    missingShipTilesItems.Add(Main.itemTexture[ItemID.RichMahogany]);
                                    missingShipTilesRespectedPos.Add(new Vector2(i, j));
                                }
                                break;

                            case 3:
                                expectedType = TileID.GoldCoinPile;
                                if (tile.type != expectedType)
                                {
                                    missingShipTilesItems.Add(Main.itemTexture[ItemID.GoldCoin]);
                                    missingShipTilesRespectedPos.Add(new Vector2(i, j));
                                }
                                break;

                            case 4:
                                expectedType = TileID.Platforms;
                                if (tile.type != expectedType)
                                {
                                    missingShipTilesItems.Add(Main.itemTexture[ItemID.WoodPlatform]);
                                    missingShipTilesRespectedPos.Add(new Vector2(i, j));
                                }
                                break;

                            case 5:
                                expectedType = TileID.WoodenBeam;
                                if (tile.type != expectedType)
                                {
                                    missingShipTilesItems.Add(Main.itemTexture[ItemID.WoodenBeam]);
                                    missingShipTilesRespectedPos.Add(new Vector2(i, j));
                                }
                                break;

                            case 6:
                                expectedType = TileID.SilkRope;
                                if (tile.type != expectedType)
                                {
                                    missingShipTilesItems.Add(Main.itemTexture[ItemID.SilkRope]);
                                    missingShipTilesRespectedPos.Add(new Vector2(i, j));
                                }
                                break;

                            default:
                                expectedType = 0;
                                if (tile.type != expectedType)
                                {
                                    missingShipTilesItems.Add(ModContent.GetInstance<EEMod>().GetTexture("Empty"));
                                    missingShipTilesRespectedPos.Add(new Vector2(i, j));
                                }
                                break;
                        }
                        if (tile.type != expectedType)
                        {
                            missingShipTiles.Add(new Vector2(i, j));
                        }
                    }
                }
            }*/

            //int? nullableReeX = (int)ree.X; // not a null value
            //int? nullableReeY = (int)ree.Y;
            int ShipTilePosX = (int)ree.X;// ?? 100;
            int ShipTilePosY = (int)ree.Y;// ?? TileCheckWater(100) - 22;

            bool hasSteeringWheel = false;
            int numberOfTiles = 0;
            for (int i = ShipTilePosX; i < ShipTilePosX + ShipTiles.GetLength(1); i++)
            {
                for (int j = ShipTilePosY; j < ShipTilePosY + ShipTiles.GetLength(0); j++)
                {
                    if (Main.tile[i, j].active() == true)
                    {
                        numberOfTiles++;
                    }
                    if (Main.tile[i, j].type == ModContent.TileType<WoodenShipsWheelTile>())
                    {
                        hasSteeringWheel = true;
                    }
                }
            }

            if (hasSteeringWheel && numberOfTiles > 100)
            {
                shipComplete = true;
            }
            else
            {
                shipComplete = false;
            }
        }

        public static bool HydrosCheck()
        {
            if (instance.minionsKilled >= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static IList<Vector2> Vines = new List<Vector2>();
        public override void Load(TagCompound tag)
        {
            tag.TryGetListRef("EntracesPosses", ref EntracesPosses);
            tag.TryGetRef("CoralBoatPos", ref EESubWorlds.CoralBoatPos);
            tag.TryGetRef("SubWorldSpecificVolcanoInsidePos", ref SubWorldSpecificVolcanoInsidePos);
            tag.TryGetRef("yes", ref yes);
            tag.TryGetRef("ree", ref ree);
            tag.TryGetRef("SpirePosition", ref EESubWorlds.SpirePosition);
            tag.TryGetListRef("ChainConnections", ref EESubWorlds.ChainConnections);
            tag.TryGetListRef("OrbPositions", ref EESubWorlds.OrbPositions);
            tag.TryGetListRef("AquamarineZiplineLocations", ref EESubWorlds.AquamarineZiplineLocations);
            tag.TryGetListRef("BulbousTreePosition", ref EESubWorlds.BulbousTreePosition);
            tag.TryGetListRef("WebPositions", ref EESubWorlds.WebPositions);
            foreach (Vector2 vec in EESubWorlds.WebPositions)
            {
                for (int i = 0; i < 18; i++)
                    EEMod.primitives.CreateTrail(new WebPrimTrail(null,vec*16, i));
            }
            if (tag.TryGetListRef("SwingableVines", ref VerletHelpers.SwingableVines))
            {
                if (VerletHelpers.SwingableVines.Count != 0)
                {
                    foreach (Vector2 vec in VerletHelpers.SwingableVines)
                    {
                        VerletHelpers.AddStickChainNoAdd(ref ModContent.GetInstance<EEMod>().verlet, vec, Main.rand.Next(5, 15), 27);
                    }
                }
            }
            if (tag.TryGetList<Vector2>("EmptyTileVectorMain", out IList<Vector2> vecMains) && tag.TryGetList<Vector2>("EmptyTileVectorSub", out IList<Vector2> list2))
            {
                EmptyTileEntityCache.EmptyTilePairs = vecMains.Zip(list2, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);

            }
            if (tag.TryGetList<Vector2>("EmptyTileVectorEntities", out var VecMains) && tag.TryGetList<EmptyTileDrawEntity>("EmptyTileEntities", out var VecSubs))
            {
                EmptyTileEntityCache.EmptyTileEntityPairs = VecMains.Zip(VecSubs, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
            }
            if (tag.TryGetListRef<Vector2>("CoralCrystalPosition", ref EESubWorlds.CoralCrystalPosition))
            {
                // for (int i = 0; i < EESubWorlds.CoralCrystalPosition.Count; i++)
                //    EmptyTileEntityCache.AddPair(new Crystal(EESubWorlds.CoralCrystalPosition[i]), EESubWorlds.CoralCrystalPosition[i], EmptyTileArrays.CoralCrystal);
            }
            tag.TryGetByteArrayRef("LightStates", ref LightStates);
            if (tag.TryGetList("ReefMinibiomePositions", out IList<Vector2> list) && tag.TryGetList("ReefMinibiomeTypes", out IList<int> types))
            {
                List<Vector3> templist = new List<Vector3>(list.Count);
                for (int i = 0; i < list.Count; i++)
                {
                    templist.Add(new Vector3(list[i], types[i]));
                }
                EESubWorlds.MinibiomeLocations = templist;
            }
            if (tag.TryGetList<string>("boolFlags", out var flags))
            {
                // Downed bosses
                downedAkumo = flags.Contains("downedAkumo");
                downedHydros = flags.Contains("downedHydros");
                downedKraken = flags.Contains("downedKraken");
                downedCoralGolem = flags.Contains("downedCoralGolem");
                downedOmen = flags.Contains("downedOmen");
                downedTalos = flags.Contains("downedTalos");
            }
        }

        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            if (Main.ActiveWorldFileData.Name == KeyID.CoralReefs)
            {
                tag["CoralBoatPos"] = EESubWorlds.CoralBoatPos;
                tag["ChainConnections"] = EESubWorlds.ChainConnections;
                tag["AquamarineZiplineLocations"] = EESubWorlds.AquamarineZiplineLocations;
                tag["OrbPositions"] = EESubWorlds.OrbPositions;
                tag["BulbousTreePosition"] = EESubWorlds.BulbousTreePosition;
                tag["WebPositions"] = EESubWorlds.WebPositions;
                tag["SwingableVines"] = VerletHelpers.SwingableVines;
                tag["LightStates"] = LightStates;
                tag["CoralCrystalPosition"] = EESubWorlds.CoralCrystalPosition;
                tag["SpirePosition"] = EESubWorlds.SpirePosition;
                tag["EmptyTileVectorMain"] = EmptyTileEntityCache.EmptyTilePairs.Keys.ToList();
                tag["EmptyTileVectorSub"] = EmptyTileEntityCache.EmptyTilePairs.Values.ToList();
                tag["EmptyTileVectorEntities"] = EmptyTileEntityCache.EmptyTileEntityPairs.Keys.ToList();
                tag["EmptyTileEntities"] = EmptyTileEntityCache.EmptyTileEntityPairs.Values.ToList();

                if (EESubWorlds.MinibiomeLocations.Count > 0)
                {
                    Vector2[] ReefMinibiomePositions = new Vector2[EESubWorlds.MinibiomeLocations.Count];
                    int[] ReefMinibiomeTypes = new int[EESubWorlds.MinibiomeLocations.Count];
                    for (int i = 0; i < EESubWorlds.MinibiomeLocations.Count; i++)
                    {
                        ReefMinibiomePositions[i] = new Vector2(EESubWorlds.MinibiomeLocations[i].X, EESubWorlds.MinibiomeLocations[i].Y);
                        ReefMinibiomeTypes[i] = (int)EESubWorlds.MinibiomeLocations[i].Z;
                    }
                    tag["ReefMinibiomePositions"] = ReefMinibiomePositions.ToList();
                    tag["ReefMinibiomeTypes"] = ReefMinibiomeTypes.ToList();
                }
            }
            if (Main.ActiveWorldFileData.Name == KeyID.VolcanoInside)
            {
                tag["SubWorldSpecificVolcanoInsidePos"] = SubWorldSpecificVolcanoInsidePos;
            }
            tag["EntracesPosses"] = EntracesPosses;
            tag["yes"] = yes;
            tag["ree"] = ree;
            IList<string> flags = new List<string>();
            if (downedAkumo) flags.Add("downedAkumo");
            if (downedHydros) flags.Add("downedHydros");
            if (downedKraken) flags.Add("downedKraken");
            if (downedCoralGolem) flags.Add("downedCoralGolem");
            if (downedOmen) flags.Add("downedOmen");
            if (downedTalos) flags.Add("downedTalos");
            return tag;
        }
    }
}
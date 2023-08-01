﻿using EEMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Audio;
using EEMod.NPCs.Goblins.Shaman;
using Microsoft.Xna.Framework.Graphics;
using EEMod.NPCs.Goblins.Bard;
using EEMod.NPCs.Goblins.Berserker;
using EEMod.NPCs.Goblins;
using Terraria.GameContent.ObjectInteractions;

namespace EEMod.Tiles.Furniture.Chests
{
    public class ShadowflameHexChestTile : EETile
    {
        public override void SetStaticDefaults()
        {
			Main.tileSpelunker[Type] = true;
			Main.tileContainer[Type] = true;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 1200;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileOreFinderPriority[Type] = 500;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.BasicChest[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			DustType = DustID.CrystalSerpent_Pink;
			AdjTiles = new int[] { TileID.Containers };
			ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ItemID.DirtBlock;

			// Names
			ContainerName/* tModPorter Note: Removed. Override DefaultContainerName instead */.SetDefault("Shadowflame Hex Chest");

			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Shadowflame Hex Chest");
			AddMapEntry(new Color(200, 200, 200), name, MapChestName);

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[] { TileID.MagicalIceBlock };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
		}

		public override ushort GetMapOption(int i, int j) => 0;

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override bool IsLockedChest(int i, int j) => false;

		public override bool UnlockChest(int i, int j, ref short TileFrameXAdjustment, ref int dustType, ref bool manual)
		{
			if (Main.dayTime)
			{
				return false;
			}

			DustType = dustType;
			return true;
		}

		public static string MapChestName(string name, int i, int j)
		{
			int left = i;
			int top = j;
			Tile tile = Framing.GetTileSafely(i, j);
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}

			if (tile.TileFrameY != 0)
			{
				top--;
			}

			int chest = Chest.FindChest(left, top);
			if (chest < 0)
			{
				return Language.GetTextValue("LegacyChestType.0");
			}

			if (Main.chest[chest].name == "")
			{
				return name;
			}

			return name + ": " + Main.chest[chest].name;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = 1;

		public override void KillMultiTile(int i, int j, int TileFrameX, int TileFrameY)
		{
			Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */);
			Chest.DestroyChest(i, j);
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Framing.GetTileSafely(i, j);
			Main.mouseRightRelease = false;
			int left = i;
			int top = j;
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}

			if (tile.TileFrameY != 0)
			{
				top--;
			}

			if (player.sign >= 0)
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				player.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
			}

			if (Main.editChest)
			{
				SoundEngine.PlaySound(SoundID.MenuTick);
				Main.editChest = false;
				Main.npcChatText = "";
			}

			if (player.editedChestName)
			{
				NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
				player.editedChestName = false;
			}

			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				if (left == player.chestX && top == player.chestY && player.chest >= 0)
				{
					player.chest = -1;
					Recipe.FindRecipes();
					SoundEngine.PlaySound(SoundID.MenuClose);
				}
				else
				{
					NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, top);
					Main.stackSplit = 600;
				}
			}
			else
			{
				int chest = Chest.FindChest(left, top);

				bool allGoblinsDead = true;

				for (int index = 0; index < Main.maxNPCs; index++)
				{
					if (((Main.npc[index].type == ModContent.NPCType<GoblinShaman>() ||
						  Main.npc[index].type == ModContent.NPCType<CymbalBard>() ||
						  Main.npc[index].type == ModContent.NPCType<PanfluteBard>() ||
						  Main.npc[index].type == ModContent.NPCType<PercussionBard>() ||
						  Main.npc[index].type == ModContent.NPCType<GoblinBerserker>()
						) && Main.npc[index].active))
					{
						allGoblinsDead = false;
					}
				}

				for (int index = 0; index < Main.maxProjectiles; index++)
				{
					if (((Main.projectile[index].type == ModContent.ProjectileType<GoblinDeathBolt>()) && Main.projectile[index].active))
					{
						allGoblinsDead = false;
					}
				}

				if (chest >= 0 && allGoblinsDead)
				{
					Main.stackSplit = 600;
					if (chest == player.chest)
					{
						player.chest = -1;
						SoundEngine.PlaySound(SoundID.MenuClose);
					}
					else
					{
						player.chest = chest;
						Main.playerInventory = true;
						Main.recBigList = false;
						player.chestX = left;
						player.chestY = top;
						SoundEngine.PlaySound(player.chest < 0 ? SoundID.MenuOpen : SoundID.MenuTick);
					}

					Recipe.FindRecipes();
				}
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Framing.GetTileSafely(i, j);
			int left = i;
			int top = j;
			if (tile.TileFrameX % 36 != 0)
			{
				left--;
			}

			if (tile.TileFrameY != 0)
			{
				top--;
			}

			int chest = Chest.FindChest(left, top);
			if (chest < 0)
			{
				player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
			}
			else
			{
				player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Shadowflame Hex Chest";
				if (player.cursorItemIconText == "Shadowflame Hex Chest")
				{
					player.cursorItemIconID = ItemID.DirtBlock;

					player.cursorItemIconText = "";
				}
			}

			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			MouseOver(i, j);
			Player player = Main.LocalPlayer;
			if (player.cursorItemIconText == "")
			{
				player.cursorItemIconEnabled = false;
				player.cursorItemIconID = 0;
			}
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

			Chest c = Main.chest[Chest.FindChest(i - (int)(Framing.GetTileSafely(i, j).TileFrameX / 18f), j - (int)(Framing.GetTileSafely(i, j).TileFrameY / 18f))];

			spriteBatch.Draw(ModContent.Request<Texture2D>("EEMod/Tiles/Furniture/Chests/ShadowflameHexChestTileGlow").Value,
				new Vector2((i * 16), (j * 16)) - Main.screenPosition + zero, new Rectangle(Framing.GetTileSafely(i, j).TileFrameX, Main.tile[i, j].TileFrameY + (c.frame * 38), 18, 20), Color.White);

			bool allGoblinsDead = true;

			for (int index = 0; index < Main.maxNPCs; index++)
			{
				if (((Main.npc[index].type == ModContent.NPCType<GoblinShaman>() ||
					  Main.npc[index].type == ModContent.NPCType<CymbalBard>() ||
					  Main.npc[index].type == ModContent.NPCType<PanfluteBard>() ||
					  Main.npc[index].type == ModContent.NPCType<PercussionBard>() ||
					  Main.npc[index].type == ModContent.NPCType<GoblinBerserker>()

					) && Main.npc[index].active))
				{
					allGoblinsDead = false;
				}
			}

			if (Main.tile[i, j].TileFrameX == 0 && Main.tile[i, j].TileFrameY == 0 && !allGoblinsDead)
            {
                spriteBatch.Draw(ModContent.Request<Texture2D>("EEMod/Tiles/Furniture/Chests/ShadowflameHexChestLock").Value,
					new Vector2((i * 16) + 16, (j * 16) - 20) - Main.screenPosition + zero, null, Color.White, 0f, new Vector2(10, 12), 1f, SpriteEffects.None, 0f);
            }
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Prophecy.NPCs
{

    public class Ragwhirl : ModNPC
    {
        private int aiPhase;
        private bool transition;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ragwhirl");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.alpha = 100;
            npc.aiStyle = -1;
            npc.lifeMax = Main.expertMode ? 90 : 90;    //this is the npc health
            npc.damage = Main.expertMode ? 15 : 25;  //this is the npc damage
            npc.defense = 6;         //this is the npc defense
            npc.knockBackResist = 1.5f;
            npc.width = 28; //this is where you put the npc sprite width.     important
            npc.height = 47; //this is where you put the npc sprite height.   important
            npc.boss = false;
            npc.lavaImmune = true;       //this make the npc immune to lava
            npc.noGravity = true;           //this make the npc float
            npc.noTileCollide = true;        //this make the npc go thru walls
            npc.HitSound = SoundID.NPCHit23;
            npc.DeathSound = SoundID.NPCDeath39;
            npc.behindTiles = false;

            npc.value = Item.buyPrice(0, 0, 5, 0);
            npc.npcSlots = 1f;
            npc.netAlways = true;
            aiPhase = 1;
            //npc.music = (this.music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Dionysus"));
        }
        public int Timer;
        public override void AI() 
        {
            npc.TargetClosest(false);

            Player player = Main.player[npc.target];
            Vector2 moveTo = player.Center; 

            npc.TargetClosest(true);
            float speed = 2.5f;
            Vector2 move = moveTo - npc.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 10f; 
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;

            npc.rotation = npc.velocity.X / 10f;
        }
        public override void FindFrame(int frameHeight) //Frame counter
        {
            npc.spriteDirection = 1;
            if (npc.velocity.X > 0)
            {
                npc.spriteDirection = -1;
            }
                if (npc.frameCounter++ > 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = npc.frame.Y + frameHeight;
                }
                if (npc.frame.Y >= frameHeight * 3)
                {
                    npc.frame.Y = 0;
                    return;
                }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss1 == true)
            {
                return SpawnCondition.SandstormEvent.Chance * 0.5f;
            }
            else
            {
                return SpawnCondition.SandstormEvent.Chance * 0f;
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1f;
            return null;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(0) == 0 && NPC.downedBoss1)
            {
                // this is still pretty useless to do
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MummifiedRag"), Main.rand.Next(0, 2));
            }
        }



    }
}

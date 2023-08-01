using EEMod.Extensions;
using EEMod.Prim;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EEMod.NPCs.Goblins.Scrapwizard
{
    public class SmallScrap : EEProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scrap");
        }

        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 16;

            Projectile.alpha = 0;

            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.scale = 1f;

            Projectile.aiStyle = -1;

            Projectile.tileCollide = true;

            Projectile.damage = 20;

            Projectile.timeLeft = 1000000000;

            Projectile.hide = true;
        }

        public Vector2 offset;
        public Vector2 desiredPosition;

        public float initRotation;
        public float desiredRotation;

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Lighting.GetColor((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f));
            return true;
        }

        public override void AI()
        {
            if (desiredPosition != Vector2.Zero)
            {
                Projectile.Center = desiredPosition + offset.RotatedBy(desiredRotation);
                Projectile.rotation = desiredRotation + initRotation;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
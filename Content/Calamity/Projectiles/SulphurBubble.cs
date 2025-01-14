﻿using CalamityMod.Projectiles.Magic;
using FargowiltasCrossmod.Content.Calamity.Items;
using FargowiltasCrossmod.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace FargowiltasCrossmod.Content.Calamity.Projectiles
{
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    public class SulphurBubble : ModProjectile
    {
        
        public override bool IsLoadingEnabled(Mod mod)
        {
            return FargowiltasCrossmod.EnchantLoadingEnabled;
        }
        public override string Texture => "CalamityMod/Projectiles/Enemy/SulphuricAcidBubble";
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            Main.projFrames[Type] = 7;
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.scale = 3;
            Projectile.Opacity = 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> t = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(t.Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 30 * Projectile.frame, 30, 30), lightColor * Projectile.Opacity, 0, new Vector2(15, 15), Projectile.scale, SpriteEffects.None);
            return false;
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 6)
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.Opacity < 0.7f) Projectile.Opacity += 0.01f;
            Projectile.velocity.Y = MathHelper.Lerp(Projectile.velocity.Y, -4, 0.01f);

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i] != null && Main.projectile[i].active && Main.projectile[i].damage > 0 && Main.projectile[i].GetGlobalProjectile<CalamityAddonGlobalProjectile>() != null && (Main.projectile[i].GetGlobalProjectile<CalamityAddonGlobalProjectile>().HitBubble.Count <= 0 || !Main.projectile[i].GetGlobalProjectile<CalamityAddonGlobalProjectile>().HitBubble.Contains(i)) && i != Projectile.whoAmI && Main.projectile[i].Hitbox.Intersects(Projectile.Hitbox) && Main.projectile[i].type != ModContent.ProjectileType<MiasmaGas>())
                {
                    Main.projectile[i].GetGlobalProjectile<CalamityAddonGlobalProjectile>().HitBubble.Add(Projectile.whoAmI);
                    for (int j = 0; j < 1; j++)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, new Vector2(0, Main.rand.NextFloat(3, 6)).RotatedByRandom(MathHelper.TwoPi), ModContent.ProjectileType<MiasmaGas>(), 20, 1, Projectile.owner);
                    }
                }
            }
        }
    }
}

using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Legendary_TBallFists
{
	class ThunderballExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voltaic Blast");
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 20;
			projectile.height = 50;
			projectile.width = 50;

			projectile.alpha = 255;
			aiType = ProjectileID.Bullet;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("ShockDebuff"), Main.rand.Next(240, 320));
            }
		}
	}
}

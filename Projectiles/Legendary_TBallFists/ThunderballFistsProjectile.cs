using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Legendary_TBallFists
{
	class ThunderballFistsProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voltaic Blast");
		}
		bool secondphase = false;
		int timer;
		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 3;
			projectile.timeLeft = 1000;
			projectile.height = 10;
			projectile.width = 4;

			projectile.alpha = 255;
			aiType = ProjectileID.Bullet;
			projectile.extraUpdates = 6;
		}

		public override void AI()
		{
			if (secondphase)
			{
				projectile.penetrate = -1;
				projectile.velocity.Y *= .96f;
				for (int num447 = 0; num447 < 2; num447++)
				{
					Vector2 vector33 = projectile.position;
					vector33 -= projectile.velocity * ((float)num447 * 0.25f);
					projectile.alpha = 255;
					int num448 = Dust.NewDust(vector33, 1, 1, 226, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.25f);
					Main.dust[num448].noGravity = true;
					Main.dust[num448].position = vector33;
					Main.dust[num448].scale = (float)Main.rand.Next(60, 80) * 0.013f;
					Main.dust[num448].velocity.X *= .095f;
				}
				timer++;
				if(timer >= 240)
				{
					projectile.Kill();
				}
			}
			else
			{
				for (int num447 = 0; num447 < 8; num447++)
				{
					Vector2 vector33 = projectile.position;
					vector33 -= projectile.velocity * ((float)num447 * 0.25f);
					projectile.alpha = 255;
					int num448 = Dust.NewDust(vector33, 1, 1, 226, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.25f);
					Main.dust[num448].noGravity = true;
					Main.dust[num448].position = vector33;
					Main.dust[num448].scale = (float)Main.rand.Next(10, 60) * 0.013f;
					Main.dust[num448].velocity *= 0;
				}
			}
			return;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(!secondphase)
			{
			 Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("ThunderballExplosion"), (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f);
			}
			secondphase = true;
			projectile.velocity.Y = -2f;
			projectile.velocity.X = 0f;
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(mod.BuffType("ShockDebuff"), Main.rand.Next(240, 320));
            }
            int d = 226;
            int d1 = 226;
            for (int num623 = 0; num623 < 35; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 226, 0f, 0f, 100, default(Color), .75f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 226, 0f, 0f, 100, default(Color), .25f);
				Main.dust[num624].velocity *= 2f;
			}
             
                   
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if(!secondphase)
			{
							Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
			 Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("ThunderballExplosion"), (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f);
			}
			secondphase = true;
			projectile.velocity.Y = -2f;
			projectile.velocity.X = 0f;
            int d = 226;
            int d1 = 226;
        	for (int num623 = 0; num623 < 35; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0f, 0f, 100, default(Color), .75f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0f, 0f, 100, default(Color), .25f);
				Main.dust[num624].velocity *= 2f;
			}
             
                    
			return false;
		}
		public override void Kill(int timeLeft)
		{
            int d = 226;
            int d1 = 226;
            for (int num623 = 0; num623 < 35; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0f, 0f, 100, default(Color), .85f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0f, 0f, 100, default(Color), .55f);
				Main.dust[num624].velocity *= 2f;
				Main.dust[num624].noGravity = true;
			}
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("ThunderballExplosion"), (int)(projectile.damage * 1.4f), 0, projectile.owner, 0f, 0f);
     		Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);   
        }
	}
}

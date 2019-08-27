using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Tediore.SMGWhite
{
	public class SubMG_TedioreProj : ModProjectile
	{		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subcompact MG");
	    	 ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}


		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			projectile.width = 20;
			projectile.height = 50;
            projectile.penetrate = 3;
			projectile.ranged = true;
            projectile.extraUpdates = 1;
			projectile.timeLeft = 1200;
			projectile.friendly = true;

		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
            return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
				int d = 263;
				for (int k = 0; k < 6; k++)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -.5f, 0, Color.White, 0.4f);

				}

				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -.5f, 0, Color.White, 0.7f);
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
				projectile.Kill();
			else
			{
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X)
					projectile.velocity.X = -oldVelocity.X;

				if (projectile.velocity.Y != oldVelocity.Y)
					projectile.velocity.Y = -oldVelocity.Y;

				projectile.velocity *= 0.75f;
			}
			return false;
		}
		public override void AI()
		{
            projectile.velocity *= .99f;
			projectile.rotation = projectile.velocity.ToRotation() + 1.57F;
                    for (int k = 0; k < 6; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 263);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 0f;
                            Main.dust[dust].scale = .6f;
                            Main.dust[dust].noLight = true;
                        }
	
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
			for (int num623 = 0; num623 < 15; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 263, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 1.5f;
                Main.dust[num624].scale = .6f;
                Main.dust[num624].noLight = true;
				num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 263, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 1f;
                Main.dust[num624].scale = .6f;
				Main.dust[num624].noGravity = true;
                Main.dust[num624].noLight = true;
			}
		}
	}
}

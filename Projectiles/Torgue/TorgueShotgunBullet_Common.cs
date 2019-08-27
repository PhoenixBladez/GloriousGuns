using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Torgue
{
	public class TorgueShotgunBullet_Common : ModProjectile
	{		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("EXPLOSION!");
	    	 ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}


		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.ranged = true;
            projectile.extraUpdates = 1;
			projectile.timeLeft = 1200;
			projectile.alpha = 255;
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
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + 1.57F;
			Lighting.AddLight(projectile.position,0.4f, 0.12f, 0.06f);
                    for (int k = 0; k < 3; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 87);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 0f;
                            Main.dust[dust].scale = .58f;
                        }
	
		}
		public override void Kill(int timeLeft)
		{
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("TorgueExplosion"), projectile.damage, 0, projectile.owner, 0f, 0f);
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
			for (int num623 = 0; num623 < 15; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 87, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 1.5f;
                Main.dust[num624].scale = .58f;
				num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 87, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 1f;
                Main.dust[num624].scale = .58f;
				Main.dust[num624].noGravity = true;
			}

			for (int num625 = 0; num625 < 2; num625++)
			{
				float scaleFactor10 = 0.33f;
				if (num625 == 1)
					scaleFactor10 = 0.66f;
				else if (num625 == 2)
					scaleFactor10 = 1f;

				int num626 = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Main.gore[num626].velocity.X += 1f;
				Main.gore[num626].velocity.Y += 1f;
				num626 = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[num626].velocity *= scaleFactor10;
				Main.gore[num626].velocity.X -= 1f;
				Main.gore[num626].velocity.Y += 1f;
			}
		}
	}
}

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles
{
	public class BasicBullet_Proj : ModProjectile
	{		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Standard Round");
	    	 ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}


		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 12;
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
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) * 4 / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, Color.White, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
            return true;
		}
		public override bool PreAI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + 1.57F;
			Lighting.AddLight(projectile.position, .4f, .4f, .4f);
            return true;
		}
	}
}

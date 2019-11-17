using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Vladof
{
	public class TaserZap : ModProjectile
	{		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zap");
	    	 ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}


		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 12;
			projectile.ranged = true;
            projectile.extraUpdates = 1;
			projectile.alpha = 255;
			projectile.extraUpdates = 20;
			projectile.timeLeft = 120;
			projectile.friendly = true;

		}
		public override bool PreAI()
		{
            int num = 5;
			for (int k = 0; k < 3; k++)
			{
				int index2 = Dust.NewDust(projectile.position, 1, 1, 226, 0.0f, 0.0f, 0, new Color(), 1f);
				Main.dust[index2].position = projectile.Center - projectile.velocity / num * (float)k;
				Main.dust[index2].scale = .5f;
				Main.dust[index2].velocity *= 0f;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].noLight = false;	
			}		
            return true;
		}
	}
}

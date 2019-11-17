using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Tediore.Sureshot
{
	public class SureshotMIRV : TedioreMIRV
	{		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sureshot");
	    	 ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}


		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			projectile.width = 30;
			projectile.height = 20;
            projectile.penetrate = 3;
			projectile.ranged = true;
            projectile.extraUpdates = 1;
			projectile.timeLeft = 1200;
			projectile.friendly = true;

		}
	}
}
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Torgue
{
	public class TorgueExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("EXPLOSIONN!");
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.timeLeft = 20;
			projectile.height = 40;
			projectile.penetrate = -1;
            projectile.timeLeft = 5;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.hostile = true;
			projectile.friendly = true;
		}
	}
}

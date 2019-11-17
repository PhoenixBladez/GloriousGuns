using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Legendary_Hornet
{
	public class HornetAcidSplash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acid Pool");
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.timeLeft = 20;
			projectile.height = 40;
			projectile.penetrate = 4;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.hostile = false;
			projectile.friendly = true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
		if (Main.rand.Next(2) == 0)
                {
                    target.AddBuff(mod.BuffType("CorrosiveDebuff"), Main.rand.Next(240, 320));
                }
		}
	}
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Bandit
{

	public class KillingWordProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Killing Word");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.ranged = true;
            projectile.scale = .5f;
			projectile.tileCollide = true;
			projectile.penetrate = 2;
			projectile.timeLeft = 1440;
			projectile.extraUpdates = 1;
            projectile.alpha = 255;
			projectile.ignoreWater = true;
		}
		int dustTimer;
        int counter;
        int d = 0;
		public override void AI()
		{
			counter++;
			dustTimer++;
			if (dustTimer <= 15)
			{
				d = 68;
			}
			if (dustTimer >= 16 && dustTimer <= 30)
			{
				d = 172;
			}
			if (dustTimer >= 31 && dustTimer <= 45)
			{
				d = 173;
			}
			if (dustTimer >= 46)
			{
				d = 242;
			}
			if (dustTimer >= 60)
			{
				dustTimer = 0;
			}
			if (counter >= 1440)
			{
				counter = -1440;
			}
			for (int f = 0; f < 10; f++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)f;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)f;
				
				int num = Dust.NewDust(projectile.Center - new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), 6, 6, d, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
		
			for (int i = 0; i < 16; i++)
			{
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 0f, -2f, 0, default(Color), .8f);
				Main.dust[num].noGravity = true;
				Dust expr_62_cp_0 = Main.dust[num];
				expr_62_cp_0.position.X = expr_62_cp_0.position.X + ((float)(Main.rand.Next(-50, 51) / 20) - 1.5f);
				Dust expr_92_cp_0 = Main.dust[num];
				expr_92_cp_0.position.Y = expr_92_cp_0.position.Y + ((float)(Main.rand.Next(-50, 51) / 20) - 1.5f);
				if (Main.dust[num].position != projectile.Center)
				{
					Main.dust[num].velocity = projectile.DirectionTo(Main.dust[num].position) * 6f;
				}
			}
		}

	}
}
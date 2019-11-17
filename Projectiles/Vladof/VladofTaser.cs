using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Vladof
{
	public class VladofTaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Taser");
		}

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.timeLeft = 1800;
			projectile.ranged = true;
			projectile.friendly = true;

			projectile.penetrate = -1;
		}
		public override bool PreAI()
		{
			Lighting.AddLight(projectile.position, .255f, .054f, .023f);
			if (projectile.ai[0] == 0)
			{
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
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
			}
            else
			{
				projectile.ignoreWater = true;
				projectile.tileCollide = false;
				int num996 = 15;
				bool flag52 = false;
				bool flag53 = false;
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] % 30f == 0f)
					flag53 = true;

				int num997 = (int)projectile.ai[1];
				if (projectile.localAI[0] >= (float)(60 * num996))
					flag52 = true;
				else if (num997 < 0 || num997 >= 200)
					flag52 = true;
				else if (Main.npc[num997].active && !Main.npc[num997].dontTakeDamage)
				{
					projectile.Center = Main.npc[num997].Center - projectile.velocity * 2f;
					projectile.gfxOffY = Main.npc[num997].gfxOffY;
					if (flag53)
					{
						Main.npc[num997].HitEffect(0, 1.0);
					}
				}
				else
					flag52 = true;

				if (flag52)
					projectile.Kill();
			}
            if (displayCircle)
			{
				int num = 5;
				for (int k = 0; k < 3; k++)
					{
						int index2 = Dust.NewDust(projectile.position, 1, 1, 226, 0.0f, 0.0f, 0, new Color(), 1f);
						Main.dust[index2].position = projectile.Center;
						Main.dust[index2].scale = .5f;
						Main.dust[index2].velocity *= 1f;
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = false;	
					}	                
                int[] numArray = new int[20];
                int maxValue = 0;
				float num1 = 300f;
				bool flag = false;
				float num2 = 0.0f;
				float num3 = 0.0f;
				for (int index = 0; index < 200; ++index)
				{
				if (Main.npc[index].CanBeChasedBy((object) this, false))
				{
					float num4 = Main.npc[index].position.X + (float) (Main.npc[index].width / 2);
					float num5 = Main.npc[index].position.Y + (float) (Main.npc[index].height / 2);
					if ((double) (Math.Abs(projectile.position.X + (float) (projectile.width / 2) - num4) + Math.Abs(projectile.position.Y + (float) (projectile.height / 2) - num5)) < (double) num1 && Collision.CanHit(projectile.Center, 1, 1, Main.npc[index].Center, 1, 1))
					{
					if (maxValue < 20)
					{
						numArray[maxValue] = index;
						++maxValue;
						num2 = num4;
						num3 = num5;
					}
					flag = true;
					}
				}
                }
                if (flag && Main.rand.Next(20) == 0)
                {
				int index1 = Main.rand.Next(maxValue);
				int index2 = numArray[index1];
				float num6 = Main.npc[index2].position.X + (float) (Main.npc[index2].width / 2);
				float num7 = Main.npc[index2].position.Y + (float) (Main.npc[index2].height / 2);
				float num8 = 6f;
				Vector2 vector2 = new Vector2(projectile.position.X + (float) projectile.width * 0.5f, projectile.position.Y + (float) projectile.height * 0.5f) + projectile.velocity * 4f;
				float num9 = num6 - vector2.X;
				float num10 = num7 - vector2.Y;
				float num11 = (float) Math.Sqrt((double) num9 * (double) num9 + (double) num10 * (double) num10);
				float num12 = num8 / num11;
				float SpeedX = num9 * num12;
				float SpeedY = num10 * num12;
				Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, (int) mod.ProjectileType("TaserZap"), 5, projectile.knockBack, projectile.owner, 0.0f, 0.0f);
				}
            }        
		    return false;
		}
        int gyrojets;
        bool displayCircle;
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            displayCircle = true;
            int timer = 0;
            timer++;
            if (timer >= 30)
            {
                target.StrikeNPC(5, 0f, 0, crit);
                timer = 0;
            }
			projectile.ai[0] = 1f;
			projectile.ai[1] = (float)target.whoAmI;
			projectile.velocity = (target.Center - projectile.Center) * 0.75f;
			projectile.netUpdate = true;
			projectile.damage = 0;

			int num31 = 6;
			Point[] array2 = new Point[num31];
			int num32 = 0;

			for (int n = 0; n < 1000; n++)
			{
				if (n != projectile.whoAmI && Main.projectile[n].active && Main.projectile[n].owner == Main.myPlayer && Main.projectile[n].type == projectile.type && Main.projectile[n].ai[0] == 1f && Main.projectile[n].ai[1] == target.whoAmI)
				{
                    gyrojets+= 6;
					array2[num32++] = new Point(n, Main.projectile[n].timeLeft);
					if (num32 >= array2.Length)
						break;
				}
			}

			if (num32 >= array2.Length)
			{
				int num33 = 0;
				for (int num34 = 1; num34 < array2.Length; num34++)
				{
					if (array2[num34].Y < array2[num33].Y)
						num33 = num34;
				}
				Main.projectile[array2[num33].X].Kill();
			}
		}
	}
}

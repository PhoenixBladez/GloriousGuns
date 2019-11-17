﻿using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Atlas
{
	public class TrackerGrenade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tracker Grenade");
		}

		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.timeLeft = 300;
			projectile.ranged = true;
			projectile.friendly = true;

			projectile.penetrate = -1;
		}

		public override bool PreAI()
		{
			
			if (projectile.ai[0] == 0)
			{
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
				int num = 5;
				for (int k = 0; k < 3; k++)
					{
						int index2 = Dust.NewDust(projectile.position, 1, 1, 235, 0.0f, 0.0f, 0, new Color(), 1f);
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
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.ai[0] = 1f;
			projectile.ai[1] = (float)target.whoAmI;
			target.AddBuff(mod.BuffType("AtlasTracked"), 600, false);
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

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 235);
			}
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
		}

	}
}

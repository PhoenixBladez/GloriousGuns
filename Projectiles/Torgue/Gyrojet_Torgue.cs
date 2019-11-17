using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Projectiles.Torgue
{
	public class Gyrojet_Torgue : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gyrojet");
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
						int index2 = Dust.NewDust(projectile.position, 1, 1, 6, 0.0f, 0.0f, 0, new Color(), 1f);
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
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
        	Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            if(displayCircle)
			{
                Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int) ((double) projectile.position.X + (double) projectile.width * 0.5) / 16, (int) (((double) projectile.position.Y + (double) projectile.height * 0.5) / 16.0));
        
                int r1 = (int) color1.R;
                drawOrigin.Y += 30f;
                --drawOrigin.X;
                Vector2 position1 = projectile.Bottom - Main.screenPosition;
                Texture2D texture2D2 = Main.glowMaskTexture[239];					
                float num11 = (float) ((double) Main.GlobalTime % 1.0 / 1.0);
                float num12 = num11;
                if ((double) num12 > 0.5)
                num12 = 1f - num11;
                if ((double) num12 < 0.0)
                num12 = 0.0f;
                float num13 = (float) (((double) num11 + 0.5) % 1.0);
                float num14 = num13;
                if ((double) num14 > 0.5)
                num14 = 1f - num13;
                if ((double) num14 < 0.0)
                num14 = 0.0f;
                Microsoft.Xna.Framework.Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
                drawOrigin = r2.Size() / 2f;
                Vector2 position3 = position1 + new Vector2(0.0f, -5f);
                Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color(255, 54, 23) * 1.6f;
                Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, projectile.rotation,drawOrigin, projectile.scale * 0.2f,  SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
                float num15 = 1f + num11 * 0.75f;
                Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, projectile.rotation,drawOrigin, projectile.scale * 0.2f * num15,  SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
                float num16 = 1f + num13 * 0.75f;
                Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, projectile.rotation,drawOrigin, projectile.scale * 0.2f * num16,  SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
                Texture2D texture2D3 = Main.extraTexture[89];
                Microsoft.Xna.Framework.Rectangle r3 = texture2D3.Frame(1, 1, 0, 0);
                drawOrigin = r3.Size() / 2f;
                Vector2 scale = new Vector2(0.75f, 1f + num16) * 1.5f;
                float num17 = 1f + num13 * 0.75f;
			}
            return true;
		}
        int gyrojets;
        bool displayCircle;
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            displayCircle = true;
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
        public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void Kill(int timeLeft)
		{         
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("TorgueExplosion"), (int)(25 + (1 * gyrojets)), projectile.knockBack, projectile.owner, 0f, 0f);
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
			for (int num623 = 0; num623 < 15; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 244, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 1.5f;
				num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 1f;
			}

		}

	}
}

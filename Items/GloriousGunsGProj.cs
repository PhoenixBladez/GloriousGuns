using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GloriousGuns;

namespace GloriousGuns.Items
{

	public class GloriousGunsGProj : GlobalProjectile
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public bool shotFromSlagWeaponCommon = false;
		public bool shotFromMaliwanSlagCommon = false;
		public bool shotFromFireWeaponCommon = false;
        public bool shotFromMaliwanFireCommon = false;
		public bool shotFromAcidWeaponCommon = false;
		public bool shotFromMaliwanAcidCommon = false;
        public bool shotFromJakobs = false;
        public bool shotFromAtlasHoming = false;
		public bool shotFromShockWeaponCommon = false;
		public bool shotFromMaliwanShockCommon = false;
        public bool shotFromJudge = false;
        public bool shotFromHornet = false;
        public bool shotFromGwensHead = false;

		public override bool PreAI(Projectile projectile)
		{
			if (shotFromSlagWeaponCommon == true || shotFromMaliwanSlagCommon == true)
			{
						Lighting.AddLight(projectile.position, 0.2f, 0.06f, 0.3f);
						projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                        for (int k = 0; k < 3; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 173);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 0f;
                            Main.dust[dust].scale = .78f;
                        }
			}
			if (shotFromFireWeaponCommon == true || shotFromMaliwanFireCommon == true)
			{
						Lighting.AddLight(projectile.position,0.4f, 0.12f, 0.06f);
						projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                        for (int k = 0; k < 3; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 127);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 0f;
                            Main.dust[dust].scale = .78f;
                        }
			}
			if (shotFromAcidWeaponCommon == true || shotFromMaliwanAcidCommon == true)
			{
						Lighting.AddLight(projectile.position,0.4f, 0.12f, 0.06f);
						projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                        for (int k = 0; k < 3; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1,1, 163);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 0f;
                            Main.dust[dust].scale = .78f;
                        }
			}
        	if (shotFromHornet == true)
			{
                    Lighting.AddLight(projectile.position,0.4f, 0.12f, 0.06f);
					projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                    for (int k = 0; k < 11; k++)
                    {
                        int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 163);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity *= 0f;
                        Main.dust[dust].scale = .88f;
                    }
            }
			if (shotFromShockWeaponCommon == true || shotFromMaliwanShockCommon == true)
			{
						Lighting.AddLight(projectile.position,0.4f, 0.12f, 0.06f);
						projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                        for (int k = 0; k < 3; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 1, 1, 226);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 0f;
                            Main.dust[dust].scale = .58f;
                        }
			}
            if (shotFromAtlasHoming)
            {
                bool flag25 = false;
                int jim = 1;
                for (int index1 = 0; index1 < 200; index1++)
                {
                    if (Main.npc[index1].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[index1].Center, 1, 1) && Main.npc[index1].HasBuff(mod.BuffType("AtlasTracked")))
                    {
                        float num23 = Main.npc[index1].position.X + (float)(Main.npc[index1].width / 2);
                        float num24 = Main.npc[index1].position.Y + (float)(Main.npc[index1].height / 2);
                        float num25 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num23) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num24);
                        if (num25 < 300f)
                        {
                            flag25 = true;
                            jim = index1;
                        }

                    }
                }

                if (flag25)
                {
                    float num1 = 4f;
                    Vector2 vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num2 = Main.npc[jim].Center.X - vector2.X;
                    float num3 = Main.npc[jim].Center.Y - vector2.Y;
                    float num4 = (float)Math.Sqrt((double)num2 * (double)num2 + (double)num3 * (double)num3);
                    float num5 = num1 / num4;
                    float num6 = num2 * num5;
                    float num7 = num3 * num5;
                    int num8 = 30;
                    projectile.velocity.X = (projectile.velocity.X * (float)(num8 - 1) + num6) / (float)num8;
                    projectile.velocity.Y = (projectile.velocity.Y * (float)(num8 - 1) + num7) / (float)num8;
                }

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
			return base.PreAI(projectile);
		}
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
			if (shotFromSlagWeaponCommon == true || shotFromMaliwanSlagCommon == true)
            {
                if (Main.rand.Next(4) == 0)
                {
                    target.AddBuff(mod.BuffType("SlagDebuff"), 240);
                    if (shotFromMaliwanSlagCommon)
                    {
                        target.StrikeNPC(projectile.damage / 2, 0f, 0, crit);
                        int d = 98;
                        int d1 = 173;
                        for (int k = 0; k < 20; k++)
                        {
                            Dust.NewDust(target.position, target.width, target.height, d, 2.5f, -2.5f, 0, Color.White, .7f);
                            Dust.NewDust(target.position, target.width, target.height, d, 2.5f, -2.5f, 0, Color.White, 0.27f);
                            Dust.NewDust(target.position, target.width, target.height, d1, 2.5f, -2.5f, 0, Color.White, .9f);
                            Dust.NewDust(target.position, target.width, target.height, d1, 2.5f, -2.5f, 0, Color.White, 0.27f);
                        }   
                    }
                }
            }
			if (shotFromFireWeaponCommon == true || shotFromMaliwanFireCommon == true)
            {
                if (Main.rand.Next(3) == 0)
                {
                    target.AddBuff(mod.BuffType("FireDebuff"), Main.rand.Next(300, 600));
                }
                 if (shotFromMaliwanFireCommon)
                    {
                        target.AddBuff(mod.BuffType("FireDebuff"), Main.rand.Next(240, 400));
                        target.StrikeNPC(projectile.damage / 2, 0f, 0, crit);
                        int d = 6;
                        int d1 = 6;
                        for (int k = 0; k < 20; k++)
                        {
                            Dust.NewDust(target.position, target.width, target.height, d, 2.5f, -2.5f, 0, Color.White, .7f);
                            Dust.NewDust(target.position, target.width, target.height, d, 2.5f, -2.5f, 0, Color.White, 0.27f);
                            Dust.NewDust(target.position, target.width, target.height, d1, 2.5f, -2.5f, 0, Color.White, .9f);
                            Dust.NewDust(target.position, target.width, target.height, d1, 2.5f, -2.5f, 0, Color.White, 0.27f);
                        }                           
                    }
            }
			if (shotFromAcidWeaponCommon == true || shotFromMaliwanAcidCommon == true)
            {
                if (Main.rand.Next(3) == 0)
                {
                    target.AddBuff(mod.BuffType("CorrosiveDebuff"), Main.rand.Next(240, 320));
                }
                    if (shotFromMaliwanAcidCommon)
                    {
                        target.AddBuff(mod.BuffType("CorrosiveDebuff"), Main.rand.Next(180, 240));
                        target.StrikeNPC(projectile.damage / 2, 0f, 0, crit);
                    
                        int d = 163;
                        int d1 = 163;
                        for (int k = 0; k < 20; k++)
                        {
                            Dust.NewDust(target.position, target.width, target.height, d, 2.5f, -2.5f, 0, Color.White, .7f);
                            Dust.NewDust(target.position, target.width, target.height, d, 2.5f, -2.5f, 0, Color.White, 0.27f);
                            Dust.NewDust(target.position, target.width, target.height, d1, 2.5f, -2.5f, 0, Color.White, .9f);
                            Dust.NewDust(target.position, target.width, target.height, d1, 2.5f, -2.5f, 0, Color.White, 0.27f);
                        }   
                    }
            }
            if (shotFromHornet == true)
            {
                if (Main.rand.Next(3) == 0)
                {
                    target.AddBuff(mod.BuffType("CorrosiveDebuff"), Main.rand.Next(240, 320));
                }
                {
                    int d = 163;
                    int d1 = 163;
                    for (int k = 0; k < 20; k++)
                    {
                        int du1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, d, .5f, -.5f, 0, Color.White, .7f);
                        int du2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, d, .5f, -.5f, 0, Color.White, 0.27f);
                        int du3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, d1, -.5f, .5f, 0, Color.White, .9f);
                        int du4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, d1, -.5f, .5f, 0, Color.White, 0.27f);
                        Main.dust[du1].noGravity = true;
                        Main.dust[du2].noGravity = true;
                        Main.dust[du3].noGravity = true;
                        Main.dust[du4].noGravity = true;
                    }   
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("HornetAcidSplash"), projectile.damage/3 * 2, 0, projectile.owner, 0f, 0f);
                    
                }
            }
             if (shotFromJakobs && crit)
            {
                    Main.PlaySound(SoundLoader.customSoundType, projectile.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/JakobsRicochet"));
                    projectile.penetrate = 2;
                	projectile.ai[0] += 0.1f;
					projectile.velocity.X = -projectile.velocity.X;
					projectile.velocity.Y = -projectile.velocity.Y;

					projectile.velocity *= 0.85f;
            }
			if (shotFromShockWeaponCommon == true || shotFromMaliwanShockCommon == true)
            {
                if (Main.rand.Next(5) == 0)
                {
                    target.AddBuff(mod.BuffType("ShockDebuff"), Main.rand.Next(240, 320));
                }
                    if (shotFromMaliwanShockCommon)
                    {
                            target.AddBuff(mod.BuffType("ShockDebuff"), Main.rand.Next(240, 320));
                        	target.StrikeNPC(projectile.damage / 2, 0f, 0, crit);
                    
                        int d = 226;
                        int d1 = 226;
                        for (int k = 0; k < 20; k++)
                        {
                            Dust.NewDust(target.position, target.width, target.height, d, 2.5f, -2.5f, 0, Color.White, .7f);
                            Dust.NewDust(target.position, target.width, target.height, d, 2.5f, -2.5f, 0, Color.White, 0.27f);
                            Dust.NewDust(target.position, target.width, target.height, d1, 2.5f, -2.5f, 0, Color.White, .9f);
                            Dust.NewDust(target.position, target.width, target.height, d1, 2.5f, -2.5f, 0, Color.White, 0.27f);
                        }   
                    }
            }
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (shotFromJudge && crit)
            {
                damage = (int)(damage * 1.63f);
            }
            if (shotFromGwensHead && crit)
            {
                damage = (int)(damage * 1.38f);
            }
        }
         public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity) 
         {
             if(shotFromHornet)
             {
                int d = 163;
                int d1 = 163;
                for (int k = 0; k < 20; k++)
                    {
                        int du1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, d, .5f, -.5f, 0, Color.White, .7f);
                        int du2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, d, .5f, -.5f, 0, Color.White, 0.27f);
                        int du3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, d1, -.5f, .5f, 0, Color.White, .9f);
                        int du4 = Dust.NewDust(projectile.position, projectile.width, projectile.height, d1, -.5f, .5f, 0, Color.White, 0.27f);
                        Main.dust[du1].noGravity = true;
                        Main.dust[du2].noGravity = true;
                        Main.dust[du3].noGravity = true;
                        Main.dust[du4].noGravity = true;
                    }   
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("HornetAcidSplash"), projectile.damage/3 * 2, 0, projectile.owner, 0f, 0f);
                    return true;
             }
             return true;
         }
	}
}
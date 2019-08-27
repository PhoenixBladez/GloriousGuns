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
		public bool shotFromShockWeaponCommon = false;
		public bool shotFromMaliwanShockCommon = false;
        public bool shotFromJudge = false;

		public override bool PreAI(Projectile projectile)
		{
			if (shotFromSlagWeaponCommon == true || shotFromMaliwanSlagCommon == true)
			{
						Lighting.AddLight(projectile.position, 0.2f, 0.06f, 0.3f);
						projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                        for (int k = 0; k < 3; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 173);
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
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127);
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
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 163);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 0f;
                            Main.dust[dust].scale = .78f;
                        }
			}
			if (shotFromShockWeaponCommon == true || shotFromMaliwanShockCommon == true)
			{
						Lighting.AddLight(projectile.position,0.4f, 0.12f, 0.06f);
						projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                        for (int k = 0; k < 3; k++)
                        {
                            int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity *= 0f;
                            Main.dust[dust].scale = .78f;
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
        }
	}
}
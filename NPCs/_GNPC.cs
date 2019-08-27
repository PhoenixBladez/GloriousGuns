using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

using GloriousGuns;
using GloriousGuns.Items;

namespace GloriousGuns.NPCs
{
	public class GNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public string elementalType = "";
		public bool slagged = false;
		public bool corrosive = false;
		public bool shock = false;
		public bool incendiary = false;
		
        public bool nameConfirmed = false;
        public bool MPSynced = false;
        public bool readyForChecks = false;

		
		public override void ResetEffects(NPC npc)
		{
			slagged = false;
			corrosive = false;
			shock = false;
			incendiary = false;
		}
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			int before = npc.lifeRegen;
			bool drain = false;
			bool noDamage = damage <= 1;
			int damageBefore = damage;
			if (corrosive)
			{
				drain = true;
				int tickdamage = (int)(npc.lifeMax * 3.75f / npc.life);
				npc.lifeRegen -= tickdamage;
				if (tickdamage >= 18)
				{
					tickdamage = 18;
				}
				if (damage < 2)
				{
					damage = 2;
				}
			}
			if (incendiary)
			{
				drain = true;
				npc.lifeRegen -= 9;
				damage = 1;
			}
			if (shock)
			{
				int moveDamage = 3 * (int)(npc.velocity.X*1.25f	);
				if (moveDamage < 0)
				{
					moveDamage = moveDamage * -1;
				}
				drain = true;
				npc.lifeRegen -= moveDamage;
			}
			if (noDamage)
			damage -= damageBefore;
			if (drain && before > 0)
			npc.lifeRegen -= before;
			
		}

		public override void SetDefaults(NPC npc)
        {
            npc.GivenName = npc.FullName;
            if (Main.netMode == 1 || npc == null || npc.FullName == null)//if multiplayer, but not server. 1 is client in MP, 2 is server. Prefixes are sent to client by server in MP.
            {
                return;
            }
			if (npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37)) && npc.type != 401 && npc.type != 488 && npc.type != 371 && npc.lifeMax > 1 && !(npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1))
            {
                if (Main.rand.Next(0, Main.expertMode ? 90 : 100) == 0 && (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
                {
                    npc.GivenName = "Slagged " + npc.GivenName;
                    elementalType += "Slagged #";
					npc.buffImmune[mod.BuffType("SlagDebuff")] = true;
                    npc.value *= 1.23f;
					npc.defense /= 4;
                }
                if (Main.rand.Next(0, Main.expertMode ? 90 : 100) == 0 && (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
                {
                    npc.GivenName = "Corrosive " + npc.GivenName;
                    elementalType += "Corrosive #";
					npc.buffImmune[mod.BuffType("CorrosiveDebuff")] = true;
                    npc.value *= 1.23f;
                }
                if (Main.rand.Next(0, Main.expertMode ? 90 : 100) == 0 && (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
                {
                    npc.GivenName = "Burning " + npc.GivenName;
                    elementalType += "Burning #";
					npc.buffImmune[mod.BuffType("FireDebuff")] = true;
					npc.buffImmune[BuffID.OnFire] = true;
                    npc.value *= 1.23f;
                }
                if (Main.rand.Next(0, Main.expertMode ? 90 : 100) == 0 && (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
                {
                    npc.GivenName = "Electrified " + npc.GivenName;
                    elementalType += "Electrified #";
					npc.buffImmune[mod.BuffType("ShockDebuff")] = true;
                    npc.value *= 1.23f;
                }
			}
		}
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (elementalType.Contains("Slagged ") || npc.HasBuff(mod.BuffType("SlagDebuff")))
            {
                Lighting.AddLight(npc.position, 0.192f, 0.09f, 0.356f);
                drawColor = new Microsoft.Xna.Framework.Color(134, 68, (int) byte.MaxValue, 226);
				if (Main.rand.Next(2)==0)
				{
					for (int k = 0; k < 8; k++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 98, 1.5f * npc.direction, -1.5f, 0, Color.White, .7f);
						Dust.NewDust(npc.position, npc.width, npc.height, 98, .5f * npc.direction, 0f, 0, Color.White, .37f);
					}
				}
				
            }
			if (elementalType.Contains("Corrosive ") || npc.HasBuff(mod.BuffType("CorrosiveDebuff")))
            {
                drawColor = new Microsoft.Xna.Framework.Color(138, 189, 66);
				if (Main.rand.Next(5)==0)
				{
					for (int k = 0; k < 4; k++)
					{
						int num = Dust.NewDust(npc.position, npc.width, npc.height, 163, Main.rand.NextFloat(.5f, 1.2f), 0f, 0, Color.White, .7f);
						Main.dust[num].velocity *= .01f;
						Main.dust[num].noGravity = true;
						int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 163, 0f, 0f, 0, Color.White, .37f);
						Main.dust[num1].velocity *= .0001f;
					}
				}	
            }
			if (elementalType.Contains("Burning ") || npc.HasBuff(mod.BuffType("FireDebuff")))
            {
                drawColor = new Microsoft.Xna.Framework.Color(219, 70, 43);
				if (Main.rand.Next(4)==0)
				{
					for (int k = 0; k < 4; k++)
					{
						int num = Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, -.25f, 0, Color.White, .85f);
						Main.dust[num].noGravity = true;
						int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 127, 0f, -.34f, 0, Color.White, .67f);
						Main.dust[num1].noGravity = true;						
					}
				}
				
            }
			if (elementalType.Contains("Electrified ") || npc.HasBuff(mod.BuffType("ShockDebuff")))
            {
                drawColor = new Microsoft.Xna.Framework.Color(39, 107, 196);
				if (Main.rand.Next(5)==0)
				{
					for (int k = 0; k < 4; k++)
					{
						int num = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, -.25f, 0, Color.White, .5f);
						int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 226, 0f, -.34f, 0, Color.White, .37f);
					}
				}
				
            }
        }
		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
		{
			if (npc.HasBuff(mod.BuffType("SlagDebuff")) && !npc.boss)
			{
				damage *= 2;
			}	
		}
		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (npc.HasBuff(mod.BuffType("SlagDebuff")) && !npc.boss)
			{
				if (!projectile.GetGlobalProjectile<GloriousGunsGProj>(mod).shotFromSlagWeaponCommon)
				{ 
				damage *= 2;
				}
			}	
		}
		public override bool PreAI(NPC npc)
        {  
			if (!MPSynced)
            {
                if (Main.netMode == 2)
                {
                    //send packet containing npc.whoAmI and all prefixes/suffixes.
                    var netMessage = mod.GetPacket();
                    netMessage.Write("elementalType");
                    netMessage.Write(npc.whoAmI);
                    bool haselementalType = elementalType.Length > 0;
                    netMessage.Write(haselementalType);
                    if (haselementalType)
                    {
                        netMessage.Write(elementalType);
                    }
                    netMessage.Send();
                }
                MPSynced = true;
                return true;
            }
            if (!nameConfirmed && (Main.netMode != 1 || readyForChecks))//block to ensure all enemies retain prefixes in their displayNames
            {
			 if (elementalType.Length > 1)//has prefixes
                {
                    npc.GivenName = npc.FullName;
                    string[] elementalTypeArr = elementalType.Split('#');
                    for (int i = 0; i < elementalTypeArr.Length; i++)
                    {
                        if (!npc.GivenName.Contains(elementalTypeArr[i]))
                        {
                            npc.GivenName = elementalType[i] + npc.GivenName;
                        }
                    }
                }
	            npc.GivenName = npc.GivenName.Replace("  the", "");
				nameConfirmed = true;
			}
		return base.PreAI(npc);
		}
		public override void HitEffect(NPC npc, int hitDirection, double damage) 
		{
            if (elementalType.Contains("Corrosive "))
            {
				for (int i = 0; i < 10; i++) ;
				{
					int d = 163;
					int d1 = 163;
					for (int k = 0; k < 20; k++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
						Dust.NewDust(npc.position, npc.width, npc.height, d, 1.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
						Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, .1f);
						Dust.NewDust(npc.position, npc.width, npc.height, d1, 1.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					}

					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				}
			}
			if (elementalType.Contains("Slagged ") || npc.HasBuff(mod.BuffType("SlagDebuff")))
            {
				  Main.PlaySound(new Terraria.Audio.LegacySoundStyle(3, 13));
				for (int i = 0; i < 10; i++) ;
				{
					int d = 98;
					int d1 = 173;
					for (int k = 0; k < 20; k++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
						Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
						Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, .9f);
						Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					}

					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				}
			}
			if (elementalType.Contains("Burning "))
            {
				for (int i = 0; i < 10; i++) ;
				{
					int d = 6;
					int d1 = 127;
					for (int k = 0; k < 20; k++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
						Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
						Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, .9f);
						Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					}

					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				}
			}
			if (elementalType.Contains("Electrified "))
            {
				for (int i = 0; i < 10; i++) ;
				{
					int d = 226;
					int d1 = 226;
					for (int k = 0; k < 20; k++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
						Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
						Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, .6f);
						Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					}

					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, .7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				}
			}
		}
		public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit) 
		{
			if (elementalType.Contains("Slagged "))
            {
				target.AddBuff(mod.BuffType("SlagDebuff"), 600); 
			}
			if (elementalType.Contains("Corrosive "))
            {
				target.AddBuff(mod.BuffType("CorrosiveDebuff"), 600); 
			}
			if (elementalType.Contains("Burning "))
            {
				target.AddBuff(mod.BuffType("FireDebuff"), 600); 
			}
			if (elementalType.Contains("Electrified "))
            {
				target.AddBuff(mod.BuffType("ShockDebuff"), 600); 
			}
		}
	}
}
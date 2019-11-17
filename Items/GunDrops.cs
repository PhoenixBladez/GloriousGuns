using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Items
{
	internal class GunDrops : GlobalNPC
	{
        public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}			
		public override void NPCLoot(NPC npc)
		{	
			int CommonPistol_Bandit;
			int CommonPistol_Hyperion;
			int CommonPistol_Jakobs;
			int CommonPistol_Maliwan;
			int CommonPistol_Vladof;
			int CommonPistol_Dahl;
			int CommonPistol_Torgue;
			int CommonPistol_Tediore;				
		  	Player closest = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)];	
			#region Uniques
			if (npc.type == NPCID.EyeofCthulhu)
			{
				if (Main.rand.Next(22) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Unique_Judge"));
				}
			}
			if (!npc.SpawnedFromStatue) //&& npc.type != NPCID.BlueSlime)
			{
				if (npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37)) && npc.type != 401 && npc.type != 488 && npc.type != 371 && npc.lifeMax > 1 && !(npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1))
				{
					if ((npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
					{
						#region UniquePistols
						if (closest.ZoneCrimson && Main.rand.Next(0, Main.expertMode ? 95 : 115) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GwensHead"));
						}
						if (closest.ZoneJungle && closest.ZoneRockLayerHeight && Main.rand.Next(0, Main.expertMode ? 250 : 300) == 0 || closest.ZoneJungle && closest.ZoneDirtLayerHeight && Main.rand.Next(0, Main.expertMode ? 250 : 300) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Hornet"));
						}
						if (npc.type == NPCID.GraniteGolem && Main.rand.Next(0, Main.expertMode ? 95 : 130) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("KillingWord"));
						}
					}
				}
			}
			#endregion
			#region CommonPistols_PreHM
			if (npc.type == NPCID.EyeofCthulhu)
			{
				if (Main.rand.Next(2) == 0)
				{
					CommonPistol_Bandit = Main.rand.Next(new int[]{mod.ItemType("Pistal_MaliwanSlag"),  mod.ItemType("Pistal_MaliwanCorrosive"), mod.ItemType("Pistal_MaliwanFire"),  mod.ItemType("Pistal_MaliwanShock"), mod.ItemType("Pistal_Bandit"), mod.ItemType("Pistal_Dahl")});
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Bandit);
				}
				if (Main.rand.Next(2) == 0)
				{
					CommonPistol_Dahl =  Main.rand.Next(new int[]{mod.ItemType("Repeater_MaliwanFire"), mod.ItemType("Repeater_MaliwanShock"), mod.ItemType("Repeater_MaliwanCorrosive"), mod.ItemType("Repeater_MaliwanSlag"), mod.ItemType("Repeater_Dahl"), mod.ItemType("Repeater_DahlBarrel"), mod.ItemType("Apparatus_MaliwanFire"), mod.ItemType("Apparatus_MaliwanShock"), mod.ItemType("Apparatus_MaliwanCorrosive"), mod.ItemType("Apparatus_MaliwanSlag")});
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Dahl);
				}
			}
			if (!npc.SpawnedFromStatue) //&& npc.type != NPCID.BlueSlime)
			{
				if (npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37)) && npc.type != 401 && npc.type != 488 && npc.type != 371 && npc.lifeMax > 1 && !(npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1))
				{
					if ((npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
					{
						if (Main.rand.Next(0, Main.expertMode ? 15 : 25) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BasicBullet"), Main.rand.Next(10, 22));
						}
						#endregion
						if (closest.ZoneCorrupt || closest.ZoneCrimson || !Main.dayTime && !closest.ZoneBeach && !closest.ZoneHoly && !closest.ZoneSnow && closest.ZoneDungeon && closest.ZoneOverworldHeight)
						{
							if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
							{
								if (Main.rand.Next(3) == 0)
								{
									CommonPistol_Bandit = Main.rand.Next(new int[]{mod.ItemType("Pistal_MaliwanSlag"),  mod.ItemType("Pistal_MaliwanCorrosive"),  mod.ItemType("Pistal_MaliwanFire"),  mod.ItemType("Pistal_MaliwanShock")});
								}
								else
								{
									CommonPistol_Bandit = Main.rand.Next(new int[]{mod.ItemType("Pistal_Bandit"), mod.ItemType("Pistal_Dahl")});
								}
								Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Bandit);
							}
						}
						if (closest.ZoneRockLayerHeight)
						{
							if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
							{							
								CommonPistol_Torgue = Main.rand.Next(new int[]{mod.ItemType("HandCannon_Bandit"), mod.ItemType("HandCannon_Dahl")});
								Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Torgue);
							}
						}
						if (closest.ZoneUnderworldHeight)
						{
							if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
							{
								if (Main.rand.Next(3) == 0)
								{
									CommonPistol_Vladof =  Main.rand.Next(new int[]{mod.ItemType("TMP_MaliwanFire"), mod.ItemType("TMP_MaliwanShock"), mod.ItemType("TMP_MaliwanSlag"), mod.ItemType("TMP_MaliwanCorrosive")});
								}
								else
								{
									CommonPistol_Vladof = Main.rand.Next(new int[]{mod.ItemType("TMP_Dahl"), mod.ItemType("TMP_Bandit"), mod.ItemType("TMP_Tediore")}); 
								}
								Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Vladof);
							}
						}
						if (closest.ZoneRockLayerHeight || closest.ZoneDirtLayerHeight)
						{
							if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
							{
								if (Main.rand.Next(3) == 0)
								{
									CommonPistol_Hyperion =  Main.rand.Next(new int[]{mod.ItemType("Apparatus_MaliwanFire"), mod.ItemType("Apparatus_MaliwanShock"), mod.ItemType("Apparatus_MaliwanCorrosive"), mod.ItemType("Apparatus_MaliwanSlag")});
								}
								else
								{
									CommonPistol_Hyperion =  Main.rand.Next(new int[]{mod.ItemType("Apparatus_Bandit"), mod.ItemType("Apparatus_Maliwan")});
								}
								Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Hyperion);
							}
						}	
						if (closest.ZoneRockLayerHeight || closest.ZoneDirtLayerHeight)
						{
							if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
							{
								{
									CommonPistol_Jakobs =  Main.rand.Next(new int[]{mod.ItemType("Revolver_Bandit"), mod.ItemType("Revolver_Dahl"), mod.ItemType("Revolver_Maliwan"), mod.ItemType("Revolver_Tediore")});
								}
								Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Jakobs);
							}
						}
						if (closest.ZoneJungle)	
						{
							if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
							{
								if (Main.rand.Next(3) == 0)
								{
									CommonPistol_Dahl =  Main.rand.Next(new int[]{mod.ItemType("Repeater_MaliwanFire"), mod.ItemType("Repeater_MaliwanShock"), mod.ItemType("Repeater_MaliwanCorrosive"), mod.ItemType("Repeater_MaliwanSlag")});
								}
								else
								{
									CommonPistol_Dahl =  Main.rand.Next(new int[]{mod.ItemType("Repeater_Dahl"), mod.ItemType("Repeater_DahlBarrel")});
								}
								Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Dahl);
							}
						}
						if (closest.ZoneDesert && closest.ZoneRockLayerHeight || closest.ZoneDesert && closest.ZoneDirtLayerHeight)	
						{
							if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
							{
								if (Main.rand.Next(3) == 0)
								{
									CommonPistol_Tediore =  Main.rand.Next(new int[]{mod.ItemType("Handgun_MaliwanFire"), mod.ItemType("Handgun_MaliwanShock"), mod.ItemType("Handgun_MaliwanCorrosive"), mod.ItemType("Handgun_MaliwanSlag")});
								}
								else
								{
									CommonPistol_Tediore =  Main.rand.Next(new int[]{mod.ItemType("Handgun_Dahl"), mod.ItemType("Handgun_Maliwan"), mod.ItemType("Handgun_Tediore")});
								}
								Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Tediore);
							}
						}
						if (closest.ZoneSnow && closest.ZoneRockLayerHeight || closest.ZoneSnow && closest.ZoneDirtLayerHeight)			
						{
							if (Main.rand.Next(0, Main.expertMode ? 70 : 85) == 0)
							{
								{
									CommonPistol_Maliwan =  Main.rand.Next(new int[]{mod.ItemType("Aegis_DahlFire"), mod.ItemType("Aegis_MaliwanFire"), mod.ItemType("Aegis_MaliwanShock"), mod.ItemType("Aegis_DahlShock"), mod.ItemType("Aegis_MaliwanSlag"), mod.ItemType("Aegis_DahlSlag"), mod.ItemType("Aegis_MaliwanCorrosive"), mod.ItemType("Aegis_DahlCorrosive")});
									Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Maliwan);					
								}
							}
						}
					}
				}
				#endregion
				#region UncommonPistols_PreHM
				{
					if (!npc.SpawnedFromStatue && NPC.downedBoss2) //&& npc.type != NPCID.BlueSlime)
						{
							if (npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37)) && npc.type != 401 && npc.type != 488 && npc.type != 371 && npc.lifeMax > 1 && !(npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1))
							{
								if ((npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
								{	
									if (closest.ZoneCorrupt || closest.ZoneCrimson || !Main.dayTime && !closest.ZoneBeach && !closest.ZoneHoly && !closest.ZoneSnow && closest.ZoneDungeon && closest.ZoneOverworldHeight)
									{
										//CoV
										if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
										{
											if (Main.rand.Next(3) == 0)
											{
												CommonPistol_Bandit = Main.rand.Next(new int[]{mod.ItemType("HoleyMan_Fire"),  mod.ItemType("HoleyMan_Shock"),  mod.ItemType("HoleyMan_Corrosive"),  mod.ItemType("HoleyMan_Slag")});
											}
											else
											{
												CommonPistol_Bandit = Main.rand.Next(new int[]{mod.ItemType("HoleyMan")});
											}
											Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Bandit);
										}
									}
									if (closest.ZoneRockLayerHeight)
									{
										//Torgue
										if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
										{							
											if (Main.rand.Next(3) == 0)
											{
												CommonPistol_Torgue = Main.rand.Next(new int[]{mod.ItemType("PocketRocket_Fire"),  mod.ItemType("PocketRocket_Shock"),  mod.ItemType("PocketRocket_Corrosive"),  mod.ItemType("PocketRocket_Slag")});
											}
											else
											{
												CommonPistol_Torgue = Main.rand.Next(new int[]{mod.ItemType("PocketRocket")});
											}
											Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Torgue);
										}
									}
									if (closest.ZoneUnderworldHeight)
									{
										if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
										{
											//Vladof
											Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Kot"));
										}
									}
									if (closest.ZoneRockLayerHeight || closest.ZoneDirtLayerHeight)
									{
										//Atlas
										if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
										{
											Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AX7"));
										}
									}	
									if (closest.ZoneRockLayerHeight || closest.ZoneDirtLayerHeight)
									{
										//Jakobs
										if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
										{
											Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RangerGreen"));
										}
									}
									if (closest.ZoneJungle)	
									{
										//Dahl
										if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
										{
											if (Main.rand.Next(3) == 0)
											{
												CommonPistol_Dahl =  Main.rand.Next(new int[]{mod.ItemType("Shrike_Shock"), mod.ItemType("Shrike_Corrosive"), mod.ItemType("Shrike_Fire"), mod.ItemType("Shrike_Slag")});
											}
											else
											{
												CommonPistol_Dahl =  Main.rand.Next(new int[]{mod.ItemType("Shrike")});
											}
											Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Dahl);
										}
									}
									if (closest.ZoneDesert && closest.ZoneRockLayerHeight || closest.ZoneDesert && closest.ZoneDirtLayerHeight)	
									{
										//Tediore
										if (Main.rand.Next(0, Main.expertMode ? 60 : 75) == 0)
										{
											if (Main.rand.Next(3) == 0)
											{
												CommonPistol_Tediore =  Main.rand.Next(new int[]{mod.ItemType("SureshotGreen_Slag"), mod.ItemType("SureshotGreen_Fire"), mod.ItemType("SureshotGreen_Shock"), mod.ItemType("SureshotGreen_Corrosive")});
											}
											else
											{
												CommonPistol_Tediore =  Main.rand.Next(new int[]{mod.ItemType("SureshotGreen")});
											}
											Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Tediore);
										}
									}
									if (closest.ZoneSnow && closest.ZoneRockLayerHeight || closest.ZoneSnow && closest.ZoneDirtLayerHeight)			
									{
										//Maliwan
										if (Main.rand.Next(0, Main.expertMode ? 70 : 85) == 0)
										{
											{
												CommonPistol_Maliwan = mod.ItemType("Blaster");
												Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonPistol_Maliwan);					
											}
										}
									}									
								}
							}
						}				
				}
				#endregion
				#region ShotgunWhite_PreHM
				{
					
					int CommonShotgun_Bandit;
					int CommonShotgun_Hyperion;
					int CommonShotgun_Jakobs;
					int CommonShotgun_Torgue;
					int CommonShotgun_Tediore;
					if (npc.type == NPCID.EyeofCthulhu)
					{
						if (Main.rand.Next(2) == 0)
						{
							CommonShotgun_Bandit = CommonShotgun_Bandit = Main.rand.Next(new int[]{mod.ItemType("RagneKiller_HyperionToxic"),  mod.ItemType("RagneKiller_HyperionFire"),  mod.ItemType("Skatergun_TedioreSlag"),  mod.ItemType("Skatergun_JakobsShock"), mod.ItemType("FaceTime"), mod.ItemType("ProjDiversification"), mod.ItemType("Thinking")}); 
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonShotgun_Bandit);
						}
					}
					if (NPC.downedBoss1)
					{
						if (npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37)) && npc.type != 401 && npc.type != 488 && npc.type != 371 && npc.lifeMax > 1 && !(npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1))
						{
							if ( (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
							{	
								if (closest.ZoneCorrupt || closest.ZoneCrimson || !Main.dayTime && !closest.ZoneBeach && !closest.ZoneHoly && !closest.ZoneSnow && !closest.ZoneDungeon && closest.ZoneOverworldHeight)
								{
									if (Main.rand.Next(0, Main.expertMode ? 85 : 92) == 0)
									{
										if (Main.rand.Next(3) == 0)
										{
											CommonShotgun_Bandit = Main.rand.Next(new int[]{mod.ItemType("RagneKiller_HyperionToxic"),  mod.ItemType("RagneKiller_HyperionFire"),  mod.ItemType("Skatergun_TedioreSlag"),  mod.ItemType("Skatergun_JakobsShock")});
										}
										else
										{
											CommonShotgun_Bandit = Main.rand.Next(new int[]{mod.ItemType("RagneKiller_Hyperion1"), mod.ItemType("RagneKiller_Hyperion2"), mod.ItemType("Skatergun_Tediore1"), mod.ItemType("Skatergun_Tediore2"), mod.ItemType("StretSweper_Jakobs1"), mod.ItemType("StretSweper_Jakobs2")});
										}
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonShotgun_Bandit);
									}
								}				
						
								if (closest.ZoneRockLayerHeight)
								{
									if (Main.rand.Next(0, Main.expertMode ? 85 : 92) == 0)
									{
										CommonShotgun_Torgue = Main.rand.Next(new int[]{mod.ItemType("Pounder"), mod.ItemType("Stalker"),mod.ItemType("Bangstick")});
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonShotgun_Torgue);
									}
								}
								if (closest.ZoneRockLayerHeight || closest.ZoneDirtLayerHeight)
								{
									if (Main.rand.Next(0, Main.expertMode ? 85 : 92) == 0)
									{
										{
											CommonShotgun_Jakobs =  Main.rand.Next(new int[]{mod.ItemType("CoachGun"), mod.ItemType("Scattergun"), mod.ItemType("Longrider")});
										}
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonShotgun_Jakobs);
									}
								}
								if (closest.ZoneRockLayerHeight || closest.ZoneDirtLayerHeight)
								{
									if (Main.rand.Next(0, Main.expertMode ? 85 : 92) == 0)
									{
										if (Main.rand.Next(3) == 0)
										{
											CommonShotgun_Hyperion =  Main.rand.Next(new int[]{mod.ItemType("FaceTime_Fire"), mod.ItemType("ProjDiversification_Slag"), mod.ItemType("ProjDiversification_Shock"), mod.ItemType("Thinking_Corrosive")});
										}
										else
										{
											CommonShotgun_Hyperion =  Main.rand.Next(new int[]{mod.ItemType("FaceTime"), mod.ItemType("ProjDiversification"), mod.ItemType("Thinking")});
										}
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonShotgun_Hyperion);
									}
								}
								if (closest.ZoneDesert && !closest.ZoneOverworldHeight)	
								{
									if (Main.rand.Next(0, Main.expertMode ? 85 : 92) == 0)
									{
										if (Main.rand.Next(3) == 0)
										{
											CommonShotgun_Tediore =  Main.rand.Next(new int[]{mod.ItemType("DoubleBarrels_Slag"), mod.ItemType("HomeSecurity_Fire"), mod.ItemType("Sportsman_Corrosive"), mod.ItemType("Sportstman_Shock")});
										}
										else
										{
											CommonShotgun_Tediore =  Main.rand.Next(new int[]{mod.ItemType("Sportsman"), mod.ItemType("DoubleBarrels"), mod.ItemType("HomeSecurity")});
										}
										
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonShotgun_Tediore);	
									}					
								}	
							}		
						}
					}
				}
				#endregion

				#region SMGWhite_PreHM
				{
					int CommonSMG_Bandit;
					int CommonSMG_Hyperion;
					int CommonSMG_Maliwan;
					int CommonSMG_Dahl;
					int CommonSMG_Tediore;
					if (NPC.downedBoss2)
					{
						if (npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37)) && npc.type != 401 && npc.type != 488 && npc.type != 371 && npc.lifeMax > 1 && !(npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1))
						{
							if ((npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
							{
								if (closest.ZoneCorrupt || closest.ZoneCrimson || !Main.dayTime && !closest.ZoneBeach && !closest.ZoneHoly && !closest.ZoneSnow && !closest.ZoneDungeon && closest.ZoneOverworldHeight)
								{
									if (Main.rand.Next(0, Main.expertMode ? 90 : 112) == 0)
									{
										if (Main.rand.Next(3) == 0)
										{
											CommonSMG_Bandit = Main.rand.Next(new int[]{mod.ItemType("SMG_MaliwanFire"),  mod.ItemType("SMG_MaliwanShock"),  mod.ItemType("SMG_MaliwanCorrosive"),  mod.ItemType("SMG_MaliwanSlag")});
										}
										else
										{
											CommonSMG_Bandit = Main.rand.Next(new int[]{mod.ItemType("SMG_Tediore"), mod.ItemType("SMG_Maliwan"), mod.ItemType("SMG_Bandit")});
										}
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonSMG_Bandit);
									}
								}
								
								if (closest.ZoneRockLayerHeight || closest.ZoneDirtLayerHeight)
								{
									if (Main.rand.Next(0, Main.expertMode ? 90 : 112) == 0)
									{
										if (Main.rand.Next(3) == 0)
										{
											CommonSMG_Hyperion =  Main.rand.Next(new int[]{mod.ItemType("ProjConvergence_MaliwanFire"), mod.ItemType("ProjConvergence_MaliwanSlag"), mod.ItemType("ProjConvergence_MaliwanCorrosive"), mod.ItemType("ProjConvergence_MaliwanShock")});
										}
										else
										{
											CommonSMG_Hyperion =  Main.rand.Next(new int[]{mod.ItemType("ProjConvergence_Maliwan"), mod.ItemType("ProjConvergence_Tediore"), mod.ItemType("ProjConvergence_Bandit")});
										}
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonSMG_Hyperion);
									}
								}
								if (closest.ZoneDesert && !closest.ZoneOverworldHeight)	
								{
									if (Main.rand.Next(0, Main.expertMode ? 90 : 112) == 0)
									{
										if (Main.rand.Next(3) == 0)
										{
											CommonSMG_Tediore =  Main.rand.Next(new int[]{mod.ItemType("SubMG_MaliwanFire"), mod.ItemType("SubMG_MaliwanCorrosive"), mod.ItemType("SubMG_MaliwanSlag"), mod.ItemType("SubMG_MaliwanShock")});
										}
										else
										{
											CommonSMG_Tediore =  Main.rand.Next(new int[]{mod.ItemType("SubMG_Maliwan"), mod.ItemType("SubMG_Bandit"), mod.ItemType("SubMG_TedioreBarrel")});
										}
											
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonSMG_Tediore);		
									}				
								}
								if (closest.ZoneSnow && !closest.ZoneOverworldHeight)			
								{
									if (Main.rand.Next(0, Main.expertMode ? 101 : 122) == 0)
									{
										CommonSMG_Maliwan =  Main.rand.Next(new int[]{mod.ItemType("Gospel_Corrosive"), mod.ItemType("Gospel_Fire"), mod.ItemType("Gospel_Shock"), mod.ItemType("Gospel_Slag")});
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonSMG_Maliwan);					
									}
								}
								if (closest.ZoneJungle)	
								{
									if (Main.rand.Next(0, Main.expertMode ? 90 : 112) == 0)
									{
										if (Main.rand.Next(3) == 0)
										{
											CommonSMG_Dahl =  Main.rand.Next(new int[]{mod.ItemType("SMG_DahlMaliwanFire"), mod.ItemType("SMG_DahlMaliwanSlag"), mod.ItemType("SMG_DahlMaliwanCorrosive"), mod.ItemType("SMG_DahlMaliwanShock")});
										}
										else
										{
											CommonSMG_Dahl =  Main.rand.Next(new int[]{mod.ItemType("SMG_DahlBandit"), mod.ItemType("SMG_DahlBarrel")});
										}
										Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, CommonSMG_Dahl);
									}
								}
							}							
						}
					}
				}
				#endregion
			}
		}
	}
}
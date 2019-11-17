using Terraria;
using Terraria.Utilities;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace GloriousGuns.Items.Dahl.PistolGreen
{
	public class Shrike_Fire : ModItem
	{
		public static string[] RandNames = { "Swift","Precise","Accurate","Instinctive","Innate", "Deadly", "Lethal" };
		protected ushort nameIndex;
		//protected int counter;

		public string WeaponName => "Raging " + RandNames[nameIndex%RandNames.Length]+" Shrike";
        public int maxBullets;
		public int elementalType;
        public int BurstFireRate;
		public int fireType = 1;
		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shrike");
            Tooltip.SetDefault("Right-click in inventory to toggle-between semi-auto and burst fire");
			GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Dahl/PistolGreen/Shrike_FireGlow");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 60;
			item.height = 32;
            item.scale = .9f;
			item.useStyle = 5;
			item.noMelee = true;
			item.useTurn = false;
			item.rare = 2;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

        	Generate();
		}
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (Shrike_Fire)base.Clone(itemClone);
			
			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.knockBack = item.knockBack;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.maxBullets = maxBullets;
			myClone.ApplyStats();

			return myClone;
		}
		//Behavior
         public override bool CanRightClick() 
         {
             return true;
         }
         public override bool ConsumeItem(Player player) 
         {
             return false;
         }
		public override void RightClick(Player player)
		{
			{
				if (fireType == 1)
				{
					item.useAnimation = BurstFireRate;
 					CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 10, player.width, player.height), new Color(255, 255, 255, 100),
                    "Burst Fire");
                	fireType++;			
				}
				else if (fireType == 2)
				{
					item.useAnimation = item.useTime;
 					CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 10, player.width, player.height), new Color(255, 255, 255, 100),
                    "Semi-Auto");
                	fireType--;				
				}
			}    
		}
    	public override void HoldItem(Player player)
        {
            if (fireType == 2)
				{
					item.useAnimation = BurstFireRate;
                    item.shootSpeed = (int)(BurstFireRate/2);
                    if (magazine > 0)
                    {
                        item.reuseDelay = 35;
                    }
                    if (magazine <= 0)
                    {
                        item.reuseDelay = 150;  
                    }
				}
				else if (fireType == 1)
				{
					item.useAnimation = item.useTime;			
				}
        }
		int magazine = 1;
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{	

            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,41)); 
            
            {
                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 38f;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                float spread = MathHelper.ToRadians(4f); //45 degrees converted to radians
                float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                double baseAngle = Math.Atan2(speedX,speedY);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                speedX = baseSpeed * (float)Math.Sin(randomAngle);
                speedY = baseSpeed * (float)Math.Cos(randomAngle);
                if (magazine <= 0)
                {
                    magazine = maxBullets;
                    Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,1));            
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 10, player.width, player.height), new Color(255, 255, 255, 100),
                        "Reloading!");	
                    Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Reload"));
                    item.reuseDelay = 120;
                    for (int index1 = 0; index1 < 5; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 263, 0.0f, 0.0f, (int) byte.MaxValue, new Color(), (float) GloriousGuns.instance.gloriousRNG.Next(20, 26) * 0.1f);
                        Main.dust[index2].noLight = true;
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].velocity *= 0.5f;
                        Main.dust[index2].scale *= .6f;
                    }
                }
                else
                {
                    item.noUseGraphic = false;
                    item.useStyle = 5;
                    item.reuseDelay = 0;

                }
                magazine--;
               	int projectileFired = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				Main.projectile[projectileFired].GetGlobalProjectile<GloriousGunsGProj>().shotFromFireWeaponCommon = true;
			 }
			return false;
		}
		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-10,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-48,-46f),null,new Color(24,196,55,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("GloriousGuns/Items/Dahl/PistolGreen/Shrike_FireGlow"),
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
        }
        	public override TagCompound Save() => new TagCompound {
			{ nameof(nameIndex),nameIndex },
			{ nameof(item.useTime),item.useTime },
			{ nameof(item.damage),item.damage },
			{ nameof(item.reuseDelay),item.reuseDelay },
			{ nameof(item.value),item.value },
			{ nameof(item.knockBack),item.knockBack },
			{ nameof(maxBullets),maxBullets },
			{ nameof(BurstFireRate),BurstFireRate },
			{ nameof(item.shootSpeed),item.shootSpeed }
		};
		public override void Load(TagCompound tag)
		{
			if(!tag.ContainsKey(nameof(nameIndex))) {
				return;
			}
			
			nameIndex = tag.Get<ushort>(nameof(nameIndex));
			item.useAnimation = item.useTime = tag.Get<int>(nameof(item.useTime));
			item.damage = tag.Get<int>(nameof(item.damage));
			item.reuseDelay = tag.Get<int>(nameof(item.reuseDelay));
			item.value = tag.Get<int>(nameof(item.value));
			BurstFireRate = tag.Get<int>(nameof(BurstFireRate));
			item.knockBack = tag.Get<float>(nameof(item.knockBack));
			item.shootSpeed = tag.Get<float>(nameof(item.shootSpeed));
			maxBullets = tag.Get<int>(nameof(maxBullets));

			ApplyStats();
		}

		//Net
		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(nameIndex);
			writer.Write(item.useTime);
			writer.Write(item.damage);
			writer.Write(item.reuseDelay);
			writer.Write(item.value);
			writer.Write(item.knockBack);
			writer.Write(item.shootSpeed);
			writer.Write(maxBullets);
			writer.Write(BurstFireRate);
		}
		public override void NetRecieve(BinaryReader reader)
		{
			nameIndex = reader.ReadUInt16();
			item.useAnimation = item.useTime = reader.ReadInt32();
			item.damage = reader.ReadInt32();
			item.reuseDelay = reader.ReadInt32();
			item.value = reader.ReadInt32();
			item.knockBack = reader.ReadSingle();
			item.shootSpeed = reader.ReadSingle();
			maxBullets = reader.ReadInt32();
			BurstFireRate = reader.ReadInt32();

			ApplyStats();
		}
		public void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			maxBullets = GloriousGuns.instance.gloriousRNG.Next(14, 19);
			BurstFireRate = item.useTime = GloriousGuns.instance.gloriousRNG.Next(52, 65);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(18, 23);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(11,14);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(0,1);
			item.value = item.damage * 2000;
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(10f,15f);

        	ApplyStats();
		}
        public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
            string maxBullet = "" + maxBullets;
		    var line2 = new TooltipLine(mod, "", "Reloads every " + maxBullet + " shots");     
            tooltips.Add(line2);   

			TooltipLine line = new TooltipLine(mod, "ItemName", "Incendiary Weapon\nAttacks deal ravaging fire damage");
            line.overrideColor = new Color(224, 64, 15);
            tooltips.Add(line);

            TooltipLine line1 = new TooltipLine(mod, "Damage", "Dahl");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);   
		}
	}
}
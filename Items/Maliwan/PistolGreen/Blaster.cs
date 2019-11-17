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

namespace GloriousGuns.Items.Maliwan.PistolGreen
{
	public class Blaster : ModItem
	{
		public static string[] RandNames = { "Luminous","Ecliptic","Aphelaic","Cosmic","Perihelaic", "Ionized", "Axial" };
		protected ushort nameIndex;
		//protected int counter;

		public string WeaponName =>  RandNames[nameIndex%RandNames.Length]+" Blaster";
        public int maxBullets;
		public int elementSecondary;
        public int elementPrimary;

    	public override bool CloneNewInstances => true;
		public int fireType = 1;
		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blaster");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 60;
			item.height = 32;
            item.scale = .85f;
			item.useStyle = 5;
			item.noMelee = true;
        	item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MaliwanShot1");
			item.useTurn = false;
			item.rare = 2;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

        	Generate();
		}
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (Blaster)base.Clone(itemClone);
			
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
        int dustType;
		//Behavior
         public override bool CanRightClick() 
         {
             return true;
         }
         public override bool ConsumeItem(Player player) 
         {
             return false;
         }
         public override void HoldItem (Player player)
         {
             if (elementPrimary <= 1 && fireType == 1)
			{
				GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Maliwan/PistolGreen/Blaster_FireGlow");
                dustType = 6;
			}
			if (elementPrimary >= 2 && fireType == 1)
			{
				GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Maliwan/PistolGreen/Blaster_CorrosiveGlow");
                dustType = 163;
			}
			if (elementSecondary <= 4 && fireType == 2)
			{
				GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Maliwan/PistolGreen/Blaster_ShockGlow");
                dustType = 226;
			}
			if (elementSecondary >= 5 && fireType == 2)
			{
				GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Maliwan/PistolGreen/Blaster_SlagGlow");
                dustType = 173;
			}
         }
		public override void RightClick(Player player)
		{
            if (elementPrimary <= 1)
			{
				elementalType = "Fire";
			}
			if (elementPrimary >= 2)
			{
				elementalType = "Corrosion";
			}
			if (elementSecondary <= 4)
			{
				elementalType2 = "Shock";
			}
			if (elementSecondary >= 5)
			{
				elementalType2 = "Slag";
			}
			{
				if (fireType == 1)
				{
					item.useAnimation = elementPrimary;
 					CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 10, player.width, player.height), new Color(255, 255, 255, 100),
                    elementalType2);
                	fireType++;			
				}
				else if (fireType == 2)
				{
					item.useAnimation = item.useTime;
 					CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 10, player.width, player.height), new Color(255, 255, 255, 100),
                    elementalType);
                	fireType--;				
				}
			}    
		}
		int magazine = 1;
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{	    
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
            }
            if (elementPrimary <= 1 && fireType == 1)
			{
                int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				Main.projectile[proj].GetGlobalProjectile<GloriousGunsGProj>().shotFromMaliwanFireCommon = true;
			}
			if (elementPrimary >= 2 && fireType == 1)
			{
                int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				Main.projectile[proj].GetGlobalProjectile<GloriousGunsGProj>().shotFromMaliwanAcidCommon = true;
			}
			if (elementSecondary <= 4  && fireType == 2)
			{
                int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				Main.projectile[proj].GetGlobalProjectile<GloriousGunsGProj>().shotFromMaliwanShockCommon = true;
			}
			if (elementSecondary >= 5  && fireType == 2)
			{
                int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				Main.projectile[proj].GetGlobalProjectile<GloriousGunsGProj>().shotFromMaliwanSlagCommon = true;
			}
            for (int index1 = 0; index1 < 5; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(position.X, position.Y), item.width - 64, item.height - 16, dustType, speedX, speedY, (int) byte.MaxValue, new Color(), (float) GloriousGuns.instance.gloriousRNG.Next(10, 17) * 0.1f);
                Main.dust[index2].noLight = true;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 0.5f;
                Main.dust[index2].scale *= .6f;
            }
			return false;
		}
		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-10,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-38,-46f),null,new Color(24,196,55,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}
        	public override TagCompound Save() => new TagCompound {
			{ nameof(nameIndex),nameIndex },
			{ nameof(item.useTime),item.useTime },
			{ nameof(item.damage),item.damage },
			{ nameof(item.reuseDelay),item.reuseDelay },
			{ nameof(item.value),item.value },
			{ nameof(item.knockBack),item.knockBack },
			{ nameof(maxBullets),maxBullets },
			{ nameof(elementPrimary),elementPrimary },
			{ nameof(elementSecondary),elementSecondary },
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
			elementPrimary = tag.Get<int>(nameof(elementPrimary));
			elementSecondary = tag.Get<int>(nameof(elementSecondary));
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
			writer.Write(elementPrimary);
			writer.Write(elementSecondary);
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
			elementPrimary = reader.ReadInt32();
			elementSecondary = reader.ReadInt32();

			ApplyStats();
		}
		public void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			maxBullets = GloriousGuns.instance.gloriousRNG.Next(8, 12);

			elementPrimary = GloriousGuns.instance.gloriousRNG.Next(0, 3);
			elementSecondary = GloriousGuns.instance.gloriousRNG.Next(3, 6);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(18, 23);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(10,13);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(0,1);
			item.value = item.damage * 2000;
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(2f,5f);

        	ApplyStats();
		}
        public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
		public string elementalType;
        public string elementalType2;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
            if (elementPrimary <= 1)
			{
				elementalType = "Fire";
			}
			if (elementPrimary >= 2)
			{
				elementalType = "Corrosion";
			}
			if (elementSecondary <= 4)
			{
				elementalType2 = "Shock";
			}
			if (elementSecondary >= 5)
			{
				elementalType2 = "Slag";
			}
            var line = new TooltipLine(mod, "", "Right-click in inventory to toggle between " + elementalType + " & " + elementalType2); 
            tooltips.Add(line);
            string maxBullet = "" + maxBullets;
		    var line2 = new TooltipLine(mod, "", "Reloads every " + maxBullet + " shots");     
            tooltips.Add(line2);   

            TooltipLine line1 = new TooltipLine(mod, "Damage", "Maliwan");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);   
        }
	}
}
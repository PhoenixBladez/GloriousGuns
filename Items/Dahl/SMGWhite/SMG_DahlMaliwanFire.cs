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
namespace GloriousGuns.Items.Dahl.SMGWhite
{
	public class SMG_DahlMaliwanFire : ModItem
	{
        private Vector2 newVect;
		public static string[] RandNames = { "Reserve","Smooth","Light","Burst Fire","React",};

		protected ushort nameIndex;
		//protected int counter;
		public int SMGBulletsMax;
		public string WeaponName => RandNames[nameIndex%RandNames.Length]+" Beetle";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SMG");
			Tooltip.SetDefault("Right click to zoom\nBurst fire when zoomed");
			GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Dahl/SMGWhite/SMG_DahlMaliwanFireGlow");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 36;
			item.height = 26;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item11;
			item.useTurn = false;
			item.rare = 1;
			item.autoReuse = true;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

			Generate();
		}
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (SMG_DahlMaliwanFire)base.Clone(itemClone);
			
			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.knockBack = item.knockBack;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.SMGBulletsMax = SMGBulletsMax;
			myClone.ApplyStats();

			return myClone;
		}
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string SMGClip = "" + SMGBulletsMax;
			var line = new TooltipLine(mod, "", "Reloads every " + SMGClip + " shots");     
            tooltips.Add(line);   
            TooltipLine line2 = new TooltipLine(mod, "ItemName", "Incendiary Weapon\nAttacks deal ravaging fire damage");
            line2.overrideColor = new Color(224, 64, 15);
            tooltips.Add(line2);
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Dahl");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		//Behavior
		int SMGBullets = 1;
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
			Vector2 origVect = new Vector2(speedX, speedY);
			SMGBullets--;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 38f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			float spread = MathHelper.ToRadians(23f); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			int projectileFired = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Main.projectile[projectileFired].GetGlobalProjectile<GloriousGunsGProj>().shotFromFireWeaponCommon = true;
			if (SMGBullets == 1)
			{
 				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(255, 255, 255, 100),
                    "Reloading!");	
		    	Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Reload"));
				item.reuseDelay = 90;
			}
            else
            {
                item.reuseDelay = 0;

            }
            if (SMGBullets <= 1)
            {
                SMGBullets = SMGBulletsMax;
            }
            if (Main.mouseRight)
			{
                if (SMGBullets > 3)
                {
                    SMGBullets--;
                }
                for (int index = 0; index < 58; ++index)
                {
                        if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 1)
                        {
                            player.inventory[index].stack -= 1;
                            break;
                        }
                }
			    Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,11));
                if (Main.rand.Next(2) == 1)
				{
					newVect = origVect.RotatedBy(System.Math.PI / (Main.rand.Next(82, 400) / 14));
				}
				else
				{
					newVect = origVect.RotatedBy(-System.Math.PI / (Main.rand.Next(82, 400) / 14));
				}
			int proj2 = Projectile.NewProjectile(position.X, position.Y, newVect.X, newVect.Y, type, damage, 0, player.whoAmI);
			Projectile newProj2 = Main.projectile[proj2];
 			Main.projectile[proj2].GetGlobalProjectile<GloriousGunsGProj>().shotFromFireWeaponCommon = true;           
			}
			return false;
		}

		//IO
				public override TagCompound Save() => new TagCompound {
			{ nameof(nameIndex),nameIndex },
			{ nameof(item.useTime),item.useTime },
			{ nameof(item.damage),item.damage },
			{ nameof(item.reuseDelay),item.reuseDelay },
			{ nameof(item.value),item.value },
			{ nameof(item.knockBack),item.knockBack },
			{ nameof(SMGBulletsMax),SMGBulletsMax },
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
			item.knockBack = tag.Get<float>(nameof(item.knockBack));
			item.shootSpeed = tag.Get<float>(nameof(item.shootSpeed));
			SMGBulletsMax = tag.Get<int>(nameof(SMGBulletsMax));

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
			writer.Write(SMGBulletsMax);
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
			SMGBulletsMax = reader.ReadInt32();

			ApplyStats();
		}
		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-5,0);
        	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("GloriousGuns/Items/Dahl/SMGWhite/SMG_DahlMaliwanFireGlow"),
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
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-41,-52f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(10, 12);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(5,6);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(0,1);
			item.value = GloriousGuns.instance.gloriousRNG.Next(11000,15000);
			SMGBulletsMax = GloriousGuns.instance.gloriousRNG.Next(40, 50);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(8f,13f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
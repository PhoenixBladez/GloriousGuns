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

namespace GloriousGuns.Items.Maliwan
{
	public class Aegis_DahlShock : ModItem
	{
		public static string[] RandNames = { "" };

		protected ushort nameIndex;
		//protected int counter;

		public string WeaponName => "Electrified Aegis";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aegis");
			Tooltip.SetDefault("Consumes 2 ammo per shot\nDeals bonus elemental damage");
            GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Maliwan/Aegis_Shock");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 46;
			item.height = 32;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item41;
			item.useTurn = false;
			item.rare = 2;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

			Generate();
		}
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (Aegis_DahlShock)base.Clone(itemClone);
			
			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.knockBack = item.knockBack;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.ApplyStats();

			return myClone;
		}
		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
            for (int index = 0; index < 58; ++index)
            {
                if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 1)
                {
                    player.inventory[index].stack -= 1;
                    break;
                }
            }           
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 40f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,41));
			float spread = MathHelper.ToRadians(9f); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			int projectileShockd = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Main.projectile[projectileShockd].GetGlobalProjectile<GloriousGunsGProj>(mod).shotFromMaliwanShockCommon = true;
			return false;
		}
		public override void HoldItem(Player player)
		{
			if (Main.mouseRight)
			{
				item.useAnimation = 12;
			}
		}
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("GloriousGuns/Items/Maliwan/Aegis_Shock"),
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
		//IO
		public override TagCompound Save() => new TagCompound {
			{ nameof(nameIndex),nameIndex },
			{ nameof(item.useTime),item.useTime },
			{ nameof(item.damage),item.damage },
			{ nameof(item.reuseDelay),item.reuseDelay },
			{ nameof(item.value),item.value },
			{ nameof(item.knockBack),item.knockBack },
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

			ApplyStats();
		}

		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-3,0);       
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line2 = new TooltipLine(mod, "ItemName", "Incendiary Weapon\nAttacks deal ravaging Shock damage");
            line2.overrideColor = new Color(224, 64, 15);
            tooltips.Add(line2);
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Maliwan");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		//Rendering
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.2f, 0.06f, 0.3f);
			Texture2D texture;
            texture = Main.itemTexture[item.type];
			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-45,-50f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public void Generate()
		{
			nameIndex = (ushort)new UnifiedRandom().Next(RandNames.Length);

			item.useAnimation = item.useTime = new UnifiedRandom().Next(25, 32);
			item.damage =  new UnifiedRandom().Next(9, 13);
			item.reuseDelay = new UnifiedRandom().Next(15, 21);
			item.knockBack =  new UnifiedRandom().Next(2,3);
			item.value = new UnifiedRandom().Next(5000, 8000);
			item.shootSpeed =  new UnifiedRandom().NextFloat(8f,13f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
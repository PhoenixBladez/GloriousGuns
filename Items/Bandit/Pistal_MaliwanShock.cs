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

namespace GloriousGuns.Items.Bandit
{
	public class Pistal_MaliwanShock : ModItem
	{
		public static string[] RandNames = { "Shooty","Hevy","Attakerz","Bigg","Greesy","Extnded","Killr" };

		protected ushort nameIndex;
		//protected int counter;

		public string WeaponName => "Shockerz " + RandNames[nameIndex%RandNames.Length] +" Pistal";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pistal");
			GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Bandit/Pistal_MaliwanShockGlow");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 36;
			item.height = 26;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item41;
			item.useTurn = false;
			item.rare = 1;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

			Generate();
		}
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (Pistal_MaliwanShock)base.Clone(itemClone);
			
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
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 50f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,41));
			float spread = MathHelper.ToRadians(5f); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			int projectileFired = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Main.projectile[projectileFired].GetGlobalProjectile<GloriousGunsGProj>(mod).shotFromShockWeaponCommon = true;
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
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "ItemName", "Shock Weapon\nElectric damage scales with the target's movement speed");
            line.overrideColor = new Color(29,100, 198);
            tooltips.Add(line);
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Bandit");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }

		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-10,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.6f, 0.2f, 0.4f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-40,-50f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);
			return true;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("GloriousGuns/Items/Bandit/Pistal_MaliwanShockGlow"),
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

		public void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(17, 26);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(10,14);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(1, 2);
			item.reuseDelay = GloriousGuns.instance.gloriousRNG.Next(12,18);
			item.value = GloriousGuns.instance.gloriousRNG.Next(1,5);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(6f,10f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
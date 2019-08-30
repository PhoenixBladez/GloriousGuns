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
using GloriousGuns.Items;
using System.Collections.Generic;

namespace GloriousGuns.Items.Hyperion
{
	public class Apparatus_Tediore : ModItem
	{
		public static string[] RandNames = { "Acquired","Standard","Galvanizing","Constructive","Auto Correcting","Solid","Rugged", "Expediting" };


		protected ushort nameIndex;
		public int accurateBullets = 1;
		//protected int counter;

		public string WeaponName => RandNames[nameIndex%RandNames.Length]+" Apparatus";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Apparatus");
			Tooltip.SetDefault("Firing increases accuracy\nAccuracy resets every 45 shots");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 46;
			item.height = 36;
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
			var myClone = (Apparatus_Tediore)base.Clone(itemClone);
			
			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.knockBack = item.knockBack;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.accurateBullets = accurateBullets;
            myClone.item.crit = item.crit;
			myClone.ApplyStats();

			return myClone;
		}
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Hyperion");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
			accurateBullets++;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,41));
			float spread = MathHelper.ToRadians(45f/accurateBullets); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			if (accurateBullets >= 45)
			{
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(25, 1));
                for (int index1 = 0; index1 < 5; ++index1)
                {
                int index2 = Dust.NewDust(player.position, player.width, player.height, 263, 0.0f, 0.0f, (int) byte.MaxValue, new Color(), (float) GloriousGuns.instance.gloriousRNG.Next(20, 26) * 0.1f);
                Main.dust[index2].noLight = true;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 0.5f;
                Main.dust[index2].scale *= .6f;
                }
				accurateBullets = 1;
			}
			return true;
		}
		public override void HoldItem(Player player)
		{
			if (Main.mouseRight)
			{
				item.useAnimation = 12;
			}
		}

		//IO
		public override TagCompound Save() => new TagCompound {
			{ nameof(nameIndex),nameIndex },
			{ nameof(item.useTime),item.useTime },
			{ nameof(item.damage),item.damage },
			{ nameof(item.reuseDelay),item.reuseDelay },
			{ nameof(item.value),item.value },
			{ nameof(item.knockBack),item.knockBack },
			{ nameof(accurateBullets),accurateBullets },
			{ nameof(item.crit),item.crit },
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
			accurateBullets = tag.Get<int>(nameof(accurateBullets));
            item.crit = tag.Get<int>(nameof(item.crit));
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
			writer.Write(accurateBullets);
            writer.Write(item.crit);
		}
		public override void NetRecieve(BinaryReader reader)
		{
			nameIndex = reader.ReadUInt16();
			item.useAnimation = item.useTime = reader.ReadInt32();
			item.damage = reader.ReadInt32();
			item.reuseDelay = reader.ReadInt32();
			accurateBullets = reader.ReadInt32();
			item.value = reader.ReadInt32();
			item.crit = reader.ReadInt32();
			item.knockBack = reader.ReadSingle();
			item.shootSpeed = reader.ReadSingle();

			ApplyStats();
		}

		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-3,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-45,-50f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(6, 10);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(10,14);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(1,2);
			item.value = GloriousGuns.instance.gloriousRNG.Next(5000,10000);
            item.reuseDelay = GloriousGuns.instance.gloriousRNG.Next(3,8);
            item.crit = GloriousGuns.instance.gloriousRNG.Next(3, 5);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(8f,13f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
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

namespace GloriousGuns.Items.Hyperion.ShotgunWhite
{
	public class ProjDiversification : ModItem
	{
        private Vector2 newVect;
		public static string[] RandNames = { "Acquired","Galvanizing","Constructive","Solid","Rugged" };

		protected ushort nameIndex;
		public int accurateBullets = 1;
		//protected int counter;

		public string WeaponName => RandNames[nameIndex%RandNames.Length]+" Projectile Diversification";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Projectile Diversification");
			Tooltip.SetDefault("Firing increases accuracy\nAccuracy resets every 10 shots");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 66;
			item.height = 28;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item36;
			item.useTurn = false;
			item.rare = 1;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

			Generate();
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Hyperion");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (ProjDiversification)base.Clone(itemClone);
			
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

		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
		accurateBullets++;
			Vector2 origVect = new Vector2(speedX, speedY);
			for (int X = 0; X <= 3; X++)
			{
			float spread = MathHelper.ToRadians(122f/accurateBullets); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			int proj2 = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Projectile newProj2 = Main.projectile[proj2];
			}
			if (accurateBullets >= 12)
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
		public override Vector2? HoldoutOffset() => new Vector2(-16,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-36,-56f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(35, 45);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(9,13);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(3,6);
			item.value = GloriousGuns.instance.gloriousRNG.Next(12000,20000);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(5f,9f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
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

namespace GloriousGuns.Items.Torgue
{
	public class HandCannon_Dahl : TorgueGun
	{
		protected ushort nameIndex;
		//protected int counter;

		public override string[] RandNames
		{
			get => new string[] {"Lumpy", "Slippery", "Straight", "Hard", "Easy", "Thick", "Wanton"};
		}
		public override string WeaponName => RandNames[nameIndex%RandNames.Length]+" Hand Cannon";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hand Cannon");
			Tooltip.SetDefault("Consumes 2 ammo per shot");
		}

		public override void NewSetDefaults()
		{
			item.ranged = true;
			item.width = 42;
			item.height = 32;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item11;
			item.useTurn = false;
			item.rare = 2;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

			Generate();
		}

		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
          for (int index = 0; index < 58; ++index)
          {
             if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 1)
             {
                 player.inventory[index].stack--;
				 break;
             }
          }
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY -1)) * 40f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,11));
			float spread = MathHelper.ToRadians(18f); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			type = mod.ProjectileType("TorgueRevolverBullet_Common");
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			return true;
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

		public override void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(16, 25);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(16,21);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(2,4);
			item.value = GloriousGuns.instance.gloriousRNG.Next(1000,2500);
            item.reuseDelay = GloriousGuns.instance.gloriousRNG.Next(15,25);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(.2f,1.2f);
		}
	}
}
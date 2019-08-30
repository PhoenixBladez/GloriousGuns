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

namespace GloriousGuns.Items.Torgue.ShotgunWhite
{
	public class Stalker : TorgueGun
	{
		public override string[] RandNames => new string[] { "Lumpy","Straight","Hard","Easy","Thick" };

		protected ushort nameIndex;
		//protected int counter;
		public override string WeaponName => RandNames[nameIndex%RandNames.Length]+" Stalker";
		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stalker");
		}
		public override void NewSetDefaults()
		{
			item.ranged = true;
			item.width = 74;
			item.height = 28;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item36;
			item.useTurn = false;
			item.rare = 2;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
		}

		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			for (int X = 0; X <= 3; X++)
			{
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,11));
			float spread = MathHelper.ToRadians(42f); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			type = mod.ProjectileType("TorgueShotgunBullet_Common");
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
             int proj2 = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}

		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-16,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-35,-56f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public override void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(32, 42);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(10,14);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(6,9);
			item.value = GloriousGuns.instance.gloriousRNG.Next(10000,25000);
            item.reuseDelay = GloriousGuns.instance.gloriousRNG.Next(20,30);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(.2f,1.2f);
		}
	}
}
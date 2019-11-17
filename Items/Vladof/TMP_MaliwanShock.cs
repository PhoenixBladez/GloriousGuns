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

namespace GloriousGuns.Items.Vladof
{
	public class TMP_MaliwanShock : BaseGun
	{
		public override string[] RandNames => new string[] { "Woeful","Despair","Oppressed","Grim","Angry" };

		//protected int counter;

		public override string WeaponName => "Discharge " + RandNames[nameIndex%RandNames.Length]+" TMP";

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("TMP");
            GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Vladof/TMP_MaliwanShockGlow");
		}

		public override void NewSetDefaults()
		{
			item.ranged = true;
			item.width = 46;
			item.height = 28;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item11;
			item.useTurn = false;
			item.rare = 1;
			item.autoReuse = true;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
		}
		
		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 40f;
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
            int projectileShockd = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Main.projectile[projectileShockd].GetGlobalProjectile<GloriousGunsGProj>().shotFromShockWeaponCommon = true;
			return false;
		}

		//Rendering
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("GloriousGuns/Items/Vladof/TMP_MaliwanShockGlow"),
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

		public override Vector2? HoldoutOffset() => new Vector2(-3,0);

		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-45,-56f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "ItemName", "Shock Weapon\nElectric damage scales with the target's movement speed");
            line.overrideColor = new Color(29,100, 198);
            tooltips.Add(line);
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Vladof");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }

		public override void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(8, 14);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(5,9);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(0,1);
			item.value = GloriousGuns.instance.gloriousRNG.Next(2000,5000);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(8f,13f);
		}
	}
}
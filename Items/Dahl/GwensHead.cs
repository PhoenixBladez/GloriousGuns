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

namespace GloriousGuns.Items.Dahl
{
	public class GwensHead : BaseGun
	{
		public override string[] RandNames => new string[]{ "Reserve","Smooth","Light","Heavy","React","Skirmish","Blitz" };
		public override string WeaponName => RandNames[nameIndex%RandNames.Length]+" Gwen's Head";

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gwen's Head");
			Tooltip.SetDefault("Right click to zoom\nBurst fire when zoomed\nCritical hits deal 38% more damage");
		}
		public override void NewSetDefaults()
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
		}
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
		    TooltipLine line2 = new TooltipLine(mod, "Damage", "Thinking outside the box.");
            line2.overrideColor = new Color(204, 55, 55);
            tooltips.Add(line2);
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Dahl");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float crit)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 1f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,41));
			float spread = MathHelper.ToRadians(4f); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, crit, player.whoAmI);
			Main.projectile[proj].GetGlobalProjectile<GloriousGunsGProj>(mod).shotFromGwensHead = true;
			return false;
		}
		public override void HoldItem(Player player)
		{
			if (Main.mouseRight)
			{
				item.useAnimation = 65;
                item.reuseDelay = 26;
			}
        }
		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-3,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-45,-52f),null,new Color(21,110,224,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public override void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(9, 11);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(7,9);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(0, 1);
			item.value = item.damage * 1200;
            item.reuseDelay = GloriousGuns.instance.gloriousRNG.Next(13,18);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(7f,11f);
		}
	}
}
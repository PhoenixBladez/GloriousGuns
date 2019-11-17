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
	public class TBallFists : BaseGun
	{
		public override string[] RandNames => new string[]{ "Punctilious","Elegant","Surfeit","Potent","Expeditious" };
		public override string WeaponName => RandNames[nameIndex%RandNames.Length]+" Thunderball Fists";
        public float accuracy = 8f;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thunderball Fists");
			Tooltip.SetDefault("Consumes 2 ammo per shot\nDeals bonus elemental damage");
            GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Maliwan/TBallFists_Glow");
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
			item.rare = 4;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
		}
          public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("GloriousGuns/Items/Maliwan/TBallFists_Glow"),
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
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "ItemName", "Shock Weapon\nElectric damage scales with the target's movement speed");
            line.overrideColor = new Color(29,100, 198);
            tooltips.Add(line);
		    TooltipLine line2 = new TooltipLine(mod, "Damage", "I can have such a thing?");
            line2.overrideColor = new Color(204, 55, 55);
            tooltips.Add(line2);
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Maliwan");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float crit)
		{
              for (int index = 0; index < 58; ++index)
            {
                if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 1)
                {
                    player.inventory[index].stack -= 1;
                    break;
                }
            }     
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 45f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,41));
			float spread = MathHelper.ToRadians(accuracy); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
            type = mod.ProjectileType("ThunderballFistsProjectile");
			return true;
		}
		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-3,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-45,-52f),null,new Color(247,149,12,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public override void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(10, 14);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(5,8);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(0, 2);
			item.value = item.damage * 2800;
            item.reuseDelay = GloriousGuns.instance.gloriousRNG.Next(13,15);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(10f,16f);
		}
	}
}
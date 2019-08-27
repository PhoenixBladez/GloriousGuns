using Terraria;
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
	public class Repeater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Basic Repeater");
			Tooltip.SetDefault("Fires two bullets inaccurately\n'My first gun!'");
		}


		public override void SetDefaults()
		{
			item.damage = 6;
			item.ranged = true;
			item.width = 36;
			item.height = 26;
			item.useTime = 7;
			item.useAnimation = 9;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 1;
			item.reuseDelay = 35;
            item.UseSound = SoundID.Item41;
			item.useTurn = false;
			item.value = Item.sellPrice(0, 0, 3, 0);
			item.rare = 0;
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Bullet;
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
			Lighting.AddLight(item.position, 0.15f, 0.48f, 0.5f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				Main.extraTexture[60],
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f - 50,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f - 60f
				),
				new Microsoft.Xna.Framework.Rectangle?(),
				new Color(150, 150, 150, 0),
				0f,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
            return true;
		}////
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Dahl");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
        int counter;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 41));
            float spread = 10 * 0.0174f;//45 degrees converted to radians
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double baseAngle = Math.Atan2(speedX, speedY);
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            speedX = baseSpeed * (float)Math.Sin(randomAngle);
            speedY = baseSpeed * (float)Math.Cos(randomAngle);
            return true;
		}

	}
}

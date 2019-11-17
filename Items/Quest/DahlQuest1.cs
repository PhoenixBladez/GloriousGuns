using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace GloriousGuns.Items.Quest
{
    public class DahlQuest1: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ECHO Log #1");
            Tooltip.SetDefault("Stand straight, maggot!\nThe Dahl Corporation wants Life Crystals, stat!\nCollect and use two or more. That's an order!\nFinish the mission for your rations, recruit!");
			GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Quest/EchoRecorder_Glow");

        }


        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 36;

			item.value = Item.sellPrice(0, 0, 15, 0);
            item.rare = -11;
        }
		public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "ItemName", "Quest- Dahl");
            line.overrideColor = new Color(84, 252, 255);
            tooltips.Add(line);
        } 
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.2f, 0.26f, 0.53f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("GloriousGuns/Items/Quest/EchoRecorder_Glow"),
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
		}////
    }
}

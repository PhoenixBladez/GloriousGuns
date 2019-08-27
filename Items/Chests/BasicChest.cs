using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Items.Chests
{
    public class BasicChest: ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dahl Supply Chest");
			Tooltip.SetDefault("Right click to open\n'Dahl's Vault Hunter Starter Kit guarantees effectiveness! Or not.'");
		}


        public override void SetDefaults()
        {
            item.width = item.height = 30;
            item.rare = 2;
			item.useStyle = 1;
			item.consumable = true;
            item.maxStack = 99;
            item.useTime = item.useAnimation = 20;
			item.useAnimation = 15;
			item.useTime = 10;
            item.noMelee = true;
            item.autoReuse = false;

        }


       public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(mod.ItemType("Repeater"));
			{
			int Booty = Main.rand.Next(70,120);
			for (int j = 0; j < Booty; j++)
			{
			player.QuickSpawnItem(mod.ItemType("BasicBullet"));
			}
            }
			int Coins = Main.rand.Next(10,25);
			for (int K = 0; K < Coins; K++)
			player.QuickSpawnItem(72);
			
		
        }
    }
}

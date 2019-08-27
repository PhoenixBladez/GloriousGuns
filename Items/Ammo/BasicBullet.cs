using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GloriousGuns.Items.Ammo
{
	public class BasicBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Standard Round");
			Tooltip.SetDefault("'Basic Dahl ammunition'\n'Effective evisceration guaranteed!'");
		}


		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
            item.value = 20;
            item.rare = 0;
            item.value = Item.buyPrice(0, 0, 8, 0);
            item.maxStack = 999;

            item.damage = 5;
			item.knockBack = 1.5f;
            item.ammo = AmmoID.Bullet;

            item.ranged = true;
            item.consumable = true;

            item.shoot = mod.ProjectileType("BasicBullet_Proj");
			item.shootSpeed = 4f;

		}
    }
}
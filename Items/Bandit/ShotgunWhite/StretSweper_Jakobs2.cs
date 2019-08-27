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

namespace GloriousGuns.Items.Bandit.ShotgunWhite
{
	public class StretSweper_Jakobs2 : ModItem
	{
        private Vector2 newVect;
		public static string[] RandNames = { "Bandity","Lite","Hevy","Redy","Hurty","Skavengd" };

		protected ushort nameIndex;
		//protected int counter;

		public string WeaponName => RandNames[nameIndex%RandNames.Length]+" Stret Sweper";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stret Sweper");
            Tooltip.SetDefault("Consumes 2 ammo per shot");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 66;
			item.height = 26;
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
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (StretSweper_Jakobs2)base.Clone(itemClone);
			
			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.knockBack = item.knockBack;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.ApplyStats();

			return myClone;
		}

		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
            for (int index = 0; index < 58; ++index)
            {
                    if (player.inventory[index].ammo == item.useAmmo && player.inventory[index].stack > 1)
                    {
                        player.inventory[index].stack -= 1;
                        break;
                    }
            }
			Vector2 origVect = new Vector2(speedX, speedY);
			for (int X = 0; X <= 5; X++)
			{
				if (Main.rand.Next(2) == 1)
				{
					newVect = origVect.RotatedBy(System.Math.PI / (Main.rand.Next(40, 200) / 8));
				}
				else
				{
					newVect = origVect.RotatedBy(-System.Math.PI / (Main.rand.Next(40, 200) / 8));
				}
			int proj2 = Projectile.NewProjectile(position.X, position.Y, newVect.X, newVect.Y, type, damage, knockBack, player.whoAmI);
			Projectile newProj2 = Main.projectile[proj2];
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
			item.knockBack = tag.Get<float>(nameof(item.knockBack));
			item.shootSpeed = tag.Get<float>(nameof(item.shootSpeed));

			ApplyStats();
		}
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Bandit");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
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
		}
		public override void NetRecieve(BinaryReader reader)
		{
			nameIndex = reader.ReadUInt16();
			item.useAnimation = item.useTime = reader.ReadInt32();
			item.damage = reader.ReadInt32();
			item.reuseDelay = reader.ReadInt32();
			item.value = reader.ReadInt32();
			item.knockBack = reader.ReadSingle();
			item.shootSpeed = reader.ReadSingle();

			ApplyStats();
		}

		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-10,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-34,-56f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public void Generate()
		{
			nameIndex = (ushort)new UnifiedRandom().Next(RandNames.Length);

			item.useAnimation = item.useTime = new UnifiedRandom().Next(32, 44);
			item.damage =  new UnifiedRandom().Next(10,13);
			item.knockBack =  new UnifiedRandom().Next(6,8);
			item.reuseDelay = new UnifiedRandom().Next(18,30);
			item.value = new UnifiedRandom().Next(11000,18000);
			item.shootSpeed =  new UnifiedRandom().NextFloat(4f,7f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
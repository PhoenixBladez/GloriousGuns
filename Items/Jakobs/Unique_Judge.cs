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

namespace GloriousGuns.Items.Jakobs
{
	public class Unique_Judge : ModItem
	{
		public static string[] RandNames = { "Filled","Slick","Fine","Ornery","Leather","Fast" };

		protected ushort nameIndex;
		public int revolverBullets = 9;
		//protected int counter;

		public string WeaponName => RandNames[nameIndex%RandNames.Length]+" Judge";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Judge");
			Tooltip.SetDefault("Fires as fast as you can pull the trigger\nReloads every "+ 8 + " shots\nCritical hits deal 63% more damage");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 52;
			item.height = 38;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item41;
            item.knockBack = 1f;
			item.useTurn = false;
			item.rare = 3;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

			Generate();
		}
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (Unique_Judge)base.Clone(itemClone);
			
			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.crit = item.crit;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.revolverBullets = revolverBullets;
			myClone.ApplyStats();

			return myClone;
		}
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line1 = new TooltipLine(mod, "Damage", "I am free now.");
            line1.overrideColor = new Color(204, 55, 55);
            tooltips.Add(line1);
            TooltipLine line12= new TooltipLine(mod, "CritChance", "Jakobs");
            line12.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line12);
        }
		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float crit)
		{
			revolverBullets--;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 38f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			float spread = MathHelper.ToRadians(16f/revolverBullets); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			if (revolverBullets == 1)
			{
 				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 10, player.width, player.height), new Color(255, 255, 255, 100),
                    "Reloading!");	
		    	Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Reload"));
				item.reuseDelay = 100;
			}
            else
            {
                item.reuseDelay = 0;
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,41));
            }
            if (revolverBullets <= 1)
            {
                revolverBullets = 9;
            }
            int projectileSlagd = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, crit, player.whoAmI);
			Main.projectile[projectileSlagd].GetGlobalProjectile<GloriousGunsGProj>(mod).shotFromJudge = true;
			return false;
		}
		//IO
		public override TagCompound Save() => new TagCompound {
			{ nameof(nameIndex),nameIndex },
			{ nameof(item.useTime),item.useTime },
			{ nameof(item.damage),item.damage },
			{ nameof(item.reuseDelay),item.reuseDelay },
			{ nameof(item.value),item.value },
			{ nameof(item.crit),item.crit },
			{ nameof(revolverBullets),revolverBullets },
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
			item.crit = tag.Get<int>(nameof(item.crit));
			item.shootSpeed = tag.Get<float>(nameof(item.shootSpeed));
			revolverBullets = tag.Get<int>(nameof(revolverBullets));

			ApplyStats();
		}

		//Net
		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(nameIndex);
			writer.Write(item.useTime);
			writer.Write(item.damage);
			writer.Write(item.reuseDelay);
			writer.Write(item.value);
			writer.Write(item.crit);
			writer.Write(item.shootSpeed);
			writer.Write(revolverBullets);
		}
		public override void NetRecieve(BinaryReader reader)
		{
			nameIndex = reader.ReadUInt16();
			item.useAnimation = item.useTime = reader.ReadInt32();
			item.damage = reader.ReadInt32();
			item.reuseDelay = reader.ReadInt32();
			item.value = reader.ReadInt32();
			item.crit = reader.ReadInt32();
			item.shootSpeed = reader.ReadSingle();
			revolverBullets = reader.ReadInt32();

			ApplyStats();
		}

		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-7,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-40,-48f),null,new Color(21,110,224,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public void Generate()
		{
			nameIndex = (ushort)new UnifiedRandom().Next(RandNames.Length);

			item.useAnimation = item.useTime = new UnifiedRandom().Next(4, 6);
			item.damage =  new UnifiedRandom().Next(20, 22);
			item.crit =  new UnifiedRandom().Next(1, 3);
			item.value = new UnifiedRandom().Next(120000, 125000);
			item.shootSpeed =  new UnifiedRandom().NextFloat(8f,9f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
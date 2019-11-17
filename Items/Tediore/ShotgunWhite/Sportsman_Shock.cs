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
using GloriousGuns.Items;
using System.Collections.Generic;


namespace GloriousGuns.Items.Tediore.ShotgunWhite
{
	public class Sportsman_Shock : ModItem
	{
		public static string[] RandNames = { "Value","Target","Bulk", "Genuine", "Top-Notch" };
        private Vector2 newVect;
		protected ushort nameIndex;
		public int shotgunBullets = 6;
		//protected int counter;

		public string WeaponName => "Blue Light " + RandNames[nameIndex%RandNames.Length]+" Sportsman";

		public override bool CloneNewInstances => false;

		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sportsman");
			Tooltip.SetDefault("Thrown like a grenade when reloaded\nReloads every "+ 4 + " shots");
			GloriousGunsGlowmask.AddGlowMask(item.type, "GloriousGuns/Items/Tediore/ShotgunWhite/Sportsman_ShockGlow");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 64;
			item.height = 20;
			item.useStyle = 5;
			item.noMelee = true;
			item.useTurn = false;
			item.rare = 2;
			item.autoReuse = false;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

			Generate();
		}
		public override ModItem Clone(Item itemClone)
		{
			var myClone = (Sportsman_Shock)base.Clone(itemClone);
			
			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.knockBack = item.knockBack;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.shotgunBullets = shotgunBullets;
			myClone.ApplyStats();

			return myClone;
		}

		//Behavior
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
			shotgunBullets--;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 40f;
	    	Vector2 origVect = new Vector2(speedX, speedY);
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (shotgunBullets == 1)
			{
				item.useStyle = 1;
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,1));
                item.noUseGraphic = true;
				type = mod.ProjectileType("Sportsman_Proj");
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(25, 1));
                for (int index1 = 0; index1 < 5; ++index1)
                {
                int index2 = Dust.NewDust(player.position, player.width, player.height, 263, 0.0f, 0.0f, (int) byte.MaxValue, new Color(), (float) GloriousGuns.instance.gloriousRNG.Next(20, 26) * 0.1f);
                Main.dust[index2].noLight = true;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 0.5f;
                Main.dust[index2].scale *= .6f;
                }
                int proj2 = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage /2 * 3, knockBack, player.whoAmI);
				Main.projectile[proj2].GetGlobalProjectile<GloriousGunsGProj>().shotFromShockWeaponCommon = true;
			}
            else
            {
                item.useStyle = 5;
                item.noUseGraphic = false;
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,36));
                for (int X = 0; X <= 3; X++)
                {
                    if (Main.rand.Next(2) == 1)
                    {
                        newVect = origVect.RotatedBy(System.Math.PI / (Main.rand.Next(80, 800) / 8));
                    }
                    else
                    {
                        newVect = origVect.RotatedBy(-System.Math.PI / (Main.rand.Next(80, 800) / 8));
                    }
                int proj2 = Projectile.NewProjectile(position.X, position.Y, newVect.X, newVect.Y, type, damage, knockBack, player.whoAmI);
                Projectile newProj2 = Main.projectile[proj2];
				Main.projectile[proj2].GetGlobalProjectile<GloriousGunsGProj>().shotFromShockWeaponCommon = true;
                }
            }
            if (shotgunBullets <= 1)
            {
                shotgunBullets = 6;
            }
			return false;
		}
		public override void HoldItem(Player player)
		{
			if (shotgunBullets == 1)
			{
                item.shootSpeed = 2f;
			}
        }

		//IO
		public override TagCompound Save() => new TagCompound {
			{ nameof(nameIndex),nameIndex },
			{ nameof(item.useTime),item.useTime },
			{ nameof(item.damage),item.damage },
			{ nameof(item.reuseDelay),item.reuseDelay },
			{ nameof(item.value),item.value },
			{ nameof(item.knockBack),item.knockBack },
			{ nameof(shotgunBullets),shotgunBullets },
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
			shotgunBullets = tag.Get<int>(nameof(shotgunBullets));

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
			writer.Write(item.knockBack);
			writer.Write(item.shootSpeed);
			writer.Write(shotgunBullets);
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
			shotgunBullets = reader.ReadInt32();

			ApplyStats();
		}

		//Rendering
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "ItemName", "Shock Weapon\nElectric damage scales with the target's movement speed");
            line.overrideColor = new Color(29,100, 198);
            tooltips.Add(line);
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Tediore");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		public override Vector2? HoldoutOffset() => new Vector2(-7,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-38,-62f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("GloriousGuns/Items/Tediore/ShotgunWhite/Sportsman_ShockGlow"),
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
		public void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(28, 38);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(8,12);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(5, 8);
			item.value = GloriousGuns.instance.gloriousRNG.Next(6000, 18000);
            item.reuseDelay = GloriousGuns.instance.gloriousRNG.Next(15,20);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(5f,7f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
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

namespace GloriousGuns.Items.Bandit.SMGWhite
{
	public class SMG_Bandit : ModItem
	{
		public static string[] RandNames = { "Bandity","Greesy","Lite","Jery Riged","Redy","Extnded","Killr" };

		protected ushort nameIndex;
		public int SMGBulletsMax;
		//protected int counter;

		public string WeaponName => RandNames[nameIndex%RandNames.Length]+" Smig";

		public override bool CloneNewInstances => false;
		//Stats
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Smig");
		}
		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 36;
			item.height = 26;
			item.useStyle = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item11;
			item.useTurn = false;
			item.rare = 1;
			item.autoReuse = true;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;

			Generate();
		}
			public override ModItem Clone(Item itemClone)
		{
			var myClone = (SMG_Bandit)base.Clone(itemClone);
			
			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.knockBack = item.knockBack;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.SMGBulletsMax = SMGBulletsMax;
			myClone.ApplyStats();

			return myClone;
		}

		//Behavior


		//IO
		int SMGBullets = 1;
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
			SMGBullets--;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY - 2)) * 38f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			float spread = MathHelper.ToRadians(22f); //45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX,speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			if (SMGBullets == 1)
			{
 				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 10, player.width, player.height), new Color(255, 255, 255, 100),
                    "Reloading!");	
		    	Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Reload"));
				item.reuseDelay = 100;
			}
            else
            {
                item.reuseDelay = 0;

            }
            if (SMGBullets <= 1)
            {
                SMGBullets = SMGBulletsMax;
            }
			return true;
		}
	  	public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string SMGClip = "" + SMGBulletsMax;
			var line = new TooltipLine(mod, "", "Reloads every " + SMGClip + " shots");     
            tooltips.Add(line);       
            TooltipLine line1 = new TooltipLine(mod, "Damage", "Bandit");
            line1.overrideColor = new Color(176, 157, 127);
            tooltips.Add(line1);
        }
		//Net
				public override TagCompound Save() => new TagCompound {
			{ nameof(nameIndex),nameIndex },
			{ nameof(item.useTime),item.useTime },
			{ nameof(item.damage),item.damage },
			{ nameof(item.reuseDelay),item.reuseDelay },
			{ nameof(item.value),item.value },
			{ nameof(item.knockBack),item.knockBack },
			{ nameof(SMGBulletsMax),SMGBulletsMax },
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
			SMGBulletsMax = tag.Get<int>(nameof(SMGBulletsMax));

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
			writer.Write(SMGBulletsMax);
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
			SMGBulletsMax = reader.ReadInt32();

			ApplyStats();
		}
		//Rendering
		public override Vector2? HoldoutOffset() => new Vector2(-10,0);
		public override bool PreDrawInWorld(SpriteBatch spriteBatch,Color lightColor,Color alphaColor,ref float rotation,ref float scale,int whoAmI)
		{
			Lighting.AddLight(item.position,0.45f,0.48f,0.5f);
			Texture2D texture = Main.itemTexture[item.type];

			spriteBatch.Draw(Main.extraTexture[60],item.position-Main.screenPosition+item.Size*0.5f+new Vector2(-40,-50f),null,new Color(150,150,150,0),0f,texture.Size() * 0.5f,scale,SpriteEffects.None,0f);

			return true;
		}

		public void Generate()
		{
			nameIndex = (ushort)GloriousGuns.instance.gloriousRNG.Next(RandNames.Length);

			item.useAnimation = item.useTime = GloriousGuns.instance.gloriousRNG.Next(8, 10);
			item.damage =  GloriousGuns.instance.gloriousRNG.Next(4,6);
			item.knockBack =  GloriousGuns.instance.gloriousRNG.Next(0,2);
			item.value = GloriousGuns.instance.gloriousRNG.Next(12000,15000);
			SMGBulletsMax = GloriousGuns.instance.gloriousRNG.Next(110, 130);
			item.shootSpeed =  GloriousGuns.instance.gloriousRNG.NextFloat(6f,8f);

			ApplyStats();
		}
		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using GloriousGuns.NPCs;

namespace GloriousGuns
{
	public class MyPlayer : ModPlayer
	{
		public bool slagged = false;
		public bool corrosive = false;
		public bool incendiary = false;
		public bool shock = false;

		public bool firstLootCache = false;
		public override void ResetEffects()
		{
			slagged = false;
			corrosive = false;
			incendiary = false;
			shock = false;
		}
		public override void PreUpdate()
		{
			if (player.statLifeMax2 >= 200 && !firstLootCache)
			{
				firstLootCache = true;
				player.QuickSpawnItem(mod.ItemType("BasicChest"));
 				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 60, player.width, player.height), new Color(29, 240, 255, 100),
                    "Loot Cache Obtained!");				
			}
		}
		public override void PostUpdate()
		{
			if (player.HeldItem.type == mod.ItemType("Repeater_Dahl") ||
				player.HeldItem.type == mod.ItemType("Repeater_DahlBarrel") ||
				player.HeldItem.type == mod.ItemType("Repeater_MaliwanShock") ||
				player.HeldItem.type == mod.ItemType("Repeater_MaliwanFire") || 
				player.HeldItem.type == mod.ItemType("Repeater_MaliwanSlag") ||
				player.HeldItem.type == mod.ItemType("Repeater_MaliwanCorrosive") || 
				player.HeldItem.type == mod.ItemType("SMG_DahlBarrel") ||
				player.HeldItem.type == mod.ItemType("SMG_DahlBandit") ||
				player.HeldItem.type == mod.ItemType("SMG_DahlMaliwanFire") ||
				player.HeldItem.type == mod.ItemType("SMG_DahlMaliwanShock") ||
				player.HeldItem.type == mod.ItemType("SMG_DahlMaliwanSlag") ||
				player.HeldItem.type == mod.ItemType("SMG_DahlMaliwanCorrosive"))
			{
				player.scope = true;	
			}
		}
		public override TagCompound Save()
		{
			TagCompound tag = new TagCompound();
			tag.Add("firstLootCache", firstLootCache);
			return tag;
		}
		public override void Load(TagCompound tag)
		{
			firstLootCache = tag.GetBool("firstLootCache");
		}
		public override void SetupStartInventory(IList<Item> items)
		{
			Item item = new Item();
			item.SetDefaults(mod.ItemType("DahlQuest1"));
			items.Add(item);
		}

 		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) 
		{
			if (slagged)
			{
				r *= .76f;
				g *= .37f;
				b *= .67f;
                for (int k = 0; k < 2; k++)
                {
                Dust.NewDust(player.position, player.width, player.height, 98, .5f * player.direction, 0f, 0, new Microsoft.Xna.Framework.Color(), .37f);
                }
			}
			if (corrosive)
			{
				r *= .6f;
				g *= .75f;
				b *= .2f;
                for (int k = 0; k < 2; k++)
                {
                Dust.NewDust(player.position, player.width, player.height, 163, .5f * player.direction, Main.rand.NextFloat(-0.9f, .6f), 0, new Microsoft.Xna.Framework.Color(), .37f);
                }
			}
			if (incendiary)
			{
				r *= 1.3f;
				g *= .55f;
				b *= .3f;
                for (int k = 0; k < 2; k++)
                {
                	int d = Dust.NewDust(player.position, player.width, player.height, 6, .5f * player.direction, Main.rand.NextFloat(-1.8f, 1.8f), 0, new Microsoft.Xna.Framework.Color(), Main.rand.NextFloat(.2f, 1f));
				}
			}
			if (shock)
			{
                for (int k = 0; k < 2; k++)
                {
                	int d = Dust.NewDust(player.position, player.width, player.height, 226, .5f * player.direction, Main.rand.NextFloat(-1.8f, 1.8f), 0, new Microsoft.Xna.Framework.Color(), Main.rand.NextFloat(.2f, 1f));
				}
			}
        }
 		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		 {
			if (slagged && !npc.HasBuff(mod.BuffType("SlagDebuff")))
			{
				damage *= 2;
			}
		 }
		public override void UpdateBadLifeRegen()
		{
			int before = player.lifeRegen;
			bool drain = false;
			if (corrosive)
			{
				drain = true;
				player.lifeRegen -= player.statLifeMax2 * 2 / player.statLife;
				
			}
			if (incendiary)
			{
				drain = true;
				player.lifeRegen -= 12;
			}
			if (shock)
			{
				int moveDamage = 4 * (int)(player.velocity.X*1.25f	);
				if (moveDamage < 0)
				{
					moveDamage = moveDamage * -1;
				}
				if (moveDamage == 0)
				{
					moveDamage = 2;
				}
				drain = true;
				player.lifeRegen -= moveDamage;
			}
			if (drain && before > 0)
			{
				player.lifeRegenTime = 0;
				player.lifeRegen -= before;
			}
		}
	}
}

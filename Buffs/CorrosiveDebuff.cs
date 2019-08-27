using Terraria;
using Terraria.ModLoader;
using GloriousGuns.NPCs;

namespace GloriousGuns.Buffs
{
	public class CorrosiveDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Corroded");
			Description.SetDefault("Corrosive acids deal more damage the less life you have");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>(mod).corrosive = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GNPC>(mod).corrosive = true;
		}
	}
}

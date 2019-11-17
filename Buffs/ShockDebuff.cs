using Terraria;
using Terraria.ModLoader;
using GloriousGuns.NPCs;

namespace GloriousGuns.Buffs
{
	public class ShockDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Voltaic Shock");
			Description.SetDefault("The coursing electricity deals damage based on player movement");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().shock = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GNPC>().shock = true;
		}
	}
}

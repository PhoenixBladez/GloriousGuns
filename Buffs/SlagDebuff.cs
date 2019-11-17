using Terraria;
using Terraria.ModLoader;
using GloriousGuns.NPCs;

namespace GloriousGuns.Buffs
{
	public class SlagDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Slagged");
			Description.SetDefault("Non-slagged attacks deal double damage\n'Ewwwww'");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().slagged = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GNPC>().slagged = true;
		}
	}
}

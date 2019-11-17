using Terraria;
using Terraria.ModLoader;
using GloriousGuns.NPCs;

namespace GloriousGuns.Buffs
{
	public class AtlasTracked : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Tracked");
			Description.SetDefault("Smart bullets home in on tracked enemies");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GNPC>().tracked = true;
		}
	}
}

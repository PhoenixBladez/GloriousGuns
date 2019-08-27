using Terraria;
using Terraria.ModLoader;
using GloriousGuns.NPCs;

namespace GloriousGuns.Buffs
{
	public class FireDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Incendiary Flame");
			Description.SetDefault("The fire burns at your flesh");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>(mod).incendiary = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GNPC>(mod).incendiary = true;
		}
	}
}

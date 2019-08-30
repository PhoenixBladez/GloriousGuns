using Terraria.ModLoader;
using Terraria.Utilities;

namespace GloriousGuns
{
	class GloriousGuns : Mod
	{

		internal static GloriousGuns instance;

		public UnifiedRandom gloriousRNG;
		public GloriousGuns()
		{
			instance = this;
			gloriousRNG = new UnifiedRandom();
		}

		public override void Unload()
		{
			gloriousRNG = null;
			instance = null;
		}
	}
}

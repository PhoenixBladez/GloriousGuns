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
		}

		public override void PostSetupContent()
		{
			gloriousRNG = new UnifiedRandom();
		}

		public override void Unload()
		{
			gloriousRNG = null;
			instance = null;
		}
	}
}

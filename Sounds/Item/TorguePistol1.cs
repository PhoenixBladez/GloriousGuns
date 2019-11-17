using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace GloriousGuns.Sounds.Item
{
	public class TorguePistol1 : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume * .85f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = Main.rand.Next(-1, 8) /25f;
			return soundInstance;

		}
	}
}

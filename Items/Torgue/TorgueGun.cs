using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace GloriousGuns.Items.Torgue
{
	public abstract class TorgueGun : BaseGun
	{
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(mod, "ItemName", "Explosive Weapon\nEnough said");
			line.overrideColor = new Color(237, 198, 21);
			tooltips.Add(line);
			TooltipLine line1 = new TooltipLine(mod, "Damage", "Torgue");
			line1.overrideColor = new Color(176, 157, 127);
			tooltips.Add(line1);
		}
	}
}

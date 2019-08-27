using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using System.Reflection;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using GloriousGuns;
using Terraria.Utilities;
using System.Runtime.Serialization.Formatters.Binary;

namespace GloriousGuns
{
	public class MyWorld : ModWorld
	{

		public int x;
		public int y;
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int LivingTreesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
			if (LivingTreesIndex != -1)
			{
				tasks.Insert(LivingTreesIndex + 1, new PassLegacy("Post Terrain", delegate (GenerationProgress progress)
				{
                    ErrorLogger.Log(Main.spawnTileX);
                    for (int i = 0; i < 2; i++)
                    {
                        WorldGen.PlaceTile(Main.spawnTileX + i, Main.spawnTileY, TileID.Dirt);
                    }
					WorldGen.PlaceChest(Main.spawnTileX, Main.spawnTileY + 2, (ushort)mod.TileType("BasicChest_Tile"), false, 0);
				
				}));
			}	
		}
	}
}
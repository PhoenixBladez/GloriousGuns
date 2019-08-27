using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GloriousGuns.Items.Torgue;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GloriousGuns.Items
{
	public abstract class BaseGun : ModItem
	{

		public abstract string[] RandNames { get; }
		public abstract string WeaponName { get; };

		protected ushort nameIndex;

		public override bool CloneNewInstances => false;

		public abstract void Generate();
		public abstract void NewSetDefaults();

		public sealed override void SetDefaults()
		{
			NewSetDefaults();
			Generate();
			ApplyStats();
		}

		public override ModItem Clone(Item itemClone)
		{
			var myClone = (BaseGun)base.Clone(itemClone);

			myClone.nameIndex = nameIndex;
			myClone.item.useTime = item.useAnimation = item.useTime;
			myClone.item.damage = item.damage;
			myClone.item.reuseDelay = item.reuseDelay;
			myClone.item.value = item.value;
			myClone.item.knockBack = item.knockBack;
			myClone.item.shootSpeed = item.shootSpeed;
			myClone.ApplyStats();

			return myClone;
		}

		//IO
		public override TagCompound Save() => new TagCompound
		{
			{nameof(nameIndex), nameIndex},
			{nameof(item.useTime), item.useTime},
			{nameof(item.damage), item.damage},
			{nameof(item.reuseDelay), item.reuseDelay},
			{nameof(item.value), item.value},
			{nameof(item.knockBack), item.knockBack},
			{nameof(item.shootSpeed), item.shootSpeed}
		};

		public override void Load(TagCompound tag)
		{
			if (!tag.ContainsKey(nameof(nameIndex)))
			{
				return;
			}

			nameIndex = tag.Get<ushort>(nameof(nameIndex));
			item.useAnimation = item.useTime = tag.Get<int>(nameof(item.useTime));
			item.damage = tag.Get<int>(nameof(item.damage));
			item.reuseDelay = tag.Get<int>(nameof(item.reuseDelay));
			item.value = tag.Get<int>(nameof(item.value));
			item.knockBack = tag.Get<float>(nameof(item.knockBack));
			item.shootSpeed = tag.Get<float>(nameof(item.shootSpeed));

			ApplyStats();
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(nameIndex);
			writer.Write(item.useTime);
			writer.Write(item.damage);
			writer.Write(item.reuseDelay);
			writer.Write(item.value);
			writer.Write(item.knockBack);
			writer.Write(item.shootSpeed);
		}

		public override void NetRecieve(BinaryReader reader)
		{
			nameIndex = reader.ReadUInt16();
			item.useAnimation = item.useTime = reader.ReadInt32();
			item.damage = reader.ReadInt32();
			item.reuseDelay = reader.ReadInt32();
			item.value = reader.ReadInt32();
			item.knockBack = reader.ReadSingle();
			item.shootSpeed = reader.ReadSingle();

			ApplyStats();
		}

		public void ApplyStats()
		{
			item.SetNameOverride(WeaponName);
		}

	}
}

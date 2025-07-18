using System;
using System.Collections.Generic;
using Odium.ButtonAPI.QM;
using Odium.Components;
using UnityEngine;

namespace Odium.QMPages
{
	// Token: 0x02000020 RID: 32
	public static class Entry
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00007FA8 File Offset: 0x000061A8
		public static List<QMNestedMenu> Initialize(Sprite buttonImage, Sprite halfButtonImage)
		{
			Entry.tabMenu = new QMTabMenu("<color=#8d142b>Odium</color>", "Welcome to <color=#8d142b>Odium</color>", SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\OdiumIcon.png", 100f));
			Entry.tabMenu.MenuTitleText.alignment = 514;
			QMNestedMenu item = new QMNestedMenu(Entry.tabMenu, 1f, 0f, "<color=#8d142b>World</color>", "<color=#8d142b>World</color>", "World Utility Functions", false, SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\WorldIcon.png", 100f), buttonImage);
			QMNestedMenu item2 = new QMNestedMenu(Entry.tabMenu, 2f, 0f, "<color=#8d142b>Movement</color>", "<color=#8d142b>Movement</color>", "Movement Utility Functions", false, SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\MovementIcon.png", 100f), buttonImage);
			QMNestedMenu item3 = new QMNestedMenu(Entry.tabMenu, 3f, 0f, "<color=#8d142b>Exploits</color>", "<color=#8d142b>Exploits</color>", "World Utility Functions", false, SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\ExploitIcon.png", 100f), buttonImage);
			QMNestedMenu item4 = new QMNestedMenu(Entry.tabMenu, 4f, 3.5f, "<color=#8d142b>Settings</color>", "<color=#8d142b>Settings</color>", "World Utility Functions", true, null, halfButtonImage);
			QMNestedMenu item5 = new QMNestedMenu(Entry.tabMenu, 1f, 3.5f, "<color=#8d142b>App Bots</color>", "<color=#8d142b>App Bots</color>", "App Bots Utility Functions", true, null, halfButtonImage);
			QMNestedMenu item6 = new QMNestedMenu(Entry.tabMenu, 4f, 0f, "<color=#8d142b>Visuals</color>", "<color=#8d142b>Visuals</color>", "Visuals Utility Functions", false, SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\VisualIcon.png", 100f), buttonImage);
			QMNestedMenu item7 = new QMNestedMenu(Entry.tabMenu, 2f, 1f, "<color=#8d142b>Game Hacks</color>", "<color=#8d142b>Game Hacks</color>", "World Utility Functions", false, SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\WorldCheats.png", 100f), buttonImage);
			QMNestedMenu item8 = new QMNestedMenu(Entry.tabMenu, 3f, 1f, "<color=#8d142b>Protections</color>", "<color=#8d142b>Protections</color>", "World Utility Functions", false, SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\ShieldIcon.png", 100f), buttonImage);
			return new List<QMNestedMenu>
			{
				item,
				item2,
				item3,
				item4,
				item5,
				item6,
				item7,
				item8
			};
		}

		// Token: 0x04000066 RID: 102
		public static QMTabMenu tabMenu;
	}
}

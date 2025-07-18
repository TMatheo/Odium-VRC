using System;
using System.Runtime.CompilerServices;
using Odium.ButtonAPI.MM;
using Odium.Components;
using UnityEngine;

namespace Odium.IUserPage.MM
{
	// Token: 0x0200003A RID: 58
	internal class Functions
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000D758 File Offset: 0x0000B958
		public static void Initialize()
		{
			Sprite icon = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\InfoIcon.png", 100f);
			Sprite icon2 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\JoinMeIcon.png", 100f);
			Sprite icon3 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\PlusIcon.png", 100f);
			Sprite icon4 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\MinusIcon.png", 100f);
			Sprite icon5 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\DownloadIcon.png", 100f);
			MMUserActionRow actionRow = new MMUserActionRow("Odium Actions");
			MMUserActionRow actionRow2 = new MMUserActionRow("Odium Tags");
			new MMUserButton(actionRow, "Copy ID", delegate()
			{
				Functions.<>c.<<Initialize>b__0_0>d <<Initialize>b__0_0>d = new Functions.<>c.<<Initialize>b__0_0>d();
				<<Initialize>b__0_0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<Initialize>b__0_0>d.<>4__this = Functions.<>c.<>9;
				<<Initialize>b__0_0>d.<>1__state = -1;
				<<Initialize>b__0_0>d.<>t__builder.Start<Functions.<>c.<<Initialize>b__0_0>d>(ref <<Initialize>b__0_0>d);
			}, icon);
			new MMUserButton(actionRow, "Copy Last Platform", delegate()
			{
				Functions.<>c.<<Initialize>b__0_1>d <<Initialize>b__0_1>d = new Functions.<>c.<<Initialize>b__0_1>d();
				<<Initialize>b__0_1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<Initialize>b__0_1>d.<>4__this = Functions.<>c.<>9;
				<<Initialize>b__0_1>d.<>1__state = -1;
				<<Initialize>b__0_1>d.<>t__builder.Start<Functions.<>c.<<Initialize>b__0_1>d>(ref <<Initialize>b__0_1>d);
			}, icon);
			new MMUserButton(actionRow, "Join User", delegate()
			{
				Functions.<>c.<<Initialize>b__0_2>d <<Initialize>b__0_2>d = new Functions.<>c.<<Initialize>b__0_2>d();
				<<Initialize>b__0_2>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<Initialize>b__0_2>d.<>4__this = Functions.<>c.<>9;
				<<Initialize>b__0_2>d.<>1__state = -1;
				<<Initialize>b__0_2>d.<>t__builder.Start<Functions.<>c.<<Initialize>b__0_2>d>(ref <<Initialize>b__0_2>d);
			}, icon2);
			new MMUserButton(actionRow2, "Add Tag", delegate()
			{
				Functions.<>c.<<Initialize>b__0_3>d <<Initialize>b__0_3>d = new Functions.<>c.<<Initialize>b__0_3>d();
				<<Initialize>b__0_3>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<Initialize>b__0_3>d.<>4__this = Functions.<>c.<>9;
				<<Initialize>b__0_3>d.<>1__state = -1;
				<<Initialize>b__0_3>d.<>t__builder.Start<Functions.<>c.<<Initialize>b__0_3>d>(ref <<Initialize>b__0_3>d);
			}, icon3);
			new MMUserButton(actionRow2, "Remove Tag", delegate()
			{
				Functions.<>c.<<Initialize>b__0_4>d <<Initialize>b__0_4>d = new Functions.<>c.<<Initialize>b__0_4>d();
				<<Initialize>b__0_4>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<Initialize>b__0_4>d.<>4__this = Functions.<>c.<>9;
				<<Initialize>b__0_4>d.<>1__state = -1;
				<<Initialize>b__0_4>d.<>t__builder.Start<Functions.<>c.<<Initialize>b__0_4>d>(ref <<Initialize>b__0_4>d);
			}, icon4);
			new MMUserButton(actionRow, "Download VRCA", delegate()
			{
				Functions.<>c.<<Initialize>b__0_5>d <<Initialize>b__0_5>d = new Functions.<>c.<<Initialize>b__0_5>d();
				<<Initialize>b__0_5>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<Initialize>b__0_5>d.<>4__this = Functions.<>c.<>9;
				<<Initialize>b__0_5>d.<>1__state = -1;
				<<Initialize>b__0_5>d.<>t__builder.Start<Functions.<>c.<<Initialize>b__0_5>d>(ref <<Initialize>b__0_5>d);
			}, icon5);
		}
	}
}

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x0200007D RID: 125
	public class DebugUI
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0001C950 File Offset: 0x0001AB50
		[DebuggerStepThrough]
		public static void InitializeDebugMenu()
		{
			DebugUI.<InitializeDebugMenu>d__9 <InitializeDebugMenu>d__ = new DebugUI.<InitializeDebugMenu>d__9();
			<InitializeDebugMenu>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<InitializeDebugMenu>d__.<>1__state = -1;
			<InitializeDebugMenu>d__.<>t__builder.Start<DebugUI.<InitializeDebugMenu>d__9>(ref <InitializeDebugMenu>d__);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001C982 File Offset: 0x0001AB82
		public static IEnumerator UpdateLoop()
		{
			return new DebugUI.<UpdateLoop>d__10(0);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001C98C File Offset: 0x0001AB8C
		private static void UpdateDisplay()
		{
			bool flag = Time.time - DebugUI.lastUpdateTime < DebugUI.UPDATE_INTERVAL;
			if (!flag)
			{
				DebugUI.lastUpdateTime = Time.time;
				bool flag2 = DebugUI.text != null;
				if (flag2)
				{
					DebugUI.text.text = string.Format("\r\nSubscription: <color=green>Active</color>\r\n\r\nPlayer Tags: <color=#e91f42>{0}</color>\r\n\r\nOdium Users: <color=#e91f42>{1}</color>\r\n\r\nDuration: <color=#e91f42>Lifetime</color>\r\n\r\nPing: <color=#e91f42>{2}</color>\r\n\r\nFPS: <color=#e91f42>{3}</color>\r\n\r\nBuild: <color=#e91f42>{4}</color>\r\n\r\nServer: <color=#e91f42>Connected</color>\r\n\r\nClient: <color=#e91f42>Connected</color>\r\n\r\nDrones: <color=#e91f42>0</color>\r\n        ", new object[]
					{
						DebugUI.cachedPlayerTags,
						DebugUI.cachedOdiumUsers,
						DebugUI.cachedPing,
						DebugUI.cachedFPS,
						DebugUI.cachedBuild
					});
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001CA1A File Offset: 0x0001AC1A
		private static IEnumerator GetUserCountCoroutine()
		{
			return new DebugUI.<GetUserCountCoroutine>d__14(0);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001CA24 File Offset: 0x0001AC24
		public static void AdjustPosition(float x, float y, float z)
		{
			bool flag = DebugUI.label != null;
			if (flag)
			{
				DebugUI.label.transform.localPosition = new Vector3(x, y, z);
				OdiumConsole.Log("DebugUI", string.Format("Position adjusted to: {0}, {1}, {2}", x, y, z), LogLevel.Info);
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001CA84 File Offset: 0x0001AC84
		public static void AdjustBackgroundScale(float x, float y, float z)
		{
			bool flag = DebugUI.background != null;
			if (flag)
			{
				DebugUI.background.transform.localScale = new Vector3(x, y, z);
				OdiumConsole.Log("DebugUI", string.Format("Background scale adjusted to: {0}, {1}, {2}", x, y, z), LogLevel.Info);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		public static void FixBackgroundWidth()
		{
			bool flag = DebugUI.background != null;
			if (flag)
			{
				DebugUI.background.transform.localScale = new Vector3(1f, 8f, 1f);
				OdiumConsole.Log("DebugUI", "Background width adjusted", LogLevel.Info);
			}
		}

		// Token: 0x040001B9 RID: 441
		public static GameObject label;

		// Token: 0x040001BA RID: 442
		public static GameObject background;

		// Token: 0x040001BB RID: 443
		public static TextMeshProUGUI text;

		// Token: 0x040001BC RID: 444
		private static string cachedPing = "0";

		// Token: 0x040001BD RID: 445
		private static string cachedFPS = "0";

		// Token: 0x040001BE RID: 446
		private static string cachedBuild = "Unknown";

		// Token: 0x040001BF RID: 447
		private static int cachedPlayerTags = 0;

		// Token: 0x040001C0 RID: 448
		private static int cachedOdiumUsers = 0;

		// Token: 0x040001C1 RID: 449
		private static bool isUpdating = false;

		// Token: 0x040001C2 RID: 450
		private static float lastUpdateTime = 0f;

		// Token: 0x040001C3 RID: 451
		private static readonly float UPDATE_INTERVAL = 1f;
	}
}

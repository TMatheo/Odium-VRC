using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Odium.UX
{
	// Token: 0x0200004A RID: 74
	internal class InternalConsole
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00010288 File Offset: 0x0000E488
		public static void LogIntoConsole(string txt, string type = "<color=#8d142b>[Log]</color>", string color = "8d142b")
		{
			string text = DateTime.Now.ToString("HH:mm:ss");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new string[]
			{
				"<size=14><color=#",
				color,
				">[",
				text,
				"]</color> "
			}));
			stringBuilder.Append(type);
			stringBuilder.Append(" ");
			stringBuilder.Append(txt);
			stringBuilder.Append("</size>");
			InternalConsole.ConsoleLogCache.Add(stringBuilder.ToString());
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0001031C File Offset: 0x0000E51C
		public static void ProcessLogCache()
		{
			try
			{
				bool flag = InternalConsole.ConsoleLogCache.Count > 25;
				if (flag)
				{
					InternalConsole.ConsoleLogCache.RemoveRange(0, InternalConsole.ConsoleLogCache.Count - 25);
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in InternalConsole.ConsoleLogCache)
				{
					stringBuilder.AppendLine(value);
				}
				MainMenu.ConsoleObject.SetActive(false);
				Transform transform = MainMenu.ConsoleObject.transform.FindChild("TextLayoutParent/Text_H4");
				bool flag2 = transform != null;
				if (flag2)
				{
					RectTransform component = transform.gameObject.GetComponent<RectTransform>();
					bool flag3 = component != null;
					if (flag3)
					{
						component.localPosition = new Vector3(0f, 40f, 0f);
						component.sizeDelta = new Vector2(680f, 280f);
					}
					TextMeshProUGUIEx component2 = transform.gameObject.GetComponent<TextMeshProUGUIEx>();
					component2.alignment = 257;
					component2.richText = true;
					component2.enableWordWrapping = true;
					component2.fontSize = 14f;
					component2.lineSpacing = -10f;
					component2.overflowMode = 0;
					component2.text = stringBuilder.ToString();
				}
				MainMenu.ConsoleObject.SetActive(true);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x040000E6 RID: 230
		private static List<string> ConsoleLogCache = new List<string>();
	}
}

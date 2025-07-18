using System;
using UnityEngine;

namespace Odium.ButtonAPI.MM
{
	// Token: 0x02000076 RID: 118
	public static class MMUserExtensions
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		public static MMUserButton AddButton(this MMUserActionRow row, string text, Action action = null, Sprite icon = null)
		{
			return new MMUserButton(row, text, action, icon);
		}
	}
}

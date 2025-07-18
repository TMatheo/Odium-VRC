using System;
using UnityEngine;

namespace Odium.ButtonAPI.MM
{
	// Token: 0x02000079 RID: 121
	public static class MMWorldExtensions
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		public static MMWorldButton AddButton(this MMWorldActionRow row, string text, Action action = null, Sprite icon = null)
		{
			return new MMWorldButton(row, text, action, icon);
		}
	}
}

using System;
using UnityEngine;
using VRC.Localization;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x0200007C RID: 124
	public class QMButtonBase
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public GameObject GetGameObject()
		{
			return this.button;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001C800 File Offset: 0x0001AA00
		public void SetActive(bool state)
		{
			this.button.gameObject.SetActive(state);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001C818 File Offset: 0x0001AA18
		public void SetLocation(float buttonXLoc, float buttonYLoc)
		{
			this.button.GetComponent<RectTransform>().anchoredPosition += Vector2.right * (232f * (buttonXLoc + (float)this.initShift[0]));
			this.button.GetComponent<RectTransform>().anchoredPosition += Vector2.down * (210f * (buttonYLoc + (float)this.initShift[1]));
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001C894 File Offset: 0x0001AA94
		public void SetTooltip(string tooltip)
		{
			foreach (ToolTip toolTip in this.button.GetComponents<ToolTip>())
			{
				toolTip._localizableString = LocalizableStringExtensions.Localize(tooltip, null, null, null);
				toolTip._alternateLocalizableString = LocalizableStringExtensions.Localize(tooltip, null, null, null);
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001C904 File Offset: 0x0001AB04
		public void DestroyMe()
		{
			try
			{
				Object.Destroy(this.button);
			}
			catch
			{
			}
		}

		// Token: 0x040001B5 RID: 437
		protected GameObject button;

		// Token: 0x040001B6 RID: 438
		protected string btnQMLoc;

		// Token: 0x040001B7 RID: 439
		protected Transform parent;

		// Token: 0x040001B8 RID: 440
		protected int[] initShift = new int[2];
	}
}

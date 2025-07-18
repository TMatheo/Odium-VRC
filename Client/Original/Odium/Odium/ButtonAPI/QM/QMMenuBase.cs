using System;
using TMPro;
using UnityEngine;
using VRC.UI.Elements;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x02000080 RID: 128
	public class QMMenuBase
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0001D944 File Offset: 0x0001BB44
		public string GetMenuName()
		{
			return this.MenuName;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001D95C File Offset: 0x0001BB5C
		public UIPage GetMenuPage()
		{
			return this.MenuPage;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001D974 File Offset: 0x0001BB74
		public GameObject GetMenuObject()
		{
			return this.MenuObject;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001D98C File Offset: 0x0001BB8C
		public void SetMenuTitle(string newTitle)
		{
			TextMeshProUGUI componentInChildren = this.MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
			componentInChildren.text = newTitle;
			componentInChildren.richText = true;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		public void ClearChildren()
		{
			for (int i = 0; i < this.MenuObject.transform.childCount; i++)
			{
				bool flag = this.MenuObject.transform.GetChild(i).name != "Header_H1" && this.MenuObject.transform.GetChild(i).name != "ScrollRect";
				if (flag)
				{
					Object.Destroy(this.MenuObject.transform.GetChild(i).gameObject);
				}
			}
		}

		// Token: 0x040001C5 RID: 453
		protected string btnQMLoc;

		// Token: 0x040001C6 RID: 454
		protected GameObject MenuObject;

		// Token: 0x040001C7 RID: 455
		public TextMeshProUGUI MenuTitleText;

		// Token: 0x040001C8 RID: 456
		protected UIPage MenuPage;

		// Token: 0x040001C9 RID: 457
		protected string MenuName;
	}
}

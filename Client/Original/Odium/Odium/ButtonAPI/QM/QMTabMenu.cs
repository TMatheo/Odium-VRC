using System;
using System.Collections.Generic;
using System.Linq;
using Il2CppSystem.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Pages.QM;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x02000086 RID: 134
	public class QMTabMenu : QMMenuBase
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0001F645 File Offset: 0x0001D845
		public QMTabMenu(string MenuTitle, string tooltip, Sprite ButtonImage = null)
		{
			this.Initialize(MenuTitle, tooltip, ButtonImage);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001F65C File Offset: 0x0001D85C
		private void Initialize(string MenuTitle, string tooltip, Sprite ButtonImage)
		{
			this.MenuName = string.Format("{0}-Tab-Menu-{1}", "Odium", ApiUtils.RandomNumbers());
			this.MenuObject = Object.Instantiate<GameObject>(ApiUtils.GetQMMenuTemplate(), ApiUtils.GetQMMenuTemplate().transform.parent);
			this.MenuObject.name = this.MenuName;
			this.MenuObject.SetActive(false);
			this.MenuObject.transform.SetSiblingIndex(19);
			InterfacePublicAbstractObBoObVoStObInVoStBoUnique field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_ = this.MenuObject.GetComponent<Dashboard>().field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_0;
			Object.DestroyImmediate(this.MenuObject.GetComponent<Dashboard>());
			this.MenuPage = this.MenuObject.AddComponent<UIPage>();
			this.MenuPage.field_Public_String_0 = this.MenuName;
			this.MenuPage.field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_0 = field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_;
			this.MenuPage.field_Private_List_1_UIPage_0 = new List<UIPage>();
			this.MenuPage.field_Private_List_1_UIPage_0.Add(this.MenuPage);
			ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().field_Private_Dictionary_2_String_UIPage_0.Add(this.MenuName, this.MenuPage);
			List<UIPage> list = ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().field_Public_ArrayOf_UIPage_0.ToList<UIPage>();
			list.Add(this.MenuPage);
			ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().field_Public_ArrayOf_UIPage_0 = list.ToArray();
			Transform transform = this.MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				bool flag = child == null;
				if (!flag)
				{
					Object.Destroy(child.gameObject);
				}
			}
			this.MenuTitleText = this.MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
			base.SetMenuTitle(MenuTitle);
			this.MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject.SetActive(false);
			this.MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Report").gameObject.SetActive(false);
			base.ClearChildren();
			this.MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>().enabled = false;
			this.MainButton = Object.Instantiate<GameObject>(ApiUtils.GetQMTabButtonTemplate(), ApiUtils.GetQMTabButtonTemplate().transform.parent);
			this.MainButton.name = this.MenuName;
			this.MenuTabComp = this.MainButton.GetComponent<MenuTab>();
			this.MenuTabComp.field_Private_MenuStateController_0 = ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0();
			this.MenuTabComp._controlName = this.MenuName;
			this.MenuTabComp.GetComponent<StyleElement>().field_Private_Selectable_0 = this.MenuTabComp.GetComponent<Button>();
			this.BadgeObject = this.MainButton.transform.GetChild(0).gameObject;
			this.BadgeText = this.BadgeObject.GetComponentInChildren<TextMeshProUGUI>();
			this.MainButton.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.MenuObject.SetActive(true);
				this.MenuObject.GetComponent<Canvas>().enabled = true;
				this.MenuObject.GetComponent<CanvasGroup>().enabled = true;
				this.MenuObject.GetComponent<GraphicRaycaster>().enabled = true;
				this.MenuTabComp.GetComponent<StyleElement>().field_Private_Selectable_0 = this.MenuTabComp.GetComponent<Button>();
			});
			Object.Destroy(this.MainButton.GetComponent<MonoBehaviour1PublicVoVo5>());
			this.SetTooltip(tooltip);
			bool flag2 = ButtonImage != null;
			if (flag2)
			{
				this.SetImage(ButtonImage);
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
		public void SetImage(Sprite newImg)
		{
			this.MainButton.transform.Find("Icon").GetComponent<Image>().sprite = newImg;
			this.MainButton.transform.Find("Icon").GetComponent<Image>().overrideSprite = newImg;
			this.MainButton.transform.Find("Icon").GetComponent<Image>().color = Color.white;
			this.MainButton.transform.Find("Icon").GetComponent<Image>().m_Color = Color.white;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001FA3A File Offset: 0x0001DC3A
		public void SetIndex(int newPosition)
		{
			this.MainButton.transform.SetSiblingIndex(newPosition);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001FA4F File Offset: 0x0001DC4F
		public void SetActive(bool newState)
		{
			this.MainButton.SetActive(newState);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001FA60 File Offset: 0x0001DC60
		public void SetBadge(bool showing = true, string text = "")
		{
			bool flag = this.BadgeObject != null && this.BadgeText != null;
			if (flag)
			{
				this.BadgeObject.SetActive(showing);
				this.BadgeText.text = text;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001FAAB File Offset: 0x0001DCAB
		public void OpenMe()
		{
			this.MainButton.GetComponent<Button>().onClick.Invoke();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
		public void SetTooltip(string tooltip)
		{
			foreach (ToolTip toolTip in this.MainButton.GetComponents<ToolTip>())
			{
				toolTip._localizableString = LocalizableStringExtensions.Localize(tooltip, null, null, null);
				toolTip._alternateLocalizableString = LocalizableStringExtensions.Localize(tooltip, null, null, null);
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001FB34 File Offset: 0x0001DD34
		public GameObject GetMainButton()
		{
			return this.MainButton;
		}

		// Token: 0x040001DD RID: 477
		protected GameObject MainButton;

		// Token: 0x040001DE RID: 478
		protected GameObject BadgeObject;

		// Token: 0x040001DF RID: 479
		protected TextMeshProUGUI BadgeText;

		// Token: 0x040001E0 RID: 480
		protected MenuTab MenuTabComp;
	}
}

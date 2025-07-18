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
	// Token: 0x02000081 RID: 129
	public class QMMenuPage : QMMenuBase
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0001DA53 File Offset: 0x0001BC53
		public QMMenuPage(string MenuTitle, string tooltip, Sprite ButtonImage = null)
		{
			this.Initialize(MenuTitle, tooltip, ButtonImage);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001DA68 File Offset: 0x0001BC68
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

		// Token: 0x0600038A RID: 906 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		public void SetImage(Sprite newImg)
		{
			this.MainButton.transform.Find("Icon").GetComponent<Image>().sprite = newImg;
			this.MainButton.transform.Find("Icon").GetComponent<Image>().overrideSprite = newImg;
			this.MainButton.transform.Find("Icon").GetComponent<Image>().color = Color.white;
			this.MainButton.transform.Find("Icon").GetComponent<Image>().m_Color = Color.white;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001DE46 File Offset: 0x0001C046
		public void SetIndex(int newPosition)
		{
			this.MainButton.transform.SetSiblingIndex(newPosition);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001DE5B File Offset: 0x0001C05B
		public void SetActive(bool newState)
		{
			this.MainButton.SetActive(newState);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001DE6C File Offset: 0x0001C06C
		public void SetBadge(bool showing = true, string text = "")
		{
			bool flag = this.BadgeObject != null && this.BadgeText != null;
			if (flag)
			{
				this.BadgeObject.SetActive(showing);
				this.BadgeText.text = text;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001DEB7 File Offset: 0x0001C0B7
		public void OpenMe()
		{
			this.MainButton.GetComponent<Button>().onClick.Invoke();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		public void SetTooltip(string tooltip)
		{
			foreach (ToolTip toolTip in this.MainButton.GetComponents<ToolTip>())
			{
				toolTip._localizableString = LocalizableStringExtensions.Localize(tooltip, null, null, null);
				toolTip._alternateLocalizableString = LocalizableStringExtensions.Localize(tooltip, null, null, null);
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001DF40 File Offset: 0x0001C140
		public GameObject GetMainButton()
		{
			return this.MainButton;
		}

		// Token: 0x040001CA RID: 458
		protected GameObject MainButton;

		// Token: 0x040001CB RID: 459
		protected GameObject BadgeObject;

		// Token: 0x040001CC RID: 460
		protected TextMeshProUGUI BadgeText;

		// Token: 0x040001CD RID: 461
		protected MenuTab MenuTabComp;
	}
}

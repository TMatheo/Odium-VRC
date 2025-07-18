using System;
using System.Collections.Generic;
using System.Linq;
using Il2CppSystem.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Pages.QM;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x02000082 RID: 130
	public class QMNestedMenu : QMMenuBase
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		public QMNestedMenu(QMMenuBase location, float posX, float posY, string btnText, string menuTitle, string tooltip, bool halfButton = false, Sprite sprite = null, Sprite bgImage = null)
		{
			this.btnQMLoc = location.GetMenuName();
			this.Initialize(false, btnText, posX, posY, menuTitle, tooltip, halfButton, sprite, bgImage);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001E000 File Offset: 0x0001C200
		public QMNestedMenu(DefaultVRCMenu location, float posX, float posY, string btnText, string menuTitle, string tooltip, bool halfButton = false, Sprite sprite = null, Sprite bgImage = null)
		{
			this.btnQMLoc = "Menu_" + location.ToString();
			this.Initialize(false, btnText, posX, posY, menuTitle, tooltip, halfButton, sprite, bgImage);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001E048 File Offset: 0x0001C248
		public QMNestedMenu(Transform target, float posX, float posY, string btnText, string menuTitle, string tooltip, bool halfButton = false, Sprite sprite = null, Sprite bgImage = null)
		{
			this.parent = target;
			this.Initialize(false, btnText, posX, posY, menuTitle, tooltip, halfButton, sprite, bgImage);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001E07C File Offset: 0x0001C27C
		private void Initialize(bool isRoot, string btnText, float btnPosX, float btnPosY, string menuTitle, string tooltip, bool halfButton, Sprite sprite, Sprite bgImage)
		{
			this.MenuName = string.Format("{0}-QMMenu-{1}", "Odium", ApiUtils.RandomNumbers());
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
			this.IsMenuRoot = isRoot;
			bool isMenuRoot = this.IsMenuRoot;
			if (isMenuRoot)
			{
				List<UIPage> list = ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().field_Public_ArrayOf_UIPage_0.ToList<UIPage>();
				list.Add(this.MenuPage);
				ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().field_Public_ArrayOf_UIPage_0 = list.ToArray();
			}
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
			base.SetMenuTitle(menuTitle);
			this.BackButton = this.MenuObject.transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
			this.BackButton.SetActive(true);
			this.BackButton.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
			this.BackButton.GetComponentInChildren<Button>().onClick.AddListener(delegate()
			{
				bool isRoot2 = isRoot;
				if (isRoot2)
				{
					bool flag3 = this.btnQMLoc.StartsWith("Menu_");
					if (flag3)
					{
						ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().Method_Public_Void_String_Boolean_Boolean_PDM_0("QuickMenu" + this.btnQMLoc.Remove(0, 5), false, false);
					}
					else
					{
						ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().Method_Public_Void_String_Boolean_Boolean_PDM_0(this.btnQMLoc, false, false);
					}
				}
				else
				{
					this.MenuPage.Method_Protected_Virtual_New_Void_0();
				}
			});
			this.MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject.SetActive(false);
			this.MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Report").gameObject.SetActive(false);
			bool flag2 = this.parent != null;
			if (flag2)
			{
				this.MainButton = new QMSingleButton(this.parent, btnPosX, btnPosY, btnText, new Action(this.OpenMe), tooltip, halfButton, sprite, bgImage);
			}
			else
			{
				this.MainButton = new QMSingleButton(this.btnQMLoc, btnPosX, btnPosY, btnText, new Action(this.OpenMe), tooltip, halfButton, sprite, bgImage);
			}
			base.ClearChildren();
			this.MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>().enabled = false;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		public void OpenMe()
		{
			ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().Method_Public_Void_String_UIContext_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(this.MenuPage.field_Public_String_0, null, false, 1635);
			this.MenuObject.SetActive(true);
			this.MenuObject.GetComponent<Canvas>().enabled = true;
			this.MenuObject.GetComponent<CanvasGroup>().enabled = true;
			this.MenuObject.GetComponent<GraphicRaycaster>().enabled = true;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001E43B File Offset: 0x0001C63B
		public void CloseMe()
		{
			this.BackButton.GetComponent<Button>().onClick.Invoke();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001E454 File Offset: 0x0001C654
		public QMSingleButton GetMainButton()
		{
			return this.MainButton;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001E46C File Offset: 0x0001C66C
		public GameObject GetBackButton()
		{
			return this.BackButton;
		}

		// Token: 0x040001CE RID: 462
		protected bool IsMenuRoot;

		// Token: 0x040001CF RID: 463
		protected GameObject BackButton;

		// Token: 0x040001D0 RID: 464
		protected QMSingleButton MainButton;

		// Token: 0x040001D1 RID: 465
		protected Transform parent;
	}
}

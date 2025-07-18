using System;
using TMPro;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x02000085 RID: 133
	public class QMSingleButton : QMButtonBase
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
		public QMSingleButton(QMMenuBase btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, bool halfBtn = false, Sprite icon = null, Sprite bgImage = null)
		{
			this.btnQMLoc = btnMenu.GetMenuName();
			if (halfBtn)
			{
				btnYLocation -= 0.21f;
			}
			this.Initialize(btnXLocation, btnYLocation, btnText, btnAction, tooltip, icon, halfBtn, bgImage);
			if (halfBtn)
			{
				this.button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001F040 File Offset: 0x0001D240
		public QMSingleButton(DefaultVRCMenu btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, bool halfBtn = false, Sprite sprite = null, Sprite bgImage = null)
		{
			this.btnQMLoc = "Menu_" + btnMenu.ToString();
			if (halfBtn)
			{
				btnYLocation -= 0.21f;
			}
			this.Initialize(btnXLocation, btnYLocation, btnText, btnAction, tooltip, sprite, halfBtn, bgImage);
			if (halfBtn)
			{
				this.button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		public QMSingleButton(string btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, bool halfBtn = false, Sprite sprite = null, Sprite bgImage = null)
		{
			this.btnQMLoc = btnMenu;
			if (halfBtn)
			{
				btnYLocation -= 0.21f;
			}
			this.Initialize(btnXLocation, btnYLocation, btnText, btnAction, tooltip, sprite, halfBtn, bgImage);
			if (halfBtn)
			{
				this.button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001F140 File Offset: 0x0001D340
		public QMSingleButton(Transform target, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, bool halfBtn = false, Sprite sprite = null, Sprite bgImage = null)
		{
			this.parent = target;
			if (halfBtn)
			{
				btnYLocation -= 0.21f;
			}
			this.Initialize(btnXLocation, btnYLocation, btnText, btnAction, tooltip, sprite, halfBtn, bgImage);
			if (halfBtn)
			{
				this.button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001F1B4 File Offset: 0x0001D3B4
		private void Initialize(float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, Sprite sprite, bool halfBtn, Sprite bgImage = null)
		{
			bool flag = this.parent == null;
			if (flag)
			{
				this.parent = ApiUtils.QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/" + this.btnQMLoc).transform;
			}
			this.button = Object.Instantiate<GameObject>(ApiUtils.GetQMButtonTemplate(), this.parent, true);
			this.button.transform.Find("Badge_MMJump").gameObject.SetActive(false);
			this.button.name = string.Format("{0}-Single-Button-{1}", "Odium", ApiUtils.RandomNumbers());
			this.button.GetComponentInChildren<TextMeshProUGUI>().fontSize = 30f;
			this.button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
			this.button.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 176f);
			this.button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-68f, -250f);
			bool flag2 = sprite == null;
			if (flag2)
			{
				this.button.transform.Find("Icons/Icon").GetComponentInChildren<Image>().gameObject.SetActive(false);
			}
			else
			{
				this.button.transform.Find("Icons/Icon").GetComponentInChildren<Image>().overrideSprite = sprite;
				this.button.transform.Find("Icons/Icon").GetComponentInChildren<Image>().sprite = sprite;
			}
			this.button.GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition += new Vector2(0f, 50f);
			this.initShift[0] = 0;
			this.initShift[1] = 0;
			base.SetLocation(btnXLocation, btnYLocation);
			bool flag3 = sprite == null;
			if (flag3)
			{
				this.SetButtonText(btnText, false, halfBtn);
			}
			else
			{
				this.SetButtonText(btnText, true, halfBtn);
			}
			this.SetAction(btnAction);
			base.SetActive(true);
			base.SetTooltip(tooltip);
			bool flag4 = bgImage != null;
			if (flag4)
			{
				this.ToggleBackgroundImage(true);
				this.SetBackgroundImage(bgImage);
			}
			else
			{
				this.ToggleBackgroundImage(false);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		public void SetBackgroundImage(Sprite newImg)
		{
			this.button.transform.Find("Background").GetComponent<Image>().sprite = newImg;
			this.button.transform.Find("Background").GetComponent<Image>().overrideSprite = newImg;
			this.RefreshButton();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001F453 File Offset: 0x0001D653
		public void ToggleBackgroundImage(bool state)
		{
			this.button.transform.Find("Background").gameObject.SetActive(state);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001F478 File Offset: 0x0001D678
		public void SetButtonText(string buttonText, bool hasIcon, bool halfBtn)
		{
			TextMeshProUGUI componentInChildren = this.button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
			componentInChildren.richText = true;
			componentInChildren.text = buttonText;
			if (hasIcon)
			{
				componentInChildren.gameObject.transform.position = new Vector3(componentInChildren.gameObject.transform.position.x, componentInChildren.gameObject.transform.position.y - 0.025f, componentInChildren.gameObject.transform.position.z);
			}
			if (halfBtn)
			{
				componentInChildren.gameObject.transform.position = new Vector3(componentInChildren.gameObject.transform.position.x, componentInChildren.gameObject.transform.position.y, componentInChildren.gameObject.transform.position.z);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001F568 File Offset: 0x0001D768
		public void SetAction(Action buttonAction)
		{
			this.button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
			bool flag = buttonAction != null;
			if (flag)
			{
				this.button.GetComponent<Button>().onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		public void SetInteractable(bool newState)
		{
			this.button.GetComponent<Button>().interactable = newState;
			this.RefreshButton();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		public void SetFontSize(float size)
		{
			this.button.GetComponentInChildren<TextMeshProUGUI>().fontSize = size;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public void ClickMe()
		{
			this.button.GetComponent<Button>().onClick.Invoke();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001F5FC File Offset: 0x0001D7FC
		public Image GetBackgroundImage()
		{
			return this.button.transform.Find("Background").GetComponent<Image>();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001F628 File Offset: 0x0001D828
		private void RefreshButton()
		{
			this.button.SetActive(false);
			this.button.SetActive(true);
		}
	}
}

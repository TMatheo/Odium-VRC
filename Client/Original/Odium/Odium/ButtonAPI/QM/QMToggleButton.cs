using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x02000087 RID: 135
	public class QMToggleButton : QMButtonBase
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x0001FBBC File Offset: 0x0001DDBC
		public QMToggleButton(QMMenuBase location, float btnXPos, float btnYPos, string btnText, Action onAction, Action offAction, string tooltip, bool defaultState = false, Sprite bgImage = null)
		{
			this.btnQMLoc = location.GetMenuName();
			this.Initialize(btnXPos, btnYPos, btnText, onAction, offAction, tooltip, defaultState, bgImage);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		public QMToggleButton(DefaultVRCMenu location, float btnXPos, float btnYPos, string btnText, Action onAction, Action offAction, string tooltip, bool defaultState = false, Sprite bgImage = null)
		{
			this.btnQMLoc = "Menu_" + location.ToString();
			this.Initialize(btnXPos, btnYPos, btnText, onAction, offAction, tooltip, defaultState, bgImage);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001FC3C File Offset: 0x0001DE3C
		public QMToggleButton(Transform target, float btnXPos, float btnYPos, string btnText, Action onAction, Action offAction, string tooltip, bool defaultState = false, Sprite bgImage = null)
		{
			this.parent = target;
			this.Initialize(btnXPos, btnYPos, btnText, onAction, offAction, tooltip, defaultState, bgImage);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001FC70 File Offset: 0x0001DE70
		private void Initialize(float btnXLocation, float btnYLocation, string btnText, Action onAction, Action offAction, string tooltip, bool defaultState, Sprite bgImage = null)
		{
			bool flag = this.parent == null;
			if (flag)
			{
				this.parent = ApiUtils.QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/" + this.btnQMLoc).transform;
			}
			this.button = Object.Instantiate<GameObject>(ApiUtils.GetQMButtonTemplate(), this.parent, true);
			this.button.name = string.Format("{0}-Toggle-Button-{1}", "Odium", ApiUtils.RandomNumbers());
			this.button.transform.Find("Badge_MMJump").gameObject.SetActive(false);
			this.button.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 176f);
			this.button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-68f, -250f);
			this.btnTextComp = this.button.GetComponentInChildren<TextMeshProUGUI>(true);
			this.btnTextComp.color = Color.white;
			this.btnComp = this.button.GetComponentInChildren<Button>(true);
			this.btnComp.onClick = new Button.ButtonClickedEvent();
			this.btnComp.onClick.AddListener(new Action(this.HandleClick));
			this.btnImageComp = this.button.transform.Find("Icons/Icon").GetComponentInChildren<Image>(true);
			this.btnImageComp.gameObject.SetActive(true);
			this.initShift[0] = 0;
			this.initShift[1] = 0;
			base.SetLocation(btnXLocation, btnYLocation);
			this.SetButtonText(btnText);
			this.SetButtonActions(onAction, offAction);
			base.SetTooltip(tooltip);
			base.SetActive(true);
			this.currentState = defaultState;
			Sprite sprite = this.currentState ? ApiUtils.OnIconSprite() : ApiUtils.OffIconSprite();
			this.btnImageComp.sprite = sprite;
			this.btnImageComp.overrideSprite = sprite;
			bool flag2 = bgImage != null;
			if (flag2)
			{
				this.ToggleBackgroundImage(true);
				this.SetBackgroundImage(bgImage);
			}
			else
			{
				this.ToggleBackgroundImage(false);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001FE94 File Offset: 0x0001E094
		private void HandleClick()
		{
			this.currentState = !this.currentState;
			Sprite sprite = this.currentState ? ApiUtils.OnIconSprite() : ApiUtils.OffIconSprite();
			this.btnImageComp.sprite = sprite;
			this.btnImageComp.overrideSprite = sprite;
			bool flag = this.currentState;
			if (flag)
			{
				this.OnAction();
			}
			else
			{
				this.OffAction();
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001FF04 File Offset: 0x0001E104
		public void SetButtonText(string buttonText)
		{
			TextMeshProUGUI componentInChildren = this.button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
			componentInChildren.richText = true;
			componentInChildren.text = buttonText;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001FF34 File Offset: 0x0001E134
		public void SetBackgroundImage(Sprite newImg)
		{
			this.button.transform.Find("Background").GetComponent<Image>().sprite = newImg;
			this.button.transform.Find("Background").GetComponent<Image>().overrideSprite = newImg;
			this.RefreshButton();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001FF8B File Offset: 0x0001E18B
		public void ToggleBackgroundImage(bool state)
		{
			this.button.transform.Find("Background").gameObject.SetActive(state);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001FFAF File Offset: 0x0001E1AF
		public void SetButtonActions(Action onAction, Action offAction)
		{
			this.OnAction = onAction;
			this.OffAction = offAction;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		public void SetToggleState(bool newState, bool shouldInvoke = false)
		{
			try
			{
				Sprite sprite = newState ? ApiUtils.OnIconSprite() : ApiUtils.OffIconSprite();
				this.btnImageComp.sprite = sprite;
				this.btnImageComp.overrideSprite = sprite;
				this.currentState = newState;
				if (shouldInvoke)
				{
					if (newState)
					{
						this.OnAction();
					}
					else
					{
						this.OffAction();
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00020040 File Offset: 0x0001E240
		public void SetInteractable(bool newState)
		{
			this.button.GetComponent<Button>().interactable = newState;
			this.RefreshButton();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0002005C File Offset: 0x0001E25C
		public void ClickMe()
		{
			this.HandleClick();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00020068 File Offset: 0x0001E268
		public bool GetCurrentState()
		{
			return this.currentState;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00020080 File Offset: 0x0001E280
		private void RefreshButton()
		{
			this.button.SetActive(false);
			this.button.SetActive(true);
		}

		// Token: 0x040001E1 RID: 481
		protected TextMeshProUGUI btnTextComp;

		// Token: 0x040001E2 RID: 482
		protected Button btnComp;

		// Token: 0x040001E3 RID: 483
		protected Image btnImageComp;

		// Token: 0x040001E4 RID: 484
		protected bool currentState;

		// Token: 0x040001E5 RID: 485
		protected Action OnAction;

		// Token: 0x040001E6 RID: 486
		protected Action OffAction;
	}
}

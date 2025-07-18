using System;
using Odium.Odium;
using UnityEngine;
using UnityEngine.UI;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x0200007F RID: 127
	internal class QMMainIconButton
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		public static void CreateButton(Sprite sprite, Action onClick = null)
		{
			try
			{
				Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer");
				Transform transform2 = transform.Find("Button_QM_Report");
				bool flag = transform == null || transform2 == null;
				if (flag)
				{
					Debug.LogError("Could not find required QuickMenu elements!");
				}
				else
				{
					GameObject gameObject = Object.Instantiate<GameObject>(transform2.gameObject, transform);
					gameObject.name = "Button_QMOdium" + Guid.NewGuid().ToString();
					gameObject.SetActive(true);
					RectTransform component = gameObject.GetComponent<RectTransform>();
					component.localPosition = Vector3.zero;
					component.localRotation = Quaternion.identity;
					component.localScale = Vector3.one;
					Transform transform3 = gameObject.transform.Find("Icon");
					bool flag2 = transform3 != null;
					if (flag2)
					{
						Image component2 = transform3.GetComponent<Image>();
						bool flag3 = component2 != null;
						if (flag3)
						{
							component2.sprite = sprite;
							component2.overrideSprite = sprite;
						}
					}
					Button component3 = gameObject.GetComponent<Button>();
					bool flag4 = component3 != null && onClick != null;
					if (flag4)
					{
						component3.onClick.RemoveAllListeners();
						component3.onClick.AddListener(onClick);
					}
					gameObject.transform.SetSiblingIndex(transform2.GetSiblingIndex());
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("Error creating QM icon button: {0}", arg));
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001D584 File Offset: 0x0001B784
		public static void CreateImage(Sprite sprite, Vector3 position, Vector3 size, bool includeBackground = false)
		{
			try
			{
				Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer");
				Transform transform2 = transform.Find("Button_QM_Report");
				bool flag = transform == null || transform2 == null;
				if (flag)
				{
					Debug.LogError("Could not find required QuickMenu elements!");
				}
				else
				{
					GameObject gameObject = Object.Instantiate<GameObject>(transform2.gameObject, transform);
					gameObject.name = "Button_QMOdium" + Guid.NewGuid().ToString();
					gameObject.SetActive(true);
					RectTransform component = gameObject.GetComponent<RectTransform>();
					component.localPosition = position;
					component.localRotation = Quaternion.identity;
					component.localScale = size;
					Transform transform3 = gameObject.transform.Find("Icon");
					transform3.localPosition = new Vector3(-208.8547f, -22.7455f, 0f);
					bool flag2 = transform3 != null;
					if (flag2)
					{
						Image component2 = transform3.GetComponent<Image>();
						bool flag3 = component2 != null;
						if (flag3)
						{
							component2.sprite = sprite;
							component2.overrideSprite = sprite;
						}
					}
					Transform transform4 = gameObject.transform.FindChild("Background");
					transform4.gameObject.SetActive(includeBackground);
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("Error creating QM icon button: {0}", arg));
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001D708 File Offset: 0x0001B908
		public static void CreateToggle(Sprite onSprite, Sprite offSprite, Action onAction = null, Action offAction = null)
		{
			try
			{
				Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer");
				Transform transform2 = transform.Find("Button_QM_Report");
				bool flag = transform == null || transform2 == null;
				if (flag)
				{
					Debug.LogError("Could not find required QuickMenu elements!");
				}
				else
				{
					GameObject gameObject = Object.Instantiate<GameObject>(transform2.gameObject, transform);
					gameObject.name = "Toggle_QMOdium" + Guid.NewGuid().ToString();
					gameObject.SetActive(true);
					RectTransform component = gameObject.GetComponent<RectTransform>();
					component.localPosition = Vector3.zero;
					component.localRotation = Quaternion.identity;
					component.localScale = Vector3.one;
					Transform transform3 = gameObject.transform.Find("Icon");
					bool flag2 = transform3 == null;
					if (flag2)
					{
						Debug.LogError("Could not find Icon transform!");
					}
					else
					{
						Image iconImage = transform3.GetComponent<Image>();
						bool flag3 = iconImage == null;
						if (flag3)
						{
							Debug.LogError("Could not find Image component on Icon!");
						}
						else
						{
							bool isToggled = false;
							iconImage.sprite = offSprite;
							iconImage.overrideSprite = offSprite;
							Button component2 = gameObject.GetComponent<Button>();
							bool flag4 = component2 != null;
							if (flag4)
							{
								component2.onClick.RemoveAllListeners();
								component2.onClick.AddListener(delegate()
								{
									bool isToggled = !isToggled;
									isToggled = isToggled;
									if (isToggled)
									{
										iconImage.sprite = onSprite;
										iconImage.overrideSprite = onSprite;
										Action onAction2 = onAction;
										if (onAction2 != null)
										{
											onAction2();
										}
									}
									else
									{
										iconImage.sprite = offSprite;
										iconImage.overrideSprite = offSprite;
										Action offAction2 = offAction;
										if (offAction2 != null)
										{
											offAction2();
										}
									}
								});
							}
							gameObject.transform.SetSiblingIndex(transform2.GetSiblingIndex());
						}
					}
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("Error creating QM toggle button: {0}", arg));
			}
		}
	}
}

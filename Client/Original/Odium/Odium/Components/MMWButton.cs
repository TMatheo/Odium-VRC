using System;
using Odium.Odium;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI;

namespace Odium.Components
{
	// Token: 0x0200005F RID: 95
	internal class MMWButton
	{
		// Token: 0x06000276 RID: 630 RVA: 0x00015FC8 File Offset: 0x000141C8
		public static GameObject CreateCustomWorldButton(string buttonText, string iconPath, UnityAction clickAction)
		{
			GameObject result;
			try
			{
				GameObject gameObject = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_MM_WorldInformation/Panel_World_Information/Content/Viewport/BodyContainer_World_Details/ScrollRect/Viewport/VerticalLayoutGroup/Actions/AddToFavorites").gameObject;
				Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_MM_WorldInformation/Panel_World_Information/Content/Viewport/BodyContainer_World_Details/ScrollRect/Viewport/VerticalLayoutGroup/Actions").transform;
				bool flag = gameObject == null || transform == null;
				if (flag)
				{
					Debug.LogError("Failed to find template or parent transform");
					result = null;
				}
				else
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, transform);
					gameObject2.name = "MMWButton_" + Guid.NewGuid().ToString();
					LocalizableString localizableString = new LocalizableString();
					localizableString._localizationKey = buttonText;
					Transform transform2 = gameObject2.transform.Find("Text_ButtonName");
					TextMeshProUGUIEx textMeshProUGUIEx = (transform2 != null) ? transform2.GetComponent<TextMeshProUGUIEx>() : null;
					bool flag2 = textMeshProUGUIEx != null;
					if (flag2)
					{
						textMeshProUGUIEx._localizableString = localizableString;
					}
					bool flag3 = !string.IsNullOrEmpty(iconPath);
					if (flag3)
					{
						Sprite sprite = SpriteUtil.LoadFromDisk(iconPath, 100f);
						Transform transform3 = gameObject2.transform.Find("Text_ButtonName/Icon_Add");
						ImageEx imageEx = (transform3 != null) ? transform3.GetComponent<ImageEx>() : null;
						bool flag4 = imageEx != null && sprite != null;
						if (flag4)
						{
							imageEx.sprite = sprite;
						}
					}
					Button component = gameObject2.GetComponent<Button>();
					bool flag5 = component != null;
					if (flag5)
					{
						component.onClick.RemoveAllListeners();
						bool flag6 = clickAction != null;
						if (flag6)
						{
							component.onClick.AddListener(clickAction);
						}
					}
					gameObject2.SetActive(true);
					result = gameObject2;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to create custom world button: " + ex.Message);
				result = null;
			}
			return result;
		}
	}
}

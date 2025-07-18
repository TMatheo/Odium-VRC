using System;
using System.IO;
using System.Runtime.CompilerServices;
using Odium.Components;
using Odium.UX;
using Odium.Wrappers;
using TMPro;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
using VRC.Localization;

// Token: 0x0200000D RID: 13
public class SelectedUser
{
	// Token: 0x06000048 RID: 72 RVA: 0x000034B8 File Offset: 0x000016B8
	public static string get_selected_player_name()
	{
		GameObject gameObject = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/UserProfile_Compact/PanelBG/Info/Text_Username_NonFriend");
		bool flag = gameObject == null;
		string result;
		if (flag)
		{
			result = "";
		}
		else
		{
			TextMeshProUGUIEx component = gameObject.GetComponent<TextMeshProUGUIEx>();
			bool flag2 = component == null;
			if (flag2)
			{
				result = "";
			}
			else
			{
				result = component.text;
			}
		}
		return result;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000350C File Offset: 0x0000170C
	public static GameObject CreateButton(GameObject parentGrid, string title, Action<Player> OnClick, string tooltip = "", bool DisplayIcon = false, Sprite sprite = null)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(MainMenu.ExampleButton, parentGrid.transform);
		gameObject.name = string.Format("Button_{0}", Guid.NewGuid());
		Transform transform = gameObject.transform.FindChild("TextLayoutParent/Text_H4");
		bool flag = transform != null;
		if (flag)
		{
			TextMeshProUGUIEx component = transform.gameObject.GetComponent<TextMeshProUGUIEx>();
			component.richText = true;
			component.text = title;
		}
		Transform transform2 = gameObject.transform.FindChild("Badge_MMJump");
		bool flag2 = transform2 != null;
		if (flag2)
		{
			transform2.gameObject.SetActive(false);
		}
		Il2CppArrayBase<ToolTip> components = gameObject.GetComponents<ToolTip>();
		foreach (ToolTip toolTip in components)
		{
			toolTip._localizableString = new LocalizableString
			{
				_localizationKey = tooltip
			};
		}
		VRCButtonHandle component2 = gameObject.GetComponent<VRCButtonHandle>();
		bool flag3 = component2 != null;
		if (flag3)
		{
			component2._sendAnalytics = false;
			component2.m_OnClick.RemoveAllListeners();
			component2.onClick.RemoveAllListeners();
			component2.onClick.AddListener(delegate()
			{
				string selected_player_name = SelectedUser.get_selected_player_name();
				bool flag8 = !string.IsNullOrEmpty(selected_player_name);
				if (flag8)
				{
					try
					{
						Player player = null;
						foreach (Player player2 in PlayerWrapper.Players)
						{
							bool flag9 = player2 == null;
							if (!flag9)
							{
								APIUser field_Private_APIUser_ = player2.field_Private_APIUser_0;
								bool flag10 = field_Private_APIUser_ == null;
								if (!flag10)
								{
									bool flag11 = field_Private_APIUser_.displayName == selected_player_name;
									if (flag11)
									{
										player = player2;
										break;
									}
								}
							}
						}
						bool flag12 = player != null;
						if (flag12)
						{
							Action<Player> onClick = OnClick;
							if (onClick != null)
							{
								onClick(player);
							}
						}
					}
					catch
					{
					}
				}
			});
		}
		Transform transform3 = gameObject.transform.FindChild("Icons");
		bool flag4 = transform3 != null;
		if (flag4)
		{
			transform3.gameObject.SetActive(DisplayIcon);
			bool flag5 = DisplayIcon && sprite != null;
			if (flag5)
			{
				Transform transform4 = transform3.FindChild("Icon");
				bool flag6 = transform4 != null;
				if (flag6)
				{
					GameObject gameObject2 = transform4.gameObject;
					Image component3 = gameObject2.GetComponent<Image>();
					bool flag7 = component3 != null;
					if (flag7)
					{
						component3.overrideSprite = sprite;
					}
				}
			}
		}
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000371C File Offset: 0x0000191C
	public static GameObject CreateToggle(GameObject parentGrid, string title, Action<Player> OnEnable, Action<Player> OnDisable, string tooltip = "", bool DisplayIcon = false, Sprite sprite = null)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(MainMenu.ExampleButton, parentGrid.transform);
		gameObject.name = string.Format("Button_{0}", Guid.NewGuid());
		Transform transform = gameObject.transform.FindChild("TextLayoutParent/Text_H4");
		bool flag = transform != null;
		if (flag)
		{
			TextMeshProUGUIEx component = transform.gameObject.GetComponent<TextMeshProUGUIEx>();
			component.richText = true;
			component.text = title;
		}
		Il2CppArrayBase<ToolTip> components = gameObject.GetComponents<ToolTip>();
		foreach (ToolTip toolTip in components)
		{
			toolTip._localizableString = new LocalizableString
			{
				_localizationKey = tooltip
			};
		}
		Transform transform2 = gameObject.transform.FindChild("Badge_MMJump");
		CanvasRenderer badgeRenderer = transform2.GetComponent<CanvasRenderer>();
		VRCButtonHandle component2 = gameObject.GetComponent<VRCButtonHandle>();
		bool flag2 = component2 != null;
		if (flag2)
		{
			badgeRenderer.SetColor(new Color(1f, 0f, 0f, 1f));
			component2._sendAnalytics = false;
			component2.m_OnClick.RemoveAllListeners();
			component2.onClick.RemoveAllListeners();
			component2.onClick.AddListener(delegate()
			{
				string playerName = SelectedUser.get_selected_player_name();
				bool flag7 = !string.IsNullOrEmpty(playerName);
				if (flag7)
				{
					try
					{
						Player player = PlayerWrapper.Players.Find(delegate(Player plr)
						{
							string a;
							if (plr == null)
							{
								a = null;
							}
							else
							{
								APIUser field_Private_APIUser_ = plr.field_Private_APIUser_0;
								a = ((field_Private_APIUser_ != null) ? field_Private_APIUser_.displayName : null);
							}
							return a == playerName;
						});
						bool flag8 = player != null;
						if (flag8)
						{
							bool flag9 = ToggleStateManager.ToggleStates.ContainsKey(playerName) && ToggleStateManager.ToggleStates[playerName];
							bool flag10 = !flag9;
							ToggleStateManager.ToggleStates[playerName] = flag10;
							bool flag11 = flag10;
							if (flag11)
							{
								badgeRenderer.SetColor(new Color(0f, 1f, 0f, 1f));
								Action<Player> onEnable = OnEnable;
								if (onEnable != null)
								{
									onEnable(player);
								}
							}
							else
							{
								badgeRenderer.SetColor(new Color(1f, 0f, 0f, 1f));
								Action<Player> onDisable = OnDisable;
								if (onDisable != null)
								{
									onDisable(player);
								}
							}
						}
					}
					catch
					{
					}
				}
			});
		}
		Transform transform3 = gameObject.transform.FindChild("Icons");
		bool flag3 = transform3 != null;
		if (flag3)
		{
			transform3.gameObject.SetActive(DisplayIcon);
			bool flag4 = DisplayIcon && sprite != null;
			if (flag4)
			{
				Transform transform4 = transform3.FindChild("Icon");
				bool flag5 = transform4 != null;
				if (flag5)
				{
					GameObject gameObject2 = transform4.gameObject;
					Image component3 = gameObject2.GetComponent<Image>();
					bool flag6 = component3 != null;
					if (flag6)
					{
						component3.overrideSprite = sprite;
					}
				}
			}
		}
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003948 File Offset: 0x00001B48
	public static void Setup()
	{
		bool flag = !SelectedUser.ui_ready;
		if (flag)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), "Odium", "ButtonBackground.png");
			Sprite sprite = SpriteUtil.LoadFromDisk(path, 100f);
			bool flag2 = MainMenu.ExampleButton == null;
			if (!flag2)
			{
				bool flag3 = SelectedUser.PageGrid == null;
				if (flag3)
				{
					SelectedUser.PageGrid = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");
				}
				else
				{
					SelectedUser.ui_ready = true;
				}
			}
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000039CC File Offset: 0x00001BCC
	[CompilerGenerated]
	internal static void <Setup>g__ReplaceImageExSpritesWithoutIcons|5_0(Transform parent, Sprite newSprite)
	{
		foreach (Transform transform in parent.GetComponentsInChildren<Transform>(true))
		{
			bool flag = !transform.name.ToLower().Contains("background") && !transform.name.Contains("Cell_Wallet_Contents");
			if (!flag)
			{
				Image component = transform.GetComponent<Image>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.overrideSprite = newSprite;
				}
				TextMeshPro component2 = transform.GetComponent<TextMeshPro>();
				bool flag3 = component2 != null;
				if (flag3)
				{
					component2.color = new Color(0.3894f, 0f, 1f, 1f);
				}
				TextMeshProUGUI component3 = transform.GetComponent<TextMeshProUGUI>();
				bool flag4 = component3 != null;
				if (flag4)
				{
					component3.color = new Color(0.3894f, 0f, 1f, 1f);
				}
				TextMeshProUGUIEx component4 = transform.GetComponent<TextMeshProUGUIEx>();
				bool flag5 = component4 != null;
				if (flag5)
				{
					component4.color = new Color(0.3894f, 0f, 1f, 1f);
				}
				TMP_Text component5 = transform.GetComponent<TMP_Text>();
				bool flag6 = component5 != null;
				if (flag6)
				{
					component5.color = new Color(0.3894f, 0f, 1f, 1f);
				}
			}
		}
	}

	// Token: 0x04000027 RID: 39
	public static bool ui_ready;

	// Token: 0x04000028 RID: 40
	public static GameObject PageGrid;
}

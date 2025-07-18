using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MelonLoader;
using Odium.AwooochysResourceManagement;
using Odium.Odium;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI;
using VRC.UI.Core.Styles;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x02000083 RID: 131
	public class PlayerDebugUI
	{
		// Token: 0x0600039A RID: 922 RVA: 0x0001E484 File Offset: 0x0001C684
		public static void InitializeDebugMenu()
		{
			try
			{
				GameObject userInterface = AssignedVariables.userInterface;
				bool flag = userInterface == null;
				if (flag)
				{
					OdiumConsole.Log("DebugUI", "User interface not found", LogLevel.Info);
				}
				else
				{
					Transform transform = userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks");
					transform.gameObject.SetActive(true);
					transform.transform.Find("HeaderBackground").gameObject.SetActive(true);
					bool flag2 = transform == null;
					if (flag2)
					{
						OdiumConsole.Log("DebugUI", "Dashboard header template not found", LogLevel.Info);
					}
					else
					{
						PlayerDebugUI.label = Object.Instantiate<GameObject>(transform.gameObject);
						PlayerDebugUI.label.SetActive(true);
						PlayerDebugUI.label.transform.SetParent(userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left"));
						PlayerDebugUI.label.transform.localPosition = new Vector3(-450f, -500f, 0f);
						PlayerDebugUI.label.transform.localRotation = Quaternion.identity;
						PlayerDebugUI.label.transform.localScale = new Vector3(1f, 1f, 1f);
						UIInvisibleGraphic component = PlayerDebugUI.label.GetComponent<UIInvisibleGraphic>();
						bool flag3 = component != null;
						if (flag3)
						{
							component.enabled = false;
						}
						PlayerDebugUI.text = PlayerDebugUI.label.GetComponentInChildren<TextMeshProUGUIEx>();
						bool flag4 = PlayerDebugUI.text == null;
						if (flag4)
						{
							OdiumConsole.Log("DebugUI", "Text component not found", LogLevel.Info);
						}
						else
						{
							PlayerDebugUI.text.alignment = 257;
							PlayerDebugUI.text.outlineWidth = 0.2f;
							PlayerDebugUI.text.fontSize = 20f;
							PlayerDebugUI.text.fontSizeMax = 25f;
							PlayerDebugUI.text.fontSizeMin = 18f;
							PlayerDebugUI.text.richText = true;
							Transform transform2 = PlayerDebugUI.label.transform.Find("LeftItemContainer");
							bool flag5 = transform2 != null;
							if (flag5)
							{
								LayoutGroup component2 = transform2.GetComponent<LayoutGroup>();
								bool flag6 = component2 != null;
								if (flag6)
								{
									component2.enabled = false;
									OdiumConsole.Log("DebugUI", "Disabled LayoutGroup on LeftItemContainer", LogLevel.Info);
								}
								ContentSizeFitter component3 = transform2.GetComponent<ContentSizeFitter>();
								bool flag7 = component3 != null;
								if (flag7)
								{
									component3.enabled = false;
									OdiumConsole.Log("DebugUI", "Disabled ContentSizeFitter on LeftItemContainer", LogLevel.Info);
								}
							}
							LayoutGroup component4 = PlayerDebugUI.label.GetComponent<LayoutGroup>();
							bool flag8 = component4 != null;
							if (flag8)
							{
								component4.enabled = false;
								OdiumConsole.Log("DebugUI", "Disabled LayoutGroup on label", LogLevel.Info);
							}
							bool flag9 = PlayerDebugUI.text != null;
							if (flag9)
							{
								RectTransform component5 = PlayerDebugUI.text.GetComponent<RectTransform>();
								bool flag10 = component5 != null;
								if (flag10)
								{
									component5.anchoredPosition = new Vector2(180f, 390f);
									OdiumConsole.Log("DebugUI", string.Format("Set anchored position to: {0}", component5.anchoredPosition), LogLevel.Info);
								}
							}
							Mask mask = PlayerDebugUI.label.AddComponent<Mask>();
							mask.showMaskGraphic = false;
							Transform transform3 = PlayerDebugUI.label.transform.Find("HeaderBackground");
							PlayerDebugUI.background = ((transform3 != null) ? transform3.gameObject : null);
							bool flag11 = PlayerDebugUI.background == null;
							if (flag11)
							{
								OdiumConsole.Log("DebugUI", "Background object not found", LogLevel.Info);
							}
							else
							{
								PlayerDebugUI.background.transform.localPosition = new Vector3(0f, 0f, 0f);
								PlayerDebugUI.background.transform.localScale = new Vector3(0.6f, 10f, 1f);
								PlayerDebugUI.background.transform.localRotation = Quaternion.identity;
								PlayerDebugUI.background.SetActive(true);
								ImageEx component6 = PlayerDebugUI.background.GetComponent<ImageEx>();
								bool flag12 = component6 == null;
								if (flag12)
								{
									OdiumConsole.Log("DebugUI", "Background image component not found", LogLevel.Info);
								}
								else
								{
									string text = Path.Combine(Environment.CurrentDirectory, "Odium", "QMPlayerList.png");
									Sprite sprite = text.LoadSpriteFromDisk(512, 512);
									bool flag13 = sprite == null;
									if (flag13)
									{
										OdiumConsole.Log("DebugUI", "Failed to load background sprite from path: " + text, LogLevel.Info);
										component6.m_Color = new Color(0.443f, 0.133f, 1f, 1f);
									}
									else
									{
										component6.overrideSprite = sprite;
									}
									StyleElement component7 = PlayerDebugUI.background.GetComponent<StyleElement>();
									bool flag14 = component7 != null;
									if (flag14)
									{
										Object.Destroy(component7);
									}
									PlayerDebugUI.label.SetActive(true);
									PlayerDebugUI.background.SetActive(true);
									PlayerDebugUI.StartPlayerListLoop();
									OdiumConsole.Log("DebugUI", "Debug menu positioned correctly!", LogLevel.Info);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("DebugUI", "Failed to initialize debug menu: " + ex.Message, LogLevel.Info);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
		public static void StartPlayerListLoop()
		{
			bool flag = PlayerDebugUI.playerListCoroutine != null;
			if (flag)
			{
				PlayerDebugUI.StopPlayerListLoop();
			}
			PlayerDebugUI.playerListCoroutine = MelonCoroutines.Start(PlayerDebugUI.PlayerListLoop());
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001E9D4 File Offset: 0x0001CBD4
		public static void StopPlayerListLoop()
		{
			bool flag = PlayerDebugUI.playerListCoroutine != null;
			if (flag)
			{
				MelonCoroutines.Stop(PlayerDebugUI.playerListCoroutine);
				PlayerDebugUI.playerListCoroutine = null;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001EA04 File Offset: 0x0001CC04
		public static void AdjustPosition(float x, float y, float z)
		{
			bool flag = PlayerDebugUI.label != null;
			if (flag)
			{
				PlayerDebugUI.label.transform.localPosition = new Vector3(x, y, z);
				OdiumConsole.Log("DebugUI", string.Format("Position adjusted to: {0}, {1}, {2}", x, y, z), LogLevel.Info);
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001EA64 File Offset: 0x0001CC64
		public static void AdjustBackgroundScale(float x, float y, float z)
		{
			bool flag = PlayerDebugUI.background != null;
			if (flag)
			{
				PlayerDebugUI.background.transform.localScale = new Vector3(x, y, z);
				OdiumConsole.Log("DebugUI", string.Format("Background scale adjusted to: {0}, {1}, {2}", x, y, z), LogLevel.Info);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		public static void FixBackgroundWidth()
		{
			bool flag = PlayerDebugUI.background != null;
			if (flag)
			{
				PlayerDebugUI.background.transform.localScale = new Vector3(1f, 8f, 1f);
				OdiumConsole.Log("DebugUI", "Background width adjusted", LogLevel.Info);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001EB18 File Offset: 0x0001CD18
		public static string ColorToHex(Color color, bool includeHash = false)
		{
			string result;
			try
			{
				string text = Mathf.RoundToInt(color.r * 255f).ToString("X2");
				string text2 = Mathf.RoundToInt(color.g * 255f).ToString("X2");
				string text3 = Mathf.RoundToInt(color.b * 255f).ToString("X2");
				result = (includeHash ? ("#" + text + text2 + text3) : (text + text2 + text3));
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("DebugUI", "Failed to convert color to hex: " + ex.Message, LogLevel.Info);
				result = (includeHash ? "#FFFFFF" : "FFFFFF");
			}
			return result;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001EBE8 File Offset: 0x0001CDE8
		public static string FormatMessage(string message)
		{
			bool flag = string.IsNullOrEmpty(message);
			string result;
			if (flag)
			{
				OdiumConsole.Log("DebugUI", "Message is null or empty", LogLevel.Info);
				result = string.Empty;
			}
			else
			{
				try
				{
					string text = message;
					bool flag2 = message.Length > 68;
					if (flag2)
					{
						int num = message.LastIndexOf(' ', 68);
						bool flag3 = num == -1;
						if (flag3)
						{
							num = 68;
						}
						text = message.Substring(0, num) + "\n" + PlayerDebugUI.FormatMessage(message.Substring(num + 1));
					}
					foreach (KeyValuePair<string, Color> keyValuePair in PlayerDebugUI.keywordColors)
					{
						bool flag4 = text.Contains(keyValuePair.Key);
						if (flag4)
						{
							string str = "<color=" + PlayerDebugUI.ColorToHex(keyValuePair.Value, false) + ">";
							text = text.Replace(keyValuePair.Key, str + keyValuePair.Key + "</color>");
						}
					}
					result = text + "\n";
				}
				catch (Exception ex)
				{
					OdiumConsole.Log("DebugUI", "Failed to format message: " + ex.Message, LogLevel.Info);
					result = message + "\n";
				}
			}
			return result;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001ED50 File Offset: 0x0001CF50
		public static string GetPlatformIcon(string platform)
		{
			string text = (platform != null) ? platform.ToLower() : null;
			string a = text;
			string result;
			if (!(a == "standalonewindows"))
			{
				if (!(a == "android"))
				{
					if (!(a == "ios"))
					{
						result = "[<color=#FFFFFF>UNK</color>]";
					}
					else
					{
						result = "[<color=#FF69B4>iOS</color>]";
					}
				}
				else
				{
					result = "[<color=#32CD32>QUEST</color>]";
				}
			}
			else
			{
				result = "[<color=#00BFFF>PC</color>]";
			}
			return result;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001EDB7 File Offset: 0x0001CFB7
		public static IEnumerator PlayerListLoop()
		{
			return new PlayerDebugUI.<PlayerListLoop>d__19(0);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
		private static void UpdateDisplay()
		{
			try
			{
				bool flag = PlayerDebugUI.text == null;
				if (flag)
				{
					OdiumConsole.Log("DebugUI", "Text component is null, cannot update display", LogLevel.Info);
				}
				else
				{
					PlayerDebugUI.text.text = string.Join("", PlayerDebugUI.messageList);
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("DebugUI", "Failed to update display: " + ex.Message, LogLevel.Info);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001EE40 File Offset: 0x0001D040
		public static void Cleanup()
		{
			PlayerDebugUI.StopPlayerListLoop();
			bool flag = PlayerDebugUI.label != null;
			if (flag)
			{
				Object.Destroy(PlayerDebugUI.label);
				PlayerDebugUI.label = null;
			}
			PlayerDebugUI.background = null;
			PlayerDebugUI.text = null;
			PlayerDebugUI.messageList.Clear();
			PlayerDebugUI.displayText = "";
		}

		// Token: 0x040001D2 RID: 466
		private const int MAX_LINES = 33;

		// Token: 0x040001D3 RID: 467
		private const int MAX_CHARACTERS_PER_LINE = 68;

		// Token: 0x040001D4 RID: 468
		private const int MAX_DISPLAYED_USERS = 38;

		// Token: 0x040001D5 RID: 469
		public static GameObject label;

		// Token: 0x040001D6 RID: 470
		public static GameObject background;

		// Token: 0x040001D7 RID: 471
		public static TextMeshProUGUI text;

		// Token: 0x040001D8 RID: 472
		public static List<string> messageList = new List<string>();

		// Token: 0x040001D9 RID: 473
		private static string displayText = "";

		// Token: 0x040001DA RID: 474
		private static object playerListCoroutine;

		// Token: 0x040001DB RID: 475
		private static readonly Dictionary<string, Color> keywordColors = new Dictionary<string, Color>
		{
			{
				"Join",
				Color.green
			},
			{
				"Leave",
				Color.red
			},
			{
				"+",
				Color.green
			},
			{
				"-",
				Color.red
			},
			{
				"Debug",
				Color.yellow
			},
			{
				"Log",
				Color.magenta
			},
			{
				"Photon",
				Color.magenta
			},
			{
				"Warn",
				Color.cyan
			},
			{
				"Error",
				Color.red
			},
			{
				"RPC",
				Color.white
			}
		};
	}
}

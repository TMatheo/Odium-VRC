using System;
using MelonLoader;
using Odium.Odium;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x0200007E RID: 126
	public class QMLabel
	{
		// Token: 0x06000376 RID: 886 RVA: 0x0001CB98 File Offset: 0x0001AD98
		public static bool InitializeQuickActions()
		{
			bool result;
			try
			{
				QMLabel.quickActionsTransform = AssignedVariables.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions");
				bool flag = QMLabel.quickActionsTransform == null;
				if (flag)
				{
					MelonLogger.Error("QuickActions transform not found!");
					result = false;
				}
				else
				{
					MelonLogger.Msg("QuickActions transform found successfully!");
					result = true;
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error finding QuickActions: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		public static void InsertTextIntoExistingComponent(string text, bool append = false)
		{
			bool flag = QMLabel.quickActionsTransform == null && !QMLabel.InitializeQuickActions();
			if (!flag)
			{
				try
				{
					Text componentInChildren = QMLabel.quickActionsTransform.GetComponentInChildren<Text>();
					bool flag2 = componentInChildren != null;
					if (flag2)
					{
						if (append)
						{
							Text text2 = componentInChildren;
							text2.text += text;
						}
						else
						{
							componentInChildren.text = text;
						}
						MelonLogger.Msg("Text updated: " + componentInChildren.text);
					}
					else
					{
						TextMeshProUGUI componentInChildren2 = QMLabel.quickActionsTransform.GetComponentInChildren<TextMeshProUGUI>();
						bool flag3 = componentInChildren2 != null;
						if (flag3)
						{
							if (append)
							{
								TextMeshProUGUI textMeshProUGUI = componentInChildren2;
								textMeshProUGUI.text += text;
							}
							else
							{
								componentInChildren2.text = text;
							}
							MelonLogger.Msg("TextMeshPro updated: " + componentInChildren2.text);
						}
						else
						{
							MelonLogger.Warning("No Text or TextMeshPro component found in QuickActions!");
						}
					}
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error inserting text: " + ex.Message);
				}
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001CD34 File Offset: 0x0001AF34
		public static GameObject CreateNewTextElement(string text, int fontSize = 14, Color? color = null)
		{
			bool flag = QMLabel.quickActionsTransform == null && !QMLabel.InitializeQuickActions();
			GameObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					GameObject gameObject = new GameObject("QuickAction_Text_" + DateTime.Now.Ticks.ToString());
					gameObject.transform.SetParent(QMLabel.quickActionsTransform, false);
					RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
					rectTransform.anchorMin = Vector2.zero;
					rectTransform.anchorMax = Vector2.one;
					rectTransform.offsetMin = Vector2.zero;
					rectTransform.offsetMax = Vector2.zero;
					try
					{
						TextMeshProUGUI textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
						textMeshProUGUI.text = text;
						textMeshProUGUI.fontSize = (float)fontSize;
						textMeshProUGUI.color = (color ?? Color.white);
						textMeshProUGUI.alignment = 514;
						textMeshProUGUI.raycastTarget = false;
					}
					catch
					{
						Text text2 = gameObject.AddComponent<Text>();
						text2.text = text;
						text2.fontSize = fontSize;
						text2.color = (color ?? Color.white);
						text2.alignment = 4;
						text2.raycastTarget = false;
						text2.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
					}
					MelonLogger.Msg("Created new text element: " + text);
					result = gameObject;
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error creating text element: " + ex.Message);
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		public static void InsertTextIntoButton(int buttonIndex, string text)
		{
			bool flag = QMLabel.quickActionsTransform == null && !QMLabel.InitializeQuickActions();
			if (!flag)
			{
				try
				{
					Button[] array = QMLabel.quickActionsTransform.GetComponentsInChildren<Button>();
					bool flag2 = buttonIndex >= 0 && buttonIndex < array.Length;
					if (flag2)
					{
						Button button = array[buttonIndex];
						Text componentInChildren = button.GetComponentInChildren<Text>();
						bool flag3 = componentInChildren != null;
						if (flag3)
						{
							componentInChildren.text = text;
							MelonLogger.Msg(string.Format("Button {0} text updated: {1}", buttonIndex, text));
						}
						else
						{
							TextMeshProUGUI componentInChildren2 = button.GetComponentInChildren<TextMeshProUGUI>();
							bool flag4 = componentInChildren2 != null;
							if (flag4)
							{
								componentInChildren2.text = text;
								MelonLogger.Msg(string.Format("Button {0} TextMeshPro updated: {1}", buttonIndex, text));
							}
							else
							{
								MelonLogger.Warning(string.Format("No text component found in button {0}", buttonIndex));
							}
						}
					}
					else
					{
						MelonLogger.Error(string.Format("Button index {0} is out of range. Found {1} buttons.", buttonIndex, array.Length));
					}
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error updating button text: " + ex.Message);
				}
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001D034 File Offset: 0x0001B234
		public static string[] GetAllButtonTexts()
		{
			bool flag = QMLabel.quickActionsTransform == null && !QMLabel.InitializeQuickActions();
			string[] result;
			if (flag)
			{
				result = new string[0];
			}
			else
			{
				try
				{
					Button[] array = QMLabel.quickActionsTransform.GetComponentsInChildren<Button>();
					string[] array2 = new string[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						Text componentInChildren = array[i].GetComponentInChildren<Text>();
						TextMeshProUGUI componentInChildren2 = array[i].GetComponentInChildren<TextMeshProUGUI>();
						string[] array3 = array2;
						int num = i;
						string text;
						if ((text = ((componentInChildren != null) ? componentInChildren.text : null)) == null)
						{
							text = (((componentInChildren2 != null) ? componentInChildren2.text : null) ?? string.Format("Button_{0}", i));
						}
						array3[num] = text;
					}
					MelonLogger.Msg(string.Format("Found {0} buttons in QuickActions", array.Length));
					result = array2;
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error getting button texts: " + ex.Message);
					result = new string[0];
				}
			}
			return result;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001D140 File Offset: 0x0001B340
		public static void ClearAllText()
		{
			bool flag = QMLabel.quickActionsTransform == null && !QMLabel.InitializeQuickActions();
			if (!flag)
			{
				try
				{
					int num = 0;
					Text[] array = QMLabel.quickActionsTransform.GetComponentsInChildren<Text>();
					foreach (Text text in array)
					{
						text.text = "";
						num++;
					}
					TextMeshProUGUI[] array3 = QMLabel.quickActionsTransform.GetComponentsInChildren<TextMeshProUGUI>();
					foreach (TextMeshProUGUI textMeshProUGUI in array3)
					{
						textMeshProUGUI.text = "";
						num++;
					}
					MelonLogger.Msg(string.Format("Cleared {0} text components", num));
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error clearing text: " + ex.Message);
				}
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001D23C File Offset: 0x0001B43C
		public static void DebugQuickActionsStructure()
		{
			bool flag = QMLabel.quickActionsTransform == null && !QMLabel.InitializeQuickActions();
			if (!flag)
			{
				try
				{
					MelonLogger.Msg("=== QuickActions Structure ===");
					MelonLogger.Msg("Transform name: " + QMLabel.quickActionsTransform.name);
					MelonLogger.Msg(string.Format("Child count: {0}", QMLabel.quickActionsTransform.childCount));
					for (int i = 0; i < QMLabel.quickActionsTransform.childCount; i++)
					{
						Transform child = QMLabel.quickActionsTransform.GetChild(i);
						MelonLogger.Msg(string.Format("Child {0}: {1} (Active: {2})", i, child.name, child.gameObject.activeInHierarchy));
						Text componentInChildren = child.GetComponentInChildren<Text>();
						TextMeshProUGUI componentInChildren2 = child.GetComponentInChildren<TextMeshProUGUI>();
						Button component = child.GetComponent<Button>();
						bool flag2 = componentInChildren != null;
						if (flag2)
						{
							MelonLogger.Msg("  - Has Text: '" + componentInChildren.text + "'");
						}
						bool flag3 = componentInChildren2 != null;
						if (flag3)
						{
							MelonLogger.Msg("  - Has TextMeshPro: '" + componentInChildren2.text + "'");
						}
						bool flag4 = component != null;
						if (flag4)
						{
							MelonLogger.Msg("  - Has Button component");
						}
					}
					MelonLogger.Msg("=== End Structure ===");
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error debugging structure: " + ex.Message);
				}
			}
		}

		// Token: 0x040001C4 RID: 452
		private static Transform quickActionsTransform;
	}
}

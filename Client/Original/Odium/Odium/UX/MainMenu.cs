using System;
using System.Collections.Generic;
using System.IO;
using Odium.Components;
using Odium.Odium;
using Odium.Wrappers;
using TMPro;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Client.Marketplace;
using VRC.UI.Controls;
using VRC.UI.Elements.Controls;

namespace Odium.UX
{
	// Token: 0x02000049 RID: 73
	public class MainMenu
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000F454 File Offset: 0x0000D654
		public static MenuStateController MenuStateControllerInstance
		{
			get
			{
				bool flag = MainMenu._menuStateController == null;
				if (flag)
				{
					MainMenu._menuStateController = MainMenu.MenuInstance.GetComponent<MenuStateController>();
				}
				return MainMenu._menuStateController;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000F48C File Offset: 0x0000D68C
		public static void Setup()
		{
			DateTime now = DateTime.Now;
			bool flag = MainMenu.LaunchPadText == null;
			if (flag)
			{
				MainMenu.LaunchPadText = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title");
			}
			else
			{
				bool flag2 = MainMenu.ConsoleTemplate == null;
				if (flag2)
				{
					MainMenu.ConsoleTemplate = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds");
				}
				else
				{
					bool flag3 = MainMenu.ConsoleParent == null;
					if (flag3)
					{
						MainMenu.ConsoleParent = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners");
					}
					else
					{
						bool flag4 = MainMenu.AdBanner == null;
						if (flag4)
						{
							MainMenu.AdBanner = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/Image_MASK/Image");
						}
						else
						{
							bool flag5 = MainMenu.QuickActionsHeader == null;
							if (flag5)
							{
								MainMenu.QuickActionsHeader = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions");
							}
							else
							{
								bool flag6 = MainMenu.QuickLinksHeader == null;
								if (flag6)
								{
									MainMenu.QuickLinksHeader = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks");
								}
								else
								{
									bool flag7 = MainMenu.LinksButtons == null;
									if (flag7)
									{
										MainMenu.LinksButtons = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks");
									}
									else
									{
										bool flag8 = MainMenu.ActionsButtons == null;
										if (flag8)
										{
											MainMenu.ActionsButtons = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions");
										}
										else
										{
											bool flag9 = MainMenu.buttonsQuickLinksGrid == null;
											if (flag9)
											{
												MainMenu.buttonsQuickLinksGrid = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks");
											}
											else
											{
												bool flag10 = MainMenu.buttonsQuickActionsGrid == null;
												if (flag10)
												{
													MainMenu.buttonsQuickActionsGrid = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions");
												}
												else
												{
													bool flag11 = MainMenu.SafteyButton == null;
													if (flag11)
													{
														MainMenu.SafteyButton = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton");
													}
													bool flag12 = !MainMenu.ui_ready;
													if (flag12)
													{
														MainMenu.QuickActionsHeader.SetActive(false);
														MainMenu.QuickLinksHeader.SetActive(false);
														MainMenu.AdBanner.SetActive(false);
														MainMenu.ConsoleObject = Object.Instantiate<GameObject>(MainMenu.ConsoleTemplate, MainMenu.ConsoleParent.transform);
														MainMenu.InitConsole(MainMenu.ConsoleObject);
														MainMenu.SetupButton(MainMenu.SafteyButton, "SitStandCalibrateButton");
														MainMenu.ExampleButton = Object.Instantiate<GameObject>(MainMenu.ConsoleTemplate, MainMenu.ConsoleParent.transform);
														MainMenu.ExampleButton.name = "ExampleButton";
														MainMenu.ExampleButton.SetActive(false);
														GameObject gameObject = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Marketplace_Button_Tab");
														GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, gameObject.transform.parent);
														gameObject2.name = "Heart_Button_Tab";
														Transform transform = gameObject2.transform.Find("Icon");
														bool flag13 = transform != null;
														if (flag13)
														{
															GameObject gameObject3 = transform.gameObject;
															Image component = gameObject3.GetComponent<Image>();
															bool flag14 = component != null;
															if (flag14)
															{
																string path = Path.Combine(Directory.GetCurrentDirectory(), "Odium", "OdiumIcon.png");
																Sprite sprite = SpriteUtil.LoadFromDisk(path, 100f);
																component.sprite = sprite;
															}
														}
														Transform transform2 = gameObject2.transform.Find("Text_H4");
														bool flag15 = transform2 != null;
														if (flag15)
														{
															GameObject gameObject4 = transform2.gameObject;
															TextMeshProUGUIEx component2 = gameObject4.GetComponent<TextMeshProUGUIEx>();
															bool flag16 = component2 != null;
															if (flag16)
															{
																component2.text = "Odium Client";
															}
														}
														SubscriptionNotifierComponent component3 = gameObject2.GetComponent<SubscriptionNotifierComponent>();
														bool flag17 = component3 != null;
														if (flag17)
														{
															component3.enabled = false;
														}
														MainMenu.MenuInstance = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)");
														GameObject gameObject5 = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_MM_Marketplace");
														GameObject CustomPage = Object.Instantiate<GameObject>(gameObject5, gameObject5.transform.parent);
														MonoBehaviour1PublicOb_sGa_ppaObwapuBuObUnique component4 = CustomPage.GetComponent<MonoBehaviour1PublicOb_sGa_ppaObwapuBuObUnique>();
														GameObject gameObject6 = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)");
														MenuStateController component5 = gameObject6.GetComponent<MenuStateController>();
														component5.field_Private_Dictionary_2_String_UIPage_0.Add("XD", component4);
														component5.field_Private_HashSet_1_UIPage_0.Add(component4);
														MenuTab component6 = gameObject2.GetComponent<MenuTab>();
														component6._sendAnalytics = false;
														component6.Method_Public_set_Void_String_0("Odium");
														Button component7 = gameObject2.GetComponent<Button>();
														bool flag18 = component7 != null;
														if (flag18)
														{
															component7.onClick.RemoveAllListeners();
															component7.onClick.AddListener(delegate()
															{
																CustomPage.SetActive(true);
															});
														}
													}
													bool flag19 = MainMenu.ConsoleObject != null;
													if (flag19)
													{
														MainMenu.ConsoleObject.transform.localPosition = new Vector3(0f, -272f, 0f);
													}
													bool flag20 = MainMenu.LaunchPadText != null;
													if (flag20)
													{
														bool flag21 = (now - MainMenu.lastGradientChange).TotalMilliseconds >= 250.0;
														if (flag21)
														{
															TextMeshProUGUIEx component8 = MainMenu.LaunchPadText.GetComponent<TextMeshProUGUIEx>();
															bool flag22 = component8 != null;
															if (flag22)
															{
																component8.enableVertexGradient = true;
																MainMenu.gradientShift += Time.deltaTime * 0.5f;
																MainMenu.gradientShift += Time.deltaTime * 0.5f;
																float num = Mathf.Sin(MainMenu.gradientShift + 0f) * 0.5f + 1.5f;
																float num2 = Mathf.Sin(MainMenu.gradientShift + 1f) * 0.5f + 1.5f;
																float num3 = Mathf.Sin(MainMenu.gradientShift + 2f) * 0.5f + 1.5f;
																float num4 = Mathf.Sin(MainMenu.gradientShift + 3f) * 0.5f + 1.5f;
																VertexGradient colorGradient;
																colorGradient..ctor(new Color(num, num, num), new Color(num2, num2, num2), new Color(num3, num3, num3), new Color(num4, num4, num4));
																component8.colorGradient = colorGradient;
															}
															MainMenu.lastGradientChange = now;
														}
														TextMeshProUGUIEx component9 = MainMenu.LaunchPadText.GetComponent<TextMeshProUGUIEx>();
														bool flag23 = component9 != null;
														if (flag23)
														{
															MainMenu.SetText(component9);
														}
													}
													int childCount = MainMenu.buttonsQuickLinksGrid.transform.GetChildCount();
													for (int i = 0; i < childCount; i++)
													{
														Transform child = MainMenu.buttonsQuickLinksGrid.transform.GetChild(i);
														bool flag24 = child == null;
														if (!flag24)
														{
															string name = child.gameObject.name;
															bool flag25 = name.Contains("Button_Worlds");
															if (flag25)
															{
																child.localPosition = new Vector3(-348f, -25f, 0f);
															}
															else
															{
																bool flag26 = name.Contains("Button_Avatars");
																if (flag26)
																{
																	child.localPosition = new Vector3(-116f, -25f, 0f);
																}
																else
																{
																	bool flag27 = name.Contains("Button_Social");
																	if (flag27)
																	{
																		child.localPosition = new Vector3(116f, -25f, 0f);
																	}
																	else
																	{
																		bool flag28 = name.Contains("Button_ViewGroups");
																		if (flag28)
																		{
																			child.localPosition = new Vector3(348f, -25f, 0f);
																		}
																	}
																}
															}
															bool flag29 = !MainMenu.ui_ready;
															if (flag29)
															{
																MainMenu.SetupButton(child.gameObject, "");
																MainMenu.processed_buttons.Add(name);
															}
															else
															{
																bool flag30 = !MainMenu.processed_buttons.Contains(name);
																if (flag30)
																{
																	MainMenu.SetupButton(child.gameObject, "");
																}
															}
														}
													}
													int childCount2 = MainMenu.buttonsQuickActionsGrid.transform.GetChildCount();
													for (int j = 0; j < childCount; j++)
													{
														Transform child2 = MainMenu.buttonsQuickActionsGrid.transform.GetChild(j);
														bool flag31 = child2 == null;
														if (!flag31)
														{
															string name2 = child2.gameObject.name;
															bool flag32 = name2.Contains("Button_GoHome");
															if (flag32)
															{
																child2.localPosition = new Vector3(-225f, -15f, 0f);
															}
															else
															{
																bool flag33 = name2.Contains("Button_Respawn");
																if (flag33)
																{
																	child2.localPosition = new Vector3(0f, -15f, 0f);
																}
																else
																{
																	bool flag34 = name2.Contains("Button_SelectUser");
																	if (flag34)
																	{
																		child2.localPosition = new Vector3(225f, -15f, 0f);
																	}
																	else
																	{
																		bool flag35 = name2.Contains("SitStandCalibrateButton");
																		if (flag35)
																		{
																			child2.localPosition = new Vector3(225f, -15f, 0f);
																		}
																	}
																}
															}
															bool flag36 = !MainMenu.ui_ready;
															if (flag36)
															{
																MainMenu.SetupButton(child2.gameObject, "");
																MainMenu.processed_buttons.Add(name2);
															}
															else
															{
																bool flag37 = !MainMenu.processed_buttons.Contains(name2);
																if (flag37)
																{
																	MainMenu.SetupButton(child2.gameObject, "");
																}
																else
																{
																	bool flag38 = name2.Contains("SitStandCalibrateButton");
																	if (flag38)
																	{
																		MainMenu.SetupButton(child2.gameObject, "");
																	}
																}
															}
														}
													}
													MainMenu.LinksButtons.transform.localPosition = new Vector3(0f, -100f, 0f);
													MainMenu.ActionsButtons.transform.localPosition = new Vector3(0f, -780f, 0f);
													GameObject gameObject7 = AssignedVariables.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Safety").gameObject;
													gameObject7.gameObject.SetActive(false);
													MainMenu.ui_ready = true;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000FE1D File Offset: 0x0000E01D
		public static void SetText(TextMeshProUGUIEx textComponent)
		{
			textComponent.text = "";
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000FE2C File Offset: 0x0000E02C
		private static void SetupButton(GameObject button, string name = "")
		{
			Transform transform = button.transform.FindChild("Background");
			bool flag = transform != null;
			if (flag)
			{
				RectTransform component = transform.gameObject.GetComponent<RectTransform>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.sizeDelta = new Vector2(0f, -80f);
				}
			}
			Transform transform2 = button.transform.FindChild("Icons");
			bool flag3 = transform2 != null;
			if (flag3)
			{
				transform2.gameObject.SetActive(false);
			}
			Transform transform3 = button.transform.FindChild("Badge_MMJump");
			bool flag4 = transform3 != null;
			if (flag4)
			{
				transform3.gameObject.SetActive(false);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		private static void InitConsole(GameObject consoleClone)
		{
			Transform transform = consoleClone.transform.FindChild("Background");
			bool flag = transform != null;
			if (flag)
			{
				RectTransform component = transform.gameObject.GetComponent<RectTransform>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.sizeDelta = new Vector2(725f, 380f);
					component.transform.localPosition = new Vector2(0f, -50f);
				}
				Image component2 = transform.gameObject.GetComponent<Image>();
				bool flag3 = component2 != null;
				if (flag3)
				{
					string path = Path.Combine(Directory.GetCurrentDirectory(), "Odium", "QMConsole.png");
					Sprite overrideSprite = SpriteLoader.LoadFromDisk(path, 100f);
					component2.overrideSprite = overrideSprite;
				}
			}
			Transform transform2 = consoleClone.transform.FindChild("Icons");
			bool flag4 = transform2 != null;
			if (flag4)
			{
				transform2.gameObject.SetActive(false);
			}
			Transform transform3 = consoleClone.transform.FindChild("Badge_MMJump");
			bool flag5 = transform3 != null;
			if (flag5)
			{
				transform3.gameObject.SetActive(false);
			}
			Transform transform4 = consoleClone.transform.FindChild("TextLayoutParent/Text_H4");
			bool flag6 = transform4 != null;
			if (flag6)
			{
				RectTransform component3 = transform4.gameObject.GetComponent<RectTransform>();
				bool flag7 = component3 != null;
				if (flag7)
				{
					component3.localPosition = new Vector3(0f, 40f, 0f);
					component3.sizeDelta = new Vector2(680f, 280f);
				}
				TextMeshProUGUIEx component4 = transform4.gameObject.GetComponent<TextMeshProUGUIEx>();
				component4.alignment = 257;
				component4.richText = true;
				component4.enableWordWrapping = true;
				component4.fontSize = 14f;
				component4.lineSpacing = -10f;
				component4.text = "";
			}
			Il2CppArrayBase<ToolTip> components = consoleClone.GetComponents<ToolTip>();
			foreach (ToolTip toolTip in components)
			{
				toolTip._localizableString = new LocalizableString
				{
					_localizationKey = "[ CONSOLE ]"
				};
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00010140 File Offset: 0x0000E340
		private static void ResizeButton(Transform button)
		{
			Transform transform = button.Find("Background");
			bool flag = transform != null;
			if (flag)
			{
				RectTransform component = transform.GetComponent<RectTransform>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.sizeDelta = new Vector2(0f, -90f);
				}
			}
			Transform transform2 = button.Find("Icons/Icon");
			bool flag3 = transform2 != null;
			if (flag3)
			{
				transform2.localPosition -= new Vector3(0f, 50f, 0f);
				transform2.localScale = Vector3.zero;
			}
			Transform transform3 = button.Find("TextLayoutParent/Text_H4");
			bool flag4 = transform3 != null;
			if (flag4)
			{
				transform3.localPosition += new Vector3(0f, 50f, 0f);
				transform3.localScale = Vector3.one;
			}
			Transform transform4 = button.Find("Badge_MMJump");
			bool flag5 = transform4 != null;
			if (flag5)
			{
				transform4.localScale = Vector3.zero;
			}
		}

		// Token: 0x040000D1 RID: 209
		public static GameObject ExampleButton;

		// Token: 0x040000D2 RID: 210
		public static GameObject AdBanner;

		// Token: 0x040000D3 RID: 211
		public static GameObject QuickLinksHeader;

		// Token: 0x040000D4 RID: 212
		public static GameObject QuickActionsHeader;

		// Token: 0x040000D5 RID: 213
		public static GameObject LinksButtons;

		// Token: 0x040000D6 RID: 214
		public static GameObject ActionsButtons;

		// Token: 0x040000D7 RID: 215
		public static GameObject buttonsQuickLinksGrid;

		// Token: 0x040000D8 RID: 216
		public static GameObject buttonsQuickActionsGrid;

		// Token: 0x040000D9 RID: 217
		public static GameObject LaunchPadText;

		// Token: 0x040000DA RID: 218
		public static GameObject ConsoleParent;

		// Token: 0x040000DB RID: 219
		public static GameObject ConsoleTemplate;

		// Token: 0x040000DC RID: 220
		public static GameObject ConsoleObject;

		// Token: 0x040000DD RID: 221
		public static GameObject SafteyButton;

		// Token: 0x040000DE RID: 222
		public static GameObject UserInterface;

		// Token: 0x040000DF RID: 223
		public static bool ui_ready = false;

		// Token: 0x040000E0 RID: 224
		private static List<string> processed_buttons = new List<string>();

		// Token: 0x040000E1 RID: 225
		private static DateTime lastGradientChange;

		// Token: 0x040000E2 RID: 226
		private static DateTime lastTimeCheck = DateTime.Now;

		// Token: 0x040000E3 RID: 227
		private static float gradientShift = 0f;

		// Token: 0x040000E4 RID: 228
		public static GameObject MenuInstance;

		// Token: 0x040000E5 RID: 229
		public static MenuStateController _menuStateController;
	}
}

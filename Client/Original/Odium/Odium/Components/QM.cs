using System;
using System.Collections;
using System.Collections.Generic;
using MelonLoader;
using Odium.ButtonAPI.MM;
using Odium.ButtonAPI.QM;
using Odium.IUserPage.MM;
using Odium.Odium;
using Odium.QMPages;
using UnityEngine;

namespace Odium.Components
{
	// Token: 0x02000069 RID: 105
	public class QM
	{
		// Token: 0x060002CD RID: 717 RVA: 0x000184C7 File Offset: 0x000166C7
		public static void SetupMenu()
		{
			MelonCoroutines.Start(QM.WaitForQM());
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000184D4 File Offset: 0x000166D4
		private static IEnumerator WaitForQM()
		{
			return new QM.<WaitForQM>d__5(0);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000184DC File Offset: 0x000166DC
		public static void CreateMenu()
		{
			try
			{
				Sprite sprite = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\ButtonBackground.png", 100f);
				Sprite halfButtonImage = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\QMHalfButton.png", 100f);
				Sprite sprite2 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Deafen.png", 100f);
				Sprite sprite3 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Headphones.png", 100f);
				Sprite sprite4 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Mute.png", 100f);
				Sprite sprite5 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Microphone.png", 100f);
				Sprite sprite6 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Skip.png", 100f);
				Sprite onSprite = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Pause.png", 100f);
				Sprite sprite7 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Rewind.png", 100f);
				Sprite offSprite = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Play.png", 100f);
				Sprite sprite8 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\OdiumIcon.png", 100f);
				List<QMNestedMenu> list = Entry.Initialize(sprite, halfButtonImage);
				World.InitializePage(list[0], sprite);
				Movement.InitializePage(list[1], sprite);
				Exploits.InitializePage(list[2], sprite);
				Settings.InitializePage(list[3], sprite);
				AppBot.InitializePage(list[4], sprite, halfButtonImage);
				Visuals.InitializePage(list[5], sprite);
				GameHacks.InitializePage(list[6], sprite);
				Protections.InitializePage(list[7], sprite);
				Functions.Initialize();
				WorldFunctions.Initialize();
				QMMainIconButton.CreateButton(sprite7, delegate
				{
					MediaControls.SpotifyRewind();
				});
				QMMainIconButton.CreateToggle(onSprite, offSprite, delegate
				{
					MediaControls.SpotifyPause();
				}, delegate
				{
					MediaControls.SpotifyPause();
				});
				QMMainIconButton.CreateButton(sprite6, delegate
				{
					MediaControls.SpotifySkip();
				});
				QMMainIconButton.CreateImage(sprite8, new Vector3(-150f, -50f), new Vector3(2.5f, 2.5f), false);
				Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1");
				transform.transform.localPosition = new Vector3(125.6729f, 1024f, 0f);
				Transform transform2 = AssignedVariables.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer");
				Transform transform3 = transform2.Find("Button_QM_Report");
				transform3.gameObject.SetActive(false);
				Transform transform4 = AssignedVariables.userInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer");
				Transform transform5 = transform2.Find("Button_QM_Expand");
				transform5.gameObject.SetActive(false);
				DebugUI.InitializeDebugMenu();
				PlayerDebugUI.InitializeDebugMenu();
				SidebarListItemCloner.CreateSidebarItem("Odium Users");
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("Odium", "Error creating menu: " + ex.Message, LogLevel.Error);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00018850 File Offset: 0x00016A50
		public static IEnumerator WaitForUI()
		{
			return new QM.<WaitForUI>d__7(0);
		}

		// Token: 0x04000169 RID: 361
		public static List<string> ObjectsToFind = new List<string>
		{
			"UserInterface",
			"Canvas_QuickMenu(Clone)"
		};

		// Token: 0x0400016A RID: 362
		public static List<string> Menus = new List<string>
		{
			"Menus",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01"
		};

		// Token: 0x0400016B RID: 363
		public static List<string> Buttons = new List<string>
		{
			"Buttons",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_ViewGroups",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser",
			"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Safety"
		};

		// Token: 0x0400016C RID: 364
		public static int currentObjectIndex = 0;
	}
}

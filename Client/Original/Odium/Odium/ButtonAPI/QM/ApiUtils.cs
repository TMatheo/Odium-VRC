using System;
using System.IO;
using Odium.Components;
using Odium.Odium;
using Odium.Wrappers;
using UnityEngine;
using VRC;
using VRC.UI.Elements;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x0200007A RID: 122
	public static class ApiUtils
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		public static QuickMenu QuickMenu
		{
			get
			{
				bool flag = ApiUtils._quickMenu == null;
				if (flag)
				{
					ApiUtils._quickMenu = Resources.FindObjectsOfTypeAll<QuickMenu>()[0];
				}
				return ApiUtils._quickMenu;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001C30C File Offset: 0x0001A50C
		public static Player GetPlayerByDisplayName(string name)
		{
			return PlayerWrapper.GetAllPlayers().Find((Player plr) => plr.field_Private_APIUser_0.displayName == name);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001C344 File Offset: 0x0001A544
		public static Player GetIUser()
		{
			GameObject gameObject = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/UserProfile_Compact/PanelBG/Info/Text_Username_Friend");
			gameObject.SetActive(true);
			bool flag = gameObject == null;
			Player result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TextMeshProUGUIEx component = gameObject.GetComponent<TextMeshProUGUIEx>();
				bool flag2 = component == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Player playerByDisplayName = ApiUtils.GetPlayerByDisplayName(component.text);
					gameObject.SetActive(false);
					result = playerByDisplayName;
				}
			}
			return result;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		public static string GetMMIUser()
		{
			Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_UserDetail/Header_MM_UserName/LeftItemContainer/Text_Title");
			bool flag = transform == null;
			string result;
			if (flag)
			{
				OdiumConsole.Log("Odium", "Text object not found in GetMMIUser method", LogLevel.Error);
				result = null;
			}
			else
			{
				TextMeshProUGUIEx component = transform.GetComponent<TextMeshProUGUIEx>();
				bool flag2 = component == null;
				if (flag2)
				{
					OdiumConsole.Log("Odium", "Text component not found in GetMMIUser method", LogLevel.Error);
					result = null;
				}
				else
				{
					result = component.text;
				}
			}
			return result;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001C424 File Offset: 0x0001A624
		public static string GetMMWorldName()
		{
			Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_MM_WorldInformation/Header_MM_H1/Text_WorldName");
			bool flag = transform == null;
			string result;
			if (flag)
			{
				OdiumConsole.Log("Odium", "Text object not found in GetMMWorldName method", LogLevel.Error);
				result = null;
			}
			else
			{
				TextMeshProUGUIEx component = transform.GetComponent<TextMeshProUGUIEx>();
				bool flag2 = component == null;
				if (flag2)
				{
					OdiumConsole.Log("Odium", "Text component not found in GetMMWorldName method", LogLevel.Error);
					result = null;
				}
				else
				{
					result = component.text;
				}
			}
			return result;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001C49C File Offset: 0x0001A69C
		public static string GetFPS()
		{
			Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Debug/Debug/Settings_Panel_1/VerticalLayoutGroup/DebugStats/LeftItemContainer/Cell_MM_SettingStat (1)/Text_Detail_Original");
			bool flag = transform == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TextMeshProUGUIEx component = transform.GetComponent<TextMeshProUGUIEx>();
				bool flag2 = component == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = component.text;
				}
			}
			return result;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
		public static string GetPing()
		{
			Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Debug/Debug/Settings_Panel_1/VerticalLayoutGroup/DebugStats/LeftItemContainer/Cell_MM_SettingStat (2)/Text_Detail_Original");
			bool flag = transform == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TextMeshProUGUIEx component = transform.GetComponent<TextMeshProUGUIEx>();
				bool flag2 = component == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = component.text;
				}
			}
			return result;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001C54C File Offset: 0x0001A74C
		public static string GetBuild()
		{
			Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Debug/Debug/Settings_Panel_1/VerticalLayoutGroup/DebugStats/LeftItemContainer/Cell_MM_SettingStat (3)/Text_Detail_Original");
			bool flag = transform == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TextMeshProUGUIEx component = transform.GetComponent<TextMeshProUGUIEx>();
				bool flag2 = component == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = component.text;
				}
			}
			return result;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		public static GameObject GetSelectedUserPageGrid()
		{
			bool flag = ApiUtils._selectedUserPageGrid == null;
			GameObject selectedUserPageGrid;
			if (flag)
			{
				ApiUtils._selectedUserPageGrid = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");
				selectedUserPageGrid = ApiUtils._selectedUserPageGrid;
			}
			else
			{
				selectedUserPageGrid = ApiUtils._selectedUserPageGrid;
			}
			return selectedUserPageGrid;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		public static MainMenu MainMenu
		{
			get
			{
				bool flag = ApiUtils._socialMenu == null;
				if (flag)
				{
					ApiUtils._socialMenu = Resources.FindObjectsOfTypeAll<MainMenu>()[0];
				}
				return ApiUtils._socialMenu;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001C61C File Offset: 0x0001A81C
		public static GameObject GetQMMenuTemplate()
		{
			bool flag = ApiUtils._qmMenuTemplate == null;
			if (flag)
			{
				ApiUtils._qmMenuTemplate = ApiUtils.QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard").gameObject;
			}
			return ApiUtils._qmMenuTemplate;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001C660 File Offset: 0x0001A860
		public static GameObject GetQMTabButtonTemplate()
		{
			bool flag = ApiUtils._qmTabTemplate == null;
			if (flag)
			{
				ApiUtils._qmTabTemplate = ApiUtils.QuickMenu.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings").gameObject;
			}
			return ApiUtils._qmTabTemplate;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		public static GameObject GetQMButtonTemplate()
		{
			bool flag = ApiUtils._qmButtonTemplate == null;
			if (flag)
			{
				ApiUtils._qmButtonTemplate = ApiUtils.QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Here/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_WorldActions/Button_RejoinWorld").gameObject;
			}
			return ApiUtils._qmButtonTemplate;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001C6E8 File Offset: 0x0001A8E8
		public static GameObject GetQMSmalltTemplate()
		{
			bool flag = ApiUtils._qmButtonTemplate == null;
			if (flag)
			{
				ApiUtils._qmButtonTemplate = ApiUtils.QuickMenu.transform.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Report/").gameObject;
			}
			return ApiUtils._qmButtonTemplate;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001C72C File Offset: 0x0001A92C
		public static Sprite OnIconSprite()
		{
			return SpriteUtil.LoadFromDisk(Path.Combine(Environment.CurrentDirectory, "Odium", "ToggleSwitchOn.png"), 100f);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001C75C File Offset: 0x0001A95C
		public static Sprite OffIconSprite()
		{
			return SpriteUtil.LoadFromDisk(Path.Combine(Environment.CurrentDirectory, "Odium", "ToggleSwitchOff.png"), 100f);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001C78C File Offset: 0x0001A98C
		public static int RandomNumbers()
		{
			return ApiUtils.random.Next(100000, 999999);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001C7B4 File Offset: 0x0001A9B4
		public static string GetSelectedPageName()
		{
			return ApiUtils.QuickMenu.Method_Public_get_MenuStateController_0().field_Private_UIPage_0.field_Public_String_0;
		}

		// Token: 0x040001A3 RID: 419
		public const string Identifier = "Odium";

		// Token: 0x040001A4 RID: 420
		public static readonly Random random = new Random();

		// Token: 0x040001A5 RID: 421
		private static QuickMenu _quickMenu;

		// Token: 0x040001A6 RID: 422
		private static MainMenu _socialMenu;

		// Token: 0x040001A7 RID: 423
		private static GameObject _selectedUserPageGrid;

		// Token: 0x040001A8 RID: 424
		private static GameObject _qmMenuTemplate;

		// Token: 0x040001A9 RID: 425
		private static GameObject _qmTabTemplate;

		// Token: 0x040001AA RID: 426
		private static GameObject _qmButtonTemplate;

		// Token: 0x040001AB RID: 427
		private static Sprite _onSprite;

		// Token: 0x040001AC RID: 428
		private static Sprite _offSprite;
	}
}

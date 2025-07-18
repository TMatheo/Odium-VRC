using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Odium.ButtonAPI.MM
{
	// Token: 0x02000073 RID: 115
	public class SidebarListItemCloner
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		public static GameObject CreateUserCard(string username, Sprite userThumbnail)
		{
			GameObject gameObject = GameObject.Find(SidebarListItemCloner.USER_TEMPLATE);
			gameObject.transform.SetParent(GameObject.Find(SidebarListItemCloner.VIEWPORT).transform, false);
			gameObject.name = "ODIUM_Cell_MM_User - " + username;
			gameObject.SetActive(true);
			return gameObject;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001AE4C File Offset: 0x0001904C
		public static GameObject CreateSidebarItem(string title)
		{
			GameObject original = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Social/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_SidebarListItem (1)");
			GameObject gameObject = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Social/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup");
			GameObject gameObject2 = Object.Instantiate<GameObject>(original);
			gameObject2.transform.SetParent(gameObject.transform, false);
			gameObject2.name = "ODIUM_Cell_MM_SidebarListItem - " + title;
			gameObject2.transform.Find("Mask/Text_Name").GetComponent<TextMeshProUGUIEx>().text = title;
			gameObject2.GetComponent<Button>().onClick.RemoveAllListeners();
			gameObject2.GetComponent<Button>().onClick.AddListener(delegate()
			{
				SidebarListItemCloner.strings.ForEach(delegate(string str)
				{
					GameObject gameObject3 = GameObject.Find(SidebarListItemCloner.VIEWPORT + "/" + str);
					bool flag = gameObject3 != null;
					if (flag)
					{
						gameObject3.SetActive(false);
					}
				});
				OdiumConsole.LogGradient("OWIJRFUWEHR", "Sidebar item clicked: " + title, LogLevel.Info, false);
			});
			return gameObject2;
		}

		// Token: 0x04000191 RID: 401
		private const string SIDEBAR_ITEM_PATH = "UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Social/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_SidebarListItem (1)";

		// Token: 0x04000192 RID: 402
		private const string PARENT_CONTAINER_PATH = "UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Social/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup";

		// Token: 0x04000193 RID: 403
		public static string VIEWPORT = "UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Social/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Content";

		// Token: 0x04000194 RID: 404
		public static string USER_TEMPLATE = "UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Social/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Content/Online_CellGrid_MM_Content/Cell_MM_User";

		// Token: 0x04000195 RID: 405
		public static List<string> strings = new List<string>
		{
			"Locations_Vertical_Content",
			"Online_CellGrid_MM_Content",
			"ActiveOnAnotherPlatform",
			"MM_Foldout_Offline",
			"Offline"
		};
	}
}

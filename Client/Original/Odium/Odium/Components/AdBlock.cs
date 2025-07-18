using System;
using System.IO;
using UnityEngine;

namespace Odium.Components
{
	// Token: 0x02000054 RID: 84
	public class AdBlock
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000142EC File Offset: 0x000124EC
		public static bool DoesModExist(string mod)
		{
			string path = Path.Combine(Environment.CurrentDirectory, "Mods", mod);
			return File.Exists(path);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00014318 File Offset: 0x00012518
		public static void OnQMInit()
		{
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners").active = true;
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_VRCPlusExperiment/").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_MM_Worlds/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Landing/Header").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Dashboard/ScrollRect_MM/Viewport/Content/Panel/").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Page_MM_VRChatPlus_Account/").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/DynamicSidePanel_Header").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Header_H2/RightItemContainer").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Header_H2/LeftItemContainer").transform.localPosition = new Vector3(715f, -48f, 0f);
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/Panel_MM_Header/HeaderRight/Cell_Wallet_Contents").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/VRChat_Plus_Button_Tab").active = true;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_VRCPlusHighlight").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Marketplace_Button_Tab").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_SidebarListItem - Account/").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_MM_Worlds/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Content_WorldCategory/Carousel_Banners").active = false;
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_QM_SocialIdentity/Panel_MM_Wallet/").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/VRC+ Upsell").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Explore/").active = false;
			GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Explore/").active = false;
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Explore/").active = false;
			GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Explore/").active = false;
			GameObject.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title/").GetComponent<TextMeshProUGUIEx>().text = "VampClient";
			AdBlock.QMInitStarted = true;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000144B4 File Offset: 0x000126B4
		public static void OnUpdate()
		{
			bool qminitStarted = AdBlock.QMInitStarted;
			if (qminitStarted)
			{
				bool active = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/").active;
				if (active)
				{
					GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Page_MM_Backgrounds/").active = true;
					GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Page_MM_UIColorPalettes/").active = true;
				}
				bool active2 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Here/").active;
				if (active2)
				{
					GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Here/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_WorldActions/Button_GiftDrop/").active = false;
				}
				bool active3 = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/").active;
				if (active3)
				{
					GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions/Button_GiftVRCPlus/").active = false;
				}
				bool active4 = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Settings").active;
				if (active4)
				{
					GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/UserInterface/BackgroundDesigns/").active = false;
				}
				bool active5 = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_UserDetail/").active;
				if (active5)
				{
					GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_UserDetail/ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content/GiftBtn/").active = false;
				}
			}
		}

		// Token: 0x04000112 RID: 274
		public static bool QMInitStarted;
	}
}

using System;
using System.Collections.Generic;
using Odium.ApplicationBot;
using Odium.ButtonAPI.QM;
using Odium.Components;
using Odium.IUserPage;
using Odium.Wrappers;
using UnityEngine;

namespace Odium.QMPages
{
	// Token: 0x0200001F RID: 31
	internal class AppBot
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00007744 File Offset: 0x00005944
		public static string Current_World_id
		{
			get
			{
				return RoomManager.prop_ApiWorldInstance_0.id;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007760 File Offset: 0x00005960
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

		// Token: 0x060000EE RID: 238 RVA: 0x000077B4 File Offset: 0x000059B4
		public static void InitializePage(QMNestedMenu appBotsButton, Sprite bgImage, Sprite halfButtonImage)
		{
			Sprite TeleportIcon = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\TeleportIcon.png", 100f);
			Sprite icon = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\GoHomeIcon.png", 100f);
			Sprite icon2 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\JoinMeIcon.png", 100f);
			Sprite sprite = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\OrbitIcon.png", 100f);
			Sprite icon3 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\CogWheelIcon.png", 100f);
			Sprite sprite2 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\MovementIcon.png", 100f);
			Sprite sprite3 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\OdiumIcon.png", 100f);
			QMNestedMenu qMNestedMenu2 = new QMNestedMenu(ApiUtils.GetSelectedUserPageGrid().transform, 0f, 0f, "<color=#8d142b>Odium</color>", "<color=#8d142b>Odium</color>", "Opens Select User menu", true, sprite3, bgImage);
			QMNestedMenu appBotsPage = OdiumPage.Initialize(qMNestedMenu2, bgImage);
			QMSingleButton qmsingleButton = new QMSingleButton(appBotsButton, 1f, 0f, "Join Me", delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("JoinWorld " + AppBot.Current_World_id + " " + botId);
				});
			}, "Make all bots join your instance", false, icon2, bgImage);
			QMSingleButton qmsingleButton2 = new QMSingleButton(appBotsButton, 2f, 0f, "Go Home", delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("JoinWorld wrld_aeef8228-4e86-4774-9cbb-02027cf73730:91363~region(us) " + botId);
				});
			}, "Send all bots back to their home", false, icon, bgImage);
			QMToggleButton qmtoggleButton = new QMToggleButton(appBotsButton, 4f, 0f, "USpeak Spam", delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("USpeakSpam true " + botId);
				});
			}, delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("USpeakSpam false " + botId);
				});
			}, "Toggle bots orbiting around you", false, bgImage);
			QMSingleButton qmsingleButton3 = new QMSingleButton(appBotsButton, 1f, 2f, "TP To Me", delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.LocalPlayer.field_Private_APIUser_0.id + " " + botId);
				});
			}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
			QMToggleButton qmtoggleButton2 = new QMToggleButton(appBotsButton, 2f, 3f, "Orbit Me", delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("OrbitSelected " + PlayerWrapper.LocalPlayer.field_Private_APIUser_0.id + " " + botId);
				});
			}, delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("OrbitSelected 0 " + botId);
				});
			}, "Toggle bots orbiting around you", false, bgImage);
			new QMToggleButton(appBotsButton, 1f, 3f, "Chatbox Lagger", delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("ChatBoxLagger true " + botId);
				});
			}, delegate()
			{
				Entry.ActiveBotIds.ForEach(delegate(string botId)
				{
					SocketConnection.SendCommandToClients("OrbitSelected false " + botId);
				});
			}, "Toggle bots orbiting around you", false, bgImage);
			QMNestedMenu qMNestedMenu = new QMNestedMenu(appBotsButton, 4f, 3.5f, "Profiles", "<color=#8d142b>Profiles</color>", "Manage bot profiles", true, null, halfButtonImage);
			QMNestedMenu btnMenu = new QMNestedMenu(qMNestedMenu, 4f, 3f, "Setup", "<color=#8d142b>Setup</color>", "Manage bot profiles", true, null, halfButtonImage);
			QMNestedMenu btnMenu2 = new QMNestedMenu(qMNestedMenu, 4f, 3.5f, "Launch", "<color=#8d142b>Launch</color>", "Manage bot profiles", true, null, halfButtonImage);
			new QMSingleButton(btnMenu, 1f, 0f, "Bot 1", delegate()
			{
				Entry.LaunchBotLogin(20);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu, 2f, 0f, "Bot 2", delegate()
			{
				Entry.LaunchBotLogin(21);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu, 3f, 0f, "Bot 3", delegate()
			{
				Entry.LaunchBotLogin(22);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu, 4f, 0f, "Bot 4", delegate()
			{
				Entry.LaunchBotLogin(23);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu, 1f, 1f, "Bot 5", delegate()
			{
				Entry.LaunchBotLogin(24);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu, 2f, 1f, "Bot 6", delegate()
			{
				Entry.LaunchBotLogin(25);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu, 3f, 1f, "Bot 7", delegate()
			{
				Entry.LaunchBotLogin(26);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu, 4f, 1f, "Bot 8", delegate()
			{
				Entry.LaunchBotLogin(27);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(qMNestedMenu, 1f, 3f, "Clear Bots", delegate()
			{
				AppBot.activeBots.ForEach(delegate(QMNestedMenu botMenu)
				{
					bool flag = botMenu != null;
					if (flag)
					{
						Object.Destroy(botMenu.GetMenuObject());
					}
				});
			}, "Manage bot profiles", false, null, bgImage);
			new QMSingleButton(btnMenu2, 1f, 0f, "Bot 1", delegate()
			{
				Entry.LaunchBotHeadless(20);
				string bot = Entry.ActiveBotIds[0];
				string text = bot.Split(new char[]
				{
					'-'
				})[0];
				QMNestedMenu qmnestedMenu = new QMNestedMenu(qMNestedMenu, AppBot.xCount, AppBot.yCount, text, "<color=#8d142b>" + text + "</color>", "Manage bot profiles", false, null, bgImage);
				new QMSingleButton(qmnestedMenu, 2f, 1.5f, "TP To Me", delegate()
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.LocalPlayer.field_Private_APIUser_0.id + " " + bot);
				}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
				new QMToggleButton(qmnestedMenu, 3f, 1.5f, "Chatbox Lagger", delegate()
				{
					SocketConnection.SendCommandToClients("ChatBoxLagger true " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected false " + bot);
				}, "Toggle bots orbiting around you", false, bgImage);
				QMNestedMenu qmnestedMenu2 = new QMNestedMenu(appBotsPage, AppBot.xCount, AppBot.yCount, text, "<color=#8d142b>" + text + "</color>", "Manage bot profiles", false, null, bgImage);
				new QMSingleButton(qmnestedMenu2, 1f, 0f, "TP To", delegate()
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
				new QMToggleButton(qmnestedMenu2, 2f, 0f, "Orbit", delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected 0");
				}, "Make bots orbit the selected player", false, bgImage);
				new QMToggleButton(qmnestedMenu2, 3f, 0f, "Portal Spam", delegate()
				{
					SocketConnection.SendCommandToClients("PortalSpam " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("PortalSpam 0");
				}, "Make bots mimic selected player's movement", false, bgImage);
				new QMToggleButton(qmnestedMenu2, 4f, 0f, "IK Mimic", delegate()
				{
					SocketConnection.SendCommandToClients("MovementMimic " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("MovementMimic 0");
				}, "Make bots mimic selected player's movement", false, bgImage);
				AppBot.activeBots.Add(qmnestedMenu);
				AppBot.xCount += 1f;
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu2, 2f, 0f, "Bot 2", delegate()
			{
				Entry.LaunchBotHeadless(21);
				string bot = Entry.ActiveBotIds[1];
				string text = bot.Split(new char[]
				{
					'-'
				})[0];
				QMNestedMenu qmnestedMenu = new QMNestedMenu(qMNestedMenu, AppBot.xCount, AppBot.yCount, text, "<color=#8d142b>" + text + "</color>", "Manage bot profiles", false, null, bgImage);
				new QMSingleButton(qmnestedMenu, 2f, 1.5f, "TP To Me", delegate()
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
				new QMToggleButton(qmnestedMenu, 3f, 1.5f, "Chatbox Lagger", delegate()
				{
					SocketConnection.SendCommandToClients("ChatBoxLagger true " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected false " + bot);
				}, "Toggle bots orbiting around you", false, bgImage);
				QMNestedMenu qmnestedMenu2 = new QMNestedMenu(appBotsPage, AppBot.xCount, AppBot.yCount, text, "<color=#8d142b>" + text + "</color>", "Manage bot profiles", false, null, bgImage);
				new QMSingleButton(qmnestedMenu2, 1f, 0f, "TP To", delegate()
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
				new QMToggleButton(qmnestedMenu2, 2f, 0f, "Orbit", delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected 0");
				}, "Make bots orbit the selected player", false, bgImage);
				new QMToggleButton(qmnestedMenu2, 3f, 0f, "Portal Spam", delegate()
				{
					SocketConnection.SendCommandToClients("PortalSpam " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("PortalSpam 0");
				}, "Make bots mimic selected player's movement", false, bgImage);
				new QMToggleButton(qmnestedMenu2, 4f, 0f, "IK Mimic", delegate()
				{
					SocketConnection.SendCommandToClients("MovementMimic " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("MovementMimic 0");
				}, "Make bots mimic selected player's movement", false, bgImage);
				AppBot.activeBots.Add(qmnestedMenu);
				AppBot.xCount += 1f;
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu2, 3f, 0f, "Bot 3", delegate()
			{
				Entry.LaunchBotHeadless(22);
				string bot = Entry.ActiveBotIds[2];
				string text = bot.Split(new char[]
				{
					'-'
				})[0];
				QMNestedMenu qmnestedMenu = new QMNestedMenu(qMNestedMenu, AppBot.xCount, AppBot.yCount, text, "<color=#8d142b>" + text + "</color>", "Manage bot profiles", false, null, bgImage);
				new QMSingleButton(qmnestedMenu, 2f, 1.5f, "TP To Me", delegate()
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.LocalPlayer.field_Private_APIUser_0.id + " " + bot);
				}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
				new QMToggleButton(qmnestedMenu, 3f, 1.5f, "Chatbox Lagger", delegate()
				{
					SocketConnection.SendCommandToClients("ChatBoxLagger true " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected false " + bot);
				}, "Toggle bots orbiting around you", false, bgImage);
				QMNestedMenu qmnestedMenu2 = new QMNestedMenu(appBotsPage, AppBot.xCount, AppBot.yCount, text, "<color=#8d142b>" + text + "</color>", "Manage bot profiles", false, null, bgImage);
				new QMSingleButton(qmnestedMenu2, 1f, 0f, "TP To", delegate()
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
				new QMToggleButton(qmnestedMenu2, 2f, 0f, "Orbit", delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected 0");
				}, "Make bots orbit the selected player", false, bgImage);
				new QMToggleButton(qmnestedMenu2, 3f, 0f, "Portal Spam", delegate()
				{
					SocketConnection.SendCommandToClients("PortalSpam " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("PortalSpam 0");
				}, "Make bots mimic selected player's movement", false, bgImage);
				new QMToggleButton(qmnestedMenu2, 4f, 0f, "IK Mimic", delegate()
				{
					SocketConnection.SendCommandToClients("MovementMimic " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("MovementMimic 0");
				}, "Make bots mimic selected player's movement", false, bgImage);
				AppBot.activeBots.Add(qmnestedMenu);
				AppBot.xCount += 1f;
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu2, 4f, 0f, "Bot 4", delegate()
			{
				Entry.LaunchBotHeadless(23);
				string bot = Entry.ActiveBotIds[3];
				string text = bot.Split(new char[]
				{
					'-'
				})[0];
				QMNestedMenu qmnestedMenu = new QMNestedMenu(qMNestedMenu, AppBot.xCount, AppBot.yCount, text, "<color=#8d142b>" + text + "</color>", "Manage bot profiles", false, null, bgImage);
				new QMSingleButton(qmnestedMenu, 2f, 1.5f, "TP To Me", delegate()
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.LocalPlayer.field_Private_APIUser_0.id + " " + bot);
				}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
				new QMToggleButton(qmnestedMenu, 3f, 1.5f, "Chatbox Lagger", delegate()
				{
					SocketConnection.SendCommandToClients("ChatBoxLagger true " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected false " + bot);
				}, "Toggle bots orbiting around you", false, bgImage);
				QMNestedMenu qmnestedMenu2 = new QMNestedMenu(appBotsPage, AppBot.xCount, AppBot.yCount, text, "<color=#8d142b>" + text + "</color>", "Manage bot profiles", false, null, bgImage);
				new QMSingleButton(qmnestedMenu2, 1f, 0f, "TP To", delegate()
				{
					SocketConnection.SendCommandToClients("TeleportToPlayer " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, "Teleport all bots to your location", false, TeleportIcon, bgImage);
				new QMToggleButton(qmnestedMenu2, 2f, 0f, "Orbit", delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("OrbitSelected 0");
				}, "Make bots orbit the selected player", false, bgImage);
				new QMToggleButton(qmnestedMenu2, 3f, 0f, "Portal Spam", delegate()
				{
					SocketConnection.SendCommandToClients("PortalSpam " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("PortalSpam 0");
				}, "Make bots mimic selected player's movement", false, bgImage);
				new QMToggleButton(qmnestedMenu2, 4f, 0f, "IK Mimic", delegate()
				{
					SocketConnection.SendCommandToClients("MovementMimic " + PlayerWrapper.GetPlayerByDisplayName(AppBot.get_selected_player_name()).field_Private_APIUser_0.id + " " + bot);
				}, delegate()
				{
					SocketConnection.SendCommandToClients("MovementMimic 0");
				}, "Make bots mimic selected player's movement", false, bgImage);
				AppBot.activeBots.Add(qmnestedMenu);
				AppBot.xCount += 1f;
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu2, 1f, 1f, "Bot 5", delegate()
			{
				Entry.LaunchBotHeadless(24);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu2, 2f, 1f, "Bot 6", delegate()
			{
				Entry.LaunchBotHeadless(25);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu2, 3f, 1f, "Bot 7", delegate()
			{
				Entry.LaunchBotHeadless(26);
			}, "Send all bots back to their home", false, icon3, bgImage);
			new QMSingleButton(btnMenu2, 4f, 1f, "Bot 8", delegate()
			{
				Entry.LaunchBotHeadless(27);
			}, "Send all bots back to their home", false, icon3, bgImage);
		}

		// Token: 0x04000062 RID: 98
		public static float xCount = 1f;

		// Token: 0x04000063 RID: 99
		public static float yCount = 0f;

		// Token: 0x04000064 RID: 100
		public static int botIndex = 0;

		// Token: 0x04000065 RID: 101
		public static List<QMNestedMenu> activeBots = new List<QMNestedMenu>();
	}
}

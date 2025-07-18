using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using Odium.Components;
using Odium.Odium;
using Odium.UX;
using Odium.Wrappers;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace Odium.ApplicationBot
{
	// Token: 0x0200008B RID: 139
	public class Bot
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00020719 File Offset: 0x0001E919
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00020720 File Offset: 0x0001E920
		public static int BotProfile { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00020728 File Offset: 0x0001E928
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0002072F File Offset: 0x0001E92F
		public static string BotId { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00020737 File Offset: 0x0001E937
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0002073F File Offset: 0x0001E93F
		public string SessionId { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00020748 File Offset: 0x0001E948
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00020750 File Offset: 0x0001E950
		public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00020759 File Offset: 0x0001E959
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00020761 File Offset: 0x0001E961
		public DateTime LastPing { get; set; } = DateTime.UtcNow;

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0002076A File Offset: 0x0001E96A
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00020771 File Offset: 0x0001E971
		public static bool IsHeadlessBot { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00020779 File Offset: 0x0001E979
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00020781 File Offset: 0x0001E981
		public bool IsAlive { get; set; } = true;

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0002078C File Offset: 0x0001E98C
		public static ApiWorld Current_World
		{
			get
			{
				return RoomManager.field_Internal_Static_ApiWorld_0;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000207A4 File Offset: 0x0001E9A4
		private static void MovePlayer(Vector3 pos)
		{
			bool flag = PlayerWrapper.LocalPlayer != null;
			if (flag)
			{
				PlayerWrapper.LocalPlayer.transform.position += pos;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000207E0 File Offset: 0x0001E9E0
		public static void ReceiveCommand(string Command)
		{
			int num = Command.IndexOf(" ");
			string CMD = Command.Substring(0, num);
			string text = Command.Substring(num + 1);
			int num2 = text.IndexOf(" ");
			bool flag = num2 != -1;
			if (flag)
			{
				string Parameters = text.Substring(0, num2);
				string Parameters2 = text.Substring(num2 + 1);
				bool flag2 = Parameters2.Contains(Bot.BotId);
				if (flag2)
				{
					Bot.HandleActionOnMainThread(delegate
					{
						Bot.Commands[CMD](Parameters, Parameters2);
					});
				}
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0002088D File Offset: 0x0001EA8D
		private static void HandleActionOnMainThread(Action action)
		{
			Bot.LastActionOnMainThread = action;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00020898 File Offset: 0x0001EA98
		private static void PreGenerateMessages(int count)
		{
			for (int i = 0; i < count; i++)
			{
				char[] array = new char[144];
				for (int j = 0; j < 144; j++)
				{
					array[j] = (char)Bot.random.Next(19968, 40960);
				}
				Bot.preGeneratedMessages.Add(new string(array));
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00020904 File Offset: 0x0001EB04
		private static IEnumerator OptimizedChatboxLaggerCoroutine()
		{
			return new Bot.<OptimizedChatboxLaggerCoroutine>d__45(0);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0002090C File Offset: 0x0001EB0C
		public static void OnUpdate()
		{
			bool isApplicationBot = Bot.IsApplicationBot;
			if (isApplicationBot)
			{
				bool flag = Bot.LastActionOnMainThread != null;
				if (flag)
				{
					Bot.LastActionOnMainThread();
					Bot.LastActionOnMainThread = null;
				}
				Bot.HandleBotFunctions();
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0002094A File Offset: 0x0001EB4A
		private static IEnumerator RamClearLoop()
		{
			return new Bot.<RamClearLoop>d__52(0);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00020954 File Offset: 0x0001EB54
		private static void HandleBotFunctions()
		{
			bool flag = Bot.OrbitTarget != null && PlayerWrapper.LocalPlayer != null;
			if (flag)
			{
				Physics.gravity = new Vector3(0f, 0f, 0f);
				Bot.alpha += Time.deltaTime * Bot.OrbitSpeed;
				float num = Bot.alpha + Bot.BotOrbitOffset;
				PlayerWrapper.LocalPlayer.transform.position = new Vector3(Bot.OrbitTarget.transform.position.x + Bot.a * (float)Math.Cos((double)num), Bot.OrbitTarget.transform.position.y + Bot.Height, Bot.OrbitTarget.transform.position.z + Bot.b * (float)Math.Sin((double)num));
			}
			bool flag2 = Bot.Spinbot && PlayerWrapper.LocalPlayer != null;
			if (flag2)
			{
				PlayerWrapper.LocalPlayer.transform.Rotate(new Vector3(0f, (float)Bot.SpinbotSpeed, 0f));
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00020A78 File Offset: 0x0001EC78
		public static void Start()
		{
			bool flag = Bot.IsLaunchedAsBot();
			if (flag)
			{
				Bot.IsApplicationBot = true;
				Bot.GenerateUniqueOrbitOffset();
				OdiumConsole.LogGradient("Odium", string.Format("Running as Application Bot with assigned ID: {0}, Orbit Offset: {1:F2}", Bot.BotId, Bot.BotOrbitOffset), LogLevel.Info, false);
				SocketConnection.Client();
				Bot.RamClearLoop();
				MelonCoroutines.Start(Bot.WaitForWorldJoin());
			}
			else
			{
				OdiumConsole.LogGradient("Odium", "Starting bot server...", LogLevel.Info, false);
				SocketConnection.StartServer();
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		private static void GenerateUniqueOrbitOffset()
		{
			bool flag = !string.IsNullOrEmpty(Bot.BotId);
			if (flag)
			{
				int hashCode = Bot.BotId.GetHashCode();
				Bot.BotOrbitOffset = (float)(hashCode % 360) * 0.017453292f;
			}
			else
			{
				bool flag2 = Bot.BotProfile > 0;
				if (flag2)
				{
					Bot.BotOrbitOffset = (float)Bot.BotProfile * 60f * 0.017453292f;
				}
				else
				{
					Bot.BotOrbitOffset = (float)(new Random().NextDouble() * 2.0 * 3.141592653589793);
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00020B88 File Offset: 0x0001ED88
		public static bool IsLaunchedAsBot()
		{
			bool result;
			try
			{
				string[] commandLineArgs = Environment.GetCommandLineArgs();
				bool flag = commandLineArgs.Any((string arg) => arg.ToLower() == "--appbot");
				bool flag2 = flag;
				if (flag2)
				{
					MelonLogger.Log("Found --appBot launch parameter");
					Bot.ExtractLaunchParameters(commandLineArgs);
				}
				result = flag;
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error checking launch parameters: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00020C10 File Offset: 0x0001EE10
		public static IEnumerator WaitForWorldJoin()
		{
			return new Bot.<WaitForWorldJoin>d__66(0);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00020C18 File Offset: 0x0001EE18
		public static void NotifyWorldLeave()
		{
			try
			{
				string message = "WORLD_LEFT:" + Bot.BotId;
				SocketConnection.SendMessageToServer(message);
				OdiumConsole.LogGradient("OdiumBot", "Notified server that bot " + Bot.BotId + " left world", LogLevel.Info, false);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, null);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00020C80 File Offset: 0x0001EE80
		public static void ExtractLaunchParameters(string[] args)
		{
			try
			{
				string text = args.FirstOrDefault((string arg) => arg.StartsWith("--profile="));
				bool flag = text != null;
				if (flag)
				{
					string s = text.Split(new char[]
					{
						'='
					})[1];
					int num;
					bool flag2 = int.TryParse(s, out num);
					if (flag2)
					{
						Bot.BotProfile = num;
						MelonLogger.Log(string.Format("Bot Profile: {0}", num));
					}
				}
				string text2 = args.FirstOrDefault((string arg) => arg.StartsWith("--id="));
				bool flag3 = text2 != null;
				if (flag3)
				{
					Bot.BotId = text2.Split(new char[]
					{
						'='
					})[1];
					MelonLogger.Log("Bot ID: " + Bot.BotId);
				}
				bool flag4 = args.Any((string arg) => arg.ToLower() == "-batchmode");
				bool flag5 = flag4;
				if (flag5)
				{
					Bot.IsHeadlessBot = true;
					MelonLogger.Log("Running in headless mode");
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error extracting launch parameters: " + ex.Message);
			}
		}

		// Token: 0x040001ED RID: 493
		private const float MoveSpeed = 0.1f;

		// Token: 0x040001F5 RID: 501
		public static bool voiceMimic = false;

		// Token: 0x040001F6 RID: 502
		public static bool movementMimic = false;

		// Token: 0x040001F7 RID: 503
		public static int voiceMimicActorNr = 0;

		// Token: 0x040001F8 RID: 504
		public static int movementMimicActorNr = 0;

		// Token: 0x040001F9 RID: 505
		public static bool chatBoxLagger = false;

		// Token: 0x040001FA RID: 506
		private static float BotOrbitOffset = 0f;

		// Token: 0x040001FB RID: 507
		private static Dictionary<string, Action<string, string>> Commands = new Dictionary<string, Action<string, string>>
		{
			{
				"JoinWorld",
				delegate(string WorldID, string botId)
				{
					Console.WriteLine("[Client] Joining World " + WorldID);
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						bool flag2 = Bot.Current_World != null;
						if (flag2)
						{
							Networking.GoToRoom(WorldID);
						}
					}
				}
			},
			{
				"ToggleBlockAll",
				delegate(string UserID, string botId)
				{
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						foreach (Player player in PlayerWrapper.Players)
						{
							bool flag2 = player.field_Private_APIUser_0.id != UserID;
							if (flag2)
							{
								player.Method_Public_Void_Boolean_0(UserID != string.Empty);
							}
						}
					}
				}
			},
			{
				"OrbitSelected",
				delegate(string UserID, string botId)
				{
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						Bot.OrbitTarget = ((UserID == string.Empty) ? null : PlayerWrapper.GetVRCPlayerFromId(UserID)._player);
					}
				}
			},
			{
				"PortalSpam",
				delegate(string UserID, string botId)
				{
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						bool flag2 = !Bot.voiceMimic;
						if (flag2)
						{
							ActionWrapper.portalSpamPlayer = PlayerWrapper.GetVRCPlayerFromId(UserID)._player;
							Bot.voiceMimic = true;
						}
						else
						{
							ActionWrapper.portalSpamPlayer = null;
							Bot.voiceMimic = true;
						}
					}
				}
			},
			{
				"MovementMimic",
				delegate(string UserID, string botId)
				{
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						OdiumConsole.Log("OdiumBot", string.Format("Movement mimic called for actor -> {0} ({1})", Bot.movementMimicActorNr, UserID), LogLevel.Info);
						bool flag2 = !Bot.movementMimic;
						if (flag2)
						{
							Bot.movementMimic = true;
							Bot.movementMimicActorNr = PlayerWrapper.GetVRCPlayerFromId(UserID).prop_Player_0.prop_Int32_0;
							OdiumConsole.Log("OdiumBot", string.Format("Movement mimic enabled for actor -> {0} ({1})", Bot.movementMimicActorNr, UserID), LogLevel.Info);
						}
						else
						{
							Bot.movementMimicActorNr = 0;
							Bot.movementMimic = false;
						}
					}
				}
			},
			{
				"ChatBoxLagger",
				delegate(string boolean, string botId)
				{
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						bool flag2 = !AssignedVariables.chatboxLagger;
						if (flag2)
						{
							InternalConsole.LogIntoConsole("Chatbox lagger was enabled!", "<color=#8d142b>[Log]</color>", "8d142b");
							AssignedVariables.chatboxLagger = true;
							Bot.chatboxLaggerCoroutine = MelonCoroutines.Start(Bot.OptimizedChatboxLaggerCoroutine());
						}
						else
						{
							InternalConsole.LogIntoConsole("Chatbox lagger was disabled!", "<color=#8d142b>[Log]</color>", "8d142b");
							AssignedVariables.chatboxLagger = false;
							bool flag3 = Bot.chatboxLaggerCoroutine != null;
							if (flag3)
							{
								MelonCoroutines.Stop(Bot.chatboxLaggerCoroutine);
								Bot.chatboxLaggerCoroutine = null;
							}
							Bot.preGeneratedMessages.Clear();
						}
					}
				}
			},
			{
				"ClickTP",
				delegate(string Position, string botId)
				{
					bool flag = PlayerWrapper.LocalPlayer != null;
					if (flag)
					{
						string[] array = Position.Split(new char[]
						{
							':'
						});
						float x = float.Parse(array[0]);
						float y = float.Parse(array[1]);
						float z = float.Parse(array[2]);
						PlayerWrapper.LocalPlayer.transform.position = new Vector3(x, y, z);
					}
				}
			},
			{
				"TeleportToPlayer",
				delegate(string UserID, string botId)
				{
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						bool flag2 = PlayerWrapper.LocalPlayer != null;
						if (flag2)
						{
							Networking.LocalPlayer.TeleportTo(PlayerWrapper.GetVRCPlayerFromId(UserID)._player.transform.position, PlayerWrapper.GetVRCPlayerFromId(UserID)._player.transform.rotation);
						}
					}
				}
			},
			{
				"USpeakSpam",
				delegate(string Enabled, string botId)
				{
					USpeakSpam.ToggleUSpeakSpam(bool.Parse(Enabled));
				}
			},
			{
				"SpinbotToggle",
				delegate(string Enabled, string botId)
				{
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						Bot.Spinbot = (Enabled != string.Empty);
					}
				}
			},
			{
				"SpinbotSpeed",
				delegate(string Speed, string botId)
				{
					Bot.SpinbotSpeed = int.Parse(Speed);
				}
			},
			{
				"SetTargetFramerate",
				delegate(string Framerate, string botId)
				{
					int targetFrameRate;
					bool flag = int.TryParse(Framerate, out targetFrameRate);
					if (flag)
					{
						Application.targetFrameRate = targetFrameRate;
					}
				}
			},
			{
				"SetOrbitOffset",
				delegate(string Offset, string botId)
				{
					bool flag = botId == Bot.BotId;
					if (flag)
					{
						float botOrbitOffset;
						bool flag2 = float.TryParse(Offset, out botOrbitOffset);
						if (flag2)
						{
							Bot.BotOrbitOffset = botOrbitOffset;
						}
					}
				}
			}
		};

		// Token: 0x040001FC RID: 508
		private static List<string> preGeneratedMessages = new List<string>();

		// Token: 0x040001FD RID: 509
		private static Random random = new Random();

		// Token: 0x040001FE RID: 510
		private static object chatboxLaggerCoroutine = null;

		// Token: 0x040001FF RID: 511
		private static Player OrbitTarget;

		// Token: 0x04000200 RID: 512
		private static Action LastActionOnMainThread;

		// Token: 0x04000201 RID: 513
		private static bool EventCachingDC = false;

		// Token: 0x04000202 RID: 514
		private static bool Spinbot = false;

		// Token: 0x04000203 RID: 515
		private static int SpinbotSpeed = 20;

		// Token: 0x04000204 RID: 516
		public static float OrbitSpeed = 5f;

		// Token: 0x04000205 RID: 517
		public static float alpha = 0f;

		// Token: 0x04000206 RID: 518
		public static float a = 1f;

		// Token: 0x04000207 RID: 519
		public static float b = 1f;

		// Token: 0x04000208 RID: 520
		public static float Range = 1f;

		// Token: 0x04000209 RID: 521
		public static float Height = 0f;

		// Token: 0x0400020A RID: 522
		public static VRCPlayer currentPlayer;

		// Token: 0x0400020B RID: 523
		public static Player selectedPlayer;

		// Token: 0x0400020C RID: 524
		public static Player LagTarget;

		// Token: 0x0400020D RID: 525
		public static bool IsApplicationBot = false;
	}
}

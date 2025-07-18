using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Il2CppSystem;
using MelonLoader;
using Odium.Components;
using Odium.UX;
using Odium.Wrappers;
using UnityEngine;
using VRC;
using VRC.Core;

namespace Odium.Patches
{
	// Token: 0x02000031 RID: 49
	public class PhotonPatches
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0000BE00 File Offset: 0x0000A000
		static PhotonPatches()
		{
			PhotonPatches.StartCrashDetection();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000BECC File Offset: 0x0000A0CC
		public static void StartCrashDetection()
		{
			bool flag = PhotonPatches.crashDetectionCoroutine != null;
			if (flag)
			{
				MelonCoroutines.Stop(PhotonPatches.crashDetectionCoroutine);
			}
			PhotonPatches.crashDetectionCoroutine = MelonCoroutines.Start(PhotonPatches.CrashDetectionLoop());
			OdiumConsole.Log("CrashDetection", "Crash detection system started", LogLevel.Info);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000BF14 File Offset: 0x0000A114
		public static void StopCrashDetection()
		{
			bool flag = PhotonPatches.crashDetectionCoroutine != null;
			if (flag)
			{
				MelonCoroutines.Stop(PhotonPatches.crashDetectionCoroutine);
				PhotonPatches.crashDetectionCoroutine = null;
				OdiumConsole.Log("CrashDetection", "Crash detection system stopped", LogLevel.Info);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000BF52 File Offset: 0x0000A152
		private static IEnumerator CrashDetectionLoop()
		{
			return new PhotonPatches.<CrashDetectionLoop>d__22(0);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000BF5C File Offset: 0x0000A15C
		private static void CheckForCrashedPlayers()
		{
			float time = Time.time;
			List<int> list = new List<int>(PhotonPatches.playerActivityTracker.Keys);
			foreach (int num in list)
			{
				PlayerActivityData playerActivityData = PhotonPatches.playerActivityTracker[num];
				bool flag = !playerActivityData.wasActive || playerActivityData.hasCrashed;
				if (!flag)
				{
					float num2 = time - playerActivityData.lastEvent1Time;
					float num3 = time - playerActivityData.lastEvent12Time;
					bool flag2 = num2 > 5f && num3 > 5f;
					if (flag2)
					{
						playerActivityData.hasCrashed = true;
						PhotonPatches.playerActivityTracker[num] = playerActivityData;
						bool flag3 = !string.IsNullOrEmpty(playerActivityData.userId);
						if (flag3)
						{
							PhotonPatches.crashedPlayerIds.Add(playerActivityData.userId);
							OdiumConsole.LogGradient("CrashDetection", string.Format("Player CRASHED: {0} (Actor: {1}, UserID: {2})", playerActivityData.displayName, num, playerActivityData.userId), LogLevel.Info, false);
							InternalConsole.LogIntoConsole(string.Format("{0} has crashed (no events for {1}s)", playerActivityData.displayName, 5f), "<color=#8d142b>[Log]</color>", "8d142b");
						}
					}
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		private static void CleanupDisconnectedPlayers()
		{
			try
			{
				PlayerManager playerManager = PlayerManager.prop_PlayerManager_0;
				bool flag = ((playerManager != null) ? playerManager.field_Private_List_1_Player_0 : null) == null;
				if (!flag)
				{
					HashSet<string> currentPlayers = (from p in PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().Where(delegate(Player p)
					{
						object obj;
						if (p == null)
						{
							obj = null;
						}
						else
						{
							APIUser field_Private_APIUser_ = p.field_Private_APIUser_0;
							obj = ((field_Private_APIUser_ != null) ? field_Private_APIUser_.id : null);
						}
						return obj != null;
					})
					select p.field_Private_APIUser_0.id).ToHashSet<string>();
					List<string> list = (from userId in PhotonPatches.crashedPlayerIds
					where !currentPlayers.Contains(userId)
					select userId).ToList<string>();
					foreach (string text in list)
					{
						PhotonPatches.crashedPlayerIds.Remove(text);
						OdiumConsole.LogGradient("CrashDetection", "Removed disconnected crashed player: " + text, LogLevel.Info, false);
					}
					List<int> list2 = PhotonPatches.playerActivityTracker.Keys.Where(delegate(int actorId)
					{
						PlayerActivityData playerActivityData = PhotonPatches.playerActivityTracker[actorId];
						return !string.IsNullOrEmpty(playerActivityData.userId) && !currentPlayers.Contains(playerActivityData.userId);
					}).ToList<int>();
					foreach (int key in list2)
					{
						PhotonPatches.playerActivityTracker.Remove(key);
					}
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("CrashDetection", "Error in cleanup: " + ex.Message, LogLevel.Info);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		public static void UpdatePlayerActivity(int actorId, int eventCode)
		{
			try
			{
				Player vrcplayerFromActorNr = PlayerWrapper.GetVRCPlayerFromActorNr(actorId);
				bool flag = !PhotonPatches.playerActivityTracker.ContainsKey(actorId);
				if (flag)
				{
					PlayerActivityData playerActivityData = default(PlayerActivityData);
					playerActivityData.actorId = actorId;
					string text;
					if (vrcplayerFromActorNr == null)
					{
						text = null;
					}
					else
					{
						APIUser field_Private_APIUser_ = vrcplayerFromActorNr.field_Private_APIUser_0;
						text = ((field_Private_APIUser_ != null) ? field_Private_APIUser_.id : null);
					}
					playerActivityData.userId = (text ?? "");
					string text2;
					if (vrcplayerFromActorNr == null)
					{
						text2 = null;
					}
					else
					{
						APIUser field_Private_APIUser_2 = vrcplayerFromActorNr.field_Private_APIUser_0;
						text2 = ((field_Private_APIUser_2 != null) ? field_Private_APIUser_2.displayName : null);
					}
					playerActivityData.displayName = (text2 ?? "Unknown");
					playerActivityData.lastEvent1Time = 0f;
					playerActivityData.lastEvent12Time = 0f;
					playerActivityData.hasCrashed = false;
					playerActivityData.wasActive = false;
					PlayerActivityData value = playerActivityData;
					PhotonPatches.playerActivityTracker[actorId] = value;
				}
				PlayerActivityData playerActivityData2 = PhotonPatches.playerActivityTracker[actorId];
				float time = Time.time;
				bool flag2 = eventCode == 1;
				if (flag2)
				{
					playerActivityData2.lastEvent1Time = time;
					playerActivityData2.wasActive = true;
				}
				else
				{
					bool flag3 = eventCode == 12;
					if (flag3)
					{
						playerActivityData2.lastEvent12Time = time;
						playerActivityData2.wasActive = true;
					}
				}
				bool flag4 = playerActivityData2.hasCrashed && playerActivityData2.wasActive;
				if (flag4)
				{
					playerActivityData2.hasCrashed = false;
					bool flag5 = !string.IsNullOrEmpty(playerActivityData2.userId);
					if (flag5)
					{
						PhotonPatches.crashedPlayerIds.Remove(playerActivityData2.userId);
						OdiumConsole.LogGradient("CrashDetection", "Player RECOVERED: " + playerActivityData2.displayName + " is active again", LogLevel.Info, false);
					}
				}
				PhotonPatches.playerActivityTracker[actorId] = playerActivityData2;
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("CrashDetection", "Error updating player activity: " + ex.Message, LogLevel.Info);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000C470 File Offset: 0x0000A670
		public static bool HasPlayerCrashed(string userId)
		{
			return !string.IsNullOrEmpty(userId) && PhotonPatches.crashedPlayerIds.Contains(userId);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000C498 File Offset: 0x0000A698
		public static string GetCrashStatusInfo()
		{
			int num = (from p in PhotonPatches.playerActivityTracker.Values
			where p.wasActive && !p.hasCrashed
			select p).Count<PlayerActivityData>();
			int count = PhotonPatches.crashedPlayerIds.Count;
			return string.Format("Active: {0}, Crashed: {1}", num, count);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000C500 File Offset: 0x0000A700
		public static void MarkPlayerAsCrashed(string userId)
		{
			bool flag = !string.IsNullOrEmpty(userId);
			if (flag)
			{
				PhotonPatches.crashedPlayerIds.Add(userId);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000C52C File Offset: 0x0000A72C
		public static void UnmarkPlayerAsCrashed(string userId)
		{
			bool flag = !string.IsNullOrEmpty(userId);
			if (flag)
			{
				PhotonPatches.crashedPlayerIds.Remove(userId);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000C558 File Offset: 0x0000A758
		public static bool TryUnboxInt(Il2CppSystem.Object obj, out int result)
		{
			result = 0;
			bool result2;
			try
			{
				bool flag = obj == null;
				if (flag)
				{
					result2 = false;
				}
				else
				{
					result = obj.Unbox<int>();
					result2 = true;
				}
			}
			catch
			{
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000C59C File Offset: 0x0000A79C
		public static bool TryUnboxByte(Il2CppSystem.Object obj, out byte result)
		{
			result = 0;
			bool result2;
			try
			{
				bool flag = obj == null;
				if (flag)
				{
					result2 = false;
				}
				else
				{
					result = obj.Unbox<byte>();
					result2 = true;
				}
			}
			catch
			{
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public static bool IsKnownExploitPattern(string entryPointName, string behaviourName)
		{
			Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>
			{
				{
					"ListPatrons",
					new string[]
					{
						"Patreon Credits"
					}
				}
			};
			return dictionary.ContainsKey(entryPointName) && dictionary[entryPointName].Contains(behaviourName);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000C630 File Offset: 0x0000A830
		public static bool IsRapidFireEvent(string playerName)
		{
			DateTime now = DateTime.Now;
			bool flag = PhotonPatches.lastEventTimes.ContainsKey(playerName);
			if (flag)
			{
				bool flag2 = (now - PhotonPatches.lastEventTimes[playerName]).TotalMilliseconds < 100.0;
				if (flag2)
				{
					return true;
				}
			}
			PhotonPatches.lastEventTimes[playerName] = now;
			return false;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000C698 File Offset: 0x0000A898
		public static string GetGameObjectPath(GameObject obj)
		{
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = "null";
			}
			else
			{
				string text = obj.name;
				Transform parent = obj.transform.parent;
				while (parent != null)
				{
					text = parent.name + "/" + text;
					parent = parent.parent;
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		private static void CleanupOldCooldowns()
		{
			DateTime cutoff = DateTime.Now - TimeSpan.FromHours(1.0);
			List<string> list = (from kvp in PhotonPatches.syncAssignMCooldowns
			where kvp.Value < cutoff
			select kvp.Key).ToList<string>();
			foreach (string key in list)
			{
				PhotonPatches.syncAssignMCooldowns.Remove(key);
			}
			bool flag = list.Count > 0;
			if (flag)
			{
				InternalConsole.LogIntoConsole(string.Format("[CLEANUP] Removed {0} old cooldown entries", list.Count), "<color=#8d142b>[Log]</color>", "8d142b");
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		public static bool IsUnusualHash(uint eventHash)
		{
			bool flag = eventHash == 0U || eventHash == uint.MaxValue;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				uint[] source = new uint[]
				{
					236258089U
				};
				result = source.Contains(eventHash);
			}
			return result;
		}

		// Token: 0x04000081 RID: 129
		public static bool BlockUdon = false;

		// Token: 0x04000082 RID: 130
		public static Dictionary<int, int> blockedMessages = new Dictionary<int, int>();

		// Token: 0x04000083 RID: 131
		public static int blockedChatBoxMessages = 0;

		// Token: 0x04000084 RID: 132
		public static Dictionary<int, int> blockedMessagesCount = new Dictionary<int, int>();

		// Token: 0x04000085 RID: 133
		public static Dictionary<int, int> blockedUSpeakPacketCount = new Dictionary<int, int>();

		// Token: 0x04000086 RID: 134
		public static Dictionary<int, int> blockedUSpeakPackets = new Dictionary<int, int>();

		// Token: 0x04000087 RID: 135
		public static List<string> blockedUserIds = new List<string>();

		// Token: 0x04000088 RID: 136
		public static List<string> mutedUserIds = new List<string>();

		// Token: 0x04000089 RID: 137
		public static Dictionary<string, int> crashAttemptCounts = new Dictionary<string, int>();

		// Token: 0x0400008A RID: 138
		public static Dictionary<string, DateTime> lastLogTimes = new Dictionary<string, DateTime>();

		// Token: 0x0400008B RID: 139
		public static Dictionary<int, PlayerActivityData> playerActivityTracker = new Dictionary<int, PlayerActivityData>();

		// Token: 0x0400008C RID: 140
		public static HashSet<string> crashedPlayerIds = new HashSet<string>();

		// Token: 0x0400008D RID: 141
		private static object crashDetectionCoroutine;

		// Token: 0x0400008E RID: 142
		private const float CRASH_TIMEOUT = 5f;

		// Token: 0x0400008F RID: 143
		private const float CHECK_INTERVAL = 1f;

		// Token: 0x04000090 RID: 144
		public static Sprite LogoIcon = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\OdiumIcon.png", 100f);

		// Token: 0x04000091 RID: 145
		public static readonly Dictionary<string, DateTime> syncAssignMCooldowns = new Dictionary<string, DateTime>();

		// Token: 0x04000092 RID: 146
		public static readonly TimeSpan SYNC_ASSIGN_M_COOLDOWN = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000093 RID: 147
		public static Dictionary<string, DateTime> lastEventTimes = new Dictionary<string, DateTime>();
	}
}

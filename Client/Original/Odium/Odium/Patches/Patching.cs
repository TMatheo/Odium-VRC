using System;
using System.Net.Http;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using Odium.UX;
using VRC.Core;
using VRC.SDK3.StringLoading;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Odium.Patches
{
	// Token: 0x0200002C RID: 44
	internal class Patching
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public static void Initialize()
		{
			OdiumEntry.HarmonyInstance.Patch(typeof(VRCPlusStatus).GetProperty("prop_Object1PublicTBoTUnique_1_Boolean_0").GetGetMethod(), null, new HarmonyMethod(typeof(Patching).GetMethod("VRCPlusOverride", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null);
			Patching.patchCount++;
			OdiumEntry.HarmonyInstance.Patch(typeof(UdonBehaviour).GetMethod("SendCustomNetworkEvent", new Type[]
			{
				typeof(NetworkEventTarget),
				typeof(string)
			}), new HarmonyMethod(typeof(Patching).GetMethod("OnUdonNetworkEvent", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
			Patching.patchCount++;
			OdiumEntry.HarmonyInstance.Patch(typeof(VRCStringDownloader).GetMethod("LoadUrl"), new HarmonyMethod(typeof(Patching).GetMethod("OnStringDownload", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
			Patching.patchCount++;
			OdiumEntry.HarmonyInstance.Patch(typeof(HttpClient).GetMethod("Get"), new HarmonyMethod(typeof(Patching).GetMethod("OnGet", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
			Patching.patchCount++;
			MelonLogger.Msg(string.Format("Patches initialized successfully. Total patches: {0}", Patching.patchCount));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A958 File Offset: 0x00008B58
		private static bool OnGet(string url)
		{
			try
			{
				ApiWorld field_Internal_Static_ApiWorld_ = RoomManager.field_Internal_Static_ApiWorld_0;
				bool flag = field_Internal_Static_ApiWorld_ == null;
				if (flag)
				{
					return true;
				}
				string authorId = field_Internal_Static_ApiWorld_.authorId;
				bool flag2 = authorId == "LyCh6jlK6X";
				if (flag2)
				{
					OdiumConsole.LogGradient("BLOCKED", "Prevented string download in Jar's world (URL: " + url + ")", LogLevel.Info, false);
					return false;
				}
			}
			catch (Exception arg)
			{
				MelonLogger.Error(string.Format("Error in OnStringDownload: {0}", arg));
			}
			return true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		private static void VRCPlusOverride(ref Object1PublicTBoTUnique<bool> __result)
		{
			bool flag = __result == null;
			if (!flag)
			{
				__result.prop_T_0 = true;
				__result.field_Protected_T_0 = true;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000AA10 File Offset: 0x00008C10
		private static bool OnUdonNetworkEvent(UdonBehaviour __instance, NetworkEventTarget target, string eventName)
		{
			bool flag = eventName != "ListPatrons";
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				VRCPlayer componentInParent = __instance.GetComponentInParent<VRCPlayer>();
				bool flag2 = componentInParent == null || componentInParent == VRCPlayer.field_Internal_Static_VRCPlayer_0;
				if (flag2)
				{
					result = true;
				}
				else
				{
					InternalConsole.LogIntoConsole("[BLOCKED] Crash attempt from " + componentInParent.field_Private_VRCPlayerApi_0.displayName + "!", "[Udon]", "8d142b");
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000AA88 File Offset: 0x00008C88
		private static bool OnUdonRunProgram(UdonBehaviour __instance, string programName)
		{
			bool flag = programName != "ListPatrons";
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				VRCPlayer componentInParent = __instance.GetComponentInParent<VRCPlayer>();
				bool flag2 = componentInParent == null || componentInParent == VRCPlayer.field_Internal_Static_VRCPlayer_0;
				if (flag2)
				{
					result = true;
				}
				else
				{
					InternalConsole.LogIntoConsole("[BLOCKED] Crash attempt from " + componentInParent.field_Private_VRCPlayerApi_0.displayName + "!", "[Udon]", "8d142b");
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000AB00 File Offset: 0x00008D00
		private static bool OnStringDownload(string url)
		{
			try
			{
				ApiWorld field_Internal_Static_ApiWorld_ = RoomManager.field_Internal_Static_ApiWorld_0;
				bool flag = field_Internal_Static_ApiWorld_ == null;
				if (flag)
				{
					return true;
				}
				string authorId = field_Internal_Static_ApiWorld_.authorId;
				bool flag2 = authorId == "LyCh6jlK6X";
				if (flag2)
				{
					OdiumConsole.LogGradient("BLOCKED", "Prevented string download in Jar's world (URL: " + url + ")", LogLevel.Info, false);
					return false;
				}
			}
			catch (Exception arg)
			{
				MelonLogger.Error(string.Format("Error in OnStringDownload: {0}", arg));
			}
			return true;
		}

		// Token: 0x04000078 RID: 120
		public static int patchCount;
	}
}

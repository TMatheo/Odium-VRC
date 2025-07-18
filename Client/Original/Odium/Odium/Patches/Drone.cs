using System;
using System.Collections;
using HarmonyLib;
using MelonLoader;
using Odium.Odium;
using Odium.UX;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.PlayerDrone;

namespace Odium.Patches
{
	// Token: 0x0200002A RID: 42
	[HarmonyPatch(typeof(DroneManager))]
	internal class Drone
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000A62C File Offset: 0x0000882C
		[HarmonyPrefix]
		[HarmonyPatch("Method_Private_Void_Player_Vector3_Vector3_String_Int32_Color_Color_Color_PDM_0")]
		private static void OnDroneSpawnPrefix(DroneManager __instance, Player param_1, Vector3 param_2, Vector3 param_3, string param_4, int param_5, Color param_6, Color param_7, Color param_8)
		{
			try
			{
				string text;
				if (param_1 == null)
				{
					text = null;
				}
				else
				{
					APIUser field_Private_APIUser_ = param_1.field_Private_APIUser_0;
					text = ((field_Private_APIUser_ != null) ? field_Private_APIUser_.displayName : null);
				}
				string str = text ?? "Unknown";
				InternalConsole.LogIntoConsole("<color=#31BCF0>[DroneManager]:</color> <color=#00AAFF>Drone Spawn</color> by <color=#00FF74>" + str + "</color>", "<color=#8d142b>[Log]</color>", "8d142b");
				bool autoDroneCrash = AssignedVariables.autoDroneCrash;
				if (autoDroneCrash)
				{
					InternalConsole.LogIntoConsole("<color=#31BCF0>[AntiQuest]:</color> <color=#00AAFF>Drone Crash</color> using <color=#00FF74>" + str + "</color>'s drone!", "<color=#8d142b>[Log]</color>", "8d142b");
					MelonCoroutines.Start(Drone.DelayedDroneCrash());
				}
			}
			catch (Exception ex)
			{
				InternalConsole.LogIntoConsole(" <color=#FF0000>[ERROR]:</color> <color=#FF0000>Error in drone patch: " + ex.Message + "</color>", "<color=#8d142b>[Log]</color>", "8d142b");
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000A6F0 File Offset: 0x000088F0
		private static IEnumerator DelayedDroneCrash()
		{
			return new Drone.<DelayedDroneCrash>d__2(0);
		}

		// Token: 0x04000076 RID: 118
		public static int androidUserCount;
	}
}

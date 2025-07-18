using System;
using System.Reflection;
using ExitGames.Client.Photon;
using HarmonyLib;
using MelonLoader;
using VRC.Economy;
using VRC.SDKBase;

namespace Odium.Patches
{
	// Token: 0x02000028 RID: 40
	public class AwooochysPatchInitializer
	{
		// Token: 0x06000108 RID: 264 RVA: 0x0000A25C File Offset: 0x0000845C
		private static HarmonyMethod GetPreFix(string methodName)
		{
			return new HarmonyMethod(typeof(AwooochysPatchInitializer).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000A288 File Offset: 0x00008488
		[Obsolete]
		public static void Start()
		{
			Console.WriteLine("StartupStarting Hooks...");
			try
			{
				ClonePatch.Patch();
				AwooochysPatchInitializer.pass++;
			}
			catch (Exception ex)
			{
				Console.WriteLine(AwooochysPatchInitializer.ModuleName + "allowAvatarCopying:" + ex.Message);
				AwooochysPatchInitializer.fail++;
			}
			try
			{
				RoomManagerPatch.Patch();
				AwooochysPatchInitializer.pass++;
			}
			catch (Exception ex2)
			{
				Console.WriteLine(AwooochysPatchInitializer.ModuleName + "RoomManager:" + ex2.Message);
				AwooochysPatchInitializer.fail++;
			}
			try
			{
				EasyPatching.DeepCoreInstance.Patch(typeof(VRCPlusStatus).GetProperty("prop_Object1PublicTYBoTYUnique_1_Boolean_0").GetGetMethod(), null, AwooochysPatchInitializer.GetLocalPatch("GetVRCPlusStatus"), null, null, null);
				AwooochysPatchInitializer.pass++;
			}
			catch (Exception ex3)
			{
				Console.WriteLine(AwooochysPatchInitializer.ModuleName + "VRCPlusStatus:" + ex3.Message);
				AwooochysPatchInitializer.fail++;
			}
			try
			{
				AwooochysPatchInitializer.instance.Patch(typeof(Store).GetMethod("Method_Private_Boolean_VRCPlayerApi_IProduct_PDM_0"), AwooochysPatchInitializer.GetPreFix("RetrunPrefix"), null, null, null, null);
				AwooochysPatchInitializer.instance.Patch(typeof(Store).GetMethod("Method_Private_Boolean_IProduct_PDM_0"), AwooochysPatchInitializer.GetPreFix("RetrunPrefix"), null, null, null, null);
				AwooochysPatchInitializer.pass++;
			}
			catch (Exception ex4)
			{
				Console.WriteLine(AwooochysPatchInitializer.ModuleName + "Store:" + ex4.Message);
				AwooochysPatchInitializer.fail++;
			}
			try
			{
				AwooochysPatchInitializer.pass++;
			}
			catch (Exception ex5)
			{
				Console.WriteLine(AwooochysPatchInitializer.ModuleName + "QuickMenu:" + ex5.Message);
				AwooochysPatchInitializer.fail++;
			}
			Console.WriteLine(AwooochysPatchInitializer.ModuleName + string.Format("Placed {0} hook successfully, with {1} failed.", AwooochysPatchInitializer.pass, AwooochysPatchInitializer.fail));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000A4DC File Offset: 0x000086DC
		private static bool MarketPatch(VRCPlayerApi __0, IProduct __1, ref bool __result)
		{
			__result = true;
			return false;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000A4F4 File Offset: 0x000086F4
		private static bool RetrunPrefix(ref bool __result)
		{
			__result = true;
			return false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000A50C File Offset: 0x0000870C
		internal static bool Patch_OnEventSent(byte __0, object __1, RaiseEventOptions __2, SendOptions __3)
		{
			bool isOnEventSendDebug = PhotonDebugger.IsOnEventSendDebug;
			return !isOnEventSendDebug || PhotonDebugger.OnEventSent(__0, __1, __2, __3);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000A538 File Offset: 0x00008738
		public static HarmonyMethod GetLocalPatch(string name)
		{
			HarmonyMethod result;
			try
			{
				result = MelonUtils.ToNewHarmonyMethod(typeof(AwooochysPatchInitializer).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
			}
			catch (Exception arg)
			{
				Console.WriteLine(AwooochysPatchInitializer.ModuleName + string.Format("{0}: {1}", name, arg));
				result = null;
			}
			return result;
		}

		// Token: 0x04000072 RID: 114
		public static string ModuleName = "HookManager";

		// Token: 0x04000073 RID: 115
		public static readonly Harmony instance = new Harmony("DeepCoreV2.ultrapatch");

		// Token: 0x04000074 RID: 116
		public static int pass = 0;

		// Token: 0x04000075 RID: 117
		public static int fail = 0;
	}
}

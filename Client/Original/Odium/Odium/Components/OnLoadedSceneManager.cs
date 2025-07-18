using System;
using System.Collections;
using UnhollowerBaseLib;
using UnityEngine;
using VRC.Networking;
using VRC.SDK.Internal;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRCSDK2;

namespace Odium.Components
{
	// Token: 0x02000065 RID: 101
	public class OnLoadedSceneManager
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x00017E37 File Offset: 0x00016037
		public static void LoadedScene(int buildindex, string sceneName)
		{
			Console.WriteLine("Loaded Scene: /n" + buildindex.ToString() + sceneName);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00017E52 File Offset: 0x00016052
		public static IEnumerator WaitForLocalPlayer()
		{
			return new OnLoadedSceneManager.<WaitForLocalPlayer>d__27(0);
		}

		// Token: 0x0400014D RID: 333
		internal static float oldRespawnHight;

		// Token: 0x0400014E RID: 334
		internal static Il2CppArrayBase<VRC_Pickup> sdk2Items;

		// Token: 0x0400014F RID: 335
		internal static Il2CppArrayBase<VRCPickup> sdk3Items;

		// Token: 0x04000150 RID: 336
		internal static VRC_ObjectSync[] allSyncItems;

		// Token: 0x04000151 RID: 337
		internal static Il2CppArrayBase<VRCObjectSync> allSDK3SyncItems;

		// Token: 0x04000152 RID: 338
		internal static Il2CppArrayBase<VRCObjectPool> allPoolItems;

		// Token: 0x04000153 RID: 339
		internal static VRC_Pickup[] allBaseUdonItem;

		// Token: 0x04000154 RID: 340
		internal static Il2CppArrayBase<VRCInteractable> allInteractable;

		// Token: 0x04000155 RID: 341
		internal static Il2CppArrayBase<VRC_Interactable> allBaseInteractable;

		// Token: 0x04000156 RID: 342
		internal static Il2CppArrayBase<VRC_Interactable> allSDK2Interactable;

		// Token: 0x04000157 RID: 343
		internal static Il2CppArrayBase<VRC_Trigger> allTriggers;

		// Token: 0x04000158 RID: 344
		internal static Il2CppArrayBase<VRC_Trigger> allSDK2Triggers;

		// Token: 0x04000159 RID: 345
		internal static Il2CppArrayBase<VRC_TriggerColliderEventTrigger> allTriggerCol;

		// Token: 0x0400015A RID: 346
		private static VRC_SceneDescriptor SceneDescriptor;

		// Token: 0x0400015B RID: 347
		private static VRC_SceneDescriptor SDK2SceneDescriptor;

		// Token: 0x0400015C RID: 348
		private static VRCSceneDescriptor SDK3SceneDescriptor;

		// Token: 0x0400015D RID: 349
		internal static HighlightsFXStandalone highlightsFX;

		// Token: 0x0400015E RID: 350
		internal static UdonBehaviour[] udonBehaviours;

		// Token: 0x0400015F RID: 351
		internal static Il2CppArrayBase<UdonSync> udonSync;

		// Token: 0x04000160 RID: 352
		internal static Il2CppArrayBase<UdonManager> udonManagers = Resources.FindObjectsOfTypeAll<UdonManager>();

		// Token: 0x04000161 RID: 353
		internal static Il2CppArrayBase<OnTriggerStayProxy> udonOnTrigger;

		// Token: 0x04000162 RID: 354
		internal static Il2CppArrayBase<OnCollisionStayProxy> udonOnCol;

		// Token: 0x04000163 RID: 355
		internal static Il2CppArrayBase<OnRenderObjectProxy> udonOnRender;

		// Token: 0x04000164 RID: 356
		internal static Il2CppArrayBase<VRCUdonAnalytics> allSDKUdon;

		// Token: 0x04000165 RID: 357
		private static readonly string[] toSkip = new string[]
		{
			"PhotoCamera",
			"MirrorPickup",
			"ViewFinder",
			"AvatarDebugConsole",
			"OscDebugConsole"
		};

		// Token: 0x04000166 RID: 358
		internal static GameObject DeepCoreRpcObject;
	}
}

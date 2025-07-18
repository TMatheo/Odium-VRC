using System;
using MelonLoader;
using Odium.Components;
using VRC.Core;

namespace Odium.Patches
{
	// Token: 0x02000033 RID: 51
	internal class RoomManagerPatch
	{
		// Token: 0x0600013E RID: 318 RVA: 0x0000C989 File Offset: 0x0000AB89
		public static void Patch()
		{
			EasyPatching.EasyPatchMethodPost(typeof(RoomManager), "Method_Public_Static_Boolean_ApiWorld_ApiWorldInstance_String_Int32_0", typeof(RoomManagerPatch), "EnterWorldPatch");
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		private static void EnterWorldPatch(ApiWorld __0, ApiWorldInstance __1)
		{
			bool flag = __0 == null || __1 == null;
			bool flag2 = !flag;
			if (flag2)
			{
				Console.WriteLine(string.Concat(new string[]
				{
					"RoomManager: Joining ",
					RoomManager.field_Internal_Static_ApiWorld_0.name,
					" by ",
					RoomManager.field_Internal_Static_ApiWorld_0.authorName,
					"..."
				}));
				MelonCoroutines.Start(OnLoadedSceneManager.WaitForLocalPlayer());
				bool flag3 = __0.tags.Contains("feature_avatar_scaling_disabled");
				if (flag3)
				{
					__0.tags.Remove("feature_avatar_scaling_disabled");
				}
			}
		}
	}
}

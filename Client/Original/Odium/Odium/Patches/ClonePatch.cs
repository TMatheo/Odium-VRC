using System;
using System.Reflection;
using HarmonyLib;
using VRC.Core;

namespace Odium.Patches
{
	// Token: 0x02000029 RID: 41
	public class ClonePatch
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000A5CC File Offset: 0x000087CC
		public static void Patch()
		{
			EasyPatching.DeepCoreInstance.Patch(typeof(APIUser).GetProperty("allowAvatarCopying").GetSetMethod(), new HarmonyMethod(typeof(ClonePatch).GetMethod("Hook", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000A61D File Offset: 0x0000881D
		private static void Hook(ref bool __0)
		{
			__0 = true;
		}
	}
}

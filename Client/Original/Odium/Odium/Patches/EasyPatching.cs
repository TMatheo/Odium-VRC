using System;
using System.Reflection;
using Harmony;
using HarmonyLib;

namespace Odium.Patches
{
	// Token: 0x0200002B RID: 43
	public class EasyPatching
	{
		// Token: 0x06000116 RID: 278 RVA: 0x0000A701 File Offset: 0x00008901
		public static void EasyPatchPropertyPost(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
		{
			EasyPatching.DeepCoreInstance.Patch(AccessTools.Property(inputclass, InputMethodName).GetMethod, null, new HarmonyMethod(outputclass, outputmethodname, null), null, null, null);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000A727 File Offset: 0x00008927
		public static void EasyPatchPropertyPre(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
		{
			EasyPatching.DeepCoreInstance.Patch(AccessTools.Property(inputclass, InputMethodName).GetMethod, new HarmonyMethod(outputclass, outputmethodname, null), null, null, null, null);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000A74D File Offset: 0x0000894D
		public static void EasyPatchMethodPre(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
		{
			EasyPatching.DeepCoreInstance.Patch(inputclass.GetMethod(InputMethodName), new HarmonyMethod(AccessTools.Method(outputclass, outputmethodname, null, null)), null, null, null, null);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000A774 File Offset: 0x00008974
		public static void EasyPatchMethodPost(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
		{
			EasyPatching.DeepCoreInstance.Patch(inputclass.GetMethod(InputMethodName), null, new HarmonyMethod(AccessTools.Method(outputclass, outputmethodname, null, null)), null, null, null);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A79C File Offset: 0x0000899C
		[Obsolete]
		internal static HarmonyMethod GetLocalPatch<T>(string name)
		{
			return new HarmonyMethod(typeof(T).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
		}

		// Token: 0x04000077 RID: 119
		public static Harmony DeepCoreInstance = new Harmony("DeePatch");
	}
}

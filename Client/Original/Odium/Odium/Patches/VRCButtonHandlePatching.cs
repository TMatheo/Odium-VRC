using System;
using System.Reflection;
using HarmonyLib;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Odium.Patches
{
	// Token: 0x02000034 RID: 52
	internal class VRCButtonHandlePatching
	{
		// Token: 0x06000141 RID: 321 RVA: 0x0000CA54 File Offset: 0x0000AC54
		private static HarmonyMethod GetPatch(string name)
		{
			return new HarmonyMethod(typeof(VRCButtonHandlePatching).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000CA80 File Offset: 0x0000AC80
		public static void Initialize()
		{
			try
			{
				MethodInfo method = typeof(VRCButtonHandle).GetMethod("OnPointerClick", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				bool flag = method != null;
				if (flag)
				{
					OdiumEntry.HarmonyInstance.Patch(method, VRCButtonHandlePatching.GetPatch("OnPointerClickPrefix"), null, null, null, null);
				}
				MethodInfo method2 = typeof(VRCButtonHandle).GetMethod("HandleClick", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				bool flag2 = method2 != null;
				if (flag2)
				{
					OdiumEntry.HarmonyInstance.Patch(method2, VRCButtonHandlePatching.GetPatch("HandleClickPrefix"), null, null, null, null);
				}
				MethodInfo method3 = typeof(UnityEvent).GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
				bool flag3 = method3 != null;
				if (flag3)
				{
					OdiumEntry.HarmonyInstance.Patch(method3, VRCButtonHandlePatching.GetPatch("UnityEventInvokePrefix"), null, null, null, null);
				}
				OdiumConsole.Log("[VRCButtonHandlePatching]", "VRCButtonHandle patches initialized successfully", LogLevel.Info);
				Patching.patchCount++;
				OdiumConsole.Log("[Patching]", string.Format("Patches initialized successfully. Total patches: {0}", Patching.patchCount), LogLevel.Info);
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("[VRCButtonHandlePatching]", "Failed to initialize patches: " + ex.Message, LogLevel.Info);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		private static void OnPointerClickPrefix(VRCButtonHandle __instance, PointerEventData eventData)
		{
			OdiumConsole.Log("[VRCButtonHandle]", "OnPointerClick called on button: " + __instance.gameObject.name, LogLevel.Info);
			OdiumConsole.Log("[VRCButtonHandle]", "Button path: " + VRCButtonHandlePatching.GetGameObjectPath(__instance.gameObject), LogLevel.Info);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000CC10 File Offset: 0x0000AE10
		private static void HandleClickPrefix(VRCButtonHandle __instance)
		{
			OdiumConsole.Log("[VRCButtonHandle]", "HandleClick called on button: " + __instance.gameObject.name, LogLevel.Info);
			VRCButtonHandlePatching.LogButtonDetails(__instance);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		private static void UnityEventInvokePrefix(UnityEvent __instance)
		{
			string stackTrace = Environment.StackTrace;
			bool flag = stackTrace.Contains("VRCButtonHandle");
			if (flag)
			{
				OdiumConsole.Log("[UnityEvent]", "Invoke called from VRCButtonHandle context", LogLevel.Info);
				OdiumConsole.Log("[UnityEvent]", string.Format("Event listener count: {0}", __instance.GetPersistentEventCount()), LogLevel.Info);
				for (int i = 0; i < __instance.GetPersistentEventCount(); i++)
				{
					Object persistentTarget = __instance.GetPersistentTarget(i);
					string persistentMethodName = __instance.GetPersistentMethodName(i);
					OdiumConsole.Log("[UnityEvent]", string.Format("Listener {0}: {1}.{2}", i, (persistentTarget != null) ? persistentTarget.GetType().Name : null, persistentMethodName), LogLevel.Info);
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		private static string GetGameObjectPath(GameObject obj)
		{
			string text = obj.name;
			Transform parent = obj.transform.parent;
			while (parent != null)
			{
				text = parent.name + "/" + text;
				parent = parent.parent;
			}
			return text;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000CD40 File Offset: 0x0000AF40
		private static void LogButtonDetails(VRCButtonHandle button)
		{
			OdiumConsole.Log("[VRCButtonHandle]", "Button GameObject: " + button.gameObject.name, LogLevel.Info);
			OdiumConsole.Log("[VRCButtonHandle]", string.Format("Button Active: {0}", button.gameObject.activeInHierarchy), LogLevel.Info);
			Il2CppArrayBase<Text> componentsInChildren = button.GetComponentsInChildren<Text>();
			foreach (Text text in componentsInChildren)
			{
				OdiumConsole.Log("[VRCButtonHandle]", "Button Text: " + text.text, LogLevel.Info);
			}
			Type type = button.GetType();
			OdiumConsole.Log("[VRCButtonHandle]", "Button Type: " + type.Name, LogLevel.Info);
		}
	}
}

using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace CursorLayerMod
{
	// Token: 0x02000017 RID: 23
	public class CursorLayerMod : MelonMod
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000052A4 File Offset: 0x000034A4
		public static void OnUpdate()
		{
			bool flag = !CursorLayerMod.hasSetLayer;
			if (flag)
			{
				CursorLayerMod.SetCursorLayer();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000052C6 File Offset: 0x000034C6
		public static void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			CursorLayerMod.hasSetLayer = false;
			MelonLogger.Msg("Scene loaded: " + sceneName + ", resetting cursor layer flag");
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000052E8 File Offset: 0x000034E8
		public static void SetCursorLayer()
		{
			GameObject gameObject = CursorLayerMod.FindCursorIcon();
			bool flag = gameObject != null;
			if (flag)
			{
				bool flag2 = false;
				Canvas component = gameObject.GetComponent<Canvas>();
				bool flag3 = component != null;
				if (flag3)
				{
					component.sortingOrder = 9999;
					component.overrideSorting = true;
					flag2 = true;
					MelonLogger.Msg(string.Format("Set Canvas sorting order to {0}", 9999));
				}
				Renderer component2 = gameObject.GetComponent<Renderer>();
				bool flag4 = component2 != null;
				if (flag4)
				{
					component2.sortingOrder = 9999;
					flag2 = true;
					MelonLogger.Msg(string.Format("Set Renderer sorting order to {0}", 9999));
				}
				Graphic component3 = gameObject.GetComponent<Graphic>();
				bool flag5 = component3 != null && component3.canvas != null;
				if (flag5)
				{
					component3.canvas.sortingOrder = 9999;
					component3.canvas.overrideSorting = true;
					flag2 = true;
					MelonLogger.Msg(string.Format("Set Graphic Canvas sorting order to {0}", 9999));
				}
				Transform parent = gameObject.transform.parent;
				bool flag6 = parent != null;
				if (flag6)
				{
					gameObject.transform.SetAsLastSibling();
					flag2 = true;
					MelonLogger.Msg("Set cursor as last sibling in hierarchy");
				}
				bool flag7 = flag2;
				if (flag7)
				{
					CursorLayerMod.hasSetLayer = true;
					MelonLogger.Msg("Successfully set cursor to highest layer!");
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000544C File Offset: 0x0000364C
		public static GameObject FindCursorIcon()
		{
			GameObject gameObject = GameObject.Find("CursorManager/MouseArrow/VRCUICursorIcon");
			bool flag = gameObject != null;
			GameObject result;
			if (flag)
			{
				result = gameObject;
			}
			else
			{
				GameObject gameObject2 = GameObject.Find("CursorManager");
				bool flag2 = gameObject2 != null;
				if (flag2)
				{
					Transform transform = gameObject2.transform.Find("MouseArrow/VRCUICursorIcon");
					bool flag3 = transform != null;
					if (flag3)
					{
						return transform.gameObject;
					}
					transform = gameObject2.transform.Find("VRCUICursorIcon");
					bool flag4 = transform != null;
					if (flag4)
					{
						return transform.gameObject;
					}
				}
				GameObject[] array = Resources.FindObjectsOfTypeAll<GameObject>();
				foreach (GameObject gameObject3 in array)
				{
					bool flag5 = gameObject3.name.Contains("VRCUICursorIcon") || gameObject3.name.Contains("CursorIcon");
					if (flag5)
					{
						return gameObject3;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0400003F RID: 63
		public const int TARGET_LAYER = 9999;

		// Token: 0x04000040 RID: 64
		public const string CURSOR_PATH = "CursorManager/MouseArrow/VRCUICursorIcon";

		// Token: 0x04000041 RID: 65
		public static bool hasSetLayer;
	}
}

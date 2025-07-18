using System;
using System.Collections;
using System.IO;
using MelonLoader;
using Odium.Components;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Odium.Modules
{
	// Token: 0x0200004F RID: 79
	public static class OdiumNotificationLoader
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00012834 File Offset: 0x00010A34
		public static void Initialize()
		{
			bool isInitialized = OdiumNotificationLoader._isInitialized;
			if (isInitialized)
			{
				OdiumConsole.Log("NotificationLoader", "Already initialized, skipping...", LogLevel.Info);
			}
			else
			{
				OdiumConsole.Log("NotificationLoader", "Starting notification initialization...", LogLevel.Info);
				OdiumNotificationLoader.LoadNotifications();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00012878 File Offset: 0x00010A78
		private static void LoadNotifications()
		{
			try
			{
				bool flag = !File.Exists(OdiumNotificationLoader.NotificationBundlePath);
				if (flag)
				{
					OdiumConsole.Log("NotificationLoader", "Notification bundle file not found at: " + OdiumNotificationLoader.NotificationBundlePath, LogLevel.Error);
				}
				else
				{
					OdiumConsole.Log("NotificationLoader", "Loading AssetBundle from file...", LogLevel.Info);
					OdiumNotificationLoader._notificationBundle = AssetBundle.LoadFromFile(OdiumNotificationLoader.NotificationBundlePath);
					bool flag2 = OdiumNotificationLoader._notificationBundle == null;
					if (flag2)
					{
						OdiumConsole.Log("NotificationLoader", "Failed to load AssetBundle!", LogLevel.Error);
					}
					else
					{
						OdiumConsole.Log("NotificationLoader", "AssetBundle loaded successfully!", LogLevel.Info);
						string[] array = OdiumNotificationLoader._notificationBundle.GetAllAssetNames();
						OdiumConsole.Log("NotificationLoader", string.Format("Found {0} assets in bundle:", array.Length), LogLevel.Info);
						foreach (string str in array)
						{
							OdiumConsole.Log("NotificationLoader", "  - " + str, LogLevel.Info);
						}
						string[] array3 = new string[]
						{
							"Notification",
							"notification",
							"assets/notification.prefab",
							"notification.prefab"
						};
						foreach (string text in array3)
						{
							OdiumConsole.Log("NotificationLoader", "Trying to load prefab with name: '" + text + "'", LogLevel.Info);
							OdiumNotificationLoader._notificationPrefab = OdiumNotificationLoader._notificationBundle.LoadAsset<GameObject>(text);
							bool flag3 = OdiumNotificationLoader._notificationPrefab != null;
							if (flag3)
							{
								OdiumConsole.Log("NotificationLoader", "Successfully loaded prefab with name: '" + text + "'", LogLevel.Info);
								break;
							}
						}
						bool flag4 = OdiumNotificationLoader._notificationPrefab == null;
						if (flag4)
						{
							OdiumConsole.Log("NotificationLoader", "Standard names failed, trying to find any GameObject...", LogLevel.Warning);
							foreach (string text2 in array)
							{
								GameObject gameObject = OdiumNotificationLoader._notificationBundle.LoadAsset<GameObject>(text2);
								bool flag5 = gameObject != null;
								if (flag5)
								{
									OdiumNotificationLoader._notificationPrefab = gameObject;
									OdiumConsole.Log("NotificationLoader", "Using GameObject asset: " + text2, LogLevel.Info);
									break;
								}
							}
						}
						bool flag6 = OdiumNotificationLoader._notificationPrefab == null;
						if (flag6)
						{
							OdiumConsole.Log("NotificationLoader", "Failed to load any GameObject from AssetBundle!", LogLevel.Error);
							OdiumNotificationLoader._notificationBundle.Unload(true);
							OdiumNotificationLoader._notificationBundle = null;
						}
						else
						{
							OdiumNotificationLoader._notificationBundle.Unload(false);
							OdiumNotificationLoader._notificationBundle = null;
							OdiumNotificationLoader._isInitialized = true;
							OdiumConsole.LogGradient("NotificationLoader", "Notification system initialized successfully!", LogLevel.Info, true);
							OdiumConsole.Log("NotificationLoader", "Prefab reference: " + OdiumNotificationLoader._notificationPrefab.name, LogLevel.Info);
						}
					}
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "LoadNotifications");
				OdiumNotificationLoader._isInitialized = false;
				OdiumNotificationLoader._notificationPrefab = null;
				bool flag7 = OdiumNotificationLoader._notificationBundle != null;
				if (flag7)
				{
					OdiumNotificationLoader._notificationBundle.Unload(true);
					OdiumNotificationLoader._notificationBundle = null;
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00012B88 File Offset: 0x00010D88
		private static Transform FindOrCreateNotificationCanvas()
		{
			Transform result;
			try
			{
				Scene activeScene = SceneManager.GetActiveScene();
				OdiumConsole.Log("NotificationLoader", "Current active scene: " + activeScene.name, LogLevel.Info);
				Canvas canvas = OdiumNotificationLoader.FindBestExistingCanvas(activeScene);
				bool flag = canvas != null;
				if (flag)
				{
					OdiumConsole.Log("NotificationLoader", "Using existing canvas: " + canvas.name + " in scene: " + activeScene.name, LogLevel.Info);
					result = canvas.transform;
				}
				else
				{
					GameObject gameObject = new GameObject("OdiumNotificationCanvas");
					SceneManager.MoveGameObjectToScene(gameObject, activeScene);
					Canvas canvas2 = gameObject.AddComponent<Canvas>();
					canvas2.renderMode = 0;
					canvas2.sortingOrder = 32767;
					canvas2.pixelPerfect = false;
					CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
					canvasScaler.uiScaleMode = 1;
					canvasScaler.referenceResolution = new Vector2(1920f, 1080f);
					canvasScaler.screenMatchMode = 0;
					canvasScaler.matchWidthOrHeight = 0.5f;
					GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
					graphicRaycaster.ignoreReversedGraphics = true;
					graphicRaycaster.blockingObjects = 0;
					OdiumConsole.Log("NotificationLoader", "Created new notification canvas in active scene: " + activeScene.name, LogLevel.Info);
					result = gameObject.transform;
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "FindOrCreateNotificationCanvas");
				result = null;
			}
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00012CEC File Offset: 0x00010EEC
		private static Canvas FindBestExistingCanvas(Scene scene)
		{
			Canvas result;
			try
			{
				GameObject[] array = scene.GetRootGameObjects();
				Canvas canvas = null;
				int num = -1;
				foreach (GameObject gameObject in array)
				{
					Canvas[] array3 = gameObject.GetComponentsInChildren<Canvas>();
					foreach (Canvas canvas2 in array3)
					{
						bool flag = canvas2.renderMode == 0;
						if (flag)
						{
							bool flag2 = canvas2.sortingOrder > num;
							if (flag2)
							{
								canvas = canvas2;
								num = canvas2.sortingOrder;
							}
						}
					}
				}
				bool flag3 = canvas != null;
				if (flag3)
				{
					OdiumConsole.Log("NotificationLoader", string.Format("Found suitable existing canvas: {0} with sort order: {1}", canvas.name, num), LogLevel.Info);
				}
				result = canvas;
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "FindBestExistingCanvas");
				result = null;
			}
			return result;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00012DE4 File Offset: 0x00010FE4
		public static void ShowNotification(string text)
		{
			OdiumConsole.Log("NotificationLoader", string.Format("ShowNotification called - Initialized: {0}, Prefab null: {1}", OdiumNotificationLoader._isInitialized, OdiumNotificationLoader._notificationPrefab == null), LogLevel.Info);
			bool flag = !OdiumNotificationLoader._isInitialized;
			if (flag)
			{
				OdiumConsole.Log("NotificationLoader", "Notification system not initialized!", LogLevel.Error);
			}
			else
			{
				bool flag2 = OdiumNotificationLoader._notificationPrefab == null;
				if (flag2)
				{
					OdiumConsole.Log("NotificationLoader", "Notification prefab is null! Attempting to reinitialize...", LogLevel.Error);
					OdiumNotificationLoader._isInitialized = false;
					OdiumNotificationLoader.Initialize();
					bool flag3 = OdiumNotificationLoader._notificationPrefab == null;
					if (flag3)
					{
						OdiumConsole.Log("NotificationLoader", "Reinitialization failed - prefab still null!", LogLevel.Error);
						return;
					}
				}
				try
				{
					Scene activeScene = SceneManager.GetActiveScene();
					OdiumConsole.Log("NotificationLoader", "Target scene: " + activeScene.name, LogLevel.Info);
					GameObject gameObject = Object.Instantiate<GameObject>(OdiumNotificationLoader._notificationPrefab);
					gameObject.name = string.Format("OdiumNotification_{0}", DateTime.Now.Ticks);
					SceneManager.MoveGameObjectToScene(gameObject, activeScene);
					OdiumConsole.Log("NotificationLoader", "Instantiated notification in scene: " + gameObject.scene.name, LogLevel.Info);
					Canvas component = gameObject.GetComponent<Canvas>();
					bool flag4 = component != null;
					if (flag4)
					{
						component.enabled = true;
						component.renderMode = 0;
						component.sortingOrder = 32768;
						component.overrideSorting = true;
						component.pixelPerfect = false;
						OdiumConsole.Log("NotificationLoader", string.Format("Configured Canvas - Enabled: {0}, SortOrder: {1}", component.enabled, component.sortingOrder), LogLevel.Info);
					}
					else
					{
						OdiumConsole.Log("NotificationLoader", "No Canvas component found on notification prefab!", LogLevel.Warning);
					}
					gameObject.SetActive(true);
					RectTransform component2 = gameObject.GetComponent<RectTransform>();
					bool flag5 = component2 != null;
					if (flag5)
					{
						component2.localScale = Vector3.one;
						component2.localPosition = Vector3.zero;
						component2.localEulerAngles = Vector3.zero;
						component2.anchorMin = new Vector2(1f, 1f);
						component2.anchorMax = new Vector2(1f, 1f);
						component2.pivot = new Vector2(1f, 1f);
						component2.anchoredPosition = new Vector2(-50f, -50f);
						bool flag6 = component2.sizeDelta.x <= 0f || component2.sizeDelta.y <= 0f;
						if (flag6)
						{
							component2.sizeDelta = new Vector2(300f, 100f);
						}
						component2.localScale = new Vector3(1.1f, 1.1f, 1f);
						OdiumConsole.Log("NotificationLoader", string.Format("Positioned notification - AnchoredPos: {0}, Size: {1}", component2.anchoredPosition, component2.sizeDelta), LogLevel.Info);
					}
					TextMeshProUGUI componentInChildren = gameObject.GetComponentInChildren<TextMeshProUGUI>();
					bool flag7 = componentInChildren != null;
					if (flag7)
					{
						componentInChildren.text = text;
						componentInChildren.enabled = true;
						componentInChildren.gameObject.SetActive(true);
						OdiumConsole.Log("NotificationLoader", "Set notification text: '" + text + "'", LogLevel.Info);
					}
					else
					{
						Text componentInChildren2 = gameObject.GetComponentInChildren<Text>();
						bool flag8 = componentInChildren2 != null;
						if (flag8)
						{
							componentInChildren2.text = text;
							componentInChildren2.enabled = true;
							componentInChildren2.gameObject.SetActive(true);
							OdiumConsole.Log("NotificationLoader", "Set notification text using legacy Text component: '" + text + "'", LogLevel.Info);
						}
						else
						{
							OdiumConsole.Log("NotificationLoader", "Could not find any text component!", LogLevel.Warning);
						}
					}
					Transform[] array = gameObject.GetComponentsInChildren<Transform>(true);
					foreach (Transform transform in array)
					{
						transform.gameObject.SetActive(true);
					}
					Behaviour[] array3 = gameObject.GetComponentsInChildren<Behaviour>(true);
					foreach (Behaviour behaviour in array3)
					{
						behaviour.enabled = true;
					}
					Canvas.ForceUpdateCanvases();
					bool flag9 = component2 != null;
					if (flag9)
					{
						LayoutRebuilder.ForceRebuildLayoutImmediate(component2);
					}
					OdiumConsole.Log("NotificationLoader", "Final notification scene: " + gameObject.scene.name, LogLevel.Info);
					MelonCoroutines.Start(OdiumNotificationLoader.DestroyNotificationAfterDelay(gameObject, 9f));
					OdiumConsole.LogGradient("NotificationLoader", "Notification shown: " + text, LogLevel.Info, false);
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, "ShowNotification");
				}
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000132C4 File Offset: 0x000114C4
		private static IEnumerator DestroyNotificationAfterDelay(GameObject notification, float delay)
		{
			OdiumNotificationLoader.<DestroyNotificationAfterDelay>d__9 <DestroyNotificationAfterDelay>d__ = new OdiumNotificationLoader.<DestroyNotificationAfterDelay>d__9(0);
			<DestroyNotificationAfterDelay>d__.notification = notification;
			<DestroyNotificationAfterDelay>d__.delay = delay;
			return <DestroyNotificationAfterDelay>d__;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000132DC File Offset: 0x000114DC
		public static bool IsInitialized()
		{
			bool flag = OdiumNotificationLoader._isInitialized && OdiumNotificationLoader._notificationPrefab != null;
			OdiumConsole.Log("NotificationLoader", string.Format("IsInitialized check - _isInitialized: {0}, _notificationPrefab != null: {1}, Result: {2}", OdiumNotificationLoader._isInitialized, OdiumNotificationLoader._notificationPrefab != null, flag), LogLevel.Info);
			return flag;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001333C File Offset: 0x0001153C
		public static void Cleanup()
		{
			try
			{
				for (int i = 0; i < SceneManager.sceneCount; i++)
				{
					Scene sceneAt = SceneManager.GetSceneAt(i);
					bool flag = sceneAt.IsValid() && sceneAt.isLoaded;
					if (flag)
					{
						GameObject[] array = sceneAt.GetRootGameObjects();
						foreach (GameObject gameObject in array)
						{
							bool flag2 = gameObject.name == "OdiumNotificationCanvas";
							if (flag2)
							{
								Object.Destroy(gameObject);
								OdiumConsole.Log("NotificationLoader", "Destroyed notification canvas in scene: " + sceneAt.name, LogLevel.Info);
							}
						}
					}
				}
				bool flag3 = OdiumNotificationLoader._notificationBundle != null;
				if (flag3)
				{
					OdiumNotificationLoader._notificationBundle.Unload(true);
					OdiumNotificationLoader._notificationBundle = null;
				}
				OdiumNotificationLoader._notificationPrefab = null;
				OdiumNotificationLoader._isInitialized = false;
				OdiumConsole.Log("NotificationLoader", "Notification system cleaned up", LogLevel.Info);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "NotificationCleanup");
			}
		}

		// Token: 0x040000FD RID: 253
		private static AssetBundle _notificationBundle;

		// Token: 0x040000FE RID: 254
		private static GameObject _notificationPrefab;

		// Token: 0x040000FF RID: 255
		private static bool _isInitialized = false;

		// Token: 0x04000100 RID: 256
		private static readonly string NotificationBundlePath = Path.Combine(ModSetup.GetOdiumFolderPath(), "AssetBundles", "notification");
	}
}

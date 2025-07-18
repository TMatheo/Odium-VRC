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
	// Token: 0x0200004D RID: 77
	public static class OdiumBottomNotification
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00011004 File Offset: 0x0000F204
		public static void Initialize()
		{
			bool isInitialized = OdiumBottomNotification._isInitialized;
			if (isInitialized)
			{
				OdiumConsole.Log("NotificationLoader", "Already initialized, skipping...", LogLevel.Info);
			}
			else
			{
				OdiumConsole.Log("NotificationLoader", "Starting notification initialization...", LogLevel.Info);
				OdiumBottomNotification.LoadNotifications();
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00011048 File Offset: 0x0000F248
		private static void LoadNotifications()
		{
			try
			{
				bool flag = !File.Exists(OdiumBottomNotification.NotificationBundlePath);
				if (flag)
				{
					OdiumConsole.Log("NotificationLoader", "Notification bundle file not found at: " + OdiumBottomNotification.NotificationBundlePath, LogLevel.Error);
				}
				else
				{
					OdiumConsole.Log("NotificationLoader", "Loading AssetBundle from file...", LogLevel.Info);
					OdiumBottomNotification._notificationBundle = AssetBundle.LoadFromFile(OdiumBottomNotification.NotificationBundlePath);
					bool flag2 = OdiumBottomNotification._notificationBundle == null;
					if (flag2)
					{
						OdiumConsole.Log("NotificationLoader", "Failed to load AssetBundle!", LogLevel.Error);
					}
					else
					{
						OdiumConsole.Log("NotificationLoader", "AssetBundle loaded successfully!", LogLevel.Info);
						string[] array = OdiumBottomNotification._notificationBundle.GetAllAssetNames();
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
							OdiumBottomNotification._notificationPrefab = OdiumBottomNotification._notificationBundle.LoadAsset<GameObject>(text);
							bool flag3 = OdiumBottomNotification._notificationPrefab != null;
							if (flag3)
							{
								OdiumConsole.Log("NotificationLoader", "Successfully loaded prefab with name: '" + text + "'", LogLevel.Info);
								break;
							}
						}
						bool flag4 = OdiumBottomNotification._notificationPrefab == null;
						if (flag4)
						{
							OdiumConsole.Log("NotificationLoader", "Standard names failed, trying to find any GameObject...", LogLevel.Warning);
							foreach (string text2 in array)
							{
								GameObject gameObject = OdiumBottomNotification._notificationBundle.LoadAsset<GameObject>(text2);
								bool flag5 = gameObject != null;
								if (flag5)
								{
									OdiumBottomNotification._notificationPrefab = gameObject;
									OdiumConsole.Log("NotificationLoader", "Using GameObject asset: " + text2, LogLevel.Info);
									break;
								}
							}
						}
						bool flag6 = OdiumBottomNotification._notificationPrefab == null;
						if (flag6)
						{
							OdiumConsole.Log("NotificationLoader", "Failed to load any GameObject from AssetBundle!", LogLevel.Error);
							OdiumBottomNotification._notificationBundle.Unload(true);
							OdiumBottomNotification._notificationBundle = null;
						}
						else
						{
							OdiumBottomNotification._notificationBundle.Unload(false);
							OdiumBottomNotification._notificationBundle = null;
							OdiumBottomNotification._isInitialized = true;
							OdiumConsole.LogGradient("NotificationLoader", "Notification system initialized successfully!", LogLevel.Info, true);
							OdiumConsole.Log("NotificationLoader", "Prefab reference: " + OdiumBottomNotification._notificationPrefab.name, LogLevel.Info);
						}
					}
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "LoadNotifications");
				OdiumBottomNotification._isInitialized = false;
				OdiumBottomNotification._notificationPrefab = null;
				bool flag7 = OdiumBottomNotification._notificationBundle != null;
				if (flag7)
				{
					OdiumBottomNotification._notificationBundle.Unload(true);
					OdiumBottomNotification._notificationBundle = null;
				}
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00011358 File Offset: 0x0000F558
		private static Transform FindOrCreateNotificationCanvas()
		{
			Transform result;
			try
			{
				Scene activeScene = SceneManager.GetActiveScene();
				OdiumConsole.Log("NotificationLoader", "Current active scene: " + activeScene.name, LogLevel.Info);
				Canvas canvas = OdiumBottomNotification.FindBestExistingCanvas(activeScene);
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

		// Token: 0x060001FB RID: 507 RVA: 0x000114BC File Offset: 0x0000F6BC
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

		// Token: 0x060001FC RID: 508 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public static void ShowNotification(string text)
		{
			OdiumConsole.Log("NotificationLoader", string.Format("ShowNotification called - Initialized: {0}, Prefab null: {1}", OdiumBottomNotification._isInitialized, OdiumBottomNotification._notificationPrefab == null), LogLevel.Info);
			bool flag = !OdiumBottomNotification._isInitialized;
			if (flag)
			{
				OdiumConsole.Log("NotificationLoader", "Notification system not initialized!", LogLevel.Error);
			}
			else
			{
				bool flag2 = OdiumBottomNotification._notificationPrefab == null;
				if (flag2)
				{
					OdiumConsole.Log("NotificationLoader", "Notification prefab is null! Attempting to reinitialize...", LogLevel.Error);
					OdiumBottomNotification._isInitialized = false;
					OdiumBottomNotification.Initialize();
					bool flag3 = OdiumBottomNotification._notificationPrefab == null;
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
					GameObject gameObject = Object.Instantiate<GameObject>(OdiumBottomNotification._notificationPrefab);
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
					MelonCoroutines.Start(OdiumBottomNotification.DestroyNotificationAfterDelay(gameObject, 9f));
					OdiumConsole.LogGradient("NotificationLoader", "Notification shown: " + text, LogLevel.Info, false);
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, "ShowNotification");
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00011A94 File Offset: 0x0000FC94
		private static IEnumerator DestroyNotificationAfterDelay(GameObject notification, float delay)
		{
			OdiumBottomNotification.<DestroyNotificationAfterDelay>d__9 <DestroyNotificationAfterDelay>d__ = new OdiumBottomNotification.<DestroyNotificationAfterDelay>d__9(0);
			<DestroyNotificationAfterDelay>d__.notification = notification;
			<DestroyNotificationAfterDelay>d__.delay = delay;
			return <DestroyNotificationAfterDelay>d__;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00011AAC File Offset: 0x0000FCAC
		public static bool IsInitialized()
		{
			bool flag = OdiumBottomNotification._isInitialized && OdiumBottomNotification._notificationPrefab != null;
			OdiumConsole.Log("NotificationLoader", string.Format("IsInitialized check - _isInitialized: {0}, _notificationPrefab != null: {1}, Result: {2}", OdiumBottomNotification._isInitialized, OdiumBottomNotification._notificationPrefab != null, flag), LogLevel.Info);
			return flag;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00011B0C File Offset: 0x0000FD0C
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
				bool flag3 = OdiumBottomNotification._notificationBundle != null;
				if (flag3)
				{
					OdiumBottomNotification._notificationBundle.Unload(true);
					OdiumBottomNotification._notificationBundle = null;
				}
				OdiumBottomNotification._notificationPrefab = null;
				OdiumBottomNotification._isInitialized = false;
				OdiumConsole.Log("NotificationLoader", "Notification system cleaned up", LogLevel.Info);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "NotificationCleanup");
			}
		}

		// Token: 0x040000F4 RID: 244
		private static AssetBundle _notificationBundle;

		// Token: 0x040000F5 RID: 245
		private static GameObject _notificationPrefab;

		// Token: 0x040000F6 RID: 246
		private static bool _isInitialized = false;

		// Token: 0x040000F7 RID: 247
		private static readonly string NotificationBundlePath = Path.Combine(ModSetup.GetOdiumFolderPath(), "AssetBundles", "bottomnotification");
	}
}

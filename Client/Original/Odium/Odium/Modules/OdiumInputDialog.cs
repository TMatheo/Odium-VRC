using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MelonLoader;
using Odium.Components;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Odium.Modules
{
	// Token: 0x0200004E RID: 78
	public static class OdiumInputDialog
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00011C50 File Offset: 0x0000FE50
		public static void Initialize()
		{
			bool isInitialized = OdiumInputDialog._isInitialized;
			if (isInitialized)
			{
				OdiumConsole.Log("InputDialog", "Already initialized, skipping...", LogLevel.Info);
			}
			else
			{
				OdiumConsole.Log("InputDialog", "Starting input dialog initialization...", LogLevel.Info);
				OdiumInputDialog.LoadInputDialog();
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00011C94 File Offset: 0x0000FE94
		private static void LoadInputDialog()
		{
			try
			{
				bool flag = !File.Exists(OdiumInputDialog.InputBundlePath);
				if (flag)
				{
					OdiumConsole.Log("InputDialog", "Input dialog bundle file not found at: " + OdiumInputDialog.InputBundlePath, LogLevel.Error);
				}
				else
				{
					OdiumConsole.Log("InputDialog", "Loading AssetBundle from file...", LogLevel.Info);
					OdiumInputDialog._inputBundle = AssetBundle.LoadFromFile(OdiumInputDialog.InputBundlePath);
					bool flag2 = OdiumInputDialog._inputBundle == null;
					if (flag2)
					{
						OdiumConsole.Log("InputDialog", "Failed to load AssetBundle!", LogLevel.Error);
					}
					else
					{
						OdiumConsole.Log("InputDialog", "AssetBundle loaded successfully!", LogLevel.Info);
						string[] array = OdiumInputDialog._inputBundle.GetAllAssetNames();
						OdiumConsole.Log("InputDialog", string.Format("Found {0} assets in bundle:", array.Length), LogLevel.Info);
						foreach (string str in array)
						{
							OdiumConsole.Log("InputDialog", "  - " + str, LogLevel.Info);
						}
						string[] array3 = new string[]
						{
							"OdiumInputSystem",
							"inputdialog",
							"assets/inputdialog.prefab",
							"inputdialog.prefab"
						};
						foreach (string text in array3)
						{
							OdiumConsole.Log("InputDialog", "Trying to load prefab with name: '" + text + "'", LogLevel.Info);
							OdiumInputDialog._inputDialogPrefab = OdiumInputDialog._inputBundle.LoadAsset<GameObject>(text);
							bool flag3 = OdiumInputDialog._inputDialogPrefab != null;
							if (flag3)
							{
								OdiumConsole.Log("InputDialog", "Successfully loaded prefab with name: '" + text + "'", LogLevel.Info);
								break;
							}
						}
						bool flag4 = OdiumInputDialog._inputDialogPrefab == null;
						if (flag4)
						{
							OdiumConsole.Log("InputDialog", "Standard names failed, trying to find any GameObject...", LogLevel.Warning);
							foreach (string text2 in array)
							{
								GameObject gameObject = OdiumInputDialog._inputBundle.LoadAsset<GameObject>(text2);
								bool flag5 = gameObject != null;
								if (flag5)
								{
									OdiumInputDialog._inputDialogPrefab = gameObject;
									OdiumConsole.Log("InputDialog", "Using GameObject asset: " + text2, LogLevel.Info);
									break;
								}
							}
						}
						bool flag6 = OdiumInputDialog._inputDialogPrefab == null;
						if (flag6)
						{
							OdiumConsole.Log("InputDialog", "Failed to load any GameObject from AssetBundle!", LogLevel.Error);
							OdiumInputDialog._inputBundle.Unload(true);
							OdiumInputDialog._inputBundle = null;
						}
						else
						{
							OdiumInputDialog._inputBundle.Unload(false);
							OdiumInputDialog._inputBundle = null;
							OdiumInputDialog._isInitialized = true;
							OdiumConsole.LogGradient("InputDialog", "Input dialog system initialized successfully!", LogLevel.Info, true);
							OdiumConsole.Log("InputDialog", "Prefab reference: " + OdiumInputDialog._inputDialogPrefab.name, LogLevel.Info);
						}
					}
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "LoadInputDialog");
				OdiumInputDialog._isInitialized = false;
				OdiumInputDialog._inputDialogPrefab = null;
				bool flag7 = OdiumInputDialog._inputBundle != null;
				if (flag7)
				{
					OdiumInputDialog._inputBundle.Unload(true);
					OdiumInputDialog._inputBundle = null;
				}
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011FA4 File Offset: 0x000101A4
		public static void ShowInputDialog(string promptText, OdiumInputDialog.InputCallback callback, string defaultValue = "", string placeholder = "Enter text...")
		{
			OdiumConsole.Log("InputDialog", string.Format("ShowInputDialog called - Initialized: {0}, Prefab null: {1}", OdiumInputDialog._isInitialized, OdiumInputDialog._inputDialogPrefab == null), LogLevel.Info);
			bool flag = !OdiumInputDialog._isInitialized;
			if (flag)
			{
				OdiumConsole.Log("InputDialog", "Input dialog system not initialized!", LogLevel.Error);
				OdiumInputDialog.InputCallback callback2 = callback;
				if (callback2 != null)
				{
					callback2("", false);
				}
			}
			else
			{
				bool flag2 = OdiumInputDialog._inputDialogPrefab == null;
				if (flag2)
				{
					OdiumConsole.Log("InputDialog", "Input dialog prefab is null! Attempting to reinitialize...", LogLevel.Error);
					OdiumInputDialog._isInitialized = false;
					OdiumInputDialog.Initialize();
					bool flag3 = OdiumInputDialog._inputDialogPrefab == null;
					if (flag3)
					{
						OdiumConsole.Log("InputDialog", "Reinitialization failed - prefab still null!", LogLevel.Error);
						OdiumInputDialog.InputCallback callback3 = callback;
						if (callback3 != null)
						{
							callback3("", false);
						}
						return;
					}
				}
				try
				{
					Scene activeScene = SceneManager.GetActiveScene();
					OdiumConsole.Log("InputDialog", "Target scene: " + activeScene.name, LogLevel.Info);
					GameObject dialogInstance = Object.Instantiate<GameObject>(OdiumInputDialog._inputDialogPrefab);
					dialogInstance.name = string.Format("OdiumInputDialog_{0}", DateTime.Now.Ticks);
					SceneManager.MoveGameObjectToScene(dialogInstance, activeScene);
					OdiumConsole.Log("InputDialog", "Instantiated input dialog in scene: " + dialogInstance.scene.name, LogLevel.Info);
					Canvas component = dialogInstance.GetComponent<Canvas>();
					bool flag4 = component != null;
					if (flag4)
					{
						component.enabled = true;
						component.renderMode = 0;
						component.sortingOrder = 32770;
						component.overrideSorting = true;
						component.pixelPerfect = false;
						OdiumConsole.Log("InputDialog", string.Format("Configured Canvas - Enabled: {0}, SortOrder: {1}", component.enabled, component.sortingOrder), LogLevel.Info);
					}
					else
					{
						OdiumConsole.Log("InputDialog", "No Canvas component found on input dialog prefab!", LogLevel.Warning);
					}
					dialogInstance.SetActive(true);
					OdiumInputDialog._activeInputDialogs.Add(dialogInstance);
					RectTransform component2 = dialogInstance.GetComponent<RectTransform>();
					bool flag5 = component2 != null;
					if (flag5)
					{
						component2.localScale = Vector3.one;
						component2.localPosition = Vector3.zero;
						component2.localEulerAngles = Vector3.zero;
						component2.anchorMin = new Vector2(0.5f, 0.5f);
						component2.anchorMax = new Vector2(0.5f, 0.5f);
						component2.pivot = new Vector2(0.5f, 0.5f);
						component2.anchoredPosition = Vector2.zero;
						bool flag6 = component2.sizeDelta.x <= 0f || component2.sizeDelta.y <= 0f;
						if (flag6)
						{
							component2.sizeDelta = new Vector2(400f, 200f);
						}
						OdiumConsole.Log("InputDialog", string.Format("Positioned input dialog - AnchoredPos: {0}, Size: {1}", component2.anchoredPosition, component2.sizeDelta), LogLevel.Info);
					}
					TMP_InputField inputField = dialogInstance.GetComponentInChildren<TMP_InputField>();
					bool flag7 = inputField != null;
					if (flag7)
					{
						inputField.text = defaultValue;
						TextMeshProUGUI textMeshProUGUI = inputField.placeholder as TextMeshProUGUI;
						bool flag8 = textMeshProUGUI != null;
						if (flag8)
						{
							textMeshProUGUI.text = placeholder;
						}
						inputField.onSubmit.AddListener(delegate(string value)
						{
							OdiumInputDialog.CloseInputDialog(dialogInstance);
							OdiumInputDialog.InputCallback callback5 = callback;
							if (callback5 != null)
							{
								callback5(value, true);
							}
						});
						inputField.Select();
						inputField.ActivateInputField();
						OdiumConsole.Log("InputDialog", "Set up input field with listeners", LogLevel.Info);
					}
					else
					{
						OdiumConsole.Log("InputDialog", "Could not find TMP_InputField component!", LogLevel.Warning);
					}
					Button componentInChildren = dialogInstance.GetComponentInChildren<Button>();
					bool flag9 = componentInChildren != null;
					if (flag9)
					{
						componentInChildren.onClick.AddListener(delegate()
						{
							string input = (inputField != null) ? inputField.text : "";
							OdiumInputDialog.CloseInputDialog(dialogInstance);
							OdiumInputDialog.InputCallback callback5 = callback;
							if (callback5 != null)
							{
								callback5(input, true);
							}
						});
						OdiumConsole.Log("InputDialog", "Set up button with click listener", LogLevel.Info);
					}
					else
					{
						OdiumConsole.Log("InputDialog", "Could not find Button component!", LogLevel.Warning);
					}
					Transform[] array = dialogInstance.GetComponentsInChildren<Transform>(true);
					foreach (Transform transform in array)
					{
						transform.gameObject.SetActive(true);
					}
					Behaviour[] array3 = dialogInstance.GetComponentsInChildren<Behaviour>(true);
					foreach (Behaviour behaviour in array3)
					{
						behaviour.enabled = true;
					}
					Canvas.ForceUpdateCanvases();
					bool flag10 = component2 != null;
					if (flag10)
					{
						LayoutRebuilder.ForceRebuildLayoutImmediate(component2);
					}
					OdiumConsole.Log("InputDialog", "Final input dialog scene: " + dialogInstance.scene.name, LogLevel.Info);
					MelonCoroutines.Start(OdiumInputDialog.HandleInputDialogInput(dialogInstance, callback));
					OdiumConsole.LogGradient("InputDialog", "Input dialog shown with prompt: " + promptText, LogLevel.Info, false);
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, "ShowInputDialog");
					OdiumInputDialog.InputCallback callback4 = callback;
					if (callback4 != null)
					{
						callback4("", false);
					}
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0001257C File Offset: 0x0001077C
		private static IEnumerator HandleInputDialogInput(GameObject dialog, OdiumInputDialog.InputCallback callback)
		{
			OdiumInputDialog.<HandleInputDialogInput>d__9 <HandleInputDialogInput>d__ = new OdiumInputDialog.<HandleInputDialogInput>d__9(0);
			<HandleInputDialogInput>d__.dialog = dialog;
			<HandleInputDialogInput>d__.callback = callback;
			return <HandleInputDialogInput>d__;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00012594 File Offset: 0x00010794
		private static void CloseInputDialog(GameObject dialog)
		{
			try
			{
				bool flag = dialog != null;
				if (flag)
				{
					OdiumInputDialog._activeInputDialogs.Remove(dialog);
					Object.Destroy(dialog);
					OdiumConsole.Log("InputDialog", "Input dialog closed", LogLevel.Info);
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "CloseInputDialog");
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000125F8 File Offset: 0x000107F8
		public static bool IsInitialized()
		{
			bool flag = OdiumInputDialog._isInitialized && OdiumInputDialog._inputDialogPrefab != null;
			OdiumConsole.Log("InputDialog", string.Format("IsInitialized check - _isInitialized: {0}, _inputDialogPrefab != null: {1}, Result: {2}", OdiumInputDialog._isInitialized, OdiumInputDialog._inputDialogPrefab != null, flag), LogLevel.Info);
			return flag;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00012658 File Offset: 0x00010858
		public static void CloseAllInputDialogs()
		{
			try
			{
				foreach (GameObject gameObject in OdiumInputDialog._activeInputDialogs.ToArray())
				{
					bool flag = gameObject != null;
					if (flag)
					{
						Object.Destroy(gameObject);
					}
				}
				OdiumInputDialog._activeInputDialogs.Clear();
				OdiumConsole.Log("InputDialog", "All input dialogs closed", LogLevel.Info);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "CloseAllInputDialogs");
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000126E0 File Offset: 0x000108E0
		public static void Cleanup()
		{
			try
			{
				OdiumInputDialog.CloseAllInputDialogs();
				for (int i = 0; i < SceneManager.sceneCount; i++)
				{
					Scene sceneAt = SceneManager.GetSceneAt(i);
					bool flag = sceneAt.IsValid() && sceneAt.isLoaded;
					if (flag)
					{
						GameObject[] array = sceneAt.GetRootGameObjects();
						foreach (GameObject gameObject in array)
						{
							bool flag2 = gameObject.name.StartsWith("OdiumInputDialog");
							if (flag2)
							{
								Object.Destroy(gameObject);
								OdiumConsole.Log("InputDialog", "Destroyed input dialog in scene: " + sceneAt.name, LogLevel.Info);
							}
						}
					}
				}
				bool flag3 = OdiumInputDialog._inputBundle != null;
				if (flag3)
				{
					OdiumInputDialog._inputBundle.Unload(true);
					OdiumInputDialog._inputBundle = null;
				}
				OdiumInputDialog._inputDialogPrefab = null;
				OdiumInputDialog._isInitialized = false;
				OdiumConsole.Log("InputDialog", "Input dialog system cleaned up", LogLevel.Info);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "InputDialogCleanup");
			}
		}

		// Token: 0x040000F8 RID: 248
		private static AssetBundle _inputBundle;

		// Token: 0x040000F9 RID: 249
		private static GameObject _inputDialogPrefab;

		// Token: 0x040000FA RID: 250
		private static bool _isInitialized = false;

		// Token: 0x040000FB RID: 251
		private static List<GameObject> _activeInputDialogs = new List<GameObject>();

		// Token: 0x040000FC RID: 252
		private static readonly string InputBundlePath = Path.Combine(ModSetup.GetOdiumFolderPath(), "AssetBundles", "inputdialog");

		// Token: 0x020000F5 RID: 245
		// (Invoke) Token: 0x0600064B RID: 1611
		public delegate void InputCallback(string input, bool wasSubmitted);
	}
}

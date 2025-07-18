using System;
using System.Collections;
using System.IO;
using MelonLoader;
using Odium.Components;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;

namespace Odium.Modules
{
	// Token: 0x0200004C RID: 76
	public static class OdiumAssetBundleLoader
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00010920 File Offset: 0x0000EB20
		public static void Initialize()
		{
			OdiumConsole.Log("AssetLoader", "Starting loading screen asset loader...", LogLevel.Info);
			OdiumAssetBundleLoader.LoadAndInstantiateLoadingScreen();
			GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements").SetActive(false);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton").GetComponent<Image>().m_Color = Color.black;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().m_Color = Color.black;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().m_Color = Color.black;
			string path = Path.Combine(Environment.CurrentDirectory, "Odium", "ButtonBackground.png");
			Sprite sprite = SpriteUtil.LoadFromDisk(path, 100f);
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().m_Color = Color.black;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle").GetComponent<Image>().m_Color = Color.black;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle/Text").GetComponent<TextMeshProUGUIEx>().m_fontColor = Color.white;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton/Text").GetComponent<TextMeshProUGUIEx>().m_fontColor = OdiumAssetBundleLoader.FemboyPink;
			GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle/Text").GetComponent<TextMeshProUGUIEx>().m_fontColor = OdiumAssetBundleLoader.FemboyPink;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00010A47 File Offset: 0x0000EC47
		private static void LoadAndInstantiateLoadingScreen()
		{
			MelonCoroutines.Start(OdiumAssetBundleLoader.LoadAssetBundle());
			MelonCoroutines.Start(OdiumAssetBundleLoader.LoadPrefabFromBundle());
			OdiumAssetBundleLoader.ChangeLoadingScreen();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00010A66 File Offset: 0x0000EC66
		public static void ChangeLoadingScreen()
		{
			MelonCoroutines.Start(OdiumAssetBundleLoader.InstantiateLoadingScreen());
			MelonCoroutines.Start(OdiumAssetBundleLoader.ApplyToVRChatLoading());
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00010A7F File Offset: 0x0000EC7F
		private static IEnumerator LoadAssetBundle()
		{
			return new OdiumAssetBundleLoader.<LoadAssetBundle>d__11(0);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00010A87 File Offset: 0x0000EC87
		private static IEnumerator LoadPrefabFromBundle()
		{
			return new OdiumAssetBundleLoader.<LoadPrefabFromBundle>d__12(0);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00010A8F File Offset: 0x0000EC8F
		private static IEnumerator InstantiateLoadingScreen()
		{
			return new OdiumAssetBundleLoader.<InstantiateLoadingScreen>d__13(0);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00010A98 File Offset: 0x0000EC98
		private static void ApplyFemboyPinkToParticles()
		{
			bool flag = OdiumAssetBundleLoader._instantiatedLoadingScreen == null;
			if (!flag)
			{
				try
				{
					Il2CppArrayBase<ParticleSystem> componentsInChildren = OdiumAssetBundleLoader._instantiatedLoadingScreen.GetComponentsInChildren<ParticleSystem>(true);
					OdiumConsole.Log("AssetLoader", string.Format("Found {0} particle systems to colorize", componentsInChildren.Length), LogLevel.Info);
					OdiumConsole.LogGradient("AssetLoader", "All particle systems colorized with femboy pink!", LogLevel.Info, false);
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, "ApplyFemboyPinkToParticles");
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00010B1C File Offset: 0x0000ED1C
		private static IEnumerator InitLoadingScreenAudio()
		{
			return new OdiumAssetBundleLoader.<InitLoadingScreenAudio>d__15(0);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00010B24 File Offset: 0x0000ED24
		private static IEnumerator ApplyToVRChatLoading()
		{
			return new OdiumAssetBundleLoader.<ApplyToVRChatLoading>d__16(0);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00010B2C File Offset: 0x0000ED2C
		private static void HideOriginalLoadingElements()
		{
			try
			{
				string[] array = new string[]
				{
					"MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked",
					"MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles",
					"MenuContent/Popups/LoadingPopup/LoadingSound"
				};
				foreach (string name in array)
				{
					GameObject gameObject = GameObject.Find(name);
					bool flag = gameObject != null;
					if (flag)
					{
						gameObject.SetActive(false);
					}
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "HideOriginalLoadingElements");
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		private static void PositionCustomLoadingScreen()
		{
			try
			{
				bool flag = OdiumAssetBundleLoader._instantiatedLoadingScreen != null;
				if (flag)
				{
					OdiumAssetBundleLoader._instantiatedLoadingScreen.transform.localScale = new Vector3(400f, 400f, 400f);
					OdiumAssetBundleLoader._instantiatedLoadingScreen.transform.localPosition = Vector3.zero;
					OdiumAssetBundleLoader._instantiatedLoadingScreen.transform.localRotation = Quaternion.identity;
					OdiumConsole.Log("AssetLoader", "Custom loading screen positioned", LogLevel.Info);
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "PositionCustomLoadingScreen");
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public static IEnumerator ChangeLoadingScreenAudio()
		{
			return new OdiumAssetBundleLoader.<ChangeLoadingScreenAudio>d__19(0);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00010C64 File Offset: 0x0000EE64
		public static void ShowLoadingScreen()
		{
			bool flag = OdiumAssetBundleLoader._instantiatedLoadingScreen != null;
			if (flag)
			{
				OdiumAssetBundleLoader._instantiatedLoadingScreen.SetActive(true);
				OdiumConsole.Log("AssetLoader", "Custom loading screen shown", LogLevel.Info);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		public static void HideLoadingScreen()
		{
			bool flag = OdiumAssetBundleLoader._instantiatedLoadingScreen != null;
			if (flag)
			{
				OdiumAssetBundleLoader._instantiatedLoadingScreen.SetActive(false);
				OdiumConsole.Log("AssetLoader", "Custom loading screen hidden", LogLevel.Info);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00010CDC File Offset: 0x0000EEDC
		public static bool IsLoadingScreenLoaded()
		{
			return OdiumAssetBundleLoader._instantiatedLoadingScreen != null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00010CFC File Offset: 0x0000EEFC
		public static GameObject GetLoadingScreenInstance()
		{
			return OdiumAssetBundleLoader._instantiatedLoadingScreen;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00010D14 File Offset: 0x0000EF14
		public static void StopCustomAudio()
		{
			bool flag = OdiumAssetBundleLoader._customAudioSource != null;
			if (flag)
			{
				OdiumAssetBundleLoader._customAudioSource.Stop();
				OdiumConsole.Log("AssetLoader", "Custom audio stopped", LogLevel.Info);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00010D50 File Offset: 0x0000EF50
		public static void PlayCustomAudio()
		{
			bool flag = OdiumAssetBundleLoader._customAudioSource != null && OdiumAssetBundleLoader._customAudioClip != null;
			if (flag)
			{
				OdiumAssetBundleLoader._customAudioSource.Play();
				OdiumConsole.Log("AssetLoader", "Custom audio playing", LogLevel.Info);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00010D9C File Offset: 0x0000EF9C
		public static void Cleanup()
		{
			try
			{
				bool flag = OdiumAssetBundleLoader._instantiatedLoadingScreen != null;
				if (flag)
				{
					Object.Destroy(OdiumAssetBundleLoader._instantiatedLoadingScreen);
					OdiumAssetBundleLoader._instantiatedLoadingScreen = null;
					OdiumConsole.Log("AssetLoader", "Loading screen instance destroyed", LogLevel.Info);
				}
				bool flag2 = OdiumAssetBundleLoader._loadingScreenBundle != null;
				if (flag2)
				{
					OdiumAssetBundleLoader._loadingScreenBundle.Unload(true);
					OdiumAssetBundleLoader._loadingScreenBundle = null;
					OdiumConsole.Log("AssetLoader", "AssetBundle unloaded", LogLevel.Info);
				}
				bool flag3 = OdiumAssetBundleLoader._customAudioSource != null;
				if (flag3)
				{
					OdiumAssetBundleLoader._customAudioSource.Stop();
					Object.Destroy(OdiumAssetBundleLoader._customAudioSource.gameObject);
					OdiumAssetBundleLoader._customAudioSource = null;
					OdiumConsole.Log("AssetLoader", "Custom audio source destroyed", LogLevel.Info);
				}
				bool flag4 = OdiumAssetBundleLoader._customAudioClip != null;
				if (flag4)
				{
					Object.Destroy(OdiumAssetBundleLoader._customAudioClip);
					OdiumAssetBundleLoader._customAudioClip = null;
					OdiumConsole.Log("AssetLoader", "Custom audio clip destroyed", LogLevel.Info);
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "Cleanup");
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00010EB0 File Offset: 0x0000F0B0
		public static void RestoreOriginalLoadingScreen()
		{
			try
			{
				OdiumConsole.Log("AssetLoader", "Restoring original loading screen...", LogLevel.Info);
				OdiumAssetBundleLoader.HideLoadingScreen();
				bool flag = OdiumAssetBundleLoader._customAudioSource != null;
				if (flag)
				{
					OdiumAssetBundleLoader._customAudioSource.Stop();
					OdiumConsole.Log("AssetLoader", "Stopped custom audio", LogLevel.Info);
				}
				string[] array = new string[]
				{
					"MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked",
					"MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles"
				};
				foreach (string text in array)
				{
					GameObject gameObject = GameObject.Find(text);
					bool flag2 = gameObject != null;
					if (flag2)
					{
						gameObject.SetActive(true);
						OdiumConsole.Log("AssetLoader", "Restored original element: " + text, LogLevel.Info);
					}
				}
				OdiumConsole.LogGradient("AssetLoader", "Original loading screen restored!", LogLevel.Info, false);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "RestoreOriginalLoadingScreen");
			}
		}

		// Token: 0x040000EC RID: 236
		private static AssetBundle _loadingScreenBundle;

		// Token: 0x040000ED RID: 237
		private static GameObject _loadingScreenPrefab;

		// Token: 0x040000EE RID: 238
		private static GameObject _instantiatedLoadingScreen;

		// Token: 0x040000EF RID: 239
		public static AudioSource _customAudioSource;

		// Token: 0x040000F0 RID: 240
		public static AudioClip _customAudioClip;

		// Token: 0x040000F1 RID: 241
		private static readonly string LoadingScreenPath = Path.Combine(ModSetup.GetOdiumFolderPath(), "AssetBundles", "odium.loadingscreen");

		// Token: 0x040000F2 RID: 242
		private static readonly string LoadingMusicPath = Path.Combine(ModSetup.GetOdiumFolderPath(), "Audio", "loadingmusic.mp3");

		// Token: 0x040000F3 RID: 243
		private static readonly Color FemboyPink = new Color(0.792f, 0.008f, 0.988f, 1f);
	}
}

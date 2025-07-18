using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Text;
using CursorLayerMod;
using HarmonyLib;
using MelonLoader;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Odium.ApplicationBot;
using Odium.Components;
using Odium.Modules;
using Odium.Patches;
using Odium.QMPages;
using Odium.Threadding;
using Odium.UI;
using Odium.UX;
using Odium.Wrappers;
using OdiumLoader;
using UnityEngine;

namespace Odium
{
	// Token: 0x0200001D RID: 29
	public class OdiumEntry : MelonMod
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000060DC File Offset: 0x000042DC
		public static string Current_World_id
		{
			get
			{
				return RoomManager.prop_ApiWorldInstance_0.id;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000060F8 File Offset: 0x000042F8
		public override void OnInitializeMelon()
		{
			try
			{
				OdiumConsole.Initialize();
				OdiumConsole.LogGradient("Odium", "Starting authentication check...", LogLevel.Info, true);
				OdiumEntry.wasKeyValid = true;
				ModSetup.Initialize().GetAwaiter();
				BoneESP.SetEnabled(false);
				BoxESP.SetEnabled(false);
				PunchSystem.Initialize();
				CoroutineManager.Init();
				OdiumConsole.LogGradient("System", "Initialization complete!", LogLevel.Info, false);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006174 File Offset: 0x00004374
		private bool AuthenticateUser()
		{
			bool result;
			try
			{
				bool flag = File.Exists(OdiumEntry.AUTH_FILE);
				if (flag)
				{
					try
					{
						string text = File.ReadAllText(OdiumEntry.AUTH_FILE);
						OdiumEntry.AuthData authData = JsonConvert.DeserializeObject<OdiumEntry.AuthData>(text);
						bool flag2 = this.ValidateCredentials(authData.Email, authData.Key);
						if (flag2)
						{
							OdiumConsole.LogGradient("Auth", "Using saved credentials...", LogLevel.Info, false);
							OdiumEntry.wasKeyValid = true;
							return true;
						}
						File.Delete(OdiumEntry.AUTH_FILE);
						OdiumConsole.Log("Auth", "Saved credentials are invalid, requesting new ones...", LogLevel.Warning);
					}
					catch (Exception ex)
					{
						OdiumConsole.Log("Auth", "Error reading saved credentials: " + ex.Message, LogLevel.Warning);
						bool flag3 = File.Exists(OdiumEntry.AUTH_FILE);
						if (flag3)
						{
							File.Delete(OdiumEntry.AUTH_FILE);
						}
					}
				}
				result = this.ShowAuthenticationDialog();
			}
			catch (Exception ex2)
			{
				OdiumConsole.Log("Auth", "Authentication error: " + ex2.Message, LogLevel.Error);
				this.ShowErrorDialog("Authentication Error", "An error occurred during authentication:\n" + ex2.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000062AC File Offset: 0x000044AC
		private bool ShowAuthenticationDialog()
		{
			bool result;
			try
			{
				result = this.ShowFileBasedAuth();
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("Auth", "Dialog error: " + ex.Message, LogLevel.Error);
				result = false;
			}
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000062F8 File Offset: 0x000044F8
		private bool ShowFileBasedAuth()
		{
			bool result;
			try
			{
				string text = Path.Combine(Environment.CurrentDirectory, "Odium", "temp_auth.txt");
				Directory.CreateDirectory(Path.GetDirectoryName(text));
				bool flag = File.Exists(text);
				if (flag)
				{
					string[] array = File.ReadAllLines(text);
					bool flag2 = array.Length >= 2;
					if (flag2)
					{
						string text2 = array[0].Trim();
						string text3 = array[1].Trim();
						bool flag3 = !string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3);
						if (flag3)
						{
							bool flag4 = this.ValidateCredentials(text2, text3);
							if (flag4)
							{
								this.SaveCredentials(text2, text3);
								File.Delete(text);
								OdiumEntry.wasKeyValid = true;
								OdiumConsole.LogGradient("Auth", "Authentication successful via file!", LogLevel.Info, false);
								return true;
							}
							File.Delete(text);
							OdiumConsole.Log("Auth", "Invalid credentials in temp_auth.txt file.", LogLevel.Error);
						}
					}
					File.Delete(text);
				}
				string format = "ODIUM AUTHENTICATION REQUIRED\r\n=====================================\r\n\r\nTo authenticate Odium, please create a file named 'temp_auth.txt' in the Odium folder with your credentials:\r\n\r\nFile Location: {0}\r\n\r\nFile Contents (2 lines):\r\nLine 1: Your purchase email\r\nLine 2: Your invite key\r\n\r\nExample:\r\nuser@example.com\r\nyour-invite-key-here\r\n\r\nAfter creating the file, restart VRChat.\r\nThe file will be automatically deleted after successful authentication.\r\n\r\nVRChat will now close so you can set up authentication.";
				string text4 = Path.Combine(Environment.CurrentDirectory, "Odium", "auth_instructions.txt");
				File.WriteAllText(text4, string.Format(format, text));
				OdiumConsole.Log("Auth", "Authentication required. Instructions written to: " + text4, LogLevel.Info);
				OdiumConsole.Log("Auth", "Create temp_auth.txt at: " + text, LogLevel.Info);
				result = false;
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("Auth", "File auth error: " + ex.Message, LogLevel.Error);
				result = false;
			}
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000648C File Offset: 0x0000468C
		private bool ValidateCredentials(string email, string key)
		{
			bool result3;
			try
			{
				using (HttpClient httpClient = new HttpClient())
				{
					httpClient.Timeout = TimeSpan.FromSeconds(10.0);
					var <>f__AnonymousType = new
					{
						email,
						key
					};
					string content = JsonConvert.SerializeObject(<>f__AnonymousType);
					StringContent content2 = new StringContent(content, Encoding.UTF8, "application/json");
					HttpResponseMessage result = httpClient.PostAsync(OdiumEntry.AUTH_ENDPOINT, content2).Result;
					string result2 = result.Content.ReadAsStringAsync().Result;
					bool isSuccessStatusCode = result.IsSuccessStatusCode;
					if (isSuccessStatusCode)
					{
						OdiumEntry.ValidationResponse validationResponse = JsonConvert.DeserializeObject<OdiumEntry.ValidationResponse>(result2);
						result3 = (validationResponse != null && validationResponse.Success && validationResponse != null && validationResponse.Valid);
					}
					else
					{
						OdiumConsole.Log("Auth", string.Format("Validation request failed: {0} - {1}", result.StatusCode, result2), LogLevel.Error);
						result3 = false;
					}
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("Auth", "Validation error: " + ex.Message, LogLevel.Error);
				result3 = false;
			}
			return result3;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000065B0 File Offset: 0x000047B0
		private void SaveCredentials(string email, string key)
		{
			try
			{
				OdiumEntry.AuthData authData = new OdiumEntry.AuthData
				{
					Email = email,
					Key = key
				};
				string contents = JsonConvert.SerializeObject(authData);
				Directory.CreateDirectory(Path.GetDirectoryName(OdiumEntry.AUTH_FILE));
				File.WriteAllText(OdiumEntry.AUTH_FILE, contents);
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("Auth", "Failed to save credentials: " + ex.Message, LogLevel.Warning);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000662C File Offset: 0x0000482C
		private void ShowErrorDialog(string title, string message)
		{
			try
			{
				Interaction.MsgBox(message ?? "", MsgBoxStyle.Critical, "Odium - " + title);
			}
			catch
			{
				OdiumConsole.Log("Error", title + ": " + message, LogLevel.Error);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006688 File Offset: 0x00004888
		private void ShowSuccessDialog(string title, string message)
		{
			try
			{
				Interaction.MsgBox(message ?? "", MsgBoxStyle.Information, "Odium - " + title);
			}
			catch
			{
				OdiumConsole.Log("Success", title + ": " + message, LogLevel.Info);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000066E4 File Offset: 0x000048E4
		public override void OnApplicationStart()
		{
			OdiumEntry.HarmonyInstance = new Harmony("Odium.Harmony");
			MelonCoroutines.Start(QM.WaitForUI());
			MelonCoroutines.Start(OdiumEntry.OnNetworkManagerInit());
			QM.SetupMenu();
			PlayerRankTextDisplay.Initialize();
			PlayerRankTextDisplay.SetVisible(true);
			BoneESP.Initialize();
			BoneESP.SetEnabled(true);
			BoneESP.SetBoneColor(new Color(0.584f, 0.008f, 0.996f, 1f));
			BoxESP.Initialize();
			BoxESP.SetEnabled(true);
			BoxESP.SetBoxColor(new Color(0.584f, 0.008f, 0.996f, 1f));
			MainThreadDispatcher.Initialize();
			MelonCoroutines.Start(OdiumEntry.RamClearLoop());
			Patching.Initialize();
			ClonePatch.Patch();
			PhotonPatchesManual.ApplyPatches();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000067A5 File Offset: 0x000049A5
		public override void OnApplicationLateStart()
		{
			Bot.Start();
			OdiumModuleLoader.OnApplicationStart();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000067B4 File Offset: 0x000049B4
		internal static IEnumerator OnNetworkManagerInit()
		{
			return new OdiumEntry.<OnNetworkManagerInit>d__24(0);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000067BC File Offset: 0x000049BC
		public override void OnGUI()
		{
			BoneESP.OnGUI();
			BoxESP.OnGUI();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000067CC File Offset: 0x000049CC
		public override void OnLevelWasLoaded(int level)
		{
			bool flag = level == -1;
			if (flag)
			{
				OdiumAssetBundleLoader._customAudioClip = null;
				OdiumAssetBundleLoader._customAudioSource = null;
				OdiumAssetBundleLoader.StopCustomAudio();
			}
			OdiumAssetBundleLoader.Initialize();
			PlayerRankTextDisplay.ClearAll();
			OdiumConsole.LogGradient("OnLevelWasLoaded", string.Format("Level -> {0}", level), LogLevel.Info, false);
			OdiumEntry.loadIndex++;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000682B File Offset: 0x00004A2B
		public override void OnSceneWasLoaded(int buildindex, string sceneName)
		{
			OdiumModuleLoader.OnSceneWasLoaded(buildindex, sceneName);
			CursorLayerMod.OnSceneWasLoaded(buildindex, sceneName);
			OnLoadedSceneManager.LoadedScene(buildindex, sceneName);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006846 File Offset: 0x00004A46
		public override void OnSceneWasUnloaded(int buildindex, string sceneName)
		{
			OdiumModuleLoader.OnSceneWasUnloaded(buildindex, sceneName);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006851 File Offset: 0x00004A51
		public override void OnApplicationQuit()
		{
			OdiumModuleLoader.OnApplicationQuit();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000685A File Offset: 0x00004A5A
		private static IEnumerator RamClearLoop()
		{
			return new OdiumEntry.<RamClearLoop>d__30(0);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006864 File Offset: 0x00004A64
		public override void OnUpdate()
		{
			OdiumModuleLoader.OnUpdate();
			InternalConsole.ProcessLogCache();
			MainMenu.Setup();
			DroneSwarmWrapper.UpdateDroneSwarm();
			portalSpam.OnUpdate();
			portalTrap.OnUpdate();
			Bot.OnUpdate();
			BoneESP.Update();
			BoxESP.Update();
			SwasticaOrbit.OnUpdate();
			Jetpack.Update();
			FlyComponent.OnUpdate();
			CursorLayerMod.OnUpdate();
			Chatbox.UpdateFrameEffects();
			Exploits.UpdateChatboxLagger();
			bool flag = Time.time - this.lastStatsUpdate >= 1f;
			if (flag)
			{
				NameplateModifier.UpdatePlayerStats();
				this.lastStatsUpdate = Time.time;
			}
			bool keyDown = Input.GetKeyDown(KeyCode.Minus);
			if (keyDown)
			{
				DroneWrapper.DroneCrash();
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000690C File Offset: 0x00004B0C
		public override void OnFixedUpdate()
		{
			OdiumModuleLoader.OnFixedUpdate();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006915 File Offset: 0x00004B15
		public override void OnLateUpdate()
		{
			OdiumModuleLoader.OnLateUpdate();
			SpyCamera.LateUpdate();
		}

		// Token: 0x0400004F RID: 79
		public static string Version = "0.0.1";

		// Token: 0x04000050 RID: 80
		public static bool wasKeyValid = false;

		// Token: 0x04000051 RID: 81
		public static Harmony HarmonyInstance;

		// Token: 0x04000052 RID: 82
		private float lastStatsUpdate = 0f;

		// Token: 0x04000053 RID: 83
		private const float STATS_UPDATE_INTERVAL = 1f;

		// Token: 0x04000054 RID: 84
		public static int loadIndex = 0;

		// Token: 0x04000055 RID: 85
		public static Sprite buttonImage = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\ButtonBackground.png", 100f);

		// Token: 0x04000056 RID: 86
		private static readonly string AUTH_ENDPOINT = "https://odiumvrc.com/api/validate-purchase";

		// Token: 0x04000057 RID: 87
		private static readonly string AUTH_FILE = Path.Combine(Environment.CurrentDirectory, "Odium", "auth.dat");

		// Token: 0x04000058 RID: 88
		public static bool heartbeatRun = false;

		// Token: 0x020000AE RID: 174
		private class AuthData
		{
			// Token: 0x17000073 RID: 115
			// (get) Token: 0x0600048E RID: 1166 RVA: 0x00023357 File Offset: 0x00021557
			// (set) Token: 0x0600048F RID: 1167 RVA: 0x0002335F File Offset: 0x0002155F
			public string Email { get; set; }

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000490 RID: 1168 RVA: 0x00023368 File Offset: 0x00021568
			// (set) Token: 0x06000491 RID: 1169 RVA: 0x00023370 File Offset: 0x00021570
			public string Key { get; set; }
		}

		// Token: 0x020000AF RID: 175
		private class ValidationResponse
		{
			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000493 RID: 1171 RVA: 0x00023382 File Offset: 0x00021582
			// (set) Token: 0x06000494 RID: 1172 RVA: 0x0002338A File Offset: 0x0002158A
			public bool Success { get; set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000495 RID: 1173 RVA: 0x00023393 File Offset: 0x00021593
			// (set) Token: 0x06000496 RID: 1174 RVA: 0x0002339B File Offset: 0x0002159B
			public bool Valid { get; set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000497 RID: 1175 RVA: 0x000233A4 File Offset: 0x000215A4
			// (set) Token: 0x06000498 RID: 1176 RVA: 0x000233AC File Offset: 0x000215AC
			public string Message { get; set; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using MelonLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;

namespace Odium.UI
{
	// Token: 0x0200001E RID: 30
	public class PlayerRankTextDisplay
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000069A4 File Offset: 0x00004BA4
		public static void Initialize()
		{
			bool flag = PlayerRankTextDisplay.canvasObject != null;
			if (!flag)
			{
				PlayerRankTextDisplay.CreateStandaloneUI();
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000069CC File Offset: 0x00004BCC
		private static void CreateStandaloneUI()
		{
			try
			{
				PlayerRankTextDisplay.canvasObject = new GameObject("PlayerRankOverlayCanvas");
				Object.DontDestroyOnLoad(PlayerRankTextDisplay.canvasObject);
				PlayerRankTextDisplay.canvas = PlayerRankTextDisplay.canvasObject.AddComponent<Canvas>();
				PlayerRankTextDisplay.canvas.renderMode = 0;
				PlayerRankTextDisplay.canvas.sortingOrder = 999;
				CanvasScaler canvasScaler = PlayerRankTextDisplay.canvasObject.AddComponent<CanvasScaler>();
				canvasScaler.uiScaleMode = 1;
				canvasScaler.referenceResolution = new Vector2(1920f, 1080f);
				canvasScaler.screenMatchMode = 0;
				canvasScaler.matchWidthOrHeight = 0f;
				PlayerRankTextDisplay.canvasObject.AddComponent<GraphicRaycaster>();
				PlayerRankTextDisplay.textDisplayObject = new GameObject("PlayerRankText");
				PlayerRankTextDisplay.textDisplayObject.transform.SetParent(PlayerRankTextDisplay.canvasObject.transform, false);
				PlayerRankTextDisplay.textComponent = PlayerRankTextDisplay.textDisplayObject.AddComponent<TextMeshProUGUI>();
				PlayerRankTextDisplay.textComponent.text = "";
				PlayerRankTextDisplay.textComponent.fontSize = 18f;
				PlayerRankTextDisplay.textComponent.richText = true;
				PlayerRankTextDisplay.textComponent.enableAutoSizing = false;
				PlayerRankTextDisplay.textComponent.alignment = 257;
				PlayerRankTextDisplay.textComponent.verticalAlignment = 256;
				PlayerRankTextDisplay.textComponent.color = Color.white;
				PlayerRankTextDisplay.textComponent.fontStyle = 1;
				RectTransform component = PlayerRankTextDisplay.textDisplayObject.GetComponent<RectTransform>();
				component.anchorMin = new Vector2(0f, 1f);
				component.anchorMax = new Vector2(0f, 1f);
				component.pivot = new Vector2(0f, 1f);
				component.anchoredPosition = new Vector2(20f, -20f);
				component.sizeDelta = new Vector2(350f, 600f);
				Shadow shadow = PlayerRankTextDisplay.textDisplayObject.AddComponent<Shadow>();
				shadow.effectColor = new Color(0f, 0f, 0f, 0.8f);
				shadow.effectDistance = new Vector2(2f, -2f);
				Outline outline = PlayerRankTextDisplay.textDisplayObject.AddComponent<Outline>();
				outline.effectColor = Color.black;
				outline.effectDistance = new Vector2(1f, -1f);
				CanvasGroup canvasGroup = PlayerRankTextDisplay.textDisplayObject.AddComponent<CanvasGroup>();
				canvasGroup.blocksRaycasts = false;
				canvasGroup.interactable = false;
				MelonLogger.Msg("Standalone player rank overlay created successfully");
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Failed to create standalone UI: " + ex.Message);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006C64 File Offset: 0x00004E64
		public static void AddPlayer(string playerName, APIUser apiUser, Player plr)
		{
			bool flag = PlayerRankTextDisplay.canvasObject == null;
			if (flag)
			{
				PlayerRankTextDisplay.Initialize();
			}
			try
			{
				PlayerRankTextDisplay.Rank playerRank = PlayerRankTextDisplay.GetPlayerRank(apiUser);
				string text = ((apiUser != null) ? apiUser.id : null) ?? "";
				PlayerRankTextDisplay.PlayerInfo playerInfo = new PlayerRankTextDisplay.PlayerInfo(playerName, playerRank, plr, text);
				int num = PlayerRankTextDisplay.playerList.FindIndex((PlayerRankTextDisplay.PlayerInfo p) => p.playerName == playerName);
				bool flag2 = num >= 0;
				if (flag2)
				{
					PlayerRankTextDisplay.playerList[num] = playerInfo;
				}
				else
				{
					PlayerRankTextDisplay.playerList.Add(playerInfo);
				}
				bool flag3 = !string.IsNullOrEmpty(text);
				if (flag3)
				{
					PlayerRankTextDisplay.CheckClientUserAsync(text);
				}
				PlayerRankTextDisplay.UpdateDisplay();
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Failed to add player " + playerName + ": " + ex.Message);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006D64 File Offset: 0x00004F64
		[DebuggerStepThrough]
		private static void CheckClientUserAsync(string userId)
		{
			PlayerRankTextDisplay.<CheckClientUserAsync>d__14 <CheckClientUserAsync>d__ = new PlayerRankTextDisplay.<CheckClientUserAsync>d__14();
			<CheckClientUserAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckClientUserAsync>d__.userId = userId;
			<CheckClientUserAsync>d__.<>1__state = -1;
			<CheckClientUserAsync>d__.<>t__builder.Start<PlayerRankTextDisplay.<CheckClientUserAsync>d__14>(ref <CheckClientUserAsync>d__);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006DA0 File Offset: 0x00004FA0
		private static string GetAnimatedGradientText(string text, Color color1, Color color2, float speed = 3f, float waveLength = 1.5f)
		{
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				float num = Time.time * speed;
				for (int i = 0; i < text.Length; i++)
				{
					float num2 = (float)i / (float)(text.Length - 1);
					float num3 = num2 + (Mathf.Sin(num + (float)i * waveLength) * 0.5f + 0.5f) * 0.3f;
					num3 = Mathf.Clamp01(num3);
					Color color3 = Color.Lerp(color1, color2, num3);
					string arg = PlayerRankTextDisplay.ColorToHex(color3);
					stringBuilder.Append(string.Format("<color={0}>{1}</color>", arg, text[i]));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006E68 File Offset: 0x00005068
		public static void RemovePlayer(string playerName)
		{
			try
			{
				PlayerRankTextDisplay.PlayerInfo playerInfo = PlayerRankTextDisplay.playerList.Find((PlayerRankTextDisplay.PlayerInfo p) => p.playerName == playerName);
				bool flag = !string.IsNullOrEmpty(playerInfo.userId);
				if (flag)
				{
					PlayerRankTextDisplay.clientUsers.Remove(playerInfo.userId);
				}
				PlayerRankTextDisplay.playerList.RemoveAll((PlayerRankTextDisplay.PlayerInfo p) => p.playerName == playerName);
				PlayerRankTextDisplay.UpdateDisplay();
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Failed to remove player " + playerName + ": " + ex.Message);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006F18 File Offset: 0x00005118
		public static void ClearAll()
		{
			try
			{
				PlayerRankTextDisplay.playerList.Clear();
				PlayerRankTextDisplay.clientUsers.Clear();
				PlayerRankTextDisplay.UpdateDisplay();
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Failed to clear player list: " + ex.Message);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006F74 File Offset: 0x00005174
		public static string GetPlayerPlatform(Player player)
		{
			string result;
			try
			{
				APIUser field_Private_APIUser_ = player.field_Private_APIUser_0;
				bool flag = ((field_Private_APIUser_ != null) ? field_Private_APIUser_.last_platform : null) != null;
				if (flag)
				{
					result = field_Private_APIUser_.last_platform;
				}
				else
				{
					result = "Unknown";
				}
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006FCC File Offset: 0x000051CC
		public static string GetPlatformIcon(string platform)
		{
			string text = (platform != null) ? platform.ToLower() : null;
			string a = text;
			string result;
			if (!(a == "standalonewindows"))
			{
				if (!(a == "android"))
				{
					if (!(a == "ios"))
					{
						result = "<size=8>[<color=#FFFFFF>UNK</color>]</size>";
					}
					else
					{
						result = "<size=8>[<color=#FF69B4>iOS</color>]</size>";
					}
				}
				else
				{
					result = "<size=8>[<color=#32CD32>QUEST</color>]</size>";
				}
			}
			else
			{
				result = "<size=8>[<color=#00BFFF>PC</color>]</size>";
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007034 File Offset: 0x00005234
		public static bool IsFriend(Player player)
		{
			bool result;
			try
			{
				APIUser field_Private_APIUser_ = player.field_Private_APIUser_0;
				result = (field_Private_APIUser_ != null && field_Private_APIUser_.isFriend);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007070 File Offset: 0x00005270
		public static bool IsAdult(Player player)
		{
			bool result;
			try
			{
				APIUser field_Private_APIUser_ = player.field_Private_APIUser_0;
				result = ((field_Private_APIUser_ != null && field_Private_APIUser_.ageVerified) || (field_Private_APIUser_ != null && field_Private_APIUser_.isAdult));
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000070BC File Offset: 0x000052BC
		private static void UpdateDisplay()
		{
			bool flag = PlayerRankTextDisplay.textComponent == null;
			if (!flag)
			{
				try
				{
					string text = "";
					bool flag2 = PlayerRankTextDisplay.playerList.Count > 0;
					if (flag2)
					{
						foreach (PlayerRankTextDisplay.PlayerInfo playerInfo in PlayerRankTextDisplay.playerList)
						{
							string playerPlatform = PlayerRankTextDisplay.GetPlayerPlatform(playerInfo.player);
							string platformIcon = PlayerRankTextDisplay.GetPlatformIcon(playerPlatform);
							bool flag3 = PlayerRankTextDisplay.IsFriend(playerInfo.player);
							bool flag4 = PlayerRankTextDisplay.IsAdult(playerInfo.player);
							bool flag5 = PlayerRankTextDisplay.clientUsers.Contains(playerInfo.userId);
							string text2 = flag3 ? "<size=8><color=#FFD700>[FRIEND]</color></size>" : "";
							string text3 = flag4 ? "<size=8><color=#90EE90>[18+]</color></size>" : "";
							bool flag6 = flag5;
							if (flag6)
							{
								string animatedGradientText = PlayerRankTextDisplay.GetAnimatedGradientText(playerInfo.playerName, PlayerRankTextDisplay.gradientColor1, PlayerRankTextDisplay.gradientColor2, 3f, 1.5f);
								string text4 = "<size=8><color=#FF1493>[CLIENT]</color></size>";
								text = string.Concat(new string[]
								{
									text,
									"<size=12>",
									animatedGradientText,
									"</size> ",
									text4,
									" ",
									platformIcon,
									" ",
									text2,
									" ",
									text3,
									"\n"
								});
							}
							else
							{
								Color rankColor = PlayerRankTextDisplay.GetRankColor(playerInfo.rank);
								string text5 = PlayerRankTextDisplay.ColorToHex(rankColor);
								string rankDisplayName = PlayerRankTextDisplay.GetRankDisplayName(playerInfo.rank);
								text = string.Concat(new string[]
								{
									text,
									"<size=12><color=",
									text5,
									">",
									playerInfo.playerName,
									"</color></size> <size=8><color=#CCCCCC>[",
									rankDisplayName,
									"]</color></size> ",
									platformIcon,
									" ",
									text2,
									" ",
									text3,
									"\n"
								});
							}
						}
					}
					PlayerRankTextDisplay.textComponent.text = text;
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Failed to update display: " + ex.Message);
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007330 File Offset: 0x00005530
		public static string GetRankDisplayName(PlayerRankTextDisplay.Rank rank)
		{
			string result;
			switch (rank)
			{
			case PlayerRankTextDisplay.Rank.Visitor:
				result = "Visitor";
				break;
			case PlayerRankTextDisplay.Rank.NewUser:
				result = "New User";
				break;
			case PlayerRankTextDisplay.Rank.User:
				result = "User";
				break;
			case PlayerRankTextDisplay.Rank.Known:
				result = "Known User";
				break;
			case PlayerRankTextDisplay.Rank.Trusted:
				result = "Trusted User";
				break;
			default:
				result = "Unknown";
				break;
			}
			return result;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007390 File Offset: 0x00005590
		public static PlayerRankTextDisplay.Rank GetPlayerRank(APIUser apiUser)
		{
			bool flag = apiUser.hasLegendTrustLevel || apiUser.hasVeteranTrustLevel;
			PlayerRankTextDisplay.Rank result;
			if (flag)
			{
				result = PlayerRankTextDisplay.Rank.Trusted;
			}
			else
			{
				bool hasTrustedTrustLevel = apiUser.hasTrustedTrustLevel;
				if (hasTrustedTrustLevel)
				{
					result = PlayerRankTextDisplay.Rank.Known;
				}
				else
				{
					bool hasKnownTrustLevel = apiUser.hasKnownTrustLevel;
					if (hasKnownTrustLevel)
					{
						result = PlayerRankTextDisplay.Rank.User;
					}
					else
					{
						bool hasBasicTrustLevel = apiUser.hasBasicTrustLevel;
						if (hasBasicTrustLevel)
						{
							result = PlayerRankTextDisplay.Rank.NewUser;
						}
						else
						{
							result = PlayerRankTextDisplay.Rank.Visitor;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000073F0 File Offset: 0x000055F0
		public static Color GetRankColor(PlayerRankTextDisplay.Rank rank)
		{
			Color result;
			switch (rank)
			{
			case PlayerRankTextDisplay.Rank.Visitor:
				result = new Color(1f, 1f, 1f, 0.9f);
				break;
			case PlayerRankTextDisplay.Rank.NewUser:
				result = PlayerRankTextDisplay.ColorFromHex("#96ECFF", 0.9f);
				break;
			case PlayerRankTextDisplay.Rank.User:
				result = PlayerRankTextDisplay.ColorFromHex("#96FFA9", 0.9f);
				break;
			case PlayerRankTextDisplay.Rank.Known:
				result = PlayerRankTextDisplay.ColorFromHex("#FF5E50", 0.9f);
				break;
			case PlayerRankTextDisplay.Rank.Trusted:
				result = PlayerRankTextDisplay.ColorFromHex("#A900FE", 0.9f);
				break;
			default:
				result = new Color(1f, 1f, 1f, 0.9f);
				break;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000074A0 File Offset: 0x000056A0
		public static Color ColorFromHex(string hex, float alpha = 1f)
		{
			bool flag = hex.StartsWith("#");
			if (flag)
			{
				hex = hex.Substring(1);
			}
			bool flag2 = hex.Length == 6;
			Color result;
			if (flag2)
			{
				float r = (float)int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber) / 255f;
				float g = (float)int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber) / 255f;
				float b = (float)int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber) / 255f;
				result = new Color(r, g, b, alpha);
			}
			else
			{
				result = Color.white;
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000753C File Offset: 0x0000573C
		public static string ColorToHex(Color color)
		{
			int num = Mathf.RoundToInt(color.r * 255f);
			int num2 = Mathf.RoundToInt(color.g * 255f);
			int num3 = Mathf.RoundToInt(color.b * 255f);
			return string.Format("#{0:X2}{1:X2}{2:X2}", num, num2, num3);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000075A0 File Offset: 0x000057A0
		public static void SetPosition(float x, float y)
		{
			bool flag = PlayerRankTextDisplay.textDisplayObject != null;
			if (flag)
			{
				RectTransform component = PlayerRankTextDisplay.textDisplayObject.GetComponent<RectTransform>();
				component.anchoredPosition = new Vector2(x, y);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000075D8 File Offset: 0x000057D8
		public static void SetFontSize(float size)
		{
			bool flag = PlayerRankTextDisplay.textComponent != null;
			if (flag)
			{
				PlayerRankTextDisplay.textComponent.fontSize = size;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007604 File Offset: 0x00005804
		public static void SetVisible(bool visible)
		{
			bool flag = PlayerRankTextDisplay.canvasObject != null;
			if (flag)
			{
				PlayerRankTextDisplay.canvasObject.SetActive(visible);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007630 File Offset: 0x00005830
		public static void SetOpacity(float opacity)
		{
			bool flag = PlayerRankTextDisplay.textComponent != null;
			if (flag)
			{
				Color color = PlayerRankTextDisplay.textComponent.color;
				color.a = opacity;
				PlayerRankTextDisplay.textComponent.color = color;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007670 File Offset: 0x00005870
		public static void Destroy()
		{
			bool flag = PlayerRankTextDisplay.canvasObject != null;
			if (flag)
			{
				Object.DestroyImmediate(PlayerRankTextDisplay.canvasObject);
				PlayerRankTextDisplay.canvasObject = null;
				PlayerRankTextDisplay.canvas = null;
				PlayerRankTextDisplay.textDisplayObject = null;
				PlayerRankTextDisplay.textComponent = null;
				PlayerRankTextDisplay.playerList.Clear();
				PlayerRankTextDisplay.clientUsers.Clear();
				HttpClient httpClient = PlayerRankTextDisplay.httpClient;
				if (httpClient != null)
				{
					httpClient.Dispose();
				}
				MelonLogger.Msg("Player rank overlay destroyed");
			}
		}

		// Token: 0x04000059 RID: 89
		private static GameObject canvasObject;

		// Token: 0x0400005A RID: 90
		private static Canvas canvas;

		// Token: 0x0400005B RID: 91
		private static GameObject textDisplayObject;

		// Token: 0x0400005C RID: 92
		private static TextMeshProUGUI textComponent;

		// Token: 0x0400005D RID: 93
		private static List<PlayerRankTextDisplay.PlayerInfo> playerList = new List<PlayerRankTextDisplay.PlayerInfo>();

		// Token: 0x0400005E RID: 94
		private static HashSet<string> clientUsers = new HashSet<string>();

		// Token: 0x0400005F RID: 95
		private static Color gradientColor1 = PlayerRankTextDisplay.ColorFromHex("#D37CFE", 1f);

		// Token: 0x04000060 RID: 96
		private static Color gradientColor2 = PlayerRankTextDisplay.ColorFromHex("#8900CE", 1f);

		// Token: 0x04000061 RID: 97
		private static HttpClient httpClient = new HttpClient();

		// Token: 0x020000B6 RID: 182
		public struct PlayerInfo
		{
			// Token: 0x060004B3 RID: 1203 RVA: 0x00024201 File Offset: 0x00022401
			public PlayerInfo(string name, PlayerRankTextDisplay.Rank playerRank, Player plr, string id)
			{
				this.playerName = name;
				this.rank = playerRank;
				this.player = plr;
				this.userId = id;
			}

			// Token: 0x040002AB RID: 683
			public string playerName;

			// Token: 0x040002AC RID: 684
			public PlayerRankTextDisplay.Rank rank;

			// Token: 0x040002AD RID: 685
			public Player player;

			// Token: 0x040002AE RID: 686
			public string userId;
		}

		// Token: 0x020000B7 RID: 183
		public enum Rank
		{
			// Token: 0x040002B0 RID: 688
			Visitor,
			// Token: 0x040002B1 RID: 689
			NewUser,
			// Token: 0x040002B2 RID: 690
			User,
			// Token: 0x040002B3 RID: 691
			Known,
			// Token: 0x040002B4 RID: 692
			Trusted
		}
	}
}

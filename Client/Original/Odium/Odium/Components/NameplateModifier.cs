using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MelonLoader;
using Odium.Odium;
using Odium.Patches;
using Odium.Wrappers;
using TMPro;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace Odium.Components
{
	// Token: 0x02000064 RID: 100
	public static class NameplateModifier
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00016848 File Offset: 0x00014A48
		public static void ModifyPlayerNameplate(Player player, Sprite newDevCircleSprite = null)
		{
			try
			{
				APIUser apiuser = player.prop_APIUser_0;
				GameObject field_Public_GameObject_ = player._vrcplayer.field_Public_GameObject_0;
				Transform transform = field_Public_GameObject_.transform.FindChild("PlayerNameplate/Canvas");
				bool flag = transform == null;
				if (flag)
				{
					MelonLogger.Warning("Could not find PlayerNameplate/Canvas");
				}
				else
				{
					NameplateModifier.CleanupPlayerStats(apiuser.id);
					Rank playerRank = NameplateModifier.GetPlayerRank(apiuser);
					NameplateModifier.DestroyIconIfEnabled(transform);
					NameplateModifier.DisableBackground(transform);
					bool flag2 = newDevCircleSprite != null;
					if (flag2)
					{
						NameplateModifier.ChangeDevCircleSprite(transform, newDevCircleSprite, playerRank);
					}
					else
					{
						NameplateModifier.ApplyRankColoring(transform, playerRank);
					}
					MelonCoroutines.Start(NameplateModifier.AddStatsToNameplateCoroutine(player, transform));
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error in ModifyPlayerNameplate: " + ex.Message);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00016918 File Offset: 0x00014B18
		private static IEnumerator AddStatsToNameplateCoroutine(Player player, Transform playerNameplateCanvas)
		{
			NameplateModifier.<AddStatsToNameplateCoroutine>d__10 <AddStatsToNameplateCoroutine>d__ = new NameplateModifier.<AddStatsToNameplateCoroutine>d__10(0);
			<AddStatsToNameplateCoroutine>d__.player = player;
			<AddStatsToNameplateCoroutine>d__.playerNameplateCanvas = playerNameplateCanvas;
			return <AddStatsToNameplateCoroutine>d__;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001692E File Offset: 0x00014B2E
		private static IEnumerator CheckClientUserCoroutine(string userId)
		{
			NameplateModifier.<CheckClientUserCoroutine>d__11 <CheckClientUserCoroutine>d__ = new NameplateModifier.<CheckClientUserCoroutine>d__11(0);
			<CheckClientUserCoroutine>d__.userId = userId;
			return <CheckClientUserCoroutine>d__;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00016940 File Offset: 0x00014B40
		[DebuggerStepThrough]
		private static Task<bool> CheckClientUserAsync(string userId)
		{
			NameplateModifier.<CheckClientUserAsync>d__12 <CheckClientUserAsync>d__ = new NameplateModifier.<CheckClientUserAsync>d__12();
			<CheckClientUserAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CheckClientUserAsync>d__.userId = userId;
			<CheckClientUserAsync>d__.<>1__state = -1;
			<CheckClientUserAsync>d__.<>t__builder.Start<NameplateModifier.<CheckClientUserAsync>d__12>(ref <CheckClientUserAsync>d__);
			return <CheckClientUserAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00016984 File Offset: 0x00014B84
		private static IEnumerator FetchAndApplyTagsCoroutine(string userId, Transform quickStats, Transform nameplateGroup)
		{
			NameplateModifier.<FetchAndApplyTagsCoroutine>d__13 <FetchAndApplyTagsCoroutine>d__ = new NameplateModifier.<FetchAndApplyTagsCoroutine>d__13(0);
			<FetchAndApplyTagsCoroutine>d__.userId = userId;
			<FetchAndApplyTagsCoroutine>d__.quickStats = quickStats;
			<FetchAndApplyTagsCoroutine>d__.nameplateGroup = nameplateGroup;
			return <FetchAndApplyTagsCoroutine>d__;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000169A4 File Offset: 0x00014BA4
		private static void ApplyTagsToNameplate(string userId, List<string> userTags, Transform quickStats, Transform nameplateGroup)
		{
			try
			{
				int num = NameplateModifier.playerStats.FindIndex((NameplateData s) => s.userId == userId);
				bool flag = num == -1;
				if (!flag)
				{
					NameplateData nameplateData = NameplateModifier.playerStats[num];
					bool flag2 = userId == PlayerWrapper.LocalPlayer.field_Private_APIUser_0.id;
					if (flag2)
					{
						AssignedVariables.playerTagsCount = nameplateData.tagPlates.Count;
					}
					bool flag3 = nameplateData.tagPlates.Count > 1;
					if (flag3)
					{
						for (int i = nameplateData.tagPlates.Count - 1; i >= 1; i--)
						{
							try
							{
								bool flag4 = nameplateData.tagPlates[i] != null && nameplateData.tagPlates[i].gameObject != null;
								if (flag4)
								{
									Object.Destroy(nameplateData.tagPlates[i].gameObject);
								}
							}
							catch (Exception ex)
							{
								MelonLogger.Warning("Error destroying old tag plate: " + ex.Message);
							}
						}
						nameplateData.tagPlates.RemoveRange(1, nameplateData.tagPlates.Count - 1);
						nameplateData.statsComponents.RemoveRange(1, nameplateData.statsComponents.Count - 1);
					}
					bool flag5 = PhotonPatches.HasPlayerCrashed(userId);
					int num2 = 1;
					bool flag6 = flag5;
					if (flag6)
					{
						Transform transform = NameplateModifier.CreateStatsPlate(quickStats, nameplateGroup, "Crash Tag", num2);
						bool flag7 = transform != null;
						if (flag7)
						{
							TextMeshProUGUI textMeshProUGUI = NameplateModifier.SetupStatsComponent(transform);
							bool flag8 = textMeshProUGUI != null;
							if (flag8)
							{
								textMeshProUGUI.text = "<color=#e91f42>CRASHED</color>";
								nameplateData.statsComponents.Add(textMeshProUGUI);
								nameplateData.tagPlates.Add(transform);
							}
						}
						num2++;
					}
					for (int j = 0; j < userTags.Count; j++)
					{
						Transform transform2 = NameplateModifier.CreateStatsPlate(quickStats, nameplateGroup, string.Format("Tag Stats {0}", j), num2 + j);
						bool flag9 = transform2 != null;
						if (flag9)
						{
							TextMeshProUGUI textMeshProUGUI2 = NameplateModifier.SetupStatsComponent(transform2);
							bool flag10 = textMeshProUGUI2 != null;
							if (flag10)
							{
								textMeshProUGUI2.text = "<color=#e91e63>" + userTags[j] + "</color>";
								nameplateData.statsComponents.Add(textMeshProUGUI2);
								nameplateData.tagPlates.Add(transform2);
							}
						}
					}
					nameplateData.userTags = userTags;
					NameplateModifier.playerStats[num] = nameplateData;
					bool flag11 = NameplateModifier.clientUsers.Contains(userId) || nameplateData.isClientUser;
					int num3 = userTags.Count + (flag5 ? 1 : 0);
					MelonLogger.Msg(string.Format("Applied {0} tags for player: {1} (Client: {2}, Crashed: {3})", new object[]
					{
						num3,
						userId,
						flag11,
						flag5
					}));
				}
			}
			catch (Exception ex2)
			{
				MelonLogger.Error("Error applying tags to nameplate: " + ex2.Message);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00016D04 File Offset: 0x00014F04
		public static string ColorToHex(Color color)
		{
			int num = Mathf.RoundToInt(color.r * 255f);
			int num2 = Mathf.RoundToInt(color.g * 255f);
			int num3 = Mathf.RoundToInt(color.b * 255f);
			return string.Format("#{0:X2}{1:X2}{2:X2}", num, num2, num3);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00016D68 File Offset: 0x00014F68
		public static void CheckAndRefreshTags()
		{
			bool flag = !NameplateModifier.autoRefreshEnabled;
			if (!flag)
			{
				bool flag2 = Time.time - NameplateModifier.lastRefreshTime >= NameplateModifier.REFRESH_INTERVAL;
				if (flag2)
				{
					NameplateModifier.lastRefreshTime = Time.time;
					MelonCoroutines.Start(NameplateModifier.RefreshAllTagsCoroutine());
				}
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00016DB4 File Offset: 0x00014FB4
		private static IEnumerator RefreshAllTagsCoroutine()
		{
			return new NameplateModifier.<RefreshAllTagsCoroutine>d__17(0);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00016DBC File Offset: 0x00014FBC
		private static Player GetPlayerById(string userId)
		{
			Player result;
			try
			{
				PlayerManager playerManager = PlayerManager.prop_PlayerManager_0;
				bool flag = ((playerManager != null) ? playerManager.field_Private_List_1_Player_0 : null) == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Il2CppArrayBase<Player> source = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
					result = source.FirstOrDefault(delegate(Player p)
					{
						string a;
						if (p == null)
						{
							a = null;
						}
						else
						{
							APIUser field_Private_APIUser_ = p.field_Private_APIUser_0;
							a = ((field_Private_APIUser_ != null) ? field_Private_APIUser_.id : null);
						}
						return a == userId;
					});
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error getting player by ID " + userId + ": " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00016E54 File Offset: 0x00015054
		public static void EnableAutoRefresh()
		{
			NameplateModifier.autoRefreshEnabled = true;
			MelonLogger.Msg("Auto-refresh enabled");
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00016E68 File Offset: 0x00015068
		public static void DisableAutoRefresh()
		{
			NameplateModifier.autoRefreshEnabled = false;
			MelonLogger.Msg("Auto-refresh disabled");
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00016E7C File Offset: 0x0001507C
		public static void SetRefreshInterval(float seconds)
		{
			bool flag = seconds > 0f;
			if (flag)
			{
				FieldInfo field = typeof(NameplateModifier).GetField("REFRESH_INTERVAL", BindingFlags.Static | BindingFlags.NonPublic);
				if (field != null)
				{
					field.SetValue(null, seconds);
				}
				MelonLogger.Msg(string.Format("Refresh interval set to {0} seconds", seconds));
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00016ED8 File Offset: 0x000150D8
		public static void ManualRefreshAllTags()
		{
			MelonCoroutines.Start(NameplateModifier.RefreshAllTagsCoroutine());
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00016EE6 File Offset: 0x000150E6
		public static void CheckClientStatus(string userId)
		{
			MelonCoroutines.Start(NameplateModifier.CheckClientUserCoroutine(userId));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00016EF5 File Offset: 0x000150F5
		public static void ClearClientCache()
		{
			NameplateModifier.clientUsers.Clear();
			MelonLogger.Msg("Cleared client user cache");
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00016F10 File Offset: 0x00015110
		private static Transform CreateStatsPlate(Transform quickStats, Transform nameplateGroup, string plateName, int stackIndex)
		{
			Transform result;
			try
			{
				Transform transform = Object.Instantiate<Transform>(quickStats, nameplateGroup.FindChild("Contents"));
				bool flag = transform == null;
				if (flag)
				{
					MelonLogger.Warning("Failed to instantiate " + plateName + " transform");
					result = null;
				}
				else
				{
					transform.name = plateName;
					transform.gameObject.SetActive(true);
					float y = 180f + (float)stackIndex * 30f;
					transform.localPosition = new Vector3(0f, y, 0f);
					Transform transform2 = transform.FindChild("Trust Icon");
					bool flag2 = transform2 != null;
					if (flag2)
					{
						transform2.gameObject.SetActive(false);
					}
					Transform transform3 = transform.FindChild("Performance Icon");
					bool flag3 = transform3 != null;
					if (flag3)
					{
						transform3.gameObject.SetActive(false);
					}
					Transform transform4 = transform.FindChild("Performance Text");
					bool flag4 = transform4 != null;
					if (flag4)
					{
						transform4.gameObject.SetActive(false);
					}
					Transform transform5 = transform.FindChild("Friend Anchor Stats");
					bool flag5 = transform5 != null;
					if (flag5)
					{
						transform5.gameObject.SetActive(false);
					}
					ImageThreeSlice component = transform.GetComponent<ImageThreeSlice>();
					bool flag6 = component != null;
					if (flag6)
					{
						bool flag7 = stackIndex == 0;
						if (flag7)
						{
							component.color = new Color(0f, 0f, 0f, 0.6f);
						}
						else
						{
							component.color = new Color(0.9f, 0.1f, 0.4f, 0.3f);
						}
					}
					result = transform;
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error creating stats plate " + plateName + ": " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000170E8 File Offset: 0x000152E8
		private static TextMeshProUGUI SetupStatsComponent(Transform statsTransform)
		{
			TextMeshProUGUI result;
			try
			{
				Transform transform = statsTransform.FindChild("Trust Text");
				bool flag = transform == null;
				if (flag)
				{
					MelonLogger.Warning("Could not find Trust Text component");
					result = null;
				}
				else
				{
					TextMeshProUGUI component = transform.GetComponent<TextMeshProUGUI>();
					bool flag2 = component == null;
					if (flag2)
					{
						MelonLogger.Warning("Could not get TextMeshProUGUI component");
						result = null;
					}
					else
					{
						component.color = Color.white;
						component.fontSize = 12f;
						component.fontStyle = 1;
						result = component;
					}
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error setting up stats component: " + ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00017198 File Offset: 0x00015398
		[DebuggerStepThrough]
		private static Task<List<string>> GetUserTags(string userId)
		{
			NameplateModifier.<GetUserTags>d__27 <GetUserTags>d__ = new NameplateModifier.<GetUserTags>d__27();
			<GetUserTags>d__.<>t__builder = AsyncTaskMethodBuilder<List<string>>.Create();
			<GetUserTags>d__.userId = userId;
			<GetUserTags>d__.<>1__state = -1;
			<GetUserTags>d__.<>t__builder.Start<NameplateModifier.<GetUserTags>d__27>(ref <GetUserTags>d__);
			return <GetUserTags>d__.<>t__builder.Task;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000171DC File Offset: 0x000153DC
		public static void UpdatePlayerStats()
		{
			try
			{
				NameplateModifier.CheckAndRefreshTags();
				PlayerManager playerManager = PlayerManager.prop_PlayerManager_0;
				bool flag = ((playerManager != null) ? playerManager.field_Private_List_1_Player_0 : null) == null;
				if (!flag)
				{
					Il2CppArrayBase<Player> il2CppArrayBase = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
					using (IEnumerator<Player> enumerator = il2CppArrayBase.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Player player = enumerator.Current;
							Player player2 = player;
							object obj;
							if (player2 == null)
							{
								obj = null;
							}
							else
							{
								APIUser field_Private_APIUser_ = player2.field_Private_APIUser_0;
								obj = ((field_Private_APIUser_ != null) ? field_Private_APIUser_.id : null);
							}
							bool flag2 = obj == null;
							if (!flag2)
							{
								int num = NameplateModifier.playerStats.FindIndex((NameplateData s) => s.userId == player.field_Private_APIUser_0.id);
								bool flag3 = num == -1;
								if (!flag3)
								{
									NameplateData nameplateData = NameplateModifier.playerStats[num];
									bool flag4 = nameplateData.statsComponents == null || nameplateData.statsComponents.Count == 0;
									if (!flag4)
									{
										bool flag5 = !NameplateModifier.ValidateStatsComponents(nameplateData);
										if (flag5)
										{
											MelonLogger.Warning("Invalid stats components detected for player " + player.field_Private_APIUser_0.displayName + ", cleaning up...");
											NameplateModifier.CleanupPlayerStats(player.field_Private_APIUser_0.id);
										}
										else
										{
											bool flag6 = PhotonPatches.HasPlayerCrashed(player.field_Private_APIUser_0.id);
											bool flag7 = NameplateModifier.clientUsers.Contains(player.field_Private_APIUser_0.id) || nameplateData.isClientUser;
											bool flag8 = false;
											for (int i = 1; i < nameplateData.statsComponents.Count; i++)
											{
												bool flag9 = nameplateData.statsComponents[i] != null;
												if (flag9)
												{
													bool flag10 = nameplateData.statsComponents[i].text.Contains("CRASHED");
													if (flag10)
													{
														flag8 = true;
													}
												}
											}
											bool flag11 = flag6 != flag8;
											if (flag11)
											{
												GameObject field_Public_GameObject_ = player._vrcplayer.field_Public_GameObject_0;
												Transform transform = field_Public_GameObject_.transform.FindChild("PlayerNameplate/Canvas");
												bool flag12 = transform != null;
												if (flag12)
												{
													Transform transform2 = transform.FindChild("NameplateGroup/Nameplate");
													bool flag13 = transform2 != null;
													if (flag13)
													{
														Transform transform3 = transform2.FindChild("Contents/Quick Stats");
														bool flag14 = transform3 != null;
														if (flag14)
														{
															List<string> userTags = NameplateModifier.tagCache.ContainsKey(player.field_Private_APIUser_0.id) ? NameplateModifier.tagCache[player.field_Private_APIUser_0.id] : (nameplateData.userTags ?? new List<string>());
															NameplateModifier.ApplyTagsToNameplate(player.field_Private_APIUser_0.id, userTags, transform3, transform2);
														}
													}
												}
											}
											NameplateModifier.UpdateSinglePlayerStats(player, nameplateData);
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error in UpdatePlayerStats: " + ex.Message);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001752C File Offset: 0x0001572C
		private static bool ValidateStatsComponents(NameplateData statsData)
		{
			try
			{
				bool flag = statsData.statsComponents.Count > 0 && statsData.statsComponents[0] != null;
				if (flag)
				{
					string text = statsData.statsComponents[0].text;
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
			return false;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00017598 File Offset: 0x00015798
		private static void UpdateSinglePlayerStats(Player player, NameplateData statsData)
		{
			try
			{
				statsData.lastSeen = Time.time;
				statsData.lastPosition = player.transform.position;
				bool flag = statsData.statsComponents.Count > 0 && statsData.statsComponents[0] != null;
				if (flag)
				{
					List<string> list = new List<string>();
					string platformIcon = NameplateModifier.GetPlatformIcon(statsData.platform);
					bool flag2 = !string.IsNullOrEmpty(platformIcon);
					if (flag2)
					{
						list.Add(platformIcon);
					}
					bool flag3 = NameplateModifier.clientUsers.Contains(statsData.userId) || statsData.isClientUser;
					bool flag4 = flag3;
					if (flag4)
					{
						list.Add("[<color=#e91f42>C</color>]");
					}
					VRCPlayerApi field_Private_VRCPlayerApi_ = player.field_Private_VRCPlayerApi_0;
					bool flag5 = field_Private_VRCPlayerApi_ != null && field_Private_VRCPlayerApi_.isMaster;
					if (flag5)
					{
						list.Add("[<color=#FFD700>M</color>]");
					}
					bool flag6 = NameplateModifier.IsFriend(player);
					if (flag6)
					{
						list.Add("[<color=#FF69B4>F</color>]");
					}
					bool flag7 = NameplateModifier.IsAdult(player);
					if (flag7)
					{
						list.Add("[<color=#9966FF>18+</color>]");
					}
					string avatarReleaseStatus = NameplateModifier.GetAvatarReleaseStatus(player);
					list.Add("[<color=#9966FF>" + avatarReleaseStatus + "</color>]");
					string text = string.Join(" | ", list);
					statsData.statsComponents[0].text = text;
				}
				int num = NameplateModifier.playerStats.FindIndex((NameplateData s) => s.userId == statsData.userId);
				bool flag8 = num != -1;
				if (flag8)
				{
					NameplateModifier.playerStats[num] = statsData;
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error updating stats for player " + player.field_Private_APIUser_0.displayName + ": " + ex.Message);
				NameplateModifier.CleanupPlayerStats(player.field_Private_APIUser_0.id);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000177B8 File Offset: 0x000159B8
		public static void CleanupPlayerStats(string userId)
		{
			try
			{
				int num = NameplateModifier.playerStats.FindIndex((NameplateData s) => s.userId == userId);
				bool flag = num != -1;
				if (flag)
				{
					NameplateData nameplateData = NameplateModifier.playerStats[num];
					bool flag2 = nameplateData.tagPlates != null;
					if (flag2)
					{
						foreach (Transform transform in nameplateData.tagPlates)
						{
							try
							{
								bool flag3 = transform != null && transform.gameObject != null;
								if (flag3)
								{
									Object.Destroy(transform.gameObject);
								}
							}
							catch (Exception ex)
							{
								MelonLogger.Warning("Error destroying tag plate: " + ex.Message);
							}
						}
					}
					NameplateModifier.playerStats.RemoveAt(num);
					NameplateModifier.clientUsers.Remove(userId);
				}
			}
			catch (Exception ex2)
			{
				MelonLogger.Error("Error cleaning up stats for user " + userId + ": " + ex2.Message);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00017910 File Offset: 0x00015B10
		public static void ClearTagCache(string userId = null)
		{
			try
			{
				bool flag = string.IsNullOrEmpty(userId);
				if (flag)
				{
					NameplateModifier.tagCache.Clear();
					MelonLogger.Msg("Cleared all tag cache");
				}
				else
				{
					bool flag2 = NameplateModifier.tagCache.ContainsKey(userId);
					if (flag2)
					{
						NameplateModifier.tagCache.Remove(userId);
						MelonLogger.Msg("Cleared tag cache for user: " + userId);
					}
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error clearing tag cache: " + ex.Message);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000179A0 File Offset: 0x00015BA0
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

		// Token: 0x060002AD RID: 685 RVA: 0x000179F8 File Offset: 0x00015BF8
		private static string GetPlatformIcon(string platform)
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
						result = "[<color=#FFFFFF>UNK</color>]";
					}
					else
					{
						result = "[<color=#FF69B4>iOS</color>]";
					}
				}
				else
				{
					result = "[<color=#32CD32>Q</color>]";
				}
			}
			else
			{
				result = "[<color=#00BFFF>PC</color>]";
			}
			return result;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00017A60 File Offset: 0x00015C60
		private static bool IsFriend(Player player)
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

		// Token: 0x060002AF RID: 687 RVA: 0x00017A9C File Offset: 0x00015C9C
		private static bool IsAdult(Player player)
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

		// Token: 0x060002B0 RID: 688 RVA: 0x00017AE8 File Offset: 0x00015CE8
		private static string GetAvatarReleaseStatus(Player player)
		{
			string result;
			try
			{
				ApiAvatar apiAvatar = player.prop_ApiAvatar_0;
				result = ((apiAvatar != null) ? apiAvatar.releaseStatus : null);
			}
			catch
			{
				result = "ERR";
			}
			return result;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00017B28 File Offset: 0x00015D28
		public static Rank GetPlayerRank(APIUser apiUser)
		{
			bool flag = apiUser.hasLegendTrustLevel || apiUser.hasVeteranTrustLevel;
			Rank result;
			if (flag)
			{
				result = Rank.Trusted;
			}
			else
			{
				bool hasTrustedTrustLevel = apiUser.hasTrustedTrustLevel;
				if (hasTrustedTrustLevel)
				{
					result = Rank.Known;
				}
				else
				{
					bool hasKnownTrustLevel = apiUser.hasKnownTrustLevel;
					if (hasKnownTrustLevel)
					{
						result = Rank.User;
					}
					else
					{
						bool hasBasicTrustLevel = apiUser.hasBasicTrustLevel;
						if (hasBasicTrustLevel)
						{
							result = Rank.NewUser;
						}
						else
						{
							result = Rank.Visitor;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00017B85 File Offset: 0x00015D85
		private static void DestroyIconIfEnabled(Transform playerNameplateCanvas)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00017B88 File Offset: 0x00015D88
		private static void DisableBackground(Transform playerNameplateCanvas)
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00017B8C File Offset: 0x00015D8C
		private static void ChangeDevCircleSprite(Transform playerNameplateCanvas, Sprite newSprite, Rank rank)
		{
			Transform transform = playerNameplateCanvas.FindChild("NameplateGroup/Nameplate/Contents/Main/Dev Circle");
			bool flag = transform != null;
			if (flag)
			{
				ImageThreeSlice component = transform.GetComponent<ImageThreeSlice>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.prop_Sprite_0 = newSprite;
					component._sprite = newSprite;
					Color rankColor = NameplateModifier.GetRankColor(rank);
					CanvasRenderer component2 = transform.GetComponent<CanvasRenderer>();
					bool flag3 = component2 != null;
					if (flag3)
					{
						component2.SetColor(rankColor);
					}
					transform.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00017C18 File Offset: 0x00015E18
		private static void ApplyRankColoring(Transform playerNameplateCanvas, Rank rank)
		{
			Transform transform = playerNameplateCanvas.FindChild("NameplateGroup/Nameplate/Contents/Main/Dev Circle");
			bool flag = transform != null;
			if (flag)
			{
				Color rankColor = NameplateModifier.GetRankColor(rank);
				CanvasRenderer component = transform.GetComponent<CanvasRenderer>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.SetColor(rankColor);
					transform.gameObject.SetActive(true);
					MelonLogger.Msg(string.Format("Applied {0} coloring to Dev Circle", rank));
				}
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00017C88 File Offset: 0x00015E88
		public static Color GetRankColor(Rank rank)
		{
			Color result;
			switch (rank)
			{
			case Rank.Visitor:
				result = new Color(1f, 1f, 1f, 0.8f);
				break;
			case Rank.NewUser:
				result = NameplateModifier.ColorFromHex("#96ECFF", 0.8f);
				break;
			case Rank.User:
				result = NameplateModifier.ColorFromHex("#96FFA9", 0.8f);
				break;
			case Rank.Known:
				result = NameplateModifier.ColorFromHex("#FF5E50", 0.8f);
				break;
			case Rank.Trusted:
				result = NameplateModifier.ColorFromHex("#A900FE", 0.8f);
				break;
			default:
				result = new Color(1f, 1f, 1f, 0.8f);
				break;
			}
			return result;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00017D38 File Offset: 0x00015F38
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

		// Token: 0x04000144 RID: 324
		private static List<NameplateData> playerStats = new List<NameplateData>();

		// Token: 0x04000145 RID: 325
		private static HttpClient httpClient = new HttpClient();

		// Token: 0x04000146 RID: 326
		private static string API_BASE = "https://odiumvrc.com/api/odium/tags";

		// Token: 0x04000147 RID: 327
		private static Dictionary<string, List<string>> tagCache = new Dictionary<string, List<string>>();

		// Token: 0x04000148 RID: 328
		private static HashSet<string> clientUsers = new HashSet<string>();

		// Token: 0x04000149 RID: 329
		private static string CLIENT_API_BASE = "https://snoofz.net/api/odium/user/exists";

		// Token: 0x0400014A RID: 330
		private static bool autoRefreshEnabled = true;

		// Token: 0x0400014B RID: 331
		private static float lastRefreshTime = 0f;

		// Token: 0x0400014C RID: 332
		private static readonly float REFRESH_INTERVAL = 10f;
	}
}

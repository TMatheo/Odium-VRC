using System;
using System.Collections.Generic;
using System.Reflection;
using ExitGames.Client.Photon;
using HarmonyLib;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using Odium.Components;
using Odium.Modules;
using Odium.Odium;
using Odium.UX;
using Odium.Wrappers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VampClient.Api;
using VRC;
using VRC.Udon;

namespace Odium.Patches
{
	// Token: 0x0200002E RID: 46
	public static class PhotonPatchesManual
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public static void ApplyPatches()
		{
			OdiumEntry.HarmonyInstance.Patch(typeof(LoadBalancingClient).GetMethod("OnEvent", new Type[]
			{
				typeof(EventData)
			}), new HarmonyMethod(typeof(PhotonPatchesManual).GetMethod("OnEvent", BindingFlags.Static | BindingFlags.Public)), null, null, null, null);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000AC00 File Offset: 0x00008E00
		public static bool OnEvent(LoadBalancingClient __instance, ref EventData param_1)
		{
			byte code = param_1.Code;
			byte b = code;
			byte b2 = b;
			if (b2 <= 18)
			{
				if (b2 <= 11)
				{
					if (b2 != 1)
					{
						if (b2 == 11)
						{
							bool blockUdon = PhotonPatches.BlockUdon;
							if (blockUdon)
							{
								return false;
							}
						}
					}
				}
				else if (b2 != 12)
				{
					if (b2 == 18)
					{
						try
						{
							EventData eventData = param_1;
							bool flag = ((eventData != null) ? eventData.Parameters : null) == null || !param_1.Parameters.ContainsKey(param_1.CustomDataKey);
							if (flag)
							{
								string format = "[BLOCK] Invalid event parameters - Parameters null: {0}, Missing key: {1}";
								EventData eventData2 = param_1;
								object arg = ((eventData2 != null) ? eventData2.Parameters : null) == null;
								EventData eventData3 = param_1;
								bool? flag2;
								if (eventData3 == null)
								{
									flag2 = null;
								}
								else
								{
									ParameterDictionary parameters = eventData3.Parameters;
									flag2 = ((parameters != null) ? new bool?(!parameters.ContainsKey(param_1.CustomDataKey)) : null);
								}
								InternalConsole.LogIntoConsole(string.Format(format, arg, flag2), "<color=#8d142b>[Log]</color>", "8d142b");
								return false;
							}
							Dictionary<byte, Il2CppSystem.Object> dictionary = param_1.Parameters[param_1.CustomDataKey].Cast<Dictionary<byte, Il2CppSystem.Object>>();
							bool flag3 = dictionary == null || !dictionary.ContainsKey(0) || !dictionary.ContainsKey(1) || !dictionary.ContainsKey(2);
							if (flag3)
							{
								InternalConsole.LogIntoConsole(string.Format("[BLOCK] Missing required dictionary keys - Dict null: {0}, Has key 0: {1}, Has key 1: {2}, Has key 2: {3}", new object[]
								{
									dictionary == null,
									(dictionary != null) ? new bool?(dictionary.ContainsKey(0)) : null,
									(dictionary != null) ? new bool?(dictionary.ContainsKey(1)) : null,
									(dictionary != null) ? new bool?(dictionary.ContainsKey(2)) : null
								}), "<color=#8d142b>[Log]</color>", "8d142b");
								return false;
							}
							int num;
							byte b3;
							int num2;
							bool flag4 = !PhotonPatches.TryUnboxInt(dictionary[2], out num) || !PhotonPatches.TryUnboxByte(dictionary[0], out b3) || !PhotonPatches.TryUnboxInt(dictionary[1], out num2);
							if (flag4)
							{
								int num3;
								byte b4;
								InternalConsole.LogIntoConsole(string.Format("[BLOCK] Invalid data types - viewId unbox: {0}, eventType unbox: {1}, eventHash unbox: {2}", PhotonPatches.TryUnboxInt(dictionary[2], out num3), PhotonPatches.TryUnboxByte(dictionary[0], out b4), PhotonPatches.TryUnboxInt(dictionary[1], out num3)), "<color=#8d142b>[Log]</color>", "8d142b");
								return false;
							}
							bool flag5 = num < 0 || num > 999999;
							if (flag5)
							{
								InternalConsole.LogIntoConsole(string.Format("[BLOCK] Suspicious viewId: {0} (range: 0-999999)", num), "<color=#8d142b>[Log]</color>", "8d142b");
								return false;
							}
							bool flag6 = b3 == 1 || b3 == 2;
							if (flag6)
							{
								PhotonView photonView = PhotonView.Method_Public_Static_PhotonView_Int32_0(num);
								bool flag7 = photonView == null || photonView.gameObject == null;
								if (flag7)
								{
									InternalConsole.LogIntoConsole(string.Format("[BLOCK] Invalid PhotonView for viewId: {0} - PhotonView null: {1}, GameObject null: {2}", num, photonView == null, ((photonView != null) ? photonView.gameObject : null) == null), "<color=#8d142b>[Log]</color>", "8d142b");
									return false;
								}
								UdonBehaviour component = photonView.gameObject.GetComponent<UdonBehaviour>();
								bool flag8 = component == null;
								if (flag8)
								{
									return true;
								}
								string name = component.gameObject.name;
								uint num4 = (uint)num2;
								VRC.Player vrcplayerFromActorNr = PlayerWrapper.GetVRCPlayerFromActorNr(param_1.sender);
								bool flag9 = ((vrcplayerFromActorNr != null) ? vrcplayerFromActorNr.field_Private_APIUser_0 : null) == null;
								if (flag9)
								{
									InternalConsole.LogIntoConsole(string.Format("[BLOCK] Invalid player data - Player null: {0}, APIUser null: {1}", vrcplayerFromActorNr == null, ((vrcplayerFromActorNr != null) ? vrcplayerFromActorNr.field_Private_APIUser_0 : null) == null), "<color=#8d142b>[Log]</color>", "8d142b");
									return false;
								}
								string text = vrcplayerFromActorNr.field_Private_APIUser_0.displayName ?? "Unknown";
								string text2 = vrcplayerFromActorNr.field_Private_APIUser_0.id ?? "Unknown";
								bool flag10 = !PhotonPatches.crashAttemptCounts.ContainsKey(text);
								if (flag10)
								{
									PhotonPatches.crashAttemptCounts[text] = 0;
								}
								bool flag11 = !PhotonPatches.lastLogTimes.ContainsKey(text);
								if (flag11)
								{
									PhotonPatches.lastLogTimes[text] = DateTime.MinValue;
								}
								bool flag12 = false;
								string text3 = "";
								string text4;
								bool flag13 = component.TryGetEntrypointNameFromHash(num4, ref text4);
								if (flag13)
								{
									bool flag14 = text4 == "SyncAssignM" || text4 == "SyncAssignD" || text4 == "SyncAssignB" || text4 == "KillSync";
									if (flag14)
									{
										string key = text2 + "_" + text4;
										DateTime now = DateTime.Now;
										bool flag15 = PhotonPatches.syncAssignMCooldowns.ContainsKey(key);
										if (flag15)
										{
											DateTime d = PhotonPatches.syncAssignMCooldowns[key];
											TimeSpan timeSpan = now - d;
											bool flag16 = timeSpan < PhotonPatches.SYNC_ASSIGN_M_COOLDOWN;
											if (flag16)
											{
												TimeSpan timeSpan2 = PhotonPatches.SYNC_ASSIGN_M_COOLDOWN - timeSpan;
												InternalConsole.LogIntoConsole(string.Format("[RATELIMIT] SyncAssignM blocked for {0} - Cooldown remaining: {1:F1}s", text, timeSpan2.TotalSeconds), "<color=#8d142b>[Log]</color>", "8d142b");
												bool flag17 = (now - PhotonPatches.lastLogTimes[text]).TotalSeconds >= 30.0;
												if (flag17)
												{
													ToastBase.Toast("Odium Protection", string.Format("SyncAssignM rate limited for '{0}' - {1:F0}s remaining", text, timeSpan2.TotalSeconds), PhotonPatches.LogoIcon, 3f);
													PhotonPatches.lastLogTimes[text] = now;
												}
												return false;
											}
										}
										PhotonPatches.syncAssignMCooldowns[key] = now;
										InternalConsole.LogIntoConsole("[RATELIMIT] SyncAssignM allowed for " + text + " - Cooldown set for 2 minutes", "<color=#8d142b>[Log]</color>", "8d142b");
									}
									OdiumConsole.Log("Udon", string.Format("\r\n======= {0} =======\r\nPlayer ID -> {1}\r\nBehavior Name -> {2}\r\nEntry Point Name -> {3}\r\nEvent Hash -> {4}\r\nEvent Type -> {5}\r\nGameObject Path -> {6}\r\nTimestamp -> {7:yyyy-MM-dd HH:mm:ss.fff}\r\nRate Limited -> {8}\r\n", new object[]
									{
										text,
										text2,
										name,
										text4,
										num4,
										b3,
										PhotonPatches.GetGameObjectPath(photonView.gameObject),
										DateTime.Now,
										(text4 == "SyncAssignM") ? "Yes (2min)" : "No"
									}), LogLevel.Info);
									bool flag18 = text4 == "ListPatrons" && name == "Patreon Credits";
									if (flag18)
									{
										flag12 = true;
										text3 = "ListPatrons exploit";
									}
									else
									{
										bool flag19 = PhotonPatches.IsKnownExploitPattern(text4, name);
										if (flag19)
										{
											bool flag20 = !AssignedVariables.preventPatreonCrash;
											if (flag20)
											{
												flag12 = false;
											}
											else
											{
												flag12 = true;
												text3 = "Known exploit pattern";
											}
										}
									}
								}
								bool flag21 = !flag12;
								if (flag21)
								{
									bool flag22 = PhotonPatches.IsRapidFireEvent(text);
									if (flag22)
									{
										flag12 = true;
										text3 = "Rapid-fire events";
									}
									bool flag23 = PhotonPatches.IsUnusualHash(num4);
									if (flag23)
									{
										flag12 = true;
										text3 = "Unusual hash value";
									}
								}
								bool flag24 = flag12;
								if (flag24)
								{
									Dictionary<string, int> crashAttemptCounts = PhotonPatches.crashAttemptCounts;
									string key2 = text;
									int num3 = crashAttemptCounts[key2];
									crashAttemptCounts[key2] = num3 + 1;
									int num5 = PhotonPatches.crashAttemptCounts[text];
									DateTime now2 = DateTime.Now;
									bool flag25 = (now2 - PhotonPatches.lastLogTimes[text]).TotalSeconds >= 15.0;
									if (flag25)
									{
										InternalConsole.LogIntoConsole(string.Concat(new string[]
										{
											"-> Prevented: ",
											text,
											" [Reason: ",
											text3,
											"]"
										}), "<color=#8d142b>[Log]</color>", "8d142b");
										ToastBase.Toast("Odium Protection", string.Concat(new string[]
										{
											"Potentially harmful event blocked from user '",
											text,
											"' (Reason: ",
											text3,
											")"
										}), PhotonPatches.LogoIcon, 5f);
										PhotonPatches.lastLogTimes[text] = now2;
									}
									return false;
								}
							}
						}
						catch (Exception ex)
						{
							InternalConsole.LogIntoConsole("Error in event protection: " + ex.Message, "<color=#8d142b>[Log]</color>", "8d142b");
							return false;
						}
						return true;
					}
				}
				else
				{
					PhotonPatches.UpdatePlayerActivity(param_1.sender, (int)code);
				}
			}
			else if (b2 <= 34)
			{
				if (b2 != 33)
				{
					if (b2 == 34)
					{
						Ratelimit.ProcessRateLimit(ref param_1);
					}
				}
				else
				{
					bool flag26 = param_1.Parameters != null && param_1.Parameters.ContainsKey(245);
					if (flag26)
					{
						Dictionary<byte, Il2CppSystem.Object> dictionary2 = param_1.Parameters[245].TryCast<Dictionary<byte, Il2CppSystem.Object>>();
						bool flag27 = dictionary2 != null;
						if (flag27)
						{
							bool flag28 = dictionary2.ContainsKey(0);
							if (flag28)
							{
								byte b5 = dictionary2[0].Unbox<byte>();
							}
							bool flag29 = dictionary2.ContainsKey(1);
							if (flag29)
							{
								int id = dictionary2[1].Unbox<int>();
								bool flag30 = dictionary2.ContainsKey(10);
								if (flag30)
								{
									bool flag31 = dictionary2[10].Unbox<bool>();
									bool flag32 = flag31 && !PhotonPatches.blockedUserIds.Contains(id.ToString());
									if (flag32)
									{
										PhotonPatches.blockedUserIds.Add(id.ToString());
										VRCPlayer playerFromPhotonId = PlayerWrapper.GetPlayerFromPhotonId(id);
										bool announceBlocks = AssignedVariables.announceBlocks;
										if (announceBlocks)
										{
											Chatbox.SendCustomChatMessage("[Odium] -> " + playerFromPhotonId.field_Private_VRCPlayerApi_0.displayName + " BLOCKED me");
										}
										Color rankColor = NameplateModifier.GetRankColor(NameplateModifier.GetPlayerRank(playerFromPhotonId._player.field_Private_APIUser_0));
										string str = NameplateModifier.ColorToHex(rankColor);
										OdiumBottomNotification.ShowNotification("<color=#FF5151>BLOCKED</color> by <color=" + str + ">" + playerFromPhotonId.field_Private_VRCPlayerApi_0.displayName);
										InternalConsole.LogIntoConsole("<color=#7B02FE>" + playerFromPhotonId.field_Private_VRCPlayerApi_0.displayName + "</color> <color=red>BLOCKED</color> you!", "<color=#8d142b>[Log]</color>", "8d142b");
									}
									else
									{
										bool flag33 = !flag31 && PhotonPatches.blockedUserIds.Contains(id.ToString());
										if (flag33)
										{
											PhotonPatches.blockedUserIds.Remove(id.ToString());
											VRCPlayer playerFromPhotonId2 = PlayerWrapper.GetPlayerFromPhotonId(id);
											bool announceBlocks2 = AssignedVariables.announceBlocks;
											if (announceBlocks2)
											{
												Chatbox.SendCustomChatMessage("[Odium] -> " + playerFromPhotonId2.field_Private_VRCPlayerApi_0.displayName + " UNBLOCKED me");
											}
											Color rankColor2 = NameplateModifier.GetRankColor(NameplateModifier.GetPlayerRank(playerFromPhotonId2._player.field_Private_APIUser_0));
											string str2 = NameplateModifier.ColorToHex(rankColor2);
											OdiumBottomNotification.ShowNotification("<color=#FF5151>UNBLOCKED</color> by <color=" + str2 + ">" + playerFromPhotonId2.field_Private_VRCPlayerApi_0.displayName);
											InternalConsole.LogIntoConsole("<color=#7B02FE>" + playerFromPhotonId2.field_Private_VRCPlayerApi_0.displayName + "</color> <color=red>UNBLOCKED</color> you!", "<color=#8d142b>[Log]</color>", "8d142b");
										}
									}
								}
								bool flag34 = dictionary2.ContainsKey(11);
								if (flag34)
								{
									bool flag35 = dictionary2[11].Unbox<bool>();
									bool flag36 = flag35 && !PhotonPatches.mutedUserIds.Contains(id.ToString());
									if (flag36)
									{
										PhotonPatches.mutedUserIds.Add(id.ToString());
										VRCPlayer playerFromPhotonId3 = PlayerWrapper.GetPlayerFromPhotonId(id);
										bool announceMutes = AssignedVariables.announceMutes;
										if (announceMutes)
										{
											Chatbox.SendCustomChatMessage("[Odium] -> " + playerFromPhotonId3.field_Private_VRCPlayerApi_0.displayName + " MUTED me");
										}
										Color rankColor3 = NameplateModifier.GetRankColor(NameplateModifier.GetPlayerRank(playerFromPhotonId3._player.field_Private_APIUser_0));
										string str3 = NameplateModifier.ColorToHex(rankColor3);
										OdiumBottomNotification.ShowNotification("<color=#FF5151>MUTED</color> by <color=" + str3 + ">" + playerFromPhotonId3.field_Private_VRCPlayerApi_0.displayName);
										InternalConsole.LogIntoConsole("<color=#7B02FE>" + playerFromPhotonId3.field_Private_VRCPlayerApi_0.displayName + "</color> <color=red>MUTED</color> you!", "<color=#8d142b>[Log]</color>", "8d142b");
									}
									else
									{
										bool flag37 = !flag35 && PhotonPatches.mutedUserIds.Contains(id.ToString());
										if (flag37)
										{
											PhotonPatches.mutedUserIds.Remove(id.ToString());
											VRCPlayer playerFromPhotonId4 = PlayerWrapper.GetPlayerFromPhotonId(id);
											bool announceMutes2 = AssignedVariables.announceMutes;
											if (announceMutes2)
											{
												Chatbox.SendCustomChatMessage("[Odium] -> " + playerFromPhotonId4.field_Private_VRCPlayerApi_0.displayName + " unfortunately UNMUTED me");
											}
											Color rankColor4 = NameplateModifier.GetRankColor(NameplateModifier.GetPlayerRank(playerFromPhotonId4._player.field_Private_APIUser_0));
											string str4 = NameplateModifier.ColorToHex(rankColor4);
											OdiumBottomNotification.ShowNotification("<color=#FF5151>UNMUTED</color> by <color=" + str4 + ">" + playerFromPhotonId4.field_Private_VRCPlayerApi_0.displayName);
											InternalConsole.LogIntoConsole("<color=#7B02FE>" + playerFromPhotonId4.field_Private_VRCPlayerApi_0.displayName + "</color> <color=red>UNMUTED</color> you!", "<color=#8d142b>[Log]</color>", "8d142b");
										}
									}
								}
							}
						}
					}
					bool flag38 = param_1.Parameters != null && param_1.Parameters.ContainsKey(254);
					if (flag38)
					{
						Il2CppSystem.Object @object = param_1.Parameters[254];
					}
				}
			}
			else if (b2 != 43)
			{
				if (b2 == 208)
				{
					try
					{
						InternalConsole.LogIntoConsole("<color=#31BCF0>[MasterClient]:</color> Master client switch detected", "<color=#8d142b>[Log]</color>", "8d142b");
						bool flag39 = param_1.Parameters != null && param_1.Parameters.ContainsKey(254);
						if (flag39)
						{
							Il2CppSystem.Object object2 = param_1.Parameters[254];
							bool flag40 = object2 != null;
							if (flag40)
							{
								int num6 = object2.Unbox<int>();
								VRCPlayer playerFromPhotonId5 = PlayerWrapper.GetPlayerFromPhotonId(num6);
								bool flag41 = playerFromPhotonId5 != null;
								if (flag41)
								{
									Color rankColor5 = NameplateModifier.GetRankColor(NameplateModifier.GetPlayerRank(playerFromPhotonId5._player.field_Private_APIUser_0));
									string text5 = NameplateModifier.ColorToHex(rankColor5);
									InternalConsole.LogIntoConsole(string.Format("<color=#31BCF0>[MasterClient]:</color> New master: <color={0}>{1}</color> (ID: {2})", text5, playerFromPhotonId5.field_Private_VRCPlayerApi_0.displayName, num6), "<color=#8d142b>[Log]</color>", "8d142b");
									OdiumBottomNotification.ShowNotification(string.Concat(new string[]
									{
										"<color=",
										text5,
										">",
										playerFromPhotonId5.field_Private_VRCPlayerApi_0.displayName,
										"</color> is the new <color=#FFECA1>Master</color>"
									}));
								}
								else
								{
									InternalConsole.LogIntoConsole(string.Format("<color=#31BCF0>[MasterClient]:</color> New master actor ID: {0} (Player not found)", num6), "<color=#8d142b>[Log]</color>", "8d142b");
								}
							}
						}
					}
					catch (Exception ex2)
					{
						InternalConsole.LogIntoConsole("<color=#31BCF0>[MasterClient]:</color> <color=red>Error processing master client switch: " + ex2.Message + "</color>", "<color=#8d142b>[Log]</color>", "8d142b");
					}
				}
			}
			else
			{
				bool flag42 = !AssignedVariables.chatBoxAntis;
				if (flag42)
				{
					return true;
				}
				string text6 = "";
				try
				{
					byte[] bytes = param_1.CustomData.Il2ToByteArray();
					text6 = ChatboxLogger.ConvertBytesToText(bytes);
					text6 = text6.Replace("�", "").Replace("\v", "").Replace("\"", "").Trim();
				}
				catch (Exception ex3)
				{
					OdiumConsole.Log("ChatBox", "Error extracting message: " + ex3.Message, LogLevel.Info);
					text6 = "[Error extracting message]";
				}
				bool flag43 = ChatboxAntis.IsMessageValid(text6, -1);
				if (flag43)
				{
					return true;
				}
				bool flag44 = !PhotonPatches.blockedMessagesCount.ContainsKey(param_1.sender);
				if (flag44)
				{
					PhotonPatches.blockedMessagesCount[param_1.sender] = 0;
					PhotonPatches.blockedMessages[param_1.sender] = 0;
				}
				Dictionary<int, int> blockedMessagesCount = PhotonPatches.blockedMessagesCount;
				int num3 = param_1.sender;
				int num7 = blockedMessagesCount[num3];
				blockedMessagesCount[num3] = num7 + 1;
				Dictionary<int, int> blockedMessages = PhotonPatches.blockedMessages;
				num7 = param_1.sender;
				num3 = blockedMessages[num7];
				blockedMessages[num7] = num3 + 1;
				bool flag45 = PhotonPatches.blockedMessagesCount[param_1.sender] == 1;
				if (flag45)
				{
					VRC.Player vrcplayerFromActorNr2 = PlayerWrapper.GetVRCPlayerFromActorNr(param_1.sender);
					InternalConsole.LogIntoConsole("<color=red>Blocked chatbox message from user -> " + vrcplayerFromActorNr2.field_Private_APIUser_0.displayName + "</color>", "<color=#8d142b>[Log]</color>", "8d142b");
				}
				else
				{
					bool flag46 = PhotonPatches.blockedMessages[param_1.sender] >= 100;
					if (flag46)
					{
						VRC.Player vrcplayerFromActorNr3 = PlayerWrapper.GetVRCPlayerFromActorNr(param_1.sender);
						InternalConsole.LogIntoConsole(string.Format("<color=red>Blocked {0} total chatbox messages from user -> {1}</color>", PhotonPatches.blockedMessagesCount[param_1.sender], vrcplayerFromActorNr3.field_Private_APIUser_0.displayName), "<color=#8d142b>[Log]</color>", "8d142b");
						PhotonPatches.blockedMessages[param_1.sender] = 0;
					}
				}
				return false;
			}
			return true;
		}
	}
}

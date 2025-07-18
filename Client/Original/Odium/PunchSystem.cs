using System;
using System.Collections;
using System.Collections.Generic;
using MelonLoader;
using Odium;
using Odium.GameCheats;
using Odium.Patches;
using UnityEngine;
using VRC;

// Token: 0x0200000E RID: 14
public static class PunchSystem
{
	// Token: 0x0600004E RID: 78 RVA: 0x00003B64 File Offset: 0x00001D64
	public static void Initialize()
	{
		bool isInitialized = PunchSystem._isInitialized;
		if (!isInitialized)
		{
			MelonCoroutines.Start(PunchSystem.SetupPunchDetection());
			PunchSystem._isInitialized = true;
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003B8E File Offset: 0x00001D8E
	private static IEnumerator SetupPunchDetection()
	{
		return new PunchSystem.<SetupPunchDetection>d__8(0);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003B96 File Offset: 0x00001D96
	private static IEnumerator PunchDetectionLoop()
	{
		return new PunchSystem.<PunchDetectionLoop>d__9(0);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003BA0 File Offset: 0x00001DA0
	private static void CheckForPunches()
	{
		bool flag = Time.time - PunchSystem._lastPunchTime < 0.3f;
		if (!flag)
		{
			List<Player> nonLocalPlayers = PunchSystem.GetNonLocalPlayers();
			bool flag2 = nonLocalPlayers.Count == 0;
			if (!flag2)
			{
				bool flag3 = PunchSystem._rightHand != null;
				if (flag3)
				{
					foreach (Player player in nonLocalPlayers)
					{
						bool flag4 = PunchSystem.IsHandNearPlayer(PunchSystem._rightHand, player);
						if (flag4)
						{
							PunchSystem.HandlePunch(player);
							return;
						}
					}
				}
				bool flag5 = PunchSystem._leftHand != null;
				if (flag5)
				{
					foreach (Player player2 in nonLocalPlayers)
					{
						bool flag6 = PunchSystem.IsHandNearPlayer(PunchSystem._leftHand, player2);
						if (flag6)
						{
							PunchSystem.HandlePunch(player2);
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003CBC File Offset: 0x00001EBC
	private static bool IsHandNearPlayer(Transform hand, Player player)
	{
		bool result;
		try
		{
			Vector3 bonePosition = player.field_Private_VRCPlayerApi_0.GetBonePosition(10);
			Vector3 bonePosition2 = player.field_Private_VRCPlayerApi_0.GetBonePosition(8);
			result = (Vector3.Distance(hand.position, bonePosition) < 0.25f || Vector3.Distance(hand.position, bonePosition2) < 0.25f);
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003D2C File Offset: 0x00001F2C
	private static List<Player> GetNonLocalPlayers()
	{
		List<Player> list = new List<Player>();
		foreach (Player player in PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0)
		{
			bool flag = player != null && !player.field_Private_VRCPlayerApi_0.isLocal;
			if (flag)
			{
				list.Add(player);
			}
		}
		return list;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003D91 File Offset: 0x00001F91
	private static void HandlePunch(Player targetPlayer)
	{
		PunchSystem._lastPunchTime = Time.time;
		OdiumConsole.Log("PunchSystem", "Punched " + targetPlayer.field_Private_VRCPlayerApi_0.displayName + "!", LogLevel.Info);
		PunchSystem.SendUdonEvent(targetPlayer);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003DCC File Offset: 0x00001FCC
	private static void SendUdonEvent(Player targetPlayer)
	{
		PhotonPatches.BlockUdon = true;
		for (int i = 0; i < 100; i++)
		{
			Murder4Utils.SendTargetedPatreonEvent(targetPlayer, "ListPatrons");
		}
		PhotonPatches.BlockUdon = false;
	}

	// Token: 0x04000029 RID: 41
	private const float COOLDOWN_TIME = 0.3f;

	// Token: 0x0400002A RID: 42
	private const float DETECTION_INTERVAL = 0.05f;

	// Token: 0x0400002B RID: 43
	private const float PUNCH_DISTANCE = 0.25f;

	// Token: 0x0400002C RID: 44
	private static float _lastPunchTime;

	// Token: 0x0400002D RID: 45
	private static bool _isInitialized;

	// Token: 0x0400002E RID: 46
	private static Transform _leftHand;

	// Token: 0x0400002F RID: 47
	private static Transform _rightHand;
}

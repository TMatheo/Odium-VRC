using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Odium.Components;
using Odium.Odium;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace Odium.Wrappers
{
	// Token: 0x0200003F RID: 63
	public static class PlayerWrapper
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000DFEE File Offset: 0x0000C1EE
		public static List<Player> GetAllPlayers()
		{
			return PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList<Player>();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000E004 File Offset: 0x0000C204
		public static int LocalPlayerActorNr
		{
			get
			{
				return PlayerWrapper.LocalPlayer.prop_Player_1.prop_Int32_0;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000E018 File Offset: 0x0000C218
		public static Vector3 GetPosition(Player player)
		{
			return player.gameObject.transform.position;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000E03C File Offset: 0x0000C23C
		public static Player GetPlayerById(string playerId)
		{
			return PlayerWrapper.Players.Find((Player player) => player.field_Private_APIUser_0.id == playerId);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000E074 File Offset: 0x0000C274
		public static Vector3 GetBonePosition(Player player, HumanBodyBones bone)
		{
			return player.field_Private_VRCPlayerApi_0.GetBonePosition(bone);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000E094 File Offset: 0x0000C294
		public static Vector3 GetVelocity(Player player)
		{
			return player.field_Private_VRCPlayerApi_0.GetVelocity();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		public static Transform GetBoneTransform(Player player, HumanBodyBones bone)
		{
			return player.field_Private_VRCPlayerApi_0.GetBoneTransform(bone);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		public static void QuickSpoof()
		{
			bool adminSpoof = AssignedVariables.adminSpoof;
			if (adminSpoof)
			{
				string displayName = Networking.LocalPlayer.displayName;
				string text = "eZbake";
				VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.displayName = text;
				OdiumConsole.Log("OwnerSpoof", "Spoofed as: " + displayName + " | CustomName: " + text, LogLevel.Info);
				bool flag = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.displayName == text;
				if (flag)
				{
					OdiumConsole.Log("QuickSpoof", "Spoofing successful!", LogLevel.Info);
				}
				else
				{
					OdiumConsole.Log("QuickSpoof", "Spoofing failed. DisplayName mismatch.", LogLevel.Info);
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000E16C File Offset: 0x0000C36C
		public static int GetViewID()
		{
			VRCPlayer vrcplayer = PlayerWrapper.LocalPlayer._vrcplayer;
			return vrcplayer.Method_Public_Int32_0();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000E194 File Offset: 0x0000C394
		public static GameObject GetNamePlateContainer(Player player)
		{
			return player._vrcplayer.field_Public_GameObject_0;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		public static VRCPlayerApi GetLocalPlayerAPIUser(string userId)
		{
			return PlayerWrapper.GetAllPlayers().ToList<Player>().Find((Player plr) => plr.field_Private_APIUser_0.id == userId).field_Private_VRCPlayerApi_0;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public static Player GetPlayerByDisplayName(string name)
		{
			return PlayerWrapper.GetAllPlayers().ToList<Player>().Find((Player plr) => plr.field_Private_APIUser_0.displayName == name);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000E234 File Offset: 0x0000C434
		public static VRCPlayer GetVRCPlayerFromId(string userId)
		{
			return PlayerWrapper.GetAllPlayers().ToList<Player>().Find((Player plr) => plr.field_Private_APIUser_0.id == userId).prop_VRCPlayer_0;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000E278 File Offset: 0x0000C478
		public static VRCPlayer GetVRCPlayerFromPhotonId(int plrId)
		{
			return PlayerWrapper.GetAllPlayers().ToList<Player>().Find((Player plr) => plr.field_Private_VRCPlayerApi_0.playerId == plrId).prop_VRCPlayer_0;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public static VRCPlayer GetPlayerFromPhotonId(int id)
		{
			return PlayerWrapper.GetAllPlayers().ToList<Player>().Find((Player plr) => plr.field_Private_VRCPlayerApi_0.playerId == id).prop_VRCPlayer_0;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000E300 File Offset: 0x0000C500
		public static Player GetVRCPlayerFromActorNr(int id)
		{
			return PlayerWrapper.GetAllPlayers().ToList<Player>().Find((Player plr) => plr.prop_Player_1.prop_Int32_0 == id).prop_VRCPlayer_0._player;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000E348 File Offset: 0x0000C548
		public static VRCPlayer GetPlayerFromActorNr(int id)
		{
			return PlayerWrapper.GetAllPlayers().ToList<Player>().Find((Player plr) => plr.field_Private_VRCPlayerApi_0.playerId == id).prop_VRCPlayer_0;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000E38C File Offset: 0x0000C58C
		public static Transform GetNamePlateCanvas(Player player)
		{
			GameObject namePlateContainer = PlayerWrapper.GetNamePlateContainer(player);
			bool flag = namePlateContainer == null;
			Transform result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = namePlateContainer.transform.FindChild("PlayerNameplate/Canvas");
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		private static Rank GetPlayerRank(APIUser apiUser)
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

		// Token: 0x060001A7 RID: 423 RVA: 0x0000E424 File Offset: 0x0000C624
		private static Color GetRankColor(Rank rank)
		{
			Color result;
			switch (rank)
			{
			case Rank.Visitor:
				result = new Color(1f, 1f, 1f, 0.8f);
				break;
			case Rank.NewUser:
				result = PlayerWrapper.ColorFromHex("#96ECFF", 0.8f);
				break;
			case Rank.User:
				result = PlayerWrapper.ColorFromHex("#96FFA9", 0.8f);
				break;
			case Rank.Known:
				result = PlayerWrapper.ColorFromHex("#FF5E50", 0.8f);
				break;
			case Rank.Trusted:
				result = PlayerWrapper.ColorFromHex("#A900FE", 0.8f);
				break;
			default:
				result = new Color(1f, 1f, 1f, 0.8f);
				break;
			}
			return result;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
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

		// Token: 0x040000BA RID: 186
		public static Player LocalPlayer = null;

		// Token: 0x040000BB RID: 187
		public static int ActorId = 0;

		// Token: 0x040000BC RID: 188
		public static List<Player> Players = new List<Player>();
	}
}

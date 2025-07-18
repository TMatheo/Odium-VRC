using System;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRC.Udon;

// Token: 0x02000010 RID: 16
public static class UdonExtensions
{
	// Token: 0x0600005E RID: 94 RVA: 0x000040A8 File Offset: 0x000022A8
	public static void SendUdon(this GameObject go, string evt, Player player = null, bool check = false)
	{
		UdonBehaviour component = go.GetComponent<UdonBehaviour>();
		bool flag = player == null;
		if (flag)
		{
			if (!check)
			{
				bool flag2 = player == VRCPlayer.field_Internal_Static_VRCPlayer_0._player;
				if (flag2)
				{
					component.SendCustomEvent(evt);
				}
				else
				{
					component.SendCustomNetworkEvent(0, evt);
				}
			}
		}
		else
		{
			go.SetOwner(player);
			component.SendCustomNetworkEvent(1, evt);
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004110 File Offset: 0x00002310
	public static void SetOwner(this GameObject go, Player player)
	{
		bool flag = go.GetOwner() == player;
		if (!flag)
		{
			Networking.SetOwner(player.field_Private_VRCPlayerApi_0, go);
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004140 File Offset: 0x00002340
	public static Player GetOwner(this GameObject go)
	{
		foreach (Player player in PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0)
		{
			bool flag = player.field_Private_VRCPlayerApi_0.IsOwner(go);
			if (flag)
			{
				return player;
			}
		}
		return null;
	}
}

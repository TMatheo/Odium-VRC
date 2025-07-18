using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Odium.Components;
using UnityEngine;

namespace Odium.Wrappers
{
	// Token: 0x02000047 RID: 71
	internal class PortalWrapper
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		public static void CreatePortal(string InstanceID, Vector3 Position, float Rotation)
		{
			bool flag = InstanceID != null;
			if (flag)
			{
				PhotonExtensions.RaiseEvent(70, new Dictionary<byte, object>
				{
					{
						0,
						0
					},
					{
						5,
						InstanceID
					},
					{
						6,
						PhotonExtensions.SerializeVector3(Position)
					},
					{
						7,
						Rotation
					}
				}, new RaiseEventOptions(), SendOptions.SendReliable);
			}
		}
	}
}

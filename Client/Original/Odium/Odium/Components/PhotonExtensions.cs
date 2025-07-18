using System;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Il2CppSystem;
using Il2CppSystem.Collections.Concurrent;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace Odium.Components
{
	// Token: 0x02000066 RID: 102
	public static class PhotonExtensions
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00017EA2 File Offset: 0x000160A2
		public static void RaiseEvent(byte eventCode, Il2CppSystem.Object eventData, RaiseEventOptions options, SendOptions sendOptions)
		{
			PhotonNetwork.Method_Public_Static_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(eventCode, eventData, options, sendOptions);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00017EB0 File Offset: 0x000160B0
		public static void RaiseEvent(byte eventCode, object eventData, RaiseEventOptions options, SendOptions sendOptions)
		{
			Il2CppSystem.Object eventData2 = eventData.FromManagedToIL2CPP<Il2CppSystem.Object>();
			PhotonExtensions.RaiseEvent(eventCode, eventData2, options, sendOptions);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00017ED0 File Offset: 0x000160D0
		public static ConcurrentDictionary<int, Photon.Realtime.Player> GetAllPlayers()
		{
			return VRC.Player.prop_Player_0.prop_Player_1.prop_Room_0.prop_ConcurrentDictionary_2_Int32_Player_0;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00017EF8 File Offset: 0x000160F8
		public static void SendLowLevelEvent(byte eventCode, object payload, byte channel = 8, bool encrypt = true, bool reliable = false)
		{
			bool flag = Networking.LocalPlayer == null;
			if (!flag)
			{
				try
				{
					ParameterDictionary parameterDictionary = new ParameterDictionary();
					parameterDictionary.Add(244, new StructWrapper<byte>(4)
					{
						value = eventCode
					});
					parameterDictionary.Add(245, payload.FromManagedToIL2CPP<Il2CppSystem.Object>());
					SendOptions sendOptions = default(SendOptions);
					sendOptions.DeliveryMode = 2;
					sendOptions.Encrypt = encrypt;
					sendOptions.Channel = channel;
					sendOptions.Reliability = reliable;
					SendOptions sendOptions2 = sendOptions;
					PhotonNetwork.field_Public_Static_LoadBalancingClient_0.field_Private_LoadBalancingPeer_0.SendOperation(253, parameterDictionary, sendOptions2);
				}
				catch (Exception ex)
				{
					Debug.LogError("Failed to send low-level event: " + ex.Message);
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00017FC4 File Offset: 0x000161C4
		public static byte[] SerializeVector3(Vector3 vector)
		{
			byte[] array = new byte[12];
			Buffer.BlockCopy(BitConverter.GetBytes(vector.x), 0, array, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector.y), 0, array, 4, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(vector.z), 0, array, 8, 4);
			return array;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00018020 File Offset: 0x00016220
		public static Vector3 DeserializeVector3(byte[] bytes)
		{
			bool flag = bytes == null || bytes.Length != 12;
			if (flag)
			{
				throw new ArgumentException("Byte array must be exactly 12 bytes for Vector3 deserialization");
			}
			return new Vector3(BitConverter.ToSingle(bytes, 0), BitConverter.ToSingle(bytes, 4), BitConverter.ToSingle(bytes, 8));
		}
	}
}

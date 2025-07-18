using System;
using ExitGames.Client.Photon;
using Odium.Components;
using Photon.Realtime;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class USpeakPacketHandler
{
	// Token: 0x06000063 RID: 99 RVA: 0x00004228 File Offset: 0x00002428
	public static byte[] CreateUSpeakPacket(float gainValue, bool isMuted, bool isWhispering, byte[] audioData)
	{
		uint timestamp = (uint)(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() & (long)((ulong)-1));
		byte b = (byte)Mathf.Clamp(gainValue * 127.5f, 0f, 255f);
		byte b2 = 55;
		if (isMuted)
		{
			b2 |= 128;
		}
		if (isWhispering)
		{
			b2 |= 64;
		}
		USpeakPacketHandler.USpeakVoicePacket packet = new USpeakPacketHandler.USpeakVoicePacket(timestamp, b, b2, audioData);
		return USpeakPacketHandler.SerializePacket(packet);
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004298 File Offset: 0x00002498
	public static byte[] SerializePacket(USpeakPacketHandler.USpeakVoicePacket packet)
	{
		byte[] array = new byte[8 + packet.audioData.Length];
		array[0] = (byte)(packet.timestamp & 255U);
		array[1] = (byte)(packet.timestamp >> 8 & 255U);
		array[2] = (byte)(packet.timestamp >> 16 & 255U);
		array[3] = (byte)(packet.timestamp >> 24 & 255U);
		array[4] = packet.gain;
		array[5] = packet.flags;
		array[6] = packet.reserved1;
		array[7] = packet.reserved2;
		bool flag = packet.audioData.Length != 0;
		if (flag)
		{
			Array.Copy(packet.audioData, 0, array, 8, packet.audioData.Length);
		}
		return array;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00004350 File Offset: 0x00002550
	public static USpeakPacketHandler.USpeakVoicePacket ParseUSpeakPacket(byte[] packetData)
	{
		bool flag = packetData.Length < 8;
		if (flag)
		{
			throw new ArgumentException("USpeak packet too short");
		}
		uint timestamp = (uint)((int)packetData[0] | (int)packetData[1] << 8 | (int)packetData[2] << 16 | (int)packetData[3] << 24);
		byte b = packetData[4];
		byte flags = packetData[5];
		byte[] array = new byte[packetData.Length - 8];
		bool flag2 = array.Length != 0;
		if (flag2)
		{
			Array.Copy(packetData, 8, array, 0, array.Length);
		}
		return new USpeakPacketHandler.USpeakVoicePacket(timestamp, b, flags, array);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000043CC File Offset: 0x000025CC
	public void SendCustomVoicePacket(byte[] audioData)
	{
		byte[] array = USpeakPacketHandler.CreateUSpeakPacket(this.gain, this.muted, this.whispering, audioData);
		string text = Convert.ToBase64String(array);
		RaiseEventOptions options = new RaiseEventOptions
		{
			field_Public_EventCaching_0 = EventCaching.DoNotCache,
			field_Public_ReceiverGroup_0 = ReceiverGroup.Others
		};
		PhotonExtensions.RaiseEvent(1, array, options, default(SendOptions));
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004424 File Offset: 0x00002624
	public static byte[] ModifyUSpeakPacket(string base64Packet, float newGain, bool newMuted)
	{
		byte[] packetData = Convert.FromBase64String(base64Packet);
		USpeakPacketHandler.USpeakVoicePacket packet = USpeakPacketHandler.ParseUSpeakPacket(packetData);
		packet.gain = (byte)Mathf.Clamp(newGain * 127.5f, 0f, 255f);
		if (newMuted)
		{
			packet.flags |= 128;
		}
		else
		{
			packet.flags &= 127;
		}
		return USpeakPacketHandler.SerializePacket(packet);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004490 File Offset: 0x00002690
	public static byte[] ApplyAudioEffects(byte[] audioData, float gainMultiplier, bool distortion = false)
	{
		byte[] array = new byte[audioData.Length];
		for (int i = 0; i < audioData.Length; i++)
		{
			int num = (int)(audioData[i] - 128);
			num = (int)((float)num * gainMultiplier);
			if (distortion)
			{
				num = ((num > 0) ? Mathf.Min(num * 2, 127) : Mathf.Max(num * 2, -128));
			}
			array[i] = (byte)Mathf.Clamp(num + 128, 0, 255);
		}
		return array;
	}

	// Token: 0x04000031 RID: 49
	public float gain = 1f;

	// Token: 0x04000032 RID: 50
	public float volume = 1f;

	// Token: 0x04000033 RID: 51
	public bool muted = false;

	// Token: 0x04000034 RID: 52
	public bool whispering = false;

	// Token: 0x020000AA RID: 170
	public struct USpeakVoicePacket
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0002314F File Offset: 0x0002134F
		public USpeakVoicePacket(uint timestamp, byte gain, byte flags, byte[] audioData)
		{
			this.timestamp = timestamp;
			this.gain = gain;
			this.flags = flags;
			this.reserved1 = 45;
			this.reserved2 = 0;
			this.audioData = (audioData ?? new byte[0]);
		}

		// Token: 0x04000280 RID: 640
		public uint timestamp;

		// Token: 0x04000281 RID: 641
		public byte gain;

		// Token: 0x04000282 RID: 642
		public byte flags;

		// Token: 0x04000283 RID: 643
		public byte reserved1;

		// Token: 0x04000284 RID: 644
		public byte reserved2;

		// Token: 0x04000285 RID: 645
		public byte[] audioData;
	}
}

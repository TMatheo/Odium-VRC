using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class USpeakVoiceManager : MonoBehaviour
{
	// Token: 0x0600006A RID: 106 RVA: 0x0000453C File Offset: 0x0000273C
	public void ProcessIncomingVoicePacket(string base64Packet)
	{
		try
		{
			byte[] packetData = Convert.FromBase64String(base64Packet);
			USpeakPacketHandler.USpeakVoicePacket uspeakVoicePacket = USpeakPacketHandler.ParseUSpeakPacket(packetData);
			bool robotVoice = this.voiceSettings.robotVoice;
			if (robotVoice)
			{
				uspeakVoicePacket.audioData = USpeakPacketHandler.ApplyAudioEffects(uspeakVoicePacket.audioData, this.voiceSettings.gain, true);
			}
			byte[] array = USpeakPacketHandler.SerializePacket(uspeakVoicePacket);
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to process voice packet: " + ex.Message);
		}
	}

	// Token: 0x04000035 RID: 53
	public USpeakVoiceManager.VoiceSettings voiceSettings = new USpeakVoiceManager.VoiceSettings();

	// Token: 0x020000AB RID: 171
	[Serializable]
	public class VoiceSettings
	{
		// Token: 0x04000286 RID: 646
		public float gain = 1f;

		// Token: 0x04000287 RID: 647
		public float pitch = 1f;

		// Token: 0x04000288 RID: 648
		public bool muted = false;

		// Token: 0x04000289 RID: 649
		public bool robotVoice = false;

		// Token: 0x0400028A RID: 650
		public bool whisper = false;
	}
}

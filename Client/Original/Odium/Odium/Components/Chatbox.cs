using System;
using System.Collections.Generic;
using UnityEngine;

namespace Odium.Components
{
	// Token: 0x02000057 RID: 87
	public class Chatbox
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00014F34 File Offset: 0x00013134
		public static void SendFrameAnimation(string[] frames, string effectId = null, Action onComplete = null, bool loop = false, float loopWaitTime = 3f)
		{
			bool flag = effectId != null && Chatbox.activeFrameEffects.ContainsKey(effectId);
			if (flag)
			{
				Chatbox.activeFrameEffects.Remove(effectId);
			}
			Chatbox.FrameEffect value = new Chatbox.FrameEffect(frames, onComplete, loop, loopWaitTime);
			bool flag2 = effectId != null;
			if (flag2)
			{
				Chatbox.activeFrameEffects[effectId] = value;
			}
			else
			{
				string key = "auto_" + Guid.NewGuid().ToString("N").Substring(0, 8);
				Chatbox.activeFrameEffects[key] = value;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00014FC0 File Offset: 0x000131C0
		public static void CancelFrameEffect(string effectId)
		{
			bool flag = effectId != null && Chatbox.activeFrameEffects.ContainsKey(effectId);
			if (flag)
			{
				Chatbox.activeFrameEffects.Remove(effectId);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00014FF4 File Offset: 0x000131F4
		public static void UpdateFrameEffects()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, Chatbox.FrameEffect> keyValuePair in Chatbox.activeFrameEffects)
			{
				string key = keyValuePair.Key;
				Chatbox.FrameEffect value = keyValuePair.Value;
				value.frameTimer += Time.deltaTime;
				bool isWaiting = value.isWaiting;
				if (isWaiting)
				{
					bool flag = value.frameTimer >= value.waitTime;
					if (flag)
					{
						value.Reset();
					}
				}
				else
				{
					bool flag2 = value.frameTimer >= 0.12f;
					if (flag2)
					{
						value.frameTimer = 0f;
						bool flag3 = !value.HasMoreFrames();
						if (flag3)
						{
							Action onComplete = value.onComplete;
							if (onComplete != null)
							{
								onComplete();
							}
							bool shouldLoop = value.shouldLoop;
							if (shouldLoop)
							{
								value.isWaiting = true;
								value.frameTimer = 0f;
							}
							else
							{
								list.Add(key);
							}
						}
						else
						{
							string currentFrame = value.GetCurrentFrame();
							Chatbox.SendCustomChatMessage(currentFrame);
							value.MoveToNextFrame();
						}
					}
				}
			}
			foreach (string key2 in list)
			{
				Chatbox.activeFrameEffects.Remove(key2);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000151A0 File Offset: 0x000133A0
		public static void SendCustomChatMessage(string message)
		{
			try
			{
				PhotonExtensions.SendLowLevelEvent(43, message, 8, true, false);
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("PhotonEvent", "Failed to send custom message: " + ex.Message, LogLevel.Error);
			}
		}

		// Token: 0x04000120 RID: 288
		private static Dictionary<string, Chatbox.FrameEffect> activeFrameEffects = new Dictionary<string, Chatbox.FrameEffect>();

		// Token: 0x020000FF RID: 255
		private class FrameEffect
		{
			// Token: 0x06000665 RID: 1637 RVA: 0x0002929D File Offset: 0x0002749D
			public FrameEffect(string[] frameArray, Action callback, bool loop = false, float loopWaitTime = 3f)
			{
				this.frames = frameArray;
				this.currentFrameIndex = 0;
				this.frameTimer = 0f;
				this.onComplete = callback;
				this.shouldLoop = loop;
				this.isWaiting = false;
				this.waitTime = loopWaitTime;
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x000292DD File Offset: 0x000274DD
			public void Reset()
			{
				this.frameTimer = 0f;
				this.currentFrameIndex = 0;
				this.isWaiting = false;
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x000292FC File Offset: 0x000274FC
			public string GetCurrentFrame()
			{
				bool flag = this.frames != null && this.currentFrameIndex < this.frames.Length;
				string result;
				if (flag)
				{
					result = this.frames[this.currentFrameIndex];
				}
				else
				{
					result = "";
				}
				return result;
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x00029342 File Offset: 0x00027542
			public void MoveToNextFrame()
			{
				this.currentFrameIndex++;
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x00029354 File Offset: 0x00027554
			public bool HasMoreFrames()
			{
				return this.currentFrameIndex < this.frames.Length;
			}

			// Token: 0x04000429 RID: 1065
			public string[] frames;

			// Token: 0x0400042A RID: 1066
			public int currentFrameIndex;

			// Token: 0x0400042B RID: 1067
			public float frameTimer;

			// Token: 0x0400042C RID: 1068
			public Action onComplete;

			// Token: 0x0400042D RID: 1069
			public bool shouldLoop;

			// Token: 0x0400042E RID: 1070
			public bool isWaiting;

			// Token: 0x0400042F RID: 1071
			public float waitTime;
		}
	}
}

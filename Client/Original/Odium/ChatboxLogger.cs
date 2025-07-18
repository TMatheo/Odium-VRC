using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Odium.UX;
using UnityEngine;

// Token: 0x0200000A RID: 10
public static class ChatboxLogger
{
	// Token: 0x06000034 RID: 52 RVA: 0x00002EB0 File Offset: 0x000010B0
	public static void LogBlockedMessage(int senderId, string playerName)
	{
		DateTime now = DateTime.Now;
		bool flag = ChatboxLogger.blockedCount.ContainsKey(senderId);
		if (flag)
		{
			Dictionary<int, int> dictionary = ChatboxLogger.blockedCount;
			int num = dictionary[senderId];
			dictionary[senderId] = num + 1;
		}
		else
		{
			ChatboxLogger.blockedCount[senderId] = 1;
		}
		bool flag2 = ChatboxLogger.sessionBlockedCount.ContainsKey(senderId);
		if (flag2)
		{
			Dictionary<int, int> dictionary2 = ChatboxLogger.sessionBlockedCount;
			int num2 = dictionary2[senderId];
			dictionary2[senderId] = num2 + 1;
		}
		else
		{
			ChatboxLogger.sessionBlockedCount[senderId] = 1;
		}
		bool flag3 = false;
		bool flag4 = !ChatboxLogger.lastLogTime.ContainsKey(senderId);
		if (flag4)
		{
			flag3 = true;
		}
		else
		{
			bool flag5 = now - ChatboxLogger.lastLogTime[senderId] >= ChatboxLogger.logCooldown;
			if (flag5)
			{
				flag3 = true;
			}
		}
		bool flag6 = flag3;
		if (flag6)
		{
			int num3 = ChatboxLogger.blockedCount[senderId];
			int num4 = ChatboxLogger.sessionBlockedCount[senderId];
			bool flag7 = num3 == 1;
			string txt;
			if (flag7)
			{
				txt = string.Format("<color=#31BCF0>[ChatBox]:</color> <color=red>Blocked message from {0} (ID: {1})</color>", playerName, senderId);
			}
			else
			{
				bool flag8 = num4 == 1;
				if (flag8)
				{
					txt = string.Format("<color=#31BCF0>[ChatBox]:</color> <color=red>Blocked message from {0} (ID: {1}) - Total blocked: {2}</color>", playerName, senderId, num3);
				}
				else
				{
					txt = string.Format("<color=#31BCF0>[ChatBox]:</color> <color=red>Blocked {0} messages from {1} (ID: {2}) - Total blocked: {3}</color>", new object[]
					{
						num4,
						playerName,
						senderId,
						num3
					});
				}
			}
			InternalConsole.LogIntoConsole(txt, "<color=#8d142b>[Log]</color>", "8d142b");
			ChatboxLogger.lastLogTime[senderId] = now;
			ChatboxLogger.sessionBlockedCount[senderId] = 0;
		}
		bool flag9 = Random.Range(0, 100) < 5;
		if (flag9)
		{
			ChatboxLogger.CleanupOldEntries();
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003068 File Offset: 0x00001268
	public static string PrintByteArray(byte[] bytes)
	{
		StringBuilder stringBuilder = new StringBuilder("");
		foreach (byte b in bytes)
		{
			stringBuilder.Append(b.ToString("X2")).Append(" ");
		}
		return stringBuilder.ToString().TrimEnd(Array.Empty<char>());
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000030CC File Offset: 0x000012CC
	public static string ConvertBytesToText(byte[] bytes)
	{
		string result;
		try
		{
			string text = Encoding.UTF8.GetString(bytes).TrimEnd(new char[1]);
			text = new string((from c in text
			where !char.IsControl(c) || char.IsWhiteSpace(c)
			select c).ToArray<char>());
			bool flag = string.IsNullOrWhiteSpace(text);
			if (flag)
			{
				result = "[Empty or whitespace-only message]";
			}
			else
			{
				result = text;
			}
		}
		catch (Exception ex)
		{
			result = "[Invalid Encoding: " + ex.Message + "]";
		}
		return result;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003164 File Offset: 0x00001364
	public static void CleanupOldEntries()
	{
		DateTime now = DateTime.Now;
		List<int> list = (from kvp in ChatboxLogger.lastLogTime
		where (now - kvp.Value).TotalMinutes > 5.0
		select kvp.Key).ToList<int>();
		foreach (int key in list)
		{
			ChatboxLogger.lastLogTime.Remove(key);
			ChatboxLogger.blockedCount.Remove(key);
			ChatboxLogger.sessionBlockedCount.Remove(key);
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00003228 File Offset: 0x00001428
	public static void ResetCounts()
	{
		ChatboxLogger.lastLogTime.Clear();
		ChatboxLogger.blockedCount.Clear();
		ChatboxLogger.sessionBlockedCount.Clear();
	}

	// Token: 0x0400001A RID: 26
	private static readonly Dictionary<int, DateTime> lastLogTime = new Dictionary<int, DateTime>();

	// Token: 0x0400001B RID: 27
	private static readonly Dictionary<int, int> blockedCount = new Dictionary<int, int>();

	// Token: 0x0400001C RID: 28
	private static readonly Dictionary<int, int> sessionBlockedCount = new Dictionary<int, int>();

	// Token: 0x0400001D RID: 29
	private static readonly TimeSpan logCooldown = TimeSpan.FromSeconds(30.0);
}

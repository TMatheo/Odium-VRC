using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Odium;

// Token: 0x02000009 RID: 9
public static class ChatboxAntis
{
	// Token: 0x0600002F RID: 47 RVA: 0x00002AB8 File Offset: 0x00000CB8
	public static bool IsMessageValid(string message, int senderId = -1)
	{
		bool flag = string.IsNullOrWhiteSpace(message);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = message.Length > 200;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = message.Length < 2;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = UnicodeValidator.Sanitize(message);
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = ChatboxAntis.ContainsBlockedWords(message);
						if (flag5)
						{
							result = false;
						}
						else
						{
							bool flag6 = senderId != -1 && !ChatboxAntis.PassesRateLimit(senderId);
							if (flag6)
							{
								result = false;
							}
							else
							{
								bool flag7 = ChatboxAntis.IsRepeatedCharacterSpam(message);
								result = !flag7;
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002B50 File Offset: 0x00000D50
	private static bool ContainsBlockedWords(string message)
	{
		foreach (string text in ChatboxAntis.blockedWords)
		{
			bool flag = Regex.IsMatch(message, "\\b" + Regex.Escape(text) + "\\b", RegexOptions.IgnoreCase);
			if (flag)
			{
				OdiumConsole.Log("ChatBox", string.Concat(new string[]
				{
					"Found blocked word: '",
					text,
					"' in message: '",
					message,
					"'"
				}), LogLevel.Debug);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002C04 File Offset: 0x00000E04
	private static bool PassesRateLimit(int senderId)
	{
		DateTime now = DateTime.Now;
		List<int> list = (from kvp in ChatboxAntis.lastMessageTime
		where (now - kvp.Value).TotalMinutes > 1.0
		select kvp.Key).ToList<int>();
		foreach (int key in list)
		{
			ChatboxAntis.lastMessageTime.Remove(key);
			ChatboxAntis.messageCount.Remove(key);
		}
		bool flag = ChatboxAntis.lastMessageTime.ContainsKey(senderId);
		if (flag)
		{
			bool flag2 = (now - ChatboxAntis.lastMessageTime[senderId]).TotalSeconds < 2.0;
			if (flag2)
			{
				OdiumConsole.Log("ChatBox", string.Format("Rate limit: Too fast (User {0})", senderId), LogLevel.Debug);
				return false;
			}
			bool flag3 = ChatboxAntis.messageCount.ContainsKey(senderId) && ChatboxAntis.messageCount[senderId] >= 10;
			if (flag3)
			{
				OdiumConsole.Log("ChatBox", string.Format("Rate limit: Too many messages (User {0})", senderId), LogLevel.Debug);
				return false;
			}
		}
		ChatboxAntis.lastMessageTime[senderId] = now;
		ChatboxAntis.messageCount[senderId] = (ChatboxAntis.messageCount.ContainsKey(senderId) ? (ChatboxAntis.messageCount[senderId] + 1) : 1);
		return true;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002DB0 File Offset: 0x00000FB0
	private static bool IsRepeatedCharacterSpam(string message)
	{
		for (int i = 0; i < message.Length - 4; i++)
		{
			char c = message[i];
			int num = 1;
			int num2 = i + 1;
			while (num2 < message.Length && num2 < i + 10)
			{
				bool flag = message[num2] == c;
				if (!flag)
				{
					break;
				}
				num++;
				num2++;
			}
			bool flag2 = num > 5;
			if (flag2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000017 RID: 23
	private static readonly HashSet<string> blockedWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
	{
		"spam",
		"crash",
		"lag",
		"nigger",
		"faggot",
		"gang"
	};

	// Token: 0x04000018 RID: 24
	private static readonly Dictionary<int, DateTime> lastMessageTime = new Dictionary<int, DateTime>();

	// Token: 0x04000019 RID: 25
	private static readonly Dictionary<int, int> messageCount = new Dictionary<int, int>();
}

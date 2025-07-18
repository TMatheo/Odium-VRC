using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Il2CppSystem;

namespace Odium.Components
{
	// Token: 0x0200006A RID: 106
	internal class Ratelimit
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x00018934 File Offset: 0x00016B34
		public static void ProcessRateLimit(ref EventData eventData)
		{
			bool flag = eventData.Code != 34;
			if (!flag)
			{
				Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
				Dictionary<byte, int> dictionary2 = new Dictionary<byte, int>();
				foreach (byte key in Ratelimit.rateLimitedEvents)
				{
					dictionary2.Add(key, int.MaxValue);
				}
				dictionary.Add(0, dictionary2);
				dictionary.Add(2, true);
				eventData.customData = dictionary.FromManagedToIL2CPP<Object>();
			}
		}

		// Token: 0x0400016D RID: 365
		private static List<byte> rateLimitedEvents = new List<byte>
		{
			1,
			33,
			40,
			42,
			43,
			44,
			50,
			52,
			53,
			60,
			4,
			5,
			6,
			8,
			16,
			17,
			18,
			7,
			19,
			12,
			11,
			13,
			14,
			15,
			202,
			209,
			210,
			21,
			22,
			62,
			63,
			64,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			28,
			29,
			30
		};
	}
}

using System;
using System.Collections;
using MelonLoader;

namespace Odium.Components
{
	// Token: 0x02000071 RID: 113
	public class USpeakSpam
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0001AD84 File Offset: 0x00018F84
		public static void ToggleUSpeakSpam(bool state)
		{
			USpeakSpam.isEnabled = state;
			if (state)
			{
				MelonCoroutines.Start(USpeakSpam.USpeakSpamLoop());
			}
			else
			{
				MelonCoroutines.Stop(USpeakSpam.USpeakSpamLoop());
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001ADB9 File Offset: 0x00018FB9
		public static IEnumerator USpeakSpamLoop()
		{
			return new USpeakSpam.<USpeakSpamLoop>d__2(0);
		}

		// Token: 0x04000190 RID: 400
		public static bool isEnabled;
	}
}

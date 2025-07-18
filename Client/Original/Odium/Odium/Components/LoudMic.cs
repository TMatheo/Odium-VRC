using System;

namespace Odium.Components
{
	// Token: 0x0200005E RID: 94
	public class LoudMic
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00015F90 File Offset: 0x00014190
		public static void Activated(bool state)
		{
			if (state)
			{
				USpeaker.field_Internal_Static_Single_1 = float.MaxValue;
			}
			else
			{
				USpeaker.field_Internal_Static_Single_1 = 1f;
			}
		}
	}
}

using System;

namespace Odium.Components
{
	// Token: 0x02000072 RID: 114
	internal class VRCatReplacer
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0001ADCA File Offset: 0x00018FCA
		private void OnQuickMenuInit()
		{
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001ADD0 File Offset: 0x00018FD0
		private bool IsInEarmuffMode()
		{
			return EarmuffsVisualAide.field_Public_Static_EarmuffsVisualAide_0.field_Private_Boolean_0;
		}
	}
}

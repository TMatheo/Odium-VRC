using System;
using VRC.Localization;

namespace Odium.Wrappers
{
	// Token: 0x02000044 RID: 68
	internal class LocalizableStringWrapper
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public static LocalizableString Create(string str)
		{
			return new LocalizableString
			{
				_localizationKey = str
			};
		}
	}
}

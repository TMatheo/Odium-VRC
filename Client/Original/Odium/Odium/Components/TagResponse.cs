using System;
using System.Collections.Generic;

namespace Odium.Components
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class TagResponse
	{
		// Token: 0x04000141 RID: 321
		public bool success;

		// Token: 0x04000142 RID: 322
		public string userId;

		// Token: 0x04000143 RID: 323
		public List<string> tags;
	}
}

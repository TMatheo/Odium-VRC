using System;

namespace Odium.Odium
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public class BanCheckResponse
	{
		// Token: 0x040000A9 RID: 169
		public bool success;

		// Token: 0x040000AA RID: 170
		public string message;

		// Token: 0x040000AB RID: 171
		public bool isBanned;

		// Token: 0x040000AC RID: 172
		public UserInfo user;
	}
}

using System;

namespace Odium.ApplicationBot
{
	// Token: 0x0200008E RID: 142
	public class BotResponse
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0002128C File Offset: 0x0001F48C
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00021294 File Offset: 0x0001F494
		public string MessageId { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0002129D File Offset: 0x0001F49D
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x000212A5 File Offset: 0x0001F4A5
		public string BotId { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000212AE File Offset: 0x0001F4AE
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x000212B6 File Offset: 0x0001F4B6
		public bool Success { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000212BF File Offset: 0x0001F4BF
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x000212C7 File Offset: 0x0001F4C7
		public string Error { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000212D0 File Offset: 0x0001F4D0
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x000212D8 File Offset: 0x0001F4D8
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	}
}

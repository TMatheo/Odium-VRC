using System;

namespace Odium.ApplicationBot
{
	// Token: 0x0200008D RID: 141
	public class BotMessage
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000211FE File Offset: 0x0001F3FE
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x00021206 File Offset: 0x0001F406
		public string MessageId { get; set; } = Guid.NewGuid().ToString();

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0002120F File Offset: 0x0001F40F
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00021217 File Offset: 0x0001F417
		public string Command { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00021220 File Offset: 0x0001F420
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00021228 File Offset: 0x0001F428
		public string TargetBotId { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00021231 File Offset: 0x0001F431
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00021239 File Offset: 0x0001F439
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00021242 File Offset: 0x0001F442
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0002124A File Offset: 0x0001F44A
		public object Parameters { get; set; }
	}
}

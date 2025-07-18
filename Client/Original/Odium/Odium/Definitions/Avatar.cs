using System;
using Newtonsoft.Json;

namespace Odium.Definitions
{
	// Token: 0x0200003E RID: 62
	public class Avatar
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000DF6E File Offset: 0x0000C16E
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000DF76 File Offset: 0x0000C176
		[JsonProperty("avatarName")]
		public string AvatarName { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000DF7F File Offset: 0x0000C17F
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000DF87 File Offset: 0x0000C187
		[JsonProperty("avatarId")]
		public string AvatarId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000DF90 File Offset: 0x0000C190
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000DF98 File Offset: 0x0000C198
		[JsonProperty("vrca")]
		public string Vrca { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000DFA1 File Offset: 0x0000C1A1
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000DFA9 File Offset: 0x0000C1A9
		[JsonProperty("authorName")]
		public string AuthorName { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000DFB2 File Offset: 0x0000C1B2
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000DFBA File Offset: 0x0000C1BA
		[JsonProperty("thumbnailUrl")]
		public string ThumbnailUrl { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000DFC3 File Offset: 0x0000C1C3
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000DFCB File Offset: 0x0000C1CB
		[JsonProperty("wearer")]
		public string Wearer { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		[JsonProperty("stealer")]
		public string Stealer { get; set; }
	}
}

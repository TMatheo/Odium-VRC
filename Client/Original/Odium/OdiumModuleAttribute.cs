using System;

namespace OdiumLoader
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Class)]
	public class OdiumModuleAttribute : Attribute
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005224 File Offset: 0x00003424
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000522C File Offset: 0x0000342C
		public string Name { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00005235 File Offset: 0x00003435
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000523D File Offset: 0x0000343D
		public string Version { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00005246 File Offset: 0x00003446
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000524E File Offset: 0x0000344E
		public string Author { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005257 File Offset: 0x00003457
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000525F File Offset: 0x0000345F
		public string Description { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005268 File Offset: 0x00003468
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005270 File Offset: 0x00003470
		public bool AutoLoad { get; set; } = true;

		// Token: 0x06000099 RID: 153 RVA: 0x00005279 File Offset: 0x00003479
		public OdiumModuleAttribute(string name, string version = "1.0.0", string author = "Unknown")
		{
			this.Name = name;
			this.Version = version;
			this.Author = author;
		}
	}
}

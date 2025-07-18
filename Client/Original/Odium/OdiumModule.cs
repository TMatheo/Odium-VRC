using System;
using MelonLoader;

namespace OdiumLoader
{
	// Token: 0x02000014 RID: 20
	public abstract class OdiumModule
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000045D8 File Offset: 0x000027D8
		public virtual void OnModuleLoad()
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000045DB File Offset: 0x000027DB
		public virtual void OnApplicationStart()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000045DE File Offset: 0x000027DE
		public virtual void OnUpdate()
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000045E1 File Offset: 0x000027E1
		public virtual void OnFixedUpdate()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000045E4 File Offset: 0x000027E4
		public virtual void OnLateUpdate()
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000045E7 File Offset: 0x000027E7
		public virtual void OnApplicationQuit()
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000045EA File Offset: 0x000027EA
		public virtual void OnSceneLoaded(int buildIndex, string sceneName)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000045ED File Offset: 0x000027ED
		public virtual void OnSceneUnloaded(int buildIndex, string sceneName)
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000045F0 File Offset: 0x000027F0
		public virtual string ModuleName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000045FD File Offset: 0x000027FD
		public virtual string ModuleVersion
		{
			get
			{
				return "1.0.0";
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004604 File Offset: 0x00002804
		public virtual string ModuleAuthor
		{
			get
			{
				return "Unknown";
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000460B File Offset: 0x0000280B
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00004613 File Offset: 0x00002813
		public bool IsEnabled { get; set; } = true;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000461C File Offset: 0x0000281C
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00004624 File Offset: 0x00002824
		private protected MelonLogger.Instance Logger { protected get; private set; }

		// Token: 0x0600007B RID: 123 RVA: 0x0000462D File Offset: 0x0000282D
		internal void SetLogger(MelonLogger.Instance logger)
		{
			this.Logger = logger;
		}
	}
}

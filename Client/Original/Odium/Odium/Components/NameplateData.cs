using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Odium.Components
{
	// Token: 0x02000062 RID: 98
	public struct NameplateData
	{
		// Token: 0x04000139 RID: 313
		public string userId;

		// Token: 0x0400013A RID: 314
		public List<TextMeshProUGUI> statsComponents;

		// Token: 0x0400013B RID: 315
		public List<Transform> tagPlates;

		// Token: 0x0400013C RID: 316
		public float lastSeen;

		// Token: 0x0400013D RID: 317
		public Vector3 lastPosition;

		// Token: 0x0400013E RID: 318
		public string platform;

		// Token: 0x0400013F RID: 319
		public List<string> userTags;

		// Token: 0x04000140 RID: 320
		public bool isClientUser;
	}
}

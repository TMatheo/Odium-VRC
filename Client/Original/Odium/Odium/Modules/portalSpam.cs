using System;
using Odium.Wrappers;
using UnityEngine;

namespace Odium.Modules
{
	// Token: 0x02000050 RID: 80
	public class portalSpam
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00013480 File Offset: 0x00011680
		public static void OnUpdate()
		{
			bool flag = ActionWrapper.portalSpamPlayer != null && ActionWrapper.portalSpam;
			if (flag)
			{
				DateTime now = DateTime.Now;
				bool flag2 = (now - portalSpam.LastPortalSpawn).TotalMilliseconds >= 1.0;
				if (flag2)
				{
					portalSpam.LastPortalSpawn = now;
					Vector3 bonePosition = PlayerWrapper.GetBonePosition(ActionWrapper.portalSpamPlayer, 10);
					bonePosition.y -= 2f;
					Portal.SpawnPortal(bonePosition, "gghzak9f");
				}
			}
		}

		// Token: 0x04000101 RID: 257
		public static DateTime LastPortalSpawn = DateTime.Now;
	}
}

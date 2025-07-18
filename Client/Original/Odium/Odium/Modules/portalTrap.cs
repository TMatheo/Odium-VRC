using System;
using Odium.Wrappers;
using UnityEngine;

namespace Odium.Modules
{
	// Token: 0x02000051 RID: 81
	public class portalTrap
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0001351C File Offset: 0x0001171C
		public static void OnUpdate()
		{
			bool flag = ActionWrapper.portalTrapPlayer != null && ActionWrapper.portalTrap;
			if (flag)
			{
				DateTime now = DateTime.Now;
				bool flag2 = (now - portalTrap.LastPortalSpawn).TotalMilliseconds >= 500.0;
				if (flag2)
				{
					portalTrap.LastPortalSpawn = now;
					Vector3 velocity = PlayerWrapper.GetVelocity(ActionWrapper.portalTrapPlayer);
					float magnitude = velocity.magnitude;
					bool flag3 = magnitude > 2.5f;
					if (flag3)
					{
						portalTrap.LastPortalSpawn = now;
						Vector3 normalized = velocity.normalized;
						Vector3 position = PlayerWrapper.GetPosition(ActionWrapper.portalTrapPlayer);
						Vector3 positon = position + normalized * 3f;
						Portal.SpawnPortal(positon, "aywxh5ah");
					}
				}
			}
		}

		// Token: 0x04000102 RID: 258
		public static DateTime LastPortalSpawn = DateTime.Now;
	}
}

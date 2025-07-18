using System;
using UnityEngine;
using VRC.Udon;

namespace Odium.GameCheats
{
	// Token: 0x0200003C RID: 60
	internal class FTACUdonUtils
	{
		// Token: 0x0600015A RID: 346 RVA: 0x0000D9CA File Offset: 0x0000BBCA
		public static void SendEvent(string eventName)
		{
			GameObject gameObject = GameObject.Find("Partner Button  (4)");
			if (gameObject != null)
			{
				UdonBehaviour component = gameObject.GetComponent<UdonBehaviour>();
				if (component != null)
				{
					component.SendCustomNetworkEvent(0, eventName);
				}
			}
		}
	}
}

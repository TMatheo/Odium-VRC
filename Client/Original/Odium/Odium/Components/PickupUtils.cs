using System;
using UnityEngine;
using VRC.SDKBase;

namespace Odium.Components
{
	// Token: 0x02000067 RID: 103
	public class PickupUtils
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0001806C File Offset: 0x0001626C
		public static void TakeOwnerShipPickup(VRC_Pickup pickup)
		{
			bool flag = pickup == null;
			bool flag2 = !flag;
			if (flag2)
			{
				Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000180A0 File Offset: 0x000162A0
		public static void Respawn()
		{
			foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
			{
				Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
				vrc_Pickup.transform.localPosition = new Vector3(0f, -100000f, 0f);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001811C File Offset: 0x0001631C
		public static void BringPickups()
		{
			foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
			{
				Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
				vrc_Pickup.transform.position = Networking.LocalPlayer.gameObject.transform.position;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00018198 File Offset: 0x00016398
		public static void rotateobjse()
		{
			PickupUtils.rotationAngle += 45f;
			bool flag = PickupUtils.rotationAngle >= 360f;
			if (flag)
			{
				PickupUtils.rotationAngle -= 360f;
			}
			foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
			{
				Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
				vrc_Pickup.transform.rotation = Quaternion.Euler(0f, PickupUtils.rotationAngle, 0f);
			}
		}

		// Token: 0x04000167 RID: 359
		public static VRC_Pickup[] array;

		// Token: 0x04000168 RID: 360
		public static float rotationAngle;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace Odium.Wrappers
{
	// Token: 0x02000045 RID: 69
	internal class PickupWrapper
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public static List<VRCPickup> GetVRCPickups()
		{
			List<VRCPickup> pickups = new List<VRCPickup>();
			Object.FindObjectsOfType<VRCPickup>().ToList<VRCPickup>().ForEach(delegate(VRCPickup pickup)
			{
				bool flag = pickup.gameObject != null;
				if (flag)
				{
					pickups.Add(pickup);
				}
			});
			return pickups;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000EF30 File Offset: 0x0000D130
		public static void DropAllPickupsInRange(float range)
		{
			Vector3 position = PlayerWrapper.LocalPlayer.transform.position;
			foreach (VRCPickup vrcpickup in PickupWrapper.GetVRCPickups())
			{
				bool flag = vrcpickup != null && vrcpickup.gameObject != null;
				if (flag)
				{
					float num = Vector3.Distance(position, vrcpickup.transform.position);
					bool flag2 = num <= range;
					if (flag2)
					{
						Networking.SetOwner(Networking.LocalPlayer, vrcpickup.gameObject);
						vrcpickup.Drop();
					}
				}
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		public static void DropDronePickups()
		{
			Vector3 position = PlayerWrapper.LocalPlayer.transform.position;
			foreach (VRCPickup vrcpickup in PickupWrapper.GetVRCPickups())
			{
				bool flag = vrcpickup != null && vrcpickup.gameObject != null && vrcpickup.gameObject.name.Contains("Drone");
				if (flag)
				{
					Networking.SetOwner(Networking.LocalPlayer, vrcpickup.gameObject);
					vrcpickup.Drop();
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000F09C File Offset: 0x0000D29C
		public static void DropAllPickups()
		{
			foreach (VRCPickup vrcpickup in PickupWrapper.GetVRCPickups())
			{
				bool flag = vrcpickup != null && vrcpickup.gameObject != null;
				if (flag)
				{
					Networking.SetOwner(Networking.LocalPlayer, vrcpickup.gameObject);
					vrcpickup.Drop();
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000F124 File Offset: 0x0000D324
		public static void BringAllPickupsToPlayer(Player player)
		{
			Vector3 position = player.transform.position;
			foreach (VRCPickup vrcpickup in PickupWrapper.GetVRCPickups())
			{
				bool flag = vrcpickup != null && vrcpickup.gameObject != null;
				if (flag)
				{
					Networking.SetOwner(Networking.LocalPlayer, vrcpickup.gameObject);
					vrcpickup.transform.position = position + Vector3.up * 0.5f;
				}
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		public static void HideAllPickups()
		{
			foreach (VRCPickup vrcpickup in PickupWrapper.GetVRCPickups())
			{
				bool flag = vrcpickup != null && vrcpickup.gameObject != null;
				if (flag)
				{
					PickupWrapper.cachedPickups.Add(vrcpickup);
					vrcpickup.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000F258 File Offset: 0x0000D458
		public static void ShowAllPickups()
		{
			foreach (VRCPickup vrcpickup in PickupWrapper.GetVRCPickups())
			{
				bool flag = vrcpickup != null && PickupWrapper.cachedPickups != null;
				if (flag)
				{
					vrcpickup.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public static void RespawnAllPickups()
		{
			foreach (VRCPickup vrcpickup in PickupWrapper.GetVRCPickups())
			{
				bool flag = vrcpickup != null && vrcpickup.gameObject != null;
				if (flag)
				{
					Networking.SetOwner(Networking.LocalPlayer, vrcpickup.gameObject);
					vrcpickup.transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				}
			}
		}

		// Token: 0x040000D0 RID: 208
		public static List<VRCPickup> cachedPickups = new List<VRCPickup>();
	}
}

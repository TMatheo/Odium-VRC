using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.PlayerDrone;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace Odium.Wrappers
{
	// Token: 0x02000041 RID: 65
	internal class DroneWrapper
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000E594 File Offset: 0x0000C794
		public static DroneManager GetDroneManager()
		{
			return GameObject.Find("UIManager/DroneManager").GetComponent<DroneManager>();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		public static string GetDroneID(VRCPickup Drone)
		{
			DroneController component = Drone.gameObject.GetComponent<DroneController>();
			return component.field_Private_String_0;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public static void RemoveDrone(string id)
		{
			DroneWrapper.GetDroneManager().Method_Private_Void_String_PDM_0(id);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		public static void SpawnDrone(Vector3 position, Vector3 Rotation)
		{
			VRCPlayer vrcplayer = PlayerWrapper.LocalPlayer._vrcplayer;
			int param_ = vrcplayer.Method_Public_Int32_0();
			vrcplayer.Method_Public_Int32_0();
			DroneWrapper.GetDroneManager().Method_Private_Void_Player_Vector3_Vector3_String_Int32_Color_Color_Color_PDM_0(PlayerWrapper.LocalPlayer, position, Rotation, Guid.NewGuid().ToString(), param_, Color.black, Color.black, Color.black);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000E648 File Offset: 0x0000C848
		public static List<VRCPickup> GetDrones()
		{
			List<VRCPickup> drones = new List<VRCPickup>();
			Object.FindObjectsOfType<VRCPickup>().ToList<VRCPickup>().ForEach(delegate(VRCPickup pickup)
			{
				bool flag = pickup.gameObject.name.Contains("VRCDrone");
				if (flag)
				{
					drones.Add(pickup);
				}
			});
			return drones;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000E690 File Offset: 0x0000C890
		public static void DroneCrash()
		{
			for (int i = 0; i < DroneWrapper.GetDrones().Count; i++)
			{
				Networking.SetOwner(Networking.LocalPlayer, DroneWrapper.GetDrones()[i].gameObject);
				DroneWrapper.GetDrones()[i].gameObject.transform.position = new Vector3(2.222224E+11f, 0f);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000E6FE File Offset: 0x0000C8FE
		public static void SetDronePosition(VRCPickup drone, Vector3 vector3)
		{
			Networking.SetOwner(Networking.LocalPlayer, drone.gameObject);
			drone.gameObject.transform.position = vector3;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000E724 File Offset: 0x0000C924
		public static void SetDroneRotation(VRCPickup drone, Quaternion quaternion)
		{
			Networking.SetOwner(Networking.LocalPlayer, drone.gameObject);
			drone.gameObject.transform.rotation = quaternion;
		}

		// Token: 0x040000C4 RID: 196
		public static DroneManager DroneManager;

		// Token: 0x040000C5 RID: 197
		public static int DroneViewId = PlayerWrapper.ActorId + 10001;
	}
}

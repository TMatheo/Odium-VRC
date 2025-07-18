using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using VRC.SDK3.Components;

namespace Odium.Wrappers
{
	// Token: 0x02000042 RID: 66
	public static class DroneSwarmWrapper
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000E768 File Offset: 0x0000C968
		public static void StartDroneSwarm(GameObject target, float radius = 1.5f, float yOffset = 0.5f)
		{
			bool flag = target == null;
			if (flag)
			{
				MelonLogger.Msg("[DroneSwarm] Target object is null!");
			}
			else
			{
				DroneSwarmWrapper.targetObject = target;
				DroneSwarmWrapper.swarmRadius = radius;
				DroneSwarmWrapper.verticalOffset = yOffset;
				DroneSwarmWrapper.isSwarmActive = true;
				DroneSwarmWrapper.droneOffsets.Clear();
				List<VRCPickup> drones = DroneWrapper.GetDrones();
				foreach (VRCPickup key in drones)
				{
					Vector3 value = Random.onUnitSphere * Random.Range(0.5f, DroneSwarmWrapper.swarmRadius);
					DroneSwarmWrapper.droneOffsets[key] = value;
				}
				MelonLogger.Msg(string.Format("[DroneSwarm] Started swarm with {0} drones around {1} with {2} vertical offset", drones.Count, target.name, yOffset));
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000E84C File Offset: 0x0000CA4C
		public static void StopDroneSwarm()
		{
			DroneSwarmWrapper.isSwarmActive = false;
			DroneSwarmWrapper.targetObject = null;
			MelonLogger.Msg("[DroneSwarm] Stopped swarm");
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000E868 File Offset: 0x0000CA68
		public static void UpdateDroneSwarm()
		{
			bool flag = !DroneSwarmWrapper.isSwarmActive || DroneSwarmWrapper.targetObject == null;
			if (!flag)
			{
				bool flag2 = Time.time - DroneSwarmWrapper.lastUpdateTime < DroneSwarmWrapper.updateInterval;
				if (!flag2)
				{
					DroneSwarmWrapper.lastUpdateTime = Time.time;
					List<VRCPickup> drones = DroneWrapper.GetDrones();
					bool flag3 = drones.Count == 0;
					if (!flag3)
					{
						Vector3 position = DroneSwarmWrapper.targetObject.transform.position;
						position.y += DroneSwarmWrapper.verticalOffset;
						foreach (VRCPickup vrcpickup in drones)
						{
							bool flag4 = vrcpickup == null || vrcpickup.gameObject == null;
							if (!flag4)
							{
								bool flag5 = !DroneSwarmWrapper.droneOffsets.ContainsKey(vrcpickup);
								if (flag5)
								{
									DroneSwarmWrapper.droneOffsets[vrcpickup] = Random.onUnitSphere * Random.Range(0.5f, DroneSwarmWrapper.swarmRadius);
								}
								Vector3 a = position + DroneSwarmWrapper.droneOffsets[vrcpickup];
								Vector3 position2 = vrcpickup.transform.position;
								Vector3 vector = a - position2;
								float magnitude = vector.magnitude;
								bool flag6 = magnitude > 0.01f;
								if (flag6)
								{
									float d = Mathf.Min(DroneSwarmWrapper.maxSpeed, magnitude);
									vector = vector.normalized * d;
								}
								vector += new Vector3((float)(DroneSwarmWrapper.random.NextDouble() - 0.5) * 0.05f, (float)(DroneSwarmWrapper.random.NextDouble() - 0.5) * 0.05f, (float)(DroneSwarmWrapper.random.NextDouble() - 0.5) * 0.05f);
								Vector3 vector2 = position2 + vector;
								foreach (VRCPickup vrcpickup2 in drones)
								{
									bool flag7 = vrcpickup2 == vrcpickup || vrcpickup2 == null;
									if (!flag7)
									{
										float num = Vector3.Distance(vector2, vrcpickup2.transform.position);
										bool flag8 = num < DroneSwarmWrapper.minDistanceBetweenDrones;
										if (flag8)
										{
											Vector3 normalized = (vector2 - vrcpickup2.transform.position).normalized;
											vector2 += normalized * (DroneSwarmWrapper.minDistanceBetweenDrones - num) * 0.5f;
										}
									}
								}
								DroneWrapper.SetDronePosition(vrcpickup, vector2);
								Vector3 vector3 = (position - vector2).normalized;
								vector3 += new Vector3((float)(DroneSwarmWrapper.random.NextDouble() - 0.5) * 0.1f, (float)(DroneSwarmWrapper.random.NextDouble() - 0.5) * 0.1f, (float)(DroneSwarmWrapper.random.NextDouble() - 0.5) * 0.1f);
								Quaternion quaternion = Quaternion.LookRotation(vector3);
								DroneWrapper.SetDroneRotation(vrcpickup, quaternion);
							}
						}
					}
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		public static void ChangeSwarmTarget(GameObject newTarget)
		{
			bool flag = newTarget == null;
			if (flag)
			{
				MelonLogger.Msg("[DroneSwarm] New target object is null!");
			}
			else
			{
				DroneSwarmWrapper.targetObject = newTarget;
				MelonLogger.Msg("[DroneSwarm] Changed swarm target to " + newTarget.name);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public static void AdjustSwarmParameters(float radius = 1.5f, float speed = 0.5f, float minDistance = 0.5f, float yOffset = 0.5f)
		{
			DroneSwarmWrapper.swarmRadius = radius;
			DroneSwarmWrapper.maxSpeed = speed;
			DroneSwarmWrapper.minDistanceBetweenDrones = minDistance;
			DroneSwarmWrapper.verticalOffset = yOffset;
			bool flag = DroneSwarmWrapper.isSwarmActive;
			if (flag)
			{
				List<VRCPickup> drones = DroneWrapper.GetDrones();
				foreach (VRCPickup key in drones)
				{
					DroneSwarmWrapper.droneOffsets[key] = Random.onUnitSphere * Random.Range(0.5f, DroneSwarmWrapper.swarmRadius);
				}
			}
			MelonLogger.Msg(string.Format("[DroneSwarm] Parameters adjusted - Radius: {0}, Speed: {1}, MinDistance: {2}, VerticalOffset: {3}", new object[]
			{
				radius,
				speed,
				minDistance,
				yOffset
			}));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public static void SetVerticalOffset(float yOffset)
		{
			DroneSwarmWrapper.verticalOffset = yOffset;
			MelonLogger.Msg(string.Format("[DroneSwarm] Vertical offset set to: {0}", yOffset));
		}

		// Token: 0x040000C6 RID: 198
		public static bool isSwarmActive = false;

		// Token: 0x040000C7 RID: 199
		private static GameObject targetObject = null;

		// Token: 0x040000C8 RID: 200
		private static Random random = new Random();

		// Token: 0x040000C9 RID: 201
		private static Dictionary<VRCPickup, Vector3> droneOffsets = new Dictionary<VRCPickup, Vector3>();

		// Token: 0x040000CA RID: 202
		private static float updateInterval = 0.1f;

		// Token: 0x040000CB RID: 203
		private static float lastUpdateTime = 0f;

		// Token: 0x040000CC RID: 204
		private static float swarmRadius = 1.5f;

		// Token: 0x040000CD RID: 205
		private static float maxSpeed = 0.5f;

		// Token: 0x040000CE RID: 206
		private static float minDistanceBetweenDrones = 0.5f;

		// Token: 0x040000CF RID: 207
		private static float verticalOffset = 0.5f;
	}
}

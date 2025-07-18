using System;
using UnityEngine;
using VRC.SDKBase;

namespace Odium.Components
{
	// Token: 0x0200006E RID: 110
	public class SpyCamera : MonoBehaviour
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00019340 File Offset: 0x00017540
		public void Awake()
		{
			bool flag = SpyCamera.Instance == null;
			if (flag)
			{
				SpyCamera.Instance = this;
				Object.DontDestroyOnLoad(base.gameObject);
			}
			else
			{
				Object.DestroyImmediate(this);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001937C File Offset: 0x0001757C
		public static void Toggle(VRCPlayerApi player, bool state)
		{
			if (state)
			{
				SpyCamera.EnableSpyCamera(player);
			}
			else
			{
				SpyCamera.DisableSpyCamera();
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000193A4 File Offset: 0x000175A4
		private static void EnableSpyCamera(VRCPlayerApi player)
		{
			bool flag = SpyCamera._isActive || player == null;
			if (!flag)
			{
				SpyCamera._targetPlayer = player;
				GameObject gameObject = new GameObject("SpyCamera");
				gameObject.transform.SetParent(SpyCamera._targetPlayer.GetBoneTransform(10));
				SpyCamera._spyCam = gameObject.AddComponent<Camera>();
				SpyCamera._spyCam.fieldOfView = 60f;
				SpyCamera._spyCam.nearClipPlane = 0.1f;
				SpyCamera._spyCam.farClipPlane = 1000f;
				SpyCamera._spyCam.depth = 1f;
				SpyCamera._isActive = true;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00019440 File Offset: 0x00017640
		private static void DisableSpyCamera()
		{
			bool flag = !SpyCamera._isActive;
			if (!flag)
			{
				bool flag2 = SpyCamera._spyCam != null;
				if (flag2)
				{
					Object.Destroy(SpyCamera._spyCam.gameObject);
					SpyCamera._spyCam = null;
				}
				SpyCamera._targetPlayer = null;
				SpyCamera._isActive = false;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00019490 File Offset: 0x00017690
		public static void LateUpdate()
		{
			bool flag = !SpyCamera._isActive || SpyCamera._targetPlayer == null || !SpyCamera._targetPlayer.IsValid();
			if (!flag)
			{
				VRCPlayerApi.TrackingData trackingData = SpyCamera._targetPlayer.GetTrackingData(0);
				bool flag2 = SpyCamera._spyCam != null;
				if (flag2)
				{
					SpyCamera._spyCam.transform.position = trackingData.position;
					SpyCamera._spyCam.transform.rotation = trackingData.rotation;
				}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001950B File Offset: 0x0001770B
		public static void OnDestroy()
		{
			SpyCamera.DisableSpyCamera();
		}

		// Token: 0x0400016E RID: 366
		public static SpyCamera Instance;

		// Token: 0x0400016F RID: 367
		public static Camera _spyCam;

		// Token: 0x04000170 RID: 368
		public static VRCPlayerApi _targetPlayer;

		// Token: 0x04000171 RID: 369
		public static bool _isActive;
	}
}

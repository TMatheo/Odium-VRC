using System;
using Odium.Wrappers;
using UnityEngine;

namespace Odium.Modules
{
	// Token: 0x0200004B RID: 75
	public class FlyComponent
	{
		// Token: 0x060001DC RID: 476 RVA: 0x000104DC File Offset: 0x0000E6DC
		public static void OnUpdate()
		{
			DateTime now = DateTime.Now;
			bool flag = (now - FlyComponent.LastKeyCheck).TotalMilliseconds >= 10.0;
			if (flag)
			{
				bool flag2 = Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.LeftControl);
				if (flag2)
				{
					FlyComponent.ToggleFly();
				}
				FlyComponent.LastKeyCheck = now;
			}
			bool flag3 = PlayerWrapper.LocalPlayer == null || PlayerWrapper.LocalPlayer.field_Private_VRCPlayerApi_0 == null;
			if (!flag3)
			{
				bool flag4 = !FlyComponent.FlyEnabled;
				if (flag4)
				{
					FlyComponent.DisableFly();
				}
				else
				{
					FlyComponent.EnableFly();
					FlyComponent.HandleMovement();
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00010588 File Offset: 0x0000E788
		private static void ToggleFly()
		{
			FlyComponent.FlyEnabled = !FlyComponent.FlyEnabled;
			bool flag = !FlyComponent.FlyEnabled;
			if (flag)
			{
				FlyComponent.DisableFly();
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000105B8 File Offset: 0x0000E7B8
		private static void EnableFly()
		{
			bool flag = !FlyComponent.setupedNormalFly;
			if (!flag)
			{
				FlyComponent.cachedGravity = PlayerWrapper.LocalPlayer.field_Private_VRCPlayerApi_0.GetGravityStrength();
				PlayerWrapper.LocalPlayer.field_Private_VRCPlayerApi_0.SetGravityStrength(0f);
				Collider component = PlayerWrapper.LocalPlayer.gameObject.GetComponent<Collider>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.enabled = false;
				}
				FlyComponent.setupedNormalFly = false;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00010628 File Offset: 0x0000E828
		private static void DisableFly()
		{
			bool flag = FlyComponent.setupedNormalFly;
			if (!flag)
			{
				PlayerWrapper.LocalPlayer.field_Private_VRCPlayerApi_0.SetGravityStrength(FlyComponent.cachedGravity);
				GameObject gameObject = PlayerWrapper.LocalPlayer.gameObject;
				Collider collider = (gameObject != null) ? gameObject.GetComponent<Collider>() : null;
				bool flag2 = collider != null;
				if (flag2)
				{
					collider.enabled = true;
				}
				FlyComponent.setupedNormalFly = true;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00010688 File Offset: 0x0000E888
		private static void HandleMovement()
		{
			Transform transform = PlayerWrapper.LocalPlayer.gameObject.transform;
			Camera main = Camera.main;
			Transform transform2 = (main != null) ? main.transform : null;
			bool flag = transform2 == null;
			if (!flag)
			{
				float num = FlyComponent.FlySpeed;
				bool key = Input.GetKey(KeyCode.LeftShift);
				if (key)
				{
					num *= 2f;
				}
				Vector3 vector = Vector3.zero;
				float axis = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical");
				float axis2 = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickHorizontal");
				float axis3 = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical");
				Vector3 forward = transform2.forward;
				forward.y = 0f;
				forward.Normalize();
				Vector3 right = transform2.right;
				right.y = 0f;
				right.Normalize();
				bool flag2 = axis != 0f;
				if (flag2)
				{
					vector += Vector3.up * axis * num * Time.deltaTime;
				}
				bool flag3 = axis2 != 0f;
				if (flag3)
				{
					vector += right * axis2 * num * Time.deltaTime;
				}
				bool flag4 = axis3 != 0f;
				if (flag4)
				{
					vector += forward * axis3 * num * Time.deltaTime;
				}
				bool key2 = Input.GetKey(KeyCode.Q);
				if (key2)
				{
					vector += Vector3.down * num * Time.deltaTime;
				}
				bool key3 = Input.GetKey(KeyCode.E);
				if (key3)
				{
					vector += Vector3.up * num * Time.deltaTime;
				}
				bool key4 = Input.GetKey(KeyCode.A);
				if (key4)
				{
					vector += -right * num * Time.deltaTime;
				}
				bool key5 = Input.GetKey(KeyCode.D);
				if (key5)
				{
					vector += right * num * Time.deltaTime;
				}
				bool key6 = Input.GetKey(KeyCode.S);
				if (key6)
				{
					vector += -forward * num * Time.deltaTime;
				}
				bool key7 = Input.GetKey(KeyCode.W);
				if (key7)
				{
					vector += forward * num * Time.deltaTime;
				}
				transform.position += vector;
			}
		}

		// Token: 0x040000E7 RID: 231
		private static bool setupedNormalFly = true;

		// Token: 0x040000E8 RID: 232
		private static float cachedGravity = 0f;

		// Token: 0x040000E9 RID: 233
		public static bool FlyEnabled = false;

		// Token: 0x040000EA RID: 234
		public static float FlySpeed = 5f;

		// Token: 0x040000EB RID: 235
		public static DateTime LastKeyCheck = DateTime.Now;
	}
}

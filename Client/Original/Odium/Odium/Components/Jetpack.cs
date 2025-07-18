using System;
using MelonLoader;
using UnityEngine;
using VRC.SDKBase;

namespace Odium.Components
{
	// Token: 0x0200005D RID: 93
	public static class Jetpack
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00015E62 File Offset: 0x00014062
		public static bool IsEnabled
		{
			get
			{
				return Jetpack.jetpackEnabled;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00015E6C File Offset: 0x0001406C
		public static void Activate(bool state)
		{
			Jetpack.jetpackEnabled = state;
			if (state)
			{
				MelonLogger.Msg("Jetpack ON");
			}
			else
			{
				MelonLogger.Msg("Jetpack OFF");
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00015EA4 File Offset: 0x000140A4
		public static void Update()
		{
			bool flag = !Jetpack.jetpackEnabled || Networking.LocalPlayer == null;
			if (!flag)
			{
				bool flag2 = Input.GetKey(KeyCode.Space);
				try
				{
					bool flag3 = Bindings.Button_Jump != null;
					if (flag3)
					{
						flag2 = (flag2 || Bindings.Button_Jump.GetState(0));
					}
				}
				catch
				{
				}
				bool flag4 = flag2;
				if (flag4)
				{
					Jetpack.ApplyJetpack();
				}
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00015F1C File Offset: 0x0001411C
		private static void ApplyJetpack()
		{
			try
			{
				VRCPlayerApi localPlayer = Networking.LocalPlayer;
				Vector3 velocity = localPlayer.GetVelocity();
				velocity.y = localPlayer.GetJumpImpulse();
				localPlayer.SetVelocity(velocity);
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Jetpack error: " + ex.Message);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00015F7C File Offset: 0x0001417C
		public static void Toggle()
		{
			Jetpack.Activate(!Jetpack.jetpackEnabled);
		}

		// Token: 0x04000126 RID: 294
		private static bool jetpackEnabled;
	}
}

using System;
using Odium.Wrappers;
using OdiumLoader;
using UnityEngine;
using VRC;

namespace Odium.Modules
{
	// Token: 0x02000052 RID: 82
	public class SpinBotModule : OdiumModule
	{
		// Token: 0x06000219 RID: 537 RVA: 0x000135F4 File Offset: 0x000117F4
		public override void OnUpdate()
		{
			bool flag = !SpinBotModule._isActive;
			if (!flag)
			{
				SpinBotModule.EnsureAvatarTransform();
				SpinBotModule.RotateAvatar();
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001361C File Offset: 0x0001181C
		public static void Toggle()
		{
			SpinBotModule.SetActive(!SpinBotModule._isActive);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001362C File Offset: 0x0001182C
		public static void SetSpeed(float speed)
		{
			SpinBotModule._rotationSpeed = Mathf.Clamp(speed, 0f, 2000f);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00013644 File Offset: 0x00011844
		public static void SetActive(bool state)
		{
			SpinBotModule._isActive = state;
			bool flag = state && SpinBotModule._avatarTransform == null;
			if (flag)
			{
				SpinBotModule.CacheAvatarTransform();
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00013678 File Offset: 0x00011878
		private static void EnsureAvatarTransform()
		{
			bool flag = SpinBotModule._avatarTransform == null;
			if (flag)
			{
				SpinBotModule.CacheAvatarTransform();
				bool flag2 = SpinBotModule._avatarTransform == null;
				if (flag2)
				{
					SpinBotModule.SetActive(false);
					Debug.LogWarning("[SpinBot] Failed to locate avatar transform - disabling");
				}
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000136C8 File Offset: 0x000118C8
		private static void RotateAvatar()
		{
			bool flag = SpinBotModule._avatarTransform != null;
			if (flag)
			{
				SpinBotModule._avatarTransform.Rotate(Vector3.up, SpinBotModule._rotationSpeed * Time.deltaTime);
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00013704 File Offset: 0x00011904
		private static void CacheAvatarTransform()
		{
			Player localPlayer = PlayerWrapper.LocalPlayer;
			bool flag = localPlayer == null;
			if (!flag)
			{
				Animator componentInChildren = localPlayer.GetComponentInChildren<Animator>();
				bool flag2 = componentInChildren != null;
				if (flag2)
				{
					SpinBotModule._avatarTransform = componentInChildren.transform;
				}
				else
				{
					SpinBotModule._avatarTransform = (localPlayer.transform.Find("Avatar") ?? localPlayer.transform.Find("avatar"));
				}
			}
		}

		// Token: 0x04000103 RID: 259
		private const string DefaultAvatarObjectName = "Avatar";

		// Token: 0x04000104 RID: 260
		private const string FallbackAvatarObjectName = "avatar";

		// Token: 0x04000105 RID: 261
		private static Transform _avatarTransform;

		// Token: 0x04000106 RID: 262
		private static float _rotationSpeed = 500f;

		// Token: 0x04000107 RID: 263
		private static bool _isActive;
	}
}

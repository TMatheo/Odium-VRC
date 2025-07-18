using System;
using System.Collections.Generic;
using Odium.Wrappers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Odium.Components
{
	// Token: 0x0200005A RID: 90
	public class GhostMode
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000152CC File Offset: 0x000134CC
		public static void ToggleGhost(bool enable)
		{
			ActionWrapper.serialize = enable;
			GhostMode.isEnabled = enable;
			if (enable)
			{
				GameObject gameObject = null;
				foreach (GameObject gameObject2 in SceneManager.GetActiveScene().GetRootGameObjects())
				{
					bool flag = gameObject2.name.StartsWith("VRCPlayer[Local]");
					bool flag2 = flag;
					if (flag2)
					{
						gameObject = gameObject2;
						break;
					}
				}
				bool flag3 = gameObject == null;
				bool flag4 = !flag3;
				if (flag4)
				{
					GhostMode.originalGhostPosition = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position;
					GhostMode.originalGhostRotation = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.rotation;
					try
					{
						GhostMode.avatarClone = Object.Instantiate<GameObject>(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCAvatarManager_0.field_Private_GameObject_0, null, true);
						Animator component = GhostMode.avatarClone.GetComponent<Animator>();
						GhostMode.avatarClone.transform.position = GhostMode.originalGhostPosition;
						GhostMode.avatarClone.transform.rotation = GhostMode.originalGhostRotation;
						bool flag5 = component != null && component.isHuman;
						bool flag6 = flag5;
						if (flag6)
						{
							Transform boneTransform = component.GetBoneTransform(10);
							bool flag7 = boneTransform != null;
							bool flag8 = flag7;
							if (flag8)
							{
								boneTransform.localScale = Vector3.one;
							}
						}
						GhostMode.avatarClone.name = "Cloned Avatar";
						component.enabled = false;
						GhostMode.avatarClone.GetComponent<VRCVrIkController>().enabled = false;
						GhostMode.avatarClone.transform.position = gameObject.transform.position;
						GhostMode.avatarClone.transform.rotation = gameObject.transform.rotation;
					}
					catch (Exception ex)
					{
					}
				}
			}
			else
			{
				Object.Destroy(GhostMode.avatarClone);
			}
		}

		// Token: 0x04000121 RID: 289
		public static Vector3 originalGhostPosition;

		// Token: 0x04000122 RID: 290
		public static GameObject avatarClone;

		// Token: 0x04000123 RID: 291
		public static List<GameObject> clonedAvatarObjects = new List<GameObject>();

		// Token: 0x04000124 RID: 292
		public static Quaternion originalGhostRotation;

		// Token: 0x04000125 RID: 293
		public static bool isEnabled = false;
	}
}

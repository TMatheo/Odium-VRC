using System;
using MelonLoader;
using UnityEngine;
using VRC;

namespace Odium.Components
{
	// Token: 0x02000070 RID: 112
	public class ThirdPersonComponent
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00019BBC File Offset: 0x00017DBC
		public static void Initialize()
		{
			bool flag = ThirdPersonComponent.initialized;
			if (!flag)
			{
				try
				{
					ThirdPersonComponent.mainCamera = Camera.main;
					bool flag2 = ThirdPersonComponent.mainCamera == null;
					if (flag2)
					{
						MelonLogger.Error("Main camera not found!");
					}
					else
					{
						ThirdPersonComponent.localPlayer = Player.prop_Player_0;
						bool flag3 = ThirdPersonComponent.localPlayer == null;
						if (flag3)
						{
							MelonLogger.Warning("Local player not found, will retry...");
						}
						else
						{
							ThirdPersonComponent.originalCameraParent = ThirdPersonComponent.mainCamera.transform.parent;
							ThirdPersonComponent.originalCameraPosition = ThirdPersonComponent.mainCamera.transform.localPosition;
							ThirdPersonComponent.originalCameraRotation = ThirdPersonComponent.mainCamera.transform.localRotation;
							ThirdPersonComponent.GetHeadBone();
							ThirdPersonComponent.CreateThirdPersonCamera();
							ThirdPersonComponent.initialized = true;
							MelonLogger.Msg("Third Person Component initialized");
						}
					}
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Failed to initialize Third Person Component: " + ex.Message);
				}
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00019CB4 File Offset: 0x00017EB4
		private static void CreateThirdPersonCamera()
		{
			try
			{
				ThirdPersonComponent.thirdPersonCameraObject = new GameObject("ThirdPersonCamera");
				ThirdPersonComponent.thirdPersonCamera = ThirdPersonComponent.thirdPersonCameraObject.AddComponent<Camera>();
				ThirdPersonComponent.thirdPersonCamera.CopyFrom(ThirdPersonComponent.mainCamera);
				ThirdPersonComponent.thirdPersonCamera.enabled = false;
				Object.DontDestroyOnLoad(ThirdPersonComponent.thirdPersonCameraObject);
				MelonLogger.Msg("Third person camera created");
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Failed to create third person camera: " + ex.Message);
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00019D44 File Offset: 0x00017F44
		private static void GetHeadBone()
		{
			try
			{
				bool flag = ThirdPersonComponent.localPlayer != null;
				if (flag)
				{
					Animator componentInChildren = ThirdPersonComponent.localPlayer.GetComponentInChildren<Animator>();
					bool flag2 = componentInChildren != null && componentInChildren.isHuman;
					if (flag2)
					{
						ThirdPersonComponent.headBone = componentInChildren.GetBoneTransform(10);
						bool flag3 = ThirdPersonComponent.headBone == null;
						if (flag3)
						{
							ThirdPersonComponent.headBone = componentInChildren.GetBoneTransform(9);
						}
						ThirdPersonComponent.chestBone = componentInChildren.GetBoneTransform(8);
						bool flag4 = ThirdPersonComponent.chestBone == null;
						if (flag4)
						{
							ThirdPersonComponent.chestBone = componentInChildren.GetBoneTransform(7);
						}
						bool flag5 = ThirdPersonComponent.chestBone == null;
						if (flag5)
						{
							ThirdPersonComponent.chestBone = componentInChildren.GetBoneTransform(0);
						}
						string str = "Head bone: ";
						Transform transform = ThirdPersonComponent.headBone;
						string str2 = (transform != null) ? transform.name : null;
						string str3 = ", Chest bone: ";
						Transform transform2 = ThirdPersonComponent.chestBone;
						MelonLogger.Msg(str + str2 + str3 + ((transform2 != null) ? transform2.name : null));
						ThirdPersonComponent.FindHeadRenderers();
					}
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error getting bones: " + ex.Message);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00019E70 File Offset: 0x00018070
		private static void FindHeadRenderers()
		{
			try
			{
				bool flag = ThirdPersonComponent.headBone != null;
				if (flag)
				{
					ThirdPersonComponent.headRenderers = ThirdPersonComponent.headBone.GetComponentsInChildren<Renderer>();
					string format = "Found {0} head renderers";
					Renderer[] array = ThirdPersonComponent.headRenderers;
					MelonLogger.Msg(string.Format(format, (array != null) ? array.Length : 0));
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error finding head renderers: " + ex.Message);
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00019EF8 File Offset: 0x000180F8
		private static void SetHeadVisibility(bool visible)
		{
			try
			{
				bool flag = ThirdPersonComponent.headRenderers != null;
				if (flag)
				{
					foreach (Renderer renderer in ThirdPersonComponent.headRenderers)
					{
						bool flag2 = renderer != null;
						if (flag2)
						{
							renderer.enabled = visible;
						}
					}
					ThirdPersonComponent.headVisible = visible;
					MelonLogger.Msg(string.Format("Head visibility set to: {0}", visible));
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error setting head visibility: " + ex.Message);
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00019F94 File Offset: 0x00018194
		public static void SetThirdPerson(bool enabled)
		{
			bool flag = !ThirdPersonComponent.initialized;
			if (flag)
			{
				ThirdPersonComponent.Initialize();
				bool flag2 = !ThirdPersonComponent.initialized;
				if (flag2)
				{
					return;
				}
			}
			ThirdPersonComponent.isThirdPerson = enabled;
			try
			{
				if (enabled)
				{
					bool flag3 = ThirdPersonComponent.thirdPersonCamera == null;
					if (flag3)
					{
						ThirdPersonComponent.CreateThirdPersonCamera();
					}
					bool flag4 = ThirdPersonComponent.thirdPersonCamera != null && ThirdPersonComponent.mainCamera != null;
					if (flag4)
					{
						ThirdPersonComponent.mainCamera.enabled = false;
						ThirdPersonComponent.thirdPersonCamera.enabled = true;
						ThirdPersonComponent.SetInitialThirdPersonPosition();
						MelonLogger.Msg("Enabled third person view");
					}
				}
				else
				{
					ThirdPersonComponent.RestoreFirstPerson();
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error setting third person: " + ex.Message);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001A074 File Offset: 0x00018274
		private static void SetInitialThirdPersonPosition()
		{
			bool flag = ThirdPersonComponent.thirdPersonCamera == null || ThirdPersonComponent.localPlayer == null;
			if (!flag)
			{
				try
				{
					Vector3 position = ThirdPersonComponent.localPlayer.transform.position;
					Vector3 forward = ThirdPersonComponent.localPlayer.transform.forward;
					Vector3 a = position + Vector3.up * 1.7f;
					bool flag2 = ThirdPersonComponent.headBone != null;
					if (flag2)
					{
						a = ThirdPersonComponent.headBone.position;
					}
					Vector3 vector = a + -forward * ThirdPersonComponent.cameraDistance + Vector3.up * ThirdPersonComponent.cameraHeight;
					ThirdPersonComponent.thirdPersonCamera.transform.position = vector;
					Vector3 forward2 = a - vector;
					bool flag3 = forward2.magnitude > 0.01f;
					if (flag3)
					{
						ThirdPersonComponent.thirdPersonCamera.transform.rotation = Quaternion.LookRotation(forward2, Vector3.up);
					}
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error setting initial position: " + ex.Message);
				}
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001A1A8 File Offset: 0x000183A8
		public static void ToggleThirdPerson()
		{
			ThirdPersonComponent.SetThirdPerson(!ThirdPersonComponent.isThirdPerson);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001A1BC File Offset: 0x000183BC
		public static void Update()
		{
			bool flag = !ThirdPersonComponent.initialized;
			if (flag)
			{
				ThirdPersonComponent.Initialize();
				bool flag2 = !ThirdPersonComponent.initialized;
				if (flag2)
				{
					return;
				}
			}
			bool keyDown = Input.GetKeyDown(KeyCode.F5);
			if (keyDown)
			{
				ThirdPersonComponent.ToggleThirdPerson();
			}
			bool flag3 = ThirdPersonComponent.isThirdPerson && ThirdPersonComponent.thirdPersonCamera != null && ThirdPersonComponent.thirdPersonCamera.enabled;
			if (flag3)
			{
				ThirdPersonComponent.UpdateThirdPersonCamera();
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001A230 File Offset: 0x00018430
		private static void UpdateThirdPersonCamera()
		{
			bool flag = ThirdPersonComponent.thirdPersonCamera == null || ThirdPersonComponent.localPlayer == null;
			if (!flag)
			{
				try
				{
					Vector3 position = ThirdPersonComponent.localPlayer.transform.position;
					Vector3 forward = ThirdPersonComponent.localPlayer.transform.forward;
					Vector3 a = position + Vector3.up * 1.7f;
					bool flag2 = ThirdPersonComponent.headBone != null;
					if (flag2)
					{
						a = ThirdPersonComponent.headBone.position;
					}
					Vector3 vector = a + -forward * ThirdPersonComponent.cameraDistance + Vector3.up * ThirdPersonComponent.cameraHeight;
					Vector3 forward2 = a - vector;
					Quaternion quaternion = Quaternion.identity;
					bool flag3 = forward2.magnitude > 0.01f;
					if (flag3)
					{
						quaternion = Quaternion.LookRotation(forward2, Vector3.up);
					}
					bool flag4 = ThirdPersonComponent.smoothTime > 0f;
					if (flag4)
					{
						ThirdPersonComponent.thirdPersonCamera.transform.position = Vector3.SmoothDamp(ThirdPersonComponent.thirdPersonCamera.transform.position, vector, ref ThirdPersonComponent.currentVelocity, ThirdPersonComponent.smoothTime);
						ThirdPersonComponent.thirdPersonCamera.transform.rotation = Quaternion.Slerp(ThirdPersonComponent.thirdPersonCamera.transform.rotation, quaternion, Time.deltaTime * (1f / ThirdPersonComponent.smoothTime));
					}
					else
					{
						ThirdPersonComponent.thirdPersonCamera.transform.position = vector;
						ThirdPersonComponent.thirdPersonCamera.transform.rotation = quaternion;
					}
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error updating third person camera: " + ex.Message);
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001A3F8 File Offset: 0x000185F8
		private static void RestoreFirstPerson()
		{
			try
			{
				bool flag = ThirdPersonComponent.thirdPersonCamera != null;
				if (flag)
				{
					ThirdPersonComponent.thirdPersonCamera.enabled = false;
				}
				bool flag2 = ThirdPersonComponent.mainCamera != null;
				if (flag2)
				{
					ThirdPersonComponent.mainCamera.enabled = true;
				}
				ThirdPersonComponent.currentVelocity = Vector3.zero;
				MelonLogger.Msg("Restored first person view");
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error restoring first person: " + ex.Message);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001A484 File Offset: 0x00018684
		public static void SetThirdPersonOffset(bool enabled)
		{
			bool flag = !ThirdPersonComponent.initialized;
			if (flag)
			{
				ThirdPersonComponent.Initialize();
				bool flag2 = !ThirdPersonComponent.initialized;
				if (flag2)
				{
					return;
				}
			}
			try
			{
				if (enabled)
				{
					bool flag3 = ThirdPersonComponent.mainCamera != null && ThirdPersonComponent.headBone != null;
					if (flag3)
					{
						ThirdPersonComponent.mainCamera.transform.SetParent(ThirdPersonComponent.headBone, false);
						ThirdPersonComponent.mainCamera.transform.localPosition = new Vector3(0f, 0.8f, -2.5f);
						ThirdPersonComponent.mainCamera.transform.localRotation = Quaternion.Euler(15f, 0f, 0f);
						ThirdPersonComponent.isThirdPerson = true;
						MelonLogger.Msg("Enabled third person offset view");
					}
				}
				else
				{
					ThirdPersonComponent.RestoreFirstPersonOffset();
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error setting third person offset: " + ex.Message);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001A590 File Offset: 0x00018790
		private static void RestoreFirstPersonOffset()
		{
			try
			{
				bool flag = ThirdPersonComponent.mainCamera != null;
				if (flag)
				{
					bool flag2 = ThirdPersonComponent.originalCameraParent != null;
					if (flag2)
					{
						ThirdPersonComponent.mainCamera.transform.SetParent(ThirdPersonComponent.originalCameraParent, false);
					}
					ThirdPersonComponent.mainCamera.transform.localPosition = ThirdPersonComponent.originalCameraPosition;
					ThirdPersonComponent.mainCamera.transform.localRotation = ThirdPersonComponent.originalCameraRotation;
				}
				ThirdPersonComponent.SetHeadVisibility(true);
				ThirdPersonComponent.isThirdPerson = false;
				MelonLogger.Msg("Restored first person offset view");
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error restoring first person offset: " + ex.Message);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001A648 File Offset: 0x00018848
		public static void SetCameraDistance(float distance)
		{
			ThirdPersonComponent.cameraDistance = Mathf.Clamp(distance, 0.5f, 10f);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001A660 File Offset: 0x00018860
		public static void SetCameraHeight(float height)
		{
			ThirdPersonComponent.cameraHeight = Mathf.Clamp(height, -2f, 5f);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001A678 File Offset: 0x00018878
		public static void SetSmoothTime(float time)
		{
			ThirdPersonComponent.smoothTime = Mathf.Clamp(time, 0f, 1f);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001A690 File Offset: 0x00018890
		public static void SetOffsetCameraPosition(Vector3 localPosition)
		{
			bool flag = ThirdPersonComponent.isThirdPerson && ThirdPersonComponent.mainCamera != null && ThirdPersonComponent.headBone != null;
			if (flag)
			{
				ThirdPersonComponent.mainCamera.transform.localPosition = localPosition;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001A6D8 File Offset: 0x000188D8
		public static void SetOffsetCameraRotation(Vector3 eulerAngles)
		{
			bool flag = ThirdPersonComponent.isThirdPerson && ThirdPersonComponent.mainCamera != null && ThirdPersonComponent.headBone != null;
			if (flag)
			{
				ThirdPersonComponent.mainCamera.transform.localRotation = Quaternion.Euler(eulerAngles);
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001A724 File Offset: 0x00018924
		public static void SetThirdPersonPreset(string preset)
		{
			bool flag = !ThirdPersonComponent.isThirdPerson;
			if (!flag)
			{
				string text = preset.ToLower();
				string a = text;
				if (!(a == "default"))
				{
					if (!(a == "close"))
					{
						if (!(a == "high"))
						{
							if (!(a == "side"))
							{
								if (a == "overhead")
								{
									ThirdPersonComponent.SetOffsetCameraPosition(new Vector3(0f, 3f, -1f));
									ThirdPersonComponent.SetOffsetCameraRotation(new Vector3(45f, 0f, 0f));
								}
							}
							else
							{
								ThirdPersonComponent.SetOffsetCameraPosition(new Vector3(1.5f, 0.5f, -1f));
								ThirdPersonComponent.SetOffsetCameraRotation(new Vector3(0f, -30f, 0f));
							}
						}
						else
						{
							ThirdPersonComponent.SetOffsetCameraPosition(new Vector3(0f, 1.5f, -3f));
							ThirdPersonComponent.SetOffsetCameraRotation(new Vector3(25f, 0f, 0f));
						}
					}
					else
					{
						ThirdPersonComponent.SetOffsetCameraPosition(new Vector3(0f, 0.5f, -1.5f));
						ThirdPersonComponent.SetOffsetCameraRotation(new Vector3(10f, 0f, 0f));
					}
				}
				else
				{
					ThirdPersonComponent.SetOffsetCameraPosition(new Vector3(0f, 0.8f, -2.5f));
					ThirdPersonComponent.SetOffsetCameraRotation(new Vector3(15f, 0f, 0f));
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001A8AF File Offset: 0x00018AAF
		public static void ToggleHeadVisibility()
		{
			ThirdPersonComponent.SetHeadVisibility(!ThirdPersonComponent.headVisible);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public static void ForceHeadVisibility(bool visible)
		{
			ThirdPersonComponent.SetHeadVisibility(visible);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001A8CC File Offset: 0x00018ACC
		public static void DebugAvatarStructure()
		{
			try
			{
				bool flag = ThirdPersonComponent.localPlayer != null;
				if (flag)
				{
					Animator componentInChildren = ThirdPersonComponent.localPlayer.GetComponentInChildren<Animator>();
					bool flag2 = componentInChildren != null;
					if (flag2)
					{
						MelonLogger.Msg("=== Avatar Debug Info ===");
						string str = "Head bone: ";
						Transform transform = ThirdPersonComponent.headBone;
						MelonLogger.Msg(str + (((transform != null) ? transform.name : null) ?? "NULL"));
						string str2 = "Chest bone: ";
						Transform transform2 = ThirdPersonComponent.chestBone;
						MelonLogger.Msg(str2 + (((transform2 != null) ? transform2.name : null) ?? "NULL"));
						string format = "Head renderers found: {0}";
						Renderer[] array = ThirdPersonComponent.headRenderers;
						MelonLogger.Msg(string.Format(format, (array != null) ? array.Length : 0));
						bool flag3 = ThirdPersonComponent.headRenderers != null;
						if (flag3)
						{
							for (int i = 0; i < ThirdPersonComponent.headRenderers.Length; i++)
							{
								Renderer renderer = ThirdPersonComponent.headRenderers[i];
								MelonLogger.Msg(string.Format("  Renderer {0}: {1} - Enabled: {2}", i, ((renderer != null) ? renderer.name : null) ?? "NULL", renderer != null && renderer.enabled));
							}
						}
						string str3 = "Camera parent: ";
						Camera camera = ThirdPersonComponent.mainCamera;
						string text;
						if (camera == null)
						{
							text = null;
						}
						else
						{
							Transform transform3 = camera.transform;
							if (transform3 == null)
							{
								text = null;
							}
							else
							{
								Transform parent = transform3.parent;
								text = ((parent != null) ? parent.name : null);
							}
						}
						MelonLogger.Msg(str3 + (text ?? "NULL"));
						string format2 = "Camera local pos: {0}";
						Camera camera2 = ThirdPersonComponent.mainCamera;
						Vector3? vector;
						if (camera2 == null)
						{
							vector = null;
						}
						else
						{
							Transform transform4 = camera2.transform;
							vector = ((transform4 != null) ? new Vector3?(transform4.localPosition) : null);
						}
						MelonLogger.Msg(string.Format(format2, vector));
					}
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error debugging avatar: " + ex.Message);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001AACC File Offset: 0x00018CCC
		public static void TryDifferentBone(string boneName)
		{
			try
			{
				bool flag = ThirdPersonComponent.localPlayer != null && ThirdPersonComponent.isThirdPerson;
				if (flag)
				{
					Animator componentInChildren = ThirdPersonComponent.localPlayer.GetComponentInChildren<Animator>();
					bool flag2 = componentInChildren != null;
					if (flag2)
					{
						HumanBodyBones humanBodyBones;
						bool flag3 = Enum.TryParse<HumanBodyBones>(boneName, out humanBodyBones);
						if (flag3)
						{
							Transform boneTransform = componentInChildren.GetBoneTransform(humanBodyBones);
							bool flag4 = boneTransform != null && ThirdPersonComponent.mainCamera != null;
							if (flag4)
							{
								ThirdPersonComponent.mainCamera.transform.SetParent(boneTransform, false);
								ThirdPersonComponent.mainCamera.transform.localPosition = new Vector3(0f, 1f, -2f);
								ThirdPersonComponent.mainCamera.transform.localRotation = Quaternion.Euler(15f, 0f, 0f);
								MelonLogger.Msg("Attached camera to " + boneName + " bone: " + boneTransform.name);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error("Error trying bone " + boneName + ": " + ex.Message);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001AC00 File Offset: 0x00018E00
		public static void OnPlayerJoined()
		{
			bool flag = ThirdPersonComponent.localPlayer == null;
			if (flag)
			{
				ThirdPersonComponent.localPlayer = Player.prop_Player_0;
				ThirdPersonComponent.GetHeadBone();
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001AC30 File Offset: 0x00018E30
		public static void OnAvatarChanged()
		{
			ThirdPersonComponent.GetHeadBone();
			bool flag = ThirdPersonComponent.isThirdPerson;
			if (flag)
			{
				ThirdPersonComponent.SetHeadVisibility(false);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001AC58 File Offset: 0x00018E58
		public static void Destroy()
		{
			bool flag = ThirdPersonComponent.isThirdPerson;
			if (flag)
			{
				ThirdPersonComponent.RestoreFirstPerson();
				ThirdPersonComponent.RestoreFirstPersonOffset();
			}
			ThirdPersonComponent.SetHeadVisibility(true);
			bool flag2 = ThirdPersonComponent.thirdPersonCameraObject != null;
			if (flag2)
			{
				Object.Destroy(ThirdPersonComponent.thirdPersonCameraObject);
				ThirdPersonComponent.thirdPersonCamera = null;
				ThirdPersonComponent.thirdPersonCameraObject = null;
			}
			ThirdPersonComponent.initialized = false;
			ThirdPersonComponent.isThirdPerson = false;
			ThirdPersonComponent.mainCamera = null;
			ThirdPersonComponent.localPlayer = null;
			ThirdPersonComponent.headBone = null;
			ThirdPersonComponent.chestBone = null;
			ThirdPersonComponent.headRenderers = null;
			ThirdPersonComponent.currentVelocity = Vector3.zero;
			MelonLogger.Msg("Third Person Component destroyed");
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0001ACEB File Offset: 0x00018EEB
		public static bool IsThirdPerson
		{
			get
			{
				return ThirdPersonComponent.isThirdPerson;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0001ACF2 File Offset: 0x00018EF2
		public static bool IsInitialized
		{
			get
			{
				return ThirdPersonComponent.initialized;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0001ACF9 File Offset: 0x00018EF9
		public static bool IsHeadVisible
		{
			get
			{
				return ThirdPersonComponent.headVisible;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0001AD00 File Offset: 0x00018F00
		public static float CameraDistance
		{
			get
			{
				return ThirdPersonComponent.cameraDistance;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0001AD07 File Offset: 0x00018F07
		public static float CameraHeight
		{
			get
			{
				return ThirdPersonComponent.cameraHeight;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0001AD0E File Offset: 0x00018F0E
		public static float SmoothTime
		{
			get
			{
				return ThirdPersonComponent.smoothTime;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0001AD15 File Offset: 0x00018F15
		public static string AttachedBone
		{
			get
			{
				Camera camera = ThirdPersonComponent.mainCamera;
				string text;
				if (camera == null)
				{
					text = null;
				}
				else
				{
					Transform transform = camera.transform;
					if (transform == null)
					{
						text = null;
					}
					else
					{
						Transform parent = transform.parent;
						text = ((parent != null) ? parent.name : null);
					}
				}
				return text ?? "None";
			}
		}

		// Token: 0x0400017F RID: 383
		private static bool isThirdPerson = false;

		// Token: 0x04000180 RID: 384
		private static Camera mainCamera;

		// Token: 0x04000181 RID: 385
		private static Player localPlayer;

		// Token: 0x04000182 RID: 386
		private static Transform originalCameraParent;

		// Token: 0x04000183 RID: 387
		private static Vector3 originalCameraPosition;

		// Token: 0x04000184 RID: 388
		private static Quaternion originalCameraRotation;

		// Token: 0x04000185 RID: 389
		private static float cameraHeight = 1.8f;

		// Token: 0x04000186 RID: 390
		private static float cameraDistance = 2.5f;

		// Token: 0x04000187 RID: 391
		private static float smoothTime = 0.3f;

		// Token: 0x04000188 RID: 392
		private static bool initialized = false;

		// Token: 0x04000189 RID: 393
		private static Vector3 currentVelocity;

		// Token: 0x0400018A RID: 394
		private static Transform headBone;

		// Token: 0x0400018B RID: 395
		private static Transform chestBone;

		// Token: 0x0400018C RID: 396
		private static Camera thirdPersonCamera;

		// Token: 0x0400018D RID: 397
		private static GameObject thirdPersonCameraObject;

		// Token: 0x0400018E RID: 398
		private static Renderer[] headRenderers;

		// Token: 0x0400018F RID: 399
		private static bool headVisible = true;
	}
}

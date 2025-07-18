using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using VRC;
using VRC.Core;

namespace Odium.Components
{
	// Token: 0x02000053 RID: 83
	public class BoxESP
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00013783 File Offset: 0x00011983
		public static void Initialize()
		{
			BoxESP.CreateLineMaterial();
			MelonLogger.Msg("Box ESP initialized");
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00013798 File Offset: 0x00011998
		private static void CreateLineMaterial()
		{
			bool flag = BoxESP.lineMaterial == null;
			if (flag)
			{
				Shader shader = Shader.Find("Hidden/Internal-Colored");
				BoxESP.lineMaterial = new Material(shader);
				BoxESP.lineMaterial.hideFlags = HideFlags.HideAndDontSave;
				BoxESP.lineMaterial.SetInt("_SrcBlend", 5);
				BoxESP.lineMaterial.SetInt("_DstBlend", 10);
				BoxESP.lineMaterial.SetInt("_Cull", 0);
				BoxESP.lineMaterial.SetInt("_ZTest", 8);
				BoxESP.lineMaterial.SetInt("_ZWrite", 0);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00013830 File Offset: 0x00011A30
		public static void SetEnabled(bool enabled)
		{
			BoxESP.isEnabled = enabled;
			if (enabled)
			{
				BoxESP.RefreshPlayerList();
			}
			else
			{
				BoxESP.playerBoxes.Clear();
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00013860 File Offset: 0x00011A60
		public static void SetBoxColor(Color color)
		{
			BoxESP.boxColor = color;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00013869 File Offset: 0x00011A69
		public static void SetBoxDimensions(float height, float width, float depth)
		{
			BoxESP.boxHeight = height;
			BoxESP.boxWidth = width;
			BoxESP.boxDepth = depth;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0001387E File Offset: 0x00011A7E
		public static void SetShowOnlyVisible(bool onlyVisible)
		{
			BoxESP.showOnlyVisible = onlyVisible;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00013887 File Offset: 0x00011A87
		public static void SetShowPlayerNames(bool show)
		{
			BoxESP.showPlayerNames = show;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00013890 File Offset: 0x00011A90
		public static void SetShowDistance(bool show)
		{
			BoxESP.showDistance = show;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0001389C File Offset: 0x00011A9C
		public static void RefreshPlayerList()
		{
			bool flag = !BoxESP.isEnabled;
			if (!flag)
			{
				BoxESP.playerBoxes.Clear();
				try
				{
					Player[] array = Object.FindObjectsOfType<Player>();
					foreach (Player player in array)
					{
						bool flag2 = player == null || player.gameObject == null;
						if (!flag2)
						{
							Player player2 = Player.prop_Player_0;
							bool flag3 = player2 != null && player.gameObject == player2.gameObject;
							if (!flag3)
							{
								Animator componentInChildren = player.GetComponentInChildren<Animator>();
								bool flag4 = componentInChildren != null && componentInChildren.isHuman;
								if (flag4)
								{
									BoxESP.PlayerBoxData item = new BoxESP.PlayerBoxData(player, componentInChildren);
									BoxESP.CalculateBoundingBox(ref item);
									BoxESP.playerBoxes.Add(item);
								}
							}
						}
					}
					MelonLogger.Msg(string.Format("Found {0} players for box ESP", BoxESP.playerBoxes.Count));
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error refreshing player list: " + ex.Message);
				}
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000139D8 File Offset: 0x00011BD8
		private static void CalculateBoundingBox(ref BoxESP.PlayerBoxData boxData)
		{
			bool flag = boxData.rootBone == null;
			if (!flag)
			{
				Vector3 vector = boxData.rootBone.position;
				bool flag2 = boxData.headBone != null;
				if (flag2)
				{
					vector = Vector3.Lerp(boxData.rootBone.position, boxData.headBone.position, 0.5f);
				}
				Vector3 size = new Vector3(BoxESP.boxWidth, BoxESP.boxHeight, BoxESP.boxDepth);
				boxData.boundingBox = new Bounds(vector, size);
				Player player = Player.prop_Player_0;
				bool flag3 = player != null;
				if (flag3)
				{
					boxData.distanceToPlayer = Vector3.Distance(player.transform.position, vector);
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00013A8C File Offset: 0x00011C8C
		public static void Update()
		{
			bool flag = !BoxESP.isEnabled;
			if (!flag)
			{
				for (int i = BoxESP.playerBoxes.Count - 1; i >= 0; i--)
				{
					bool flag2 = BoxESP.playerBoxes[i].player == null || BoxESP.playerBoxes[i].animator == null;
					if (flag2)
					{
						BoxESP.playerBoxes.RemoveAt(i);
					}
					else
					{
						BoxESP.PlayerBoxData value = BoxESP.playerBoxes[i];
						BoxESP.CalculateBoundingBox(ref value);
						BoxESP.playerBoxes[i] = value;
					}
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00013B34 File Offset: 0x00011D34
		public static void OnGUI()
		{
			bool flag = !BoxESP.isEnabled || BoxESP.playerBoxes.Count == 0;
			if (!flag)
			{
				Camera current = Camera.current;
				bool flag2 = current == null;
				if (!flag2)
				{
					try
					{
						GL.PushMatrix();
						BoxESP.lineMaterial.SetPass(0);
						GL.LoadPixelMatrix();
						GL.Begin(1);
						GL.Color(BoxESP.boxColor);
						foreach (BoxESP.PlayerBoxData playerBoxData in BoxESP.playerBoxes)
						{
							bool flag3 = !playerBoxData.isValid;
							if (!flag3)
							{
								BoxESP.DrawBoundingBox(playerBoxData.boundingBox, current);
							}
						}
						GL.End();
						GL.PopMatrix();
						bool flag4 = BoxESP.showPlayerNames || BoxESP.showDistance;
						if (flag4)
						{
							BoxESP.DrawTextOverlays(current);
						}
					}
					catch (Exception ex)
					{
						MelonLogger.Error("Error drawing box ESP: " + ex.Message);
					}
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00013C60 File Offset: 0x00011E60
		private static void DrawBoundingBox(Bounds bounds, Camera camera)
		{
			Vector3 center = bounds.center;
			Vector3 size = bounds.size;
			Vector3[] array = new Vector3[]
			{
				center + new Vector3(-size.x, -size.y, -size.z) * 0.5f,
				center + new Vector3(size.x, -size.y, -size.z) * 0.5f,
				center + new Vector3(size.x, -size.y, size.z) * 0.5f,
				center + new Vector3(-size.x, -size.y, size.z) * 0.5f,
				center + new Vector3(-size.x, size.y, -size.z) * 0.5f,
				center + new Vector3(size.x, size.y, -size.z) * 0.5f,
				center + new Vector3(size.x, size.y, size.z) * 0.5f,
				center + new Vector3(-size.x, size.y, size.z) * 0.5f
			};
			Vector3[] array2 = new Vector3[8];
			bool flag = true;
			for (int i = 0; i < 8; i++)
			{
				array2[i] = camera.WorldToScreenPoint(array[i]);
				bool flag2 = array2[i].z > 0f;
				if (flag2)
				{
					flag = false;
				}
			}
			bool flag3 = flag && BoxESP.showOnlyVisible;
			if (!flag3)
			{
				BoxESP.DrawLine(array2[0], array2[1]);
				BoxESP.DrawLine(array2[1], array2[2]);
				BoxESP.DrawLine(array2[2], array2[3]);
				BoxESP.DrawLine(array2[3], array2[0]);
				BoxESP.DrawLine(array2[4], array2[5]);
				BoxESP.DrawLine(array2[5], array2[6]);
				BoxESP.DrawLine(array2[6], array2[7]);
				BoxESP.DrawLine(array2[7], array2[4]);
				BoxESP.DrawLine(array2[0], array2[4]);
				BoxESP.DrawLine(array2[1], array2[5]);
				BoxESP.DrawLine(array2[2], array2[6]);
				BoxESP.DrawLine(array2[3], array2[7]);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00013F60 File Offset: 0x00012160
		private static void DrawLine(Vector3 start, Vector3 end)
		{
			bool flag = start.z > 0f && end.z > 0f;
			if (flag)
			{
				GL.Vertex3(start.x, start.y, 0f);
				GL.Vertex3(end.x, end.y, 0f);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00013FC0 File Offset: 0x000121C0
		private static void DrawTextOverlays(Camera camera)
		{
			foreach (BoxESP.PlayerBoxData playerBoxData in BoxESP.playerBoxes)
			{
				bool flag = !playerBoxData.isValid;
				if (!flag)
				{
					Bounds boundingBox = playerBoxData.boundingBox;
					Vector3 center = boundingBox.center;
					Vector3 up = Vector3.up;
					boundingBox = playerBoxData.boundingBox;
					Vector3 position = center + up * (boundingBox.size.y * 0.5f);
					Vector3 vector = camera.WorldToScreenPoint(position);
					bool flag2 = vector.z > 0f;
					if (flag2)
					{
						vector.y = (float)Screen.height - vector.y;
						string text = "";
						bool flag3 = BoxESP.showPlayerNames;
						if (flag3)
						{
							text += playerBoxData.playerName;
						}
						bool flag4 = BoxESP.showDistance;
						if (flag4)
						{
							bool flag5 = text.Length > 0;
							if (flag5)
							{
								text += "\n";
							}
							text += string.Format("{0:F1}m", playerBoxData.distanceToPlayer);
						}
						bool flag6 = text.Length > 0;
						if (flag6)
						{
							GUI.color = BoxESP.boxColor;
							GUI.Label(new Rect(vector.x - 50f, vector.y - 30f, 100f, 50f), text);
							GUI.color = Color.white;
						}
					}
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00014168 File Offset: 0x00012368
		public static void OnPlayerJoined(Player player)
		{
			bool flag = !BoxESP.isEnabled;
			if (!flag)
			{
				BoxESP.RefreshPlayerList();
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0001418C File Offset: 0x0001238C
		public static void OnPlayerLeft(Player player)
		{
			bool flag = !BoxESP.isEnabled;
			if (!flag)
			{
				BoxESP.playerBoxes.RemoveAll((BoxESP.PlayerBoxData p) => p.player == player);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000141CC File Offset: 0x000123CC
		public static void Destroy()
		{
			BoxESP.isEnabled = false;
			BoxESP.playerBoxes.Clear();
			bool flag = BoxESP.lineMaterial != null;
			if (flag)
			{
				Object.DestroyImmediate(BoxESP.lineMaterial);
				BoxESP.lineMaterial = null;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0001420D File Offset: 0x0001240D
		public static void SetBoxHeight(float height)
		{
			BoxESP.boxHeight = Mathf.Clamp(height, 0.5f, 4f);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00014225 File Offset: 0x00012425
		public static void SetBoxWidth(float width)
		{
			BoxESP.boxWidth = Mathf.Clamp(width, 0.2f, 2f);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001423D File Offset: 0x0001243D
		public static void SetBoxDepth(float depth)
		{
			BoxESP.boxDepth = Mathf.Clamp(depth, 0.2f, 2f);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00014255 File Offset: 0x00012455
		public static bool IsEnabled
		{
			get
			{
				return BoxESP.isEnabled;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0001425C File Offset: 0x0001245C
		public static int PlayerCount
		{
			get
			{
				return BoxESP.playerBoxes.Count;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00014268 File Offset: 0x00012468
		public static float BoxHeight
		{
			get
			{
				return BoxESP.boxHeight;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0001426F File Offset: 0x0001246F
		public static float BoxWidth
		{
			get
			{
				return BoxESP.boxWidth;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00014276 File Offset: 0x00012476
		public static float BoxDepth
		{
			get
			{
				return BoxESP.boxDepth;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0001427D File Offset: 0x0001247D
		public static bool ShowPlayerNames
		{
			get
			{
				return BoxESP.showPlayerNames;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00014284 File Offset: 0x00012484
		public static bool ShowDistance
		{
			get
			{
				return BoxESP.showDistance;
			}
		}

		// Token: 0x04000108 RID: 264
		private static bool isEnabled = false;

		// Token: 0x04000109 RID: 265
		private static Color boxColor = Color.white;

		// Token: 0x0400010A RID: 266
		private static List<BoxESP.PlayerBoxData> playerBoxes = new List<BoxESP.PlayerBoxData>();

		// Token: 0x0400010B RID: 267
		private static Material lineMaterial;

		// Token: 0x0400010C RID: 268
		private static float boxHeight = 2f;

		// Token: 0x0400010D RID: 269
		private static float boxWidth = 0.6f;

		// Token: 0x0400010E RID: 270
		private static float boxDepth = 0.4f;

		// Token: 0x0400010F RID: 271
		private static bool showOnlyVisible = false;

		// Token: 0x04000110 RID: 272
		private static bool showPlayerNames = true;

		// Token: 0x04000111 RID: 273
		private static bool showDistance = true;

		// Token: 0x020000FA RID: 250
		public struct PlayerBoxData
		{
			// Token: 0x0600065E RID: 1630 RVA: 0x00029174 File Offset: 0x00027374
			public PlayerBoxData(Player plr, Animator anim)
			{
				this.player = plr;
				this.animator = anim;
				this.rootBone = null;
				this.headBone = null;
				this.isValid = (anim != null);
				this.boundingBox = default(Bounds);
				this.distanceToPlayer = 0f;
				string text;
				if (plr == null)
				{
					text = null;
				}
				else
				{
					APIUser field_Private_APIUser_ = plr.field_Private_APIUser_0;
					text = ((field_Private_APIUser_ != null) ? field_Private_APIUser_.displayName : null);
				}
				this.playerName = (text ?? "Unknown");
				bool flag = anim != null && anim.isHuman;
				if (flag)
				{
					this.rootBone = anim.GetBoneTransform(0);
					this.headBone = anim.GetBoneTransform(10);
				}
			}

			// Token: 0x04000417 RID: 1047
			public Player player;

			// Token: 0x04000418 RID: 1048
			public Animator animator;

			// Token: 0x04000419 RID: 1049
			public Transform rootBone;

			// Token: 0x0400041A RID: 1050
			public Transform headBone;

			// Token: 0x0400041B RID: 1051
			public bool isValid;

			// Token: 0x0400041C RID: 1052
			public Bounds boundingBox;

			// Token: 0x0400041D RID: 1053
			public float distanceToPlayer;

			// Token: 0x0400041E RID: 1054
			public string playerName;
		}
	}
}

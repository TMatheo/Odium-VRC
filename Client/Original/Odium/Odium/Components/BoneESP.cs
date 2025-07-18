using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using VRC;

namespace Odium.Components
{
	// Token: 0x02000056 RID: 86
	public class BoneESP
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0001464E File Offset: 0x0001284E
		public static void Initialize()
		{
			BoneESP.CreateLineMaterial();
			MelonLogger.Msg("Bone ESP initialized");
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00014664 File Offset: 0x00012864
		private static void CreateLineMaterial()
		{
			bool flag = BoneESP.lineMaterial == null;
			if (flag)
			{
				Shader shader = Shader.Find("Hidden/Internal-Colored");
				BoneESP.lineMaterial = new Material(shader);
				BoneESP.lineMaterial.hideFlags = HideFlags.HideAndDontSave;
				BoneESP.lineMaterial.SetInt("_SrcBlend", 5);
				BoneESP.lineMaterial.SetInt("_DstBlend", 10);
				BoneESP.lineMaterial.SetInt("_Cull", 0);
				BoneESP.lineMaterial.SetInt("_ZTest", 8);
				BoneESP.lineMaterial.SetInt("_ZWrite", 0);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000146FC File Offset: 0x000128FC
		public static void SetEnabled(bool enabled)
		{
			BoneESP.isEnabled = enabled;
			if (enabled)
			{
				BoneESP.RefreshPlayerList();
			}
			else
			{
				BoneESP.playerBones.Clear();
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0001472C File Offset: 0x0001292C
		public static void SetBoneColor(Color color)
		{
			BoneESP.boneColor = color;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00014738 File Offset: 0x00012938
		public static void RefreshPlayerList()
		{
			bool flag = !BoneESP.isEnabled;
			if (!flag)
			{
				BoneESP.playerBones.Clear();
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
									BoneESP.PlayerBoneData item = new BoneESP.PlayerBoneData(player, componentInChildren);
									BoneESP.SetupBoneConnections(ref item);
									BoneESP.playerBones.Add(item);
								}
							}
						}
					}
					MelonLogger.Msg(string.Format("Found {0} players with valid bone data", BoneESP.playerBones.Count));
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error refreshing player list: " + ex.Message);
				}
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00014874 File Offset: 0x00012A74
		private static void SetupBoneConnections(ref BoneESP.PlayerBoneData boneData)
		{
			boneData.connections.Clear();
			foreach (ValueTuple<HumanBodyBones, HumanBodyBones> valueTuple in BoneESP.boneConnections)
			{
				Transform boneTransform = boneData.animator.GetBoneTransform(valueTuple.Item1);
				Transform boneTransform2 = boneData.animator.GetBoneTransform(valueTuple.Item2);
				bool flag = boneTransform != null && boneTransform2 != null;
				if (flag)
				{
					BoneESP.BoneConnection item = new BoneESP.BoneConnection(boneTransform, boneTransform2, valueTuple.Item1, valueTuple.Item2);
					boneData.connections.Add(item);
				}
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00014914 File Offset: 0x00012B14
		public static void Update()
		{
			bool flag = !BoneESP.isEnabled;
			if (!flag)
			{
				for (int i = BoneESP.playerBones.Count - 1; i >= 0; i--)
				{
					bool flag2 = BoneESP.playerBones[i].player == null || BoneESP.playerBones[i].animator == null;
					if (flag2)
					{
						BoneESP.playerBones.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00014994 File Offset: 0x00012B94
		public static void OnGUI()
		{
			bool flag = !BoneESP.isEnabled || BoneESP.playerBones.Count == 0;
			if (!flag)
			{
				Camera current = Camera.current;
				bool flag2 = current == null;
				if (!flag2)
				{
					try
					{
						GL.PushMatrix();
						BoneESP.lineMaterial.SetPass(0);
						GL.LoadPixelMatrix();
						GL.Begin(1);
						GL.Color(BoneESP.boneColor);
						foreach (BoneESP.PlayerBoneData playerBoneData in BoneESP.playerBones)
						{
							bool flag3 = !playerBoneData.isValid || playerBoneData.connections == null;
							if (!flag3)
							{
								foreach (BoneESP.BoneConnection boneConnection in playerBoneData.connections)
								{
									bool flag4 = boneConnection.startBone == null || boneConnection.endBone == null;
									if (!flag4)
									{
										Vector3 vector = current.WorldToScreenPoint(boneConnection.startBone.position);
										Vector3 vector2 = current.WorldToScreenPoint(boneConnection.endBone.position);
										bool flag5 = vector.z > 0f && vector2.z > 0f;
										if (flag5)
										{
											GL.Vertex3(vector.x, vector.y, 0f);
											GL.Vertex3(vector2.x, vector2.y, 0f);
										}
									}
								}
							}
						}
						GL.End();
						GL.PopMatrix();
					}
					catch (Exception ex)
					{
						MelonLogger.Error("Error drawing bone ESP: " + ex.Message);
					}
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00014BC0 File Offset: 0x00012DC0
		public static void OnPlayerJoined(Player player)
		{
			bool flag = !BoneESP.isEnabled;
			if (!flag)
			{
				BoneESP.RefreshPlayerList();
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00014BE4 File Offset: 0x00012DE4
		public static void OnPlayerLeft(Player player)
		{
			bool flag = !BoneESP.isEnabled;
			if (!flag)
			{
				BoneESP.playerBones.RemoveAll((BoneESP.PlayerBoneData p) => p.player == player);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00014C24 File Offset: 0x00012E24
		public static void Destroy()
		{
			BoneESP.isEnabled = false;
			BoneESP.playerBones.Clear();
			bool flag = BoneESP.lineMaterial != null;
			if (flag)
			{
				Object.DestroyImmediate(BoneESP.lineMaterial);
				BoneESP.lineMaterial = null;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00014C65 File Offset: 0x00012E65
		public static bool IsEnabled
		{
			get
			{
				return BoneESP.isEnabled;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00014C6C File Offset: 0x00012E6C
		public static int PlayerCount
		{
			get
			{
				return BoneESP.playerBones.Count;
			}
		}

		// Token: 0x0400011B RID: 283
		private static bool isEnabled = false;

		// Token: 0x0400011C RID: 284
		private static Color boneColor = Color.white;

		// Token: 0x0400011D RID: 285
		private static List<BoneESP.PlayerBoneData> playerBones = new List<BoneESP.PlayerBoneData>();

		// Token: 0x0400011E RID: 286
		private static Material lineMaterial;

		// Token: 0x0400011F RID: 287
		private static readonly ValueTuple<HumanBodyBones, HumanBodyBones>[] boneConnections = new ValueTuple<HumanBodyBones, HumanBodyBones>[]
		{
			new ValueTuple<HumanBodyBones, HumanBodyBones>(0, 7),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(7, 8),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(8, 54),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(54, 9),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(9, 10),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(54, 11),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(11, 13),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(13, 15),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(15, 17),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(54, 12),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(12, 14),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(14, 16),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(16, 18),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(0, 1),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(1, 3),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(3, 5),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(5, 19),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(0, 2),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(2, 4),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(4, 6),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(6, 20),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(17, 24),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(24, 25),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(25, 26),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(17, 27),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(27, 28),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(28, 29),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(17, 30),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(30, 31),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(31, 32),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(18, 39),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(39, 40),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(40, 41),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(18, 42),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(42, 43),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(43, 44),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(18, 45),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(45, 46),
			new ValueTuple<HumanBodyBones, HumanBodyBones>(46, 47)
		};

		// Token: 0x020000FC RID: 252
		public struct PlayerBoneData
		{
			// Token: 0x06000661 RID: 1633 RVA: 0x00029238 File Offset: 0x00027438
			public PlayerBoneData(Player plr, Animator anim)
			{
				this.player = plr;
				this.animator = anim;
				this.connections = new List<BoneESP.BoneConnection>();
				this.isValid = (anim != null);
			}

			// Token: 0x04000420 RID: 1056
			public Player player;

			// Token: 0x04000421 RID: 1057
			public Animator animator;

			// Token: 0x04000422 RID: 1058
			public List<BoneESP.BoneConnection> connections;

			// Token: 0x04000423 RID: 1059
			public bool isValid;
		}

		// Token: 0x020000FD RID: 253
		public struct BoneConnection
		{
			// Token: 0x06000662 RID: 1634 RVA: 0x00029261 File Offset: 0x00027461
			public BoneConnection(Transform start, Transform end, HumanBodyBones startType, HumanBodyBones endType)
			{
				this.startBone = start;
				this.endBone = end;
				this.startBoneType = startType;
				this.endBoneType = endType;
			}

			// Token: 0x04000424 RID: 1060
			public Transform startBone;

			// Token: 0x04000425 RID: 1061
			public Transform endBone;

			// Token: 0x04000426 RID: 1062
			public HumanBodyBones startBoneType;

			// Token: 0x04000427 RID: 1063
			public HumanBodyBones endBoneType;
		}
	}
}

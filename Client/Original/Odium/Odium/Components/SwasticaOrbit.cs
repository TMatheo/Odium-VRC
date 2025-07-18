using System;
using Odium.ButtonAPI.QM;
using Odium.QMPages;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace Odium.Components
{
	// Token: 0x0200006F RID: 111
	public class SwasticaOrbit
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00019520 File Offset: 0x00017720
		public static void Activated(Player player, bool state)
		{
			string selected_player_name = AppBot.get_selected_player_name();
			player = ApiUtils.GetPlayerByDisplayName(selected_player_name);
			if (state)
			{
				SwasticaOrbit._target = player;
				SwasticaOrbit._swastika = true;
			}
			else
			{
				SwasticaOrbit._swastika = false;
				SwasticaOrbit._target = null;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00019560 File Offset: 0x00017760
		public static void OnUpdate()
		{
			try
			{
				bool swastika = SwasticaOrbit._swastika;
				bool flag = swastika;
				if (flag)
				{
					try
					{
						bool flag2 = SwasticaOrbit._target != null;
						bool flag3 = flag2;
						if (flag3)
						{
							Vector3 bonePosition = SwasticaOrbit._target.field_Private_VRCPlayerApi_0.GetBonePosition(10);
							bonePosition.Set(bonePosition.x, bonePosition.y + 2f, bonePosition.z);
							SwasticaOrbit._setLocation = bonePosition;
						}
						bool keyDown = Input.GetKeyDown(KeyCode.UpArrow);
						bool flag4 = keyDown;
						if (flag4)
						{
							SwasticaOrbit._swastikaSize += 2f;
						}
						else
						{
							bool keyDown2 = Input.GetKeyDown(KeyCode.DownArrow);
							bool flag5 = keyDown2;
							if (flag5)
							{
								SwasticaOrbit._swastikaSize -= 2f;
							}
						}
						bool flag6 = SwasticaOrbit._rotateState >= 360f;
						bool flag7 = flag6;
						if (flag7)
						{
							SwasticaOrbit._rotateState = Time.deltaTime;
						}
						else
						{
							SwasticaOrbit._rotateState += Time.deltaTime;
						}
						bool flag8 = SwasticaOrbit._hasTakenOwner >= 90f;
						bool flag9 = flag8;
						if (flag9)
						{
							SwasticaOrbit._hasTakenOwner = 0f;
							for (int i = 0; i < OnLoadedSceneManager.sdk3Items.Length; i++)
							{
								VRC_Pickup vrc_Pickup = OnLoadedSceneManager.sdk3Items[i];
								Networking.SetOwner(Player.prop_Player_0.field_Private_VRCPlayerApi_0, vrc_Pickup.gameObject);
							}
						}
						else
						{
							SwasticaOrbit._hasTakenOwner += 1f;
						}
						float num = (float)Convert.ToInt16(OnLoadedSceneManager.sdk3Items.Length / 8);
						float num2 = (float)OnLoadedSceneManager.sdk3Items.Length / SwasticaOrbit._swastikaSize;
						for (int j = 0; j < OnLoadedSceneManager.sdk3Items.Length; j++)
						{
							VRC_Pickup vrc_Pickup2 = OnLoadedSceneManager.sdk3Items[j];
							float num3 = (float)(j % 8);
							float num4 = (float)(j / 8);
							float num5 = num3;
							float num6 = num5;
							bool flag10 = num6 != 6f;
							if (flag10)
							{
								bool flag11 = num6 != 5f;
								if (flag11)
								{
									bool flag12 = num6 != 4f;
									if (flag12)
									{
										bool flag13 = num6 != 3f;
										if (flag13)
										{
											bool flag14 = num6 != 2f;
											if (flag14)
											{
												bool flag15 = num6 != 1f;
												if (flag15)
												{
													bool flag16 = num6 != 0f;
													if (flag16)
													{
														vrc_Pickup2.transform.position = SwasticaOrbit._setLocation + new Vector3((0f - Mathf.Cos(SwasticaOrbit._rotateState)) * num2 * (num4 / num), num2, Mathf.Sin(SwasticaOrbit._rotateState) * num2 * (num4 / num));
													}
													else
													{
														vrc_Pickup2.transform.position = SwasticaOrbit._setLocation + new Vector3(0f, num2 * (num4 / num), 0f);
													}
												}
												else
												{
													vrc_Pickup2.transform.position = SwasticaOrbit._setLocation + new Vector3(0f, (0f - num2) * (num4 / num), 0f);
												}
											}
											else
											{
												vrc_Pickup2.transform.position = SwasticaOrbit._setLocation + new Vector3((0f - Mathf.Cos(SwasticaOrbit._rotateState)) * num2 * (num4 / num), 0f, Mathf.Sin(SwasticaOrbit._rotateState) * num2 * (num4 / num));
											}
										}
										else
										{
											vrc_Pickup2.transform.position = SwasticaOrbit._setLocation + new Vector3((0f - Mathf.Cos(SwasticaOrbit._rotateState + SwasticaOrbit._setMultiplier)) * num2 * (num4 / num), 0f, Mathf.Sin(SwasticaOrbit._rotateState + SwasticaOrbit._setMultiplier) * num2 * (num4 / num));
										}
									}
									else
									{
										vrc_Pickup2.transform.position = SwasticaOrbit._setLocation + new Vector3((0f - Mathf.Cos(SwasticaOrbit._rotateState + SwasticaOrbit._setMultiplier)) * num2, num2 * (num4 / num), Mathf.Sin(SwasticaOrbit._rotateState + SwasticaOrbit._setMultiplier) * num2);
									}
								}
								else
								{
									vrc_Pickup2.transform.position = SwasticaOrbit._setLocation + new Vector3((0f - Mathf.Cos(SwasticaOrbit._rotateState)) * num2, (0f - num2) * (num4 / num), Mathf.Sin(SwasticaOrbit._rotateState) * num2);
								}
							}
							else
							{
								vrc_Pickup2.transform.position = SwasticaOrbit._setLocation + new Vector3((0f - Mathf.Cos(SwasticaOrbit._rotateState + SwasticaOrbit._setMultiplier)) * num2 * (num4 / num), 0f - num2, Mathf.Sin(SwasticaOrbit._rotateState + SwasticaOrbit._setMultiplier) * (num2 * (num4 / num)));
							}
							Vector3 originalVelocity = SwasticaOrbit._originalVelocity;
							bool flag17 = false;
							bool flag18 = flag17;
							if (flag18)
							{
								SwasticaOrbit._originalVelocity = vrc_Pickup2.GetComponent<Rigidbody>().velocity;
							}
							SwasticaOrbit._returnedValue = false;
							vrc_Pickup2.GetComponent<Rigidbody>().velocity = Vector3.zero;
							vrc_Pickup2.transform.rotation = Quaternion.Euler(0f, SwasticaOrbit._rotateState * -90f, 0f);
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine("Module : SwasticaOrbit" + ex.Message);
					}
				}
				else
				{
					bool returnedValue = SwasticaOrbit._returnedValue;
					bool flag19 = returnedValue;
					if (flag19)
					{
						for (int k = 0; k < OnLoadedSceneManager.sdk3Items.Length; k++)
						{
							OnLoadedSceneManager.sdk3Items[k].GetComponent<Rigidbody>().velocity = SwasticaOrbit._originalVelocity;
						}
						SwasticaOrbit._returnedValue = true;
					}
				}
			}
			catch (Exception ex2)
			{
				Console.WriteLine("Module : SwasticaOrbit 2" + ex2.Message);
				SwasticaOrbit._itemOrbit = false;
			}
		}

		// Token: 0x04000172 RID: 370
		public static Player _target;

		// Token: 0x04000173 RID: 371
		public static bool _blind = false;

		// Token: 0x04000174 RID: 372
		public static bool _instance = false;

		// Token: 0x04000175 RID: 373
		public static bool _itemOrbit;

		// Token: 0x04000176 RID: 374
		public static bool _swastika;

		// Token: 0x04000177 RID: 375
		public static GameObject _targetItem;

		// Token: 0x04000178 RID: 376
		internal static Vector3 _setLocation;

		// Token: 0x04000179 RID: 377
		public static float _swastikaSize = 45f;

		// Token: 0x0400017A RID: 378
		public static float _hasTakenOwner = 1999f;

		// Token: 0x0400017B RID: 379
		public static float _setMultiplier = 160f;

		// Token: 0x0400017C RID: 380
		public static float _rotateState;

		// Token: 0x0400017D RID: 381
		public static Vector3 _originalVelocity;

		// Token: 0x0400017E RID: 382
		public static bool _returnedValue;
	}
}

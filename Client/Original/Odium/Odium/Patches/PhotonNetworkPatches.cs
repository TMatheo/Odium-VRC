using System;
using ExitGames.Client.Photon;
using HarmonyLib;
using Il2CppSystem;
using Odium.ApplicationBot;
using Odium.Wrappers;
using Photon.Realtime;

namespace Odium.Patches
{
	// Token: 0x02000032 RID: 50
	[HarmonyPatch(typeof(LoadBalancingClient))]
	public class PhotonNetworkPatches
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0000C834 File Offset: 0x0000AA34
		[HarmonyPrefix]
		[HarmonyPatch("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0")]
		private static bool PrefixSendEvent(byte __0, Object __1, RaiseEventOptions __2, SendOptions __3)
		{
			bool flag = (__0 == 12 && ActionWrapper.serialize) || Bot.movementMimic;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = __0 != 43;
				if (flag2)
				{
					result = true;
				}
				else
				{
					try
					{
						bool flag3 = __1 != null;
						if (flag3)
						{
							bool flag4 = __1.TryCast<Array>() != null;
							if (flag4)
							{
								Array array = __1.TryCast<Array>();
								bool flag5 = array.Length > 1;
								if (flag5)
								{
									for (int i = 0; i < array.Length; i++)
									{
										Object value = array.GetValue(i);
										OdiumConsole.LogGradient("PhotonEvent", ((value != null) ? value.ToString() : null) ?? "null", LogLevel.Info, false);
									}
								}
								else
								{
									OdiumConsole.LogGradient("PhotonEvent", "Array too short", LogLevel.Info, false);
								}
							}
							else
							{
								OdiumConsole.LogGradient("PhotonEvent", __1.ToString(), LogLevel.Info, false);
							}
						}
						else
						{
							OdiumConsole.LogGradient("PhotonEvent", "Event data is null", LogLevel.Info, false);
						}
					}
					catch (Exception ex)
					{
						OdiumConsole.Log("PhotonEvent", "Error logging Photon event 43: " + ex.Message, LogLevel.Error);
					}
					result = true;
				}
			}
			return result;
		}
	}
}

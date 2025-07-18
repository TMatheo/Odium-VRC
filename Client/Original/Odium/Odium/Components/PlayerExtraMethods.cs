using System;
using Il2CppSystem;
using Odium.ButtonAPI.QM;
using Odium.QMPages;
using Odium.Wrappers;
using UnityEngine;
using VRC;

namespace Odium.Components
{
	// Token: 0x02000068 RID: 104
	public class PlayerExtraMethods
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00018254 File Offset: 0x00016454
		public static void setInfiniteVoiceRange(Player player, bool state)
		{
			string selected_player_name = AppBot.get_selected_player_name();
			player = ApiUtils.GetPlayerByDisplayName(selected_player_name);
			try
			{
				if (state)
				{
					OdiumConsole.Log("SetInfiniteVoiceRange: ", "You are no longer listening to  " + selected_player_name, LogLevel.Info);
					Console.WriteLine("SetInfiniteVoiceRange: You are no longer listening to  " + selected_player_name);
					player.field_Private_VRCPlayerApi_0.SetVoiceDistanceFar(25f);
				}
				else
				{
					OdiumConsole.Log("SetInfiniteVoiceRange: ", "Listening to  " + selected_player_name, LogLevel.Info);
					Console.WriteLine("SetInfiniteVoiceRange: Listening to  " + selected_player_name);
					player.field_Private_VRCPlayerApi_0.SetVoiceDistanceFar(float.PositiveInfinity);
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("PlayerExtraMethods: ", "SetInfiniteVoiceRange shat itself.", LogLevel.Warning);
				OdiumConsole.LogException(ex, null);
				string str = "PlayerExtraMethods: ";
				Exception ex2 = ex;
				Console.WriteLine(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001833C File Offset: 0x0001653C
		public static void focusTargetAudio(Player targetPlayer, bool state)
		{
			float defaultVoiceGain = 0f;
			try
			{
				if (state)
				{
					defaultVoiceGain = targetPlayer.field_Private_VRCPlayerApi_0.GetVoiceGain();
					targetPlayer.field_Private_VRCPlayerApi_0.SetVoiceDistanceFar(float.PositiveInfinity);
					PlayerWrapper.Players.ForEach(delegate(Player player)
					{
						player.field_Private_VRCPlayerApi_0.SetVoiceGain(0f);
					});
				}
				else
				{
					targetPlayer.field_Private_VRCPlayerApi_0.SetVoiceDistanceFar(25f);
					PlayerWrapper.Players.ForEach(delegate(Player player)
					{
						player.field_Private_VRCPlayerApi_0.SetVoiceGain(defaultVoiceGain);
					});
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("PlayerExtraMethods: ", "focusTargetAudio shat itself.", LogLevel.Warning);
				OdiumConsole.LogException(ex, null);
				string str = "PlayerExtraMethods: ";
				Exception ex2 = ex;
				Console.WriteLine(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001842C File Offset: 0x0001662C
		public static void teleportBehind(Player targetPlayer)
		{
			Vector3 position = targetPlayer.transform.position;
			Vector3 forward = targetPlayer.transform.forward;
			float d = 2f;
			Vector3 vector = position - forward * d;
			PlayerWrapper.LocalPlayer.transform.position = vector;
			Vector3 normalized = (position - vector).normalized;
			PlayerWrapper.LocalPlayer.transform.rotation = Quaternion.LookRotation(normalized);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000184A0 File Offset: 0x000166A0
		public static void teleportTo(Player targetPlayer)
		{
			PlayerWrapper.LocalPlayer.transform.position = targetPlayer.transform.position;
		}
	}
}

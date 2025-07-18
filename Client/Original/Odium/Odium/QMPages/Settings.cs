using System;
using Odium.ButtonAPI.QM;
using Odium.Components;
using Odium.Odium;
using Odium.UI;
using Odium.Wrappers;
using UnityEngine;
using VRC;

namespace Odium.QMPages
{
	// Token: 0x02000025 RID: 37
	internal class Settings
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00009E20 File Offset: 0x00008020
		public static void InitializePage(QMNestedMenu movementButton, Sprite buttonImage)
		{
			new QMToggleButton(movementButton, 1.5f, 1.5f, "Announce Blocks", delegate()
			{
				AssignedVariables.announceBlocks = true;
			}, delegate()
			{
				AssignedVariables.announceBlocks = false;
			}, "Toggle Flight Mode", false, buttonImage);
			new QMToggleButton(movementButton, 2.5f, 1.5f, "Announce Mutes", delegate()
			{
				AssignedVariables.announceMutes = true;
			}, delegate()
			{
				AssignedVariables.announceMutes = false;
			}, "Toggle Flight Mode", false, buttonImage);
			new QMToggleButton(movementButton, 3.5f, 1.5f, "Desktop Playerlist", delegate()
			{
				AssignedVariables.desktopPlayerList = true;
				PlayerWrapper.Players.ForEach(delegate(Player player)
				{
					PlayerRankTextDisplay.AddPlayer(player.prop_IUser_0.prop_String_1, PlayerWrapper.GetVRCPlayerFromId(player.prop_IUser_0.prop_String_0)._player.field_Private_APIUser_0, PlayerWrapper.GetVRCPlayerFromId(player.prop_IUser_0.prop_String_0)._player);
				});
			}, delegate()
			{
				AssignedVariables.desktopPlayerList = false;
				PlayerWrapper.Players.ForEach(delegate(Player player)
				{
					PlayerRankTextDisplay.RemovePlayer(player.prop_IUser_0.prop_String_1);
				});
				PlayerRankTextDisplay.ClearAll();
			}, "Toggle Flight Mode", true, buttonImage);
			new QMSingleButton(movementButton, 2.5f, 2.5f, "Remove Ads", delegate()
			{
				AdBlock.OnQMInit();
			}, "", false, null, buttonImage);
		}
	}
}

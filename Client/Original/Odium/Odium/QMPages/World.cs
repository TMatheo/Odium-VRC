using System;
using Odium.ButtonAPI.QM;
using Odium.Components;
using Odium.Wrappers;
using UnityEngine;

namespace Odium.QMPages
{
	// Token: 0x02000027 RID: 39
	internal class World
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000A058 File Offset: 0x00008258
		public static void InitializePage(QMNestedMenu worldButton, Sprite buttonImage)
		{
			Sprite sprite = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\PickupsIcon.png", 100f);
			QMNestedMenu qmnestedMenu = new QMNestedMenu(worldButton, 1f, 3f, "<color=#8d142b>Pickups</color>", "<color=#8d142b>Pickups</color>", "Opens Select User menu", false, null, buttonImage);
			new QMToggleButton(worldButton, 1f, 0f, "Drone Swarm", delegate()
			{
				DroneSwarmWrapper.isSwarmActive = true;
				DroneSwarmWrapper.ChangeSwarmTarget(PlayerWrapper.LocalPlayer.gameObject);
			}, delegate()
			{
				DroneSwarmWrapper.isSwarmActive = false;
			}, "Swarms your player with every available drone in the instance", false, buttonImage);
			new QMSingleButton(worldButton, 2f, 0f, "Drop Drones", delegate()
			{
				PickupWrapper.DropDronePickups();
			}, "Drop all drones in the instance", false, null, buttonImage);
			new QMSingleButton(qmnestedMenu, 1f, 0f, "Drop Pickups", delegate()
			{
				PickupWrapper.DropAllPickups();
			}, "Drop all pickups in the instance", false, null, buttonImage);
			new QMSingleButton(qmnestedMenu, 2f, 0f, "Bring Pickups", delegate()
			{
				PickupWrapper.BringAllPickupsToPlayer(PlayerWrapper.LocalPlayer);
			}, "Brings all pickups in the instance", false, null, buttonImage);
			new QMSingleButton(qmnestedMenu, 3f, 0f, "Respawn Pickups", delegate()
			{
				PickupWrapper.RespawnAllPickups();
			}, "Brings all pickups in the instance", false, null, buttonImage);
			World.hidePickupsToggle = new QMToggleButton(qmnestedMenu, 1f, 3f, "Hide Pickups", delegate()
			{
				PickupWrapper.HideAllPickups();
				World.hidePickupsToggle.SetButtonText("Show Pickups");
			}, delegate()
			{
				PickupWrapper.ShowAllPickups();
				World.hidePickupsToggle.SetButtonText("Hide Pickups");
			}, "Toggle visibility of all pickups in the instance", false, buttonImage);
		}

		// Token: 0x04000071 RID: 113
		public static QMToggleButton hidePickupsToggle;
	}
}

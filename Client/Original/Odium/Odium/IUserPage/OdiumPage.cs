using System;
using System.Windows.Forms;
using Odium.ButtonAPI.QM;
using Odium.Components;
using Odium.GameCheats;
using Odium.Patches;
using Odium.Wrappers;
using UnityEngine;
using VRC;

namespace Odium.IUserPage
{
	// Token: 0x02000039 RID: 57
	internal class OdiumPage
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000CF54 File Offset: 0x0000B154
		public static QMNestedMenu Initialize(QMNestedMenu qMNestedMenu1, Sprite bgImage)
		{
			Sprite icon = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\TeleportIcon.png", 100f);
			Sprite sprite = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\GoHomeIcon.png", 100f);
			Sprite sprite2 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\JoinMeIcon.png", 100f);
			Sprite sprite3 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\OrbitIcon.png", 100f);
			Sprite sprite4 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\CogWheelIcon.png", 100f);
			Sprite sprite5 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\MovementIcon.png", 100f);
			Sprite sprite6 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\OdiumIcon.png", 100f);
			Sprite icon2 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\InfoIcon.png", 100f);
			Sprite sprite7 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Murder.png", 100f);
			QMNestedMenu result = new QMNestedMenu(qMNestedMenu1, 1f, 1f, "App Bots", "<color=#8d142b>App Bots</color>", "Opens Select User menu", false, null, bgImage);
			QMNestedMenu qmnestedMenu = new QMNestedMenu(qMNestedMenu1, 2f, 1f, "Pickups", "<color=#8d142b>Pickups</color>", "Opens Select User menu", false, null, bgImage);
			QMNestedMenu qmnestedMenu2 = new QMNestedMenu(qMNestedMenu1, 3f, 1f, "Functions", "<color=#8d142b>Functions</color>", "Opens Select User menu", false, null, bgImage);
			QMNestedMenu location = new QMNestedMenu(qMNestedMenu1, 4f, 1f, "Spy Utils", "<color=#8d142b>Spy Utils</color>", "Opens Select User menu", false, null, bgImage);
			QMNestedMenu location2 = new QMNestedMenu(qMNestedMenu1, 2.5f, 2f, "Murder 4", "<color=#8d142b>Murder 4</color>", "Opens Select User menu", false, sprite7, bgImage);
			Sprite sprite8 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Kill.png", 100f);
			Sprite sprite9 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Win.png", 100f);
			Sprite sprite10 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\People.png", 100f);
			Sprite sprite11 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\Gun.png", 100f);
			Sprite sprite12 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\WorldIcon.png", 100f);
			Sprite sprite13 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\FTAC.png", 100f);
			QMNestedMenu btnMenu = new QMNestedMenu(location2, 2f, 0f, "<color=#8d142b>Win Triggers</color>", "<color=#8d142b>Win Triggers</color>", "Opens Select User menu", false, sprite9, bgImage);
			QMNestedMenu btnMenu2 = new QMNestedMenu(location2, 3f, 0f, "<color=#8d142b>Player Actions</color>", "<color=#8d142b>Player Actions</color>", "Opens Select User menu", false, sprite10, bgImage);
			QMNestedMenu btnMenu3 = new QMNestedMenu(location2, 2.5f, 1f, "<color=#8d142b>Exploits</color>", "<color=#8d142b>Exploits</color>", "Opens Select User menu", false, SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\ExploitIcon.png", 100f), bgImage);
			new QMSingleButton(btnMenu3, 2.5f, 1.5f, "Crash", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				PhotonPatches.BlockUdon = true;
				for (int i = 0; i < 300; i++)
				{
					Murder4Utils.SendTargetedPatreonEvent(iuser, "ListPatrons");
				}
				PhotonPatches.BlockUdon = false;
			}, "Brings death to all players", false, null, bgImage);
			new QMSingleButton(btnMenu, 2f, 2f, "Assign Murderer", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				Murder4Utils.SendTargetedEvent(iuser, "SyncAssignM");
			}, "Brings death to all players", false, sprite9, bgImage);
			new QMSingleButton(btnMenu, 3f, 2f, "Assign Bystander", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				Murder4Utils.SendTargetedEvent(iuser, "SyncAssignB");
			}, "Brings death to all players", false, sprite9, bgImage);
			new QMSingleButton(btnMenu2, 1f, 0f, "Assign Detective", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				Murder4Utils.SendTargetedEvent(iuser, "SyncAssignD");
			}, "Brings death to all players", false, null, bgImage);
			new QMSingleButton(btnMenu2, 2f, 0f, "Blind", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				Murder4Utils.SendTargetedEvent(iuser, "SyncAssignD");
			}, "Brings death to all players", false, null, bgImage);
			new QMSingleButton(btnMenu2, 3f, 0f, "Explode", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				Murder4Utils.ExplodeAtTarget(iuser);
			}, "Brings death to all players", false, null, bgImage);
			new QMToggleButton(location, 1.5f, 2f, "Spy USpeak", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				PlayerExtraMethods.focusTargetAudio(iuser, true);
			}, delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				PlayerExtraMethods.focusTargetAudio(iuser, false);
			}, "Focus audio on a single user and mutes everyone else", false, bgImage);
			new QMToggleButton(location, 2.5f, 2f, "Max Voice Range", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				PlayerExtraMethods.setInfiniteVoiceRange(iuser, true);
			}, delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				PlayerExtraMethods.setInfiniteVoiceRange(iuser, false);
			}, "Hear people from whatever distance they are", false, bgImage);
			new QMToggleButton(location, 3.5f, 2f, "Spy Camera", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				SpyCamera.Toggle(iuser.field_Private_VRCPlayerApi_0, true);
			}, delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				SpyCamera.Toggle(iuser.field_Private_VRCPlayerApi_0, false);
			}, "Allows to see from the point of view of other users", false, bgImage);
			new QMSingleButton(qmnestedMenu2, 1.5f, 1f, "TP Behind", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				PlayerExtraMethods.teleportBehind(iuser);
			}, "Teleport behind selected player facing them", false, icon, bgImage);
			new QMToggleButton(qmnestedMenu2, 2.5f, 1f, "Portal Spam", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				ActionWrapper.portalSpam = true;
				ActionWrapper.portalSpamPlayer = iuser;
			}, delegate()
			{
				ActionWrapper.portalSpam = false;
				ActionWrapper.portalSpamPlayer = null;
			}, "Spams portals on the target, be careful your name is still shown", false, bgImage);
			new QMSingleButton(qmnestedMenu2, 3.5f, 1f, "TP To", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				PlayerExtraMethods.teleportTo(iuser);
			}, "Teleport behind selected player facing them", false, icon, bgImage);
			new QMSingleButton(qmnestedMenu2, 1.5f, 2f, "Copy ID", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				Clipboard.SetText(iuser.field_Private_APIUser_0.id.ToString());
			}, "Copy the id of the avatar the selected user is wearing", false, icon2, bgImage);
			new QMSingleButton(qmnestedMenu2, 2.5f, 2f, "Copy Avatar ID", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				Clipboard.SetText(iuser.prop_ApiAvatar_0.id.ToString());
			}, "Copies to clipboard the selected user name", false, icon2, bgImage);
			new QMSingleButton(qmnestedMenu2, 3.5f, 2f, "Copy Display Name", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				Clipboard.SetText(iuser.prop_APIUser_0.displayName.ToString());
			}, "Teleport behind selected player facing them", false, icon2, bgImage);
			new QMSingleButton(qmnestedMenu, 1.5f, 1.5f, "Bring Pickups", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				PickupWrapper.BringAllPickupsToPlayer(iuser);
			}, "Bring all pickups in world to your position", false, icon, bgImage);
			new QMToggleButton(qmnestedMenu, 2.5f, 1.5f, "Pickup Swastika", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				SwasticaOrbit.Activated(iuser, true);
			}, delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				SwasticaOrbit.Activated(iuser, false);
			}, "Creates a Swastika with pickups and places it on top of the selected user.", false, bgImage);
			new QMToggleButton(qmnestedMenu, 3.5f, 1.5f, "Drone Swarm", delegate()
			{
				Player iuser = ApiUtils.GetIUser();
				DroneSwarmWrapper.isSwarmActive = true;
				DroneSwarmWrapper.ChangeSwarmTarget(iuser.gameObject);
			}, delegate()
			{
				DroneSwarmWrapper.isSwarmActive = false;
			}, "Swarms your player with every available drone in the instance", false, bgImage);
			return result;
		}

		// Token: 0x040000B1 RID: 177
		public static float defaultVoiceGain;
	}
}

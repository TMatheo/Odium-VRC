using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRC.Udon;

namespace Odium.GameCheats
{
	// Token: 0x0200003D RID: 61
	internal static class Murder4Utils
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		private static void ExecuteDoorAction(string action)
		{
			(from go in Object.FindObjectsOfType<GameObject>()
			where go.name.StartsWith("Door")
			select go).ToList<GameObject>().ForEach(delegate(GameObject door)
			{
				Transform transform = door.transform.Find("Door Anim/Hinge/Interact " + action);
				if (transform != null)
				{
					UdonBehaviour component = transform.GetComponent<UdonBehaviour>();
					if (component != null)
					{
						component.Interact();
					}
				}
			});
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000DA52 File Offset: 0x0000BC52
		public static void OpenDoors()
		{
			Murder4Utils.ExecuteDoorAction("open");
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000DA5F File Offset: 0x0000BC5F
		public static void CloseDoors()
		{
			Murder4Utils.ExecuteDoorAction("close");
			Murder4Utils.LockDoors();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000DA73 File Offset: 0x0000BC73
		public static void LockDoors()
		{
			Murder4Utils.ExecuteDoorAction("lock");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000DA80 File Offset: 0x0000BC80
		public static void ForceOpenDoors()
		{
			(from go in Object.FindObjectsOfType<GameObject>()
			where go.name.StartsWith("Door")
			select go).ToList<GameObject>().ForEach(delegate(GameObject door)
			{
				Transform transform = door.transform.Find("Door Anim/Hinge/Interact shove");
				UdonBehaviour udonBehaviour = (transform != null) ? transform.GetComponent<UdonBehaviour>() : null;
				for (int i = 0; i < 4; i++)
				{
					if (udonBehaviour != null)
					{
						udonBehaviour.Interact();
					}
				}
			});
			Murder4Utils.OpenDoors();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000DAE7 File Offset: 0x0000BCE7
		private static void SendGameEvent(string eventName)
		{
			GameObject gameObject = GameObject.Find("Game Logic");
			if (gameObject != null)
			{
				UdonBehaviour component = gameObject.GetComponent<UdonBehaviour>();
				if (component != null)
				{
					component.SendCustomNetworkEvent(0, eventName);
				}
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		private static void SendWeaponEvent(string weaponPath, string eventName)
		{
			GameObject gameObject = GameObject.Find(weaponPath);
			if (gameObject != null)
			{
				UdonBehaviour component = gameObject.GetComponent<UdonBehaviour>();
				if (component != null)
				{
					component.SendCustomNetworkEvent(0, eventName);
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000DB2D File Offset: 0x0000BD2D
		private static void SendPatreonEvent(string eventName)
		{
			GameObject gameObject = GameObject.Find("Patreon Credits");
			if (gameObject != null)
			{
				UdonBehaviour component = gameObject.GetComponent<UdonBehaviour>();
				if (component != null)
				{
					component.SendCustomNetworkEvent(0, eventName);
				}
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000DB52 File Offset: 0x0000BD52
		public static void StartGame()
		{
			Murder4Utils.SendGameEvent("Btn_Start");
			Murder4Utils.SendGameEvent("SyncStartGame");
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000DB6B File Offset: 0x0000BD6B
		public static void AbortGame()
		{
			Murder4Utils.SendGameEvent("SyncAbort");
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public static void RefreshRoles()
		{
			Murder4Utils.SendGameEvent("OnLocalPlayerAssignedRole");
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000DB85 File Offset: 0x0000BD85
		public static void TriggerBystanderWin()
		{
			Murder4Utils.SendGameEvent("SyncVictoryB");
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000DB92 File Offset: 0x0000BD92
		public static void TriggerMurdererWin()
		{
			Murder4Utils.SendGameEvent("SyncVictoryM");
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000DB9F File Offset: 0x0000BD9F
		public static void ExecuteAll()
		{
			Murder4Utils.SendGameEvent("KillLocalPlayer");
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		public static void BlindAll()
		{
			Murder4Utils.SendGameEvent("OnLocalPlayerBlinded");
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000DBB9 File Offset: 0x0000BDB9
		public static void FlashAll()
		{
			Murder4Utils.SendGameEvent("OnLocalPlayerFlashbanged");
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		private static void TeleportWeapon(string path)
		{
			GameObject gameObject = GameObject.Find(path);
			bool flag = gameObject == null;
			if (!flag)
			{
				Networking.SetOwner(Networking.LocalPlayer, gameObject);
				gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + Vector3.up * 0.1f;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000DC2A File Offset: 0x0000BE2A
		public static void GetRevolver()
		{
			Murder4Utils.TeleportWeapon("Game Logic/Weapons/Revolver");
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000DC37 File Offset: 0x0000BE37
		public static void GetShotgun()
		{
			Murder4Utils.TeleportWeapon("Game Logic/Weapons/Unlockables/Shotgun (0)");
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public static void GetLuger()
		{
			Murder4Utils.TeleportWeapon("Game Logic/Weapons/Unlockables/Luger (0)");
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000DC51 File Offset: 0x0000BE51
		public static void GetSmoke()
		{
			Murder4Utils.TeleportWeapon("Game Logic/Weapons/Unlockables/Smoke (0)");
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000DC5E File Offset: 0x0000BE5E
		public static void GetCamera()
		{
			Murder4Utils.TeleportWeapon("Game Logic/Polaroids Unlock Camera/FlashCamera");
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000DC6B File Offset: 0x0000BE6B
		public static void GetTraps()
		{
			Enumerable.Range(0, 3).ToList<int>().ForEach(delegate(int i)
			{
				Murder4Utils.TeleportWeapon(string.Format("Game Logic/Weapons/Bear Trap ({0})", i));
			});
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		public static void DeployFrag(VRCPlayer target, bool detonate = false)
		{
			GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
			bool flag = gameObject != null;
			if (flag)
			{
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
				gameObject.transform.position = target.transform.position + Vector3.up * 0.1f;
				if (detonate)
				{
					Murder4Utils.DetonateFrag();
				}
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000DD0D File Offset: 0x0000BF0D
		public static IEnumerator CreateKnifeShield(VRCPlayer player)
		{
			Murder4Utils.<CreateKnifeShield>d__25 <CreateKnifeShield>d__ = new Murder4Utils.<CreateKnifeShield>d__25(0);
			<CreateKnifeShield>d__.player = player;
			return <CreateKnifeShield>d__;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		public static void FireShotgun()
		{
			Murder4Utils.SendWeaponEvent("Game Logic/Weapons/Unlockables/Shotgun (0)", "Fire");
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000DD2E File Offset: 0x0000BF2E
		public static void FireRevolver()
		{
			Murder4Utils.SendWeaponEvent("Game Logic/Weapons/Revolver", "Fire");
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public static void FireLuger()
		{
			Murder4Utils.SendWeaponEvent("Game Logic/Weapons/Unlockables/Luger (0)", "Fire");
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000DD52 File Offset: 0x0000BF52
		public static void DetonateFrag()
		{
			Murder4Utils.SendWeaponEvent("Game Logic/Weapons/Unlockables/Frag (0)", "Explode");
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public static void ApplyRevolverSkin()
		{
			Murder4Utils.SendWeaponEvent("Game Logic/Weapons/Revolver", "PatronSkin");
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000DD76 File Offset: 0x0000BF76
		public static void SpawnSnake()
		{
			Murder4Utils.SendWeaponEvent("Game Logic/Snakes/SnakeDispenser", "DispenseSnake");
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public static void IdentifyMurderer()
		{
			Transform transform = Resources.FindObjectsOfTypeAll<Transform>().FirstOrDefault((Transform t) => t.gameObject.name == "Murderer Name");
			GameObject gameObject = (transform != null) ? transform.gameObject : null;
			string str;
			if (gameObject == null)
			{
				str = null;
			}
			else
			{
				TextMeshProUGUI component = gameObject.GetComponent<TextMeshProUGUI>();
				str = ((component != null) ? component.text : null);
			}
			string text = str + ", Is the murder.";
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		public static void ExplodeAtTarget(Player target)
		{
			GameObject gameObject = GameObject.Find("Frag (0)");
			Networking.LocalPlayer.TakeOwnership(gameObject);
			gameObject.transform.position = target.transform.position;
			gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Explode");
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000DE3E File Offset: 0x0000C03E
		public static void BlindTarget(Player target)
		{
			Murder4Utils.SendTargetedEvent(target, "SyncFlashbang");
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000DE4C File Offset: 0x0000C04C
		public static void AssignRole(string username, string role)
		{
			for (int i = 0; i < 24; i++)
			{
				string text = GameObject.Find(string.Format("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry ({0})/Player Name Text", i)).GetComponent<TextMeshProUGUI>().text;
				bool flag = text == username;
				if (flag)
				{
					GameObject.Find(string.Format("Player Node ({0})", i)).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignM");
					break;
				}
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000DEC1 File Offset: 0x0000C0C1
		public static IEnumerator InitializeTheme()
		{
			return new Murder4Utils.<InitializeTheme>d__36(0);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public static void SendTargetedPatreonEvent(Player target, string eventName)
		{
			GameObject gameObject = GameObject.Find("Patreon Credits");
			gameObject.GetComponent<UdonBehaviour>().enabled = true;
			gameObject.SendUdon(eventName, target, false);
			gameObject.GetComponent<UdonBehaviour>().enabled = false;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000DF0C File Offset: 0x0000C10C
		public static void SendTargetedEvent(Player target, string eventName)
		{
			GameObject go = GameObject.Find("Game Logic");
			go.SendUdon(eventName, target, false);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000DF2F File Offset: 0x0000C12F
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000DF36 File Offset: 0x0000C136
		public static bool KnifeShieldActive
		{
			get
			{
				return Murder4Utils._knifeShieldActive;
			}
			set
			{
				Murder4Utils._knifeShieldActive = value;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000DF40 File Offset: 0x0000C140
		[CompilerGenerated]
		internal static void <InitializeTheme>g__SetRedText|36_0(string path, string text)
		{
			TextMeshProUGUI component = GameObject.Find(path).GetComponent<TextMeshProUGUI>();
			component.text = text;
			component.color = Color.red;
		}

		// Token: 0x040000B2 RID: 178
		private static bool _knifeShieldActive;
	}
}

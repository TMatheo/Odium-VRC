using System;
using Valve.VR;

namespace Odium.Components
{
	// Token: 0x02000055 RID: 85
	public class Bindings
	{
		// Token: 0x06000244 RID: 580 RVA: 0x000145AC File Offset: 0x000127AC
		public static void Register()
		{
			Bindings.Button_Jump = SteamVR_Input.GetBooleanAction("jump", false);
			Bindings.Button_Mic = SteamVR_Input.GetBooleanAction("Toggle Microphone", false);
			Bindings.Button_QM = SteamVR_Input.GetBooleanAction("Menu", false);
			Bindings.Button_Grab = SteamVR_Input.GetBooleanAction("Grab", false);
			Bindings.Button_Interact = SteamVR_Input.GetBooleanAction("Interact", false);
			Bindings.Trigger = SteamVR_Input.GetSingleAction("gesture_trigger_axis", false);
			Bindings.MoveJoystick = SteamVR_Input.GetVector2Action("Move", false);
			Bindings.RotateJoystick = SteamVR_Input.GetVector2Action("Rotate", false);
			Console.WriteLine("VRBinds: Binds Registered.");
		}

		// Token: 0x04000113 RID: 275
		public static SteamVR_Action_Boolean Button_Jump;

		// Token: 0x04000114 RID: 276
		public static SteamVR_Action_Boolean Button_QM;

		// Token: 0x04000115 RID: 277
		public static SteamVR_Action_Boolean Button_Mic;

		// Token: 0x04000116 RID: 278
		public static SteamVR_Action_Boolean Button_Grab;

		// Token: 0x04000117 RID: 279
		public static SteamVR_Action_Boolean Button_Interact;

		// Token: 0x04000118 RID: 280
		public static SteamVR_Action_Single Trigger;

		// Token: 0x04000119 RID: 281
		public static SteamVR_Action_Vector2 MoveJoystick;

		// Token: 0x0400011A RID: 282
		public static SteamVR_Action_Vector2 RotateJoystick;
	}
}

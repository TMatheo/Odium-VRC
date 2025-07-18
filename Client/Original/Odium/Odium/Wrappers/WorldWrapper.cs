using System;

namespace Odium.Wrappers
{
	// Token: 0x02000048 RID: 72
	internal class WorldWrapper
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000F424 File Offset: 0x0000D624
		public static bool IsInRoom()
		{
			return RoomManager.field_Internal_Static_ApiWorld_0 != null && RoomManager.field_Private_Static_ApiWorldInstance_0 != null;
		}
	}
}

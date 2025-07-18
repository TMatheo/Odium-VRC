using System;
using ExitGames.Client.Photon;

namespace Odium.Patches
{
	// Token: 0x0200002F RID: 47
	public class PhotonDebugger
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public static bool OnEventSent(byte code, object data, RaiseEventOptions options, SendOptions sendOptions)
		{
			Console.WriteLine("Photon:OnEventSent----------------------");
			Console.WriteLine("Photon:OnEventSent" + string.Format("Code:{0}", code));
			Console.WriteLine("Photon:OnEventSent" + string.Format("Data:{0}", data));
			Console.WriteLine("Photon:OnEventSent" + string.Format("Data:{0}", data));
			Console.WriteLine("Photon:OnEventSent" + string.Format("RaiseEventOptions:{0}", options));
			Console.WriteLine("Photon:OnEventSent" + string.Format("SendOptions:{0}", sendOptions));
			Console.WriteLine("Photon:OnEventSent----------------------");
			return true;
		}

		// Token: 0x04000079 RID: 121
		public static bool IsOnEventSendDebug;
	}
}

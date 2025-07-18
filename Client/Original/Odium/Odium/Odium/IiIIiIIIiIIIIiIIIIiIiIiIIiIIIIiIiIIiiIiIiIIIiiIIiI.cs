using System;
using System.Collections;
using System.Net.Http;
using MelonLoader;

namespace Odium.Odium
{
	// Token: 0x02000036 RID: 54
	internal class IiIIiIIIiIIIIiIIIIiIiIiIIiIIIIiIiIIiiIiIiIIIiiIIiI
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000CE95 File Offset: 0x0000B095
		public static void IiIIiIIIIIIIIIIIIiiiiiiIIIIiIIiIiIIIiiIiiIiIiIiiIiIiIiIIiIiIIIiiiIIIIIiIIiIiIiIiiIIIiiIiiiiiiiiIiiIIIiIiiiiIIIIIiII(string userId, string username, string hexColor)
		{
			MelonCoroutines.Start(IiIIiIIIiIIIIiIIIIiIiIiIIiIIIIiIiIIiiIiIiIIIiiIIiI.CheckBanStatusHttpClient(userId, username, hexColor));
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000CEA6 File Offset: 0x0000B0A6
		private static IEnumerator CheckBanStatusHttpClient(string userId, string username, string hexColor)
		{
			IiIIiIIIiIIIIiIIIIiIiIiIIiIIIIiIiIIiiIiIiIIIiiIIiI.<CheckBanStatusHttpClient>d__3 <CheckBanStatusHttpClient>d__ = new IiIIiIIIiIIIIiIIIIiIiIiIIiIIIIiIiIIiiIiIiIIIiiIIiI.<CheckBanStatusHttpClient>d__3(0);
			<CheckBanStatusHttpClient>d__.userId = userId;
			<CheckBanStatusHttpClient>d__.username = username;
			<CheckBanStatusHttpClient>d__.hexColor = hexColor;
			return <CheckBanStatusHttpClient>d__;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000CEC3 File Offset: 0x0000B0C3
		private static IEnumerator RegisterUserHttpClient(string userId, string username, string hexColor)
		{
			IiIIiIIIiIIIIiIIIIiIiIiIIiIIIIiIiIIiiIiIiIIIiiIIiI.<RegisterUserHttpClient>d__4 <RegisterUserHttpClient>d__ = new IiIIiIIIiIIIIiIIIIiIiIiIIiIIIIiIiIIiiIiIiIIIiiIIiI.<RegisterUserHttpClient>d__4(0);
			<RegisterUserHttpClient>d__.userId = userId;
			<RegisterUserHttpClient>d__.username = username;
			<RegisterUserHttpClient>d__.hexColor = hexColor;
			return <RegisterUserHttpClient>d__;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		private static void ShowBanPopup()
		{
			VRCUiManager.field_Private_Static_VRCUiManager_0.Method_Public_Void_String_Single_Action_PDM_0("You have been banned from using Odium. The application will close in 3 seconds.", 3f, null);
			MelonCoroutines.Start(IiIIiIIIiIIIIiIIIIiIiIiIIiIIIIiIiIIiiIiIiIIIiiIIiI.QuitApplicationDelayed());
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000CF04 File Offset: 0x0000B104
		private static IEnumerator QuitApplicationDelayed()
		{
			return new IiIIiIIIiIIIIiIIIIiIiIiIIiIIIIiIiIIiiIiIiIIIiiIIiI.<QuitApplicationDelayed>d__6(0);
		}

		// Token: 0x040000A7 RID: 167
		private static string apiUrl = "https://snoofz.net";

		// Token: 0x040000A8 RID: 168
		private static readonly HttpClient httpClient = new HttpClient
		{
			Timeout = TimeSpan.FromSeconds(10.0)
		};
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Odium.ApplicationBot
{
	// Token: 0x0200008C RID: 140
	internal class Entry
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x00021028 File Offset: 0x0001F228
		public static string LaunchBotLogin(int profile)
		{
			string result;
			try
			{
				string text = Guid.NewGuid().ToString();
				string fileName = Path.Combine(Directory.GetCurrentDirectory(), "launch.exe");
				string arguments = string.Format("--profile={0} --id={1} --appBot --fps=25 --no-vr", profile, text);
				Process.Start(fileName, arguments);
				Entry.ActiveBotIds.Add(text);
				OdiumConsole.LogGradient("ApplicationBot", string.Format("Launching login bot on profile {0} with ID: {1}", profile, text), LogLevel.Info, false);
				result = text;
			}
			catch (Exception ex)
			{
				OdiumConsole.LogGradient("ApplicationBot", "Failed to launch bot: " + ex.Message, LogLevel.Info, false);
				result = null;
			}
			return result;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000210DC File Offset: 0x0001F2DC
		public static string LaunchBotHeadless(int profile)
		{
			string result;
			try
			{
				string text = Guid.NewGuid().ToString();
				string fileName = Path.Combine(Directory.GetCurrentDirectory(), "launch.exe");
				string arguments = string.Format("--profile={0} --id={1} --appBot --fps=25 --no-vr -batchmode -noUpm -nographics -disable-gpu-skinning -no-stereo-rendering -nolog", profile, text);
				Process.Start(fileName, arguments);
				Entry.ActiveBotIds.Add(text);
				OdiumConsole.LogGradient("ApplicationBot", string.Format("Launching headless bot on profile {0} with ID: {1}", profile, text), LogLevel.Info, false);
				result = text;
			}
			catch (Exception ex)
			{
				OdiumConsole.LogGradient("ApplicationBot", "Failed to launch headless bot: " + ex.Message, LogLevel.Info, false);
				result = null;
			}
			return result;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00021190 File Offset: 0x0001F390
		public static bool RemoveBotId(string botId)
		{
			return Entry.ActiveBotIds.Remove(botId);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000211B0 File Offset: 0x0001F3B0
		public static List<string> GetActiveBotIds()
		{
			return new List<string>(Entry.ActiveBotIds);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000211CC File Offset: 0x0001F3CC
		public static bool IsBotActive(string botId)
		{
			return Entry.ActiveBotIds.Contains(botId);
		}

		// Token: 0x0400020E RID: 526
		public static List<string> ActiveBotIds = new List<string>();
	}
}

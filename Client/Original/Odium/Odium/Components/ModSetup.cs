using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Odium.Components
{
	// Token: 0x02000060 RID: 96
	public static class ModSetup
	{
		// Token: 0x06000278 RID: 632 RVA: 0x000161B0 File Offset: 0x000143B0
		[DebuggerStepThrough]
		public static Task Initialize()
		{
			ModSetup.<Initialize>d__12 <Initialize>d__ = new ModSetup.<Initialize>d__12();
			<Initialize>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<ModSetup.<Initialize>d__12>(ref <Initialize>d__);
			return <Initialize>d__.<>t__builder.Task;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000161F0 File Offset: 0x000143F0
		[DebuggerStepThrough]
		private static Task CheckAndCreateOdiumFolder()
		{
			ModSetup.<CheckAndCreateOdiumFolder>d__13 <CheckAndCreateOdiumFolder>d__ = new ModSetup.<CheckAndCreateOdiumFolder>d__13();
			<CheckAndCreateOdiumFolder>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CheckAndCreateOdiumFolder>d__.<>1__state = -1;
			<CheckAndCreateOdiumFolder>d__.<>t__builder.Start<ModSetup.<CheckAndCreateOdiumFolder>d__13>(ref <CheckAndCreateOdiumFolder>d__);
			return <CheckAndCreateOdiumFolder>d__.<>t__builder.Task;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00016230 File Offset: 0x00014430
		[DebuggerStepThrough]
		private static Task CheckAndCreatePreferencesFile()
		{
			ModSetup.<CheckAndCreatePreferencesFile>d__14 <CheckAndCreatePreferencesFile>d__ = new ModSetup.<CheckAndCreatePreferencesFile>d__14();
			<CheckAndCreatePreferencesFile>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CheckAndCreatePreferencesFile>d__.<>1__state = -1;
			<CheckAndCreatePreferencesFile>d__.<>t__builder.Start<ModSetup.<CheckAndCreatePreferencesFile>d__14>(ref <CheckAndCreatePreferencesFile>d__);
			return <CheckAndCreatePreferencesFile>d__.<>t__builder.Task;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00016270 File Offset: 0x00014470
		[DebuggerStepThrough]
		private static Task<bool> CreateDefaultPreferencesFile()
		{
			ModSetup.<CreateDefaultPreferencesFile>d__15 <CreateDefaultPreferencesFile>d__ = new ModSetup.<CreateDefaultPreferencesFile>d__15();
			<CreateDefaultPreferencesFile>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CreateDefaultPreferencesFile>d__.<>1__state = -1;
			<CreateDefaultPreferencesFile>d__.<>t__builder.Start<ModSetup.<CreateDefaultPreferencesFile>d__15>(ref <CreateDefaultPreferencesFile>d__);
			return <CreateDefaultPreferencesFile>d__.<>t__builder.Task;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000162B0 File Offset: 0x000144B0
		[DebuggerStepThrough]
		private static Task ValidatePreferencesFile()
		{
			ModSetup.<ValidatePreferencesFile>d__16 <ValidatePreferencesFile>d__ = new ModSetup.<ValidatePreferencesFile>d__16();
			<ValidatePreferencesFile>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ValidatePreferencesFile>d__.<>1__state = -1;
			<ValidatePreferencesFile>d__.<>t__builder.Start<ModSetup.<ValidatePreferencesFile>d__16>(ref <ValidatePreferencesFile>d__);
			return <ValidatePreferencesFile>d__.<>t__builder.Task;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000162F0 File Offset: 0x000144F0
		private static bool AssetsDownloaded()
		{
			bool result;
			try
			{
				bool flag = File.Exists(ModSetup.ButtonBackgroundPath);
				bool flag2 = File.Exists(ModSetup.QMBackgroundPath);
				bool flag3 = Directory.Exists(ModSetup.AssetBundlesFolderPath) && Directory.GetFiles(ModSetup.AssetBundlesFolderPath).Length != 0;
				OdiumConsole.Log("ModSetup", string.Format("Asset check - ButtonBackground: {0}, QMBackground: {1}, AssetBundles folder has files: {2}", flag, flag2, flag3), LogLevel.Info);
				result = (flag && flag2 && flag3);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "AssetsDownloaded");
				result = false;
			}
			return result;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00016388 File Offset: 0x00014588
		[DebuggerStepThrough]
		private static Task<bool> DownloadAndExtractAssets()
		{
			ModSetup.<DownloadAndExtractAssets>d__18 <DownloadAndExtractAssets>d__ = new ModSetup.<DownloadAndExtractAssets>d__18();
			<DownloadAndExtractAssets>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DownloadAndExtractAssets>d__.<>1__state = -1;
			<DownloadAndExtractAssets>d__.<>t__builder.Start<ModSetup.<DownloadAndExtractAssets>d__18>(ref <DownloadAndExtractAssets>d__);
			return <DownloadAndExtractAssets>d__.<>t__builder.Task;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000163C5 File Offset: 0x000145C5
		public static string GetOdiumFolderPath()
		{
			return ModSetup.OdiumFolderPath;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000163CC File Offset: 0x000145CC
		public static string GetButtonBackgroundPath()
		{
			return ModSetup.ButtonBackgroundPath;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000163D3 File Offset: 0x000145D3
		public static string GetQMBackgroundPath()
		{
			return ModSetup.QMBackgroundPath;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000163DA File Offset: 0x000145DA
		public static string GetOdiumPrefsPath()
		{
			return ModSetup.OdiumPrefsPath;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000163E1 File Offset: 0x000145E1
		public static string GetQMHalfButtonPath()
		{
			return ModSetup.QMHalfButtonPath;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000163E8 File Offset: 0x000145E8
		public static string GetQMConsolePath()
		{
			return ModSetup.QMConsolePath;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000163EF File Offset: 0x000145EF
		public static string GetTabImagePath()
		{
			return ModSetup.TabImagePath;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000163F6 File Offset: 0x000145F6
		public static string GetNameplatePath()
		{
			return ModSetup.NameplatePath;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00016400 File Offset: 0x00014600
		public static bool ValidateSetup()
		{
			bool result;
			try
			{
				bool flag = Directory.Exists(ModSetup.OdiumFolderPath);
				bool flag2 = File.Exists(ModSetup.OdiumPrefsPath);
				bool flag3 = ModSetup.AssetsDownloaded();
				OdiumConsole.Log("ModSetup", string.Format("Setup validation - Folder: {0}, Preferences: {1}, Assets: {2}", flag, flag2, flag3), LogLevel.Info);
				result = (flag && flag2 && flag3);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "ValidateSetup");
				result = false;
			}
			return result;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00016480 File Offset: 0x00014680
		public static void CleanUp()
		{
			try
			{
				bool flag = Directory.Exists(ModSetup.OdiumFolderPath);
				if (flag)
				{
					Directory.Delete(ModSetup.OdiumFolderPath, true);
					OdiumConsole.Log("ModSetup", "Odium folder and contents deleted", LogLevel.Info);
				}
				bool flag2 = File.Exists(ModSetup.TempZipPath);
				if (flag2)
				{
					File.Delete(ModSetup.TempZipPath);
					OdiumConsole.Log("ModSetup", "Temp zip file cleaned up", LogLevel.Info);
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, "CleanUp");
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001650C File Offset: 0x0001470C
		[DebuggerStepThrough]
		public static Task ForceUpdateAllAssets()
		{
			ModSetup.<ForceUpdateAllAssets>d__29 <ForceUpdateAllAssets>d__ = new ModSetup.<ForceUpdateAllAssets>d__29();
			<ForceUpdateAllAssets>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceUpdateAllAssets>d__.<>1__state = -1;
			<ForceUpdateAllAssets>d__.<>t__builder.Start<ModSetup.<ForceUpdateAllAssets>d__29>(ref <ForceUpdateAllAssets>d__);
			return <ForceUpdateAllAssets>d__.<>t__builder.Task;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0001654C File Offset: 0x0001474C
		[DebuggerStepThrough]
		public static Task ForceRecreatePreferences()
		{
			ModSetup.<ForceRecreatePreferences>d__30 <ForceRecreatePreferences>d__ = new ModSetup.<ForceRecreatePreferences>d__30();
			<ForceRecreatePreferences>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceRecreatePreferences>d__.<>1__state = -1;
			<ForceRecreatePreferences>d__.<>t__builder.Start<ModSetup.<ForceRecreatePreferences>d__30>(ref <ForceRecreatePreferences>d__);
			return <ForceRecreatePreferences>d__.<>t__builder.Task;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001658C File Offset: 0x0001478C
		[DebuggerStepThrough]
		public static Task ForceUpdateQMBackground()
		{
			ModSetup.<ForceUpdateQMBackground>d__31 <ForceUpdateQMBackground>d__ = new ModSetup.<ForceUpdateQMBackground>d__31();
			<ForceUpdateQMBackground>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceUpdateQMBackground>d__.<>1__state = -1;
			<ForceUpdateQMBackground>d__.<>t__builder.Start<ModSetup.<ForceUpdateQMBackground>d__31>(ref <ForceUpdateQMBackground>d__);
			return <ForceUpdateQMBackground>d__.<>t__builder.Task;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000165CC File Offset: 0x000147CC
		[DebuggerStepThrough]
		public static Task ForceUpdateButtonBackground()
		{
			ModSetup.<ForceUpdateButtonBackground>d__32 <ForceUpdateButtonBackground>d__ = new ModSetup.<ForceUpdateButtonBackground>d__32();
			<ForceUpdateButtonBackground>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceUpdateButtonBackground>d__.<>1__state = -1;
			<ForceUpdateButtonBackground>d__.<>t__builder.Start<ModSetup.<ForceUpdateButtonBackground>d__32>(ref <ForceUpdateButtonBackground>d__);
			return <ForceUpdateButtonBackground>d__.<>t__builder.Task;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001660C File Offset: 0x0001480C
		[DebuggerStepThrough]
		public static Task ForceUpdateQMHalfButton()
		{
			ModSetup.<ForceUpdateQMHalfButton>d__33 <ForceUpdateQMHalfButton>d__ = new ModSetup.<ForceUpdateQMHalfButton>d__33();
			<ForceUpdateQMHalfButton>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceUpdateQMHalfButton>d__.<>1__state = -1;
			<ForceUpdateQMHalfButton>d__.<>t__builder.Start<ModSetup.<ForceUpdateQMHalfButton>d__33>(ref <ForceUpdateQMHalfButton>d__);
			return <ForceUpdateQMHalfButton>d__.<>t__builder.Task;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0001664C File Offset: 0x0001484C
		[DebuggerStepThrough]
		public static Task ForceUpdateQMConsole()
		{
			ModSetup.<ForceUpdateQMConsole>d__34 <ForceUpdateQMConsole>d__ = new ModSetup.<ForceUpdateQMConsole>d__34();
			<ForceUpdateQMConsole>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceUpdateQMConsole>d__.<>1__state = -1;
			<ForceUpdateQMConsole>d__.<>t__builder.Start<ModSetup.<ForceUpdateQMConsole>d__34>(ref <ForceUpdateQMConsole>d__);
			return <ForceUpdateQMConsole>d__.<>t__builder.Task;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001668C File Offset: 0x0001488C
		[DebuggerStepThrough]
		public static Task ForceUpdateTabImage()
		{
			ModSetup.<ForceUpdateTabImage>d__35 <ForceUpdateTabImage>d__ = new ModSetup.<ForceUpdateTabImage>d__35();
			<ForceUpdateTabImage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceUpdateTabImage>d__.<>1__state = -1;
			<ForceUpdateTabImage>d__.<>t__builder.Start<ModSetup.<ForceUpdateTabImage>d__35>(ref <ForceUpdateTabImage>d__);
			return <ForceUpdateTabImage>d__.<>t__builder.Task;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000166CC File Offset: 0x000148CC
		[DebuggerStepThrough]
		public static Task ForceUpdateNameplate()
		{
			ModSetup.<ForceUpdateNameplate>d__36 <ForceUpdateNameplate>d__ = new ModSetup.<ForceUpdateNameplate>d__36();
			<ForceUpdateNameplate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceUpdateNameplate>d__.<>1__state = -1;
			<ForceUpdateNameplate>d__.<>t__builder.Start<ModSetup.<ForceUpdateNameplate>d__36>(ref <ForceUpdateNameplate>d__);
			return <ForceUpdateNameplate>d__.<>t__builder.Task;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001670C File Offset: 0x0001490C
		[DebuggerStepThrough]
		public static Task ForceUpdateAllImages()
		{
			ModSetup.<ForceUpdateAllImages>d__37 <ForceUpdateAllImages>d__ = new ModSetup.<ForceUpdateAllImages>d__37();
			<ForceUpdateAllImages>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ForceUpdateAllImages>d__.<>1__state = -1;
			<ForceUpdateAllImages>d__.<>t__builder.Start<ModSetup.<ForceUpdateAllImages>d__37>(ref <ForceUpdateAllImages>d__);
			return <ForceUpdateAllImages>d__.<>t__builder.Task;
		}

		// Token: 0x04000127 RID: 295
		private static readonly string OdiumFolderPath = Path.Combine(Environment.CurrentDirectory, "Odium");

		// Token: 0x04000128 RID: 296
		private static readonly string AssetBundlesFolderPath = Path.Combine(Environment.CurrentDirectory, "Odium", "AssetBundles");

		// Token: 0x04000129 RID: 297
		private static readonly string OdiumPrefsPath = Path.Combine(ModSetup.OdiumFolderPath, "odium_prefs.json");

		// Token: 0x0400012A RID: 298
		private static readonly string QMBackgroundPath = Path.Combine(ModSetup.OdiumFolderPath, "ButtonBackground.png");

		// Token: 0x0400012B RID: 299
		private static readonly string ButtonBackgroundPath = Path.Combine(ModSetup.OdiumFolderPath, "QMBackground.png");

		// Token: 0x0400012C RID: 300
		private static readonly string QMHalfButtonPath = Path.Combine(ModSetup.OdiumFolderPath, "QMHalfButton.png");

		// Token: 0x0400012D RID: 301
		private static readonly string QMConsolePath = Path.Combine(ModSetup.OdiumFolderPath, "QMConsole.png");

		// Token: 0x0400012E RID: 302
		private static readonly string TabImagePath = Path.Combine(ModSetup.OdiumFolderPath, "OdiumIcon.png");

		// Token: 0x0400012F RID: 303
		private static readonly string NameplatePath = Path.Combine(ModSetup.OdiumFolderPath, "Nameplate.png");

		// Token: 0x04000130 RID: 304
		private static readonly string NotificationAssetBundlePath = Path.Combine(ModSetup.OdiumFolderPath, "AssetBundles", "notification");

		// Token: 0x04000131 RID: 305
		private const string AssetsZipUrl = "https://odiumvrc.com/files/odium-build-060.zip";

		// Token: 0x04000132 RID: 306
		private static readonly string TempZipPath = Path.Combine(Path.GetTempPath(), "odium_assets.zip");
	}
}

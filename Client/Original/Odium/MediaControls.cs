using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Odium;

// Token: 0x0200000B RID: 11
public class MediaControls
{
	// Token: 0x0600003A RID: 58
	[DllImport("user32.dll")]
	private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

	// Token: 0x0600003B RID: 59 RVA: 0x00003280 File Offset: 0x00001480
	[DebuggerStepThrough]
	public static Task ToggleDiscordMute()
	{
		MediaControls.<ToggleDiscordMute>d__9 <ToggleDiscordMute>d__ = new MediaControls.<ToggleDiscordMute>d__9();
		<ToggleDiscordMute>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<ToggleDiscordMute>d__.<>1__state = -1;
		<ToggleDiscordMute>d__.<>t__builder.Start<MediaControls.<ToggleDiscordMute>d__9>(ref <ToggleDiscordMute>d__);
		return <ToggleDiscordMute>d__.<>t__builder.Task;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000032C0 File Offset: 0x000014C0
	[DebuggerStepThrough]
	public static Task ToggleDiscordDeafen()
	{
		MediaControls.<ToggleDiscordDeafen>d__10 <ToggleDiscordDeafen>d__ = new MediaControls.<ToggleDiscordDeafen>d__10();
		<ToggleDiscordDeafen>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<ToggleDiscordDeafen>d__.<>1__state = -1;
		<ToggleDiscordDeafen>d__.<>t__builder.Start<MediaControls.<ToggleDiscordDeafen>d__10>(ref <ToggleDiscordDeafen>d__);
		return <ToggleDiscordDeafen>d__.<>t__builder.Task;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003300 File Offset: 0x00001500
	public static void SendDiscordHotkey(string hotkey)
	{
		Process[] processesByName = Process.GetProcessesByName("Discord");
		bool flag = processesByName.Length != 0;
		if (flag)
		{
			MediaControls.SetForegroundWindow(processesByName[0].MainWindowHandle);
			Thread.Sleep(100);
			SendKeys.SendWait(hotkey);
		}
	}

	// Token: 0x0600003E RID: 62
	[DllImport("user32.dll")]
	private static extern bool SetForegroundWindow(IntPtr hWnd);

	// Token: 0x0600003F RID: 63 RVA: 0x00003344 File Offset: 0x00001544
	[DebuggerStepThrough]
	public static Task ToggleSpotifyPlayback()
	{
		MediaControls.<ToggleSpotifyPlayback>d__13 <ToggleSpotifyPlayback>d__ = new MediaControls.<ToggleSpotifyPlayback>d__13();
		<ToggleSpotifyPlayback>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<ToggleSpotifyPlayback>d__.<>1__state = -1;
		<ToggleSpotifyPlayback>d__.<>t__builder.Start<MediaControls.<ToggleSpotifyPlayback>d__13>(ref <ToggleSpotifyPlayback>d__);
		return <ToggleSpotifyPlayback>d__.<>t__builder.Task;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003384 File Offset: 0x00001584
	[DebuggerStepThrough]
	public static Task SpotifyWebAPIToggle()
	{
		MediaControls.<SpotifyWebAPIToggle>d__14 <SpotifyWebAPIToggle>d__ = new MediaControls.<SpotifyWebAPIToggle>d__14();
		<SpotifyWebAPIToggle>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<SpotifyWebAPIToggle>d__.<>1__state = -1;
		<SpotifyWebAPIToggle>d__.<>t__builder.Start<MediaControls.<SpotifyWebAPIToggle>d__14>(ref <SpotifyWebAPIToggle>d__);
		return <SpotifyWebAPIToggle>d__.<>t__builder.Task;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000033C1 File Offset: 0x000015C1
	public static void SpotifyRewind()
	{
		MediaControls.keybd_event(177, 0, 0U, UIntPtr.Zero);
		MediaControls.keybd_event(177, 0, 2U, UIntPtr.Zero);
		OdiumConsole.Log("Spotify", "Previous track", LogLevel.Info);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000033F9 File Offset: 0x000015F9
	public static void SpotifySkip()
	{
		MediaControls.keybd_event(176, 0, 0U, UIntPtr.Zero);
		MediaControls.keybd_event(176, 0, 176U, UIntPtr.Zero);
		OdiumConsole.Log("Spotify", "Next track", LogLevel.Info);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003435 File Offset: 0x00001635
	public static void SpotifyPause()
	{
		MediaControls.keybd_event(179, 0, 0U, UIntPtr.Zero);
		MediaControls.keybd_event(179, 0, 2U, UIntPtr.Zero);
		OdiumConsole.Log("Spotify", "Paused", LogLevel.Info);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003470 File Offset: 0x00001670
	private static string GetSpotifyAccessToken()
	{
		return null;
	}

	// Token: 0x0400001E RID: 30
	private static readonly HttpClient httpClient = new HttpClient();

	// Token: 0x0400001F RID: 31
	private static bool isSpotifyPlaying = false;

	// Token: 0x04000020 RID: 32
	private static bool isDiscordMuted = false;

	// Token: 0x04000021 RID: 33
	private static bool isDiscordDeafened = false;

	// Token: 0x04000022 RID: 34
	private const int KEYEVENTF_KEYUP = 2;

	// Token: 0x04000023 RID: 35
	private const byte VK_MEDIA_PLAY_PAUSE = 179;

	// Token: 0x04000024 RID: 36
	private const byte VK_MEDIA_PREV_TRACK = 177;

	// Token: 0x04000025 RID: 37
	private const byte VK_MEDIA_NEXT_TRACK = 176;
}

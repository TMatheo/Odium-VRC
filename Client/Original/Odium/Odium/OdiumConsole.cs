using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Odium.Components;

namespace Odium
{
	// Token: 0x02000019 RID: 25
	public static class OdiumConsole
	{
		// Token: 0x060000A2 RID: 162
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool AllocConsole();

		// Token: 0x060000A3 RID: 163
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool FreeConsole();

		// Token: 0x060000A4 RID: 164
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetConsoleWindow();

		// Token: 0x060000A5 RID: 165
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int nStdHandle);

		// Token: 0x060000A6 RID: 166
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int lpMode);

		// Token: 0x060000A7 RID: 167
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool SetConsoleMode(IntPtr hConsoleHandle, int dwMode);

		// Token: 0x060000A8 RID: 168 RVA: 0x00005600 File Offset: 0x00003800
		public static void Initialize()
		{
			bool flag = !OdiumConsole.ShouldAllocateConsole();
			if (!flag)
			{
				bool flag2 = OdiumConsole.GetConsoleWindow() != IntPtr.Zero;
				if (!flag2)
				{
					try
					{
						OdiumConsole.AllocConsole();
						Thread.Sleep(200);
						OdiumConsole.InitializeStandardStreams();
						Console.Title = "Odium Console";
						OdiumConsole.EnableVirtualTerminalProcessing();
						OdiumConsole.EnableInputMode();
						Console.CursorVisible = true;
						OdiumConsole.DisplayBanner();
						OdiumConsole.Log("System", "Console initialized successfully", LogLevel.Info);
						OdiumConsole.Log("System", "Console ready for input commands", LogLevel.Info);
						OdiumConsole._isInitialized = true;
					}
					catch (Exception ex)
					{
						OdiumConsole.Log("System", "Failed to initialize console: " + ex.Message, LogLevel.Error);
					}
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000056D0 File Offset: 0x000038D0
		private static void InitializeStandardStreams()
		{
			try
			{
				Stream stream = Console.OpenStandardOutput();
				StreamWriter @out = new StreamWriter(stream)
				{
					AutoFlush = true
				};
				Console.SetOut(@out);
				Stream stream2 = Console.OpenStandardInput();
				StreamReader @in = new StreamReader(stream2);
				Console.SetIn(@in);
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("System", "Failed to initialize streams: " + ex.Message, LogLevel.Error);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005748 File Offset: 0x00003948
		private static void EnableInputMode()
		{
			try
			{
				IntPtr stdHandle = OdiumConsole.GetStdHandle(-10);
				int num;
				bool flag = stdHandle != IntPtr.Zero && OdiumConsole.GetConsoleMode(stdHandle, out num);
				if (flag)
				{
					num |= 7;
					OdiumConsole.SetConsoleMode(stdHandle, num);
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.Log("System", "Failed to set input mode: " + ex.Message, LogLevel.Warning);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000057C0 File Offset: 0x000039C0
		private static bool ShouldAllocateConsole()
		{
			bool result;
			try
			{
				string odiumFolderPath = ModSetup.GetOdiumFolderPath();
				string text = Path.Combine(odiumFolderPath, "odium_prefs.json");
				bool flag = !File.Exists(text);
				if (flag)
				{
					OdiumConsole.CreateDefaultPreferencesFile(text);
					result = true;
				}
				else
				{
					string jsonString = File.ReadAllText(text);
					OdiumPreferences odiumPreferences = OdiumJsonHandler.ParsePreferences(jsonString);
					result = (odiumPreferences == null || odiumPreferences.AllocConsole);
				}
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005834 File Offset: 0x00003A34
		private static void CreateDefaultPreferencesFile(string filePath)
		{
			try
			{
				OdiumPreferences preferences = new OdiumPreferences
				{
					AllocConsole = true
				};
				string contents = OdiumJsonHandler.SerializePreferences(preferences);
				File.WriteAllText(filePath, contents);
			}
			catch
			{
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005878 File Offset: 0x00003A78
		public static void Log(string category, string message, LogLevel level = LogLevel.Info)
		{
			bool flag = !OdiumConsole._isInitialized;
			if (!flag)
			{
				try
				{
					string str = DateTime.Now.ToString("HH:mm:ss");
					Console.ResetColor();
					Console.Write("[" + str + "] ");
					Console.ForegroundColor = OdiumConsole.GetCategoryColor(category);
					Console.Write("[" + category + "] ");
					Console.ResetColor();
					Console.ForegroundColor = OdiumConsole.GetLevelColor(level);
					Console.WriteLine(message);
					Console.ResetColor();
				}
				catch
				{
					Console.WriteLine(string.Format("[{0:HH:mm:ss}] [{1}] {2}", DateTime.Now, category, message));
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005940 File Offset: 0x00003B40
		public static string Readline(string category, string message, LogLevel level = LogLevel.Info)
		{
			string text = string.Empty;
			bool flag = !OdiumConsole._isInitialized;
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				try
				{
					text = Console.ReadLine();
				}
				catch
				{
					Console.WriteLine(string.Format("[{0:HH:mm:ss}] [{1}] {2}", DateTime.Now, category, message));
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000059A8 File Offset: 0x00003BA8
		public static void LogGradient(string category, string message, LogLevel level = LogLevel.Info, bool gradientCategory = false)
		{
			bool flag = !OdiumConsole._isInitialized;
			if (!flag)
			{
				try
				{
					string str = DateTime.Now.ToString("HH:mm:ss");
					Console.ResetColor();
					Console.Write("[" + str + "] ");
					if (gradientCategory)
					{
						Console.Write("[");
						OdiumConsole.LogMessageWithGradient(category, false);
						Console.Write("] ");
						Console.ResetColor();
						Console.WriteLine(message);
					}
					else
					{
						Console.ForegroundColor = OdiumConsole.GetCategoryColor(category);
						Console.Write("[" + category + "] ");
						Console.ResetColor();
						OdiumConsole.LogMessageWithGradient(message, true);
					}
				}
				catch
				{
					Console.WriteLine(string.Format("[{0:HH:mm:ss}] [{1}] {2}", DateTime.Now, category, message));
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005A94 File Offset: 0x00003C94
		private static void LogMessageWithGradient(string text, bool addNewline = true)
		{
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				int num = 255;
				int num2 = 204 - i * 204 / length;
				int num3 = 203 + i * 52 / length;
				Console.Write(string.Format("\u001b[38;2;{0};{1};{2}m{3}", new object[]
				{
					num,
					num2,
					num3,
					text[i]
				}));
			}
			Console.Write("\u001b[0m");
			if (addNewline)
			{
				Console.WriteLine();
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005B3C File Offset: 0x00003D3C
		private static void LogWithGradient(string text, [TupleElementNames(new string[]
		{
			"red",
			"green",
			"blue"
		})] ValueTuple<int, int, int> startColor, [TupleElementNames(new string[]
		{
			"red",
			"green",
			"blue"
		})] ValueTuple<int, int, int> endColor)
		{
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				int num = startColor.Item1 + i * (endColor.Item1 - startColor.Item1) / length;
				int num2 = startColor.Item2 + i * (endColor.Item2 - startColor.Item2) / length;
				int num3 = startColor.Item3 + i * (endColor.Item3 - startColor.Item3) / length;
				Console.Write(string.Format("\u001b[38;2;{0};{1};{2}m{3}", new object[]
				{
					num,
					num2,
					num3,
					text[i]
				}));
			}
			Console.WriteLine("\u001b[0m");
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005C04 File Offset: 0x00003E04
		public static void LogException(Exception ex, string context = null)
		{
			bool flag = !OdiumConsole._isInitialized;
			if (!flag)
			{
				string str = DateTime.Now.ToString("HH:mm:ss");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n[" + str + "] ============ EXCEPTION ============");
				Console.WriteLine("Context: " + (context ?? "None"));
				Console.WriteLine("Type: " + ex.GetType().Name);
				Console.WriteLine("Message: " + ex.Message);
				Console.WriteLine("Stack Trace:\n" + ex.StackTrace);
				Console.WriteLine("===================================\n");
				Console.ResetColor();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005CC8 File Offset: 0x00003EC8
		private static void EnableVirtualTerminalProcessing()
		{
			try
			{
				IntPtr stdHandle = OdiumConsole.GetStdHandle(-11);
				int num;
				bool flag = !OdiumConsole.GetConsoleMode(stdHandle, out num);
				if (!flag)
				{
					num |= 4;
					OdiumConsole.SetConsoleMode(stdHandle, num);
				}
			}
			catch
			{
				OdiumConsole.Log("System", "Failed to enable VT processing", LogLevel.Warning);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005D28 File Offset: 0x00003F28
		private static ConsoleColor GetCategoryColor(string category)
		{
			bool flag = category.StartsWith("System", StringComparison.OrdinalIgnoreCase);
			ConsoleColor result;
			if (flag)
			{
				result = ConsoleColor.Cyan;
			}
			else
			{
				bool flag2 = category.StartsWith("Network", StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					result = ConsoleColor.Magenta;
				}
				else
				{
					bool flag3 = category.StartsWith("UI", StringComparison.OrdinalIgnoreCase);
					if (flag3)
					{
						result = ConsoleColor.Green;
					}
					else
					{
						result = ConsoleColor.White;
					}
				}
			}
			return result;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005D7C File Offset: 0x00003F7C
		private static ConsoleColor GetLevelColor(LogLevel level)
		{
			ConsoleColor result;
			switch (level)
			{
			case LogLevel.Debug:
				result = ConsoleColor.Blue;
				break;
			case LogLevel.Warning:
				result = ConsoleColor.Yellow;
				break;
			case LogLevel.Error:
				result = ConsoleColor.Red;
				break;
			default:
				result = ConsoleColor.Gray;
				break;
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005DB8 File Offset: 0x00003FB8
		private static void DisplayBanner()
		{
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			OdiumConsole.LogWithGradient("\r\n                    /================================================================================\\\r\n                    ||                                                                              ||\r\n                    ||                                                                              ||\r\n                    ||                                                                              ||\r\n                    ||                                                                              ||\r\n                    ||                                                                              ||\r\n                    ||                 ______    _______   __   __    __  .___  ___.                ||\r\n                    ||                /  __  \\  |       \\ |  | |  |  |  | |   \\/   |                ||\r\n                    ||               |  |  |  | |  .--.  ||  | |  |  |  | |  \\  /  |                ||\r\n                    ||               |  |  |  | |  |  |  ||  | |  |  |  | |  |\\/|  |                ||\r\n                    ||               |  `--'  | |  '--'  ||  | |  `--'  | |  |  |  |                ||\r\n                    ||                \\______/  |_______/ |__|  \\______/  |__|  |__|                ||\r\n                    ||                                                                              ||\r\n                    ||                                                                              ||\r\n                    ||                                                                              ||\r\n                    ||                                                                              ||\r\n                    ||                                                                              ||\r\n                    \\================================================================================/\r\n                         ", new ValueTuple<int, int, int>(255, 192, 203), new ValueTuple<int, int, int>(255, 20, 147));
			Console.ResetColor();
			OdiumConsole.LogWithGradient(string.Format("                    Odium Console - {0:yyyy-MM-dd HH:mm:ss}\n", DateTime.Now), new ValueTuple<int, int, int>(255, 192, 203), new ValueTuple<int, int, int>(255, 20, 147));
		}

		// Token: 0x04000042 RID: 66
		private const int STD_OUTPUT_HANDLE = -11;

		// Token: 0x04000043 RID: 67
		private const int STD_INPUT_HANDLE = -10;

		// Token: 0x04000044 RID: 68
		private const int ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;

		// Token: 0x04000045 RID: 69
		private const int ENABLE_ECHO_INPUT = 4;

		// Token: 0x04000046 RID: 70
		private const int ENABLE_LINE_INPUT = 2;

		// Token: 0x04000047 RID: 71
		private const int ENABLE_PROCESSED_INPUT = 1;

		// Token: 0x04000048 RID: 72
		private static bool _isInitialized;
	}
}

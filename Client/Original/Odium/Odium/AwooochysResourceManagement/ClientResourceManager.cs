using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

namespace Odium.AwooochysResourceManagement
{
	// Token: 0x02000088 RID: 136
	public static class ClientResourceManager
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x000200A0 File Offset: 0x0001E2A0
		public static void EnsureAllResourcesExist()
		{
			ClientResourceManager.EnsureDirectoryStructure();
			foreach (ValueTuple<string, string> valueTuple in ClientResourceManager.RequiredResources)
			{
				string path = Path.Combine(valueTuple.Item2, valueTuple.Item1);
				bool flag = !File.Exists(path);
				if (flag)
				{
					ClientResourceManager.DownloadFile(valueTuple.Item1, valueTuple.Item2);
				}
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00020108 File Offset: 0x0001E308
		public static bool TryGetResourcePath(string fileName, string subDirectory, out string fullPath)
		{
			string path = string.IsNullOrEmpty(subDirectory) ? "Odium" : Path.Combine("Odium", subDirectory);
			fullPath = Path.Combine(path, fileName);
			return File.Exists(fullPath);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00020145 File Offset: 0x0001E345
		private static void EnsureDirectoryStructure()
		{
			ClientResourceManager.CreateDirectoryIfNotExists("Odium");
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00020154 File Offset: 0x0001E354
		private static void CreateDirectoryIfNotExists(string path)
		{
			bool flag = !Directory.Exists(path);
			if (flag)
			{
				Directory.CreateDirectory(path);
				Console.WriteLine("ClientResourceManager: Created directory: " + path);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0002018C File Offset: 0x0001E38C
		private static void DownloadFile(string fileName, string targetDirectory)
		{
			string text = Path.Combine(targetDirectory, fileName);
			string url = "https://nigga.rest/DeepOdium/" + fileName;
			Console.WriteLine("ClientResourceManager: File not found: " + text + ", Downloading...");
			try
			{
				byte[] bytes = ClientResourceManager.DownloadFileData(url);
				File.WriteAllBytes(text, bytes);
				Console.WriteLine("ClientResourceManager: Successfully downloaded: " + text);
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Concat(new string[]
				{
					"ClientResourceManager: Failed to download ",
					fileName,
					" to ",
					targetDirectory,
					": ",
					ex.Message
				}));
				bool flag = fileName.EndsWith(".dll");
				if (flag)
				{
					Console.WriteLine("ClientResourceManager: CRITICAL: Failed to download a dependency DLL. Some features may not work.");
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00020258 File Offset: 0x0001E458
		private static byte[] DownloadFileData(string url)
		{
			byte[] result;
			using (WebClient webClient = new WebClient())
			{
				result = webClient.DownloadData(url);
			}
			return result;
		}

		// Token: 0x040001E7 RID: 487
		private const string ClientDirectory = "Odium";

		// Token: 0x040001E8 RID: 488
		private const string ResourceBaseUrl = "https://nigga.rest/DeepOdium/";

		// Token: 0x040001E9 RID: 489
		[TupleElementNames(new string[]
		{
			"FileName",
			"TargetDirectory"
		})]
		private static readonly ValueTuple<string, string>[] RequiredResources = new ValueTuple<string, string>[]
		{
			new ValueTuple<string, string>("LoadingBackgrund.png", "Odium")
		};
	}
}

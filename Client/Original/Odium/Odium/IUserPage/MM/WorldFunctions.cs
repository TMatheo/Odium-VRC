using System;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Odium.ButtonAPI.MM;
using Odium.ButtonAPI.QM;
using Odium.Components;
using Odium.Modules;
using Odium.Threadding;
using UnityEngine;

namespace Odium.IUserPage.MM
{
	// Token: 0x0200003B RID: 59
	internal class WorldFunctions
	{
		// Token: 0x06000158 RID: 344 RVA: 0x0000D91C File Offset: 0x0000BB1C
		public static void Initialize()
		{
			Sprite icon = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\InfoIcon.png", 100f);
			Sprite icon2 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\DownloadIcon.png", 100f);
			MMWorldActionRow actionRow = new MMWorldActionRow("Odium Actions");
			new MMWorldButton(actionRow, "Copy World ID", delegate()
			{
				OdiumConsole.Log("Odium", "Attempting to copy world ID...", LogLevel.Info);
				try
				{
					string worldName = ApiUtils.GetMMWorldName();
					bool flag = string.IsNullOrEmpty(worldName);
					if (flag)
					{
						OdiumConsole.Log("Odium", "No world name found", LogLevel.Info);
					}
					else
					{
						OdiumConsole.Log("Odium", "Fetching world ID for: " + worldName, LogLevel.Info);
						MainThreadDispatcher.Enqueue(delegate
						{
							try
							{
								HttpClient httpClient = new HttpClient();
								string requestUri = "http://api.snoofz.net:3778/api/odium/vrc/getWorldByName?worldName=" + Uri.EscapeDataString(worldName);
								HttpResponseMessage result = httpClient.GetAsync(requestUri).Result;
								bool isSuccessStatusCode = result.IsSuccessStatusCode;
								if (isSuccessStatusCode)
								{
									string result2 = result.Content.ReadAsStringAsync().Result;
									JObject jobject = JObject.Parse(result2);
									JToken jtoken = jobject["id"];
									string text = (jtoken != null) ? jtoken.ToString() : null;
									bool flag2 = !string.IsNullOrEmpty(text);
									if (flag2)
									{
										Clipboard.SetText(text);
										OdiumConsole.Log("Odium", "Copied world ID to clipboard: " + text, LogLevel.Info);
										OdiumBottomNotification.ShowNotification("Copied World ID");
									}
									else
									{
										OdiumConsole.Log("Odium", "No world ID found in API response", LogLevel.Info);
									}
								}
								else
								{
									OdiumConsole.Log("Odium", string.Format("API request failed with status: {0}", result.StatusCode), LogLevel.Info);
								}
							}
							catch (Exception ex2)
							{
								OdiumConsole.Log("Odium", "Error in main thread execution: " + ex2.Message, LogLevel.Info);
							}
						});
					}
				}
				catch (Exception ex)
				{
					OdiumConsole.Log("Odium", "Error fetching world ID: " + ex.Message, LogLevel.Info);
				}
			}, icon);
			new MMWorldButton(actionRow, "Download VRCW", delegate()
			{
			}, icon2);
		}
	}
}

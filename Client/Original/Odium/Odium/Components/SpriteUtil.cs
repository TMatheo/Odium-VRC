using System;
using System.IO;
using MelonLoader;
using Odium.Odium;
using UnityEngine;
using UnityEngine.UI;

namespace Odium.Components
{
	// Token: 0x0200006D RID: 109
	public class SpriteUtil
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00018F80 File Offset: 0x00017180
		public static Texture2D CreateTextureFromBase64(string base64)
		{
			byte[] arr = Convert.FromBase64String(base64);
			Texture2D texture2D = new Texture2D(2, 2);
			ImageConversion.LoadImage(texture2D, arr);
			return texture2D;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00018FB0 File Offset: 0x000171B0
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 size)
		{
			return Sprite.Create(texture, rect, size);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00018FCC File Offset: 0x000171CC
		public static void ApplySpriteToButton(string objectName, string image)
		{
			try
			{
				bool flag = FileHelper.IsPath(image);
				Sprite overrideSprite;
				if (flag)
				{
					overrideSprite = SpriteUtil.LoadFromDisk(image, 100f);
				}
				else
				{
					image = Path.Combine(Environment.CurrentDirectory, "Odium", image);
					overrideSprite = SpriteUtil.LoadFromDisk(image, 100f);
				}
				AssignedVariables.userInterface.transform.Find(objectName).transform.Find("Background").GetComponent<Image>().overrideSprite = overrideSprite;
				OdiumConsole.LogGradient("Odium", "Sprite applied to " + objectName + " successfully!", LogLevel.Info, false);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, null);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00019080 File Offset: 0x00017280
		public static void ApplySpriteToMenu(string objectName, string image)
		{
			try
			{
				bool flag = FileHelper.IsPath(image);
				Sprite overrideSprite;
				if (flag)
				{
					overrideSprite = SpriteUtil.LoadFromDisk(image, 100f);
				}
				else
				{
					image = Path.Combine(Environment.CurrentDirectory, "Odium", image);
					overrideSprite = SpriteUtil.LoadFromDisk(image, 100f);
				}
				AssignedVariables.userInterface.transform.Find(objectName).GetComponent<Image>().overrideSprite = overrideSprite;
				OdiumConsole.LogGradient("Odium", "Sprite applied to " + objectName + " successfully!", LogLevel.Info, false);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, null);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00019124 File Offset: 0x00017324
		public static void ApplyColorToMenu(string objectName, Color color)
		{
			try
			{
				AssignedVariables.userInterface.transform.Find(objectName).GetComponent<Image>().m_Color = color;
				OdiumConsole.LogGradient("Odium", "Color applied to " + objectName + " successfully!", LogLevel.Info, false);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, null);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001918C File Offset: 0x0001738C
		public static void ApplyColorToButton(string objectName, Color color)
		{
			try
			{
				AssignedVariables.userInterface.transform.Find(objectName).GetComponent<Image>().m_Color = color;
				OdiumConsole.LogGradient("Odium", "Color applied to " + objectName + " successfully!", LogLevel.Info, false);
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, null);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000191F4 File Offset: 0x000173F4
		public static Sprite LoadFromDisk(string path, float pixelsPerUnit = 100f)
		{
			bool flag = string.IsNullOrEmpty(path);
			Sprite result;
			if (flag)
			{
				MelonLogger.Warning("Cannot load sprite: Path is null or empty");
				result = null;
			}
			else
			{
				try
				{
					byte[] array = File.ReadAllBytes(path);
					bool flag2 = array == null || array.Length == 0;
					if (flag2)
					{
						MelonLogger.Warning("Cannot load sprite: No data found at path '" + path + "'");
						result = null;
					}
					else
					{
						Texture2D texture2D = new Texture2D(2, 2);
						bool flag3 = !ImageConversion.LoadImage(texture2D, array);
						if (flag3)
						{
							MelonLogger.Error("Failed to convert image data to texture from path '" + path + "'");
							result = null;
						}
						else
						{
							Rect rect = new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height);
							Vector2 pivot = new Vector2(0.5f, 0.5f);
							Sprite sprite = Sprite.Create(texture2D, rect, pivot, pixelsPerUnit, 0U, SpriteMeshType.FullRect, Vector4.zero, false);
							sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
							texture2D.hideFlags |= HideFlags.DontUnloadUnusedAsset;
							result = sprite;
						}
					}
				}
				catch (Exception ex)
				{
					MelonLogger.Error("Error loading sprite from '" + path + "': " + ex.Message, ex);
					result = null;
				}
			}
			return result;
		}
	}
}

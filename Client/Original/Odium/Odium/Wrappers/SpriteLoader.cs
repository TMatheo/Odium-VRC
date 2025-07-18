using System;
using System.IO;
using MelonLoader;
using UnityEngine;

namespace Odium.Wrappers
{
	// Token: 0x02000043 RID: 67
	internal static class SpriteLoader
	{
		// Token: 0x060001BC RID: 444 RVA: 0x0000ED7C File Offset: 0x0000CF7C
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

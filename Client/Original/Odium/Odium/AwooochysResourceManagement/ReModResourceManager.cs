using System;
using System.IO;
using System.Reflection;
using UnhollowerBaseLib;
using UnityEngine;

namespace Odium.AwooochysResourceManagement
{
	// Token: 0x02000089 RID: 137
	public static class ReModResourceManager
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x000202B8 File Offset: 0x0001E4B8
		public static Sprite LoadSpriteFromDisk(this string path, int width = 512, int height = 512)
		{
			bool flag = string.IsNullOrEmpty(path);
			bool flag2 = flag;
			Sprite result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				byte[] array = File.ReadAllBytes(path);
				bool flag3 = array == null || array.Length == 0;
				bool flag4 = flag3;
				if (flag4)
				{
					result = null;
				}
				else
				{
					Texture2D texture2D = new Texture2D(width, height);
					bool flag5 = !ImageConversion.LoadImage(texture2D, array);
					bool flag6 = flag5;
					if (flag6)
					{
						result = null;
					}
					else
					{
						Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f), 100000f, 1000U, SpriteMeshType.FullRect, Vector4.zero, false);
						sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
						result = sprite;
					}
				}
			}
			return result;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00020394 File Offset: 0x0001E594
		public static Sprite LoadSpriteFromByteArray(this byte[] bytes, int width = 512, int height = 512)
		{
			bool flag = bytes == null || bytes.Length == 0;
			bool flag2 = flag;
			Sprite result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				Texture2D texture2D = new Texture2D(width, height);
				bool flag3 = !ImageConversion.LoadImage(texture2D, bytes);
				bool flag4 = flag3;
				if (flag4)
				{
					result = null;
				}
				else
				{
					Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f), 100000f, 1000U, SpriteMeshType.FullRect, Vector4.zero, false);
					sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
					result = sprite;
				}
			}
			return result;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00020448 File Offset: 0x0001E648
		public static Sprite LoadSpriteFromByteArray(this Il2CppStructArray<byte> bytes, int width = 512, int height = 512)
		{
			bool flag = bytes == null || bytes.Length == 0;
			bool flag2 = flag;
			Sprite result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				Texture2D texture2D = new Texture2D(width, height);
				bool flag3 = !ImageConversion.LoadImage(texture2D, bytes);
				bool flag4 = flag3;
				if (flag4)
				{
					result = null;
				}
				else
				{
					Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f), 100000f, 1000U, SpriteMeshType.FullRect, Vector4.zero, false);
					sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
					result = sprite;
				}
			}
			return result;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000204F8 File Offset: 0x0001E6F8
		public static Sprite LoadSpriteFromBundledResource(this string path, int width = 512, int height = 512)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
			MemoryStream memoryStream = new MemoryStream((int)manifestResourceStream.Length);
			manifestResourceStream.CopyTo(memoryStream);
			Texture2D texture2D = new Texture2D(width, height);
			bool flag = !ImageConversion.LoadImage(texture2D, memoryStream.ToArray());
			bool flag2 = flag;
			Sprite result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0f, 0f), 100000f, 1000U, SpriteMeshType.FullRect, Vector4.zero, false);
				sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
				result = sprite;
			}
			return result;
		}
	}
}

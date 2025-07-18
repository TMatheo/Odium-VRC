using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x0200000F RID: 15
public static class ImageLoader
{
	// Token: 0x06000056 RID: 86 RVA: 0x00003E05 File Offset: 0x00002005
	public static IEnumerator LoadSpriteFromURL(string url, Action<Sprite> onComplete, Action<string> onError = null)
	{
		ImageLoader.<LoadSpriteFromURL>d__1 <LoadSpriteFromURL>d__ = new ImageLoader.<LoadSpriteFromURL>d__1(0);
		<LoadSpriteFromURL>d__.url = url;
		<LoadSpriteFromURL>d__.onComplete = onComplete;
		<LoadSpriteFromURL>d__.onError = onError;
		return <LoadSpriteFromURL>d__;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003E22 File Offset: 0x00002022
	public static IEnumerator LoadSpriteFromURLWebRequest(string url, Action<Sprite> onComplete, Action<string> onError = null)
	{
		ImageLoader.<LoadSpriteFromURLWebRequest>d__2 <LoadSpriteFromURLWebRequest>d__ = new ImageLoader.<LoadSpriteFromURLWebRequest>d__2(0);
		<LoadSpriteFromURLWebRequest>d__.url = url;
		<LoadSpriteFromURLWebRequest>d__.onComplete = onComplete;
		<LoadSpriteFromURLWebRequest>d__.onError = onError;
		return <LoadSpriteFromURLWebRequest>d__;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003E40 File Offset: 0x00002040
	[DebuggerStepThrough]
	public static Task<Sprite> LoadSpriteFromURLAsync(string url)
	{
		ImageLoader.<LoadSpriteFromURLAsync>d__3 <LoadSpriteFromURLAsync>d__ = new ImageLoader.<LoadSpriteFromURLAsync>d__3();
		<LoadSpriteFromURLAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Sprite>.Create();
		<LoadSpriteFromURLAsync>d__.url = url;
		<LoadSpriteFromURLAsync>d__.<>1__state = -1;
		<LoadSpriteFromURLAsync>d__.<>t__builder.Start<ImageLoader.<LoadSpriteFromURLAsync>d__3>(ref <LoadSpriteFromURLAsync>d__);
		return <LoadSpriteFromURLAsync>d__.<>t__builder.Task;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003E84 File Offset: 0x00002084
	public static Sprite LoadSpriteFromURLSync(string url)
	{
		bool flag = ImageLoader.spriteCache.ContainsKey(url);
		Sprite result;
		if (flag)
		{
			result = ImageLoader.spriteCache[url];
		}
		else
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] arr = webClient.DownloadData(url);
					Texture2D texture2D = new Texture2D(2, 2);
					bool flag2 = ImageConversion.LoadImage(texture2D, arr);
					if (!flag2)
					{
						throw new Exception("Failed to load image data into texture");
					}
					Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f);
					ImageLoader.spriteCache[url] = sprite;
					result = sprite;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to download image: " + ex.Message);
			}
		}
		return result;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003F80 File Offset: 0x00002180
	public static IEnumerator LoadSpriteFromURLManual(string url, Action<Sprite> onComplete, Action<string> onError = null)
	{
		ImageLoader.<LoadSpriteFromURLManual>d__5 <LoadSpriteFromURLManual>d__ = new ImageLoader.<LoadSpriteFromURLManual>d__5(0);
		<LoadSpriteFromURLManual>d__.url = url;
		<LoadSpriteFromURLManual>d__.onComplete = onComplete;
		<LoadSpriteFromURLManual>d__.onError = onError;
		return <LoadSpriteFromURLManual>d__;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00003FA0 File Offset: 0x000021A0
	public static void ClearCache()
	{
		foreach (Sprite sprite in ImageLoader.spriteCache.Values)
		{
			bool flag = sprite != null && sprite.texture != null;
			if (flag)
			{
				Object.Destroy(sprite.texture);
				Object.Destroy(sprite);
			}
		}
		ImageLoader.spriteCache.Clear();
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00004034 File Offset: 0x00002234
	public static void RemoveFromCache(string url)
	{
		bool flag = ImageLoader.spriteCache.ContainsKey(url);
		if (flag)
		{
			Sprite sprite = ImageLoader.spriteCache[url];
			bool flag2 = sprite != null && sprite.texture != null;
			if (flag2)
			{
				Object.Destroy(sprite.texture);
				Object.Destroy(sprite);
			}
			ImageLoader.spriteCache.Remove(url);
		}
	}

	// Token: 0x04000030 RID: 48
	private static Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
}

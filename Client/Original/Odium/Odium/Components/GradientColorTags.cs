using System;
using System.Text;
using UnityEngine;

namespace Odium.Components
{
	// Token: 0x0200005B RID: 91
	public class GradientColorTags
	{
		// Token: 0x06000262 RID: 610 RVA: 0x000154F8 File Offset: 0x000136F8
		public static string GetAnimatedGradientText(string text, Color color1, Color color2, float speed = 1f, float waveLength = 2f)
		{
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				float num = Time.time * speed;
				for (int i = 0; i < text.Length; i++)
				{
					float num2 = (float)i / (float)(text.Length - 1);
					float num3 = num2 + (Mathf.Sin(num + (float)i * waveLength) * 0.5f + 0.5f) * 0.3f;
					num3 = Mathf.Clamp01(num3);
					Color color3 = Color.Lerp(color1, color2, num3);
					string arg = ColorUtility.ToHtmlStringRGB(color3);
					stringBuilder.Append(string.Format("<color=#{0}>{1}</color>", arg, text[i]));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000155C0 File Offset: 0x000137C0
		public static string GetWaveGradientText(string text, Color color1, Color color2, float speed = 2f, float frequency = 0.5f)
		{
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				float num = Time.time * speed;
				for (int i = 0; i < text.Length; i++)
				{
					float t = Mathf.Sin(num + (float)i * frequency) * 0.5f + 0.5f;
					Color color3 = Color.Lerp(color1, color2, t);
					string arg = ColorUtility.ToHtmlStringRGB(color3);
					stringBuilder.Append(string.Format("<color=#{0}>{1}</color>", arg, text[i]));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00015664 File Offset: 0x00013864
		public static string GetPulseGradientText(string text, Color color1, Color color2, float speed = 1.5f)
		{
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				float num = Mathf.Sin(Time.time * speed) * 0.5f + 0.5f;
				for (int i = 0; i < text.Length; i++)
				{
					float t = (float)i / (float)(text.Length - 1);
					float t2 = Mathf.Lerp(num * 0.3f, 1f - num * 0.3f, t);
					Color color3 = Color.Lerp(color1, color2, t2);
					string arg = ColorUtility.ToHtmlStringRGB(color3);
					stringBuilder.Append(string.Format("<color=#{0}>{1}</color>", arg, text[i]));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001572C File Offset: 0x0001392C
		public static string GetRainbowText(string text, float speed = 1f, float frequency = 0.3f)
		{
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				float num = Time.time * speed;
				for (int i = 0; i < text.Length; i++)
				{
					float h = (num + (float)i * frequency) % 1f;
					Color color = Color.HSVToRGB(h, 1f, 1f);
					string arg = ColorUtility.ToHtmlStringRGB(color);
					stringBuilder.Append(string.Format("<color=#{0}>{1}</color>", arg, text[i]));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000157C8 File Offset: 0x000139C8
		public static string GetFireText(string text, float speed = 2f)
		{
			Color a = new Color(1f, 0.2f, 0f);
			Color b = new Color(1f, 1f, 0f);
			Color b2 = new Color(1f, 0.5f, 0f);
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				float num = Time.time * speed;
				for (int i = 0; i < text.Length; i++)
				{
					float t = Mathf.Sin(num + (float)i * 0.5f) * 0.5f + 0.5f;
					float t2 = Mathf.Cos(num * 1.3f + (float)i * 0.3f) * 0.5f + 0.5f;
					Color a2 = Color.Lerp(a, b, t);
					Color color = Color.Lerp(a2, b2, t2);
					string arg = ColorUtility.ToHtmlStringRGB(color);
					stringBuilder.Append(string.Format("<color=#{0}>{1}</color>", arg, text[i]));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000158F0 File Offset: 0x00013AF0
		public static string GetGlitchText(string text, float speed = 5f)
		{
			Color red = Color.red;
			Color cyan = Color.cyan;
			Color white = Color.white;
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				float num = Time.time * speed;
				for (int i = 0; i < text.Length; i++)
				{
					float num2 = Mathf.Sin(num * 7f + (float)i * 2f) * 0.5f + 0.5f;
					bool flag2 = num2 < 0.33f;
					Color color;
					if (flag2)
					{
						color = red;
					}
					else
					{
						bool flag3 = num2 < 0.66f;
						if (flag3)
						{
							color = cyan;
						}
						else
						{
							color = white;
						}
					}
					string arg = ColorUtility.ToHtmlStringRGB(color);
					stringBuilder.Append(string.Format("<color=#{0}>{1}</color>", arg, text[i]));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}
	}
}

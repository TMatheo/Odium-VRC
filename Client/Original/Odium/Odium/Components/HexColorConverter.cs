using System;
using UnityEngine;

namespace Odium.Components
{
	// Token: 0x0200005C RID: 92
	public static class HexColorConverter
	{
		// Token: 0x06000269 RID: 617 RVA: 0x000159E4 File Offset: 0x00013BE4
		public static Color HexToColor(string hex)
		{
			bool flag = string.IsNullOrEmpty(hex);
			if (flag)
			{
				throw new ArgumentException("Hex string cannot be null or empty");
			}
			hex = hex.TrimStart(new char[]
			{
				'#'
			});
			bool flag2 = !HexColorConverter.IsValidHex(hex);
			if (flag2)
			{
				throw new ArgumentException("Invalid hex color: #" + hex);
			}
			switch (hex.Length)
			{
			case 3:
				hex = string.Format("{0}{1}{2}{3}{4}{5}", new object[]
				{
					hex[0],
					hex[0],
					hex[1],
					hex[1],
					hex[2],
					hex[2]
				});
				goto IL_19C;
			case 4:
				hex = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", new object[]
				{
					hex[0],
					hex[0],
					hex[1],
					hex[1],
					hex[2],
					hex[2],
					hex[3],
					hex[3]
				});
				goto IL_19C;
			case 6:
				hex += "FF";
				goto IL_19C;
			case 8:
				goto IL_19C;
			}
			throw new ArgumentException("Invalid hex color length: #" + hex);
			IL_19C:
			byte b = Convert.ToByte(hex.Substring(0, 2), 16);
			byte b2 = Convert.ToByte(hex.Substring(2, 2), 16);
			byte b3 = Convert.ToByte(hex.Substring(4, 2), 16);
			byte b4 = Convert.ToByte(hex.Substring(6, 2), 16);
			return new Color((float)b / 255f, (float)b2 / 255f, (float)b3 / 255f, (float)b4 / 255f);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00015BF8 File Offset: 0x00013DF8
		public static Color32 HexToColor32(string hex)
		{
			Color color = HexColorConverter.HexToColor(hex);
			return new Color32((byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), (byte)(color.a * 255f));
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00015C4C File Offset: 0x00013E4C
		public static string ColorToHex(Color color, bool includeAlpha = true)
		{
			Color32 color2 = color;
			string result;
			if (includeAlpha)
			{
				result = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
				{
					color2.r,
					color2.g,
					color2.b,
					color2.a
				});
			}
			else
			{
				result = string.Format("#{0:X2}{1:X2}{2:X2}", color2.r, color2.g, color2.b);
			}
			return result;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00015CE0 File Offset: 0x00013EE0
		public static string Color32ToHex(Color32 color, bool includeAlpha = true)
		{
			string result;
			if (includeAlpha)
			{
				result = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
				{
					color.r,
					color.g,
					color.b,
					color.a
				});
			}
			else
			{
				result = string.Format("#{0:X2}{1:X2}{2:X2}", color.r, color.g, color.b);
			}
			return result;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00015D70 File Offset: 0x00013F70
		public static bool TryHexToColor(string hex, out Color color)
		{
			color = Color.white;
			bool result;
			try
			{
				color = HexColorConverter.HexToColor(hex);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00015DB4 File Offset: 0x00013FB4
		private static bool IsValidHex(string hex)
		{
			bool flag = string.IsNullOrEmpty(hex);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = hex.Length != 3 && hex.Length != 4 && hex.Length != 6 && hex.Length != 8;
				if (flag2)
				{
					result = false;
				}
				else
				{
					foreach (char c in hex)
					{
						bool flag3 = (c < '0' || c > '9') && (c < 'A' || c > 'F') && (c < 'a' || c > 'f');
						if (flag3)
						{
							return false;
						}
					}
					result = true;
				}
			}
			return result;
		}
	}
}

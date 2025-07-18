using System;

// Token: 0x02000011 RID: 17
public static class UnicodeValidator
{
	// Token: 0x06000061 RID: 97 RVA: 0x0000418C File Offset: 0x0000238C
	public static bool Sanitize(string input)
	{
		bool flag = string.IsNullOrEmpty(input);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			foreach (char c in input)
			{
				bool flag2 = c > '\u007f';
				if (flag2)
				{
					return true;
				}
				bool flag3 = char.IsControl(c) && !UnicodeValidator.IsAllowedWhitespace(c);
				if (flag3)
				{
					return true;
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000041FC File Offset: 0x000023FC
	private static bool IsAllowedWhitespace(char c)
	{
		return c == ' ' || c == '\t' || c == '\r' || c == '\n';
	}
}

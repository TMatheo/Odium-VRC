using System;
using System.IO;

namespace Odium.Components
{
	// Token: 0x02000059 RID: 89
	public class FileHelper
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00015214 File Offset: 0x00013414
		public static bool IsPath(string input)
		{
			bool flag = string.IsNullOrEmpty(input);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					bool flag2 = Path.IsPathRooted(input);
					if (flag2)
					{
						result = true;
					}
					else
					{
						bool flag3 = input.Length >= 2 && char.IsLetter(input[0]) && input[1] == ':';
						if (flag3)
						{
							result = true;
						}
						else
						{
							bool flag4 = input.StartsWith("\\\\") && input.Length > 2;
							if (flag4)
							{
								result = true;
							}
							else
							{
								bool flag5 = input.StartsWith("/");
								if (flag5)
								{
									result = true;
								}
								else
								{
									result = false;
								}
							}
						}
					}
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}
	}
}

using System;
using System.Text;

namespace Odium
{
	// Token: 0x0200001A RID: 26
	public static class OdiumJsonHandler
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00005E44 File Offset: 0x00004044
		public static OdiumPreferences ParsePreferences(string jsonString)
		{
			OdiumPreferences result;
			try
			{
				bool flag = string.IsNullOrWhiteSpace(jsonString);
				if (flag)
				{
					result = null;
				}
				else
				{
					OdiumPreferences odiumPreferences = new OdiumPreferences();
					string text = jsonString.Trim().Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
					bool flag2 = text.StartsWith("{");
					if (flag2)
					{
						text = text.Substring(1);
					}
					bool flag3 = text.EndsWith("}");
					if (flag3)
					{
						text = text.Substring(0, text.Length - 1);
					}
					string[] array = text.Split(new char[]
					{
						','
					});
					foreach (string text2 in array)
					{
						bool flag4 = string.IsNullOrWhiteSpace(text2);
						if (!flag4)
						{
							string[] array3 = text2.Split(new char[]
							{
								':'
							});
							bool flag5 = array3.Length != 2;
							if (!flag5)
							{
								string text3 = OdiumJsonHandler.RemoveQuotes(array3[0].Trim());
								string value = OdiumJsonHandler.RemoveQuotes(array3[1].Trim());
								bool flag6 = text3.Equals("allocConsole", StringComparison.OrdinalIgnoreCase);
								if (flag6)
								{
									bool allocConsole;
									bool flag7 = bool.TryParse(value, out allocConsole);
									if (flag7)
									{
										odiumPreferences.AllocConsole = allocConsole;
									}
								}
							}
						}
					}
					result = odiumPreferences;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005FD0 File Offset: 0x000041D0
		public static string SerializePreferences(OdiumPreferences preferences)
		{
			string result;
			try
			{
				bool flag = preferences == null;
				if (flag)
				{
					result = "{}";
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("{");
					stringBuilder.AppendLine("  \"allocConsole\": " + preferences.AllocConsole.ToString().ToLower());
					stringBuilder.AppendLine("}");
					result = stringBuilder.ToString();
				}
			}
			catch
			{
				result = "{}";
			}
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006058 File Offset: 0x00004258
		private static string RemoveQuotes(string input)
		{
			bool flag = string.IsNullOrEmpty(input);
			string result;
			if (flag)
			{
				result = input;
			}
			else
			{
				bool flag2 = input.StartsWith("\"") && input.EndsWith("\"") && input.Length >= 2;
				if (flag2)
				{
					result = input.Substring(1, input.Length - 2);
				}
				else
				{
					result = input;
				}
			}
			return result;
		}
	}
}

using System;
using Odium.Odium;
using UnityEngine;

namespace Odium.ButtonAPI.MM
{
	// Token: 0x02000074 RID: 116
	public class MMUserActionRow
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0001AF7B File Offset: 0x0001917B
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0001AF83 File Offset: 0x00019183
		public GameObject RowObject { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001AF8C File Offset: 0x0001918C
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0001AF94 File Offset: 0x00019194
		public string Title { get; private set; }

		// Token: 0x06000324 RID: 804 RVA: 0x0001AF9D File Offset: 0x0001919D
		public MMUserActionRow(string title)
		{
			this.Title = title;
			this.CreateRow();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001AFB8 File Offset: 0x000191B8
		private void CreateRow()
		{
			try
			{
				Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_UserDetail/ScrollRect/Viewport/VerticalLayoutGroup");
				bool flag = transform == null;
				if (flag)
				{
					Debug.LogError("Could not find user detail menu container");
				}
				else
				{
					Transform transform2 = transform.Find("Row3");
					bool flag2 = transform2 == null;
					if (flag2)
					{
						Debug.LogError("Could not find Row3 to clone");
					}
					else
					{
						OdiumConsole.Log("Odium", "Creating new user action row with title: " + this.Title, LogLevel.Info);
						this.RowObject = Object.Instantiate<GameObject>(transform2.gameObject);
						this.RowObject.transform.SetParent(transform, false);
						this.RowObject.transform.localScale = Vector3.one;
						this.RowObject.transform.localPosition = Vector3.zero;
						this.RowObject.transform.localRotation = Quaternion.identity;
						this.ClearAllButtons();
						this.SetRowTitle();
						this.RowObject.name = "Odium_CustomUserRow_" + this.Title.Replace(" ", "");
						OdiumConsole.Log("Odium", "Successfully created custom user action row: " + this.Title, LogLevel.Info);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error creating custom user action row: " + ex.Message);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001B144 File Offset: 0x00019344
		private void ClearAllButtons()
		{
			bool flag = this.RowObject == null;
			if (!flag)
			{
				Transform transform = this.RowObject.transform.Find("CellGrid_MM_Content");
				bool flag2 = transform != null;
				if (flag2)
				{
					for (int i = transform.childCount - 1; i >= 0; i--)
					{
						Object.DestroyImmediate(transform.GetChild(i).gameObject);
					}
					OdiumConsole.Log("Odium", "Cleared existing buttons from cloned user row", LogLevel.Info);
				}
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001B1C8 File Offset: 0x000193C8
		private void SetRowTitle()
		{
			bool flag = this.RowObject == null;
			if (!flag)
			{
				try
				{
					Transform transform = this.RowObject.transform.Find("Header_H2");
					bool flag2 = transform != null;
					if (flag2)
					{
						Transform transform2 = transform.Find("LeftItemContainer");
						bool flag3 = transform2 != null;
						if (flag3)
						{
							Transform transform3 = transform2.Find("Text_Title");
							bool flag4 = transform3 != null;
							if (flag4)
							{
								TextMeshProUGUIEx component = transform3.GetComponent<TextMeshProUGUIEx>();
								bool flag5 = component != null;
								if (flag5)
								{
									component.text = this.Title;
									OdiumConsole.Log("Odium", "Set user row title to: " + this.Title, LogLevel.Info);
								}
								else
								{
									OdiumConsole.Log("Odium", "Warning: Could not find TextMeshProUGUIEx component on Text_Title", LogLevel.Info);
								}
							}
							else
							{
								OdiumConsole.Log("Odium", "Warning: Could not find Text_Title", LogLevel.Info);
							}
						}
						else
						{
							OdiumConsole.Log("Odium", "Warning: Could not find LeftItemContainer", LogLevel.Info);
						}
					}
					else
					{
						OdiumConsole.Log("Odium", "Warning: Could not find Header_H2", LogLevel.Info);
					}
				}
				catch (Exception ex)
				{
					OdiumConsole.Log("Odium", "Error setting row title: " + ex.Message, LogLevel.Info);
				}
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001B31C File Offset: 0x0001951C
		public MMUserButton AddButton(string text, Action action = null, Sprite icon = null)
		{
			return new MMUserButton(this, text, action, icon);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001B338 File Offset: 0x00019538
		public void Destroy()
		{
			bool flag = this.RowObject != null;
			if (flag)
			{
				Object.DestroyImmediate(this.RowObject);
				this.RowObject = null;
				OdiumConsole.Log("Odium", "Destroyed user action row: " + this.Title, LogLevel.Info);
			}
		}
	}
}

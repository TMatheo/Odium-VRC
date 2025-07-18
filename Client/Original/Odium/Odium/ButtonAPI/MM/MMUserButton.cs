using System;
using Odium.Odium;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI;

namespace Odium.ButtonAPI.MM
{
	// Token: 0x02000075 RID: 117
	public class MMUserButton
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0001B388 File Offset: 0x00019588
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0001B390 File Offset: 0x00019590
		public string Text { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0001B399 File Offset: 0x00019599
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0001B3A1 File Offset: 0x000195A1
		public Action Action { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0001B3AA File Offset: 0x000195AA
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0001B3B2 File Offset: 0x000195B2
		public Sprite Icon { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0001B3BB File Offset: 0x000195BB
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0001B3C3 File Offset: 0x000195C3
		public GameObject ButtonObject { get; private set; }

		// Token: 0x06000332 RID: 818 RVA: 0x0001B3CC File Offset: 0x000195CC
		public MMUserButton(MMUserActionRow actionRow, string text, Action action = null, Sprite icon = null)
		{
			this.Text = text;
			this.Action = action;
			this.Icon = icon;
			bool flag = ((actionRow != null) ? actionRow.RowObject : null) != null;
			if (flag)
			{
				this.AttachToRow(actionRow.RowObject);
			}
			else
			{
				Debug.LogError("User action row is null or not properly initialized");
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001B434 File Offset: 0x00019634
		private void AttachToRow(GameObject customRow)
		{
			try
			{
				bool flag = customRow == null;
				if (flag)
				{
					Debug.LogError("Custom user row is null");
				}
				else
				{
					Transform transform = customRow.transform.Find("CellGrid_MM_Content");
					bool flag2 = transform == null;
					if (flag2)
					{
						Debug.LogError("Could not find CellGrid_MM_Content in custom user row");
					}
					else
					{
						Transform transform2 = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_UserDetail/ScrollRect/Viewport/VerticalLayoutGroup");
						Transform transform3 = (transform2 != null) ? transform2.Find("Row3") : null;
						Transform transform4 = (transform3 != null) ? transform3.Find("CellGrid_MM_Content") : null;
						Button button = (transform4 != null) ? transform4.GetComponentInChildren<Button>() : null;
						bool flag3 = button == null;
						if (flag3)
						{
							Debug.LogError("Could not find template button to clone");
						}
						else
						{
							OdiumConsole.Log("Odium", "Adding button '" + this.Text + "' to custom user row", LogLevel.Info);
							this.ButtonObject = Object.Instantiate<GameObject>(button.gameObject);
							this.ButtonObject.transform.SetParent(transform, false);
							this.ButtonObject.transform.localScale = Vector3.one;
							this.ButtonObject.transform.localPosition = Vector3.zero;
							this.ButtonObject.transform.localRotation = Quaternion.identity;
							Button component = this.ButtonObject.GetComponent<Button>();
							bool flag4 = component == null;
							if (flag4)
							{
								Debug.LogError("Cloned object doesn't have Button component");
								Object.DestroyImmediate(this.ButtonObject);
							}
							else
							{
								this.UpdateButtonText();
								this.UpdateButtonIcon();
								component.onClick.RemoveAllListeners();
								component.onClick.AddListener(delegate()
								{
									OdiumConsole.Log("Odium", "Custom user button '" + this.Text + "' clicked", LogLevel.Info);
									Action action = this.Action;
									if (action != null)
									{
										action();
									}
								});
								this.ButtonObject.name = "Odium_CustomUserButton_" + this.Text.Replace(" ", "");
								OdiumConsole.Log("Odium", "Successfully added button '" + this.Text + "' to custom user row", LogLevel.Info);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error adding button to custom user row: " + ex.Message);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001B694 File Offset: 0x00019894
		private void UpdateButtonText()
		{
			bool flag = this.ButtonObject == null;
			if (!flag)
			{
				TextMeshProUGUIEx textMeshProUGUIEx = this.ButtonObject.GetComponentInChildren<TextMeshProUGUIEx>();
				bool flag2 = textMeshProUGUIEx == null;
				if (flag2)
				{
					Transform transform = this.ButtonObject.transform.Find("Text_ButtonName");
					bool flag3 = transform != null;
					if (flag3)
					{
						textMeshProUGUIEx = transform.GetComponent<TextMeshProUGUIEx>();
					}
				}
				bool flag4 = textMeshProUGUIEx != null;
				if (flag4)
				{
					textMeshProUGUIEx.text = this.Text;
					OdiumConsole.Log("Odium", "Set button text to: " + this.Text, LogLevel.Info);
				}
				else
				{
					OdiumConsole.Log("Odium", "Warning: Could not find text component", LogLevel.Info);
				}
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001B748 File Offset: 0x00019948
		private void UpdateButtonIcon()
		{
			bool flag = this.ButtonObject == null;
			if (!flag)
			{
				ImageEx imageEx = null;
				Transform transform = this.ButtonObject.transform.Find("Text_ButtonName");
				bool flag2 = transform != null;
				if (flag2)
				{
					Transform transform2 = transform.Find("Icon");
					bool flag3 = transform2 != null;
					if (flag3)
					{
						imageEx = transform2.GetComponent<ImageEx>();
					}
				}
				bool flag4 = this.Icon != null && imageEx != null;
				if (flag4)
				{
					imageEx.sprite = this.Icon;
					imageEx.enabled = true;
					OdiumConsole.Log("Odium", "Icon set successfully", LogLevel.Info);
				}
				else
				{
					bool flag5 = imageEx != null;
					if (flag5)
					{
						imageEx.enabled = false;
						OdiumConsole.Log("Odium", "Icon hidden (no sprite provided)", LogLevel.Info);
					}
				}
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001B828 File Offset: 0x00019A28
		public void Destroy()
		{
			bool flag = this.ButtonObject != null;
			if (flag)
			{
				Object.DestroyImmediate(this.ButtonObject);
				this.ButtonObject = null;
				OdiumConsole.Log("Odium", "Destroyed user button: " + this.Text, LogLevel.Info);
			}
		}
	}
}

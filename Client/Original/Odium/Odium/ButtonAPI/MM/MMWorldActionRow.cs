using System;
using Odium.Odium;
using UnityEngine;
using UnityEngine.UI;

namespace Odium.ButtonAPI.MM
{
	// Token: 0x02000077 RID: 119
	public class MMWorldActionRow
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0001B8CB File Offset: 0x00019ACB
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0001B8D3 File Offset: 0x00019AD3
		public GameObject HeaderObject { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0001B8DC File Offset: 0x00019ADC
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0001B8E4 File Offset: 0x00019AE4
		public GameObject ActionsObject { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0001B8ED File Offset: 0x00019AED
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0001B8F5 File Offset: 0x00019AF5
		public string Title { get; private set; }

		// Token: 0x0600033F RID: 831 RVA: 0x0001B8FE File Offset: 0x00019AFE
		public MMWorldActionRow(string title)
		{
			this.Title = title;
			this.CreateRow();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001B918 File Offset: 0x00019B18
		private void CreateRow()
		{
			try
			{
				Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_MM_WorldInformation/Panel_World_Information/Content/Viewport/BodyContainer_World_Details/ScrollRect/Viewport/VerticalLayoutGroup");
				bool flag = transform == null;
				if (flag)
				{
					Debug.LogError("Could not find menu container");
				}
				else
				{
					Transform transform2 = transform.Find("Header_Actions");
					Transform transform3 = transform.Find("Actions");
					bool flag2 = transform2 == null;
					if (flag2)
					{
						Debug.LogError("Could not find Header_Actions to clone");
					}
					else
					{
						bool flag3 = transform3 == null;
						if (flag3)
						{
							Debug.LogError("Could not find Actions to clone");
						}
						else
						{
							OdiumConsole.Log("Odium", "Creating new action row with title: " + this.Title, LogLevel.Info);
							this.HeaderObject = Object.Instantiate<GameObject>(transform2.gameObject);
							this.HeaderObject.transform.SetParent(transform, false);
							this.HeaderObject.transform.localScale = Vector3.one;
							this.HeaderObject.name = "Odium_Header_" + this.Title.Replace(" ", "");
							this.ActionsObject = Object.Instantiate<GameObject>(transform3.gameObject);
							this.ActionsObject.transform.SetParent(transform, false);
							this.ActionsObject.transform.localScale = Vector3.one;
							this.ActionsObject.name = "Odium_Actions_" + this.Title.Replace(" ", "");
							int siblingIndex = transform3.GetSiblingIndex();
							this.HeaderObject.transform.SetSiblingIndex(siblingIndex + 1);
							this.ActionsObject.transform.SetSiblingIndex(siblingIndex + 2);
							this.UpdateHeaderTitle();
							this.ClearAllButtons();
							OdiumConsole.Log("Odium", "Successfully created custom action row: " + this.Title, LogLevel.Info);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error creating custom action row: " + ex.Message);
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001BB40 File Offset: 0x00019D40
		private void UpdateHeaderTitle()
		{
			bool flag = this.HeaderObject == null;
			if (!flag)
			{
				try
				{
					TextMeshProUGUIEx textMeshProUGUIEx = this.HeaderObject.GetComponentInChildren<TextMeshProUGUIEx>();
					bool flag2 = textMeshProUGUIEx == null;
					if (flag2)
					{
						Transform transform = this.HeaderObject.transform.Find("LeftItemContainer");
						bool flag3 = transform != null;
						if (flag3)
						{
							Transform transform2 = transform.Find("Text_Title");
							bool flag4 = transform2 != null;
							if (flag4)
							{
								textMeshProUGUIEx = transform2.GetComponent<TextMeshProUGUIEx>();
							}
						}
					}
					bool flag5 = textMeshProUGUIEx != null;
					if (flag5)
					{
						textMeshProUGUIEx.text = this.Title;
						OdiumConsole.Log("Odium", "Set header title to: " + this.Title, LogLevel.Info);
					}
					else
					{
						OdiumConsole.Log("Odium", "Warning: Could not find text component in header", LogLevel.Info);
					}
				}
				catch (Exception ex)
				{
					OdiumConsole.Log("Odium", "Error updating header title: " + ex.Message, LogLevel.Info);
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001BC4C File Offset: 0x00019E4C
		private void ClearAllButtons()
		{
			bool flag = this.ActionsObject == null;
			if (!flag)
			{
				for (int i = this.ActionsObject.transform.childCount - 1; i >= 0; i--)
				{
					Transform child = this.ActionsObject.transform.GetChild(i);
					bool flag2 = child.GetComponent<Button>() != null;
					if (flag2)
					{
						Object.DestroyImmediate(child.gameObject);
					}
				}
				OdiumConsole.Log("Odium", "Cleared existing buttons from cloned actions container", LogLevel.Info);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		public MMWorldButton AddButton(string text, Action action = null, Sprite icon = null)
		{
			return new MMWorldButton(this, text, action, icon);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001BCF4 File Offset: 0x00019EF4
		public void Destroy()
		{
			bool flag = this.HeaderObject != null;
			if (flag)
			{
				Object.DestroyImmediate(this.HeaderObject);
				this.HeaderObject = null;
			}
			bool flag2 = this.ActionsObject != null;
			if (flag2)
			{
				Object.DestroyImmediate(this.ActionsObject);
				this.ActionsObject = null;
			}
			OdiumConsole.Log("Odium", "Destroyed action row: " + this.Title, LogLevel.Info);
		}
	}
}

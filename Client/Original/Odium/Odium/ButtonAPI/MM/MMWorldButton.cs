using System;
using Odium.Odium;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI;

namespace Odium.ButtonAPI.MM
{
	// Token: 0x02000078 RID: 120
	public class MMWorldButton
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0001BD6A File Offset: 0x00019F6A
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0001BD72 File Offset: 0x00019F72
		public string Text { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0001BD7B File Offset: 0x00019F7B
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0001BD83 File Offset: 0x00019F83
		public Action Action { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0001BD8C File Offset: 0x00019F8C
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0001BD94 File Offset: 0x00019F94
		public Sprite Icon { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0001BD9D File Offset: 0x00019F9D
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0001BDA5 File Offset: 0x00019FA5
		public GameObject ButtonObject { get; private set; }

		// Token: 0x0600034D RID: 845 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		public MMWorldButton(MMWorldActionRow actionRow, string text, Action action = null, Sprite icon = null)
		{
			this.Text = text;
			this.Action = action;
			this.Icon = icon;
			bool flag = ((actionRow != null) ? actionRow.ActionsObject : null) != null;
			if (flag)
			{
				this.AttachToRow(actionRow.ActionsObject);
			}
			else
			{
				Debug.LogError("Action row is null or not properly initialized");
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001BE18 File Offset: 0x0001A018
		private void AttachToRow(GameObject actionsContainer)
		{
			try
			{
				bool flag = actionsContainer == null;
				if (flag)
				{
					Debug.LogError("Actions container is null");
				}
				else
				{
					Transform transform = AssignedVariables.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_MM_WorldInformation/Panel_World_Information/Content/Viewport/BodyContainer_World_Details/ScrollRect/Viewport/VerticalLayoutGroup");
					Transform transform2 = (transform != null) ? transform.Find("Actions") : null;
					GameObject gameObject = null;
					bool flag2 = transform2 != null;
					if (flag2)
					{
						Transform transform3 = transform2.Find("MakeHome");
						bool flag3 = transform3 != null;
						if (flag3)
						{
							gameObject = transform3.gameObject;
						}
						else
						{
							for (int i = 0; i < transform2.childCount; i++)
							{
								Transform child = transform2.GetChild(i);
								bool flag4 = child.GetComponent<Button>() != null;
								if (flag4)
								{
									gameObject = child.gameObject;
									break;
								}
							}
						}
					}
					bool flag5 = gameObject == null;
					if (flag5)
					{
						Debug.LogError("Could not find template button");
					}
					else
					{
						OdiumConsole.Log("Odium", "Adding button '" + this.Text + "' to actions container", LogLevel.Info);
						this.ButtonObject = Object.Instantiate<GameObject>(gameObject);
						this.ButtonObject.transform.SetParent(actionsContainer.transform, false);
						this.ButtonObject.transform.localScale = Vector3.one;
						this.ButtonObject.transform.localPosition = Vector3.zero;
						this.ButtonObject.transform.localRotation = Quaternion.identity;
						Button component = this.ButtonObject.GetComponent<Button>();
						bool flag6 = component == null;
						if (flag6)
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
								OdiumConsole.Log("Odium", "Custom button '" + this.Text + "' clicked", LogLevel.Info);
								Action action = this.Action;
								if (action != null)
								{
									action();
								}
							});
							this.ButtonObject.name = "Odium_CustomButton_" + this.Text.Replace(" ", "");
							OdiumConsole.Log("Odium", "Successfully added button '" + this.Text + "' to actions container", LogLevel.Info);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error adding button to actions container: " + ex.Message);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001C09C File Offset: 0x0001A29C
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

		// Token: 0x06000350 RID: 848 RVA: 0x0001C150 File Offset: 0x0001A350
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

		// Token: 0x06000351 RID: 849 RVA: 0x0001C230 File Offset: 0x0001A430
		public void Destroy()
		{
			bool flag = this.ButtonObject != null;
			if (flag)
			{
				Object.DestroyImmediate(this.ButtonObject);
				this.ButtonObject = null;
				OdiumConsole.Log("Odium", "Destroyed button: " + this.Text, LogLevel.Info);
			}
		}
	}
}

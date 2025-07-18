using System;
using Odium.ButtonAPI.QM;
using Odium.Components;
using UnityEngine;

namespace Odium.QMPages
{
	// Token: 0x02000026 RID: 38
	internal class Visuals
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00009F88 File Offset: 0x00008188
		public static void InitializePage(QMNestedMenu movementButton, Sprite buttonImage)
		{
			new QMToggleButton(movementButton, 2f, 0f, "Bone ESP", delegate()
			{
				BoneESP.SetEnabled(true);
			}, delegate()
			{
				BoneESP.SetEnabled(false);
			}, "Toggle Flight Mode", false, buttonImage);
			new QMToggleButton(movementButton, 3f, 0f, "Box ESP", delegate()
			{
				BoxESP.SetEnabled(true);
			}, delegate()
			{
				BoxESP.SetEnabled(false);
			}, "Toggle Flight Mode", false, buttonImage);
		}
	}
}

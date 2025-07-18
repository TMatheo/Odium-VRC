using System;
using Odium.ButtonAPI.QM;
using Odium.Components;
using Odium.Modules;
using UnityEngine;

namespace Odium.QMPages
{
	// Token: 0x02000023 RID: 35
	internal class Movement
	{
		// Token: 0x060000FE RID: 254 RVA: 0x000094BC File Offset: 0x000076BC
		public static void InitializePage(QMNestedMenu movementButton, Sprite buttonImage)
		{
			Sprite icon = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\PlusIcon.png", 100f);
			Sprite icon2 = SpriteUtil.LoadFromDisk(Environment.CurrentDirectory + "\\Odium\\MinusIcon.png", 100f);
			QMToggleButton qmtoggleButton = new QMToggleButton(movementButton, 2.5f, 1f, "Flight", delegate()
			{
				FlyComponent.FlyEnabled = true;
			}, delegate()
			{
				FlyComponent.FlyEnabled = false;
			}, "Toggle Flight Mode", false, buttonImage);
			QMToggleButton qmtoggleButton2 = new QMToggleButton(movementButton, 2f, 0f, "Jetpack", delegate()
			{
				Jetpack.Activate(true);
			}, delegate()
			{
				Jetpack.Activate(false);
			}, "Allows you to fly", false, buttonImage);
			QMToggleButton qmtoggleButton3 = new QMToggleButton(movementButton, 3f, 0f, "SpinBot", delegate()
			{
				SpinBotModule.SetActive(true);
			}, delegate()
			{
				SpinBotModule.SetActive(false);
			}, "HvH mode", false, buttonImage);
			QMSingleButton qmsingleButton = new QMSingleButton(movementButton, 2f, 3f, "Fly Speed", delegate()
			{
				FlyComponent.FlySpeed += 0.1f;
			}, "Increase Fly Speed", false, icon, buttonImage);
			QMSingleButton qmsingleButton2 = new QMSingleButton(movementButton, 3f, 3f, "Fly Speed", delegate()
			{
				FlyComponent.FlySpeed -= 0.1f;
			}, "Decrease Fly Speed", false, icon2, buttonImage);
		}
	}
}

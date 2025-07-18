using System;
using System.Collections;
using MelonLoader;
using UnityEngine;
using VRC.Localization;

namespace VampClient.Api
{
	// Token: 0x02000018 RID: 24
	public class ToastBase
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000555C File Offset: 0x0000375C
		public static void Toast(string content, string description = null, Sprite icon = null, float duration = 5f)
		{
			LocalizableString param_ = LocalizableStringExtensions.Localize(content, null, null, null);
			LocalizableString param_2 = LocalizableStringExtensions.Localize(description, null, null, null);
			VRCUiManager.field_Private_Static_VRCUiManager_0.field_Private_HudController_0.notification.Method_Public_Void_Sprite_LocalizableString_LocalizableString_Single_Object1PublicTBoTUnique_1_Boolean_0(icon, param_, param_2, duration, null);
			MelonLogger.Msg(string.Concat(new string[]
			{
				"\n",
				content,
				"\n",
				description,
				"\n\n"
			}));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000055C9 File Offset: 0x000037C9
		public static IEnumerator DelayToast(float delay, string content, string description = null, Sprite icon = null, float duration = 5f)
		{
			ToastBase.<DelayToast>d__1 <DelayToast>d__ = new ToastBase.<DelayToast>d__1(0);
			<DelayToast>d__.delay = delay;
			<DelayToast>d__.content = content;
			<DelayToast>d__.description = description;
			<DelayToast>d__.icon = icon;
			<DelayToast>d__.duration = duration;
			return <DelayToast>d__;
		}
	}
}

using System;
using UnityEngine;

namespace Odium.ButtonAPI.QM
{
	// Token: 0x02000084 RID: 132
	public class CoroutineStarter : MonoBehaviour
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001EF78 File Offset: 0x0001D178
		public static CoroutineStarter Instance
		{
			get
			{
				bool flag = CoroutineStarter._instance == null;
				if (flag)
				{
					GameObject gameObject = new GameObject("CoroutineStarter");
					CoroutineStarter._instance = gameObject.AddComponent<CoroutineStarter>();
					Object.DontDestroyOnLoad(gameObject);
				}
				return CoroutineStarter._instance;
			}
		}

		// Token: 0x040001DC RID: 476
		private static CoroutineStarter _instance;
	}
}

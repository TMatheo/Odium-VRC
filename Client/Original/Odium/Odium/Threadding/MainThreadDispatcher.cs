using System;
using System.Collections;
using System.Collections.Generic;
using MelonLoader;

namespace Odium.Threadding
{
	// Token: 0x0200008A RID: 138
	public static class MainThreadDispatcher
	{
		// Token: 0x060003DB RID: 987 RVA: 0x000205B8 File Offset: 0x0001E7B8
		public static void Initialize()
		{
			bool initialized = MainThreadDispatcher._initialized;
			if (!initialized)
			{
				MelonEvents.OnGUI.Subscribe(new LemonAction(MainThreadDispatcher.ProcessQueue), 0, false);
				MainThreadDispatcher._initialized = true;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000205F0 File Offset: 0x0001E7F0
		private static void ProcessQueue()
		{
			object @lock = MainThreadDispatcher._lock;
			lock (@lock)
			{
				while (MainThreadDispatcher._executionQueue.Count > 0)
				{
					try
					{
						MainThreadDispatcher._executionQueue.Dequeue()();
					}
					catch (Exception arg)
					{
						MelonLogger.Error(string.Format("Error executing main thread action: {0}", arg));
					}
				}
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0002067C File Offset: 0x0001E87C
		public static void Enqueue(Action action)
		{
			bool flag = action == null;
			if (!flag)
			{
				object @lock = MainThreadDispatcher._lock;
				lock (@lock)
				{
					MainThreadDispatcher._executionQueue.Enqueue(action);
				}
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000206D0 File Offset: 0x0001E8D0
		public static void EnqueueCoroutine(IEnumerator coroutine)
		{
			MainThreadDispatcher.Enqueue(delegate
			{
				MelonCoroutines.Start(coroutine);
			});
		}

		// Token: 0x040001EA RID: 490
		private static readonly Queue<Action> _executionQueue = new Queue<Action>();

		// Token: 0x040001EB RID: 491
		private static readonly object _lock = new object();

		// Token: 0x040001EC RID: 492
		private static bool _initialized = false;
	}
}

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Il2CppSystem;
using Il2CppSystem.IO;
using Il2CppSystem.Reflection;
using Il2CppSystem.Runtime.Serialization.Formatters.Binary;

namespace Odium.Components
{
	// Token: 0x0200006C RID: 108
	public static class Serializer
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x00018CC4 File Offset: 0x00016EC4
		public static byte[] Il2ToByteArray(this Object obj)
		{
			bool flag = obj == null;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream memoryStream = new MemoryStream();
					binaryFormatter.Serialize(memoryStream, obj);
					result = memoryStream.ToArray();
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, null);
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00018D24 File Offset: 0x00016F24
		public static byte[] ManagedToByteArray(this object obj)
		{
			bool flag = obj == null;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream memoryStream = new MemoryStream();
					binaryFormatter.Serialize(memoryStream, obj);
					result = memoryStream.ToArray();
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, null);
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00018D80 File Offset: 0x00016F80
		public static T FromByteArray<T>(this byte[] data)
		{
			bool flag = data == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					using (MemoryStream memoryStream = new MemoryStream(data))
					{
						object obj = binaryFormatter.Deserialize(memoryStream);
						result = (T)((object)obj);
					}
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, null);
					result = default(T);
				}
			}
			return result;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00018E0C File Offset: 0x0001700C
		public static T IL2CPPFromByteArray<T>(this byte[] data)
		{
			bool flag = data == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream serializationStream = new MemoryStream(data);
					object obj = binaryFormatter.Deserialize(serializationStream);
					result = (T)((object)obj);
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, null);
					result = default(T);
				}
			}
			return result;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00018E80 File Offset: 0x00017080
		public static T FromIL2CPPToManaged<T>(this Object obj)
		{
			return obj.Il2ToByteArray().FromByteArray<T>();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00018EA0 File Offset: 0x000170A0
		public static T FromManagedToIL2CPP<T>(this object obj)
		{
			return obj.ManagedToByteArray().IL2CPPFromByteArray<T>();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00018EC0 File Offset: 0x000170C0
		public static object[] FromIL2CPPArrayToManagedArray(this Object[] obj)
		{
			object[] array = new object[obj.Length];
			for (int i = 0; i < obj.Length; i++)
			{
				bool flag = obj[i].GetIl2CppType().Attributes == TypeAttributes.Serializable;
				if (flag)
				{
					array[i] = obj[i].FromIL2CPPToManaged<object>();
				}
				else
				{
					array[i] = obj[i];
				}
			}
			return array;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00018F1C File Offset: 0x0001711C
		public static Object[] FromManagedArrayToIL2CPPArray(this object[] obj)
		{
			Object[] array = new Object[obj.Length];
			for (int i = 0; i < obj.Length; i++)
			{
				bool flag = obj[i].GetType().Attributes == TypeAttributes.Serializable;
				if (flag)
				{
					array[i] = obj[i].FromManagedToIL2CPP<Object>();
				}
				else
				{
					array[i] = (Object)obj[i];
				}
			}
			return array;
		}
	}
}

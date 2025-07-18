using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MelonLoader;
using Odium;
using UnityEngine;

namespace OdiumLoader
{
	// Token: 0x02000015 RID: 21
	public class OdiumModuleLoader
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00004648 File Offset: 0x00002848
		public static void OnApplicationStart()
		{
			OdiumConsole.LogGradient("ModuleLoader", "Odium Module Loader starting...", LogLevel.Info, false);
			OdiumModuleLoader.LoadModules();
			foreach (OdiumModule odiumModule in OdiumModuleLoader.loadedModules)
			{
				bool isEnabled = odiumModule.IsEnabled;
				if (isEnabled)
				{
					try
					{
						odiumModule.OnApplicationStart();
					}
					catch (Exception arg)
					{
						OdiumConsole.LogGradient("ModuleLoader", string.Format("Error in {0}.OnApplicationStart(): {1}", odiumModule.ModuleName, arg), LogLevel.Error, false);
					}
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000046F8 File Offset: 0x000028F8
		public static void OnUpdate()
		{
			foreach (OdiumModule odiumModule in OdiumModuleLoader.loadedModules)
			{
				bool isEnabled = odiumModule.IsEnabled;
				if (isEnabled)
				{
					try
					{
						odiumModule.OnUpdate();
					}
					catch (Exception arg)
					{
						OdiumConsole.LogGradient("ModuleLoader", string.Format("Error in {0}.OnUpdate(): {1}", odiumModule.ModuleName, arg), LogLevel.Error, false);
					}
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004790 File Offset: 0x00002990
		public static void OnFixedUpdate()
		{
			foreach (OdiumModule odiumModule in OdiumModuleLoader.loadedModules)
			{
				bool isEnabled = odiumModule.IsEnabled;
				if (isEnabled)
				{
					try
					{
						odiumModule.OnFixedUpdate();
					}
					catch (Exception arg)
					{
						OdiumConsole.LogGradient("ModuleLoader", string.Format("Error in {0}.OnFixedUpdate(): {1}", odiumModule.ModuleName, arg), LogLevel.Error, false);
					}
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004828 File Offset: 0x00002A28
		public static void OnLateUpdate()
		{
			foreach (OdiumModule odiumModule in OdiumModuleLoader.loadedModules)
			{
				bool isEnabled = odiumModule.IsEnabled;
				if (isEnabled)
				{
					try
					{
						odiumModule.OnLateUpdate();
					}
					catch (Exception arg)
					{
						OdiumConsole.LogGradient("ModuleLoader", string.Format("Error in {0}.OnLateUpdate(): {1}", odiumModule.ModuleName, arg), LogLevel.Error, false);
					}
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000048C0 File Offset: 0x00002AC0
		public static void OnApplicationQuit()
		{
			foreach (OdiumModule odiumModule in OdiumModuleLoader.loadedModules)
			{
				bool isEnabled = odiumModule.IsEnabled;
				if (isEnabled)
				{
					try
					{
						odiumModule.OnApplicationQuit();
					}
					catch (Exception arg)
					{
						OdiumConsole.LogGradient("ModuleLoader", string.Format("Error in {0}.OnApplicationQuit(): {1}", odiumModule.ModuleName, arg), LogLevel.Error, false);
					}
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004958 File Offset: 0x00002B58
		public static void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			foreach (OdiumModule odiumModule in OdiumModuleLoader.loadedModules)
			{
				bool isEnabled = odiumModule.IsEnabled;
				if (isEnabled)
				{
					try
					{
						odiumModule.OnSceneLoaded(buildIndex, sceneName);
					}
					catch (Exception arg)
					{
						OdiumConsole.LogGradient("ModuleLoader", string.Format("Error in {0}.OnSceneLoaded(): {1}", odiumModule.ModuleName, arg), LogLevel.Error, false);
					}
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000049F0 File Offset: 0x00002BF0
		public static void OnSceneWasUnloaded(int buildIndex, string sceneName)
		{
			foreach (OdiumModule odiumModule in OdiumModuleLoader.loadedModules)
			{
				bool isEnabled = odiumModule.IsEnabled;
				if (isEnabled)
				{
					try
					{
						odiumModule.OnSceneUnloaded(buildIndex, sceneName);
					}
					catch (Exception arg)
					{
						OdiumConsole.LogGradient("ModuleLoader", string.Format("Error in {0}.OnSceneUnloaded(): {1}", odiumModule.ModuleName, arg), LogLevel.Error, false);
					}
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004A88 File Offset: 0x00002C88
		public static void LoadModules()
		{
			try
			{
				string text = Path.Combine(Environment.CurrentDirectory, "OdiumModules");
				bool flag = !Directory.Exists(text);
				if (flag)
				{
					Directory.CreateDirectory(text);
					OdiumConsole.LogGradient("ModuleLoader", "Created OdiumModules directory at: " + text, LogLevel.Info, false);
					OdiumConsole.LogGradient("ModuleLoader", "Place your module DLL files in this folder to load them.", LogLevel.Info, false);
				}
				else
				{
					OdiumConsole.LogGradient("ModuleLoader", "Loading modules from: " + text, LogLevel.Info, false);
					string[] files = Directory.GetFiles(text, "*.dll", SearchOption.TopDirectoryOnly);
					bool flag2 = files.Length == 0;
					if (flag2)
					{
						OdiumConsole.LogGradient("ModuleLoader", "No module DLL files found in OdiumModules folder.", LogLevel.Warning, false);
					}
					else
					{
						List<Assembly> list = new List<Assembly>();
						foreach (string text2 in files)
						{
							try
							{
								string fileName = Path.GetFileName(text2);
								OdiumConsole.LogGradient("ModuleLoader", "Loading assembly: " + fileName, LogLevel.Info, false);
								Assembly item = Assembly.LoadFrom(text2);
								list.Add(item);
								OdiumConsole.LogGradient("ModuleLoader", "Successfully loaded assembly: " + fileName, LogLevel.Info, false);
							}
							catch (Exception ex)
							{
								OdiumConsole.LogGradient("ModuleLoader", "Failed to load assembly " + Path.GetFileName(text2) + ": " + ex.Message, LogLevel.Info, false);
							}
						}
						List<Assembly> collection = (from a in AppDomain.CurrentDomain.GetAssemblies()
						where !a.IsDynamic && a.Location.Contains("OdiumModules")
						select a).ToList<Assembly>();
						list.AddRange(collection);
						foreach (Assembly assembly in list)
						{
							try
							{
								List<Type> list2 = (from t in assembly.GetTypes()
								where t.IsSubclassOf(typeof(OdiumModule)) && !t.IsAbstract
								select t).ToList<Type>();
								bool flag3 = list2.Any<Type>();
								if (flag3)
								{
									MelonLogger.Msg(string.Format("Found {0} module(s) in {1}", list2.Count, assembly.GetName().Name));
								}
								foreach (Type type in list2)
								{
									try
									{
										OdiumModuleAttribute customAttribute = type.GetCustomAttribute<OdiumModuleAttribute>();
										bool flag4 = customAttribute != null && !customAttribute.AutoLoad;
										if (flag4)
										{
											OdiumConsole.LogGradient("ModuleLoader", "Skipping module " + type.Name + " (AutoLoad disabled)", LogLevel.Info, false);
										}
										else
										{
											OdiumModule odiumModule = (OdiumModule)Activator.CreateInstance(type);
											MelonLogger.Instance logger = new MelonLogger.Instance("[" + odiumModule.ModuleName + "]");
											odiumModule.SetLogger(logger);
											OdiumModuleLoader.loadedModules.Add(odiumModule);
											OdiumConsole.LogGradient("ModuleLoader", string.Concat(new string[]
											{
												"Loaded module: ",
												odiumModule.ModuleName,
												" v",
												odiumModule.ModuleVersion,
												" by ",
												odiumModule.ModuleAuthor
											}), LogLevel.Info, false);
											odiumModule.OnModuleLoad();
										}
									}
									catch (Exception arg)
									{
										OdiumConsole.LogGradient("ModuleLoader", string.Format("Failed to instantiate module {0}: {1}", type.Name, arg), LogLevel.Info, false);
									}
								}
							}
							catch (Exception ex2)
							{
								OdiumConsole.LogGradient("ModuleLoader", "Could not process assembly " + assembly.GetName().Name + ": " + ex2.Message, LogLevel.Error, false);
							}
						}
						MelonLogger.Msg(string.Format("Successfully loaded {0} modules total from OdiumModules folder", OdiumModuleLoader.loadedModules.Count));
						bool flag5 = OdiumModuleLoader.loadedModules.Count == 0;
						if (flag5)
						{
							OdiumConsole.LogGradient("ModuleLoader", "No valid modules found. Make sure your module DLLs:", LogLevel.Info, false);
							OdiumConsole.LogGradient("ModuleLoader", "  - Are placed in the OdiumModules folder", LogLevel.Info, false);
							OdiumConsole.LogGradient("ModuleLoader", "  - Contain classes that inherit from OdiumModule", LogLevel.Info, false);
							OdiumConsole.LogGradient("ModuleLoader", "  - Are compiled for the same .NET version", LogLevel.Info, false);
						}
					}
				}
			}
			catch (Exception arg2)
			{
				OdiumConsole.LogGradient("ModuleLoader", string.Format("Error loading modules: {0}", arg2), LogLevel.Error, false);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004F6C File Offset: 0x0000316C
		public static T GetModule<T>() where T : OdiumModule
		{
			return OdiumModuleLoader.loadedModules.OfType<T>().FirstOrDefault<T>();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004F90 File Offset: 0x00003190
		public static List<OdiumModule> GetAllModules()
		{
			return new List<OdiumModule>(OdiumModuleLoader.loadedModules);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004FAC File Offset: 0x000031AC
		public static void SetModuleEnabled<T>(bool enabled) where T : OdiumModule
		{
			T module = OdiumModuleLoader.GetModule<T>();
			bool flag = module != null;
			if (flag)
			{
				module.IsEnabled = enabled;
				OdiumConsole.LogGradient("ModuleLoader", "Module " + module.ModuleName + " " + (enabled ? "enabled" : "disabled"), LogLevel.Info, false);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005014 File Offset: 0x00003214
		public static T GetOrCreateCachedType<T>() where T : class, new()
		{
			Type typeFromHandle = typeof(T);
			bool flag = !OdiumModuleLoader.typeCache.ContainsKey(typeFromHandle);
			if (flag)
			{
				OdiumModuleLoader.typeCache[typeFromHandle] = Activator.CreateInstance<T>();
			}
			return (T)((object)OdiumModuleLoader.typeCache[typeFromHandle]);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000506C File Offset: 0x0000326C
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Object.FindObjectsOfType<T>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005088 File Offset: 0x00003288
		public static T FindObjectOfType<T>() where T : Object
		{
			return Object.FindObjectOfType<T>();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000050A0 File Offset: 0x000032A0
		public static void ReloadModules()
		{
			OdiumConsole.LogGradient("ModuleLoader", "Reloading modules...", LogLevel.Info, false);
			foreach (OdiumModule odiumModule in OdiumModuleLoader.loadedModules)
			{
				try
				{
					odiumModule.OnApplicationQuit();
				}
				catch (Exception arg)
				{
					OdiumConsole.LogGradient("ModuleLoader", string.Format("Error during {0} cleanup: {1}", odiumModule.ModuleName, arg), LogLevel.Error, false);
				}
			}
			OdiumModuleLoader.loadedModules.Clear();
			OdiumModuleLoader.typeCache.Clear();
			OdiumModuleLoader.LoadModules();
			foreach (OdiumModule odiumModule2 in OdiumModuleLoader.loadedModules)
			{
				bool isEnabled = odiumModule2.IsEnabled;
				if (isEnabled)
				{
					try
					{
						odiumModule2.OnApplicationStart();
					}
					catch (Exception arg2)
					{
						OdiumConsole.LogGradient("ModuleLoader", string.Format("Error in {0}.OnApplicationStart() during reload: {1}", odiumModule2.ModuleName, arg2), LogLevel.Error, false);
					}
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000051E4 File Offset: 0x000033E4
		public static string GetModulesPath()
		{
			return Path.Combine(Environment.CurrentDirectory, "OdiumModules");
		}

		// Token: 0x04000038 RID: 56
		private static List<OdiumModule> loadedModules = new List<OdiumModule>();

		// Token: 0x04000039 RID: 57
		private static Dictionary<Type, object> typeCache = new Dictionary<Type, object>();
	}
}

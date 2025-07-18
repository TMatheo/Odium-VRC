using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Odium.Modules;
using Odium.Threadding;

namespace Odium.ApplicationBot
{
	// Token: 0x02000095 RID: 149
	internal class SocketConnection
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x000213B4 File Offset: 0x0001F5B4
		public static void SendCommandToClients(string Command)
		{
			OdiumConsole.LogGradient("BotServer", string.Format("[{0}] Sending Message ({1})", DateTime.Now, Command), LogLevel.Info, false);
			(from s in SocketConnection.ServerHandlers
			where s != null
			select s).ToList<Socket>().ForEach(delegate(Socket s)
			{
				s.Send(Encoding.ASCII.GetBytes(Command));
			});
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00021438 File Offset: 0x0001F638
		public static void SendMessageToServer(string message)
		{
			bool flag = SocketConnection.clientSocket != null && SocketConnection.clientSocket.Connected;
			if (flag)
			{
				try
				{
					byte[] bytes = Encoding.ASCII.GetBytes(message);
					SocketConnection.clientSocket.Send(bytes);
					OdiumConsole.LogGradient("BotClient", string.Format("[{0}] Sent Message to Server ({1})", DateTime.Now, message), LogLevel.Info, false);
				}
				catch (Exception ex)
				{
					OdiumConsole.LogException(ex, null);
				}
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000214BC File Offset: 0x0001F6BC
		public static void OnClientReceiveCommand(string Command)
		{
			OdiumConsole.LogGradient("BotServer", string.Format("[{0}] Received Message ({1})", DateTime.Now, Command), LogLevel.Info, false);
			Bot.ReceiveCommand(Command);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000214E8 File Offset: 0x0001F6E8
		public static void OnServerReceiveMessage(string message, Socket clientSocket)
		{
			OdiumConsole.LogGradient("BotServer", string.Format("[{0}] Received from Bot ({1})", DateTime.Now, message), LogLevel.Info, false);
			bool flag = message.StartsWith("WORLD_JOINED:");
			if (flag)
			{
				string[] array = message.Split(new char[]
				{
					':'
				});
				bool flag2 = array.Length >= 3;
				if (flag2)
				{
					string botName = array[1];
					string worldName = array[2];
					OdiumConsole.LogGradient("BotServer", "Bot " + botName + " joined world " + worldName, LogLevel.Info, false);
					MainThreadDispatcher.Enqueue(delegate
					{
						OdiumBottomNotification.ShowNotification(string.Concat(new string[]
						{
							"[<color=#7A00FE>Bot</color>] <color=#FC7C93>",
							botName,
							"</color> joined <color=#00FE9C>",
							worldName,
							"</color>"
						}));
					});
				}
			}
			else
			{
				bool flag3 = message.StartsWith("BOT_STATUS:");
				if (flag3)
				{
					string[] array2 = message.Split(new char[]
					{
						':'
					});
					bool flag4 = array2.Length >= 3;
					if (flag4)
					{
						string str = array2[1];
						string str2 = array2[2];
						OdiumConsole.LogGradient("BotServer", "Bot " + str + " status: " + str2, LogLevel.Info, false);
					}
				}
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00021602 File Offset: 0x0001F802
		public static void StartServer()
		{
			SocketConnection.ServerHandlers.Clear();
			Task.Run(new Action(SocketConnection.HandleServer));
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00021624 File Offset: 0x0001F824
		private static void HandleServer()
		{
			IPHostEntry hostEntry = Dns.GetHostEntry("localhost");
			IPAddress ipaddress = hostEntry.AddressList[0];
			IPEndPoint localEP = new IPEndPoint(ipaddress, 11000);
			try
			{
				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				socket.Bind(localEP);
				socket.Listen(10);
				for (int i = 0; i < SocketConnection.botCount; i++)
				{
					Socket handler = socket.Accept();
					SocketConnection.ServerHandlers.Add(handler);
					Task.Run(delegate()
					{
						SocketConnection.HandleClientMessages(handler);
					});
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogException(ex, null);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000216EC File Offset: 0x0001F8EC
		private static void HandleClientMessages(Socket clientHandler)
		{
			byte[] array = new byte[1024];
			try
			{
				while (clientHandler.Connected)
				{
					int num = clientHandler.Receive(array);
					bool flag = num > 0;
					if (flag)
					{
						string @string = Encoding.ASCII.GetString(array, 0, num);
						SocketConnection.OnServerReceiveMessage(@string, clientHandler);
					}
				}
			}
			catch (Exception ex)
			{
				OdiumConsole.LogGradient("BotServer", "Client disconnected or error: " + ex.Message, LogLevel.Info, false);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00021778 File Offset: 0x0001F978
		public static void Client()
		{
			Task.Run(new Action(SocketConnection.HandleClient));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00021790 File Offset: 0x0001F990
		private static void HandleClient()
		{
			byte[] array = new byte[1024];
			OdiumConsole.LogGradient("BotServer", "Connecting to server!", LogLevel.Info, false);
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry("localhost");
				IPAddress ipaddress = hostEntry.AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(ipaddress, 11000);
				SocketConnection.clientSocket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				try
				{
					SocketConnection.clientSocket.Connect(remoteEP);
					OdiumConsole.LogGradient("BotServer", "Socket connected to " + SocketConnection.clientSocket.RemoteEndPoint.ToString(), LogLevel.Info, false);
					for (;;)
					{
						int num = SocketConnection.clientSocket.Receive(array);
						bool flag = num > 0;
						if (flag)
						{
							SocketConnection.OnClientReceiveCommand(Encoding.ASCII.GetString(array, 0, num));
						}
					}
				}
				catch (ArgumentNullException ex)
				{
					OdiumConsole.LogException(ex, null);
				}
				catch (SocketException ex2)
				{
					OdiumConsole.LogException(ex2, null);
				}
				catch (Exception ex3)
				{
					OdiumConsole.LogException(ex3, null);
				}
			}
			catch (Exception ex4)
			{
				OdiumConsole.LogException(ex4, null);
			}
		}

		// Token: 0x04000221 RID: 545
		private static readonly int botCount = 8;

		// Token: 0x04000222 RID: 546
		private static Socket clientSocket;

		// Token: 0x04000223 RID: 547
		private static List<Socket> ServerHandlers = new List<Socket>();
	}
}

using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Net;
using System.Text;
public class Cliente : MonoBehaviour {
		public string msg;
		public string resposta;

	// Use this for initialization
	void Start () {
				send ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}



		void send(){
				Socket sck = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				IPEndPoint endPoint = new IPEndPoint (IPAddress.Parse ("127.0.0.5"), 50007);
				sck.Connect (endPoint);

				msg = "oi mundo";
				byte[] msgBuffer = Encoding.Default.GetBytes (msg);
				sck.Send (msgBuffer, 0, msgBuffer.Length, 0);

				byte[] buffer = new byte[255];
				int rec = sck.Receive (buffer, 0, buffer.Length, 0);

				Array.Resize (ref buffer, rec);
				resposta = Encoding.Default.GetString (buffer);

		}
//	public static void Send(Socket socket, byte[] buffer, int offset, int size, int timeout)
//	{
//			int startTickCount = Environment.TickCount;
//			int sent = 0;  // how many bytes is already sent
//			do {
//					if (Environment.TickCount > startTickCount + timeout)
//							throw new Exception("Timeout.");
//					try {
//							sent += socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
//					}
//					catch (SocketException ex)
//					{
//							if (ex.SocketErrorCode == SocketError.WouldBlock ||
//									ex.SocketErrorCode == SocketError.IOPending ||
//									ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
//							{
//									// socket buffer is probably full, wait and try again
//									Thread.Sleep(30);
//							}
//							else
//									throw ex;  // any serious error occurr
//					}
//			} while (sent < size);
//	}


}

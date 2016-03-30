using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using SimpleJSON;

public class s_TCP : MonoBehaviour {
		//String Host = "192.168.0.104";
		String Host = "127.0.0.1";
		Int32 Port = 50014;
		public static bool teste = false;
		public  float Velocidade;
		public static String Velocidade2;
		public static String spacing2;
		public float spacing;


		public static String received;
		public static bool receivedServer;
		public bool beginTcp;
		private static Thread tcpThread;
		private IPHostEntry ipHostInfo;
		private IPAddress ipAddress;
		private IPEndPoint remoteEP;
		private Socket sender;
		byte[] bytes;
		void Start () {
				beginTcp = false;
				teste = false;
				receivedServer = false;
				Velocidade = 1.2f;
				Velocidade2 = "1.2";
				received = "1.2f";
				bytes = new byte[1024];
				spacing = 2.43f;
				spacing2 = "2.43";
				//var teste = JSONNode.Parse (json2);

				try {
						// Establish the remote endpoint for the socket.
						// This example uses port 11000 on the local computer.

						//sender.Bind(remoteEP); 

						// start listening
						//sender.Listen(1);

				ipHostInfo = Dns.Resolve (Dns.GetHostName ());
				//IPAddress ipAddress = ipHostInfo.AddressList[0];
				ipAddress = System.Net.IPAddress.Parse (Host);
				remoteEP = new IPEndPoint (ipAddress, Port);

				// Create a TCP/IP  socket.
				sender = new Socket (AddressFamily.InterNetwork, 
						SocketType.Stream, ProtocolType.Tcp);
				
				} catch (Exception e) {
						Debug.Log (e.ToString ());
				}
				//setupSocket ();
				//StartCoroutine (changeTeste ());

		}
		void Update () {
				if (GetComponent<BirdMovement>().contarTempo == true) {
						Velocidade2 = Velocidade.ToString ();
						spacing2 = spacing.ToString ();
						StartCoroutine (StartClient ());
						StartCoroutine (changeTeste ());
						print (received);
						var receivedJson = JSONNode.Parse (received);
						GetComponent<ChangeDifficulty> ().birdVelocity = float.Parse (receivedJson ["birdVelocity"]);
						GetComponent<ChangeDifficulty> ().spacing = float.Parse (receivedJson ["spacing"]);
				}

		}

		IEnumerator changeTeste(){
				if (teste == false) {
						teste = true;
						yield return new WaitForSeconds (6);
						//Velocidade += 0.5f;

						float seconds = 0;

						while (seconds != 60) {
								yield return new WaitForSeconds(6);
								Velocidade += 0.5f;
								seconds += 6;
						}
						Velocidade = 1.2f;
						spacing -= 0.2f;

						teste =false;
				}
		}
		// **********************************************
		IEnumerator StartClient() {
				//if(teste == false){
				// Data buffer for incoming data.
				if (receivedServer == false) {
						receivedServer = true;
						 tcpThread = new Thread (o => {
					

								// Connect to a remote device.
								
										// Connect the socket to the remote endpoint. Catch any errors.
										try {
								sender = new Socket (AddressFamily.InterNetwork, 
										SocketType.Stream, ProtocolType.Tcp);
												sender.Connect (remoteEP);

												print ("Socket connected to" +
												sender.RemoteEndPoint.ToString ());

												// Encode the data string into a byte array.
										byte[] msg = Encoding.ASCII.GetBytes ("{ birdVelocity: " + Velocidade2+ ", spacing: "+ spacing2 + "}");

												// Send the data through the socket.
												int bytesSent = sender.Send (msg);

												// Receive the response from the remote device.
												int bytesRec = sender.Receive (bytes);
												print ("Echoed test = " +
												Encoding.ASCII.GetString (bytes, 0, bytesRec));
												received = Encoding.ASCII.GetString (bytes, 0, bytesRec);
												// Release the socket.
												sender.Shutdown (SocketShutdown.Both);
												sender.Close ();


										} catch (ArgumentNullException ane) {
												Debug.Log ("ArgumentNullException :" + ane.ToString ());
										} catch (SocketException se) {
												Debug.Log ("SocketException : " + se.ToString ());
										} catch (Exception e) {
												Debug.Log ("Unexpected exception :" + e.ToString ());
										}




						});

						yield return sender;

						tcpThread.Start ();
				yield return new WaitForSeconds (1);
						receivedServer = false;
				}

		//}
		}

} // end class s_TCP

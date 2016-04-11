using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using SimpleJSON;

public class TCP : MonoBehaviour {
		//String Host = "192.168.0.100";
		String Host = "127.0.0.1";
		Int32 Port = 50021;
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
		public bool birdDead;
		byte[] bytes;
		public int countColision;


		void Start () {
				beginTcp = false;
				teste = false;
				receivedServer = false;
				bytes = new byte[1024];
				countColision = GetComponent<BirdMovement> ().countColision;

				connectSocket ();

				//setupSocket ();
				//StartCoroutine (changeTeste ());

		}
		void Update () {
				countColision = GetComponent<BirdMovement> ().countColision;
				birdDead = GetComponent<BirdMovement> ().dead;
				if (!sender.Connected) {
						connectSocket ();
				}
				if (GetComponent<BirdMovement>().contarTempo == true) {
						StartCoroutine (StartClient ());
				//		StartCoroutine (changeTeste ());



						print (received);
						var receivedJson = JSONNode.Parse (received);
						if (receivedJson != null) {
								GetComponent<ChangeDifficulty> ().birdVelocity = float.Parse (receivedJson ["birdVelocity"]);
								GetComponent<ChangeDifficulty> ().spacing = float.Parse (receivedJson ["spacing"]);
						}
				}

		}


		void connectSocket(){
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
						//sender = new Socket (AddressFamily.InterNetwork, 
						//		SocketType.Stream, ProtocolType.Tcp);

						sender = new Socket (AddressFamily.InterNetwork, 
								SocketType.Stream, ProtocolType.Tcp);
						sender.Connect (remoteEP);


				} catch (Exception e) {
						Debug.Log (e.ToString ());
				}

		
		}
		// **********************************************
		IEnumerator StartClient() {
				//if(teste == false){
				// Data buffer for incoming data.
				if (receivedServer == false) {
						receivedServer = true;
						tcpThread = new Thread (o => {
								try {

										print ("Socket connected to" +
												sender.RemoteEndPoint.ToString ());
										byte[] msg ;
										int bytesRec ;

										if(birdDead== true){
//												FileInfo theSourceFile = new FileInfo ("log1.txt");
//												StreamReader r = theSourceFile.OpenText ();
//												msg = Encoding.ASCII.GetBytes ("Inicio do Log");
//												int bytesSent1 = sender.Send (msg);
//
//										while(!r.EndOfStream){
//												string s = r.ReadLine();
//												print (s);
//												msg = Encoding.ASCII.GetBytes (s+"\n");
//												bytesSent1 = sender.Send (msg);
//
//												}
//												msg = Encoding.ASCII.GetBytes ("Fim do Log");
//												bytesSent1 = sender.Send (msg);
										}else{

											msg = Encoding.ASCII.GetBytes ("{ countColision: " + countColision + "}");
											int bytesSent = sender.Send (msg);
											bytesRec = sender.Receive (bytes);
											received = Encoding.ASCII.GetString (bytes, 0, bytesRec);
										}
								} catch (ArgumentNullException ane) {
										Debug.Log ("ArgumentNullException :" + ane.ToString ());
								} catch (SocketException se) {
										Debug.Log ("SocketException : " + se.ToString ());
								} catch (Exception e) {
										Debug.Log ("Unexpected exception :" + e.ToString ());
								}




						});
//						 tcpThread = new Thread (o => {
//								// Connect to a remote device.
//								
//										// Connect the socket to the remote endpoint. Catch any errors.
//										try {
//								sender = new Socket (AddressFamily.InterNetwork, 
//										SocketType.Stream, ProtocolType.Tcp);
//												sender.Connect (remoteEP);
//
//												print ("Socket connected to" +
//												sender.RemoteEndPoint.ToString ());
//										byte[] msg ;
//										int bytesRec ;
////
//									//	else{
//												// Encode the data string into a byte array.
//										while(true){
//												msg = Encoding.ASCII.GetBytes ("{ birdVelocity: " + Velocidade2+ ", spacing: "+ spacing2 + "}");
//												int bytesSent = sender.Send (msg);
//												bytesRec = sender.Receive (bytes);
//
//										//}
//												// Send the data through the socket.
//
//												// Receive the response from the remote device.
//												//print ("Echoed test = " +
//												//Encoding.ASCII.GetString (bytes, 0, bytesRec));
//												received = Encoding.ASCII.GetString (bytes, 0, bytesRec);
//										}
//												//}
//												// Release the socket.
//												sender.Shutdown (SocketShutdown.Both);
//												sender.Close ();
//
//
//										} catch (ArgumentNullException ane) {
//												Debug.Log ("ArgumentNullException :" + ane.ToString ());
//										} catch (SocketException se) {
//												Debug.Log ("SocketException : " + se.ToString ());
//										} catch (Exception e) {
//												Debug.Log ("Unexpected exception :" + e.ToString ());
//										}
//
//
//
//
//						});
//
//						yield return sender;
//
						tcpThread.Start ();
						yield return new WaitForSeconds (1);
						receivedServer = false;
				}

		//}
		}



		void OnDestroy(){
				sender.Shutdown (SocketShutdown.Both);
				sender.Close ();

		}

} // end class TCP

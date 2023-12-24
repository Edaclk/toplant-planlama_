using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace toplantıplanlama_
{
    internal class Server
    {
        private static Server instance;
        private TcpListener tcpListener;
        private List<TcpClient> clients;

        private Server()
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 1234);
            clients = new List<TcpClient>();
        }

        public static Server Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Server();
                }
                return instance;
            }
        }

        public void Start()
        {
            tcpListener.Start();
            Console.WriteLine("Sunucu başlatıldı...");

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                clients.Add(client);

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                    break;

                string clientMessage = Encoding.ASCII.GetString(message, 0, bytesRead);
                Console.WriteLine("Received: " + clientMessage);

                // Gelen mesajı diğer istemcilere iletmek için
                BroadcastMessage(clientMessage, tcpClient);
            }

            clients.Remove(tcpClient);
            tcpClient.Close();
        }

        private void BroadcastMessage(string message, TcpClient sender)
        {
            foreach (var client in clients)
            {
                if (client != sender)
                {
                    NetworkStream stream = client.GetStream();
                    byte[] broadcastMessage = Encoding.ASCII.GetBytes(message);
                    stream.Write(broadcastMessage, 0, broadcastMessage.Length);
                    stream.Flush();
                }
            }
        }
    }
    
    }




using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace toplantıplanlama_
{
    internal class Client
    {
        private static Client instance;
        private TcpClient tcpClient;
        private NetworkStream clientStream;

        private Client()
        {
            tcpClient = new TcpClient("127.0.0.1", 1234);
            clientStream = tcpClient.GetStream();
        }

        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                }
                return instance;
            }
        }

        public void Start()
        {
            Thread receiveThread = new Thread(new ThreadStart(ReceiveMessages));
            receiveThread.Start();

            // İstemci başlatma örneği
            Thread clientThread = new Thread(new ThreadStart(SendSampleMessage));
            clientThread.Start();
        }

        private void ReceiveMessages()
        {
            byte[] receivedMessage = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(receivedMessage, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                    break;

                string message = Encoding.ASCII.GetString(receivedMessage, 0, bytesRead);
                Console.WriteLine("Received from server: " + message);
            }

            tcpClient.Close();
        }

        private void SendSampleMessage()
        {
            // İstemci, sunucuya bir örnek mesaj gönderir
            SendMessage("Hello from client!");
        }

        private void SendMessage(string message)
        {
            byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(message);
            clientStream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
            clientStream.Flush();
        }
    }
}


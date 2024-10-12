using System;
using System.Net.Sockets;
using System.Text;

namespace MessageClient
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите команду (SEND или RECEIVE):");
            string? command = Console.ReadLine();

            if (command == "SEND")
            {
                Console.WriteLine("Введите имя получателя:");
                string? recipient = Console.ReadLine();
                Console.WriteLine("Введите сообщение:");
                string? message = Console.ReadLine();
                SendMessageToServer($"SEND|{recipient}|{message}");
            }
            else if (command == "RECEIVE")
            {
                Console.WriteLine("Введите свое имя:");
                string? recipient = Console.ReadLine();
                SendMessageToServer($"RECEIVE|{recipient}");
            }
        }

        private static void SendMessageToServer(string message)
        {
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Ответ сервера: " + response);

            client.Close();
        }
    }
}

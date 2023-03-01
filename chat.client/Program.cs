using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace chat.client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите IP-адрес сервера:");
            string serverIp = Console.ReadLine();
            Console.WriteLine("Введите порт сервера:");
            int serverPort = int.Parse(Console.ReadLine());

            TcpClient client = new TcpClient(serverIp, serverPort);
            NetworkStream stream = client.GetStream();

            Console.WriteLine("Введите ваше имя:");
            string userName = Console.ReadLine();
            byte[] userNameBytes = Encoding.UTF8.GetBytes(userName);
            stream.Write(userNameBytes, 0, userNameBytes.Length);

            Console.WriteLine("Добро пожаловать в чат!");
            Console.WriteLine("Введите сообщения:");

            while (true)
            {
                string message = Console.ReadLine();
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                stream.Write(messageBytes, 0, messageBytes.Length);
            }
        }
    }
}

using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chat.client
{
    class Program
    {
        static async Task Main(string[] args)
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
            await stream.WriteAsync(userNameBytes, 0, userNameBytes.Length);

            Console.WriteLine("Добро пожаловать в чат!");
            Console.WriteLine("Введите сообщения:");

            // Асинхронное чтение данных из сетевого потока
            Task.Run(async () =>
            {
                while (true)
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(message);
                }
            });

            // Отправка сообщений на сервер
            while (true)
            {
                string message = Console.ReadLine();
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
            }
        }
    }
}

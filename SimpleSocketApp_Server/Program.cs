using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSocketApp_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ip адрес и порт сокета сервера
            string serverIpStr = "127.0.0.1";
            int serverPort = 1024;

            // 1. подготовим объекты endpoint-а для сокета
            IPAddress serverIp = IPAddress.Parse(serverIpStr);
            IPEndPoint serverEndpoint = new IPEndPoint(serverIp, serverPort);

            // 2. создадим сокет сервера и приввяжем к данному endpoint-у
            Socket server = new Socket(
                AddressFamily.InterNetwork, 
                SocketType.Stream, 
                ProtocolType.IP
            );
            server.Bind(serverEndpoint); // 127.0.0.1:1024

            // 3. перевести сокет в режим прослушивания входящих подключений
            server.Listen(1);

            // 4. начать ожидание входящего подключения к сокету            
            Socket client = server.Accept(); //сервер начнет ждать входящее подключение
           
            // 5. попробуем отправить сообщение клиенту
            string message = "Добрый день";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Кондитер]: {message}");

            // 6. получим сообщение от клиента
            byte[] buf = new byte[1024];
            int bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // отправить сообщение клиенту
            message = "Приму. Начинка и оформление?";
            messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Кондитер]: {message}");

            // получим сообщение от клиента            
            bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // отправить сообщение клиенту
            message = "По начинке поняла. С картинкой пока затрудняюсь. Поищу.";
            messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Кондитер]: {message}");

            // получим сообщение от клиента            
            bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // отправить сообщение клиенту
            message = "1000 руб.";
            messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Кондитер]: {message}");

            // получим сообщение от клиента            
            bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // отправить сообщение клиенту
            message = "Получила. Заказ принят.";
            messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Кондитер]: {message}");

            // получим сообщение от клиента            
            bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // 7. закрыть соединение с клиентом
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            server.Close();
            
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSockerApp_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ip адрес и порт сокета сервера
            string serverIpStr = "127.0.0.1";
            int serverPort = 1024;

            // 1. подготовить endpoint сервера
            IPAddress serverIp = IPAddress.Parse(serverIpStr);
            IPEndPoint serverEndpoint = new IPEndPoint(serverIp, serverPort);

            // 2. создать сокет клиента
            Socket client = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.IP
            );

            // 3. иницировать подключение к серверу            
            client.Connect(serverEndpoint);

            byte[] buf = new byte[1024];
            string message;
            
            // 4. получение сообщения от продавца
            int bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // 5. отправить ответ продавцу
            message = "Здравствуйте. Хочу заказать бенто торт на 22 марта. Примете заказ?";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Покупатель]: {message}");

            // получение сообщения          
            bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // отправить ответ
            message = "Ванильный банан/карамель. Мальчику на 15-летие, будущий программист.";
            messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Покупатель]: {message}");

            // получение сообщения           
            bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // отправить ответ 
            message = "Спасибо. Сколько предоплата?";
            messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Покупатель]: {message}");

            // получение сообщения           
            bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // отправить ответ 
            message = "Хорошо. Отправляю...";
            messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Покупатель]: {message}");

            // получение сообщения     
            bytesReceived = client.Receive(buf);
            message = Encoding.UTF8.GetString(buf, 0, bytesReceived);
            Console.WriteLine($"Полученное сообщение: '{message}'");

            // отправить ответ
            message = "До свидания.";
            messageBytes = Encoding.UTF8.GetBytes(message);
            client.Send(messageBytes);
            Console.WriteLine($"[Покупатель]: {message}");

            // 6. закрыть соединение с сервером
            client.Shutdown(SocketShutdown.Both);   // завершить общение в 2 стороны
            client.Close();
            
            Console.ReadLine();
        }
    }
}

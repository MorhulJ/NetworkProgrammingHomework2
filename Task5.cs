using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

Socket clientSocket = new Socket(AddressFamily.InterNetwork,
        SocketType.Stream,
        ProtocolType.Tcp);

IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);

Console.WriteLine("Connecting to server 127.0.0.1:5000...");

clientSocket.Connect(serverEndPoint);

Console.WriteLine("Connected!");

string message = "Hello, server!";
byte[] data = Encoding.UTF8.GetBytes(message);

clientSocket.Send(data);

Console.WriteLine($"Sent: {message}");

byte[] buffer = new byte[1024];
int bytesReceived = clientSocket.Receive(buffer);

string response = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

Console.WriteLine($"Received response: {response}");

clientSocket.Shutdown(SocketShutdown.Both);
clientSocket.Close();

Console.WriteLine("Connection closed");

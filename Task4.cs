using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

Socket serverSocket = new Socket(AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);

IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5000);

serverSocket.Bind(endPoint);
serverSocket.Listen(10);

Console.WriteLine("Server started on port 5000...");
Console.WriteLine("Waiting for client...");

Socket clientSocket = serverSocket.Accept();

IPEndPoint clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
Console.WriteLine($"Client connected: {clientEndPoint.Address}:{clientEndPoint.Port}");

byte[] buffer = new byte[1024];
int bytesReceived = clientSocket.Receive(buffer);

string message = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
Console.WriteLine($"Received ({bytesReceived} bytes): {message}");

string response = "Message received by server!";
byte[] responseBytes = Encoding.UTF8.GetBytes(response);

clientSocket.Send(responseBytes);

Console.WriteLine("Response sent to client");

clientSocket.Shutdown(SocketShutdown.Both);
clientSocket.Close();

serverSocket.Close();

Console.WriteLine("Connection closed");
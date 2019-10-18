using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
namespace ServerSocket
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ServerS.MessageDelegate = (string s) => { Console.WriteLine(s); };
            ServerS server = new ServerS(new IPEndPoint(IPAddress.Parse("127.0.0.1"), ServerS.port));
            server.Start();
        }
        
    }
}

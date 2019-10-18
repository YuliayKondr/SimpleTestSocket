using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace ClientSocket
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ClientS.MessageDelegate = (string str) => { Console.WriteLine(str); };

            bool check = true;
            string forMess;
            while(check)
            {
                ClientS client = new ClientS(new IPEndPoint(IPAddress.Parse("127.0.0.1"), ClientS.port));
                Console.WriteLine("Message > ");
                forMess = Console.ReadLine();
                check = client.SendClientMessag(forMess);
                if (!check)
                {
                    break;
                }
                client.RequestSever();

            }

        }
    }
}

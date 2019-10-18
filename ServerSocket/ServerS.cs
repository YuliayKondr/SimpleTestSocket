using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace ServerSocket
{
    public class ServerS
    {
        public static Action<string> MessageDelegate;
        Socket _lisServer;
        public static int port = 8005;
        private IPEndPoint _localhost;
        public ServerS(IPEndPoint iPEnd)
        {
            _localhost = iPEnd;
            _lisServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }
        public void Start()
        {
            BindServer();
        }
        private void BindServer()
        {
            try
            {
                _lisServer.Bind(_localhost);
                _lisServer.Listen(10);
                MessageDelegate?.Invoke("Start server");
                Listen();
                MessageDelegate?.Invoke("End server");
            }
            catch(Exception ex)
            {
                MessageDelegate?.Invoke(ex.ToString());
            }
        }
        private void Listen()
        {
            while(true)
            {
                try
                {
                    Socket handle = _lisServer.Accept();
                    
                    //handle.ReceiveTimeout = 100;
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handle.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handle.Available > 0);
                    MessageDelegate?.Invoke(DateTime.Now.ToShortTimeString() + " : " + builder);
                    string mess = "your message delivered > " + DateTime.Now.ToShortTimeString();
                    data = Encoding.Unicode.GetBytes(mess);
                    handle.Send(data);
                    handle.Close();
                }
                catch (SocketException ex)
                {
                    MessageDelegate?.Invoke(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageDelegate?.Invoke(ex.ToString());
                }
            }

        }
    }
}

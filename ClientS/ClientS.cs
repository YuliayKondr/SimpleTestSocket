using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ClientSocket
{
    public class ClientS
    {
        public static Action<string> MessageDelegate;
        public static int port = 8005;
        //private byte[] _bytesWorke;
        private Socket _socketClient;
        private IPEndPoint _ipendpoint;
        public ClientS(IPEndPoint iPEndPoint)
        {
            _ipendpoint = iPEndPoint;
            _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Connect();
        }
        private void Connect()
        {
            _socketClient.Connect(_ipendpoint);
            MessageDelegate?.Invoke("Connected");
        }
        public bool SendClientMessag(string str)
        {
            try
            {
                byte[] dataSendMess = Encoding.Unicode.GetBytes(str);
                _socketClient.Send(dataSendMess);
               
            }
            catch(Exception ex)
            {
                MessageDelegate?.Invoke(ex.Message);
                _socketClient.Shutdown(SocketShutdown.Both);
                _socketClient.Close();
                return false;
            }
            return true;
        }

        public bool RequestSever()
        {
            try
            {
                byte[] data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = _socketClient.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (_socketClient.Available > 0);
                MessageDelegate?.Invoke(builder.ToString());
            }
            catch(Exception ex)
            {
                MessageDelegate?.Invoke(ex.Message);
                return false;
            }
            finally
            {
                _socketClient.Shutdown(SocketShutdown.Both);
                _socketClient.Close();
            }
            return true;
        }
    }
}

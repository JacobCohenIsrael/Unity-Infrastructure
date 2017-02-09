//using System.Net.Sockets;
//using System.Net;
//using System;
//using System.Text;
//
//namespace Infrastructure.Base.Connection.Socket
//{
//    public class SocketIO
//    {
//        protected string host;
//
//        protected int port;
//
//        TcpClient client;
//
//        public SocketIO(string host, int port)
//        {
//            this.host = host;
//            this.port = port;
//        }
//
//        public bool connect()
//        {
//            if (isConnected())
//            {
//                return false;
//            }
//
//            try
//            {
//                client = new TcpClient();
//                client.Connect(host, port);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                client = null;
//                return false;
//            }
//        }
//
//        public bool close()
//        {
//            if (!isConnected())
//            {
//                return false;
//            }
//
//            try
//            {
//                client.Close();
//            }
//            catch (Exception ex)
//            {
//
//            }
//            finally
//            {
//                client = null;
//
//            }
//            return true;
//        }
//
//        public void Emit(string message, string data)
//        {
//            if (!isConnected())
//            {
//                return;
//            }
//
//            NetworkStream nwStream = client.GetStream();
//
//            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(data);
//            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
//
//            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
//            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
//            string received = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
//
//        }
//
//        protected bool isConnected()
//        {
//            if (client == null)
//            {
//                return false;
//            }
//            return true;
//        }
//
//    }
//
//
//}
//

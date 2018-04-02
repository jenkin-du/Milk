using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SocketTest.Configration;

namespace SocketTest
{

    //用于传递消息的委托
    public delegate String Handler(String message);
    class Server
    {
        //声明一个委托，用于传递消息
        public Handler handler;

        public static bool SERVER_RUN = true;

        public Server()
        {

        }

        ///// <summary>
        ///// 开启服务
        ///// </summary>
        //public void start()
        //{
        //    Thread serverThread = new Thread(receiveMessage);
        //    serverThread.Start();
        //}

        //private void receiveMessage()
        //{
        //    IPAddress serverIP = IPAddress.Parse(Config.IP);
        //    IPEndPoint address = new IPEndPoint(serverIP, Config.PORT);


        //    Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //    Console.WriteLine("开始通信！！！！");

        //    //server.Bind(address);
        //    //while (SERVER_RUN)
        //    //{
                
        //    //    //监听是否有连接传入，指定挂起的连接队列的最大值为10
        //    //    server.Listen(10);

        //    //    Socket socket = server.Accept();
        //    //    Console.WriteLine("接收到信息！！！");

        //    //    Thread task = new Thread(handleTask);
        //    //    task.Start(socket);


        //    //}
        //}

        ///// <summary>
        ///// 利用多线程处理接受的信息
        ///// </summary>
        ///// <param name="socket"></param>
        //private void handleTask(object socket)
        //{
        //    try
        //    {
        //        Socket sock = (Socket)socket;
        //        //接收数据
        //        byte[] bs = new byte[1024 * 8];
        //        string message = "";
        //        string temp = "";

        //        while (true)
        //        {
        //            sock.Receive(bs);
        //            temp = Encoding.UTF8.GetString(bs);

        //            if (temp.Contains("`"))
        //            {
        //                int endIndex = temp.LastIndexOf("`");

        //                temp = temp.Substring(0, endIndex);
                                               
        //                message += temp;
        //                Console.WriteLine("accept over");
        //                break;
        //            }

        //            message += temp;
        //        }
        //        //将消息传递出去,得到返回的信息
        //        String outString = handler(message);

        //        byte[] bytes = Encoding.Default.GetBytes(outString);
        //        sock.Send(bytes);

        //        sock.Close();
        //    }
        //    catch
        //    {
        //        Console.WriteLine("出错了！");
        //    }

        //}
    }
}

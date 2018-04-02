using SocketTest.model;
using System;
using Newtonsoft.Json;

namespace SocketTest.tool
{
    class DatagramPaser
    {
        /// <summary>
        /// 从数据包中获取请求
        /// </summary>
        /// <param name="jsonDatagram"></param>
        /// <returns></returns>
        public static String getRequest(String jsonDatagram)
        {
            Datagram datagram = JsonConvert.DeserializeObject<Datagram>(jsonDatagram);

            return datagram.Request;
        }

        /// <summary>
        /// 从数据包中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonDatagram"></param>
        /// <returns></returns>
        public static T getObject<T>(String jsonDatagram)
        {
            Datagram datagram = JsonConvert.DeserializeObject<Datagram>(jsonDatagram);
            T t = JsonConvert.DeserializeObject<T>(datagram.JsonStream);
            return t;
        }

        /// <summary>
        /// 从数据包中获取传输对象类型
        /// </summary>
        /// <param name="jsonDatagram"></param>
        /// <returns></returns>
        public static String getType(String jsonDatagram)
        {

            Datagram datagram = JsonConvert.DeserializeObject<Datagram>(jsonDatagram);
            return datagram.Type;


        }

        /// <summary>
        /// 将数据包序列化为json格式
        /// </summary>
        /// <param name="datagram"></param>
        /// <returns></returns>
        public static String toJsonDatagram(Datagram datagram)
        {
            return JsonConvert.SerializeObject(datagram);
        }

        /// <summary>
        /// 将数据包序列化为json格式
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static String toJsonDatagram(Object obj, String type, String request)
        {
            Datagram datagram = new Datagram();
            datagram.Request = request;
            datagram.Type = type;
            datagram.JsonStream = JsonConvert.SerializeObject(obj);

            return JsonConvert.SerializeObject(datagram);
        }
    }
}

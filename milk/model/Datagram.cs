using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketTest.model
{
    class Datagram
    {

        private String request;
        private String type;
        private String jsonStream;

        public Datagram()
        {

        }

        public string Request
        {
            get
            {
                return request;
            }

            set
            {
                request = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string JsonStream
        {
            get
            {
                return jsonStream;
            }

            set
            {
                jsonStream = value;
            }
        }

        public override string ToString()
        {
            return "Datagram[request="+request+",type="+type+",jsonStream="+jsonStream+"]";
        }
    }
}

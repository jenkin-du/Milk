using SocketTest.model;
using System;

namespace milk.model
{
   public class SharedStory
    {
        private String id;
        
        private String message;
        private String imageName;
        private String time;
        private String longitude;
        private String latitude;

       

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

       

        public string Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Longitude
        {
            get
            {
                return longitude;
            }

            set
            {
                longitude = value;
            }
        }

        public string Latitude
        {
            get
            {
                return latitude;
            }

            set
            {
                latitude = value;
            }
        }

        public string ImageName
        {
            get
            {
                return imageName;
            }

            set
            {
                imageName = value;
            }
        }

        public override string ToString()
        {
            return "[id="+id+",message="+message+",imageName="+ImageName+",time="+time+",longitude="+longitude+",lattitude="+latitude+"]";
        }
    }
}

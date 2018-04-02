using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace milk.model
{
    class Image
    {
        private String imageName;
        private int length;
        private String imageCode;

       

        public int Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }

       

        public string ImageCode
        {
            get
            {
                return imageCode;
            }

            set
            {
                imageCode = value;
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
            return "Image[name="+ImageName+",length="+length+"]";
        }
    }
}

using milk.dao;
using milk.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace milk
{

    delegate void MessageHandler(SharedStory story);
    delegate void ImageHandler(Bitmap image);

    public partial class MyTip : Form
    {
        private SharedStory mStory;
        private String message;
        private Bitmap bitmap;
        private String time;

        public MyTip()
        {
            InitializeComponent();
        }



        public MyTip(String id)
        {
            InitializeComponent();

            showTip(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTip_Click(object sender, EventArgs e)
        {
            if (mStory != null && bitmap != null)
            {
                StoryForm form = new StoryForm(message, bitmap, time);
                form.TopMost = true;

                Point storyPoint = new Point();
                storyPoint.X = Location.X - 15;
                storyPoint.Y = Location.Y - 15;

                form.Location = storyPoint;
                form.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private  void showTip(String id)
        {

            try
            {
                SharedStory story = DAO.query(id);

                if (story != null)
                {
                    mStory = story;
                    message = story.Message;
                    time = story.Time;

                    ML.Text = message;

                    String imageName = story.ImageName;
                    model.Image image = DAO.getImageByName(imageName);

                    if (image != null)
                    {
                        String imageCode = image.ImageCode;
                        if (imageCode != null && imageCode != "")
                        {
                            byte[] imageBuffer = Convert.FromBase64String(image.ImageCode);

                            MemoryStream ms = new MemoryStream(imageBuffer);
                            bitmap = (Bitmap)System.Drawing.Image.FromStream(ms);

                            picture.Image = bitmap;
                        }

                    }
                }
            }
            catch
            {

            }
           
        }
    }
}

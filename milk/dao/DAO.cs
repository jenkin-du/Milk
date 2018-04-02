using milk.model;
using MySql.Data.MySqlClient;
using SocketTest.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace milk.dao
{
    class DAO
    {
        /// <summary>
        /// 向数据库中添加数据
        /// </summary>
        public static void insert(SharedStory story)
        {

            String message = story.Message;
            String time = story.Time;
            String storyId = story.Id;
            String longitude = story.Longitude;
            String latitude = story.Latitude;
            String imageName = story.ImageName;

         

          


            string connString = "server=localhost;User Id=root;password=root;Database=internship;Charset=utf8";
            MySqlConnection myconn = new MySqlConnection(connString);
            myconn.Open();

            MySqlCommand insertCmd;
            insertCmd = new MySqlCommand("insert into shared_story(story_id,message,image_name,time,longitude,latitude)"
                + "values('" + storyId + "','" + message + "','" + imageName + "','" + time + "','" + longitude + "','" + latitude + "')", myconn);
            insertCmd.ExecuteNonQuery();

           


        }

        /// <summary>
        /// 根据id集合号查询结果
        /// </summary>
        /// <param name="storyIds"></param>
        /// <returns></returns>
        public static List<SharedStory> query(List<String> storyIds)
        {
            List<SharedStory> stories = new List<SharedStory>();
            Image image = null;
            SharedStory story = null;
            String sql = "";

            string connString = "server=localhost;User Id=root;password=root;Database=internship;Charset=utf8";
            MySqlConnection myconn = new MySqlConnection(connString);
            myconn.Open();

            for (int i = 0; i < storyIds.Count; i++)
            {
                sql = "select story_id,message,latitude,longitude,time,image_name from shared_story where story_id='" + storyIds[i] + "'  ";
                MySqlCommand queryCmd = new MySqlCommand(sql, myconn);

              
                MySqlDataReader reader = queryCmd.ExecuteReader();
              

                if (reader.Read())
                {

                    story = new SharedStory();

                    story.Id = reader.GetString("story_id");
                    story.Message = reader.GetString("message");
                    story.Latitude = reader.GetString("latitude");
                    story.Longitude = reader.GetString("longitude");
                    story.Time = reader.GetString("time");
                    story.ImageName = reader.GetString("image_name");

                    Console.WriteLine("queryStory="+story.ToString());

                    stories.Add(story);
                }


                reader.Close();

            }

            Console.WriteLine("queryStory=" + stories.Count);

            myconn.Close();
            return stories;
        }


        /// <summary>
        /// 根据id号查询结果
        /// </summary>
        /// <param name="storyIds"></param>
        /// <returns></returns>
        public static SharedStory query(String storyId)
        {

           
            SharedStory story = story = new SharedStory(); ;
            String sql = "";

            string connString = "server=localhost;User Id=root;password=root;Database=internship;Charset=utf8";
            MySqlConnection myconn = new MySqlConnection(connString);
            myconn.Open();


            sql = "select story_id,message,latitude,longitude,time,image_name from shared_story  where story_id='" + storyId + "' ";
            MySqlCommand queryCmd = new MySqlCommand(sql, myconn);

            Console.WriteLine(sql);
            MySqlDataReader reader = queryCmd.ExecuteReader();

            if (reader.Read())
            {

                story.Id = reader.GetString("story_id");
                story.Message = reader.GetString("message");
                story.Latitude = reader.GetString("latitude");
                story.Longitude = reader.GetString("longitude");
                story.Time = reader.GetString("time");
                story.ImageName = reader.GetString("image_name");
                

                reader.Close();

            }


            myconn.Close();
            return story;
        }

        /// <summary>
        /// 查询照片
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static Image getImageByName(String imageName)
        {
            Image image = new Image();

            string connString = "server=localhost;User Id=root;password=root;Database=internship;Charset=utf8";
            MySqlConnection myconn = new MySqlConnection(connString);
            myconn.Open();

            String sql = "select  * from image where image_name='" + imageName + "'";
            MySqlCommand queryCmd = new MySqlCommand(sql, myconn);

            MySqlDataReader reader = queryCmd.ExecuteReader();

            while (reader.Read())
            {
                image.ImageName = reader.GetString("image_name");
                image.ImageCode = reader.GetString("image_code");
            }

            myconn.Close();
            return image;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="imageCode"></param>
        public static void insertImage(String imageName,String imageCode)
        {

            string connString = "server=localhost;User Id=root;password=root;Database=internship;Charset=utf8";
            MySqlConnection myconn = new MySqlConnection(connString);
            myconn.Open();

            MySqlCommand insertCmd;

            insertCmd = new MySqlCommand("insert into image(image_name,image_code)" +
                "values('" + imageName + "','" + imageCode + "')", myconn);
            insertCmd.ExecuteNonQuery();


            myconn.Close();
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="imageCode"></param>
        public static void insertImage(model.Image image)
        {
            String imageName = image.ImageName;
            String imageCode = image.ImageCode;
            int length = image.Length;

            string connString = "server=localhost;User Id=root;password=root;Database=internship;Charset=utf8";
            MySqlConnection myconn = new MySqlConnection(connString);
            myconn.Open();

            MySqlCommand insertCmd;

            insertCmd = new MySqlCommand("insert into image(image_name,image_code,image_length)" +
                "values('" + imageName + "','" + imageCode + "',"+length+")", myconn);
            insertCmd.ExecuteNonQuery();


            myconn.Close();
        }
    }
}

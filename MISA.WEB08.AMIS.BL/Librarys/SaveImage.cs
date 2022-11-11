using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.Common.Resources;

namespace MISA.WEB08.AMIS.BL
{
    public class SaveFileImage
    {
        /// <summary>
        /// Save image to Folder's Avatar
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns>@Created by tungnt.net - 6/2015</returns>
        public static string Post(string imageBase64, string url)
        {
            string nameImage = "";
            //For demo purpose I only use jpg file and save file name by userId
            using (Image image = Base64ToImage(imageBase64.Split(",")[1]))
            {
                nameImage = Guid.NewGuid().ToString() + ".Jpeg";
                string filePath = $@"{Resource.Path_SaveImage}{url}/" + nameImage;
                image.Save(filePath);
            }
            return Resource.Path_SaveImageInDB + url + "/" + nameImage;
        }

        /// <summary>
        /// Chuyển đổi hình ảnh base64
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        private static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Bitmap tempBmp;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                using (Image image = Image.FromStream(ms, true))
                {
                    //Create another object image for dispose old image handler
                    tempBmp = new Bitmap(image.Width, image.Height);
                    Graphics g = Graphics.FromImage(tempBmp);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }
            }
            return tempBmp;
        }
    }
}

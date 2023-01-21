using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreSalesSystemSolution.Businesses.Concrete
{
    public class ImageBusiness
    {
        public static byte[] ImageToByte(string imageFile)
        {
            Image image = Image.FromFile(imageFile);
            byte[] arr;
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                arr = ms.ToArray();
                return arr;
            }
        }

        public static Image ByteToImage(byte[] buffer)
        {
            Bitmap bitmap;
            using (var ms = new MemoryStream(buffer))
            {
                bitmap = new Bitmap(ms);
                return bitmap;
            }
        }
    }
}

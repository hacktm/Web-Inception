using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Feromon.Web.Services
{
    public class ImageService
    {
        public static Stream Compress(HttpPostedFileBase strImgUrl, int width)
        {
            var imgFullSize = Image.FromStream(strImgUrl.InputStream);

            int _width = 0, _height = 0;
            float scale = 1;
            if (imgFullSize.Width > imgFullSize.Height)
            {
                if (imgFullSize.Width > width)
                {
                    _width = width;
                    _height = (int)(((float)width / imgFullSize.Width) * imgFullSize.Height);
                    scale = (float)width / imgFullSize.Width;
                }
                else
                {
                    _width = imgFullSize.Width;
                    _height = imgFullSize.Height;
                }
            }
            else
            {
                if (imgFullSize.Height > width)
                {
                    _height = width;
                    _width = (int)(((float)width / imgFullSize.Height) * imgFullSize.Width);
                    scale = (float)width / imgFullSize.Height;
                }
                else
                {
                    _width = imgFullSize.Width;
                    _height = imgFullSize.Height;
                }
            }

            var thump = new Bitmap(_width, _height);
            var graphics = Graphics.FromImage(thump);
            var transform = new Matrix();

            transform.Scale(scale, scale, MatrixOrder.Append);

            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SetClip(new Rectangle(0, 0, _width, _height));
            graphics.Transform = transform;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(imgFullSize, 0, 0, imgFullSize.Width, imgFullSize.Height);

            // Find a JPEG encoder
            ImageCodecInfo jpegEncoderInfo = null;
            var encoderInfoArray = ImageCodecInfo.GetImageEncoders();
            foreach (var imageCodecInfo in encoderInfoArray.Where(imageCodecInfo => 0 == String.Compare("image/jpeg", imageCodecInfo.MimeType, StringComparison.OrdinalIgnoreCase)))
            {
                jpegEncoderInfo = imageCodecInfo;
            }

            var myEncoderParameters = new EncoderParameters(3);
            myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            myEncoderParameters.Param[1] = new EncoderParameter(Encoder.ScanMethod, (int)EncoderValue.ScanMethodInterlaced);
            myEncoderParameters.Param[2] = new EncoderParameter(Encoder.RenderMethod, (int)EncoderValue.RenderProgressive);



            //TO-DO upload on server

            Stream stream = new MemoryStream();
            thump.Save(stream, jpegEncoderInfo, myEncoderParameters);
            stream.Position = 0;

            thump.Dispose();
            graphics.Dispose();
            return stream;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toyota.Class
{
    public enum ModePrint
    {
        Default, Nice
    }
    [Serializable]
    public class Config
    {
        public ModePrint Print1 = ModePrint.Default;
        public ModePrint Print2 = ModePrint.Default;
        public ModePrint Print3 = ModePrint.Nice;
        public string PrintManager = "TOYOTA CMD.exe";
        public double width = 1152;
        public double height = 768;
        public string folderData = @"Data/Copy";
        public string folderSave = "Save";
        public string runFile = "Folder Clone.exe";
        public string folderCache = @"Cache";
        public string userFile = @"Data/user.xml";
        public string email = "Flixvn12m@gmail.com";
        public int[]  printId = {0,1};
        public string passEmail = "Flix@2014";
        public string emailBody =   "<html>"
                                  + "<div style=\" height: 650px; width: 100%;font-family: Gotham, Helvetica, Arial, sans-serif;font-size: 14px;	color: #808285;	line-height:19px; min-width: 620px;\" >"
                                  + "<div style=\" width: 620px;	height: 650px; background:url(https://lh6.googleusercontent.com/rw8zUAAf5q0j6j4ftkOXfqFEavn5_UcxfGj6PeJ8J7bx94iMfDm6kA0hYeI9eUuIawmFn5xL4WahYlWQ3mPEYc4P0J2tzvQX4OANBxzMAvc70Lr376GfDuXyMME)\">"
                                  + "<img src=\"cid:{1}\" alt=\"Toyota Viet Nam\" />"
                                   + "<div style=\"	width: 563px;	height: 526px;	background: #edf2f5; margin-left: 28px; margin-top: 35px;\">"
                                      + "<div style=\"padding: 31px 26px; width:511px; height:464px;\"><span>Thân chào bạn,</span><br>"
                                       +"<br><span>Cảm ơn bạn đã tham gia chương trình \"Chụp hình miễn phí, nhận ảnh liền tay\" cùng Toyota Việt Nam tại Triển lãm Ô tô Việt Nam 2014.</span> <br> <br>"
                                       +"<span>Vui lòng tải tập tin đính kèm để xem hình ảnh đẹp nhất của mình tại gian trưng bày Toyota. </span> <br> <br>"
                                       +"<span>Chúng tôi hy vọng bạn đã có những khoảnh khắc trải nghiệm thú vị cùng Toyota.</span><br><br>"
                                       +"<span>Để xem thêm hình ảnh của buổi triển lãm, hãy truy cập ngay trang Facebook chính thức của Toyota Việt Nam theo địa chỉ: <a href=\"http://www.facebook.com/ToyotaVietNam\" style=\"color:#04b5ff;\">http://www.facebook.com/Toyota VietNam </a></span> <br><br>"
                                       +"<span>Xem thêm hình ảnh và thông tin sản phẩm, truy cập website chính thức của Toyota Việt Nam theo địa chỉ: <a href=\"http://www.toyota.com.vn\" style=\"color:#04b5ff;\">http://www.toyota.com.vn</a></span> <br><br>"
                                       +"<span>Xin cảm ơn.</span><br><br>"
                                       +"<strong><span>Công ty Ô tô Toyota Việt Nam</span></strong><br>"
                                       +"<span>Trụ sở chính: Phúc Thắng, Phúc Yên, Vĩnh Phúc</span><br>"
                                       +"<span>Email: tmv_cs@toyotavn.com.vn</span><br>"
                                       +"<span>Hotline: 1800 1524 | 0916 001 524</span></div>"
                                       +"</div>"
                                  +"</div>"
                                  +"</div>"
                                  +"</html>";
        public string facebookPostMsg = "{0} vừa tham gia chụp hình và in ảnh miễn phí tại gian hàng của Toyota trong Triển lãm ô tô Việt Nam năm 2014.";
        public string subject = " [Toyota Việt Nam] Hình ảnh của bạn tại Triển lãm Ô tô Việt Nam 2014";
        public float timecheck = 60f;
        public string from = @"Data/Original";
        public string to = @"Data/Copy";
        public string dataName = "Data/data.xml";
        public int block = 100;
        public int time = 1;
        public bool isChange = false;
        public int DecodePixelWidth=800;
        public string fanPageID = "ToyotaVietnam";
       
        public static Config Read(string file)
        {
            if (!File.Exists(file))
            {
                Write(file, new Config());
                return new Config();
            }
            else
            {
                FileInfo inf = new FileInfo(file);
                while (inf.IsFileLocked()) { Console.WriteLine("Wait"); };
                try
                {
                    using (Stream s = File.Open(file, FileMode.Open))
                    {
                        System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Config));
                        return (Config)reader.Deserialize(s);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().ToString());
                    return new Config();
                }

            }
        }

        public static void Write(string file, Config overview)
        {
            if (string.IsNullOrEmpty(file))
                throw new Exception("File Not Empty");
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(Config));
            FileInfo inf = new FileInfo(file);
            while (inf.IsFileLocked() && inf.Exists) { Console.WriteLine("Wait"); }
            using (Stream s = File.OpenWrite(file))
            {
                writer.Serialize(s, overview);
            }
        }
    }
}

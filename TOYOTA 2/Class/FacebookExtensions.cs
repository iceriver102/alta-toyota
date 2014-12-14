using Facebook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toyota.Class
{
   
    public static class  FacebookExtensions
    {
        public static string PostData(this Facebook.FacebookClient FBClient,string msg, string fileName, string ContentType = "image/jpg")
        {
            FacebookMediaObject facebookUploader = new FacebookMediaObject { FileName = fileName, ContentType = ContentType };
            var bytes = File.ReadAllBytes(fileName);
            facebookUploader.SetValue(bytes);
            var postInfo = new Dictionary<string, object>();
            postInfo.Add("message", msg);
            postInfo.Add("image", facebookUploader);
            try
            {
                var tmp = FBClient.Post("/me/photos", postInfo);

                if (tmp != null)
                {
                    string jsonStr = tmp.ToString();
                    var json = SimpleJSON.JSON.Parse(jsonStr);
                    return json["id"].Value;
                }
               
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toyota.Class
{
    public enum TypeUser
    {
        Mail,Facebook
    }
    [Serializable]
    public class User
    {
        public DateTime date;
        public string id = string.Empty;
        public string Name = string.Empty;
        public string Facebook = string.Empty;
        public string Pass = string.Empty;
        public string Email = string.Empty;
        public string ImageOriginal = string.Empty;
        public string ImageCover = string.Empty;
        public string ImagePost = string.Empty;
        public string idImageCover = string.Empty;
        public string idImagePost;
        public bool isPrint = false;
        public string AcessToken = string.Empty;
        public string idFacebook = string.Empty;
        public string nameFacebook = string.Empty;
        public string linkFacebook = string.Empty;
        public string Cache = string.Empty;
        public string lastUrl = string.Empty;
        public bool isSendEmail=false;
        public TypeUser @Type;

        public User()
        {
            Random rand = new Random();
            this.id = string.Format("alta_{0}", DateTime.Now.Ticks);
            this.Cache = this.id;
            this.date = DateTime.Now;
            this.Type = TypeUser.Facebook;
        }

        public User(string p)
        {
            Random rand = new Random();
            this.id = string.Format("alta_{0}_{1}", p, DateTime.Now.Ticks);
            this.Cache = this.id;
            this.date = DateTime.Now;
            this.Type = TypeUser.Facebook;
        }

        public CsvRow toStringCSV()
        {
            CsvRow row = new CsvRow();
            row.Add(Name);
            row.Add(Email);
            row.Add(nameFacebook);
            row.Add(linkFacebook);
            row.Add(ImageOriginal);
            row.Add(ImagePost);
            row.Add(ImageCover);
            row.Add(this.date.ToShortDateString() +" "+ this.date.ToShortTimeString());
            return row;

        }

    }
}

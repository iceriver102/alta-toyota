using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOYOTA_2;

namespace toyota.Class
{
    [Serializable]
    public class UserCache
    {
        public List<User> ListUser;
        public UserCache()
        {
            ListUser = new List<User>();
        }

        public void Add(User u)
        {
            this.ListUser.Add(u);
            Write(App.config.userFile,this);
        }

        
       

        public static UserCache Read(string file)
        {

            if (!File.Exists(file))
            {
                Write(file, new UserCache());
                return new UserCache();
            }
            else
            {
                using (Stream s = File.Open(file, FileMode.Open))
                {
                    System.Xml.Serialization.XmlSerializer reader =
         new System.Xml.Serialization.XmlSerializer(typeof(UserCache));
                    return (UserCache)reader.Deserialize(s);
                }
            }
        }
        public static void Write(string file, UserCache u)
        {
            if (string.IsNullOrEmpty(file))
                throw new Exception("File Not Empty");
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(UserCache));
            using (StreamWriter s = new StreamWriter(file))
            {
                writer.Serialize(s, u);
            }
        }
    }
}

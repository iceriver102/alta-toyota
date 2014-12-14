using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toyota.Class
{
    [Serializable]
    public class CacheUserPin
    {
        public User user1;
        public User user2;
        public User user3;

        public static CacheUserPin Read(string file)
        {
            if (!File.Exists(file))
            {
                Write(file, new CacheUserPin());
                return new CacheUserPin();
            }
            else
            {
                FileInfo inf = new FileInfo(file);
                while (inf.IsFileLocked()) { Console.WriteLine("Wait"); };
                try
                {
                    using (Stream s = File.Open(file, FileMode.Open))
                    {
                        System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(CacheUserPin));
                        return (CacheUserPin)reader.Deserialize(s);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().ToString());
                    return new CacheUserPin();
                }

            }
        }

        public static void Write(string file, CacheUserPin overview)
        {
            if (string.IsNullOrEmpty(file))
                throw new Exception("File Not Empty");
            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(CacheUserPin));
            FileInfo inf = new FileInfo(file);
            while (inf.IsFileLocked() && inf.Exists) { Console.WriteLine("Wait"); }
            using (Stream s = File.OpenWrite(file))
            {
                writer.Serialize(s, overview);
            }
        }
        
    }
}

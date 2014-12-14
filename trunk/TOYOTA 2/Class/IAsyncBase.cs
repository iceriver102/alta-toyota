using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altamedia_MTC_CMD.Class
{
    public interface IAsyncBase
    {
        void StartServer();
        void StartClient(string msg);
    }
}

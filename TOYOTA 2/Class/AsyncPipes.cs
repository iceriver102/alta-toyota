using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Altamedia_MTC_CMD.Class
{
    public class StreamString
    {
        private Stream ioStream;
        private Encoding streamEncoding;
        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            //this.ioStream.ReadTimeout = 120;
            streamEncoding = Encoding.UTF8;
        }
        public string ReadString()
        {
            int len = 0;
            len = ioStream.ReadByte() * 256;
            if (len > 0 || true)
            {
                // len += ioStream.ReadByte();
                byte[] inBuffer = new byte[len];
                ioStream.Read(inBuffer, 0, inBuffer.Length);
                return streamEncoding.GetString(inBuffer);
            }
            else
            {
                return null;
            }
        }
        public int WriteString(string outString)
        {
            try
            {
                byte[] outBuffer = streamEncoding.GetBytes(outString);
                int len = outBuffer.Length;
                if (len > UInt16.MaxValue)
                {
                    len = (int)UInt16.MaxValue;
                }
                // ioStream.WriteByte((byte)(len / 256));
                ioStream.WriteByte((byte)(len & 255));
                ioStream.Write(outBuffer, 0, len);
                ioStream.Flush();
                return outBuffer.Length + 2;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
    public class ReactivePipes : IAsyncBase
    {
        private string _pipeName;

        public ReactivePipes()
        {
            this._pipeName = "altamedia_mtc";
        }

        public ReactivePipes(string name)
        {
            this._pipeName = "altamedia_mtc_" + name;
        }

        public void StartServer()
        {

        }
        public void StartClient(string msg)
        {
            NamedPipeClientStream clientPipe = new NamedPipeClientStream(".", _pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

            //var read = Observable.FromAsyncPattern<byte[], int, int, int>(clientPipe.BeginRead, clientPipe.EndRead);
            //var write = Observable.FromAsyncPattern<byte[], int, int>(clientPipe.BeginWrite, clientPipe.EndWrite);

            try
            {
                clientPipe.Connect(100);
                //   clientPipe.ReadTimeout=120;
                StreamString ss = new StreamString(clientPipe);
                ss.WriteString(msg);
                
            }
            catch (TimeoutException oEX)
            {
                Console.WriteLine(oEX.GetBaseException().ToString());

            }
        }
    }
}

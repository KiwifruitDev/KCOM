using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://stackoverflow.com/a/2784979

namespace KiwisCoOpMod
{
    public delegate void MessageAvailableEventHandler(object sender,
    MessageAvailableEventArgs e);

    public class MessageAvailableEventArgs : EventArgs
    {
        public MessageAvailableEventArgs(string messageType, byte[] data) : base()
        {
            MessageType = messageType;
            Data = data;
        }

        public string MessageType { get; private set; }
        public byte[] Data { get; private set;}
    }
    public class StreamWatcher
    {
        private readonly Stream stream;

        private bool working = false;

        public StreamWatcher(Stream stream)
        {
            this.stream = stream;
        }

        public void SetWorking(bool state)
        {
            bool oldWorking = working;
            working = state;
            if(oldWorking != state && state)
                Read();
        }

        protected void OnMessageAvailable(MessageAvailableEventArgs e)
        {
            var handler = MessageAvailable;
            if (handler != null)
                handler(this, e);
        }

        protected async void Read()
        {
            while (working)
            {
                if (stream != null)
                {
                    byte[] headerBuffer = new byte[10];
                    await stream.ReadAsync(headerBuffer, 0, headerBuffer.Length);
                    byte[] type = new byte[4] {
                        headerBuffer[0], headerBuffer[1], headerBuffer[2], headerBuffer[3]
                    };
                    string commandType = Encoding.ASCII.GetString(type);
                    byte length = (byte)(headerBuffer[9] - headerBuffer.Length);
                    byte[] data = new byte[length];
                    await stream.ReadAsync(data, 0, length);
                    OnMessageAvailable(new MessageAvailableEventArgs(commandType, data));
                }
            }
        }

        public event MessageAvailableEventHandler? MessageAvailable;
    }
}

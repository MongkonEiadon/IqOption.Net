using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iqoptionapi.extensions;

namespace iqoptionapi {
    public static class WebSocketMixins {

        public static Task SendString(this ClientWebSocket ws, object data, CancellationToken cancellation) {
            return ws.SendString(data.AsJson(), cancellation);
        }
        public static Task SendString(this ClientWebSocket ws, String data, CancellationToken cancellation)
        {
            var encoded = Encoding.UTF8.GetBytes(data);
            var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);
            return ws.SendAsync(buffer, WebSocketMessageType.Text, true, cancellation);
        }

        public static async Task<String> ReadString(this ClientWebSocket ws)
        {
            ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[8192]);

            WebSocketReceiveResult result = null;

            using (var ms = new MemoryStream())
            {
                do
                {
                    result = await ws.ReceiveAsync(buffer, CancellationToken.None);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(ms, Encoding.UTF8))
                    return reader.ReadToEnd();
            }
        }
    }
}
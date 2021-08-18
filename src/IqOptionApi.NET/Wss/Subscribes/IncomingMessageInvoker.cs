using System;
using System.Collections.Generic;
using System.Text;

namespace IqOptionApi.Wss
{
    public class IncomingMessageInvoker
    {
        public Type Type { get; set; }
        public Action<object> Action { get; set; }
    }
}

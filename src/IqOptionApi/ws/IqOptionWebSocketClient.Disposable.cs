using System.Linq;
using System.Reflection;

using IqOptionApi.utilities;

namespace IqOptionApi.ws {

    public partial class IqOptionWebSocketClient {

        
        private static MethodInvoker<PredisposableAttribute>[] _disposableMethodInfos =
            typeof(IqOptionWebSocketClient).GetMethods()
                .Where(x => x.GetCustomAttribute(typeof(PredisposableAttribute)) != null)
                .Select(x => new MethodInvoker<PredisposableAttribute>(
                    x.GetCustomAttribute<PredisposableAttribute>(), x))
                .ToArray();



        public void Dispose() {
            
            //signal to complete the observer
            foreach (var disposableMethodInfo in _disposableMethodInfos) {
                disposableMethodInfo.TargetMethod.Invoke(this, null);
            }

            _client?.CloseAsync();
        }
    }

}
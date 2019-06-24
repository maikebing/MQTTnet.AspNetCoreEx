using MQTTnet.AspNetCoreEx;
using System;
using System.Threading.Tasks;

namespace MQTTnet.Server
{
    public class MqttServerClientConnectionValidatorHandlerDelegate : IMqttServerClientConnectionValidatorHandler
    {
        private readonly Func<MqttServerClientConnectionValidatorEventArgs, Task> _handler;

        public MqttServerClientConnectionValidatorHandlerDelegate(Action<MqttServerClientConnectionValidatorEventArgs> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            _handler = delegate (MqttServerClientConnectionValidatorEventArgs context)
            {
                handler(context);
                return Task.FromResult(0);
            };
        }

        public MqttServerClientConnectionValidatorHandlerDelegate(Func<MqttServerClientConnectionValidatorEventArgs, Task> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            _handler = handler;
        }

        public Task HandleClientConnectionValidatorAsync(MqttServerClientConnectionValidatorEventArgs eventArgs)
        {
            return _handler(eventArgs);
        }
    }
}
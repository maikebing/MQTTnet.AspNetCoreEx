using MQTTnet.AspNetCoreEx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQTTnet.Server
{
    public class MqttServerClientConnectionValidatorHandlerDelegate : IMqttServerClientConnectionValidatorHandler
    {
        private readonly Func<MqttServerClientConnectionValidatorEventArgs, Task> _handler;
        private readonly MqttServerConnectionValidator _connectionValidator;
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

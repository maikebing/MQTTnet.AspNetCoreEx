using MQTTnet.Server;

namespace MQTTnet.AspNetCoreEx
{
    public class MqttServerClientConnectionValidatorEventArgs
    {
        public MqttConnectionValidatorContext Context { get; set; }
    }
}
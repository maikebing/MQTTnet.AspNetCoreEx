using MQTTnet.Server;

namespace MQTTnet.AspNetCoreEx
{
    public class MqttClientConnectionValidatorEventArgs
    {
        public MqttConnectionValidatorContext Context { get; set; }
    }
}
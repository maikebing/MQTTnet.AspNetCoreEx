# MQTTnet.AspNetCoreEx


[![Build status](https://ci.appveyor.com/api/projects/status/5hjxcfsvhhm3cebw?svg=true)](https://ci.appveyor.com/project/MaiKeBing/mqttnet-aspnetcoreex)

` dotnet add package MQTTnet.AspNetCoreEx --version 3.0.9

```c#
services.AddHostedMqttServerEx();


   app.UseMqttServerEx(server =>
            {
                server.ClientConnectedHandler = new MqttServerClientConnectedHandlerDelegate(args => mqttEventsHandler.Server_ClientConnected(server, args));
            //ClientConnectionValidatorHandler 
                server.ClientConnectionValidatorHandler = new MqttServerClientConnectionValidatorHandlerDelegate(args => mqttEventsHandler.Server_ClientConnectionValidator(server, args));
            });

`
```


# MQTTnet.AspNetCoreEx

```c#
services.AddHostedMqttServerEx();


app.UseMqttServerEx(server =>
    {
        server.ClientConnectionValidator += mqttEventsHandler.Server_ClientConnectionValidator;
    });

`
```


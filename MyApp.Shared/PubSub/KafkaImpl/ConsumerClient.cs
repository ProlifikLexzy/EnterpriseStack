using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;

namespace MyApp.Shared.PubSub.KafkaImpl
{
    public class ConsumerClient<T> : IConsumerClient<T> where T: new()
    {
        private readonly ConsumerConfig consumerConfig;
        private Action<string> messageReceived;
        private readonly IConsumer<Ignore, T> consumer;

        private Action<string> GetMessageReceived()
        {
            return messageReceived;
        }

        private void SetMessageReceived(Action<string> value)
        {
            messageReceived = value;
        }

        public event Action<string> OnMessageRecieved
        {
            add
            {
                SetMessageReceived(GetMessageReceived() + value);
            }
            remove
            {
                SetMessageReceived(GetMessageReceived() - value);
            }
        }

        public ConsumerClient(IHostingEnvironment env, IConfiguration globalconf)
        {
            if (env.IsDevelopment())
            {
                consumerConfig = new ConsumerConfig
            {
                GroupId = globalconf.GetValue<string>("Kafka:GroupId"),
                BootstrapServers = globalconf.GetValue<string>("Kafka:BootstrapServers"),
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = globalconf.GetValue<string>("Kafka:Password"),
                SslCaLocation = $@"{ApplicationEnvironment.ApplicationBasePath}{globalconf.GetValue<string>("Kafka:Cert")}",
                Debug = "security,broker,protocol",
                ApiVersionRequestTimeoutMs = 60000,
               // BrokerVersionFallback = "1.0.0",
                AutoOffsetReset = AutoOffsetReset.Latest,
            };
            }
            else
            {   consumerConfig = new ConsumerConfig
                {
                    GroupId = globalconf.GetValue<string>("Kafka:GroupId"),
                    BootstrapServers = globalconf.GetValue<string>("Kafka:BootstrapServers"),
                    ApiVersionRequestTimeoutMs = 60000,
                    BrokerVersionFallback = "0.11.0",
                };
            }

            var consumerBuilder = new ConsumerBuilder<Ignore, T>(consumerConfig);
            consumerBuilder.SetValueDeserializer(new KafkaByteDeserializer<T>());
            consumer = consumerBuilder.Build();
        }

        public void Subscribe(List<string> topics) => consumer.Subscribe(topics);

        public ConsumeResult<Ignore, T> Poll(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return consumer.Consume(cancellationToken); 
            }
            catch(ConsumeException)
            {
            }

            return null;
           
        } 
    }
}

using System;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.DotNet.PlatformAbstractions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace MyApp.Shared.PubSub.KafkaImpl
{
    public class ProducerClient<T>: IProducerClient<T> where T: new()
    {
        private readonly ProducerConfig producerConfig;
        private readonly IProducer<Null, T> producer;

        public ProducerClient(IHostingEnvironment env, IConfiguration globalconf)
        {
            string sslCaLocation = $@"{ApplicationEnvironment.ApplicationBasePath}{globalconf.GetValue<string>("Kafka:Cert")}";
            if (env.IsDevelopment())
            {
                producerConfig = new ProducerConfig
                {
                    BootstrapServers = globalconf.GetValue<string>("Kafka:BootstrapServers"),
                    SecurityProtocol = SecurityProtocol.SaslSsl,
                    SaslMechanism = SaslMechanism.Plain,
                    SaslUsername = globalconf.GetValue<string>("Kafka:Username"),
                    SaslPassword = globalconf.GetValue<string>("Kafka:Password"),
                    SslCaLocation = sslCaLocation,
                    ApiVersionFallbackMs = 0,
                    Debug = "security,broker,protocol"
                };
            }
            else
            {
                producerConfig = new ProducerConfig
                {
                    BootstrapServers = globalconf.GetValue<string>("Kafka:BootstrapServers"),
                    SecurityProtocol = SecurityProtocol.SaslSsl,
                    SaslMechanism = SaslMechanism.Plain,
                    SaslUsername = globalconf.GetValue<string>("Kafka:Username"),
                    SaslPassword = globalconf.GetValue<string>("Kafka:Password"),
                    SslCaLocation = sslCaLocation,
                    ApiVersionFallbackMs = 0,
                };
            }

            var producerBuilder = new ProducerBuilder<Null, T>(producerConfig);
            producerBuilder.SetValueSerializer(new KafkaByteSerializer<T>());
            producer = producerBuilder.Build();
        }

        public async Task<DeliveryResult<Null, T>> Produce(string topic, T message)
        {
            var msg = new Message<Null, T>();
            msg.Value = message;
            return await producer.ProduceAsync(topic, msg);
        }
    }
}

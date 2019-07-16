using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using MyApp.Shared.Extensions;

namespace MyApp.Shared.PubSub.KafkaImpl
{
    public class KafkaByteDeserializer<T> : IDeserializer<T> where T: new()
    {

        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var d =  data.ToArray().Deserialize<T>();
            return d;
        }
           
    }
}

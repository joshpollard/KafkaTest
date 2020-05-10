using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KTProducer
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:29092" };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var topicCount = 100;

                    while (true)
                    {
                        for (int i = 0; i < topicCount; i++)
                        {
                            var dr = await p.ProduceAsync($"test-topic-{i}", new Message<Null, string> { Value = "test" });
                            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");

                            await Task.Delay(250);
                        }
                    }
                    
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}

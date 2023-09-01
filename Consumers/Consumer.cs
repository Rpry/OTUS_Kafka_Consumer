using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Consumer.Settings;
using Microsoft.Extensions.Configuration;

namespace Consumer.Consumers
{
    public class Consumer
    {
        private ConsumerConfig _consumerConfig;
        private IConsumer<string, string> _kafkaConsumer;
        
        public Consumer(IConfiguration configuration, string groupId, string topicName)
        {
            _consumerConfig = GetConsumerConfig(groupId, configuration);
            _kafkaConsumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            var topics = new List<string>() { topicName };
            _kafkaConsumer.Subscribe(topics);
        }

        public void Consume()
        {
            var consumeResult = _kafkaConsumer.Consume(TimeSpan.FromSeconds(10));
            if (consumeResult != null)
            {
                Console.WriteLine($"RECEIVED: {consumeResult.Message.Value} (Partition: {consumeResult.Partition.Value} Offset: {consumeResult.Offset.Value})");    
            }
            else
            {
                Console.WriteLine("no message...");
            }
        }
        
        private static ConsumerConfig GetConsumerConfig(string groupId, IConfiguration configuration)
        {
            var kafkaSettings = configuration.Get<ApplicationSettings>().KafkaSettings;
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            return config;
        }
    }
}
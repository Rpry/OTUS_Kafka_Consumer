using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Consumer.Settings;
using Microsoft.Extensions.Configuration;

namespace Consumer.Consumers
{
    public class Consumer
    {
        public ConsumerConfig _consumerConfig;
        
        public Consumer(IConfiguration configuration, string groupId)
        {
            _consumerConfig = GetRabbitConnection(groupId, configuration);
        }

        public void Consume(string topicName)
        {
            var topics = new List<string>() { topicName };
            using (var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build())
            {
                consumer.Subscribe(topics);
                var consumeResult = consumer.Consume(TimeSpan.FromSeconds(10));
                if (consumeResult != null)
                {
                    Console.WriteLine($"INCOMING MESSAGE: {consumeResult.Message.Value}");    
                }
                else
                {
                    Console.WriteLine("no message...");
                }
            }
        }
        
        private static ConsumerConfig GetRabbitConnection(string groupId, IConfiguration configuration)
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
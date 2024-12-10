using System;

namespace Consumer.Models;

public class OrderCreatedMessage : IKafkaMessage
{
    public long Id { get; set; }
    public OrderState State { get; set; }
    public DateTimeOffset ChangedAt { get; set; }
}
namespace Consumer.Options
{
    public sealed class ApplicationOptions
    {
        public KafkaOptions KafkaOptions { get; set; }
 
        public string GroupId { get; set; }
    }
}
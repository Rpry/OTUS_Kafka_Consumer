namespace Consumer.Options
{
    public class ApplicationOptions
    {
        public KafkaOptions KafkaOptions { get; set; }
        
        public string GroupId { get; set; }
    }
}
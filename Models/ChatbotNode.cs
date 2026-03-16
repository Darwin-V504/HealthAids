namespace HealthAids.Models
{
    public class ChatbotNode : IComparable<ChatbotNode>
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ChatbotOption> Options { get; set; } = new();
        public bool IsEndNode { get; set; }
        public string? Action { get; set; } 

        public int CompareTo(ChatbotNode? other)
        {
            if (other == null) return 1;
            return Id.CompareTo(other.Id);
        }
    }
public class ChatbotOption
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int NextNodeId { get; set; }
        public string? Action { get; set; } 
    }
}
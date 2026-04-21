namespace Email_Spam_Project.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Prediction { get; set; }
        public float Confidence { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

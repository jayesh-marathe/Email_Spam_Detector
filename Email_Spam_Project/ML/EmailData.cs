using Microsoft.ML.Data;

namespace Email_Spam_Project.Models
{
    public class EmailData
    {
        [LoadColumn(1)]
        public string Message { get; set; } = string.Empty;

        [LoadColumn(0)]
        public bool Label { get; set; }
    }
}

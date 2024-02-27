using Azure.AI.TextAnalytics;

namespace SmartHelper.Dtos
{
    public class RecognizedEntityDto
    {
        public string? Text { get; set; }
        public EntityCategory Category { get; set; }
    }
}

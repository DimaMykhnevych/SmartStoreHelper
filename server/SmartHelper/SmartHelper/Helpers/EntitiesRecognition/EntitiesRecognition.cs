using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Options;
using SmartHelper.Dtos;
using SmartHelper.Options;

namespace SmartHelper.Helpers.EntitiesRecognition
{
    public class EntitiesRecognition : IEntitiesRecognition
    {
        private readonly TextAnalyticsApiClientOptions _textAnalyticsApiClientOptions;
        private readonly ILogger _logger;

        public EntitiesRecognition(ILogger<EntitiesRecognition> logger, IOptions<TextAnalyticsApiClientOptions> textAnalyticsApiClientOptions)
        {
            _textAnalyticsApiClientOptions = textAnalyticsApiClientOptions.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<RecognizedEntityDto>> RecognizedEntitiesAsync(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new List<RecognizedEntityDto>();
            }

            AzureKeyCredential credentials = new(_textAnalyticsApiClientOptions.ApiKey);
            Uri endpoint = new(_textAnalyticsApiClientOptions.Endpoint);

            var client = new TextAnalyticsClient(endpoint, credentials);

            var response = await client.RecognizeEntitiesAsync(text);
            var result = new List<RecognizedEntityDto>();

            _logger.LogInformation("Named Entities:");
            foreach (var entity in response.Value)
            {
                result.Add(new()
                {
                    Text = entity.Text,
                    Category = entity.Category
                });;
                _logger.LogInformation($"\tText: {entity.Text},\tCategory: {entity.Category},\tSub-Category: {entity.SubCategory}\n" +
                    $"\t\tScore: {entity.ConfidenceScore:F2},\tLength: {entity.Length},\tOffset: {entity.Offset}\n");
            }

            return result;
        }
    }
}

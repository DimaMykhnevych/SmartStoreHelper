using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Options;
using SmartHelper.Options;

namespace SmartHelper.Helpers.SpeechRecognition
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly SpeechRecognitionApiOptions _speechRecognitionApiOptions;
        private readonly ILogger _logger;

        public SpeechRecognition(ILogger<SpeechRecognition> logger, IOptions<SpeechRecognitionApiOptions> speechRecognitionApiOptions)
        {
            _speechRecognitionApiOptions = speechRecognitionApiOptions.Value;
            _logger = logger;
        }

        public async Task<string?> RecognizedSpeechAsync(string filePath)
        {
            var speechConfig = SpeechConfig.FromSubscription(_speechRecognitionApiOptions.ApiKey, _speechRecognitionApiOptions.Region);
            speechConfig.SpeechRecognitionLanguage = "en-US";

            using var audioConfig = AudioConfig.FromWavFileInput(filePath);
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
            return ProcessSpeechRecognitionResult(speechRecognitionResult);
        }

        private string? ProcessSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
        {
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    _logger.LogInformation("Text recognized successfully");
                    return speechRecognitionResult.Text;
                case ResultReason.NoMatch:
                    _logger.LogWarning("NOMATCH: Speech could not be recognized.");
                    return null;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                    _logger.LogWarning($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        _logger.LogWarning($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        _logger.LogWarning($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        _logger.LogWarning($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    return null;
            }

            return null;
        }
    }
}

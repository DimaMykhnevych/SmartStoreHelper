namespace SmartHelper.Helpers.SpeechRecognition
{
    public interface ISpeechRecognition
    {
        public Task<string?> RecognizedSpeechAsync(string filePath);
    }
}

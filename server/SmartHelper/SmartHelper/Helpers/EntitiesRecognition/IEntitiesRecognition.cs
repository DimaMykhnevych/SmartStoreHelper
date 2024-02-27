using SmartHelper.Dtos;

namespace SmartHelper.Helpers.EntitiesRecognition
{
    public interface IEntitiesRecognition
    {
        public Task<IEnumerable<RecognizedEntityDto>> RecognizedEntitiesAsync(string? text);
    }
}

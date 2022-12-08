using CloudinaryDotNet.Actions;

namespace RunGroopTutorial.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicUrl);

    }
}

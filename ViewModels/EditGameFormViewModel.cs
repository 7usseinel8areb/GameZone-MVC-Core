
namespace GameZone.ViewModels
{
    public class EditGameFormViewModel:GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentImage { get; set; }

        [AlloewdExtentions(FileSettings.AllowedExtentions)]
        /*[MaxFileSize(FileSettings.MaxImageSizeInBytes)]*/
        public IFormFile? Image { get; set; } = default!;
    }
}

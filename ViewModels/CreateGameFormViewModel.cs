namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel:GameFormViewModel
    {
        [AlloewdExtentions(FileSettings.AllowedExtentions)]
        /*[MaxFileSize(FileSettings.MaxImageSizeInBytes)]*/
        public IFormFile Image { get; set; } = default!;

    }
}

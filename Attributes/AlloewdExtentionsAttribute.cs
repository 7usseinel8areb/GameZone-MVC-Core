namespace GameZone.Attributes
{
    public class AlloewdExtentionsAttribute:ValidationAttribute
    {
        private readonly string _allowedExtentions;

        public AlloewdExtentionsAttribute(string allowedExtentions)
        {
            _allowedExtentions = allowedExtentions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if(file is not null)
            {
                var extentions = Path.GetExtension(file.FileName);

                var isAllowed = _allowedExtentions.Split(',')
                    .Contains(extentions, StringComparer.OrdinalIgnoreCase);

                if (!isAllowed)
                {
                    return new ValidationResult(ErrorMessage = $"Only {_allowedExtentions} are allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}

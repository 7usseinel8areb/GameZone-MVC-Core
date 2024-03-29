namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFilesize;

        public MaxFileSizeAttribute(int allowedSize)
        {
            _maxFilesize = allowedSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                bool isAllowed = file.Length > _maxFilesize;
                if (!isAllowed)
                {
                    return new ValidationResult(ErrorMessage = $"Maximum allowed size is {_maxFilesize} bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}

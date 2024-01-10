using System.ComponentModel.DataAnnotations;

namespace stationeryManagement.Data.Annotation;

public class CompressedImageAttribute : ValidationAttribute
{
    public new string ErrorMessage { get; set; } = "Only jpeg and png images are allowed.";
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        // List of compressed image types
        var imageTypes = new List<string> { "image/jpeg", "image/png","image/jpg" };

        if (file != null && !imageTypes.Contains(file.ContentType))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
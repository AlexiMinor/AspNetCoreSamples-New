using System.ComponentModel.DataAnnotations;

namespace FirstMvcApp.Models
{
    public class UserLoginModel : IValidatableObject
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errorsList = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Email))
            {
                errorsList.Add(new ValidationResult("Empty Email", 
                    new List<string>(){"Email"}));
            }

            return errorsList;
        }
    }
}

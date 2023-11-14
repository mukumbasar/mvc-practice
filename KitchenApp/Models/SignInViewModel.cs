using System.ComponentModel.DataAnnotations;

namespace KitchenApp.Models
{
    public class SignInViewModel
    {
        [EmailAddress(ErrorMessage = "Email girmelisiniz.")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
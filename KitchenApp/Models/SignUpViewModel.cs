using System.ComponentModel.DataAnnotations;

namespace KitchenApp.Models
{
    
    public class SignUpViewModel
    {
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        public string Surname { get; set; }
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}


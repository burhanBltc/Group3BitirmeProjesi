using System.ComponentModel.DataAnnotations;

namespace Group3BitirmeProjesi.Models.AccountVMs
{
    public class RegisterVM
    {

        [Required(ErrorMessage = "İsim girilmeli")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        //[StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }




        // İstenirse kullanıcıya özel başka özellikler eklenebilir.


    }
}

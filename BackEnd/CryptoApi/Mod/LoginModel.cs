using System.ComponentModel.DataAnnotations;

namespace CryptoAPI
{

    public class LoginModel
     {
         [Required]
         public string username { get; set; }
         [Required]
         public string password { get; set; }
     }
}
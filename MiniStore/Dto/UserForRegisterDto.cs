using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Dto
{
    public class UserForRegisterDto
    {
        [Required]
        public string  UserName { get; set; }
        
        [Required]
        [StringLength(26,MinimumLength =4,ErrorMessage ="Veillez saisir un mot de passe qui contient minimum 4 chiffres!")]
        public string Password { get; init; }
        
        [EmailAddress]
        [Required]
        public string Email { get; init; }
        
    }
}

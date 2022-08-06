using System.ComponentModel.DataAnnotations;

namespace HomeworkApi.Base
{
    public class UpdatePasswordRequest
    {
        [Required]
        [PasswordAttribute]
        public string OldPassword { get; set; }

        [Required]
        [PasswordAttribute]
        public string NewPassword { get; set; }
    }
}

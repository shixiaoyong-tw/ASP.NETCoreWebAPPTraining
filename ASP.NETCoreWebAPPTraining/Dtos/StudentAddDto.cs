using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreWebAPPTraining.Dtos
{
    public class StudentAddDto
    {
        [Required]
        [MaxLength(16)]
        public string Name { get; set; }

        [Required]
        public string StudentNo { get; set; }

        [Range(6,18)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "wrong phone number")]
        public string PhoneNumber { get; set; }
    }
}
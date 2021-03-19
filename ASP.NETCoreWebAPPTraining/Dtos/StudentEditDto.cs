using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreWebAPPTraining.Dtos
{
    public class StudentEditDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required] 
        [MaxLength(16)] 
        public string Name { get; set; }

        [Required] 
        public string StudentNo { get; set; }

        [Range(6, 18)] 
        public int Age { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
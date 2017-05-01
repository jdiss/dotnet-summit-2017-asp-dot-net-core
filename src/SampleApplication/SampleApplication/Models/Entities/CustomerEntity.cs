using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApplication.Models.Entities
{
    public class CustomerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
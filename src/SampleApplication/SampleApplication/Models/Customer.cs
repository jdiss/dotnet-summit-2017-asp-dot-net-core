using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Models
{
    public class Customer
    {
        public int Id { get; set; }

        /// <summary>
        /// The first name of the customer.
        /// </summary>
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
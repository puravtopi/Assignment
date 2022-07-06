using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("First Name")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid First Name")]
        [RegularExpression(@"^[a-zA-z ]*$", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("First Name")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid Last Name")]
        [RegularExpression(@"^[a-zA-z ]*$", ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; }

    }
}

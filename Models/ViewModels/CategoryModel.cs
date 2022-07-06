using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Category")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid Category")]
        [RegularExpression(@"^[a-zA-z ]*$", ErrorMessage = "Invalid Category")]
        public string CategoryName { get; set; }

    }
}

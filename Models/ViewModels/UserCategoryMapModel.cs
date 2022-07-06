using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class UserCategoryMapModel
    {
        [Key]
        public int UserCategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("User Name")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}

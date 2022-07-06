using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class UserCategoryMap
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserCategoryId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId{ get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}

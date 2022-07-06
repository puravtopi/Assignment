using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class Expenses
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseId { get; set; }

        [Required]
        public int UserCategoryId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ExpenseDate{ get; set; }

        [ForeignKey("UserCategoryId")]
        public virtual UserCategoryMap UserCategoryMap { get; set; }

    }
}

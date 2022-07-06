using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class ExpensesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Category")]
        public int UserCategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Expense Date")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }

        [ForeignKey("UserCategoryId")]
        public virtual UserCategoryMap UserCategoryMap { get; set; }
    }
}

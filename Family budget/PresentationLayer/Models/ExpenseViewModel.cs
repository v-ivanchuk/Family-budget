using System;

namespace Family_budget.PresentationLayer.Models
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDateTime { get; set; }
        public int MemberId { get; set; }
    }
}

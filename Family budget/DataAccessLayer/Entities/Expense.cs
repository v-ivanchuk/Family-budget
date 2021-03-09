using Family_budget.DataAccessLayer.Entities;
using System;

namespace Family_budget.DataAccessLayer
{
    public class Expense : BaseEntity
    {
        public decimal Value { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDateTime { get; set; }
        public Member Member { get; set; }
    }
}

using System;

namespace Family_budget.BusinessLayer.DTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int MemberId { get; set; }
    }
}

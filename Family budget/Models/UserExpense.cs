using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.Models
{
    public class UserExpense
    {
        public int Id { get; set; }
        public decimal Expense { get; set; }
        public DateTime CountDate { get; set; }
    }
}

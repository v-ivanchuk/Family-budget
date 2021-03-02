using Family_budget.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}

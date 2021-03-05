using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.PresentationLayer.Models
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public ICollection<ExpenseViewModel> Expenses { get; set; }
    }
}

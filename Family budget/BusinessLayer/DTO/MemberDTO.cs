using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.DTO
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}

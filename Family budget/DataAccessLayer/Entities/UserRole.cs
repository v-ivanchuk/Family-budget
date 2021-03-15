using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Entities
{
    public enum UserRole
    {
        NotAssigned = 0,
        Admin = 1,
        FamilyHead = 2,
        FamilyMember = 3
    }
}

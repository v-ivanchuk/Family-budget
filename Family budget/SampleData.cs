using Family_budget.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget
{
    public class SampleData
    {
        public static void Initialize(BudgetContext context)
        {
            if (context.Members.Any())
            {
                return;
            }
            context.Members.AddRange(
                new Member
                {
                    Name = "Den"
                },
                new Member
                {
                    Name = "Donald"
                },
                new Member
                {
                    Name = "Joe"
                }
            );
            context.SaveChanges();

        }
    }
}

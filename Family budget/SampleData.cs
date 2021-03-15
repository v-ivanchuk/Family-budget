using Family_budget.DataAccessLayer;
using System.Linq;

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
            context.Users.AddRange(
                new User
                {
                    Name = "admintest",
                    Login = "admin",
                    Password = "sa"
                }
            );
            context.SaveChanges();

        }
    }
}

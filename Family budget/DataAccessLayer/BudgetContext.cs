using Microsoft.EntityFrameworkCore;

namespace Family_budget.DataAccessLayer
{
    public class BudgetContext : DbContext
    {

        #region Tables
        public DbSet<Member> Members { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        public BudgetContext(DbContextOptions<BudgetContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

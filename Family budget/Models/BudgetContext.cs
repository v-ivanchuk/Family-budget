using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.Models
{
    public class BudgetContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserExpense> UserExpenses { get; set; }

        public BudgetContext(DbContextOptions<BudgetContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

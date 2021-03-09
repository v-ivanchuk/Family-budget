using Family_budget.DataAccessLayer.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(BudgetContext budgetContext)
            : base(budgetContext)
        {
        }

        public new async Task UpdateAsync(Member member)
        {
            var checkMember = context.Members.FirstOrDefault(m => m.Id == member.Id);
            if (checkMember == null)
            {
                return;
            }

            checkMember.DateUpdated = DateTime.UtcNow;
            checkMember.Name = member.Name;

            await Task.Run(() => context.Members.Update(checkMember));
        }
    }
}

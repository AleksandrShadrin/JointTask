using Core;
using Interfaces;

namespace FinanceUtilities
{
    public class UsersRewards : IUsersRewards
    {
        public IEnumerable<User> AddRewards(IEnumerable<User> users, int percent)
        {
            foreach(var user in users)
            {
                var amount = int.Parse(user.GetAmount());
                var totalSum = amount * ((double)percent / 100 + 1);
                user.SetAmount(totalSum.ToString());
                yield return user;
            }
        }
    }
}

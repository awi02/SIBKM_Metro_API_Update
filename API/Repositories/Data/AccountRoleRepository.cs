using API.Context;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data
{
    public class AccountRoleRepository : GeneralRepos<AccountRole, int, MyContext>, IAccountRoleRepository
    {
        public AccountRoleRepository(MyContext context) : base(context)
        {
        }
    }
}


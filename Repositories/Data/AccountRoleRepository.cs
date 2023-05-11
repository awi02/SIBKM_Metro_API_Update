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
        public IEnumerable<string> GetRolesByEmail(string email)
        {
            var employeeNIK =_context.Employees.FirstOrDefault(e=>e.Email==email)!.NIK;
            var accountRoles = _context.Account_Roles
                                     .Where(ar => ar.Account_nik == employeeNIK)
                                     .Join(_context.Roles,
                                            ar => ar.Role_id,
                                            r => r.Id,
                                            (ar, r) => new { ar, r })
                                     .Select(role => role.r.Name);
            return accountRoles;
        }
    }
}


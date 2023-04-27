using API.Models;

namespace API.Repositories.Interface
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        Account? GetById(string id);
        int Insert(Account profiling);
        int Update(Account profiling);
        int Delete(string id);
    }
}

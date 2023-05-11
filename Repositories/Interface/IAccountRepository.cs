using API.Models;
using API.ViewModels;

namespace API.Repositories.Interface
{
    public interface IAccountRepository:IGeneralRepos<Account,string>
    {
        int Register(RegisterVM register);
        bool Login(loginVM loginVM);
    }
}

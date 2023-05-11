using API.Models;

namespace API.Repositories.Interface
{
    public interface IEmployeeRepository:IGeneralRepos<Employee,string>
    {
        string GetFullNameByEmail(string email);
    }
}

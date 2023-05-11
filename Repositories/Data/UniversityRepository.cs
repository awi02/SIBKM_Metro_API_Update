using API.Context;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Data
{
    public class UniversityRepository : GeneralRepos<University, int, MyContext>, IUniversityRepository
    {
        public UniversityRepository(MyContext context) : base(context)
        {
        }
    }
}

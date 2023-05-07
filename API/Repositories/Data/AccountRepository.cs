using API.Context;
using API.Handler;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;

namespace API.Repositories.Data
{
    public class AccountRepository : GeneralRepos<Account, string, MyContext>, IAccountRepository
    {
        public AccountRepository(MyContext context) : base(context)
        {
        }
        public int Register(RegisterVM registvm)
        {
            int result = 0;
            var univeristy = new University
            {
                Name = registvm.UniversityName
            };
            if (_context.Universities.Any(u => u.Name.Contains(registvm.UniversityName)))
            {
                univeristy.Id = _context.Universities.FirstOrDefault(u => u.Name.Contains(registvm.UniversityName))!.Id;
            }
            else
            {
                _context.Set<University>().Add(univeristy);
                result = _context.SaveChanges();
            }

            var education = new Education
            {
                Major = registvm.Major,
                Degree = registvm.Degree,
                Gpa = registvm.GPA,
                University_id = univeristy.Id
            };
            _context.Set<Education>().Add(education);
            result += _context.SaveChanges();

            var employee = new Employee
            {
                NIK = registvm.NIK,
                FirstName = registvm.FirstName,
                LastName = registvm.LastName,
                BirthDate = registvm.BirthDate,
                Gender = registvm.Gender,
                HiringDate = DateTime.Now,
                Email = registvm.Email,
                PhoneNumber = registvm.PhoneNumber
            };
            _context.Set<Employee>().Add(employee);
            result += _context.SaveChanges();

            var account = new Account
            {
                EmployeeNIK = registvm.NIK,
                Password = Hashing.HashPassword(registvm.Password)
            };
            _context.Set<Account>().Add(account);
            result += _context.SaveChanges();

            var profiling = new Profiling
            {
                EmployeeNIK = registvm.NIK,
                EducationId = education.Id
            };
            _context.Set<Profiling>().Add(profiling);
            result += _context.SaveChanges();

            var accorole = new AccountRole
            {
                Account_nik = registvm.NIK,
                Role_id =1
            };
            _context.Set<AccountRole>().Add(accorole);
            result += _context.SaveChanges();

            return result;
        }
        public bool Login(loginVM loginVM)
        {
            var getEmployeeAccount = _context.Employees.Join(_context.Accounts,
                                                             e => e.NIK,
                                                             a => a.EmployeeNIK,
                                                             (e, a) => new {
                                                                 Email = e.Email,
                                                                 Password = a.Password
                                                             }).FirstOrDefault(e =>
                                                                                   e.Email == loginVM.Email);

            if (getEmployeeAccount == null)
            {
                return false;
            }

            return Hashing.ValidatePassword(loginVM.Password, getEmployeeAccount.Password);
        }
    }

}
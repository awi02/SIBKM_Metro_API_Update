using API.Context;
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
            _context.Universities.Add(univeristy);
            result=_context.SaveChanges();

            var education = new Education
            {
                Major = registvm.Major,
                Degree = registvm.Degree,
                Gpa = registvm.GPA,
                University_id = univeristy.Id
            };
            _context.Educations.Add(education);
            result=_context.SaveChanges();

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
            _context.Employees.Add(employee);
            result=_context.SaveChanges();

            var account = new Account
            {
                EmployeeNIK = registvm.NIK,
                Password = registvm.Password
            };
            _context.Accounts.Add(account);
            result=_context.SaveChanges();

            var profiling = new Profiling
            {
                EmployeeNIK = registvm.NIK,
                EducationId = education.Id
            };
            _context.Profilings.Add(profiling);
            result = _context.SaveChanges();

            var accorole = new AccountRole
            {
                Account_nik = registvm.NIK,
                Role_id = 1
            };
            _context.Account_Roles.Add(accorole);
            result = _context.SaveChanges();

            return result;
        }
        public bool Login(loginVM loginVM)
        {

            //ambil data dari database berdasar email
            var employee = _context.Employees.FirstOrDefault(e=>e.Email==loginVM.Email);
            if (employee == null)
            {
                return false;
            }
            //gabng data dari database berdasar NIK
            var account = _context.Accounts.FirstOrDefault(e => e.EmployeeNIK == employee.NIK);
            if (account == null)
            {
                return false;
            }

            //cocokan dengan password
            if(account.Password != loginVM.Password)
            {
                return false;
            }

            //cek

            return true;
        }
    }
}
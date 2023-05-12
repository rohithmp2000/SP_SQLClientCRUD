using SP_SQLClientCRUD.Models;

namespace SP_SQLClientCRUD.Services
{
    public interface IEmpServices
    {
        public IEnumerable<EmpModel> GetAllEmployees();
        public int AddEmployee(EmpModel emp);
        public int UpdateEmployee(EmpModel empSub);
        public int DeleteEmployee(int empSub);
        public EmpModel Readbyid(int empId);
    }
}
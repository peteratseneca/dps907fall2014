using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Associations
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeService.svc or EmployeeService.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeService : IEmployeeService
    {
        // Reference to the existing worker class
        ServiceLayer.Worker m = new ServiceLayer.Worker();

        public string HelloWorld()
        {
            return "Hello, world!";
        }

        public IEnumerable<Controllers.EmployeeBase> AllEmployees()
        {
            // Use the repository method to get all employees
            return m.Employees.GetAll();
        }

        public Controllers.EmployeeBase AddEmployee(Controllers.EmployeeAdd newItem)
        {
            // Use the repository method to add a new employee
            var addedItem = m.Employees.AddNew(newItem);

            return addedItem;
        }

    }
}



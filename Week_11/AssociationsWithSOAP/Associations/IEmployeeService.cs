using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Associations
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeService" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        string HelloWorld();

        [OperationContract]
        IEnumerable<Controllers.EmployeeBase> AllEmployees();

        [OperationContract]
        Controllers.EmployeeBase AddEmployee(Controllers.EmployeeAdd newItem);
    }
}








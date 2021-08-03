using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICore.Model;

namespace WebAPICore.Interface
{
  public  interface IDepartment
    {
        Task AddDepartment(Department department);
        Task GetAllDepartment();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Application.DataAccess;


namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataAccess<Medicine, int> t = new MedicineDataAccess();
            PatientDataAccess pac = new PatientDataAccess();
            pac.GetPatientTotalBill(1);
        }
    }
}

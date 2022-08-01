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
            var med =  (t.Get());
            foreach (var medd in med)
            {
                Console.WriteLine($"{medd.id}, {medd.m_name},{medd.m_price}");
            }

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using System.Data.SqlClient;

namespace Application.DataAccess
{
    public class DepartmentDataAccess : IDataAccess<Department, int>
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        public DepartmentDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=HMS;Integrated Security=SSPI");
        }

        Department IDataAccess<Department, int>.Create(Department entity)
        {
            Department d = new Department();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_InsertDepartment";

                // Define Parameters Here
                SqlParameter dept_name = new SqlParameter();
                dept_name.ParameterName = "@dept_name";
                dept_name.DbType = System.Data.DbType.String;
                dept_name.Direction = System.Data.ParameterDirection.Input;
                dept_name.Size = 255;
                dept_name.Value = @dept_name;




                // Add these parameters into the Parameters Collection of the SqlCommand Object
                Cmd.Parameters.AddRange(new SqlParameter[] { dept_name });

                int Result = Cmd.ExecuteNonQuery();
                if (Result > 0)
                    Console.WriteLine("Insert is Successfull");
                else
                    Console.WriteLine("Insert Failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading all records: {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }

            return d;
        }

        Department IDataAccess<Department, int>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Department> IDataAccess<Department, int>.Get()
        {
            List<Department> list = new List<Department>();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetDepartment";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Department dd = new Department();
                    dd.id = Convert.ToInt32(reader["id"]);
                    dd.dept_name = reader["dept_name"].ToString();
                    list.Add(dd);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading all records: {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }

            return list;
        }

        Department IDataAccess<Department, int>.Get(int d_id)
        {
            Department d = new Department();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetDepartmentById";

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.DbType = System.Data.DbType.Int32;
                id.Direction = System.Data.ParameterDirection.Input;
                id.Value = d_id;
                Cmd.Parameters.AddRange(new SqlParameter[] { id });

                int Result = Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Department dept = new Department();
                    dept.dept_name = reader["dept_name"].ToString();
                    dept.id = Convert.ToInt32(reader["id"]);
                    d = dept;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading all records: {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return d;
        }

        Department IDataAccess<Department, int>.Update(int id, Department entity)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using System.Data.SqlClient;

namespace Application.DataAccess
{
    public class TestDataAccess : IDataAccess<Test, int>
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        public TestDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=HMS;Integrated Security=SSPI");
        }

        Test IDataAccess<Test, int>.Create(Test test)
        {
            throw new NotImplementedException();
        }

        Test IDataAccess<Test, int>.Get(int test_id)
        {
            Test m = new Test();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetTestById";

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.DbType = System.Data.DbType.Int32;
                id.Direction = System.Data.ParameterDirection.Input;
                id.Value = test_id;
                Cmd.Parameters.AddRange(new SqlParameter[] { id });

                int Result = Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Test test = new Test();
                    test.id = Convert.ToInt32(reader["id"]);
                    test.test_name = reader["test_name"].ToString();
                    test.price = (float)Convert.ToDouble(reader["price"]);
                    m = test;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading records: {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return m;
        }

        Test IDataAccess<Test, int>.Delete(int test_id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Test> IDataAccess<Test, int>.Get()
        {
            List<Test> test_list = new List<Test>();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetTest";
                SqlDataReader reader = Cmd.ExecuteReader();

                while (reader.Read())
                {
                    Test test = new Test();
                    test.id = Convert.ToInt32(reader["id"]);
                    test.test_name = reader["test_name"].ToString();
                    test.price = (float)Convert.ToDouble(reader["price"]);
                    test_list.Add(test);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading records: {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return test_list;
        }

        Test IDataAccess<Test, int>.Update(int id, Test entity)
        {
            throw new NotImplementedException();
        }

        public void TestToPatient(int patient_id, int t_id)
        {
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_InsertTest_to_patient";

                // Define Parameters Here
                SqlParameter p_id = new SqlParameter();
                p_id.ParameterName = "@p_id";
                p_id.DbType = System.Data.DbType.Int32;
                p_id.Direction = System.Data.ParameterDirection.Input;
                p_id.Value = patient_id;


                SqlParameter test_id = new SqlParameter();
                test_id.ParameterName = "@test_id";
                test_id.DbType = System.Data.DbType.Int32;
                test_id.Direction = System.Data.ParameterDirection.Input;
                test_id.Value = t_id;

                var dateTime = DateTime.Now;
                var shortDateValue = dateTime.ToShortDateString();

                SqlParameter date_of_test = new SqlParameter();
                date_of_test.ParameterName = "@date_of_test";
                date_of_test.DbType = System.Data.DbType.Date;
                date_of_test.Direction = System.Data.ParameterDirection.Input;
                date_of_test.Value = shortDateValue;

                // Add these parameters into the Parameters Collection of the SqlCommand Object
                Cmd.Parameters.AddRange(new SqlParameter[] { p_id, test_id, date_of_test });

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
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using System.Data.SqlClient;

namespace Application.DataAccess
{
    public class OperationDataAccess : IDataAccess<Operation, int>
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        public  OperationDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=HMS;Integrated Security=SSPI");
        }

        Operation IDataAccess<Operation, int>.Create(Operation operation)
        {
            throw new NotImplementedException();
        }

        Operation IDataAccess<Operation, int>.Get(int op_id)
        {
            Operation m = new Operation();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetOperationById";

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.DbType = System.Data.DbType.Int32;
                id.Direction = System.Data.ParameterDirection.Input;
                id.Value = op_id;
                Cmd.Parameters.AddRange(new SqlParameter[] { id });

                int Result = Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Operation operation = new Operation();
                    operation.id = Convert.ToInt32(reader["id"]);
                    operation.op_name = reader["op_name"].ToString();
                    operation.op_charge = (float)Convert.ToDouble(reader["op_charge"]);
                    m = operation;
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

        Operation IDataAccess<Operation, int>.Delete(int op_id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Operation> IDataAccess<Operation, int>.Get()
        {
            List<Operation> op_list = new List<Operation>();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetOperation";
                SqlDataReader reader = Cmd.ExecuteReader();

                while (reader.Read())
                {
                    Operation operation = new Operation();
                    operation.id = Convert.ToInt32(reader["id"]);
                    operation.op_name = reader["op_name"].ToString();
                    operation.op_charge = (float)Convert.ToDouble(reader["op_charge"]);
                    op_list.Add(operation);
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
            return op_list;
        }

        Operation IDataAccess<Operation, int>.Update(int id, Operation entity)
        {
            throw new NotImplementedException();
        }

        public void OperationToPatient(int patient_id, int operation_id)
        {
            var dateTime = DateTime.Now;
            var shortDateValue = dateTime.ToShortDateString();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"Insert into Operations_to_Patient Values({patient_id}, {operation_id}, '{shortDateValue}')";
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
        }

    }
}
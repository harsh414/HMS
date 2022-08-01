using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using System.Data.SqlClient;

namespace Application.DataAccess
{
    public class MedicineDataAccess : IDataAccess<Medicine, int>
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        public MedicineDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=HMS;Integrated Security=SSPI");
        }

        Medicine IDataAccess<Medicine, int>.Create(Medicine medicine)
        {
            Medicine m = new Medicine();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_InsertMedicine";

                // Define Parameters Here
                SqlParameter m_name = new SqlParameter();
                m_name.ParameterName = "@m_name";
                m_name.DbType = System.Data.DbType.String;
                m_name.Direction = System.Data.ParameterDirection.Input;
                m_name.Size = 255;
                m_name.Value = medicine.m_name;

                SqlParameter m_price = new SqlParameter();
                m_price.ParameterName = "@m_price";
                m_price.DbType = (System.Data.DbType)(float)System.Data.DbType.Double;
                m_price.Direction = System.Data.ParameterDirection.Input;
                m_price.Value = medicine.m_price;

                // Add these parameters into the Parameters Collection of the SqlCommand Object
                Cmd.Parameters.AddRange(new SqlParameter[] { m_name, m_price});

                int Result = Cmd.ExecuteNonQuery();
                if (Result > 0)
                    Console.WriteLine("Insert is Successfull");
                else
                    Console.WriteLine("Insert Failed");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating the record: {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return m;
        }

        Medicine IDataAccess<Medicine, int>.Get(int m_id)
        {
            Medicine m = new Medicine();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetMedicineById";

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.DbType = System.Data.DbType.Int32;
                id.Direction = System.Data.ParameterDirection.Input;
                id.Value = m_id;
                Cmd.Parameters.AddRange(new SqlParameter[] { id });

                int Result = Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Medicine medicine = new Medicine();
                    medicine.id = Convert.ToInt32(reader["id"]);
                    medicine.m_name = reader["m_name"].ToString();
                    medicine.m_price = (float)Convert.ToDouble(reader["m_price"]);
                    m = medicine;
                }
                reader.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while reading records: {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }
            return m;
        }

        Medicine IDataAccess<Medicine, int>.Delete(int m_id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Medicine> IDataAccess<Medicine, int>.Get()
        {
            List<Medicine> m_list = new List<Medicine>();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetMedicine";
                SqlDataReader reader = Cmd.ExecuteReader();

                while (reader.Read())
                {
                    Medicine medicine = new Medicine();
                    medicine.id = Convert.ToInt32(reader["id"]);
                    medicine.m_name = reader["m_name"].ToString();
                    medicine.m_price = (float)Convert.ToDouble(reader["m_price"]);
                    m_list.Add(medicine);
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
            return m_list;
        }

        Medicine IDataAccess<Medicine, int>.Update(int id, Medicine entity)
        {
            throw new NotImplementedException();
        }

        public void InsertPatientMedicineMap(int patient_id, int medicine_id, int med_qty)
        {
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_InsertPatientMedicineMap";

                // Define Parameters Here
                SqlParameter p_id = new SqlParameter();
                p_id.ParameterName = "@p_id";
                p_id.DbType = System.Data.DbType.Int32;
                p_id.Direction = System.Data.ParameterDirection.Input;
                p_id.Value = patient_id;


                SqlParameter m_id = new SqlParameter();
                m_id.ParameterName = "@m_id";
                m_id.DbType = System.Data.DbType.Int32;
                m_id.Direction = System.Data.ParameterDirection.Input;
                m_id.Value = medicine_id;

                SqlParameter m_quantity = new SqlParameter();
                m_quantity.ParameterName = "@m_quantity";
                m_quantity.DbType = System.Data.DbType.Int32;
                m_quantity.Direction = System.Data.ParameterDirection.Input;
                m_quantity.Value = med_qty;

                var dateTime = DateTime.Now;
                var shortDateValue = dateTime.ToShortDateString();

                SqlParameter m_intake_date = new SqlParameter();
                m_intake_date.ParameterName = "@m_intake_date";
                m_intake_date.DbType = System.Data.DbType.Date;
                m_intake_date.Direction = System.Data.ParameterDirection.Input;
                m_intake_date.Value = shortDateValue;

                // Add these parameters into the Parameters Collection of the SqlCommand Object
                Cmd.Parameters.AddRange(new SqlParameter[] { p_id, m_id, m_quantity, m_intake_date });

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

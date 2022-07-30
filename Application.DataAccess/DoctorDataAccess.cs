using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using System.Data.SqlClient;

namespace Application.DataAccess
{
    public class DoctorDataAccess : IDataAccess<Doctor, int>
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        Doctor doc = new Doctor();

        public DoctorDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=HMS;Integrated Security=SSPI");

        }

        Doctor IDataAccess<Doctor, int>.Create(Doctor doctor)
        {

            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_InsertDoctor";

                SqlParameter dr_name = new SqlParameter();
                dr_name.ParameterName = "@dr_name";
                dr_name.DbType = System.Data.DbType.String;
                dr_name.Direction = System.Data.ParameterDirection.Input;
                dr_name.Size = 255;
                dr_name.Value = doctor.dr_name;

                SqlParameter dept_id = new SqlParameter();
                dept_id.ParameterName = "@dept_id";
                dept_id.DbType = System.Data.DbType.Int32;
                dept_id.Direction = System.Data.ParameterDirection.Input;
                dept_id.Value = doctor.dept_id;

                Cmd.Parameters.AddRange(new SqlParameter[] { dr_name, dept_id });

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

            return doc;
        }

        Doctor IDataAccess<Doctor, int>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Doctor> IDataAccess<Doctor, int>.Get()
        {
            List<Doctor> doctorList = new List<Doctor>();

            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetDoctor";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {

                    Doctor doctor = new Doctor();
                    doctor.id= Convert.ToInt32(reader["id"]);
                    doctor.dr_name = reader["dr_name"].ToString();
                    doctor.dept_id = Convert.ToInt32(reader["dept_id"]);

                    doctorList.Add(doctor);
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

            return doctorList;

        }

        Doctor IDataAccess<Doctor, int>.Get(int dr_id)
        {
            Doctor doctor = new Doctor();

            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetDoctorById";

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.DbType = System.Data.DbType.Int32;
                id.Direction = System.Data.ParameterDirection.Input;
                id.Value = dr_id;
                Cmd.Parameters.AddRange(new SqlParameter[] { id });


                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Doctor doc = new Doctor();
                    doc.dr_name = reader["dr_name"].ToString();
                    doc.dept_id = Convert.ToInt32(reader["dept_id"]);
                    doctor = doc;
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

            return doctor;
        }

        Doctor IDataAccess<Doctor, int>.Update(int id, Doctor entity)
        {
            throw new NotImplementedException();
        }

        public List<Doctor> GetDoctorsForADept(int dept_id)
        {
            List<Doctor> list = new List<Doctor>();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT * FROM Doctor WHERE dept_id={dept_id}";
                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Doctor dd = new Doctor();
                    dd.id= Convert.ToInt32(reader["id"]);
                    dd.dr_name = (reader["dr_name"]).ToString();
                    dd.dept_id = Convert.ToInt32(reader["dept_id"]);
                    list.Add(dd);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        public void AssignToIPD(int p_id,dept_id)
        {

        }
    }
}

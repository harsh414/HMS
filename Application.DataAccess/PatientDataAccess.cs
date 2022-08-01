using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using System.Data.SqlClient;

namespace Application.DataAccess
{
    public class PatientDataAccess : IDataAccess<Patient, int>
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        public PatientDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=HMS;Integrated Security=SSPI");
        }

        Patient IDataAccess<Patient, int>.Create(Patient patient)
        {
            Patient p = new Patient();
            patient.dr_id = AssignDrToDept(patient.dept_id);
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_InsertPatient";

                // Define Parameters Here
                SqlParameter p_name = new SqlParameter();
                p_name.ParameterName = "@p_name";
                p_name.DbType = System.Data.DbType.String;
                p_name.Direction = System.Data.ParameterDirection.Input;
                p_name.Size = 255;
                p_name.Value = patient.p_name;

                SqlParameter age = new SqlParameter();
                age.ParameterName = "@age";
                age.DbType = System.Data.DbType.Int32;
                age.Direction = System.Data.ParameterDirection.Input;
                age.Value = patient.age;

                SqlParameter gender = new SqlParameter();
                gender.ParameterName = "@gender";
                gender.DbType = System.Data.DbType.String;
                gender.Direction = System.Data.ParameterDirection.Input;
                gender.Size = 255;
                gender.Value = patient.gender;

                SqlParameter address = new SqlParameter();
                address.ParameterName = "@address";
                address.DbType = System.Data.DbType.String;
                address.Direction = System.Data.ParameterDirection.Input;
                address.Size = 255;
                address.Value = patient.address;

                SqlParameter phone = new SqlParameter();
                phone.ParameterName = "@phone";
                phone.DbType = System.Data.DbType.String;
                phone.Direction = System.Data.ParameterDirection.Input;
                phone.Size = 255;
                phone.Value = patient.phone;

                SqlParameter dept_id = new SqlParameter();
                dept_id.ParameterName = "@dept_id";
                dept_id.DbType = System.Data.DbType.Int32;
                dept_id.Direction = System.Data.ParameterDirection.Input;
                dept_id.Value = patient.dept_id;

                SqlParameter dr_id = new SqlParameter();
                dr_id.ParameterName = "@dr_id";
                dr_id.DbType = System.Data.DbType.Int32;
                dr_id.Direction = System.Data.ParameterDirection.Input;
                dr_id.Value = patient.dr_id;

                SqlParameter assigned_to_dr_id = new SqlParameter();
                assigned_to_dr_id.ParameterName = "@assigned_to_dr_id";
                assigned_to_dr_id.DbType = System.Data.DbType.Int32;
                assigned_to_dr_id.Direction = System.Data.ParameterDirection.Input;
                assigned_to_dr_id.Value = DBNull.Value;

                SqlParameter is_assigned = new SqlParameter();
                is_assigned.ParameterName = "@is_assigned";
                is_assigned.DbType = System.Data.DbType.Byte;
                is_assigned.Direction = System.Data.ParameterDirection.Input;
                is_assigned.Value = 0;


                // Add these parameters into the Parameters Collection of the SqlCommand Object
                Cmd.Parameters.AddRange(new SqlParameter[] { p_name, age, gender, address, phone, dept_id, dr_id, assigned_to_dr_id, is_assigned });

                int Result = Cmd.ExecuteNonQuery();

                if (Result > 0)
                    Console.WriteLine("Insert is Successfull");
                else
                    Console.WriteLine("Insert Failed");
                //Cmd = new SqlCommand();
                //Cmd.Connection = Conn;
                //Cmd.CommandType = System.Data.CommandType.Text;
                //Cmd.CommandText = $"SELECT SCOPE_IDENTITY()";
                ////Cmd.CommandText = $"Insert INTO PatientsSeenInOPD values({dr_id},{p.id})";
                //Cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading all records: {ex.Message}");
            }
            finally
            {
                Conn.Close();
            }

            return p;
        }

        Patient IDataAccess<Patient, int>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Patient> IDataAccess<Patient, int>.Get()
        {
            List<Patient> l= new List<Patient> ();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetPatient";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Patient patient = new Patient();
                    patient.id = Convert.ToInt32(reader["id"]);
                    patient.p_name = reader["p_name"].ToString();
                    patient.age = Convert.ToInt32(reader["age"]);
                    patient.gender = reader["gender"].ToString();
                    patient.address = reader["address"].ToString();
                    patient.phone = reader["phone"].ToString();
                    patient.dept_id = (reader["dept_id"] as int?).GetValueOrDefault();
                    patient.dr_id = (reader["dr_id"] as int?).GetValueOrDefault();
                    patient.assigned_to_dr_id = (reader["assigned_to_dr_id"] as int?).GetValueOrDefault(); ;
                    patient.is_assigned = Convert.ToBoolean(reader["is_assigned"]);
                    l.Add(patient);
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

            return l;
        }

        Patient IDataAccess<Patient, int>.Get(int p_id)
        {
            Patient p = new Patient();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetPatientById";

                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.DbType = System.Data.DbType.Int32;
                id.Direction = System.Data.ParameterDirection.Input;
                id.Value = p_id;
                Cmd.Parameters.AddRange(new SqlParameter[] { id });

                int Result = Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Patient patient = new Patient();
                    patient.id = Convert.ToInt32(reader["id"]);
                    patient.p_name = reader["p_name"].ToString();
                    patient.age = Convert.ToInt32(reader["age"]);
                    patient.gender = reader["gender"].ToString();
                    patient.address = reader["address"].ToString();
                    patient.phone = reader["phone"].ToString();
                    patient.dept_id = Convert.ToInt32(reader["dept_id"]);
                    patient.dr_id = Convert.ToInt32(reader["dr_id"]);
                    patient.assigned_to_dr_id = (reader["assigned_to_dr_id"] as int?).GetValueOrDefault(); ;
                    patient.is_assigned = Convert.ToBoolean(reader["is_assigned"]);
                    p=patient;
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
            return p;
        }

        Patient IDataAccess<Patient, int>.Update(int id, Patient entity)
        {
            throw new NotImplementedException();
        }

        
        public int AssignDrToDept(int dept_id)
        {
            int dr_id = 0; 
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT TOP 1 id FROM Doctor where dept_id={dept_id}  ORDER BY NEWID()";
                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    dr_id = Convert.ToInt32(reader["id"]);
                    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }

            return dr_id;
        }

        public List<Patient> GetPatientsForADoctor(int dr_id)
        {
            List<Patient> patients = new List<Patient>();
            
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT * FROM Patient WHERE dr_id={dr_id}";
                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Patient patient = new Patient();
                    patient.id = Convert.ToInt32(reader["id"]);
                    patient.p_name = reader["p_name"].ToString();
                    patient.age = Convert.ToInt32(reader["age"]);
                    patient.gender = reader["gender"].ToString();
                    patient.address = reader["address"].ToString();
                    patient.phone = reader["phone"].ToString();
                    patient.dept_id = Convert.ToInt32(reader["dept_id"]);
                    patient.dr_id = Convert.ToInt32(reader["dr_id"]);
                    patient.assigned_to_dr_id = (reader["assigned_to_dr_id"] as int?).GetValueOrDefault();
                    patient.is_assigned = Convert.ToBoolean(reader["is_assigned"]);
                    patients.Add(patient);
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
            return patients;
        }


        public void AssignToIPD(Patient patient, int new_dr_id)
        {

            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_UpdatePatientById";

                // Define Parameters Here
                SqlParameter id = new SqlParameter();
                id.ParameterName = "@id";
                id.DbType = System.Data.DbType.Int32;
                id.Direction = System.Data.ParameterDirection.Input;
                id.Value = patient.id;


                SqlParameter p_name = new SqlParameter();
                p_name.ParameterName = "@p_name";
                p_name.DbType = System.Data.DbType.String;
                p_name.Direction = System.Data.ParameterDirection.Input;
                p_name.Size = 255;
                p_name.Value = patient.p_name;

                SqlParameter age = new SqlParameter();
                age.ParameterName = "@age";
                age.DbType = System.Data.DbType.Int32;
                age.Direction = System.Data.ParameterDirection.Input;
                age.Value = patient.age;

                SqlParameter gender = new SqlParameter();
                gender.ParameterName = "@gender";
                gender.DbType = System.Data.DbType.String;
                gender.Direction = System.Data.ParameterDirection.Input;
                gender.Size = 255;
                gender.Value = patient.gender;

                SqlParameter address = new SqlParameter();
                address.ParameterName = "@address";
                address.DbType = System.Data.DbType.String;
                address.Direction = System.Data.ParameterDirection.Input;
                address.Size = 255;
                address.Value = patient.address;

                SqlParameter phone = new SqlParameter();
                phone.ParameterName = "@phone";
                phone.DbType = System.Data.DbType.String;
                phone.Direction = System.Data.ParameterDirection.Input;
                phone.Size = 255;
                phone.Value = patient.phone;

                SqlParameter dept_id = new SqlParameter();
                dept_id.ParameterName = "@dept_id";
                dept_id.DbType = System.Data.DbType.Int32;
                dept_id.Direction = System.Data.ParameterDirection.Input;
                dept_id.Value = patient.dept_id;

                SqlParameter dr_id = new SqlParameter();
                dr_id.ParameterName = "@dr_id";
                dr_id.DbType = System.Data.DbType.Int32;
                dr_id.Direction = System.Data.ParameterDirection.Input;
                dr_id.Value = patient.dr_id;


                SqlParameter is_assigned = new SqlParameter();
                is_assigned.ParameterName = "@is_assigned";
                is_assigned.DbType = System.Data.DbType.Byte;
                is_assigned.Direction = System.Data.ParameterDirection.Input;
                is_assigned.Value = patient.is_assigned;


                SqlParameter assigned_to_dr_id = new SqlParameter();
                assigned_to_dr_id.ParameterName = "@assigned_to_dr_id";
                assigned_to_dr_id.DbType = System.Data.DbType.Int32;
                assigned_to_dr_id.Direction = System.Data.ParameterDirection.Input;
                assigned_to_dr_id.Value = new_dr_id;

               

            


                // Add these parameters into the Parameters Collection of the SqlCommand Object
                Cmd.Parameters.AddRange(new SqlParameter[] { id, p_name, age, gender, address, phone, dept_id, dr_id, is_assigned, assigned_to_dr_id});
                int Result = Cmd.ExecuteNonQuery();
                if (Result > 0)
                {
                    Console.WriteLine("sds");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Conn.Close();
            }
        }

        public bool isAlreadyAssignedToIPD(int p_id)
        {
            int inc = 0;
            try
            {
                
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT id FROM Patient where id={p_id} and is_assigned=1 and assigned_to_dr_id>=1";
                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    inc++;
                }
                reader.Close();
                if(inc > 0) return true;
                return false;   
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

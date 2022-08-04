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

                //*************************************
           
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT MAX(ID) as new_p_id FROM Patient";
                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    patient.id = Convert.ToInt32(reader["new_p_id"]);
                    break;
                }
                reader.Close(); 


                //**************************************************insert to patientsSeenInOPD
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_InsertPatientsSeenInOPD";

                // Define Parameters Here

                var dateTime = DateTime.Now;
                var shortDateValue = dateTime.ToShortDateString();
                SqlParameter doctor_id = new SqlParameter();
                doctor_id.ParameterName = "@dr_id";
                doctor_id.DbType = System.Data.DbType.Int32;
                doctor_id.Direction = System.Data.ParameterDirection.Input;
                doctor_id.Value = patient.dr_id;

                SqlParameter patient_id = new SqlParameter();
                patient_id.ParameterName = "@p_id";
                patient_id.DbType = System.Data.DbType.Int32;
                patient_id.Direction = System.Data.ParameterDirection.Input;
                patient_id.Value = patient.id;

                SqlParameter date_of_admitted = new SqlParameter();
                date_of_admitted.ParameterName = "@date";
                date_of_admitted.DbType = System.Data.DbType.Date;
                date_of_admitted.Direction = System.Data.ParameterDirection.Input;
                date_of_admitted.Value = shortDateValue ;

                


                // Add these parameters into the Parameters Collection of the SqlCommand Object
                Cmd.Parameters.AddRange(new SqlParameter[] { doctor_id, patient_id, date_of_admitted});

                 Result = Cmd.ExecuteNonQuery();
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

        public List<Patient> GetPatientsInIpd()
        {
            List<Patient> patients = new List<Patient>();

            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT * FROM Patient WHERE is_assigned = 1";
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

                var dateTime = DateTime.Now;
                var shortDateValue = dateTime.ToShortDateString();

               
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"Insert into SeenByIPDDoctor Values({new_dr_id}, {patient.id}, '{shortDateValue}')";
                Cmd.ExecuteNonQuery();
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

        public float GetPatientTotalBill(int patient_id)
        {
            Bill bill = new Bill();
            bill.p_id = patient_id;
            bill.consultancy_fee = 500;
            float final_bill = 0;
            try
            {

                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT Room.room_type,Alloted_room.date_admitted,Alloted_room.date_checkout,DATEDIFF(DAY, Alloted_room.date_admitted, Alloted_room.date_checkout) as no_of_days,Room.price" +
                    $" FROM Room INNER JOIN Alloted_room ON room.id = Alloted_room.room_id where Alloted_room.p_id = {patient_id}";
                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    bill.room_type = reader["room_type"].ToString();
                    bill.no_of_days = Convert.ToInt32(reader["no_of_days"]);
                    bill.room_charge = bill.no_of_days * (float)Convert.ToDouble(reader["price"]);

                }
                reader.Close();


                //query2
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT Doctor.dr_name as dr_name"+
                    $" FROM Patient INNER JOIN Doctor ON patient.dr_id = doctor.id where Patient.id = {patient_id}";
                Cmd.ExecuteNonQuery();
                reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    bill.dr_name = reader["dr_name"].ToString();
                }
                reader.Close();


                //query3
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT Operations.op_charge as op_charge FROM Operations INNER JOIN Operations_to_Patient ON Operations.id = Operations_to_Patient.op_id where Operations_to_Patient.p_id ={patient_id}";
                reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    bill.op_charge = ((float)Convert.ToDouble(reader["op_charge"]) as float?).GetValueOrDefault();
                }
                reader.Close();


                //query4
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT ISNULL(SUM(Test.price),0) as total_test_price FROM Test INNER JOIN Test_to_patient ON Test.id= Test_to_patient.test_id where Test_to_patient.p_id={patient_id}";
                reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["total_test_price"]);
                    
                    bill.test_charge = ((float)Convert.ToDouble(reader["total_test_price"]) as float?).GetValueOrDefault();
                }
                reader.Close();


                //query5
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT ISNULL(SUM(Medicine.m_price* PatientMedicineMap.m_quantity),0) as total_medicine_price FROM Medicine INNER JOIN PatientMedicineMap ON Medicine.id= PatientMedicineMap.m_id where PatientMedicineMap.p_id={patient_id}";
                reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    bill.med_charge = ((float)Convert.ToDouble(reader["total_medicine_price"]) as float?).GetValueOrDefault();
                }
                reader.Close();

                //(p_id,consultancy_fee,room_type,no_of_days,dr_name,op_charge,room_charge,test_charge,med_charge,)
                //insert into BILLS;
                final_bill += bill.room_charge + bill.consultancy_fee + bill.op_charge + bill.test_charge + bill.med_charge;
                bill.final_bill = final_bill;
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"INSERT INTO Bill values({bill.p_id},{bill.consultancy_fee},'{bill.room_type}',{bill.no_of_days},'{bill.dr_name}',{bill.op_charge},{bill.room_charge},{bill.test_charge},{bill.med_charge},{bill.final_bill})";
                int Result = Cmd.ExecuteNonQuery();
                if(Result > 0)
                {
                    Console.WriteLine("Insert Successfull");
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

            
            return final_bill;
        }

        public int AssignNurseToDept(int dept_id)
        {
            int nurse_id = 0;
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT TOP 1 id FROM Nurse where dept_id={dept_id}  ORDER BY NEWID()";
                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    nurse_id = Convert.ToInt32(reader["id"]);
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

            return nurse_id;
        }



        public int seenByNurse(int p_id, int new_nurse_id)
        {
            var dateTime = DateTime.Now;
            var shortDateValue = dateTime.ToShortDateString();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"Insert into SeenByNurse Values({new_nurse_id}, {p_id}, '{shortDateValue}')";
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
            return 0;
        }

        
        public List<Bill> PopulateBill(int patient_id)
        {
            List<Bill> list = new List<Bill>();
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetBillById";

                SqlParameter p_id = new SqlParameter();
                p_id.ParameterName = "@p_id";
                p_id.DbType = System.Data.DbType.Int32;
                p_id.Direction = System.Data.ParameterDirection.Input;
                p_id.Value = patient_id;

                Cmd.Parameters.AddRange(new SqlParameter[] { p_id });

                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Bill bill = new Bill();
                    bill.id= Convert.ToInt32(reader["id"]);
                    bill.p_id = Convert.ToInt32(reader["p_id"]);
                    bill.consultancy_fee = (float)Convert.ToDouble(reader["consultancy_fee"]);
                    
                    bill.room_type = reader["room_type"].ToString();
                    
                    bill.no_of_days = Convert.ToInt32(reader["no_of_days"]);
                    bill.dr_name = reader["dr_name"].ToString();
                    bill.op_charge = (float)Convert.ToDouble(reader["op_charge"]);
                    bill.room_charge = (float)Convert.ToDouble(reader["room_charge"]);
                    bill.test_charge = (float)Convert.ToDouble(reader["test_charge"]);
                    bill.med_charge = (float)Convert.ToDouble(reader["med_charge"]);
                    
                    bill.final_bill = (float)Convert.ToDouble(reader["final_bill"]);
                    list.Add(bill);
                }
                reader.Close();
                return list;
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

        public bool ifBillWasGenerated(int p_id)
        {
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT count(id) as cnt FROM Bill where p_id={p_id}";
                Cmd.ExecuteNonQuery();

                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    if(Convert.ToInt32(reader["cnt"]) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

            return false;
        }


    }


}

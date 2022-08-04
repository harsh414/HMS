using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using System.Data.SqlClient;

namespace Application.DataAccess
{

    public class RoomDataAccess : IDataAccess<Room, int>
    {
        SqlConnection Conn;
        SqlCommand Cmd;

        public RoomDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=HMS;Integrated Security=SSPI");
        }

        Room IDataAccess<Room, int>.Create(Room entity)
        {
            throw new NotImplementedException();
        }

        Room IDataAccess<Room, int>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Room> IDataAccess<Room, int>.Get()
        {
            List<Room> roomList = new List<Room>();

            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetRoom";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {

                    Room room = new Room();
                    room.id = Convert.ToInt32(reader["room_id"]);
                    room.room_type = reader["room_type"].ToString();
                    room.room_status = reader["room_status"].ToString();
                    room.occupancy = Convert.ToInt32(reader["occupancy"]);
                    room.price = (float)(Convert.ToDouble(reader["price"]));


                    roomList.Add(room);
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

            return roomList;
        }

        Room IDataAccess<Room, int>.Get(int id)
        {
            Room room = new Room();

            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetRoomById";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {

                    room.id = Convert.ToInt32(reader["room_id"]);
                    room.room_type = reader["room_type"].ToString();
                    room.room_status = reader["room_status"].ToString();
                    room.occupancy = Convert.ToInt32(reader["occupancy"]);
                    room.price = (float)(Convert.ToDouble(reader["price"]));



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

            return room;
        }

        Room IDataAccess<Room, int>.Update(int id, Room entity)
        {
            throw new NotImplementedException();
        }


        public void RoomToPatient(int patient_id, int room_id)
        {
            try
            {
                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                Cmd.CommandText = "sp_InsertAlloted_room";

                // Define Parameters Here
                SqlParameter p_id = new SqlParameter();
                p_id.ParameterName = "@p_id";
                p_id.DbType = System.Data.DbType.Int32;
                p_id.Direction = System.Data.ParameterDirection.Input;
                p_id.Value = patient_id;


                SqlParameter r_id = new SqlParameter();
                r_id.ParameterName = "@room_id";
                r_id.DbType = System.Data.DbType.Int32;
                r_id.Direction = System.Data.ParameterDirection.Input;
                r_id.Value = room_id;

                var dateTime = DateTime.Now;
                var endDate= dateTime.AddDays(3); ;
                var shortDateValue = dateTime.ToShortDateString();
                var shortDateValue2 = endDate.ToShortDateString();

                SqlParameter date_admitted = new SqlParameter();
                date_admitted.ParameterName = "@date_admitted";
                date_admitted.DbType = System.Data.DbType.Date;
                date_admitted.Direction = System.Data.ParameterDirection.Input;
                date_admitted.Value = shortDateValue;

                SqlParameter date_of_checkout = new SqlParameter();
                date_of_checkout.ParameterName = "@date_checkout";
                date_of_checkout.DbType = System.Data.DbType.Date;
                date_of_checkout.Direction = System.Data.ParameterDirection.Input;
                date_of_checkout.Value = shortDateValue2;

                // Add these parameters into the Parameters Collection of the SqlCommand Object
                Cmd.Parameters.AddRange(new SqlParameter[] { p_id, r_id, date_admitted, date_of_checkout });

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

        public bool isPatientAllotedRoom(int p_id)
        {
            int inc = 0;
            try
            {

                Conn.Open();
                Cmd = new SqlCommand();
                Cmd.Connection = Conn;
                Cmd.CommandType = System.Data.CommandType.Text;
                Cmd.CommandText = $"SELECT id FROM Alloted_room where p_id={p_id}";
                Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    inc++;
                }
                reader.Close();
                if (inc > 0) return true;
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

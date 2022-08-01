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
                Cmd.CommandText = "sp_GetRoom";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {


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
    }
}

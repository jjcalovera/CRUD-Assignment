using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUD_Assignment.Functions
{
    class Login
    {
        Components.Connection con = new Components.Connection();
        Components.Value val = new Components.Value();

        public bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT u.id, u.profilePicture, u.firstName, u.middleName, u.lastName, g.gender, u.age,
                                    u.birthday, u.contactNumber, u.email, u.username
                                    FROM users AS u
                                    INNER JOIN genders AS g ON u.genderFID = g.id
                                    WHERE u.username = @username AND u.password = MD5(@password);";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        connection.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        dt.Clear();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            val.MyId = dt.Rows[0].Field<int>("id");
                            val.MyProfilePicture = dt.Rows[0].Field<byte[]>("profilePicture");
                            val.MyFirstName = dt.Rows[0].Field<string>("firstName");
                            val.MyMiddleName = dt.Rows[0].Field<string>("middleName");
                            val.MyLastName = dt.Rows[0].Field<string>("lastName");
                            val.MyGender = dt.Rows[0].Field<string>("gender");
                            val.MyBirthday = dt.Rows[0].Field<DateTime>("birthday");
                            val.MyContactNumber = dt.Rows[0].Field<string>("contactNumber");
                            val.MyEmail = dt.Rows[0].Field<string>("email");
                            val.MyUsername = dt.Rows[0].Field<string>("username");

                            connection.Close();
                            return true;
                        }
                        else
                        {
                            connection.Close();
                            return false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error authenticating user: " + ex.ToString());
                return false;
            }
        }
    }
}

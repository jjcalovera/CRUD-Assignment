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
    class Check
    {
        Components.Connection con = new Components.Connection();
        Components.Value val = new Components.Value();

        public bool CheckUsernameIfExists(string username)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT username
                                    FROM users
                                    WHERE username = @username;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        connection.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        dt.Clear();
                        da.Fill(dt);

                        if(dt.Rows.Count > 0)
                        {
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
                Console.WriteLine("Error checking username if exists: " + ex.ToString());
                return false;
            }
        }
    }
}

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
    class Gender
    {
        Components.Connection con = new Components.Connection();
        Components.Value val = new Components.Value();

        public void LoadGendersInCmbGender(ComboBox cmb)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT gender
                                    FROM genders;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        connection.Open();

                        MySqlDataReader dr = cmd.ExecuteReader();

                        while(dr.Read())
                        {
                            string gender = dr.GetString("gender");
                            cmb.Items.Add(gender);
                        }

                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading genders in combo box gender: " + ex.ToString());
            }
        }
    }
}

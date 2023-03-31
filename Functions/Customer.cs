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
    class Customer
    {
        Components.Connection con = new Components.Connection();
        Components.Value val = new Components.Value();

        public void LoadCustomers(DataGridView grid)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT c.id, c.firstName, c.middleName, c.lastName, g.gender, c.age, DATE_FORMAT(c.birthday, '%m/%d/%Y'),
                                    c.contactNumber, c.email, DATE_FORMAT(c.createdAt, '%m/%d/%Y'), DATE_FORMAT(c.updatedAt, '%m/%d/%Y')
                                    FROM customers AS c
                                    INNER JOIN genders AS g ON c.genderFID = g.id
                                    ORDER BY firstName;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        connection.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        dt.Clear();
                        da.Fill(dt);

                        grid.DataSource = dt;
                        grid.ClearSelection();

                        grid.Columns["id"].Visible = false;
                        grid.Columns["firstName"].HeaderText = "First Name";
                        grid.Columns["middleName"].HeaderText = "Middle Name";
                        grid.Columns["lastName"].HeaderText = "Last Name";
                        grid.Columns["gender"].HeaderText = "Gender";
                        grid.Columns["age"].HeaderText = "Age";
                        grid.Columns["DATE_FORMAT(c.birthday, '%m/%d/%Y')"].HeaderText = "Birthday";
                        grid.Columns["contactNumber"].HeaderText = "Contact Number";
                        grid.Columns["email"].HeaderText = "Email";
                        grid.Columns["DATE_FORMAT(c.createdAt, '%m/%d/%Y')"].HeaderText = "Created At";
                        grid.Columns["DATE_FORMAT(c.updatedAt, '%m/%d/%Y')"].HeaderText = "Updated At";

                        connection.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error loading customers: " + ex.ToString());
            }
        }

        public bool GetCustomer(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT c.id, c.firstName, c.middleName, c.lastName, g.gender, c.age, c.birthday,
                                    c.contactNumber, c.email
                                    FROM customers AS c
                                    INNER JOIN genders AS g ON c.genderFID = g.id
                                    WHERE c.id = @id;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        connection.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        dt.Clear();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            val.CustomerId = dt.Rows[0].Field<int>("id");
                            val.CustomerFirstName = dt.Rows[0].Field<string>("firstName");
                            val.CustomerMiddleName = dt.Rows[0].Field<string>("middleName");
                            val.CustomerLastName = dt.Rows[0].Field<string>("lastName");
                            val.CustomerGender = dt.Rows[0].Field<string>("gender");
                            val.CustomerAge = dt.Rows[0].Field<int>("age");
                            val.CustomerBirthday = dt.Rows[0].Field<DateTime>("birthday");
                            val.CustomerContactNumber = dt.Rows[0].Field<string>("contactNumber");
                            val.CustomerEmail = dt.Rows[0].Field<string>("email");

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
                Console.WriteLine("Error getting customer: " + ex.ToString());
                return false;
            }
        }

        public bool AddCustomer(string firstName, string middleName, string lastName, string gender, int age, DateTime birthday,
            string contactNumber, string email)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT id
                                    FROM genders
                                    WHERE gender = @gender;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@gender", gender);

                        connection.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        dt.Clear();
                        da.Fill(dt);

                        val.GenderId = dt.Rows[0].Field<int>("id");
                    }

                    sql = @"INSERT INTO customers(firstName, middleName, lastName, genderFID, age, birthday, contactNumber, email)
                                VALUES(@firstName, @middleName, @lastName, @genderFID, @age, @birthday, @contactNumber, @email);";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@middleName", middleName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@genderFID", val.GenderId);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@birthday", birthday);
                        cmd.Parameters.AddWithValue("@contactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@email", email);

                        MySqlDataReader dr = cmd.ExecuteReader();
                        dr.Close();

                        connection.Close();

                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error adding customer: " + ex.ToString());
                return false;
            }
        }

        public bool UpdateCustomer(int id, string firstName, string middleName, string lastName, string gender, int age, DateTime birthday,
            string contactNumber, string email)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT id
                                    FROM genders
                                    WHERE gender = @gender;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@gender", gender);

                        connection.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        dt.Clear();
                        da.Fill(dt);

                        val.GenderId = dt.Rows[0].Field<int>("id");
                    }

                    sql = @"UPDATE customers
                                SET firstName = @firstName, middleName = @middleName, lastName = @lastName, genderFID = @genderFID, age = @age,
                                birthday = @birthday, contactNumber = @contactNumber, email = @email, updatedAt = CURRENT_TIMESTAMP
                                WHERE id = @id;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@middleName", middleName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@genderFID", val.GenderId);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@birthday", birthday);
                        cmd.Parameters.AddWithValue("@contactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@email", email);

                        MySqlDataReader dr = cmd.ExecuteReader();
                        dr.Close();

                        connection.Close();

                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error updating customer: " + ex.ToString());
                return false;
            }
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"DELETE FROM customers
                                    WHERE id = @id;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        connection.Open();

                        MySqlDataReader dr = cmd.ExecuteReader();
                        dr.Close();

                        connection.Close();

                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Errorr deleting customer: " + ex.ToString());
                return false;
            }
        }
    }
}

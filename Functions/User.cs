﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace CRUD_Assignment.Functions
{
    class User
    {
        Components.Connection con = new Components.Connection();
        Components.Value val = new Components.Value();

        public void LoadUsers(DataGridView grid)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT u.id, u.profilePicture, u.firstName, u.middleName, u.lastName, g.gender, u.age,
                                    DATE_FORMAT(u.birthday, '%m/%d/%Y'), u.contactNumber, u.email,
                                    DATE_FORMAT(u.createdAt, '%m/%d/%Y'), DATE_FORMAT(u.updatedAt, '%m/%d/%Y')
                                    FROM users AS u
                                    INNER JOIN genders AS g ON u.genderFID = g.id
                                    ORDER BY u.lastName, u.firstName;";

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
                        grid.Columns["profilePicture"].HeaderText = "Profile Picture";
                        grid.Columns["firstName"].HeaderText = "First Name";
                        grid.Columns["middleName"].HeaderText = "Middle Name";
                        grid.Columns["lastName"].HeaderText = "Last Name";
                        grid.Columns["gender"].HeaderText = "Gender";
                        grid.Columns["age"].HeaderText = "Age";
                        grid.Columns["DATE_FORMAT(u.birthday, '%m/%d/%Y')"].HeaderText = "Birthday";
                        grid.Columns["contactNumber"].HeaderText = "Contact Number";
                        grid.Columns["email"].HeaderText = "Email";
                        grid.Columns["DATE_FORMAT(u.createdAt, '%m/%d/%Y')"].HeaderText = "Created At";
                        grid.Columns["DATE_FORMAT(u.updatedAt, '%m/%d/%Y')"].HeaderText = "UpdatedAt";

                        DataGridViewImageColumn clmProfilePicture = new DataGridViewImageColumn();
                        clmProfilePicture = (DataGridViewImageColumn)grid.Columns["profilePicture"];
                        clmProfilePicture.ImageLayout = DataGridViewImageCellLayout.Stretch;

                        connection.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error loading users: " + ex.ToString());
            }
        }

        public bool GetUser(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"SELECT u.id, u.profilePicture, u.firstName, u.middleName, u.lastName, g.gender, u.age,
                                    u.birthday, u.contactNumber, u.email, u.username
                                    FROM users AS u
                                    INNER JOIN genders AS g ON u.genderFID = g.id
                                    WHERE u.id = @id;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        connection.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        dt.Clear();
                        da.Fill(dt);

                        if(dt.Rows.Count > 0)
                        {
                            val.UserId = dt.Rows[0].Field<int>("id");
                            val.UserProfilePicture = dt.Rows[0].Field<byte[]>("profilePicture");
                            val.UserFirstName = dt.Rows[0].Field<string>("firstName");
                            val.UserMiddleName = dt.Rows[0].Field<string>("middleName");
                            val.UserLastName = dt.Rows[0].Field<string>("lastName");
                            val.UserGender = dt.Rows[0].Field<string>("gender");
                            val.UserAge = dt.Rows[0].Field<int>("age");
                            val.UserBirthday = dt.Rows[0].Field<DateTime>("birthday");
                            val.UserContactNumber = dt.Rows[0].Field<string>("contactNumber");
                            val.UserEmail = dt.Rows[0].Field<string>("email");
                            val.UserUsername = dt.Rows[0].Field<string>("username");

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
            catch (Exception ex)
            {
                Console.WriteLine("Error getting user: " + ex.ToString());
                return false;
            }
        }

        public bool AddUser(byte[] profilePicture, string firstName, string middleName, string lastName, string gender, int age, DateTime birthday,
            string contactNumber, string email, string username, string password)
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

                    sql = @"INSERT INTO users(profilePicture, firstName, middleName, lastName, genderFID, age, birthday, contactNumber, email, username, password)
                                VALUES(@profilePicture, @firstName, @middleName, @lastName, @genderFID, @age, @birthday, @contactNumber, @email, @username, MD5(@password));";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@profilePicture", profilePicture);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@middleName", middleName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@genderFID", val.GenderId);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@birthday", birthday);
                        cmd.Parameters.AddWithValue("@contactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        MySqlDataReader dr = cmd.ExecuteReader();
                        dr.Close();

                        connection.Close();

                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error adding user: " + ex.ToString());
                return false;
            }
        }

        public bool UpdateUser(int id, byte[] profilePicture, string firstName, string middleName, string lastName, string gender, int age, DateTime birthday,
            string contactNumber, string email, string username)
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

                    sql = @"UPDATE users
                                SET profilePicture = @profilePicture, firstName = @firstName, middleName = @middleName, lastName = @lastName, genderFID = @genderFID, age = @age,
                                birthday = @birthday, contactNumber = @contactNumber, email = @email, username = @username, updatedAt = CURRENT_TIMESTAMP
                                WHERE id = @id;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@profilePicture", profilePicture);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@middleName", middleName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@genderFID", val.GenderId);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@birthday", birthday);
                        cmd.Parameters.AddWithValue("@contactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@username", username);

                        MySqlDataReader dr = cmd.ExecuteReader();
                        dr.Close();

                        connection.Close();

                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error updating user: " + ex.ToString());
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.conString()))
                {
                    string sql = @"DELETE FROM users
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
                Console.WriteLine("Error deleting user: " + ex.ToString());
                return false;
            }
        }
    }
}

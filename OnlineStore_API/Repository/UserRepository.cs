using Microsoft.Extensions.Configuration;
using OnlineStore_API.Models;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace OnlineStore_API.Repository
{
    public class UserRepository
    {
        //private IConfiguration Configuration;
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public UserRepository()
        {
            builder.DataSource = ".";
            builder.InitialCatalog = "rOhit";
            builder.IntegratedSecurity = true;
        }
        //public UserRepository(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //Configuration.GetConnectionString("DefaultConnection")
        //}

        public User AuthenticateUser(User user)
        {           

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[Authentication]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    User UserDetails = new User();
                    try
                    {
                        cmd.Parameters.AddWithValue("@Email", user.Email);

                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            UserDetails.UserId = reader["UserId"].ToString();
                            UserDetails.Email = reader["Email"].ToString();
                            UserDetails.Password = reader["Password"].ToString();
                            UserDetails.Role = reader["Role"].ToString();
                        }                        
                        
                        if(String.Compare(Encryption.MD5Hash(user.Password), UserDetails.Password) == 0)
                        {
                            return UserDetails;
                        }
                        else
                        {
                            return new User();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
        public string AddUser(User user)
        {
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[UserRegistration]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    try
                    {
                        if(user.DOB == new DateTime(0001, 01, 01))
                        {
                            user.DOB = new DateTime(1900,01,01);
                        }
                        
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", Encryption.MD5Hash(user.Password));
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@DOB", user.DOB);
                        //cmd.Parameters.AddWithValue("@Gender", user.Gender);
                        //cmd.Parameters.AddWithValue("@Address", user.Address);
                        //cmd.Parameters.AddWithValue("@Contact", user.Contact);
                        cmd.Parameters.AddWithValue("@Role", user.Role);

                        con.Open();
                        int count = cmd.ExecuteNonQuery();

                        if (count > 0)
                        {
                            return "success";
                        }
                        else
                        {
                            return "failed";
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public List<User> GetUserDetails()
        {
            List<User> users = new List<User>();

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[GetUserDetails]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            User user = new User();
                            user.UserId = reader["UserId"].ToString();
                            user.Email = reader["Email"].ToString();
                            user.Password = reader["Password"].ToString();
                            user.Name = reader["Name"].ToString();
                            user.Role = reader["Role"].ToString();

                            users.Add(user);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return users;
        }

        public User GetUserDetailsById(string userId)
        {

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[GetUserDetailsById]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    User UserDetails = new User();
                    try
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            UserDetails.UserId = reader["UserId"].ToString();
                            UserDetails.Email = reader["Email"].ToString();
                            UserDetails.Name = reader["Name"].ToString();
                            UserDetails.Password = reader["Password"].ToString();
                            UserDetails.Role = reader["Role"].ToString();
                        }

                        return UserDetails;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public User GetUserDetailsByEmail(string email)
        {

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[GetUserDetailsByEmail]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    User UserDetails = new User();
                    try
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            UserDetails.UserId = reader["UserId"].ToString();
                            UserDetails.Email = reader["Email"].ToString();
                            UserDetails.Name = reader["Name"].ToString();
                            UserDetails.Password = reader["Password"].ToString();
                            UserDetails.DOB = DateTime.Parse(reader["DOB"].ToString());
                            UserDetails.Role = reader["Role"].ToString();
                        }

                        return UserDetails;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
        public void DeleteUser(string userId)
        {

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[DeleteUser]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    User UserDetails = new User();
                    try
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public void UpdateUser(string userId, User user)
        {

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[UpdateUser]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        user.Password = Encryption.MD5Hash(user.Password);

                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public void ForgotPassword(User user)
        {
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[ForgotPassword]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        user.Password = Encryption.MD5Hash(user.Password);

                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        
    }
}

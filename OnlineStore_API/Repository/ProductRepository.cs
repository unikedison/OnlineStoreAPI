using OnlineStore_API.Models;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace OnlineStore_API.Repository
{
    public class ProductRepository
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public ProductRepository()
        {
            builder.DataSource = ".";
            builder.InitialCatalog = "rOhit";
            builder.IntegratedSecurity = true;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[GetProductDetails]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        con.Open();
                        SqlDataReader reader  = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Product product = new Product();

                            product.ProductNo = reader["ProductNo"].ToString();
                            product.Brand = reader["Brand"].ToString();
                            product.Title = reader["Title"].ToString();
                            product.Price = Convert.ToDouble(reader["Price"].ToString());
                            product.Description = reader["Description"].ToString();
                            product.Category = reader["Category"].ToString();
                            product.ImageUrl = reader["ImageUrl"].ToString();

                            products.Add(product);
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
            return products;
        }

        public Product GetProductDetailsById(string productNo)
        {

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[GetProductDetailsById]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    Product product = new Product();
                    try
                    {
                        cmd.Parameters.AddWithValue("@ProductNo", productNo);

                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            product.ProductNo = reader["ProductNo"].ToString();
                            product.Brand = reader["Brand"].ToString();
                            product.Title = reader["Title"].ToString();
                            product.Price = Convert.ToDouble(reader["Price"].ToString());
                            product.Description = reader["Description"].ToString();
                            product.Category = reader["Category"].ToString();
                            product.ImageUrl = reader["ImageUrl"].ToString();
                        }

                        return product;
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
        public string AddProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[CreateProduct]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {                        
                        cmd.Parameters.AddWithValue("@Brand", product.Brand);
                        cmd.Parameters.AddWithValue("@Title", product.Title);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@Category", product.Category); 
                        cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);

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

        public string UpdateProduct(string productNo, Product product)
        {
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[UpdateProduct]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cmd.Parameters.AddWithValue("@ProductNo", productNo);
                        cmd.Parameters.AddWithValue("@Brand", product.Brand);
                        cmd.Parameters.AddWithValue("@Title", product.Title);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@Category", product.Category);
                        cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);

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

        public string DeleteProduct(string productNo)
        {
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Shopify.[DeleteProduct]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cmd.Parameters.AddWithValue("@ProductNo", productNo);

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
    }
}

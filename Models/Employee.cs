using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMVCApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        public static void AddNewEmployee(Employee obj)
        {
            using (SqlConnection cn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My_Employees_Database;Integrated Security=True"))
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand
                {
                    Connection = cn,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "INSERT INTO Employees VALUES (@Name, @City, @Address)"
                };

                cmdInsert.Parameters.AddWithValue("@Name", obj.Name);
                cmdInsert.Parameters.AddWithValue("@City", obj.City);
                cmdInsert.Parameters.AddWithValue("@Address", obj.Address);
                cmdInsert.ExecuteNonQuery();
            }
        }

        public static void UpdateEmployee(Employee obj)
        {
            using (SqlConnection cn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My_Employees_Database;Integrated Security=True"))
            {
                cn.Open();
                SqlCommand cmdUpdate = new SqlCommand
                {
                    Connection = cn,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "UPDATE Employees SET Name = @Name, City = @City, Address = @Address WHERE Id = @Id"
                };

                cmdUpdate.Parameters.AddWithValue("@Id", obj.Id);
                cmdUpdate.Parameters.AddWithValue("@Name", obj.Name);
                cmdUpdate.Parameters.AddWithValue("@City", obj.City);
                cmdUpdate.Parameters.AddWithValue("@Address", obj.Address);
                cmdUpdate.ExecuteNonQuery();
            }
        }

        public static void DeleteEmployee(int Id)
        {
            using (SqlConnection cn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My_Employees_Database;Integrated Security=True"))
            {
                cn.Open();
                SqlCommand cmdDelete = new SqlCommand
                {
                    Connection = cn,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "DELETE FROM Employees WHERE Id = @Id"
                };

                cmdDelete.Parameters.AddWithValue("@Id", Id);
                cmdDelete.ExecuteNonQuery();
            }
        }

        public static Employee GetSingleEmployee(int Id)
        {
            Employee obj = new Employee();
            using (SqlConnection cn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My_Employees_Database;Integrated Security=True"))
            {
                cn.Open();
                SqlCommand cmdSelect = new SqlCommand
                {
                    Connection = cn,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "SELECT * FROM Employees WHERE Id = @Id"
                };

                cmdSelect.Parameters.AddWithValue("@Id", Id);
                SqlDataReader dr = cmdSelect.ExecuteReader();
                if (dr.Read())
                {
                    obj.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    obj.Name = dr.GetString(dr.GetOrdinal("Name"));
                    obj.City = dr.GetString(dr.GetOrdinal("City"));
                    obj.Address = dr.GetString(dr.GetOrdinal("Address"));
                }
                else
                {
                    obj = null;
                }
                dr.Close();
            }
            return obj;
        }

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> lstEmps = new List<Employee>();
            using (SqlConnection cn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My_Employees_Database;Integrated Security=True"))
            {
                cn.Open();
                SqlCommand cmdSelectAll = new SqlCommand
                {
                    Connection = cn,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "SELECT * FROM Employees"
                };

                SqlDataReader dr = cmdSelectAll.ExecuteReader();
                while (dr.Read())
                {
                    lstEmps.Add(new Employee
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        Name = dr.GetString(dr.GetOrdinal("Name")),
                        City = dr.GetString(dr.GetOrdinal("City")),
                        Address = dr.GetString(dr.GetOrdinal("Address"))
                    });
                }
                dr.Close();
            }
            return lstEmps;
        }
    }
}

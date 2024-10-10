using HospitalDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace HospitalDAL
{
    public class DoctorDataAccess
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalManagementSystem;Integrated Security=True";
        public void InsertDoctor(Doctor doctor)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "INSERT INTO Doctors (Name, Specialization) VALUES (@Name, @Specialization)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name",doctor.Name);
                cmd.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Doctor inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Insertion!");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public List<Doctor> GetAllDoctorsFromDataBase()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Doctor> doctors = new List<Doctor>();
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "SELECT * FROM Doctors";
                cmd = new SqlCommand(query, conn);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    doctors.Add(new Doctor{DoctorId = reader.GetInt32(0), Name = reader.GetString(1),Specialization = reader.GetString(2)});
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Listing!");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
            return doctors;
        }

        public void UpdateDoctorInDatabase(Doctor doctor)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "UPDATE Doctors SET Name = @Name, Specialization = @Specialization WHERE Id = @Id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", doctor.Name);
                cmd.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                cmd.Parameters.AddWithValue("@Id", doctor.DoctorId);
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                if (noOfRowsAffected > 0)
                {
                    Console.WriteLine("Doctor updated successfully.");
                }
                else
                {
                    Console.WriteLine("Doctor not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Updation!");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public void DeleteDoctorFromDatabase(int doctorId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "DELETE FROM Doctors WHERE Id = @Id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", doctorId);
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                if (noOfRowsAffected > 0)
                {
                    Console.WriteLine("Doctor deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Doctor not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Deletion!");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public List<Doctor> SearchDoctorsInDatabase(string specialization)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Doctor> doctors = new List<Doctor>();
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "SELECT * FROM Doctors WHERE Specialization = @Specialization";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Specialization",specialization);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    doctors.Add(new Doctor { DoctorId = reader.GetInt32(0), Name = reader.GetString(1), Specialization = reader.GetString(2) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Searching Doctors!");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
            return doctors;
        }
        public Doctor searchDoctorById(int doctorId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            Doctor doctor = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "SELECT * FROM Doctors WHERE Id = @doctorId";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("doctorId", doctorId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    doctor = new Doctor { DoctorId = reader.GetInt32(0), Name = reader.GetString(1), Specialization = reader.GetString(2) };
                }
                else
                {
                    Console.WriteLine("Doctor not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while searching for doctor!");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
            return doctor;
        }
    }
}

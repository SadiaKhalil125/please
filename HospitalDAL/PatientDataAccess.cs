using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace HospitalDAL
{
    public class PatientDataAccess
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalManagementSystem;Integrated Security=True";
        public void InsertPatient(Patient patient)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "INSERT INTO Patients (Name, Email, Disease) VALUES (@Name, @Email, @Disease)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", patient.Name);
                cmd.Parameters.AddWithValue("@Email", patient.Email);
                cmd.Parameters.AddWithValue("@Disease", patient.Disease);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Patient inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Insertion! ");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public List<Patient> GetAllPatientsFromDataBase()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Patient> patients = new List<Patient>();
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "SELECT * FROM Patients";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patients.Add(new Patient { PatientId = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Disease = reader.GetString(3) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Listing! ");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
            return patients;
        }
        public void UpdatePatientInDatabase(Patient patient)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "UPDATE Patients SET Name = @Name , Email = @Email, Disease = @Disease WHERE Id = @PatientId";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", patient.Name);
                cmd.Parameters.AddWithValue("@Email", patient.Email);
                cmd.Parameters.AddWithValue("@Disease", patient.Disease);
                cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                if (noOfRowsAffected > 0)
                {
                    Console.WriteLine("Patient updated successfully.");
                }
                else
                {
                    Console.WriteLine("Patient not found.");
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
        public void DeletePatientFromDatabase(int patientId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "DELETE FROM Patients WHERE Id = @Id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", patientId);
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                if (noOfRowsAffected > 0)
                {
                    Console.WriteLine("Patient deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Patient not found.");
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
        public List<Patient> SearchPatientsInDatabase(string Name)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Patient> patients = new List<Patient>();
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "SELECT * FROM Patients WHERE Name = @Name";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", Name);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patients.Add(new Patient { PatientId = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Disease = reader.GetString(3) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Searching patients ");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
            return patients;
        }
        public Patient SearchPatientById(int patientId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            Patient patient = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string query = $"SELECT * FROM Patients WHERE Id = @Id";
                
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", patientId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    patient = new Patient { PatientId = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Disease = reader.GetString(3) };
                }
                else
                {
                    Console.WriteLine("Patient not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while searching for patient! ");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
            return patient;
        }
    }
}

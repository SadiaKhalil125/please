using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;
using System.Drawing;
using System.Runtime.Remoting.Contexts;
using System.Text.Json;
namespace HospitalDAL
{
    public class AppointmentDataAccess
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalManagementSystem;Integrated Security=True";
        public void InsertAppointment(Appointment appointment)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate) VALUES  (@PatientId,@DoctorId,@AppointmentDate)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                cmd.Parameters.AddWithValue("@AppointmentDate",appointment.AppointmentDate);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Appointment inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Insertion ");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public List<Appointment> GetAllAppointmentsFromDataBase()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Appointment> appointments = new List<Appointment>();

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "SELECT * FROM Appointments";
                cmd = new SqlCommand(query, conn);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Appointment appointment = new Appointment{ AppointmentId = reader.GetInt32(0), PatientId = reader.GetInt32(1),DoctorId = reader.GetInt32(2),AppointmentDate = reader.GetDateTime(3) };
                    appointments.Add(appointment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured! ");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }

            return appointments;
        }
        public void UpdateAppointmentInDatabase(Appointment appointment)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "UPDATE Appointments SET PatientId = @PatientId, DoctorId = @DoctorId, AppointmentDate = @AppointmentDate WHERE Id = @Id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                cmd.Parameters.AddWithValue("@Id", appointment.AppointmentId);
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                if (noOfRowsAffected > 0)
                {
                    Console.WriteLine("Appointment updated successfully.");
                }
                else
                {
                    Console.WriteLine("Appointment not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Updation! ");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public void DeleteAppointmentFromDatabase(int appointmentId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "DELETE FROM Appointments WHERE Id = @Id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id",appointmentId);
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                if (noOfRowsAffected > 0)
                {
                    Console.WriteLine("Appointment cancelled successfully.");
                }
                else
                {
                    Console.WriteLine("Appointment not found.");
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
        public List<Appointment> SearchAppointmentsInDatabase(int doctorId, int patientId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = "SELECT * FROM Appointments WHERE PatientId = @PatientId AND DoctorId = @DoctorId"; 
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                cmd.Parameters.AddWithValue("@DoctorId",doctorId);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    appointments.Add(new Appointment{ AppointmentId = reader.GetInt32(0), PatientId = reader.GetInt32(1), DoctorId = reader.GetInt32(2), AppointmentDate = reader.GetDateTime(3) });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error In Finding Appointments");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
            return appointments;
        }
        public Appointment SearchAppointmentById(int Id)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            Appointment appointment = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string query = $"SELECT * FROM Appointments WHERE Id = {Id}";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    appointment = new Appointment { AppointmentId = reader.GetInt32(0), PatientId = reader.GetInt32(1), DoctorId = reader.GetInt32(2), AppointmentDate = reader.GetDateTime(3) };
                }
                else
                {
                    Console.WriteLine("Appointment not found for the given Doctor and Patient ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while searching for appointment!");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }

            return appointment;
        }
    }
}

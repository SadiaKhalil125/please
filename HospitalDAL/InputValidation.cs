using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalDAL
{
    public class InputValidation
    {
        public void ValidateDoctorData(Doctor doctor)
        {
            while ((doctor.Name == null) || (doctor.Name == ""))
            {
                Console.WriteLine("Doctor name is required! Enter again: ");
                doctor.Name = Console.ReadLine();
            }
            while ((doctor.Specialization == null) || (doctor.Specialization == ""))
            {
                Console.WriteLine("Doctor specialization is required! Enter again: ");
                doctor.Specialization = Console.ReadLine();
            }
        }
        private static bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@gmail\.com$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        public void ValidatePatientData(Patient patient)
        {
            while ((patient.Name == null) || (patient.Name == ""))
            {
                Console.WriteLine("Patient Name is required! Enter again: ");
                patient.Name = Console.ReadLine();
            }
            while ((patient.Email == null) || (patient.Email == ""))
            {
                Console.WriteLine("Email is required! Enter again: ");
                patient.Email = Console.ReadLine();
            }
            while (!IsValidEmail(patient.Email))
            {
                Console.WriteLine("Invalid email format! Enter again: ");
                patient.Email = Console.ReadLine();
            }
        }
        private bool IsSlotTaken(int doctorId, int patientId, DateTime appointmentDate)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalManagementSystem;Integrated Security=True");
                conn.Open();

                string query = "SELECT COUNT(*) FROM Appointments WHERE DoctorId = @doctorId AND PatientId = @patientId AND AppointmentDate = @appointmentDate";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@doctorId", doctorId);
                cmd.Parameters.AddWithValue("@patientId", patientId);
                cmd.Parameters.AddWithValue("@appointmentDate", appointmentDate);
                object count = cmd.ExecuteScalar();
                return (int)count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! ");
                return false;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (conn != null)
                    conn.Close();
            }
        }
        public void validateAppointmentDate(Appointment appointment)
        {
            while (appointment.AppointmentDate <= DateTime.Now)
            {
                Console.WriteLine("Appointment date must be in the future! Enter again: ");
                appointment.AppointmentDate = DateTime.Parse(Console.ReadLine());
            }
            while (IsSlotTaken(appointment.DoctorId, appointment.PatientId, appointment.AppointmentDate))
            {
                Console.WriteLine("The selected appointment slot is already taken. Enter again: ");
                appointment.AppointmentDate = DateTime.Parse(Console.ReadLine());
            }
        }
        public void validateAppointmentDateForUpdation(Appointment appointment)
        {
            while (appointment.AppointmentDate <= DateTime.Now)
            {
                Console.WriteLine("Appointment date must be in the future! Enter again: ");
                appointment.AppointmentDate = DateTime.Parse(Console.ReadLine());
            }
        }
    }
}

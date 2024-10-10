using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;
using System.Numerics;
namespace HospitalDAL
{
    public class DeletedPatient
    {
        private Patient patient;
        private DateTime deletionTime;
        public Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }
       public DateTime DeletionTime
        {
            get { return deletionTime; }
            set { deletionTime = value; }
        }
        public override string ToString()
        {
            return $"PatientId: {patient.PatientId}, PatientName: {patient.Name}, PatientEmail: {patient.Email}, PatientDisease: {patient.Disease}, DeletionTime: {deletionTime}";
        }
    }
    public class DeletedDoctor
    {
        private Doctor doctor;
        private DateTime deletionTime;
        
        public Doctor Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }
        public DateTime DeletionTime
        {
            get { return deletionTime; }
            set { deletionTime = value; }
        }

        public override string ToString()
        {
            return $"DoctorId: {doctor.DoctorId}, DoctorName: {doctor.Name}, Specialization: {doctor.Specialization}, DeletionTime: {deletionTime}";
        }
    }
    public class DeletedAppointment
    { 
        private Appointment appointment;
        private DateTime deletionTime;
        public Appointment Appointment
        {
            get { return appointment; }
            set { appointment = value; }
        }

        public DateTime DeletionTime
        {
            get { return deletionTime; }
            set { deletionTime = value; }
        }
        public override string ToString()
        {
            return $"AppointmentId: {appointment.AppointmentId}, DoctorId: {appointment.DoctorId}, PatientId: {appointment.PatientId}, AppointmentDate: {appointment.AppointmentDate}, DeletionTime: {deletionTime}";
        }
    }

    public class HistoryLogger
    {
        public void AddDeletedPatientRecordInHistory(DeletedPatient patient)
        {
            FileStream fin = new FileStream("Patients.txt", FileMode.Append);
            StreamWriter writer = new StreamWriter(fin);
            try
            {
                string jsonFormatObject = JsonSerializer.Serialize(patient);
                writer.WriteLine(jsonFormatObject);
                Console.WriteLine("STORED IN HISTORY");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR IN LOGGING!");
            }
            finally
            {
                writer.Close();
                fin.Close();
            }
        }
        public void AddDeletedDoctorRecordInHistory(DeletedDoctor doctor)
        {
            FileStream fin = new FileStream("Doctors.txt", FileMode.Append);
            StreamWriter writer = new StreamWriter(fin);
            try
            {
                string jsonFormatObject = JsonSerializer.Serialize(doctor);
                writer.WriteLine(jsonFormatObject);
                Console.WriteLine("STORED IN HISTORY!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR IN LOGGING!");
            }
            finally
            {
                writer.Close();
                fin.Close();
            }
        }
        public void AddDeletedApointmentRecordInHistory(DeletedAppointment appointment)
        {
            FileStream fin = new FileStream("Appointments.txt", FileMode.Append);
            StreamWriter writer = new StreamWriter(fin);
            try
            {
                string jsonFormatObject = JsonSerializer.Serialize(appointment);
                writer.WriteLine(jsonFormatObject);
                Console.WriteLine("STORED IN HISTORY!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR IN LOGGING!");
            }
            finally
            {      
                writer.Close();
                fin.Close();
            }
        }
        public void ShowDeletedDoctors()
        {
            FileStream fin = new FileStream("Doctors.txt", FileMode.Open);
            StreamReader reader = new StreamReader(fin);
            string oneInstance = reader.ReadLine();
            while (oneInstance != null)
            {
                Console.WriteLine(oneInstance);
                oneInstance = reader.ReadLine();
            }
            reader.Close();
            fin.Close();
        }
        public void ShowDeletedPatients()
        {
            FileStream fin = new FileStream("Patients.txt", FileMode.Open);
            StreamReader reader = new StreamReader(fin);
            string oneInstance = reader.ReadLine();
            while (oneInstance != null)
            {
                Console.WriteLine(oneInstance);
                oneInstance = reader.ReadLine();
            }
            reader.Close();
            fin.Close ();
        }
        public void ShowDeletedAppointments()
        {
            FileStream fin = new FileStream("Appointments.txt", FileMode.Open);
            StreamReader reader = new StreamReader(fin);
            string oneInstance = reader.ReadLine();
            while (oneInstance != null)
            {
                Console.WriteLine(oneInstance);
                oneInstance = reader.ReadLine();
            }
            reader.Close();
            fin.Close();
        }
    }
}

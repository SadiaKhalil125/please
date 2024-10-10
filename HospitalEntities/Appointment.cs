using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL
{
    public class Appointment
    {
        private int appointmentId;
        private int patientId;
        private int doctorId;
        private DateTime appointmentDate;
        public int AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; }
        }
        public int PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }
        public DateTime AppointmentDate
        {
            get { return appointmentDate; }
            set { appointmentDate = value; }
        }
        public override string ToString()
        {
            return $"AppointmentId: {appointmentId}, PatientId: {patientId}, DoctorId: {doctorId}, AppointmentDate: {appointmentDate}";
        }
    }
}

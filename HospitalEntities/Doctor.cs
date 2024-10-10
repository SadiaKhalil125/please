using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL
{
    public class Doctor
    {
        private int doctorId;
        private string name;
        private string specialization;
        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }
        public override string ToString()
        {
            return $"DoctorId: {doctorId}, DoctorName: {name}, Specialization: {specialization}";
        }
    }
}

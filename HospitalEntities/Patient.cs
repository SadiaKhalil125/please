using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL
{
    public class Patient
    {
        private int patientId;
        private string name;
        private string email;
        private string disease;

        public int PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Disease
        {
            get { return disease; }
            set { disease = value; }
        }
        public override string ToString()
        {
            return $"Patient Id: {patientId}, PatientName: {name}, Email: {email}, Disease: {disease}";
        }
    }
}

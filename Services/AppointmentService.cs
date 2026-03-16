using HealthAids.Models;
using HealthAids.Structures;
using System;

namespace HealthAids.Services
{
    public class AppointmentService
    {
        private readonly Structures.List<Appointment> _appointments;
        private int _nextId = 1;

        public AppointmentService()
        {
            _appointments = new Structures.List<Appointment>();
        }

        public Appointment Create(int clientId, string doctorName, string doctorSpecialty,
                                  string date, string time, string reason)
        {
            

            DateTime appointmentDate;
            if (DateTime.TryParse($"{date} {time}", out appointmentDate))
            {
                
            }
            else
            {
                appointmentDate = DateTime.Now.AddDays(1);
                
            }

            var appointment = new Appointment
            {
                Id = _nextId++,
                UserId = clientId,
                DoctorName = doctorName,
                DoctorSpecialty = doctorSpecialty,
                AppointmentDate = appointmentDate,
                Reason = reason,
                Status = "pending",
                CreatedAt = DateTime.Now
            };

            _appointments.Add(appointment);
            
            return appointment;
        }

        public System.Collections.Generic.List<Appointment> GetByUser(int userId)
        {
            var result = new System.Collections.Generic.List<Appointment>();

            for (int i = 0; i < _appointments.Count; i++)
            {
                var appointment = _appointments.GetAt(i); 
                if (appointment != null && appointment.UserId == userId)
                {
                    result.Add(appointment);
                }
            }

            return result;
        }

        public Appointment? GetById(int id)
        {
            for (int i = 0; i < _appointments.Count; i++)
            {
                var appointment = _appointments.GetAt(i);
                if (appointment != null && appointment.Id == id)
                    return appointment;
            }
            return null;
        }

        public bool Cancel(int id)
        {
            for (int i = 0; i < _appointments.Count; i++)
            {
                var appointment = _appointments.GetAt(i);
                if (appointment != null && appointment.Id == id)
                {
                    appointment.Status = "cancelled";
                    return true;
                }
            }
            return false;
        }

        public System.Collections.Generic.List<Appointment> GetUpcoming(int userId)
        {
            var result = new System.Collections.Generic.List<Appointment>();
            var now = DateTime.Now;

            for (int i = 0; i < _appointments.Count; i++)
            {
                var apt = _appointments.GetAt(i);
                if (apt != null && apt.UserId == userId &&
                    apt.Status == "pending" &&
                    apt.AppointmentDate >= now)
                {
                    result.Add(apt);
                }
            }

            // Ordenar manualmente (burbuja)
            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = 0; j < result.Count - 1 - i; j++)
                {
                    if (result[j].AppointmentDate > result[j + 1].AppointmentDate)
                    {
                        var temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }

            return result;
        }

        public System.Collections.Generic.List<Appointment> GetHistory(int userId)
        {
            var result = new System.Collections.Generic.List<Appointment>();
            var now = DateTime.Now;

            for (int i = 0; i < _appointments.Count; i++)
            {
                var apt = _appointments.GetAt(i);
                if (apt != null && apt.UserId == userId &&
                    (apt.Status != "pending" || apt.AppointmentDate < now))
                {
                    result.Add(apt);
                }
            }

            // Ordenar descendente (burbuja)
            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = 0; j < result.Count - 1 - i; j++)
                {
                    if (result[j].AppointmentDate < result[j + 1].AppointmentDate)
                    {
                        var temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }

            return result;
        }
    }
}
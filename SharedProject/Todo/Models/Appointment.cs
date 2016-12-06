using System;
using SQLite;

namespace TermineAudibene
{
	public class Appointment : DoctorDetails
	{
		
		public Appointment()
		{
		}

		public Appointment(string specialist, string notes, DateTime dateTime)
		{
			this.Specialist = specialist;
			this.Notes = notes;
			DateTime = dateTime;
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Specialist { get; set; }

		public string Notes { get; set; }

		public DateTime DateTime { get; set; }

		public bool Done { get; set; }
	}
}


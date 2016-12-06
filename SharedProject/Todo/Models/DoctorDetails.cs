using System;
using SQLite;
namespace TermineAudibene
{
	public class DoctorDetails
	{

		public DoctorDetails()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Name { get; set; }

		public string Address { get; set;}

		public string Availability { get; set; }

	}
}

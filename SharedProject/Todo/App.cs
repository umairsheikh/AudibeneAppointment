using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TermineAudibene
{
	public class App : Application
	{
		static AppointmentDatabse database;
		static SpecialistDatabase _specialistDatabase;

		public static DoctorDetails[] SpecialistsArray;

		public static AppointmentDatabse Database {
			get {
				database = database ?? new AppointmentDatabse ();
				return database;
			}
		}


		public static SpecialistDatabase specialistsDatabase
		{
			get
			{
				_specialistDatabase = specialistsDatabase ?? new SpecialistDatabase();
				return _specialistDatabase;
			}
		}


		public App ()
		{
			Resources = new ResourceDictionary ();
			Resources.Add ("primaryGreen", Color.FromHex("91CA47"));
			Resources.Add ("primaryDarkGreen", Color.FromHex ("6FA22E"));

			loadAppointmentDatabase();

			var nav = new NavigationPage (new AppointmentListPage ());
			nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
			nav.BarTextColor = Color.White;
			MainPage = nav;
		}


		protected override void OnStart()
		{
			// Handle when your app starts
			Debug.WriteLine("OnStart");
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			Debug.WriteLine("OnSleep");
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
			Debug.WriteLine("OnResume");
		}

		public void loadAppointmentDatabase()
		{
			// These commented-out lines were used to test the ListView prior to integrating the database
			var AppointmentArray = new Appointment[] {
				new Appointment {Specialist = "Dr. Norman",Notes=" Bring Insurance Card and tests", DateTime= new DateTime(2016,12,10)},
				new Appointment {Specialist = "Dr. Christopher",Notes="Bring Insurance Card and tests", DateTime= new DateTime(2016,12,12)},
				new Appointment {Specialist = "Dr. Who",Notes="Bring Insurance Card and tests", DateTime= new DateTime(2016,12,14)},
				new Appointment {Specialist = "Dr. Jasmin",Notes="Bring Insurance Card and tests", DateTime= new DateTime(2016,12,16)},
				new Appointment {Specialist = "Dr. Nadine",Notes="Bring Insurance Card and tests", DateTime= new DateTime(2016,12,19)}
			};
			loadSpecialistDatabase();
			if(Database.IsEmptyDB())
			foreach (Appointment item in AppointmentArray)
			{
				App.Database.SaveItem(item);
			}
		}

		public void loadSpecialistDatabase()
		{
			// These commented-out lines were used to test the ListView prior to integrating the database
			 SpecialistsArray = new DoctorDetails[] {
				new DoctorDetails {Name = "Dr. Norman",Address="Berliner Tor ", Availability= "Mon to Fri 13:00 to 19:00"},
				new DoctorDetails {Name = "Dr. Christopher",Address="MarienPlatz", Availability= "Mon to Fri 13:00 to 19:00"},
				new DoctorDetails {Name = "Dr. Who",Address="Albert Einstein Strasse", Availability= "Mon to Fri 13:00 to 19:00"},
				new DoctorDetails {Name = "Dr. Jasmin",Address="OdeonPlatz", Availability= "Mon to Fri 13:00 to 19:00"},
				new DoctorDetails {Name = "Dr. Nadine",Address="SchanzenStrasse", Availability= "Mon to Fri 13:00 to 19:00"}
			};

			/*foreach (DoctorDetails item in SpecialistsArray)
			{
				App.specialistsDatabase.SaveItem(item);
			}*/
		}

	}
}
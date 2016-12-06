using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TermineAudibene
{
	public class AppointmentItemPage : ContentPage
	{
		public AppointmentItemPage()
		{
			this.SetBinding(ContentPage.TitleProperty, "Specialist");

			NavigationPage.SetHasNavigationBar(this, true);

			var nameLabel = new Label { Text = "Specialist" };
			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "Specialist");


			var dateLabel = new Label { Text = "DateTime" };
			var dateEntry = new Entry();
			dateEntry.SetBinding(Entry.TextProperty, "DateTime");


			var notesLabel = new Label { Text = "Notes" };
			var notesEntry = new Entry();
			notesEntry.SetBinding(Entry.TextProperty, "Notes");

			var doneLabel = new Label { Text = "Done" };
			var doneEntry = new Xamarin.Forms.Switch();
			doneEntry.SetBinding(Xamarin.Forms.Switch.IsToggledProperty, "Done");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += (sender, e) =>
			{
				var appointmentItem = (Appointment)BindingContext;
				App.Database.SaveItem(appointmentItem);
				Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += (sender, e) =>
			{
				var todoItem = (Appointment)BindingContext;
				App.Database.DeleteItem(todoItem.ID);
				Navigation.PopAsync();
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) =>
			{
				var todoItem = (Appointment)BindingContext;
				Navigation.PopAsync();
			};


			var speakButton = new Button { Text = "Speak" };
			speakButton.Clicked += (sender, e) =>
			{
				var appointmentItem = (Appointment)BindingContext;
				DependencyService.Get<ITextToSpeech>().Speak(appointmentItem.Specialist + " " + appointmentItem.Notes);
			};

					
			//IOS Select Appointment and Navigate to Detail of Appointment

			ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.iOS)
			{
				tbi = new ToolbarItem("Detail", null, () =>
				{
					//var x = App.specialistsDatabase.GetItem(nameEntry.Text.ToString());

					//Debug.WriteLine(x);FirstOrDefault(x => x.ID == id);
					DoctorDetails SelectedDocItem= null;
					foreach (DoctorDetails item in App.SpecialistsArray)
					{
						if (item.Name == nameEntry.Text.ToString())
							SelectedDocItem = item;
					}
					var specialistPage = new SpecialistDetailPage();
					var specialistDetail = (DoctorDetails)BindingContext;
						specialistPage.BindingContext =  SelectedDocItem;
					Navigation.PushAsync(specialistPage);
				}, 0, 0);
			}
			if (Device.OS == TargetPlatform.Android)
			{ // BUG: Android doesn't support the icon being null
				tbi = new ToolbarItem("Detail", "Detail", () =>
				{
					var specialistItem = new DoctorDetails();
					var specialistDetailPage = new SpecialistDetailPage();
					specialistDetailPage.BindingContext = specialistItem;
					Navigation.PushAsync(specialistDetailPage);
				}, 0, 0);
			}
			if (Device.OS == TargetPlatform.WinPhone)
			{
				tbi = new ToolbarItem("Detail", "add.png", () =>
				{
					var specialistItem = new DoctorDetails();
					var specialistDetailPage = new SpecialistDetailPage();
					specialistDetailPage.BindingContext = specialistItem;
					Navigation.PushAsync(specialistDetailPage);
				}, 0, 0);
			}
			ToolbarItems.Add(tbi);


			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry,
					dateLabel,dateEntry,
					notesLabel, notesEntry,
					doneLabel, doneEntry,
					saveButton, deleteButton, cancelButton,
					speakButton
				}
			};
		}
	}
}
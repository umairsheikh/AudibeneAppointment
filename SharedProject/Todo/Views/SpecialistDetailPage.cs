using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TermineAudibene
{
	public class SpecialistDetailPage : ContentPage
	{
		Entry nameEntry;
		public SpecialistDetailPage()
		{
			this.SetBinding(ContentPage.TitleProperty, "DoctorDetails");



			NavigationPage.SetHasNavigationBar(this, true);

			var nameLabel = new Label { Text = "Name" };
			nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "Name");


		


			var addressLabel = new Label { Text = "Address" };
			var addressEntry = new Entry();
			addressEntry.SetBinding(Entry.TextProperty, "Address");
		

				var availabilityLabel = new Label { Text = "Avaialability" };
			var availabilityEntry = new Entry();
			availabilityEntry.SetBinding(Entry.TextProperty, "Availability");


			var speakButton = new Button { Text = "Speak" };
			speakButton.Clicked += (sender, e) =>
			{
				var DocDetail = (DoctorDetails)BindingContext;
				DependencyService.Get<ITextToSpeech>().Speak(DocDetail.Name + "with availability " + DocDetail.Availability);
			};



			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry,
					availabilityLabel,availabilityEntry,
					addressLabel, addressEntry,
					speakButton
				}
			};
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
		}
	}
}
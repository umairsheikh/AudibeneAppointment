using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TermineAudibene
{
	public class AppointmentListPage : ContentPage
	{
		ListView listView;

		public AppointmentListPage()
		{
			Title = "Appointment";

			NavigationPage.SetHasNavigationBar(this, true);

			listView = new ListView
			{
				RowHeight = 30,
				ItemTemplate = new DataTemplate(typeof(AppointmentCell))
			};




			listView.ItemSelected += (sender, e) =>
			{
				var appointmentItem = (Appointment)e.SelectedItem;
				var appointmentPage = new AppointmentItemPage();
				appointmentPage.BindingContext = appointmentItem;
				Navigation.PushAsync(appointmentPage);
			};

			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone)
			{ // WinPhone doesn't have the title showing
				layout.Children.Add(new Label
				{
					Text = "Appointment",
					FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
					FontAttributes = FontAttributes.Bold
				});
			}
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

			//IOS Select Appointment and Navigate to Detail of Appointment
			ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.iOS)
			{
				tbi = new ToolbarItem("+", null, () =>
				{
					var appointmentItem = new Appointment();
					var appointmentPage = new AppointmentItemPage();
					appointmentPage.BindingContext = appointmentItem;
					Navigation.PushAsync(appointmentPage);
				}, 0, 0);
			}
			if (Device.OS == TargetPlatform.Android)
			{ // BUG: Android doesn't support the icon being null
				tbi = new ToolbarItem("+", "plus", () =>
				{
					var appointmentItem = new Appointment();
					var appointmentPage = new AppointmentItemPage();
					appointmentPage.BindingContext = appointmentItem;
					Navigation.PushAsync(appointmentPage);
				}, 0, 0);
			}

			if (Device.OS == TargetPlatform.WinPhone)
			{
				tbi = new ToolbarItem("Add", "add.png", () =>
				{
					var appointmentItem = new Appointment();
					var appointmentPage = new AppointmentItemPage();
					appointmentPage.BindingContext = appointmentItem;
					Navigation.PushAsync(appointmentPage);
				}, 0, 0);
			}

			ToolbarItems.Add(tbi);


		}



		protected override void OnAppearing()
		{
			base.OnAppearing();
			listView.ItemsSource = App.Database.GetItems();
		}
	}
}


using System;
using Xamarin.Forms;

namespace TermineAudibene
{
	public class AppointmentCell : ViewCell
	{
		public AppointmentCell()
		{
			var label = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			label.SetBinding(Label.TextProperty, "Specialist");

			var label2 = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			label2.SetBinding(Label.TextProperty, "DateTime");



			var tick = new Image
			{
				Source = FileImageSource.FromFile("check.png"),
				HorizontalOptions = LayoutOptions.End
			};

			tick.SetBinding(Image.IsVisibleProperty, "Done");

			var layout = new StackLayout
			{
				Padding = new Thickness(20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { label2, label, tick }
			};

			View = layout;
		}
	}
}


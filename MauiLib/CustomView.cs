namespace MauiLib
{
    // All the code in this file is included in all platforms.
    public class CustomView : ContentView
    {

        public CustomView()
        {
            var grid = new Grid {
                Padding = new Thickness(10),
                RowSpacing = 10,
                Background = Colors.LightBlue
            };
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            var mauiButton = new Button { Text = "hello from Maui button" };
            var mauiButton2 = new Button { Text = "hello from button 2" };
            grid.Add(mauiButton, 0, 0);
            grid.Add(mauiButton2, 0, 1);

            mauiButton.Clicked += (s, e) => {
                mauiButton.Text = "clicked 1";
            };

            Content = grid;
        }
    }
}
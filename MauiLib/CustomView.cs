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
            grid.RowDefinitions.Add(new RowDefinition());
            var mauiButton = new Button { Text = "hello from Maui button" };
            var mauiButton2 = new Button { Text = "hello from button 2" };
            var collectionView = new CollectionView
            {
                ItemsSource = new List<string> { "Item 1", "Item 2", "Item 3" },
                //ItemTemplate = new DataTemplate(() =>
                //{
                //    var label = new Label();
                //    label.SetBinding(Label.TextProperty, ".");
                //    return label;
                //})
            };
            grid.Add(mauiButton, 0, 0);
            grid.Add(mauiButton2, 0, 1);
            grid.Add(collectionView, 0, 2);

            mauiButton.Clicked += (s, e) => {
                mauiButton.Text = "clicked 1";
            };

            Content = grid;
        }
    }
}
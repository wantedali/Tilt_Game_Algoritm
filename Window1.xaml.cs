using System.Windows;
using TiltGame;

namespace YourNamespace
{
    public partial class ExampleXamlWindow : Window
    {
        public ExampleXamlWindow()
        {
            InitializeComponent();
        }

        private void btnGUI_Click(object sender, RoutedEventArgs e)
        {

            MainWindow M1 = new MainWindow();
            M1.Show();  // Use Show for non-modal or ShowDialog for modal
        }

        private void btnBlackScreen_Click(object sender, RoutedEventArgs e)
        {
            // Perform actions for the Black Screen button
            // Example: Change background color to black

            Window2 M2 = new Window2();
            M2.Show();  // Use Show for non-modal or ShowDialog for modal
        }
    }
}
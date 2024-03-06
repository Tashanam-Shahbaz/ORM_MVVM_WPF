using ORM_MVVM_WPF.ViewModels;
using System.Windows;


namespace ORM_MVVM_WPF.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        private MainViewModel _mainViewModel;
        public AdminDashboard()
        {
            InitializeComponent();
            _mainViewModel  = new MainViewModel();
            //_loginVM = new LoginViewModel();
            DataContext = _mainViewModel;
        }

        private void AdminLogout_Click(object sender, RoutedEventArgs e)
        {
            //_loginVM.LogoutAction();
            
            // Open the main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Closing Admin Dashboard
            Close();
        }
    }
}

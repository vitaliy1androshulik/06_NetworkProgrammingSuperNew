using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string username;
        string password;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxUsername.Text == null)
            {
                MessageBox.Show("Enter username!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (TextBoxPassword.Text == null)
            {
                MessageBox.Show("Enter password!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                username = TextBoxUsername.Text;
                password = TextBoxPassword.Text;
                Login login = new Login(username, password);
                login.Show();
                this.Close();
            }
        }
    }
}
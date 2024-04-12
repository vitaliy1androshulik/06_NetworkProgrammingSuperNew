using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_MailSender
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string server = "smtp.gmail.com";
        int port = 587;

        string username;
        string password;
        MailMessage message;
        public Login(string username, string password)
        {
            InitializeComponent();

            this.username = username;
            this.password = password;
            TextBoxFromWho.Text = username;
            message = new MailMessage();
            message.Priority = MailPriority.Normal;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            message.From = new MailAddress(TextBoxFromWho.Text);
            message.To.Add(TextBoxToWho.Text);
            message.Subject = TextBoxTheme.Text;
            message.Body = TextBoxText.Text;

            SmtpClient client = new SmtpClient(server, port);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(username, password);

            client.SendCompleted += Client_SendCompleted;

            client.SendAsync(message, message);
            TextBoxText.Text = null;
        }

        private void Client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var state = (MailMessage)e.UserState;
            MessageBox.Show($"Message was sent! Subject: {state.Subject}!");
        }

        private void BtnNewLetter_Click(object sender, RoutedEventArgs e)
        {
            TextBoxTheme.Text = null;
            TextBoxText.Text = null;
            message.Attachments.Clear();
            ListBoxAttachFiles.Items.Clear();
        }

        private void BtnAttachFiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
                if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    ListBoxAttachFiles.Items.Add(fileDialog.FileName.Trim());
                    message.Attachments.Add(new Attachment(fileDialog.FileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CheckBoxPriority_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxPriority.IsChecked == true)
            {
                message.Priority = MailPriority.High;
            }
            else if (CheckBoxPriority.IsChecked == false)
            {
                message.Priority = MailPriority.Normal;
            }
        }
    }
}

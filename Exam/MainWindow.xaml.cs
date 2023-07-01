using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void DateTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && (e.Text != "."))
            {
                e.Handled = true;
            }
        }

        private void GenerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UserTb.Text))
                {
                    MessageBox.Show("Заполните имя", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(DateTb.Text))
                {
                    MessageBox.Show("Заполните дату рождения", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {

                    string username = UserTb.Text;
                    string date = DateTb.Text;
                    if (Regex.IsMatch(date, @"\d{2}.\d{2}.\d{4}"))
                    {
                        string login = GenerateLogin(username, date);
                        LoginTb.Text = login;
                    }
                    else
                    {
                        MessageBox.Show($"Проверьте {date} на корректность", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        DateTb.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateLogin(string username, string date)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in username.ToLower())
            {
                if (Char.IsLetter(c))
                {
                    int position = (int)c - (int)'a' + 1;
                    stringBuilder.Append(position);
                }
            }

            int sumDigits = 0;
            foreach (char c in date)
            {
                if (char.IsDigit(c))
                {
                    sumDigits += (int)Char.GetNumericValue(c);

                }
            }
            stringBuilder.Append(sumDigits);
            string login = stringBuilder.ToString();
            return login;
        }

        private void Gener2Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int length = 9;
                string password = GeneratePassword(length);
                PasswordTb.Text = password;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GeneratePassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // символы, которые будут использоваться в пароле
            const string specialChars = "-;+_=\"[-@#$%^&?**)(!]"; // специальные символы, которые должны быть включены

            StringBuilder passwordBuilder = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                passwordBuilder.Append(chars[random.Next(chars.Length)]);
            }

            // Добавляем случайно выбранный специальный символ
            passwordBuilder.Append(specialChars[random.Next(specialChars.Length)]);

            string password = passwordBuilder.ToString();
            return password;
        }
    }
}

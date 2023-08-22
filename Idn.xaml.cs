using System;
using System.Collections.Generic;
using System.Linq;
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

namespace UsersApp {
    /// <summary>
    /// Логика взаимодействия для Idn.xaml
    /// </summary>
    public partial class Idn: Window {
        public Idn() {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e) {
            var txt1 = txtLoginOrEmail.Text;
            var txt2 = txtPswrd.Password;
            if(App.MyApp.Db.isUser(txt1, txt2)) {
                MessageBox.Show("Идентификация прошла!");
            } else {
                MessageBox.Show("Логин(Email) пароль неверен...\nПопробуйте ещё...");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Hide();
            App.MyApp.Shutdown(0);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e) {
            //Hide();
            var w = new wndRegister();
            w.Show();
        }
    }
}

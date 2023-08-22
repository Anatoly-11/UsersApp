using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using Microsoft.Win32;
using System.Drawing;
using Brushes = System.Windows.Media.Brushes;
using System.Windows.Media.Imaging;

namespace UsersApp {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class wndRegister: Window {

        /// <summary>
        /// Флаги состояния валидации полей ввода
        /// </summary>
        [Flags]
        [Description("Состояния валидации полей ввода")]
        private enum ValidateState {
            [Description("Валидация пройдена")]
            OK = 0,
            [Description("Ошибка ввода логинга")]
            LoginErr = 1,
            [Description("Ошибка ввода пароля")]
            PasswErr = 2,
            [Description("Ошибка повтора ввода пароля")]
            PasswRepErr = 4,
            [Description("Ошибка ввода эл почты")]
            EmailErr =    8,
        };

        /// <summary>
        /// Флаги состояния вввода полей
        /// введено или нет
        /// </summary>
        [Flags]
        [Description("Состояния ввода полей")]
        private enum InputedState {
            [Description("Ничего не введено")]
            None = 0,
            [Description("Введён логин")]
            LoginInp = 1,
            [Description("Введён пароль")]
            PasswInp = 2,
            [Description("Введена эл почта")]
            EmailInp = 4,
            [Description("Все поля введены")]
            AllInp = LoginInp | PasswInp | EmailInp,
        };

        private ValidateState valid;
        private InputedState inputed;
        //private SQLiteCommand cmdTestLogin, cmdTestEmail;
        public wndRegister() {
            InitializeComponent();
            // Animation:
            valid = ValidateState.OK;
            inputed = InputedState.None;
            //btnShowPswd
    

        }

        protected override void OnClosed(EventArgs e) {
            //cmdTestLogin.Dispose(); cmdTestEmail.Dispose();
            base.OnClosed(e);
        }

        private void btnReg_Click(object sender, RoutedEventArgs e) {
            if(inputed != InputedState.AllInp || valid != ValidateState.OK) // Не всё ввели или есть ошибки
                return;
            App.MyApp.Db.Add(new User(){Login = txtLogin.Text, Pswd = MD5.toBytes(txtPswrd.Password), Email = txtEmail.Text });
        }
        private void TxtFieldText_Changed(object sender, RoutedEventArgs e) {
            if(sender is TextBox) {
                var ctl = sender as TextBox;
                ctl.ToolTip = "";
                ctl.Background = Brushes.Transparent;
                ctl.TextChanged -= TxtFieldText_Changed;
            } else if(sender is PasswordBox) {
                var ctl = sender as PasswordBox;
                ctl.ToolTip = "";
                ctl.Background = Brushes.Transparent;
                ctl.PasswordChanged -= TxtFieldText_Changed;
            }
        }

        private readonly SolidColorBrush errBckgr = Brushes.DarkRed;

        private void txtLogin_LostFocus(object sender, RoutedEventArgs e) {
            var ctl = (sender as TextBox); var txt = ctl.Text;
            var res = Valid_funcs.Login(txt);
            string pr = "логин", errStr = "";
            switch(res) {
                case Valid_funcs.LoginState.EMPTY:
                    errStr = "не мб пустым";
                    inputed &= ~InputedState.LoginInp;
                    break;
                case Valid_funcs.LoginState.BAD_SIZE:
                    errStr = "должен быть от 6 до 16 символов";
                    break;
                case Valid_funcs.LoginState.BAD_FIRST_LETTER:
                    errStr = "Первый символ дб ASCII буквой англ алфавита";
                    break;
                case Valid_funcs.LoginState.BAD_NEXT_LETTERS:
                    errStr = "Символы дб ASCII буквой, цифрой, '-' или '_'";
                    break;
            }
            if(errStr.Length > 0) {
                ctl.ToolTip = Valid_funcs.msgFmt(pr, errStr);
                ctl.Background = errBckgr;
                valid |= ValidateState.LoginErr;
                ctl.TextChanged += TxtFieldText_Changed;
            } else {
                if(App.MyApp.Db.isLog(txt)) {
                    errStr = $"Пользователь с таким логином {txt} уже существует";
                    ctl.ToolTip = Valid_funcs.msgFmt(pr, errStr);
                    ctl.Background = errBckgr;
                    valid |= ValidateState.LoginErr;
                    ctl.TextChanged += TxtFieldText_Changed;
                    return;
                }
                valid &= ~ValidateState.LoginErr;
                ctl.ToolTip = "";
                ctl.Background = Brushes.Transparent;
                inputed |= InputedState.LoginInp;
            }
        }

        private void txtPswrd_LostFocus(object sender, RoutedEventArgs e) {
            var ctl = (sender as PasswordBox); var txt = ctl.Password;
            var res = Valid_funcs.Pasword(txt);
            string pr = "пароль", errStr = "";
            switch(res){
                case Valid_funcs.PasswordState.EMPTY:
                    errStr = "не мб пуcтым";
                    inputed &= ~InputedState.PasswInp;
                    break;
                case Valid_funcs.PasswordState.BAD_SIZE:
                    errStr = "дб от 6 до 12 символов";
                    break;
                case Valid_funcs.PasswordState.BAD_FMT:
                    errStr = "не должно содержать пробельных символов";
                    break;
            }
            if(errStr.Length > 0) {
                ctl.ToolTip = Valid_funcs.msgFmt(pr, errStr);
                ctl.Background = errBckgr;
                valid |= ValidateState.PasswErr;
                ctl.PasswordChanged += TxtFieldText_Changed;
            } else {
                valid &= ~ValidateState.PasswErr;
                ctl.ToolTip = "";
                ctl.Background = Brushes.Transparent;
                inputed |= InputedState.PasswInp;
            }
        }

        private void txtRepPswrd_LostFocus(object sender, RoutedEventArgs e) {
            var ctl = (sender as PasswordBox);
            var txt = ctl.Password;
            string pr = "повтор пароля", errStr = "";
            if(txt.Length == 0) {
                errStr = "не мб пуcтым";
            } else if(txt != txtPswrd.Password) {
                pr = "поля паролей";
                errStr = "должны совпадать";
            }
            if(errStr.Length > 0) {
                ctl.ToolTip = Valid_funcs.msgFmt(pr, errStr);
                ctl.Background = errBckgr;
                valid |= ValidateState.PasswRepErr;
                ctl.PasswordChanged += TxtFieldText_Changed;
            } else {
                valid &= ~ValidateState.PasswRepErr;
                ctl.ToolTip = "";
                ctl.Background = Brushes.Transparent;
            }
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e) {
            var ctl = (sender as TextBox); var txt = ctl.Text;
            var res = Valid_funcs.Email(txt);
            string pr = "email", errStr = "";
            switch(res) {
                case Valid_funcs.EmailState.EMPTY:
                    errStr = "не мб пуcтым";
                    inputed &= ~InputedState.EmailInp;
                    break;
                case Valid_funcs.EmailState.BAD_NO_DOG:
                    errStr = "Нет символа @";
                    break;
                case Valid_funcs.EmailState.BAD_MANY_DOGS:
                    errStr = "Символ @ может быть только один";
                    break;
                case Valid_funcs.EmailState.BAD_NAME_EMPTY:
                    errStr = "пустое имя";
                    break;
                case Valid_funcs.EmailState.BAD_DOM_EMPTY:
                    errStr = "пустой домен";
                    break;
                case Valid_funcs.EmailState.BAD_NAME:
                    errStr = "имя дб ASCII буква в нижнем регистре, цифра, '_' или '-'";
                    break;
                case Valid_funcs.EmailState.BAD_DOM:
                    errStr = "домен дб ASCII буква в нижнем регистре, цифра, '_', '-' или '.'";
                    break;
                case Valid_funcs.EmailState.BAD_DOM_NO_DOT:
                    errStr = "домен не содержит '.'";
                    break;
            }
            if(errStr.Length > 0) {
                ctl.ToolTip = Valid_funcs.msgFmt(pr, errStr);
                ctl.Background = errBckgr;
                valid |= ValidateState.EmailErr;
                ctl.TextChanged += TxtFieldText_Changed;
            } else {
                if(App.MyApp.Db.isEmail(txt)) {
                    errStr = $"Пользователь с таким email {txt} уже существует";
                    ctl.ToolTip = Valid_funcs.msgFmt(pr, errStr);
                    ctl.Background = errBckgr;
                    valid |= ValidateState.LoginErr;
                    ctl.TextChanged += TxtFieldText_Changed;
                    return;
                }
                valid &= ~ValidateState.EmailErr;
                ctl.ToolTip = "";
                ctl.Background = Brushes.Transparent;
                inputed |= InputedState.EmailInp;
            }
        }

        private void imgPhoto_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
           var dlg = new OpenFileDialog();
            dlg.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            dlg.FilterIndex = 1;
            if(dlg.ShowDialog() == true){
                imgPhoto.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}

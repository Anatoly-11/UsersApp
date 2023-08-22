using System.Windows;
using System.Data.SQLite;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.Xml.Linq;

namespace UsersApp {
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App: Application {

        /// <summary>
        /// Статическая ссвлка на объект приложения
        /// </summary>
        [Description("Статическая ссвлка на объект приложения")]
        public static App MyApp { get; private set; }

        /// <summary>
        /// Ссылка на  БД
        /// </summary>
        [Description("Ссылка на БД")]
        public Database Db { get; private set; }

        /// <summary>
        /// Обработчик события запуска приложения
        /// </summary>
        /// <param name="sender">Отправитель события(Ссылка на приложение)</param>
        /// <param name="e">Аргумет события(содержит аррументы коммандной строки</param>
        private void Application_Startup(object sender, StartupEventArgs e) {
            MyApp = this; // Для удобства
            Db = new Database();
        }

        /// <summary>
        /// Cобытиe закрытия приложения
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e) {
            e.ApplicationExitCode = 0;
            Db.Dispose();
            Db = null;
            base.OnExit(e);
        }
    }
}

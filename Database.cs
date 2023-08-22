using System.Data.SQLite;
using System.Windows;
using System.Configuration;
using System.ComponentModel;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace UsersApp {
    /// <summary>
    ///  Класс базы данных
    /// </summary>
    [Description("Класс базы данных")]
    public class Database: IDisposable {
        /// <summary>
        /// Соединнеие с бд
        /// </summary>
        [Description("Соединнеие с бд")]
        public SQLiteConnection Connection { get; set; }
        /// <summary>
        /// Имя БД
        /// </summary>
        [Description("Имя БД")]
        public string DbName { get; set; }

        /// <summary>
        /// Добавление пользователя в БД
        /// </summary>
        /// <param name="usr">Класс модель</param>
        /// <returns>результат добавления</returns>
        public bool Add(User usr) {
            int res = 0;
            using(var cmd = new SQLiteCommand("insert into Users(Login, Pswd, Email) VALUES(:Login, :Pswd, :Email)", Connection)) {
                cmd.Parameters.AddWithValue("Login", usr.Login);
                cmd.Parameters.AddWithValue("Pswd",  usr.Pswd);
                cmd.Parameters.AddWithValue("Email", usr.Email);
                try {
                    res = cmd.ExecuteNonQuery();
                } catch(SQLiteException ex) {
                    MessageBox.Show($"Ошибка:\n {ex.Message}", "Результат", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return res > 0;
        }

        public bool isUser(string logOrEmail, string psw) {
            if(logOrEmail == "") return false;
            var spls = logOrEmail.Split('@');
            string cmdTxt = "";
            Int64 cnt = 0L; 
            if(spls.Length == 1) { // Логин
                cmdTxt = "select count(*) from Users where Login=:Idn AND  Pswd=:Pswd";
            } else if(spls.Length == 2) { // Email
                cmdTxt = "select count(*)  from Users where Email=:Idn AND  Pswd=:Pswd";
            } else
                return false;
            using(var cmd = new SQLiteCommand(cmdTxt, Connection)) {
                cmd.Parameters.Add("Idn", DbType.String); cmd.Parameters.Add("Pswd", DbType.Binary);
                cmd.Parameters["Idn"].Value = logOrEmail;
                cmd.Parameters["Pswd"].Value =  MD5.toBytes(psw);
                try {
                    cnt = (Int64)cmd.ExecuteScalar();
                } catch(SQLiteException _) {
                    MessageBox.Show($"Ошибка:\n {_.Message}", "Результат", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return cnt == 1L;
        }
        public bool isLog(string log) {
            int cnt = 0;
            using(var cmd = new SQLiteCommand("select count(Login) from Users where Login=:Login)", Connection)) {
                cmd.Parameters.AddWithValue("Login", log);
                try {
                    cnt = (int)cmd.ExecuteScalar();
                } catch(SQLiteException ex) {
                    MessageBox.Show($"Ошибка:\n {ex.Message}", "Результат", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return cnt == 1;
        }
        public bool isEmail(string email) {
            int cnt = 0;
            using(var cmd = new SQLiteCommand("select count(Email) from Users where Email=:Email)", Connection)) {
                cmd.Parameters.AddWithValue("Email", email);
                try {
                    cnt = (int)cmd.ExecuteScalar();
                } catch(SQLiteException ex) {
                    MessageBox.Show($"Ошибка:\n {ex.Message}", "Результат", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return cnt == 1;
        }


        /// <summary>
        /// Конатруктор класса
        /// </summary>
        [Description("Конатруктор класса")]
        public Database() {

            var ConnStr = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
            DbName = Regex.Match(ConnStr, @"Data\s+Source\s*=\s*\b(?<name>.+?(?:\.(?:\w+))?)\b", RegexOptions.IgnoreCase).Groups["name"].Value;
#if FILL_DB
            if(File.Exists(DbName)) {
                File.Delete(DbName);
            } else {
                SQLiteConnection.CreateFile(DbName);
            }
            var cs = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
            Connection = new SQLiteConnection(cs);

            Connection.Open();
            using(var cmd = new SQLiteCommand(@"CREATE TABLE ""Users"" (""Id""	INTEGER NOT NULL UNIQUE, ""Login""	TEXT(32) NOT NULL UNIQUE,
	            ""Pswd"" BLOB(32) NOT NULL, ""Email""	TEXT(32) NOT NULL UNIQUE, PRIMARY KEY(""Id"" AUTOINCREMENT))", Connection)) {
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO Users(Login, Pswd, Email) VALUES(:L0,:P0,:E0),(:L1,:P1,:E1),(:L2,:P2,:E2),(:L3,:P3,:E3),(:L4,:P4,:E4)";
                cmd.Parameters.AddWithValue("L0", "Petrov"); cmd.Parameters.AddWithValue("P0", MD5.toBytes("qwerty")); cmd.Parameters.AddWithValue("E0", "ipiter@mail.ru");
                cmd.Parameters.AddWithValue("L1", "Sidorov"); cmd.Parameters.AddWithValue("P1", MD5.toBytes("1(*3idi")); cmd.Parameters.AddWithValue("E1", "sidor@gmail.ru"); 
                cmd.Parameters.AddWithValue("L2", "Fialkin"); cmd.Parameters.AddWithValue("P2", MD5.toBytes("*#49uieuio")); cmd.Parameters.AddWithValue("E2", "fialka@yandex.ru");
                cmd.Parameters.AddWithValue("L3", "Hilovsky"); cmd.Parameters.AddWithValue("P3", MD5.toBytes("EUi82783")); cmd.Parameters.AddWithValue("E3", "nilovs@mail.ru");
                cmd.Parameters.AddWithValue("L4", "Nazarov"); cmd.Parameters.AddWithValue("P4", MD5.toBytes("_oweo?290")); cmd.Parameters.AddWithValue("E4", "nazar@yandex.ru");
                cmd.ExecuteNonQuery();  cmd.Parameters.Clear();
                cmd.CommandText = @"CREATE TABLE ""Street_types""(""Id"" INTEGER NOT NULL UNIQUE,
	                ""Name""    TEXT(24) NOT NULL, ""Short""   TEXT(6) NOT NULL,  PRIMARY KEY(""Id"" AUTOINCREMENT))";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"INSERT INTO Street_types(Name, Short) VALUES('Aллея', 'ал'),('Бульвар', 'бул'),('Деревня', 'дер'),('Квартал', 'кврт'),('Линия', 'лин'),
                   ('Микрорайон', 'мкрн'), ('Мостовая','мст'),('Набережная','наб'),('Парк','прк'),('Вал','вал'),('Взвоз','взв'),('Линия','лин'),('Луч','луч'),
                   ('Улица','ул'),('Переулок','пер'), ('Площадь','пл'),('Проезд','прзд'),('Проектируемый проезд','прпрзд'),('Просека','прск'),('Проспект','пр'),
                   ('Тупик','туп'),('Шоссе','ш'),('Проулок','пру'),('Завоз','зав'),('Заезд','зезд'),('Кольцо','клц'),('Магистраль','маг'),('Перспектива','прсп'),
                   ('Разъезд','рзд'),('Спуск','спс'),('Съезд','сзд')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"CREATE TABLE ""Location_types""(""Id"" INTEGER NOT NULL UNIQUE, ""Name"" TEXT(24) NOT NULL UNIQUE,
	                ""Short"" TEXT(6) NOT NULL UNIQUE, PRIMARY KEY(""Id"" AUTOINCREMENT))";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"INSERT INTO Location_types(Name, Short) VALUES('Город', 'г'),('Посёлок городского типа', 'пгт'),('Деревня', 'дер'),
                    ('Станция', 'стн'),('Станица', 'стц'),('Разъезд', 'рзд'),('Аул','ау'),('Хутор','хут'),('Кишлак','киш'),('Починок','поч'),('Заимка','зaим'),
                    ('Погост','пог'),('Слобода','слб'),('Выселок','выс'),('Околоток','окл')";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE ""Adresses""(""Id"" INTEGER NOT NULL UNIQUE, ""Post_Index"" INTEGER NOT NULL, ""Location"" TEXT(32) NOT NULL,
	                ""Loc_Id"" INTEGER NOT NULL, ""Street"" TEXT(48) NOT NULL, ""Stt_Id""	INTEGER NOT NULL, ""Nhouse"" INTEGER NOT NULL,
                    ""Nshared""	INTEGER, ""Build"" INTEGER,	""Ftat"" INTEGER, FOREIGN KEY(""Loc_Id"") REFERENCES ""Location_types""(""Id""),
                    FOREIGN KEY(""Stt_Id"") REFERENCES ""Street_types""(""Id""), PRIMARY KEY(""Id"" AUTOINCREMENT))";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"CREATE TABLE ""Users_Date""(""Id"" INTEGER NOT NULL UNIQUE, ""Usr_Id"" INTEGER NOT NULL, ""Name"" TEXT(32) NOT NULL,
	                ""Family""	TEXT(32) NOT NULL, ""Patronic""	TEXT(32) NOT NULL, ""Phone"" TEXT(32) NOT NULL, ""Post_Index"" INTEGER NOT NULL,
	                ""Loc_Id""	INTEGER NOT NULL, ""Birthday""	TEXT(32) NOT NULL, ""Brn_Id"" INTEGER NOT NULL, 
                    FOREIGN KEY(""Loc_Id"") REFERENCES ""Adresses""(""Id""),FOREIGN KEY(""Brn_Id"") REFERENCES ""Adresses""(""Id""),
	                FOREIGN KEY(""Usr_Id"") REFERENCES ""Users""(""Id""),PRIMARY KEY(""Id"" AUTOINCREMENT))";
                cmd.ExecuteNonQuery();
            }
#else
            Connection = new SQLiteConnection(ConnStr);
            Connection.Open();
#endif
        }

        [Description("Освободжждение ресурсов")]
        public void Dispose() {
            Connection.Close();
            Connection.Dispose();
        }
           
    }
}

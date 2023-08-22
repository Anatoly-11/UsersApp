using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace UsersApp {
    /// <summary>
    /// Класс заливки БД из скрипта
    /// </summary>
    [Description("Класс заливки БД из скрипта")]
    public static class FillDb {
        /// <summary>
        /// Метод заливки из файла 
        /// </summary>
        /// <param name="scrFName">Имя файла</param>
        /// <returns>Число выполненных записей</returns>
        [Description("Метод заливки из файла")]
        public static int FillFromFile(string scrFName) { 
            string scr;
            using(var rdr = new StreamReader(scrFName)){
                scr = rdr.ReadToEnd();
            }
            return FillStr(scr);
        }

        /// <summary>
        /// Метод заливки из строки 
        /// </summary>
        /// <param name="scr">Строка со скриптом</param>
        /// <returns>Число выполненных записей</returns>
        [Description("Метод заливки из строки")]
        public static int FillStr(string scr) {
            var n = 0; 
            if(string.IsNullOrEmpty(scr)) return -1;
            using(var cmd = new SQLiteCommand()) {
                foreach(var s in scr.Split(':')) {
                    var txt = s.Trim();
                    if(txt == "") continue;
                    cmd.CommandText = txt;
                    n += cmd.ExecuteNonQuery();
                }
            }
            return n;
        }
    }
}

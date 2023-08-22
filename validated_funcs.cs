using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;


namespace UsersApp {

    /// <summary>
    /// Статический класс проверок полей ввода
    /// </summary>
    [Description("Статический класс проверок полей ввода")]
    public class Valid_funcs {

        /// <summary>
        /// Перечисление для возврата результат проверки Логина
        /// </summary>
        [Flags]
        [Description("Перечисление для возврата результат проверки Логина")]
        public enum LoginState {
            [Description("Проверка пройдена успешно")]
            OK,                   // Проверка пройдена успешно
            [Description("Пустая строка")]
            EMPTY,                // Пустая строка
            [Description("Длинна логина вышла за размеры")]
            BAD_SIZE,             // Длинна логина вышла за размеры 
            [Description("Неверный первый символ логина")]
            BAD_FIRST_LETTER,     // Неверный первый символ логина 
            [Description("Неверный последующий символ логина")]
            BAD_NEXT_LETTERS,     // Неверный последующий символ логина 
        }

        /// <summary>
        /// Перечисление для возврата результат проверки Email
        /// </summary>
        [Flags]
        [Description("Перечисление для возврата результат проверки Email")]
        public enum EmailState {
            [Description("Проверка пройдена успешно")]
            OK,                    // Проверка пройдена успешно
            [Description("Пустая строка")]
            EMPTY,                 // Пустая строка
            [Description("Нет домена в email")]
            BAD_NO_DOG,            // Нет домена в email
            [Description("Множественные домены в email")]
            BAD_MANY_DOGS,         // Множественные домены в email
            [Description("Пустое имя  в email")]
            BAD_NAME_EMPTY,        // Пустое имя  в email
            [Description("Пустой домен в email")]
            BAD_DOM_EMPTY,         // Пустой домен в email
            [Description("Неверное имя в email")]
            BAD_NAME,              // Неверное имя в email
            [Description("Неверный домен в email")]
            BAD_DOM,               // Неверный домен в email
            [Description("Нет точки в домене")]
            BAD_DOM_NO_DOT,        // Нет точки в домене
        }

        /// <summary>
        /// Фнункция определяет является ли символ буквой или нет
        /// из первой половины таблицы ASCII кодов 
        /// </summary>
        /// <param name="chr">Проверяемый символ</param>
        /// <returns>Реузультат проверки</returns>
        [Description("Явояется ли символ ASCII буквой")]
        public static bool IsAsciiLetter(char chr) {
            return 'a' <= chr && chr <= 'z' || 'A' <= chr && chr <= 'Z';
        }

        /// <summary>
        /// Фнункция определяет является ли символ буквой 
        /// в верхнем регистре или нет из первой половины таблицы ASCII кодов
        /// <param name="chr">Проверяемый символ</param>
        /// <returns></returns>
        [Description("Явояется ли символ ASCII буквой в верхнем регистре")]
        public static bool IsUpAsciiLetter(char chr) {
            return 'A' <= chr && chr <= 'Z';
        }

        /// <summary>
        ///  Фнункция определяет является ли символ ASCII буквой 
        ///  в нижнем регистре
        /// </summary>
        /// <param name="chr">Проверяемый символ</param>
        /// <returns>Реузультат проверки</returns>
        [Description("Явояется ли символ ASCII буквой в нижнем регистре")]
        public static bool IsLowAsciiLetter(char chr) {
            return 'a' <= chr && chr <= 'z';
        }

        /// <summary>
        ///  Фнункция определяет является ли символ ASCII буквой или цифрой 
        /// </summary>
        /// <param name="chr">Проверяемый символ</param>
        /// <returns>Реузультат проверки</returns>
        [Description("Является ли символ ASCII буквой или числом")]
        public static bool IsAsciiLetterOrDigit(char chr) {
            return 'a' <= chr && chr <= 'z' || 'A' <= chr && chr <= 'Z' || '0' <= chr && chr <= '9';
        }

        /// <summary>
        ///  Фнункция определяет является ли символ ASCII буквой в нижнем регистре или цифрой 
        /// </summary>
        /// <param name="chr">Проверяемый симол</param>
        /// <returns>Результат проверки</returns>
        [Description("Явояется ли символ ASCII буквой в нижнем регистре или числом")]
        public static bool IsLowAsciiLetterOrDigit(char chr) {
            return 'a' <= chr && chr <= 'z' || '0' <= chr && chr <= '9';
        }

        /// <summary>
        ///  Фнункция определяет является ли символ ASCII буквой в нижнем регистре или цифрой 
        /// </summary>
        /// <param name="chr">Проверяемый симол</param>
        /// <returns>Результат проверки</returns>
        [Description("Является ли символ ASCII буквой в верхнем регистре или числом")]
        public static bool IsUpAsciiLetterOrDigit(char chr) {
            return 'A' <= chr && chr <= 'Z' || '0' <= chr && chr <= '9';
        }

        /// <summary>
        /// Фнункция определяет есть ли в проверяемой строке пробельные символы 
        /// </summary>
        /// <param name="txt">Проверяемая строка</param>
        /// <returns>Результат проверки</returns>
        [Description("Если ли во входной строеке пробельные символы")]
        public static bool IsSeparetorInStr(string txt) {
            foreach(var ch in txt) {
                if(char.IsSeparator(ch))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Фнункция определяет есть ли во входной строке символы из проверяемого набора
        /// </summary>
        /// <param name="txt">Входная строка</param>
        /// <param name="symols">Проверяемый набор</param>
        /// <returns>Результат проверки</returns>
        [Description("Есть ли во входной строке символы из проверяемого набора")]
        public static bool IsSomeOfSymbInStr(string txt, string symols) {
            foreach( var s in symols) {
                foreach(var ch in txt) {
                    if(s == ch)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Фнункция определяет есть ли во входной строке символы из проверяемого набора
        /// Перегруженая фрнкция
        /// </summary>
        /// <param name="txt">Входная строка</param>
        /// <param name="chars">Проверяемый набор</param>
        /// <returns>Результат проверки</returns>
        [Description("Есть ли во входной строке символы из проверяемого набора")]
        public static bool IsSomeOfSymbInStr(string txt, char[] chars) {
            foreach(var s in chars) {
                foreach(var ch in txt) {
                    if(s == ch)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Функция проверки логина
        /// </summary>
        /// <param name="txt">Проверяемый логин</param>
        /// <returns>Результат проверки</returns>
        [Description("Проверка логина")]
        public static LoginState Login(string txt) {
            var res = LoginState.OK;
            if(txt.Length == 0)
                res = LoginState.EMPTY;
            else if(!(6 <= txt.Length && txt.Length <= 16))
                res = LoginState.BAD_SIZE;
            else if(!IsAsciiLetter(txt[0]))
                res = LoginState.BAD_FIRST_LETTER;
            else {
                foreach(var ch in txt) {
                    if(!(IsAsciiLetterOrDigit(ch) || ch == '-' || ch == '_')) {
                        res = LoginState.BAD_NEXT_LETTERS;
                        break;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Функция проверки Email
        /// </summary>
        /// <param name="txt">Проверяемый email</param>
        /// <returns>Результат проверки</returns>
        [Description("Проверка email")]
        public static EmailState Email(string txt) {// without RegEx
            var res = EmailState.OK;
            if(txt.Length == 0)
                return EmailState.EMPTY;
            var spls = txt.Split('@');
            if(!txt.Contains("@")) {
                return EmailState.BAD_NO_DOG;
            } 
            if(spls.Length > 2) {
                return EmailState.BAD_MANY_DOGS;
            }
            var ss = new { Name = spls[0], Dom = spls[1] };
            if(ss.Name.Length == 0)
                res = EmailState.BAD_NAME_EMPTY;
            else if(ss.Dom.Length == 0)
                res = EmailState.BAD_DOM_EMPTY;
            else {
                foreach(var ch in ss.Name) {
                    if(!(IsLowAsciiLetter(ch) || char.IsDigit(ch) || ch == '-' || ch == '_')) {
                        res = EmailState.BAD_NAME;
                        break;
                    }
                }
                if(res != EmailState.OK) {
                    foreach(var ch in ss.Dom) {
                        if(!(IsLowAsciiLetterOrDigit(ch) || ch == '-' || ch == '_' || ch == '.')) {
                            res = EmailState.BAD_DOM;
                            break;
                        }
                    }
                    if(res == EmailState.OK && !ss.Dom.Contains('.'))
                        res = EmailState.BAD_DOM_NO_DOT;
                }
            }
            return res;
        }

        /// <summary>
        /// Перечисление для возврата результат проверки пароля
        /// </summary>
        [Flags]
        [Description("Перечисление для возврата результат проверки пароля")]
        public enum PasswordState {
            [Description("Проверка пройдена успешно")]
            OK,           // Проверка пройдена успешно
            [Description("Пустая строка")]
            EMPTY,        // Пустая строка
            [Description("Неверный размер дб от 6 до 12 символов")]
            BAD_SIZE,     // Неверный размер дб от 6 до 12 символов
            [Description("Неверные символы")]
            BAD_FMT,      // Неверные символы
        }

        /// <summary>
        /// Функция проверки пароля
        /// </summary>
        /// <param name="txt">Проверяемый пароль</param>
        /// <returns>Результат проверки</returns>
        [Description("Проверка пароля")]
        public static PasswordState Pasword(string txt) {
            var res = PasswordState.OK; 
            if(txt.Length == 0) {
                res = PasswordState.EMPTY;
            } else if(!(6 <= txt.Length && txt.Length <= 12)) {
                res = PasswordState.BAD_SIZE;
            } else {
                foreach(var ch in txt) {
                    if(char.IsSeparator(ch)) {
                        res = PasswordState.BAD_FMT;
                        break;
                    }
                }
            }
            return res;
        }

       
        

        /// <summary>
        /// Функция форматирует строку ошибки если есть или возвращает пустую строку 
        /// </summary>
        /// <param name="fld"></param>
        /// <param name="strEr"></param>
        /// <returns>Отформатированная или пустая строка</returns>
        [Description("Форматирует строку ошибки если есть или возвращает пустую строку")]
        public static string msgFmt(string fld, string strEr) => strEr.Length > 0 ?  $"Поле \"{fld}\": {strEr}..." : "";

        /// <summary>
        /// Перечисление для возврата результат проверки телефона
        /// </summary>
        [Flags]
        [Description("Перечисление для возврата результат проверки телефона")]
        public enum PhoneState {
            [Description("Проверка пройдена успешно")]
            OK,           // Проверка пройдена успешно
            [Description("Пустая строка")]
            EMPTY,        // Пустая строка
            [Description("Неверный формат")]
            BAD,          // Неверный формат
        }

        /// <summary>
        /// Функция проверки телефона
        /// </summary>
        /// <param name="txt">Проверяемый телефон</param>
        /// <returns>Результат проверки</returns>
        [Description("Проверка телефона")]
        public static PhoneState Phone(string txt) {
            PhoneState r = PhoneState.OK;
            if(string.IsNullOrEmpty(txt))
                r = PhoneState.EMPTY;
            else { // "((+7|8)-)ddd(-)ddd(-)dd(-)dd"
                if(!Regex.Match(txt, @"^(\+?\d-)?[\d]{3}-?[\d]{3}-?[\d]{2}-&[\d]{2}$").Success)
                    r = PhoneState.BAD;
            }
            return r;
        }
    }
}

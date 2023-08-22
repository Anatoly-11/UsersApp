using System.Text;
using System.Security.Cryptography;
using System.ComponentModel;

namespace UsersApp {
    /// <summary>
    /// Класс для хэширования строк
    /// </summary>
    [Description("Класс для хэширования строк")]
    public static class MD5 {
        private static readonly MD5Cng MD5Fact = new MD5Cng();

        /// <summary>
        /// Хэширование строки в строку
        /// </summary>
        /// <param name="txt">Хэшируемая строка</param>
        /// <returns>Захэширования строка</returns>
        [Description("Хэширование строки в строку")]
        public static string toStr(string txt) => Encoding.Default.GetString(toBytes(txt));

        /// <summary>
        /// Хэширование строки в массив байт
        /// </summary>
        /// <param name="txt">Хэшируемая строка</param>
        /// <returns>Захэшированый массив байт</returns>
        [Description("Хэширование строки в массив байт")]
        public static byte[] toBytes(string txt) => MD5Fact.ComputeHash(Encoding.Default.GetBytes(txt));

    }
}

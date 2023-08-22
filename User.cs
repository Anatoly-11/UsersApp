using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp {
    /// <summary>
    /// Класс модель User
    /// </summary>
    [Description("Класс модель User")]
    public class User {
        /// <summary>
        /// User ID
        /// </summary>
        [Description("User ID")]
        public int Id { get; set; }

        /// <summary>
        /// User Login
        /// </summary>
        [Description("User Login")]
        public string Login { get; set; }

        /// <summary>
        /// User pswd
        /// </summary>
        [Description("User pswd")]
        public byte[] Pswd { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Description("User email")]
        public string Email { get; set; }
    }
}

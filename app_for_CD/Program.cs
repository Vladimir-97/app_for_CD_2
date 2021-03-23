using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_for_CD
{
    static class Data
    {
        public static int f_c { get; set; }
        public static int f_s { get; set; }
        public static int f_d { get; set; }
        public static string st_data_orig { get; set; }
        public static string end_data_orig { get; set; }
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_agreement());
        }
    }

}

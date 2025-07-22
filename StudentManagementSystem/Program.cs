using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentManagementSystem.Forms;

namespace StudentManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new StudentForm());
            //Application.Run(new CourseForm());
            //Application.Run(new SubjectForm());
            //Application.Run(new EnrollmentForm());
            //Application.Run(new MarksForm());
            //Application.Run(new AttendanceForm());
            //Application.Run(new FeesForm());
            Application.Run(new DashboardForm());
        }
    }
}

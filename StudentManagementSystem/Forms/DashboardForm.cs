using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }
        private void btnStudents_Click(object sender, EventArgs e)
        {
            StudentForm sf = new StudentForm();
            sf.FormClosed += (s, args) => this.Show();
            this.Hide();
            sf.Show();
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            CourseForm cf = new CourseForm();
            cf.FormClosed += (s, args) => this.Show();
            this.Hide();
            cf.Show();
        }

        private void btnSubjects_Click(object sender, EventArgs e)
        {
            SubjectForm sf = new SubjectForm();
            sf.FormClosed += (s, args) => this.Show();
            this.Hide();
            sf.Show();
        }

        private void btnEnrollments_Click(object sender, EventArgs e)
        {
            EnrollmentForm ef = new EnrollmentForm();
            ef.FormClosed += (s, args) => this.Show();
            this.Hide();
            ef.Show();
        }

        private void btnMarks_Click(object sender, EventArgs e)
        {
            MarksForm mf = new MarksForm();
            mf.FormClosed += (s, args) => this.Show();
            this.Hide();
            mf.Show();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            AttendanceForm af = new AttendanceForm();
            af.FormClosed += (s, args) => this.Show();
            this.Hide();
            af.Show();
        }

        private void btnFees_Click(object sender, EventArgs e)
        {
            FeesForm ff = new FeesForm();
            ff.FormClosed += (s, args) => this.Show();
            this.Hide();
            ff.Show();
        }

    }
}

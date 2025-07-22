using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Forms
{
    public partial class AttendanceForm : Form
    {
        public AttendanceForm()
        {
            InitializeComponent();
        }

        private void AttendanceForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
            LoadAttendance();
        }
        private void LoadStudents()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id, FullName FROM Students", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbStudent.DataSource = dt;
                cmbStudent.DisplayMember = "FullName";
                cmbStudent.ValueMember = "Id";
                cmbStudent.SelectedIndex = -1;
            }
        }
        private void LoadAttendance()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT a.Id, s.FullName AS Student, a.Date, a.Status
            FROM Attendance a
            JOIN Students s ON a.StudentId = s.Id
            ORDER BY a.Date DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAttendance.DataSource = dt;

                dgvAttendance.Columns["Id"].Visible = false;
            }
        }
        private void btnMarkAttendance_Click(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a student.");
                return;
            }

            string status = "";
            if (rbtnPresent.Checked) status = "Present";
            else if (rbtnAbsent.Checked) status = "Absent";
            else if (rbtnLate.Checked) status = "Late";
            else
            {
                MessageBox.Show("Please select attendance status.");
                return;
            }

            int studentId = Convert.ToInt32(cmbStudent.SelectedValue);
            DateTime date = dtpDate.Value.Date;

            // Optional: Prevent duplicate entries for the same student and date
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Attendance WHERE StudentId = @StudentId AND Date = @Date", conn);
                checkCmd.Parameters.AddWithValue("@StudentId", studentId);
                checkCmd.Parameters.AddWithValue("@Date", date);

                int exists = (int)checkCmd.ExecuteScalar();
                if (exists > 0)
                {
                    MessageBox.Show("Attendance already marked for this student on selected date.");
                    return;
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO Attendance (StudentId, Date, Status) VALUES (@StudentId, @Date, @Status)", conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Attendance marked successfully.");
                LoadAttendance();
                ClearFields();
            }
        }
        private void ClearFields()
        {
            cmbStudent.SelectedIndex = -1;
            dtpDate.Value = DateTime.Today;
            rbtnPresent.Checked = false;
            rbtnAbsent.Checked = false;
            rbtnLate.Checked = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

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
    public partial class EnrollmentForm : Form
    {
        public EnrollmentForm()
        {
            InitializeComponent();
        }

        private void EnrollmentForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
            LoadCourses();
            LoadEnrollments();
        }

        private void LoadStudents()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, FullName FROM Students", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                cmbStudent.DataSource = dt;
                cmbStudent.DisplayMember = "FullName";
                cmbStudent.ValueMember = "Id";
            }
        }

        private void LoadCourses()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, CourseName FROM Courses", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                cmbCourse.DataSource = dt;
                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "Id";
            }
        }
        private void btnEnroll_Click(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedValue == null || cmbCourse.SelectedValue == null)
            {
                MessageBox.Show("Please select both student and course.");
                return;
            }

            int studentId = Convert.ToInt32(cmbStudent.SelectedValue);
            int courseId = Convert.ToInt32(cmbCourse.SelectedValue);
            DateTime enrollDate = dtpEnrollDate.Value;

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Enrollments (StudentId, CourseId, EnrollmentDate) VALUES (@StudentId, @CourseId, @EnrollmentDate)", conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.Parameters.AddWithValue("@EnrollmentDate", enrollDate);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Student enrolled successfully.");
            LoadEnrollments();
        }
        private void LoadEnrollments()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT e.Id, s.FullName AS Student, c.CourseName AS Course, e.EnrollmentDate 
              FROM Enrollments e
              JOIN Students s ON e.StudentId = s.Id
              JOIN Courses c ON e.CourseId = c.Id", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvEnrollments.DataSource = dt;
            }
        }
        private void dgvEnrollments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEnrollments.Rows[e.RowIndex];

                cmbStudent.Text = row.Cells["Student"].Value.ToString();
                cmbCourse.Text = row.Cells["Course"].Value.ToString();
                dtpEnrollDate.Value = Convert.ToDateTime(row.Cells["EnrollmentDate"].Value);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEnrollments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            int id = Convert.ToInt32(dgvEnrollments.SelectedRows[0].Cells["Id"].Value);

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Enrollments WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Enrollment deleted.");
            LoadEnrollments();
            ClearFields();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvEnrollments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to update.");
                return;
            }

            int id = Convert.ToInt32(dgvEnrollments.SelectedRows[0].Cells["Id"].Value);
            int studentId = Convert.ToInt32(cmbStudent.SelectedValue);
            int courseId = Convert.ToInt32(cmbCourse.SelectedValue);
            DateTime enrollDate = dtpEnrollDate.Value;

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Enrollments SET StudentId=@StudentId, CourseId=@CourseId, EnrollmentDate=@EnrollmentDate WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.Parameters.AddWithValue("@EnrollmentDate", enrollDate);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Enrollment updated.");
            LoadEnrollments();
            ClearFields();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            cmbStudent.SelectedIndex = -1;
            cmbCourse.SelectedIndex = -1;
            dtpEnrollDate.Value = DateTime.Today;
            dgvEnrollments.ClearSelection();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

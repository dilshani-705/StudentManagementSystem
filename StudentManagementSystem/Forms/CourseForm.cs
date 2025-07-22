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
    public partial class CourseForm : Form
    {
        public CourseForm()
        {
            InitializeComponent();
        }

        private void CourseForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Courses", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCourses.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCourseName.Text == "" || txtDuration.Text == "")
            {
                MessageBox.Show("Please enter all fields.");
                return;
            }

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Courses (CourseName, Duration) VALUES (@CourseName, @Duration)", conn);
                cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                cmd.Parameters.AddWithValue("@Duration", Convert.ToInt32(txtDuration.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Course added!");
                LoadCourses();
                ClearFields();
            }
        }
        private void dgvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCourseName.Text = dgvCourses.Rows[e.RowIndex].Cells["CourseName"].Value.ToString();
                txtDuration.Text = dgvCourses.Rows[e.RowIndex].Cells["Duration"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a course to update.");
                return;
            }

            int id = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["Id"].Value);
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Courses SET CourseName=@CourseName, Duration=@Duration WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                cmd.Parameters.AddWithValue("@Duration", Convert.ToInt32(txtDuration.Text));
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Course updated!");
                LoadCourses();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a course to delete.");
                return;
            }

            int id = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["Id"].Value);
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Courses WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Course deleted!");
                LoadCourses();
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtCourseName.Clear();
            txtDuration.Clear();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

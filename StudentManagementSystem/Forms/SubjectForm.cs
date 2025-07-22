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
    public partial class SubjectForm : Form
    {
        public SubjectForm()
        {
            InitializeComponent();
        }

        private void SubjectForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadSubjects();
        }
        private void LoadCourses()
        {
            using (SqlConnection conn = new SqlConnection(DbHelper.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ID, CourseName FROM Courses", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbCourse.DataSource = dt;
                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "ID";
            }
        }
        private void LoadSubjects()
        {
            using (SqlConnection conn = new SqlConnection(DbHelper.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT s.ID, s.SubjectName, c.CourseName 
            FROM Subjects s 
            INNER JOIN Courses c ON s.CourseID = c.ID", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSubjects.DataSource = dt;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text) || cmbCourse.SelectedValue == null)
            {
                MessageBox.Show("Please enter subject name and select a course.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Subjects (SubjectName, CourseID) VALUES (@SubjectName, @CourseID)", conn);
                cmd.Parameters.AddWithValue("@SubjectName", txtSubjectName.Text);
                cmd.Parameters.AddWithValue("@CourseID", cmbCourse.SelectedValue);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Subject added successfully!");
                LoadSubjects();
                ClearFields();
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a subject to update.");
                return;
            }

            int id = Convert.ToInt32(dgvSubjects.SelectedRows[0].Cells["Id"].Value);

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Subjects SET SubjectName = @SubjectName, CourseId = @CourseId WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@SubjectName", txtSubjectName.Text);

                // Assuming cmbCourse.SelectedValue holds the CourseId (not just text!)
                cmd.Parameters.AddWithValue("@CourseId", Convert.ToInt32(cmbCourse.SelectedValue));
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Subject updated!");

                LoadSubjects(); // Refresh DataGridView with updated data
                ClearFields();  // Clear input fields
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.CurrentRow != null)
            {
                int subjectId = Convert.ToInt32(dgvSubjects.CurrentRow.Cells["ID"].Value);
                using (SqlConnection conn = new SqlConnection(DbHelper.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Subjects WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("@ID", subjectId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Subject deleted!");
                    LoadSubjects();
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            txtSubjectName.Clear();
            cmbCourse.SelectedIndex = -1;
        }

        private void dgvSubjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSubjects.Rows[e.RowIndex];
                txtSubjectName.Text = row.Cells["SubjectName"].Value.ToString();
                cmbCourse.Text = row.Cells["CourseName"].Value.ToString();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

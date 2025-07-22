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
    public partial class MarksForm : Form
    {
        public MarksForm()
        {
            InitializeComponent();
        }

        private void MarksForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
            LoadSubjects();
            LoadMarks();
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

        private void LoadSubjects()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id, SubjectName FROM Subjects", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbSubject.DataSource = dt;
                cmbSubject.DisplayMember = "SubjectName";
                cmbSubject.ValueMember = "Id";
                cmbSubject.SelectedIndex = -1;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedIndex == -1 || cmbSubject.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtMark.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (!int.TryParse(txtMark.Text, out int mark) || mark < 0 || mark > 100)
            {
                MessageBox.Show("Enter a valid mark between 0 and 100.");
                return;
            }

            int studentId = Convert.ToInt32(cmbStudent.SelectedValue);
            int subjectId = Convert.ToInt32(cmbSubject.SelectedValue);

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Marks (StudentId, SubjectId, Mark) VALUES (@StudentId, @SubjectId, @Mark)", conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                cmd.Parameters.AddWithValue("@Mark", mark);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Mark saved successfully.");
            LoadMarks();
            ClearFields();
        }
        private void LoadMarks()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT m.Id, s.FullName AS Student, sub.SubjectName AS Subject, m.Mark
            FROM Marks m
            JOIN Students s ON m.StudentId = s.Id
            JOIN Subjects sub ON m.SubjectId = sub.Id";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMarks.DataSource = dt;

                dgvMarks.Columns["Id"].Visible = false; // Hide ID
            }
        }
        private void ClearFields()
        {
            cmbStudent.SelectedIndex = -1;
            cmbSubject.SelectedIndex = -1;
            txtMark.Clear();
            dgvMarks.ClearSelection();
        }
        private void dgvMarks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMarks.Rows[e.RowIndex];
                cmbStudent.Text = row.Cells["Student"].Value.ToString();
                cmbSubject.Text = row.Cells["Subject"].Value.ToString();
                txtMark.Text = row.Cells["Mark"].Value.ToString();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

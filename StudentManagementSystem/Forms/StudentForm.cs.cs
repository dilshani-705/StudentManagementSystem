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


namespace StudentManagementSystem
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }
        private void LoadStudents()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Students", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvStudents.DataSource = dt;
            }
        }
        private void ClearFields()
        {
            txtFullName.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            cmbGender.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now;
            txtAddress.Clear();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Students (FullName, DOB, Gender, Contact, Email, Address) " +
                               "VALUES (@FullName, @DOB, @Gender, @Contact, @Email, @Address)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@DOB", dtpDOB.Value);
                cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem?.ToString() ?? "");
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully!");
                LoadStudents();
                ClearFields();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["Id"].Value);
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Students SET FullName=@FullName, DOB=@DOB, Gender=@Gender, Contact=@Contact, Email=@Email, Address=@Address WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@DOB", dtpDOB.Value);
                    cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Id", studentId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Updated Successfully!");
                    LoadStudents();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Please select a student to update.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int studentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["Id"].Value);
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Students WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", studentId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted Successfully!");
                    LoadStudents();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtFullName.Text = dgvStudents.Rows[e.RowIndex].Cells["FullName"].Value.ToString();
                dtpDOB.Value = Convert.ToDateTime(dgvStudents.Rows[e.RowIndex].Cells["DOB"].Value);
                cmbGender.SelectedItem = dgvStudents.Rows[e.RowIndex].Cells["Gender"].Value.ToString();
                txtContact.Text = dgvStudents.Rows[e.RowIndex].Cells["Contact"].Value.ToString();
                txtEmail.Text = dgvStudents.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                txtAddress.Text = dgvStudents.Rows[e.RowIndex].Cells["Address"].Value.ToString();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

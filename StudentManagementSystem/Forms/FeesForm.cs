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
    public partial class FeesForm : Form
    {
        public FeesForm()
        {
            InitializeComponent();
        }

        private void FeesForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
            LoadFees();
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
        private void LoadFees()
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT f.Id, s.FullName AS Student, f.Amount, f.PaidDate
            FROM Fees f
            JOIN Students s ON f.StudentId = s.Id
            ORDER BY f.PaidDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvFees.DataSource = dt;

                dgvFees.Columns["Id"].Visible = false;
            }
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a student.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Enter a valid amount.");
                return;
            }

            int studentId = Convert.ToInt32(cmbStudent.SelectedValue);
            DateTime paidDate = dtpPaidDate.Value.Date;

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Fees (StudentId, Amount, PaidDate) VALUES (@StudentId, @Amount, @PaidDate)", conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@PaidDate", paidDate);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Fee paid successfully.");
                LoadFees();
                ClearFields();
            }
        }
        private void ClearFields()
        {
            cmbStudent.SelectedIndex = -1;
            txtAmount.Clear();
            dtpPaidDate.Value = DateTime.Today;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

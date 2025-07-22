namespace StudentManagementSystem.Forms
{
    partial class AttendanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbStudent = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.rbtnPresent = new System.Windows.Forms.RadioButton();
            this.rbtnAbsent = new System.Windows.Forms.RadioButton();
            this.rbtnLate = new System.Windows.Forms.RadioButton();
            this.btnMarkAttendance = new System.Windows.Forms.Button();
            this.dgvAttendance = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbStudent
            // 
            this.cmbStudent.FormattingEnabled = true;
            this.cmbStudent.Location = new System.Drawing.Point(134, 43);
            this.cmbStudent.Name = "cmbStudent";
            this.cmbStudent.Size = new System.Drawing.Size(121, 24);
            this.cmbStudent.TabIndex = 0;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(134, 89);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 22);
            this.dtpDate.TabIndex = 1;
            // 
            // rbtnPresent
            // 
            this.rbtnPresent.AutoSize = true;
            this.rbtnPresent.Location = new System.Drawing.Point(134, 146);
            this.rbtnPresent.Name = "rbtnPresent";
            this.rbtnPresent.Size = new System.Drawing.Size(74, 20);
            this.rbtnPresent.TabIndex = 2;
            this.rbtnPresent.TabStop = true;
            this.rbtnPresent.Text = "Present";
            this.rbtnPresent.UseVisualStyleBackColor = true;
            // 
            // rbtnAbsent
            // 
            this.rbtnAbsent.AutoSize = true;
            this.rbtnAbsent.Location = new System.Drawing.Point(259, 146);
            this.rbtnAbsent.Name = "rbtnAbsent";
            this.rbtnAbsent.Size = new System.Drawing.Size(70, 20);
            this.rbtnAbsent.TabIndex = 3;
            this.rbtnAbsent.TabStop = true;
            this.rbtnAbsent.Text = "Absent";
            this.rbtnAbsent.UseVisualStyleBackColor = true;
            // 
            // rbtnLate
            // 
            this.rbtnLate.AutoSize = true;
            this.rbtnLate.Location = new System.Drawing.Point(379, 146);
            this.rbtnLate.Name = "rbtnLate";
            this.rbtnLate.Size = new System.Drawing.Size(54, 20);
            this.rbtnLate.TabIndex = 4;
            this.rbtnLate.TabStop = true;
            this.rbtnLate.Text = "Late";
            this.rbtnLate.UseVisualStyleBackColor = true;
            // 
            // btnMarkAttendance
            // 
            this.btnMarkAttendance.Location = new System.Drawing.Point(134, 185);
            this.btnMarkAttendance.Name = "btnMarkAttendance";
            this.btnMarkAttendance.Size = new System.Drawing.Size(84, 26);
            this.btnMarkAttendance.TabIndex = 5;
            this.btnMarkAttendance.Text = "Mark";
            this.btnMarkAttendance.UseVisualStyleBackColor = true;
            this.btnMarkAttendance.Click += new System.EventHandler(this.btnMarkAttendance_Click);
            // 
            // dgvAttendance
            // 
            this.dgvAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttendance.Location = new System.Drawing.Point(24, 229);
            this.dgvAttendance.Name = "dgvAttendance";
            this.dgvAttendance.RowHeadersWidth = 51;
            this.dgvAttendance.RowTemplate.Height = 24;
            this.dgvAttendance.Size = new System.Drawing.Size(737, 209);
            this.dgvAttendance.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Student";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Attendance";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(224, 187);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // AttendanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAttendance);
            this.Controls.Add(this.btnMarkAttendance);
            this.Controls.Add(this.rbtnLate);
            this.Controls.Add(this.rbtnAbsent);
            this.Controls.Add(this.rbtnPresent);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cmbStudent);
            this.Name = "AttendanceForm";
            this.Text = "AttendanceForm";
            this.Load += new System.EventHandler(this.AttendanceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStudent;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.RadioButton rbtnPresent;
        private System.Windows.Forms.RadioButton rbtnAbsent;
        private System.Windows.Forms.RadioButton rbtnLate;
        private System.Windows.Forms.Button btnMarkAttendance;
        private System.Windows.Forms.DataGridView dgvAttendance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBack;
    }
}
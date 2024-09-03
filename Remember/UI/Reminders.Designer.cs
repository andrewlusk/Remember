namespace Remember.UI
{
    partial class Reminders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reminders));
            dgvReminders = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReminders).BeginInit();
            SuspendLayout();
            // 
            // dgvReminders
            // 
            dgvReminders.AllowUserToAddRows = false;
            dgvReminders.AllowUserToDeleteRows = false;
            dgvReminders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvReminders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReminders.Location = new Point(16, 11);
            dgvReminders.Name = "dgvReminders";
            dgvReminders.ReadOnly = true;
            dgvReminders.Size = new Size(456, 180);
            dgvReminders.TabIndex = 0;
            // 
            // Reminders
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 203);
            Controls.Add(dgvReminders);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Reminders";
            Text = "Reminders";
            ((System.ComponentModel.ISupportInitialize)dgvReminders).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public DataGridView dgvReminders;
    }
}
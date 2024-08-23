namespace Remember
{
    partial class ModalNewSubfolder
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
            txtNewSubfolderName = new TextBox();
            lblNewSubfolderName = new Label();
            btnNewSubfolderOK = new Button();
            btnNewSubfolderCancel = new Button();
            SuspendLayout();
            // 
            // txtNewSubfolderName
            // 
            txtNewSubfolderName.Location = new Point(24, 32);
            txtNewSubfolderName.Name = "txtNewSubfolderName";
            txtNewSubfolderName.Size = new Size(390, 27);
            txtNewSubfolderName.TabIndex = 0;
            txtNewSubfolderName.TextChanged += txtNewSubfolderName_TextChanged;
            txtNewSubfolderName.KeyPress += txtNewSubfolderName_KeyPress;
            // 
            // lblNewSubfolderName
            // 
            lblNewSubfolderName.AutoSize = true;
            lblNewSubfolderName.Location = new Point(23, 7);
            lblNewSubfolderName.Name = "lblNewSubfolderName";
            lblNewSubfolderName.Size = new Size(129, 20);
            lblNewSubfolderName.TabIndex = 1;
            lblNewSubfolderName.Text = "New Folder Name";
            // 
            // btnNewSubfolderOK
            // 
            btnNewSubfolderOK.Enabled = false;
            btnNewSubfolderOK.Location = new Point(26, 72);
            btnNewSubfolderOK.Name = "btnNewSubfolderOK";
            btnNewSubfolderOK.Size = new Size(94, 29);
            btnNewSubfolderOK.TabIndex = 2;
            btnNewSubfolderOK.Text = "OK";
            btnNewSubfolderOK.UseVisualStyleBackColor = true;
            btnNewSubfolderOK.Click += btnNewSubfolderOK_Click;
            // 
            // btnNewSubfolderCancel
            // 
            btnNewSubfolderCancel.Location = new Point(126, 72);
            btnNewSubfolderCancel.Name = "btnNewSubfolderCancel";
            btnNewSubfolderCancel.Size = new Size(94, 29);
            btnNewSubfolderCancel.TabIndex = 3;
            btnNewSubfolderCancel.Text = "Cancel";
            btnNewSubfolderCancel.UseVisualStyleBackColor = true;
            btnNewSubfolderCancel.Click += btnNewSubfolderCancel_Click;
            // 
            // ModalNewSubfolder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 122);
            Controls.Add(btnNewSubfolderCancel);
            Controls.Add(btnNewSubfolderOK);
            Controls.Add(lblNewSubfolderName);
            Controls.Add(txtNewSubfolderName);
            Name = "ModalNewSubfolder";
            Text = "ModalNewSubfolder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNewSubfolderName;
        private Label lblNewSubfolderName;
        private Button btnNewSubfolderOK;
        private Button btnNewSubfolderCancel;
    }
}
namespace Remember
{
    partial class ModalSaveLoadQuery
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
            cmbQueryNames = new ComboBox();
            btnSaveLoad = new Button();
            lblQueryName = new Label();
            btnDeleteQuery = new Button();
            SuspendLayout();
            // 
            // cmbQueryNames
            // 
            cmbQueryNames.FormattingEnabled = true;
            cmbQueryNames.Location = new Point(90, 19);
            cmbQueryNames.Name = "cmbQueryNames";
            cmbQueryNames.Size = new Size(345, 23);
            cmbQueryNames.TabIndex = 0;
            cmbQueryNames.SelectedIndexChanged += cmbQueryNames_SelectedIndexChanged;
            // 
            // btnSaveLoad
            // 
            btnSaveLoad.Location = new Point(89, 48);
            btnSaveLoad.Name = "btnSaveLoad";
            btnSaveLoad.Size = new Size(75, 23);
            btnSaveLoad.TabIndex = 1;
            btnSaveLoad.Text = "Save/Load";
            btnSaveLoad.UseVisualStyleBackColor = true;
            btnSaveLoad.Click += btnSaveLoad_Click;
            // 
            // lblQueryName
            // 
            lblQueryName.AutoSize = true;
            lblQueryName.Location = new Point(11, 22);
            lblQueryName.Name = "lblQueryName";
            lblQueryName.Size = new Size(74, 15);
            lblQueryName.TabIndex = 2;
            lblQueryName.Text = "Query Name";
            // 
            // btnDeleteQuery
            // 
            btnDeleteQuery.Location = new Point(306, 48);
            btnDeleteQuery.Name = "btnDeleteQuery";
            btnDeleteQuery.Size = new Size(129, 23);
            btnDeleteQuery.TabIndex = 3;
            btnDeleteQuery.Text = "Delete Saved Query";
            btnDeleteQuery.UseVisualStyleBackColor = true;
            btnDeleteQuery.Visible = false;
            btnDeleteQuery.Click += btnDeleteQuery_Click;
            // 
            // ModalSaveLoadQuery
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 81);
            Controls.Add(btnDeleteQuery);
            Controls.Add(lblQueryName);
            Controls.Add(btnSaveLoad);
            Controls.Add(cmbQueryNames);
            Name = "ModalSaveLoadQuery";
            Text = "ModalSaveLoadQuery";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbQueryNames;
        private Button btnSaveLoad;
        private Label lblQueryName;
        private Button btnDeleteQuery;
    }
}
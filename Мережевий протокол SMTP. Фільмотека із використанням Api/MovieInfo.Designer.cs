namespace HomeWorkWinForms
{
    partial class MovieInfo
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
            pictureBox1 = new PictureBox();
            TitleLabel = new Label();
            DateDurationLabel = new Label();
            DescriptionTextBox = new TextBox();
            label3 = new Label();
            MovieDataGridView = new DataGridView();
            IdColumn = new DataGridViewTextBoxColumn();
            TitleColumn = new DataGridViewTextBoxColumn();
            ReleaseDateColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MovieDataGridView).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 87);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(195, 343);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI", 12F);
            TitleLabel.Location = new Point(12, 9);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(49, 28);
            TitleLabel.TabIndex = 1;
            TitleLabel.Text = "Title";
            // 
            // DateDurationLabel
            // 
            DateDurationLabel.AutoSize = true;
            DateDurationLabel.Font = new Font("Segoe UI", 9F);
            DateDurationLabel.Location = new Point(12, 49);
            DateDurationLabel.Name = "DateDurationLabel";
            DateDurationLabel.Size = new Size(111, 20);
            DateDurationLabel.TabIndex = 2;
            DateDurationLabel.Text = "Date | Duration";
            // 
            // DescriptionTextBox
            // 
            DescriptionTextBox.Location = new Point(222, 17);
            DescriptionTextBox.Multiline = true;
            DescriptionTextBox.Name = "DescriptionTextBox";
            DescriptionTextBox.ReadOnly = true;
            DescriptionTextBox.Size = new Size(363, 193);
            DescriptionTextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(222, 222);
            label3.Name = "label3";
            label3.Size = new Size(165, 20);
            label3.TabIndex = 4;
            label3.Text = "Watch simmilar movies:";
            // 
            // MovieDataGridView
            // 
            MovieDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MovieDataGridView.Columns.AddRange(new DataGridViewColumn[] { IdColumn, TitleColumn, ReleaseDateColumn });
            MovieDataGridView.Location = new Point(222, 245);
            MovieDataGridView.Name = "MovieDataGridView";
            MovieDataGridView.RowHeadersWidth = 51;
            MovieDataGridView.Size = new Size(363, 185);
            MovieDataGridView.TabIndex = 20;
            // 
            // IdColumn
            // 
            IdColumn.DataPropertyName = "id";
            IdColumn.Frozen = true;
            IdColumn.HeaderText = "Id";
            IdColumn.MinimumWidth = 6;
            IdColumn.Name = "IdColumn";
            IdColumn.ReadOnly = true;
            IdColumn.Visible = false;
            IdColumn.Width = 125;
            // 
            // TitleColumn
            // 
            TitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            TitleColumn.DataPropertyName = "title";
            TitleColumn.HeaderText = "Title";
            TitleColumn.MinimumWidth = 6;
            TitleColumn.Name = "TitleColumn";
            // 
            // ReleaseDateColumn
            // 
            ReleaseDateColumn.DataPropertyName = "release_date";
            ReleaseDateColumn.HeaderText = "Release Date";
            ReleaseDateColumn.MinimumWidth = 6;
            ReleaseDateColumn.Name = "ReleaseDateColumn";
            ReleaseDateColumn.ReadOnly = true;
            ReleaseDateColumn.Width = 125;
            // 
            // MovieInfo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(597, 450);
            Controls.Add(MovieDataGridView);
            Controls.Add(label3);
            Controls.Add(DescriptionTextBox);
            Controls.Add(DateDurationLabel);
            Controls.Add(TitleLabel);
            Controls.Add(pictureBox1);
            Name = "MovieInfo";
            Text = "MovieInfo";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)MovieDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label TitleLabel;
        private Label DateDurationLabel;
        private TextBox DescriptionTextBox;
        private Label label3;
        private DataGridView MovieDataGridView;
        private DataGridViewTextBoxColumn IdColumn;
        private DataGridViewTextBoxColumn TitleColumn;
        private DataGridViewTextBoxColumn ReleaseDateColumn;
    }
}
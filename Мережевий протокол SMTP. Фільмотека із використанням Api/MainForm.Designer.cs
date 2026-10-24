namespace HomeWorkWinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            GenreComboBox = new ComboBox();
            SearchTextBox = new TextBox();
            SearchButton = new Button();
            PreviousButton = new Button();
            NextButton = new Button();
            PageInfoLabel = new Label();
            MovieDataGridView = new DataGridView();
            TitleColumn = new DataGridViewTextBoxColumn();
            IdColumn = new DataGridViewTextBoxColumn();
            ReleaseDateColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)MovieDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 46);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 0;
            label1.Text = "Movies";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(268, 46);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 2;
            label2.Text = "Genres:";
            // 
            // GenreComboBox
            // 
            GenreComboBox.FormattingEnabled = true;
            GenreComboBox.Location = new Point(331, 42);
            GenreComboBox.Name = "GenreComboBox";
            GenreComboBox.Size = new Size(180, 28);
            GenreComboBox.TabIndex = 3;
            // 
            // SearchTextBox
            // 
            SearchTextBox.Location = new Point(12, 10);
            SearchTextBox.Name = "SearchTextBox";
            SearchTextBox.Size = new Size(387, 27);
            SearchTextBox.TabIndex = 4;
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(405, 12);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(106, 25);
            SearchButton.TabIndex = 5;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            // 
            // PreviousButton
            // 
            PreviousButton.Location = new Point(12, 413);
            PreviousButton.Name = "PreviousButton";
            PreviousButton.Size = new Size(106, 25);
            PreviousButton.TabIndex = 16;
            PreviousButton.Text = "Previous";
            PreviousButton.UseVisualStyleBackColor = true;
            // 
            // NextButton
            // 
            NextButton.Location = new Point(124, 413);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(106, 25);
            NextButton.TabIndex = 17;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            // 
            // PageInfoLabel
            // 
            PageInfoLabel.AutoSize = true;
            PageInfoLabel.Location = new Point(247, 415);
            PageInfoLabel.Name = "PageInfoLabel";
            PageInfoLabel.Size = new Size(101, 20);
            PageInfoLabel.TabIndex = 18;
            PageInfoLabel.Text = "Page 1 from 1";
            // 
            // MovieDataGridView
            // 
            MovieDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MovieDataGridView.Columns.AddRange(new DataGridViewColumn[] { TitleColumn, IdColumn, ReleaseDateColumn });
            MovieDataGridView.Location = new Point(12, 76);
            MovieDataGridView.Name = "MovieDataGridView";
            MovieDataGridView.RowHeadersWidth = 51;
            MovieDataGridView.Size = new Size(496, 332);
            MovieDataGridView.TabIndex = 19;
            // 
            // TitleColumn
            // 
            TitleColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            TitleColumn.DataPropertyName = "title";
            TitleColumn.Frozen = true;
            TitleColumn.HeaderText = "Title";
            TitleColumn.MinimumWidth = 6;
            TitleColumn.Name = "TitleColumn";
            TitleColumn.ReadOnly = true;
            TitleColumn.Width = 318;
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
            // ReleaseDateColumn
            // 
            ReleaseDateColumn.DataPropertyName = "release_date";
            ReleaseDateColumn.HeaderText = "Release Date";
            ReleaseDateColumn.MinimumWidth = 6;
            ReleaseDateColumn.Name = "ReleaseDateColumn";
            ReleaseDateColumn.ReadOnly = true;
            ReleaseDateColumn.Width = 125;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 450);
            Controls.Add(MovieDataGridView);
            Controls.Add(PageInfoLabel);
            Controls.Add(NextButton);
            Controls.Add(PreviousButton);
            Controls.Add(SearchButton);
            Controls.Add(SearchTextBox);
            Controls.Add(GenreComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)MovieDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox GenreComboBox;
        private TextBox SearchTextBox;
        private Button SearchButton;
        private Button PreviousButton;
        private Button NextButton;
        private Label PageInfoLabel;
        private DataGridView MovieDataGridView;
        private DataGridViewTextBoxColumn TitleColumn;
        private DataGridViewTextBoxColumn IdColumn;
        private DataGridViewTextBoxColumn ReleaseDateColumn;
    }
}

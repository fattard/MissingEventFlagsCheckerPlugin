namespace MissingEventFlagsCheckerPlugin.Forms
{
    partial class ChecklistViewer
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
            dataGridView = new DataGridView();
            dgv_ref = new DataGridViewTextBoxColumn();
            dgv_completed = new DataGridViewCheckBoxColumn();
            dgv_category = new DataGridViewTextBoxColumn();
            dgv_location = new DataGridViewTextBoxColumn();
            dgv_txtDesc = new DataGridViewTextBoxColumn();
            showOnlyMarkedChk = new CheckBox();
            showOnlyUnmarkedChk = new CheckBox();
            filterBySearchChk = new CheckBox();
            searchTermBox = new TextBox();
            exportCurrentViewBtn = new Button();
            exportFullChecklistBtn = new Button();
            exportMissingEventsBtn = new Button();
            showTimedEventsChk = new CheckBox();
            closeBtn = new Button();
            totalsLabel = new Label();
            totalsCountTxt = new Label();
            getFlagsEditorExLinkLabel = new LinkLabel();
            editEventsBtn = new Button();
            filterByCategoryCombo = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { dgv_ref, dgv_completed, dgv_category, dgv_location, dgv_txtDesc });
            dataGridView.Location = new Point(14, 33);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(933, 425);
            dataGridView.TabIndex = 0;
            // 
            // dgv_ref
            // 
            dgv_ref.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_ref.HeaderText = "Ref";
            dgv_ref.Name = "dgv_ref";
            dgv_ref.ReadOnly = true;
            dgv_ref.Width = 49;
            // 
            // dgv_completed
            // 
            dgv_completed.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_completed.HeaderText = "Completed";
            dgv_completed.Name = "dgv_completed";
            dgv_completed.ReadOnly = true;
            dgv_completed.Width = 72;
            // 
            // dgv_category
            // 
            dgv_category.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_category.HeaderText = "Category";
            dgv_category.Name = "dgv_category";
            dgv_category.ReadOnly = true;
            dgv_category.Width = 80;
            // 
            // dgv_location
            // 
            dgv_location.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_location.HeaderText = "Location";
            dgv_location.Name = "dgv_location";
            dgv_location.ReadOnly = true;
            dgv_location.Width = 78;
            // 
            // dgv_txtDesc
            // 
            dgv_txtDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_txtDesc.HeaderText = "Description";
            dgv_txtDesc.Name = "dgv_txtDesc";
            dgv_txtDesc.ReadOnly = true;
            dgv_txtDesc.Width = 92;
            // 
            // showOnlyMarkedChk
            // 
            showOnlyMarkedChk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            showOnlyMarkedChk.AutoSize = true;
            showOnlyMarkedChk.Location = new Point(18, 475);
            showOnlyMarkedChk.Name = "showOnlyMarkedChk";
            showOnlyMarkedChk.Size = new Size(141, 19);
            showOnlyMarkedChk.TabIndex = 1;
            showOnlyMarkedChk.Text = "Show only completed";
            showOnlyMarkedChk.UseVisualStyleBackColor = true;
            showOnlyMarkedChk.CheckedChanged += ShowOnlyMarkedChk_CheckedChanged;
            // 
            // showOnlyUnmarkedChk
            // 
            showOnlyUnmarkedChk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            showOnlyUnmarkedChk.AutoSize = true;
            showOnlyUnmarkedChk.Location = new Point(18, 508);
            showOnlyUnmarkedChk.Name = "showOnlyUnmarkedChk";
            showOnlyUnmarkedChk.Size = new Size(162, 19);
            showOnlyUnmarkedChk.TabIndex = 2;
            showOnlyUnmarkedChk.Text = "Show only not completed";
            showOnlyUnmarkedChk.UseVisualStyleBackColor = true;
            showOnlyUnmarkedChk.CheckedChanged += ShowOnlyUnmarkedChk_CheckedChanged;
            // 
            // filterBySearchChk
            // 
            filterBySearchChk.Anchor = AnchorStyles.Bottom;
            filterBySearchChk.AutoSize = true;
            filterBySearchChk.Location = new Point(309, 514);
            filterBySearchChk.Name = "filterBySearchChk";
            filterBySearchChk.Size = new Size(133, 19);
            filterBySearchChk.TabIndex = 5;
            filterBySearchChk.Text = "Filter by search term";
            filterBySearchChk.UseVisualStyleBackColor = true;
            filterBySearchChk.CheckedChanged += FilterBySearchChk_CheckedChanged;
            // 
            // searchTermBox
            // 
            searchTermBox.Anchor = AnchorStyles.Bottom;
            searchTermBox.Location = new Point(309, 539);
            searchTermBox.Name = "searchTermBox";
            searchTermBox.Size = new Size(250, 23);
            searchTermBox.TabIndex = 6;
            searchTermBox.KeyDown += SearchTermBox_KeyDown;
            // 
            // exportCurrentViewBtn
            // 
            exportCurrentViewBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exportCurrentViewBtn.Location = new Point(599, 470);
            exportCurrentViewBtn.Name = "exportCurrentViewBtn";
            exportCurrentViewBtn.Size = new Size(210, 27);
            exportCurrentViewBtn.TabIndex = 7;
            exportCurrentViewBtn.Text = "Export current view";
            exportCurrentViewBtn.UseVisualStyleBackColor = true;
            exportCurrentViewBtn.Click += ExportCurrentViewBtn_Click;
            // 
            // exportFullChecklistBtn
            // 
            exportFullChecklistBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exportFullChecklistBtn.Location = new Point(599, 503);
            exportFullChecklistBtn.Name = "exportFullChecklistBtn";
            exportFullChecklistBtn.Size = new Size(210, 27);
            exportFullChecklistBtn.TabIndex = 8;
            exportFullChecklistBtn.Text = "Export Full Checklist";
            exportFullChecklistBtn.UseVisualStyleBackColor = true;
            exportFullChecklistBtn.Click += ExportFullChecklistBtn_Click;
            // 
            // exportMissingEventsBtn
            // 
            exportMissingEventsBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exportMissingEventsBtn.Location = new Point(599, 536);
            exportMissingEventsBtn.Name = "exportMissingEventsBtn";
            exportMissingEventsBtn.Size = new Size(210, 27);
            exportMissingEventsBtn.TabIndex = 9;
            exportMissingEventsBtn.Text = "Export only missing events";
            exportMissingEventsBtn.UseVisualStyleBackColor = true;
            exportMissingEventsBtn.Click += ExportMissingEventsBtn_Click;
            // 
            // showTimedEventsChk
            // 
            showTimedEventsChk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            showTimedEventsChk.AutoSize = true;
            showTimedEventsChk.Checked = true;
            showTimedEventsChk.CheckState = CheckState.Checked;
            showTimedEventsChk.Location = new Point(18, 541);
            showTimedEventsChk.Name = "showTimedEventsChk";
            showTimedEventsChk.Size = new Size(164, 19);
            showTimedEventsChk.TabIndex = 3;
            showTimedEventsChk.Text = "Show Daily/Weekly events";
            showTimedEventsChk.UseVisualStyleBackColor = true;
            showTimedEventsChk.CheckedChanged += ShowTimedEventsChk_CheckedChanged;
            // 
            // closeBtn
            // 
            closeBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeBtn.Location = new Point(837, 536);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(110, 27);
            closeBtn.TabIndex = 10;
            closeBtn.Text = "Close";
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += CloseBtn_Click;
            // 
            // totalsLabel
            // 
            totalsLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            totalsLabel.AutoSize = true;
            totalsLabel.Location = new Point(12, 8);
            totalsLabel.Name = "totalsLabel";
            totalsLabel.Size = new Size(40, 15);
            totalsLabel.Text = "Totals:";
            // 
            // totalsCountTxt
            // 
            totalsCountTxt.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            totalsCountTxt.AutoSize = true;
            totalsCountTxt.Location = new Point(70, 8);
            totalsCountTxt.Name = "totalsCountTxt";
            totalsCountTxt.Size = new Size(48, 15);
            totalsCountTxt.Text = "000/000";
            // 
            // getFlagsEditorExLinkLabel
            // 
            getFlagsEditorExLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            getFlagsEditorExLinkLabel.Location = new Point(657, 8);
            getFlagsEditorExLinkLabel.Name = "getFlagsEditorExLinkLabel";
            getFlagsEditorExLinkLabel.Size = new Size(180, 15);
            getFlagsEditorExLinkLabel.TabIndex = 11;
            getFlagsEditorExLinkLabel.TabStop = true;
            getFlagsEditorExLinkLabel.Text = "Download FlagsEditorEX";
            getFlagsEditorExLinkLabel.TextAlign = ContentAlignment.TopRight;
            getFlagsEditorExLinkLabel.LinkClicked += GetFlagsEditorExLinkLabel_LinkClicked;
            // 
            // editEventsBtn
            // 
            editEventsBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editEventsBtn.Enabled = false;
            editEventsBtn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editEventsBtn.Location = new Point(837, 4);
            editEventsBtn.Name = "editEventsBtn";
            editEventsBtn.Size = new Size(110, 23);
            editEventsBtn.TabIndex = 12;
            editEventsBtn.Text = "Edit Events";
            editEventsBtn.UseVisualStyleBackColor = true;
            editEventsBtn.Click += EditEventsBtn_Click;
            // 
            // filterByCategoryCombo
            // 
            filterByCategoryCombo.Anchor = AnchorStyles.Bottom;
            filterByCategoryCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            filterByCategoryCombo.FormattingEnabled = true;
            filterByCategoryCombo.Location = new Point(309, 473);
            filterByCategoryCombo.Name = "filterByCategoryCombo";
            filterByCategoryCombo.Size = new Size(250, 23);
            filterByCategoryCombo.TabIndex = 4;
            filterByCategoryCombo.SelectedIndexChanged += FilterByCategoryCombo_SelectedIndexChanged;
            // 
            // ChecklistViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 570);
            Controls.Add(filterByCategoryCombo);
            Controls.Add(editEventsBtn);
            Controls.Add(getFlagsEditorExLinkLabel);
            Controls.Add(totalsCountTxt);
            Controls.Add(totalsLabel);
            Controls.Add(closeBtn);
            Controls.Add(showTimedEventsChk);
            Controls.Add(exportMissingEventsBtn);
            Controls.Add(exportFullChecklistBtn);
            Controls.Add(exportCurrentViewBtn);
            Controls.Add(searchTermBox);
            Controls.Add(filterBySearchChk);
            Controls.Add(showOnlyUnmarkedChk);
            Controls.Add(showOnlyMarkedChk);
            Controls.Add(dataGridView);
            MaximizeBox = false;
            MaximumSize = new Size(977, 609);
            MinimumSize = new Size(977, 609);
            Name = "ChecklistViewer";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Checklist Viewer";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView;
        private CheckBox showOnlyMarkedChk;
        private CheckBox showOnlyUnmarkedChk;
        private CheckBox filterBySearchChk;
        private TextBox searchTermBox;
        private Button exportCurrentViewBtn;
        private Button exportFullChecklistBtn;
        private Button exportMissingEventsBtn;
        private CheckBox showTimedEventsChk;
        private Button closeBtn;
        private Label totalsLabel;
        private Label totalsCountTxt;
        private LinkLabel getFlagsEditorExLinkLabel;
        private Button editEventsBtn;
        private ComboBox filterByCategoryCombo;
        private DataGridViewTextBoxColumn dgv_ref;
        private DataGridViewCheckBoxColumn dgv_completed;
        private DataGridViewTextBoxColumn dgv_category;
        private DataGridViewTextBoxColumn dgv_location;
        private DataGridViewTextBoxColumn dgv_txtDesc;
    }
}
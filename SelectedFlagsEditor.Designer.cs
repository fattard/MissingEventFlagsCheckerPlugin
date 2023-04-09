
namespace MissingEventFlagsCheckerPlugin
{
    partial class SelectedFlagsEditor
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
            this.markFlagsBtn = new System.Windows.Forms.Button();
            this.unmarkFlagsBtn = new System.Windows.Forms.Button();
            this.fieldItemsChk = new System.Windows.Forms.CheckBox();
            this.hiddenItemsChk = new System.Windows.Forms.CheckBox();
            this.trainerBattlesChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // markFlagsBtn
            // 
            this.markFlagsBtn.Location = new System.Drawing.Point(30, 270);
            this.markFlagsBtn.Name = "markFlagsBtn";
            this.markFlagsBtn.Size = new System.Drawing.Size(125, 23);
            this.markFlagsBtn.TabIndex = 0;
            this.markFlagsBtn.Text = "Mark Selected Flags";
            this.markFlagsBtn.UseVisualStyleBackColor = true;
            this.markFlagsBtn.Click += new System.EventHandler(this.markFlagsBtn_Click);
            // 
            // unmarkFlagsBtn
            // 
            this.unmarkFlagsBtn.Location = new System.Drawing.Point(180, 270);
            this.unmarkFlagsBtn.Name = "unmarkFlagsBtn";
            this.unmarkFlagsBtn.Size = new System.Drawing.Size(125, 23);
            this.unmarkFlagsBtn.TabIndex = 1;
            this.unmarkFlagsBtn.Text = "Unmark Selected Flags";
            this.unmarkFlagsBtn.UseVisualStyleBackColor = true;
            this.unmarkFlagsBtn.Click += new System.EventHandler(this.unmarkFlagsBtn_Click);
            // 
            // fieldItemsChk
            // 
            this.fieldItemsChk.AutoSize = true;
            this.fieldItemsChk.Location = new System.Drawing.Point(126, 62);
            this.fieldItemsChk.Name = "fieldItemsChk";
            this.fieldItemsChk.Size = new System.Drawing.Size(76, 17);
            this.fieldItemsChk.TabIndex = 2;
            this.fieldItemsChk.Text = "Field Items";
            this.fieldItemsChk.UseVisualStyleBackColor = true;
            // 
            // hiddenItemsChk
            // 
            this.hiddenItemsChk.AutoSize = true;
            this.hiddenItemsChk.Location = new System.Drawing.Point(126, 85);
            this.hiddenItemsChk.Name = "hiddenItemsChk";
            this.hiddenItemsChk.Size = new System.Drawing.Size(88, 17);
            this.hiddenItemsChk.TabIndex = 3;
            this.hiddenItemsChk.Text = "Hidden Items";
            this.hiddenItemsChk.UseVisualStyleBackColor = true;
            // 
            // trainerBattlesChk
            // 
            this.trainerBattlesChk.AutoSize = true;
            this.trainerBattlesChk.Location = new System.Drawing.Point(126, 109);
            this.trainerBattlesChk.Name = "trainerBattlesChk";
            this.trainerBattlesChk.Size = new System.Drawing.Size(64, 17);
            this.trainerBattlesChk.TabIndex = 4;
            this.trainerBattlesChk.Text = "Trainers";
            this.trainerBattlesChk.UseVisualStyleBackColor = true;
            // 
            // SelectedFlagsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.trainerBattlesChk);
            this.Controls.Add(this.hiddenItemsChk);
            this.Controls.Add(this.fieldItemsChk);
            this.Controls.Add(this.unmarkFlagsBtn);
            this.Controls.Add(this.markFlagsBtn);
            this.Name = "SelectedFlagsEditor";
            this.Text = "Edit Selected Flags";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button markFlagsBtn;
        private System.Windows.Forms.Button unmarkFlagsBtn;
        private System.Windows.Forms.CheckBox fieldItemsChk;
        private System.Windows.Forms.CheckBox hiddenItemsChk;
        private System.Windows.Forms.CheckBox trainerBattlesChk;
    }
}
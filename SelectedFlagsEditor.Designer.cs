
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
            this.giftsChk = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.miscEventsChk = new System.Windows.Forms.CheckBox();
            this.berryTreesChk = new System.Windows.Forms.CheckBox();
            this.sideEventsChk = new System.Windows.Forms.CheckBox();
            this.inGameTradesChk = new System.Windows.Forms.CheckBox();
            this.staticEncounterChk = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // markFlagsBtn
            // 
            this.markFlagsBtn.Location = new System.Drawing.Point(30, 270);
            this.markFlagsBtn.Name = "markFlagsBtn";
            this.markFlagsBtn.Size = new System.Drawing.Size(125, 23);
            this.markFlagsBtn.TabIndex = 10;
            this.markFlagsBtn.Text = "Mark Selected Flags";
            this.markFlagsBtn.UseVisualStyleBackColor = true;
            this.markFlagsBtn.Click += new System.EventHandler(this.markFlagsBtn_Click);
            // 
            // unmarkFlagsBtn
            // 
            this.unmarkFlagsBtn.Location = new System.Drawing.Point(180, 270);
            this.unmarkFlagsBtn.Name = "unmarkFlagsBtn";
            this.unmarkFlagsBtn.Size = new System.Drawing.Size(125, 23);
            this.unmarkFlagsBtn.TabIndex = 11;
            this.unmarkFlagsBtn.Text = "Unmark Selected Flags";
            this.unmarkFlagsBtn.UseVisualStyleBackColor = true;
            this.unmarkFlagsBtn.Click += new System.EventHandler(this.unmarkFlagsBtn_Click);
            // 
            // fieldItemsChk
            // 
            this.fieldItemsChk.AutoSize = true;
            this.fieldItemsChk.Location = new System.Drawing.Point(6, 10);
            this.fieldItemsChk.Name = "fieldItemsChk";
            this.fieldItemsChk.Size = new System.Drawing.Size(76, 17);
            this.fieldItemsChk.TabIndex = 1;
            this.fieldItemsChk.Text = "Field Items";
            this.fieldItemsChk.UseVisualStyleBackColor = true;
            // 
            // hiddenItemsChk
            // 
            this.hiddenItemsChk.AutoSize = true;
            this.hiddenItemsChk.Location = new System.Drawing.Point(6, 33);
            this.hiddenItemsChk.Name = "hiddenItemsChk";
            this.hiddenItemsChk.Size = new System.Drawing.Size(88, 17);
            this.hiddenItemsChk.TabIndex = 2;
            this.hiddenItemsChk.Text = "Hidden Items";
            this.hiddenItemsChk.UseVisualStyleBackColor = true;
            // 
            // trainerBattlesChk
            // 
            this.trainerBattlesChk.AutoSize = true;
            this.trainerBattlesChk.Location = new System.Drawing.Point(6, 79);
            this.trainerBattlesChk.Name = "trainerBattlesChk";
            this.trainerBattlesChk.Size = new System.Drawing.Size(64, 17);
            this.trainerBattlesChk.TabIndex = 4;
            this.trainerBattlesChk.Text = "Trainers";
            this.trainerBattlesChk.UseVisualStyleBackColor = true;
            // 
            // giftsChk
            // 
            this.giftsChk.AutoSize = true;
            this.giftsChk.Location = new System.Drawing.Point(6, 56);
            this.giftsChk.Name = "giftsChk";
            this.giftsChk.Size = new System.Drawing.Size(47, 17);
            this.giftsChk.TabIndex = 3;
            this.giftsChk.Text = "Gifts";
            this.giftsChk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.miscEventsChk);
            this.groupBox1.Controls.Add(this.berryTreesChk);
            this.groupBox1.Controls.Add(this.sideEventsChk);
            this.groupBox1.Controls.Add(this.inGameTradesChk);
            this.groupBox1.Controls.Add(this.staticEncounterChk);
            this.groupBox1.Controls.Add(this.giftsChk);
            this.groupBox1.Controls.Add(this.fieldItemsChk);
            this.groupBox1.Controls.Add(this.trainerBattlesChk);
            this.groupBox1.Controls.Add(this.hiddenItemsChk);
            this.groupBox1.Location = new System.Drawing.Point(85, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 217);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // miscEventsChk
            // 
            this.miscEventsChk.AutoSize = true;
            this.miscEventsChk.Location = new System.Drawing.Point(7, 173);
            this.miscEventsChk.Name = "miscEventsChk";
            this.miscEventsChk.Size = new System.Drawing.Size(87, 17);
            this.miscEventsChk.TabIndex = 8;
            this.miscEventsChk.Text = "Misc. Events";
            this.miscEventsChk.UseVisualStyleBackColor = true;
            // 
            // berryTreesChk
            // 
            this.berryTreesChk.AutoSize = true;
            this.berryTreesChk.Location = new System.Drawing.Point(7, 196);
            this.berryTreesChk.Name = "berryTreesChk";
            this.berryTreesChk.Size = new System.Drawing.Size(80, 17);
            this.berryTreesChk.TabIndex = 9;
            this.berryTreesChk.Text = "Berry Trees";
            this.berryTreesChk.UseVisualStyleBackColor = true;
            // 
            // sideEventsChk
            // 
            this.sideEventsChk.AutoSize = true;
            this.sideEventsChk.Location = new System.Drawing.Point(7, 150);
            this.sideEventsChk.Name = "sideEventsChk";
            this.sideEventsChk.Size = new System.Drawing.Size(83, 17);
            this.sideEventsChk.TabIndex = 7;
            this.sideEventsChk.Text = "Side Events";
            this.sideEventsChk.UseVisualStyleBackColor = true;
            // 
            // inGameTradesChk
            // 
            this.inGameTradesChk.AutoSize = true;
            this.inGameTradesChk.Location = new System.Drawing.Point(7, 127);
            this.inGameTradesChk.Name = "inGameTradesChk";
            this.inGameTradesChk.Size = new System.Drawing.Size(102, 17);
            this.inGameTradesChk.TabIndex = 6;
            this.inGameTradesChk.Text = "In-Game Trades";
            this.inGameTradesChk.UseVisualStyleBackColor = true;
            // 
            // staticEncounterChk
            // 
            this.staticEncounterChk.AutoSize = true;
            this.staticEncounterChk.Location = new System.Drawing.Point(7, 103);
            this.staticEncounterChk.Name = "staticEncounterChk";
            this.staticEncounterChk.Size = new System.Drawing.Size(88, 17);
            this.staticEncounterChk.TabIndex = 5;
            this.staticEncounterChk.Text = "Static Battles";
            this.staticEncounterChk.UseVisualStyleBackColor = true;
            // 
            // SelectedFlagsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.unmarkFlagsBtn);
            this.Controls.Add(this.markFlagsBtn);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectedFlagsEditor";
            this.Text = "Edit Selected Flags";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button markFlagsBtn;
        private System.Windows.Forms.Button unmarkFlagsBtn;
        private System.Windows.Forms.CheckBox fieldItemsChk;
        private System.Windows.Forms.CheckBox hiddenItemsChk;
        private System.Windows.Forms.CheckBox trainerBattlesChk;
        private System.Windows.Forms.CheckBox giftsChk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox staticEncounterChk;
        private System.Windows.Forms.CheckBox miscEventsChk;
        private System.Windows.Forms.CheckBox berryTreesChk;
        private System.Windows.Forms.CheckBox sideEventsChk;
        private System.Windows.Forms.CheckBox inGameTradesChk;
    }
}
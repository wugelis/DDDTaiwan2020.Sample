namespace CleanArchitectureCQRSTemplate.Forms
{
    partial class frmApplicationLayerWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApplicationLayerWizard));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkQueryCommand = new System.Windows.Forms.CheckBox();
            this.chkCreateCommand = new System.Windows.Forms.CheckBox();
            this.chkDeleteCommand = new System.Windows.Forms.CheckBox();
            this.chkUpdateCommand = new System.Windows.Forms.CheckBox();
            this.txtQueryCommandName = new System.Windows.Forms.TextBox();
            this.btnQueryDTO = new System.Windows.Forms.Button();
            this.txtQueryDtoName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.SeaGreen;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ForeColor = System.Drawing.SystemColors.Control;
            this.btnOK.Location = new System.Drawing.Point(459, 326);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(115, 40);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "(&O) 確定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.CloseWindow);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Location = new System.Drawing.Point(580, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 38);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "(&C) 取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.CloseWindow);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Command (結尾)";
            // 
            // chkQueryCommand
            // 
            this.chkQueryCommand.AutoSize = true;
            this.chkQueryCommand.Checked = true;
            this.chkQueryCommand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQueryCommand.ForeColor = System.Drawing.Color.White;
            this.chkQueryCommand.Location = new System.Drawing.Point(204, 199);
            this.chkQueryCommand.Name = "chkQueryCommand";
            this.chkQueryCommand.Size = new System.Drawing.Size(132, 16);
            this.chkQueryCommand.TabIndex = 3;
            this.chkQueryCommand.Text = "建立 Query Command";
            this.chkQueryCommand.UseVisualStyleBackColor = true;
            // 
            // chkCreateCommand
            // 
            this.chkCreateCommand.AutoSize = true;
            this.chkCreateCommand.Location = new System.Drawing.Point(204, 231);
            this.chkCreateCommand.Name = "chkCreateCommand";
            this.chkCreateCommand.Size = new System.Drawing.Size(130, 16);
            this.chkCreateCommand.TabIndex = 4;
            this.chkCreateCommand.Text = "建立 CreateCommand";
            this.chkCreateCommand.UseVisualStyleBackColor = true;
            // 
            // chkDeleteCommand
            // 
            this.chkDeleteCommand.AutoSize = true;
            this.chkDeleteCommand.Location = new System.Drawing.Point(204, 263);
            this.chkDeleteCommand.Name = "chkDeleteCommand";
            this.chkDeleteCommand.Size = new System.Drawing.Size(129, 16);
            this.chkDeleteCommand.TabIndex = 5;
            this.chkDeleteCommand.Text = "建議 DeleteCommand";
            this.chkDeleteCommand.UseVisualStyleBackColor = true;
            // 
            // chkUpdateCommand
            // 
            this.chkUpdateCommand.AutoSize = true;
            this.chkUpdateCommand.Location = new System.Drawing.Point(204, 294);
            this.chkUpdateCommand.Name = "chkUpdateCommand";
            this.chkUpdateCommand.Size = new System.Drawing.Size(133, 16);
            this.chkUpdateCommand.TabIndex = 6;
            this.chkUpdateCommand.Text = "建立 UpdateCommand";
            this.chkUpdateCommand.UseVisualStyleBackColor = true;
            // 
            // txtQueryCommandName
            // 
            this.txtQueryCommandName.Location = new System.Drawing.Point(204, 81);
            this.txtQueryCommandName.Name = "txtQueryCommandName";
            this.txtQueryCommandName.Size = new System.Drawing.Size(163, 22);
            this.txtQueryCommandName.TabIndex = 7;
            this.txtQueryCommandName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnQueryDTO
            // 
            this.btnQueryDTO.BackColor = System.Drawing.Color.Black;
            this.btnQueryDTO.Location = new System.Drawing.Point(204, 118);
            this.btnQueryDTO.Name = "btnQueryDTO";
            this.btnQueryDTO.Size = new System.Drawing.Size(162, 27);
            this.btnQueryDTO.TabIndex = 8;
            this.btnQueryDTO.Text = "建立 Query Command DTO";
            this.btnQueryDTO.UseVisualStyleBackColor = false;
            this.btnQueryDTO.Click += new System.EventHandler(this.btnQueryDTO_Click);
            // 
            // txtQueryDtoName
            // 
            this.txtQueryDtoName.Location = new System.Drawing.Point(204, 160);
            this.txtQueryDtoName.Name = "txtQueryDtoName";
            this.txtQueryDtoName.Size = new System.Drawing.Size(162, 22);
            this.txtQueryDtoName.TabIndex = 9;
            this.txtQueryDtoName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(372, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Dto (結尾)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(23, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(693, 29);
            this.label3.TabIndex = 11;
            this.label3.Text = "Clean Architecture (CQRS) C# Project Templates 樣板精靈";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "1. 建立 CQRS  IRequestHandler：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "2. 建立 DTO 物件";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "3. 為 DTO 取一個名子";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "4. 建立 CQRS Command";
            // 
            // frmApplicationLayerWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(742, 394);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtQueryDtoName);
            this.Controls.Add(this.btnQueryDTO);
            this.Controls.Add(this.txtQueryCommandName);
            this.Controls.Add(this.chkUpdateCommand);
            this.Controls.Add(this.chkDeleteCommand);
            this.Controls.Add(this.chkCreateCommand);
            this.Controls.Add(this.chkQueryCommand);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmApplicationLayerWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "建立 Clean Architecture (CQRS) 的 Application 層";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkQueryCommand;
        private System.Windows.Forms.CheckBox chkCreateCommand;
        private System.Windows.Forms.CheckBox chkDeleteCommand;
        private System.Windows.Forms.CheckBox chkUpdateCommand;
        private System.Windows.Forms.TextBox txtQueryCommandName;
        private System.Windows.Forms.Button btnQueryDTO;
        private System.Windows.Forms.TextBox txtQueryDtoName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
    }
}
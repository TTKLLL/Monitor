namespace WF
{
    partial class AddDevice
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DeviceIdText = new System.Windows.Forms.TextBox();
            this.TypeText = new System.Windows.Forms.TextBox();
            this.CompanyText = new System.Windows.Forms.TextBox();
            this.InfoText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "传感器编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "传感器类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "生产厂商：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "传感器信息：";
            // 
            // DeviceIdText
            // 
            this.DeviceIdText.Location = new System.Drawing.Point(176, 65);
            this.DeviceIdText.Name = "DeviceIdText";
            this.DeviceIdText.Size = new System.Drawing.Size(100, 21);
            this.DeviceIdText.TabIndex = 4;
            // 
            // TypeText
            // 
            this.TypeText.Location = new System.Drawing.Point(176, 101);
            this.TypeText.Name = "TypeText";
            this.TypeText.Size = new System.Drawing.Size(100, 21);
            this.TypeText.TabIndex = 5;
            // 
            // CompanyText
            // 
            this.CompanyText.Location = new System.Drawing.Point(176, 136);
            this.CompanyText.Name = "CompanyText";
            this.CompanyText.Size = new System.Drawing.Size(100, 21);
            this.CompanyText.TabIndex = 6;
            // 
            // InfoText
            // 
            this.InfoText.Location = new System.Drawing.Point(176, 164);
            this.InfoText.Name = "InfoText";
            this.InfoText.Size = new System.Drawing.Size(100, 21);
            this.InfoText.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(201, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 298);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.InfoText);
            this.Controls.Add(this.CompanyText);
            this.Controls.Add(this.TypeText);
            this.Controls.Add(this.DeviceIdText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddDevice";
            this.Text = "添加传感器";
            this.Load += new System.EventHandler(this.AddDevice_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddDevice_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DeviceIdText;
        private System.Windows.Forms.TextBox TypeText;
        private System.Windows.Forms.TextBox CompanyText;
        private System.Windows.Forms.TextBox InfoText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
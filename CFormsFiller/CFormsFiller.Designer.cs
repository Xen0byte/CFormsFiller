namespace CFormsFiller
{
    partial class CFormsFiller
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(488, 31);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "This tool generates sequential custom SNOMED codes for unique clinical statements" +
    ".\r\nThe first 10 digits of the code are user-defined, and the last 4 digits are a" +
    "scending from 0001 to 9999.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seed:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(50, 72);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(85, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "NULL";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "PLACEHOLDER";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(12, 49);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(488, 13);
            this.textBox3.TabIndex = 4;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "Clinical statements are stored as XML, so duplicate entries will have the same cu" +
    "stom SNOMED code.";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textBox4.Location = new System.Drawing.Point(12, 104);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox4.Size = new System.Drawing.Size(1104, 380);
            this.textBox4.TabIndex = 5;
            this.textBox4.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(578, 496);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 44);
            this.button1.TabIndex = 6;
            this.button1.Text = "Read Clinical Form\r\n(Stores Clinical Statements as XML)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(850, 496);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(266, 44);
            this.button2.TabIndex = 7;
            this.button2.Text = "Write Clinical Form\r\n(Assigns Custom SNOMED Codes)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1094, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(22, 22);
            this.button3.TabIndex = 8;
            this.button3.TabStop = false;
            this.button3.Text = "!";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // CFormsFiller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 552);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "CFormsFiller";
            this.Text = "CFormFiller";
            this.Load += new System.EventHandler(this.ConsoleBox);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}


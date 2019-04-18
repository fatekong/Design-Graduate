namespace Demo
{
    partial class ZoomToCoordinatesDialog
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
            this.label4 = new System.Windows.Forms.Label();
            this.lonStatus = new System.Windows.Forms.Label();
            this.latStatus = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.d2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.d1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.BT_Accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(245, 17);
            this.label4.TabIndex = 37;
            this.label4.Text = "Please enter the desired coordinates:";
            // 
            // lonStatus
            // 
            this.lonStatus.AutoSize = true;
            this.lonStatus.Location = new System.Drawing.Point(39, 131);
            this.lonStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lonStatus.Name = "lonStatus";
            this.lonStatus.Size = new System.Drawing.Size(88, 17);
            this.lonStatus.TabIndex = 36;
            this.lonStatus.Text = "longWarning";
            // 
            // latStatus
            // 
            this.latStatus.AutoSize = true;
            this.latStatus.Location = new System.Drawing.Point(39, 76);
            this.latStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.latStatus.Name = "latStatus";
            this.latStatus.Size = new System.Drawing.Size(76, 17);
            this.latStatus.TabIndex = 35;
            this.latStatus.Text = "latWarning";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(282, 102);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 17);
            this.label10.TabIndex = 34;
            this.label10.Text = "°";
            // 
            // d2
            // 
            this.d2.Location = new System.Drawing.Point(159, 102);
            this.d2.Margin = new System.Windows.Forms.Padding(4);
            this.d2.Name = "d2";
            this.d2.Size = new System.Drawing.Size(120, 22);
            this.d2.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 17);
            this.label3.TabIndex = 33;
            this.label3.Text = "°";
            // 
            // d1
            // 
            this.d1.Location = new System.Drawing.Point(159, 47);
            this.d1.Margin = new System.Windows.Forms.Padding(4);
            this.d1.Name = "d1";
            this.d1.Size = new System.Drawing.Size(120, 22);
            this.d1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 30;
            this.label2.Text = "Longitude";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 29;
            this.label1.Text = "Latitude";
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_Cancel.Location = new System.Drawing.Point(183, 157);
            this.BT_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(96, 37);
            this.BT_Cancel.TabIndex = 32;
            this.BT_Cancel.Text = "Cancel";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // BT_Accept
            // 
            this.BT_Accept.Location = new System.Drawing.Point(79, 157);
            this.BT_Accept.Margin = new System.Windows.Forms.Padding(4);
            this.BT_Accept.Name = "BT_Accept";
            this.BT_Accept.Size = new System.Drawing.Size(96, 37);
            this.BT_Accept.TabIndex = 31;
            this.BT_Accept.Text = "OK";
            this.BT_Accept.UseVisualStyleBackColor = true;
            this.BT_Accept.Click += new System.EventHandler(this.AcceptButtonClick);
            // 
            // ZoomToCoordinatesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 212);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lonStatus);
            this.Controls.Add(this.latStatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.d2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.d1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_Accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "ZoomToCoordinatesDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZoomToCoordinatesDialog";
            this.Load += new System.EventHandler(this.ZoomToCoordinatesDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lonStatus;
        private System.Windows.Forms.Label latStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox d2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox d1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.Button BT_Accept;
    }
}
namespace рисовалка
{
    partial class DocumentForm
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
            this.Plusbutton = new System.Windows.Forms.Button();
            this.Minusbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Plusbutton
            // 
            this.Plusbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Plusbutton.Location = new System.Drawing.Point(720, 415);
            this.Plusbutton.Name = "Plusbutton";
            this.Plusbutton.Size = new System.Drawing.Size(27, 23);
            this.Plusbutton.TabIndex = 0;
            this.Plusbutton.Text = "+";
            this.Plusbutton.UseVisualStyleBackColor = true;
            this.Plusbutton.Click += new System.EventHandler(this.Plusbutton_Click);
            // 
            // Minusbutton
            // 
            this.Minusbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Minusbutton.Location = new System.Drawing.Point(753, 415);
            this.Minusbutton.Name = "Minusbutton";
            this.Minusbutton.Size = new System.Drawing.Size(27, 23);
            this.Minusbutton.TabIndex = 1;
            this.Minusbutton.Text = "-";
            this.Minusbutton.UseVisualStyleBackColor = true;
            this.Minusbutton.Click += new System.EventHandler(this.Minusbutton_Click);
            // 
            // DocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Minusbutton);
            this.Controls.Add(this.Plusbutton);
            this.DoubleBuffered = true;
            this.Name = "DocumentForm";
            this.Text = "DocumentForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.DocumentForm_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DocumentForm_MouseDown_1);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DocumentForm_MouseMove_1);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DocumentForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Plusbutton;
        private System.Windows.Forms.Button Minusbutton;
    }
}
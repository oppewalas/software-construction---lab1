﻿namespace рисовалка
{
    partial class PluginsDialog
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
            this.listViewPlugins = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewPlugins
            // 
            this.listViewPlugins.HideSelection = false;
            this.listViewPlugins.Location = new System.Drawing.Point(2, 1);
            this.listViewPlugins.Name = "listViewPlugins";
            this.listViewPlugins.Size = new System.Drawing.Size(464, 453);
            this.listViewPlugins.TabIndex = 0;
            this.listViewPlugins.UseCompatibleStateImageBehavior = false;
            // 
            // PluginsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 452);
            this.Controls.Add(this.listViewPlugins);
            this.Name = "PluginsDialog";
            this.Text = "PluginsDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewPlugins;
    }
}